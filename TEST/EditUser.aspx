<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="TEST.EditUser" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphheader" runat="server">
    <style type="text/css">
        .form-container {
            width: 80%;
            margin: 0 auto;
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 8px;
        }

        h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-label {
            font-weight: bold;
            display: block;
            margin-bottom: 5px;
        }

        .form-input {
            width: 100%;
            padding: 8px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 14px;
        }

        .error-message {
            color: red;
            font-size: 12px;
        }

        /*        .form-button {
            display: block;
            width: 100%;
            padding: 10px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
        }

            .form-button:hover {
                background-color: #45a049;
            }*/
    </style>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="cphcontent" runat="server">
    <div class="form-container">
           <h2 id="title" runat="server">&nbsp;</h2>


        <asp:Label ID="lblError" runat="server" ForeColor="Red" CssClass="error-message" meta:resourcekey="lblErrorResource1"></asp:Label>

        <div class="form-group">
            <asp:Label ID="lblUserName" runat="server" Text="User Name:" CssClass="form-label" meta:resourcekey="lblUserNameResource1"></asp:Label>
            <asp:RequiredFieldValidator ValidationGroup="editvalidation" ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbUserName" ErrorMessage="User name is Required" CssClass="error-message" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
            <asp:TextBox ReadOnly="True" MaxLength="250" ID="tbUserName" runat="server" CssClass="form-input" meta:resourcekey="tbUserNameResource1"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lblGender" runat="server" Text="Gender:" CssClass="form-label" meta:resourcekey="lblGenderResource1"></asp:Label>
            <asp:RequiredFieldValidator ValidationGroup="editvalidation" ID="RequiredFieldValidator6" runat="server" ControlToValidate="rblGender" ErrorMessage="Gender is Required" CssClass="error-message" meta:resourcekey="RequiredFieldValidator6Resource1"></asp:RequiredFieldValidator>
            <asp:RadioButtonList ID="rblGender" runat="server" CssClass="form-input" meta:resourcekey="rblGenderResource1">
                <asp:ListItem Value="Male" Text="Male" meta:resourcekey="ListItemResource1" />
                <asp:ListItem Value="Female" Text="Female" meta:resourcekey="ListItemResource2" />
            </asp:RadioButtonList>
        </div>

        <div class="form-group">
            <asp:Label ID="lblClientName" runat="server" Text="Client Name:" CssClass="form-label" meta:resourcekey="lblClientNameResource1"></asp:Label>
            <asp:RequiredFieldValidator ValidationGroup="editvalidation" ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbClientName" ErrorMessage="Client Name is Required" CssClass="error-message" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
            <asp:TextBox ID="tbClientName" MaxLength="250" runat="server" CssClass="form-input" meta:resourcekey="tbClientNameResource1"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lblBirthday" runat="server" Text="Birthday:" CssClass="form-label" meta:resourcekey="lblBirthdayResource1"></asp:Label>

            <asp:RequiredFieldValidator ValidationGroup="editvalidation" ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbBirthday" ErrorMessage="Birthday" CssClass="error-message" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
<asp:TextBox SkinID="TextBoxSkinID" ID="tbBirthday" runat="server" TextMode="Date" 
    CssClass="form-input" meta:resourcekey="tbBirthdayResource1" />


        </div>


        <div>
             
     <asp:Label ID="lbNationalId" SkinID="LabelSkinID" runat="server" CssClass="form-label" Text="National ID" meta:resourcekey="lbNationalIdResource1" ></asp:Label>
<asp:RegularExpressionValidator 
    ID="revNationalId" 
    runat="server" 
    ControlToValidate="tbNationalId" 
    ValidationExpression="^\d{9}$" 
    ErrorMessage=" National ID must be exactly 9 digits." 
    ForeColor="Red" 
    Display="Dynamic" 
    ValidationGroup="AddUserGroup">
</asp:RegularExpressionValidator>


     <asp:RequiredFieldValidator ID="rfvNationalId" runat="server" ControlToValidate="tbNationalId" ErrorMessage="National ID is required." ForeColor="Red" Display="Dynamic" ValidationGroup="AddUserGroup" meta:resourcekey="rfvNationalIdResource1"></asp:RequiredFieldValidator>
     <asp:TextBox SkinID="TextBoxSkinID"  ID="tbNationalId" runat="server" TextMode="Number"  MaxLength="9"  CssClass="form-input" meta:resourcekey="tbNationalIdResource1" ></asp:TextBox>
 
        </div>


        <div class="form-group">
            <asp:Label ID="lbusertype" runat="server" Text="User Type:" CssClass="form-label" meta:resourcekey="lbusertypeResource1"></asp:Label>
            <asp:TextBox ReadOnly="True"  ID="tbusertype444" runat="server" CssClass="form-input" meta:resourcekey="tbusertype444Resource1"></asp:TextBox>
        </div>

        <div class="form-group">


            <asp:RequiredFieldValidator InitialValue="0" ID="RequiredFieldlanguage" runat="server" ControlToValidate="DropDownListLanguages" ErrorMessage="Language is required." ForeColor="Red" Display="Static" ValidationGroup="AddUserGroup" meta:resourcekey="RequiredFieldlanguageResource1"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="DropDownListLanguages" runat="server"  CssClass="form-control form-input custom-dropdown" meta:resourcekey="DropDownListLanguagesResource1">
            </asp:DropDownList>
        </div>


        <div class="form-group">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="fileUploadControl"></asp:RequiredFieldValidator>
            <asp:FileUpload ID="fileUploadControl" runat="server" onchange="previewImage(this);" meta:resourcekey="fileUploadControlResource1" />
            <asp:Image ID="imgUser" runat="server"  Width="100px" Height="100px" meta:resourcekey="imgUserResource1" />

        </div>

        <div class="form-row">
            <asp:Button ID="btnAdd" ValidationGroup="editvalidation" SkinID="Inline-button" runat="server" OnClick="btnAdd_Click" CssClass="form-button" Width="45%" meta:resourcekey="btnAddResource1" />
            <asp:Button ID="btnBack1" runat="server" Text="Back" CssClass="form-button" BackColor="Red" Width="45%" CausesValidation="False" OnClick="btnBackk_Click" meta:resourcekey="btnBack1Resource1" />
        </div>
        <asp:HiddenField ID="hfUserId" runat="server" />

<asp:Button ID="btnDeleteImage" runat="server" Text="Delete Image" CssClass="btn btn-danger"
    OnClientClick="DeleteImage(); return false;" meta:resourcekey="btndeleteResource1" />

    </div>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>


    <script type="text/javascript">             
        function previewImage(input) {
            if (input.files && input.files[0]) {
                var file = input.files[0];
                var fileName = file.name.toLowerCase();
                var validExtensions = ["png", "jpg", "jpeg"];

                var fileExtension = fileName.split('.').pop();
                if (!validExtensions.includes(fileExtension)) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Invalid File Type',
                        text: '❌ Only PNG, JPG, and JPEG files are allowed!',
                        confirmButtonColor: '#d33',
                    });

                    input.value = "";
                    return;
                }

                var reader = new FileReader();
                reader.onload = function (e) {
                    var imgElement = document.getElementById('<%= imgUser.ClientID %>');
                    imgElement.src = e.target.result;
                };
                reader.readAsDataURL(file);
            }
        }

        function DeleteImage() {
            var userId = document.getElementById("<%= hfUserId.ClientID %>").value; 

            if (!userId) {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'User Id not found',
                    confirmButtonColor: '#d33'
                });
                return;
            }

            
            Swal.fire({
                title: "are You sure",
                text: "You won't be able to recover this image after deletion",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#d33",
                cancelButtonColor: "#3085d6",
                confirmButtonText: "Yes، Delete!",
                cancelButtonText: "Cancel"
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: `http://itg-mmmomani/FilesApis/api/Image/DeleteImage/${userId}`,
                        type: "POST",
                        success: function (response) {
                            Swal.fire({
                                icon: 'success',
                                title: ' success deleted !',
                                text: response,
                                confirmButtonColor: '#28a745'
                            }).then(() => {
                                window.location.reload();
                            });
                        },
                        error: function (xhr) {
                            Swal.fire({
                                icon: 'error',
                                title: ' Errore',
                                text: ` Error: ${xhr.status} - ${xhr.responseText}`,
                                confirmButtonColor: '#d33'
                            });
                        }
                    });
                }
            });
        }



    
            document.addEventListener("DOMContentLoaded", function () {
        var tbNationalId = document.getElementById('<%= tbNationalId.ClientID %>');

            tbNationalId.addEventListener("input", function () {
                this.value = this.value.replace(/[^0-9]/g, ''); 
        });
            });



        document.addEventListener("DOMContentLoaded", function () {
            var tbNationalId = document.getElementById('<%= tbNationalId.ClientID %>');

                 tbNationalId.addEventListener("input", function () {
                     this.value = this.value.replace(/\D/g, ''); 

                     if (this.value.length > 9) {
                         this.value = this.value.slice(0, 9); 
                     }
                 });
             });
    </script>




</asp:Content>

