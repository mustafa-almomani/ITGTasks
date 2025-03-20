<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="LOGIN.aspx.cs" Inherits="TEST.LOGIN" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphheader" runat="server">
    <h2 id="title" runat="server"></h2>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="cphcontent" runat="server">

        <div class="login-container">
            <div class="form-group">
                <asp:Label runat="server" meta:resourcekey="lblusserName" for="tbusserName" ></asp:Label>
                <asp:TextBox ID="tbusserName" runat="server" CssClass="form-control" Width="180px" meta:resourcekey="tbusserNameResource1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rvuserName" runat="server" ControlToValidate="tbusserName" EnableClientScript="False" ErrorMessage="User Name is required!" ForeColor="Red" meta:resourcekey="rvuserNameResource1" />
            </div>

            <div class="form-group">
                <asp:Label runat="server" meta:resourcekey="lblusserpassword" for="tbpassword" ></asp:Label>
                <asp:TextBox ID="tbpassword" runat="server" TextMode="Password" CssClass="form-control" Width="179px" meta:resourcekey="tbpasswordResource1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rvpassword" runat="server" ControlToValidate="tbpassword" EnableClientScript="False" ErrorMessage="Password is required!" ForeColor="Red" meta:resourcekey="rvpasswordResource1" />
            </div>

            <div class="form-group">
                <asp:Label ID="lblError" runat="server" ForeColor="Red" meta:resourcekey="lblErrorResource1"></asp:Label>
            </div>

            <div class="form-group">
                <asp:Button ID="btnlogin" runat="server" OnClick="btnLogin" Text="Login" CssClass="btn btn-primary" meta:resourcekey="btnloginResource1" />
            </div>
        </div>
</asp:Content>

<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="cphhead">

   

    <style type="text/css">
        /* General Container Styling */
        .login-container {
            max-width: 400px;
            margin: 50px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 8px;
            background-color: #f9f9f9;
        }
        .sidebar{
            display:none;
        }
        .content{
            margin-left:0 !important;
        }

        /* Form Group Styling */
        .form-group {
            margin-bottom: 15px;
        }

        .form-group label {
            display: block;
            font-size: 14px;
            margin-bottom: 5px;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }

        .form-control:focus {
            border-color: #007bff;
            outline: none;
        }

        /* Button Styling */
        .btn {
            padding: 10px 20px;
            font-size: 16px;
            color: white;
            background-color: #007bff;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            width: 100%;
        }

        .btn:hover {
            background-color: #0056b3;
        }

        /* Label Error Message Styling */
        .error-message {
            color: red;
            font-size: 12px;
        }
        .navbar{
            height:10%;
        }
    </style>
</asp:Content>
