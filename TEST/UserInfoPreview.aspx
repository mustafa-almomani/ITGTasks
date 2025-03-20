<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UserInfoPreview.aspx.cs" Inherits="TEST.UserInfoPreview" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<%@ Register Src="~/UserDetails.ascx" TagPrefix="uc1" TagName="UserDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="server">
    <h2 id="title" runat="server"></h2>
</asp:Content>




<asp:Content ID="Content3" ContentPlaceHolderID="cphcontent" runat="server">
    <div class="content-container">





        <uc1:UserDetails ID="UserDetailsControl" runat="server" />

    </div>
</asp:Content>

<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="cphhead">
    <style type="text/css">
        /* Main Content Container */



        .content-container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 8px;
        }

            /* Page Heading */
            .content-container h2 {
                text-align: center;
                font-size: 32px;
                color: #333;
                padding-bottom: 15px;
                font-weight: bold;
            }

        /* Custom GridView Styles */
        .custom-gridview {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
            font-size: 14px;
        }

            .custom-gridview th, .custom-gridview td {
                padding: 12px 15px;
                text-align: left;
            }

        /* Header Row Style */
        .grid-header {
            background-color: #A55129;
            color: white;
            font-weight: bold;
        }

        /* Row Style */
        .grid-row {
            background-color: #FFF7E7;
            color: #8C4510;
        }

        /* Selected Row Style */
        .grid-selected-row {
            background-color: #738A9C;
            font-weight: bold;
            color: white;
        }

        /* Pager Style */
        .grid-pager {
            text-align: center;
            margin-top: 20px;
        }

            .grid-pager a {
                text-decoration: none;
                padding: 5px 10px;
                margin: 0 3px;
                background-color: #A55129;
                color: white;
                border-radius: 5px;
            }

                .grid-pager a:hover {
                    background-color: #93451F;
                }

        /* Grid Alternating Row Colors */
        .custom-gridview tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .custom-gridview tr:nth-child(odd) {
            background-color: #FFF7E7;
        }

        /* Responsive Design */
        @media (max-width: 768px) {
            .content-container {
                padding: 10px;
            }

            .custom-gridview th, .custom-gridview td {
                font-size: 12px;
                padding: 8px;
            }
        }
    </style>




</asp:Content>
