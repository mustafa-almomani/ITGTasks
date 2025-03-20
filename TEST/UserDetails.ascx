<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDetails.ascx.cs" Inherits="TEST.UserDetails" %>







<div class="content-container">
   <h2 <asp:Label ID="Title" runat="server" CssClass="info-label" ></asp:Label><br /></h2>

    <asp:Label ID="lblUserName" runat="server" CssClass="info-label" ></asp:Label><br />
    <asp:Label ID="lblEmail" runat="server" CssClass="info-label" ></asp:Label><br />
    <asp:Label ID="lblClientName" runat="server" CssClass="info-label" ></asp:Label><br />
    <asp:Label ID="lblBirthday" runat="server" CssClass="info-label" ></asp:Label><br />
    <asp:Label ID="lblGender" runat="server" CssClass="info-label"></asp:Label><br />
    <asp:Label ID="lblLanguage" runat="server" CssClass="info-label" ></asp:Label><br />
    <asp:Label ID="lbNational_ID" runat="server" CssClass="info-label" ></asp:Label><br />
    <asp:Image ID="imgUser" runat="server" Width="120px" Height="120px" CssClass="user-image" />
    <br />

    <asp:Button ID="btnBack1" runat="server" Text="Back" CssClass="form-button" BackColor="Red" Width="45%" CausesValidation="False" OnClick="btnBackk_Click" meta:resourcekey="btnBack1Resource1" />

     

</div>

