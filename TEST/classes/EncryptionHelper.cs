using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TEST.classes
{
    public class EncryptionHelper
    {
        #region Key Generation
        /// <summary>
        /// Generates a SHA-256 hash of the provided key to be used as an encryption key.
        /// </summary>
        /// <param name="key">The key to generate a SHA-256 hash from.</param>
        /// <returns>A byte array representing the SHA-256 hash of the key.</returns>
        private static byte[] GenerateKey(string key)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
        }
        #endregion

        #region Encryption
        /// <summary>
        /// Encrypts the provided plaintext using AES encryption and returns the encrypted string in Base64URL format.
        /// </summary>
        /// <param name="plainText">The plaintext to encrypt.</param>
        /// <param name="key">The key to use for encryption.</param>
        /// <returns>A Base64URL-encoded string representing the encrypted data.</returns>
        public static string EncryptData(string plainText, string key)
        {
            byte[] keyBytes = GenerateKey(key);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyBytes;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;
                aesAlg.GenerateIV();

                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                    byte[] combinedData = new byte[aesAlg.IV.Length + encryptedBytes.Length];
                    Array.Copy(aesAlg.IV, 0, combinedData, 0, aesAlg.IV.Length);
                    Array.Copy(encryptedBytes, 0, combinedData, aesAlg.IV.Length, encryptedBytes.Length);

                    return ToBase64Url(Convert.ToBase64String(combinedData)); // Convert to Base64URL format
                }
            }
        }
        #endregion

        #region Decryption
        /// <summary>
        /// Decrypts the provided ciphertext using AES decryption and returns the decrypted plaintext.
        /// </summary>
        /// <param name="cipherText">The encrypted text to decrypt (in Base64URL format).</param>
        /// <param name="key">The key to use for decryption.</param>
        /// <returns>The decrypted plaintext string.</returns>
        public static string DecryptData(string cipherText, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(cipherText))
                {
                    System.Diagnostics.Debug.WriteLine("❌ Error: cipherText is NULL or Empty!");
                    return null;
                }

                try
                {
                    cipherText = FromBase64Url(HttpUtility.UrlDecode(cipherText)); // Decode from Base64URL format
                    byte[] cipherBytes = Convert.FromBase64String(cipherText);
                    byte[] keyBytes = GenerateKey(key);

                    using (Aes aesAlg = Aes.Create())
                    {
                        aesAlg.Key = keyBytes;
                        aesAlg.Mode = CipherMode.CBC;
                        aesAlg.Padding = PaddingMode.PKCS7;

                        byte[] iv = new byte[16];
                        Array.Copy(cipherBytes, 0, iv, 0, iv.Length);
                        aesAlg.IV = iv;

                        byte[] encryptedData = new byte[cipherBytes.Length - iv.Length];
                        Array.Copy(cipherBytes, iv.Length, encryptedData, 0, encryptedData.Length);

                        using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                        {
                            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                            string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

                            if (string.IsNullOrEmpty(decryptedText))
                            {
                                System.Diagnostics.Debug.WriteLine("❌ Error: Decrypted text is NULL or Empty!");
                                return null;
                            }

                            System.Diagnostics.Debug.WriteLine($"✅ Decryption successful: {decryptedText}");
                            return decryptedText;
                        }
                    }
                }
                catch (FormatException fe)
                {
                    System.Diagnostics.Debug.WriteLine("❌ Base64 Format Exception: " + fe.Message);
                    return null;
                }
                catch (CryptographicException ce)
                {
                    System.Diagnostics.Debug.WriteLine("❌ Cryptographic Exception: " + ce.Message);
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("❌ Error decrypting: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region Base64/URL Conversion
        /// <summary>
        /// Converts a Base64 string to a Base64URL string (replacing "+" and "/" with URL-safe characters).
        /// </summary>
        /// <param name="base64">The Base64 string to convert.</param>
        /// <returns>A Base64URL string.</returns>
        public static string ToBase64Url(string base64)
        {
            return base64.Replace("+", "-").Replace("/", "_").TrimEnd('=');
        }

        /// <summary>
        /// Converts a Base64URL string back to a Base64 string (replacing URL-safe characters with Base64 characters).
        /// </summary>
        /// <param name="base64Url">The Base64URL string to convert.</param>
        /// <returns>A Base64 string.</returns>
        public static string FromBase64Url(string base64Url)
        {
            string base64 = base64Url.Replace("-", "+").Replace("_", "/");
            int padding = 4 - (base64.Length % 4);
            if (padding != 4) base64 = base64.PadRight(base64.Length + padding, '=');
            return base64;
        }
        #endregion
    }
}
