<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="TEST.ErrorPage" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeader" runat="server">
    <h2 id="title" runat="server"></h2>
</asp:Content>




<asp:Content ID="Content3" ContentPlaceHolderID="cphcontent" runat="server">
    <div class="content-container">







        <h1> <asp:Label ID="lbtitle" runat="server" meta:resourcekey="lbtitle" ></asp:Label></h1>



        <table id="errorsTable" border="1" style="border-collapse: collapse; width: 100%;">
            <thead>
                <tr>




                    <th>
                        <asp:Label ID="lbmessage" runat="server" meta:resourcekey="lbmessageResource1" ></asp:Label></th>
                    <th>
                        <asp:Label ID="lbstackTrace" runat="server" meta:resourcekey="lbstackTraceResource1" ></asp:Label></th>
                    <th>
                        <asp:Label ID="lbsource" runat="server" meta:resourcekey="lbsourceResource1" ></asp:Label></th>
                    <th>
                        <asp:Label ID="CreatedAt" runat="server" meta:resourcekey="CreatedAtResource1" ></asp:Label>
                    </th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
        <script>
            async function UplodeData() {
                debugger
                const response = await fetch("<%= ConfigurationManager.AppSettings["apigetErrorLog"] %>");

                if (response.ok) {
                    const data = await response.json();
                    const tableBody = document.getElementById('errorsTable').getElementsByTagName('tbody')[0];
                    tableBody.innerHTML = '';

                    data.forEach(log => {
                        const row = tableBody.insertRow();

                        const cell1 = row.insertCell(0);
                        cell1.textContent = log.message;

                        const cell2 = row.insertCell(1);
                        cell2.textContent = log.stackTrace;

                        const cell3 = row.insertCell(2);
                        cell3.textContent = log.source;

                        const cell4 = row.insertCell(3);
                        cell4.textContent = new Date(log.loggedAt).toLocaleString();
                    });
                } 
            }

            window.onload = UplodeData;
        </script>



    </div>
</asp:Content>

<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="cphhead">
    <style type="text/css">



        .content-container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 8px;
        }

            .content-container h2 {
                text-align: center;
                font-size: 32px;
                color: #333;
                padding-bottom: 15px;
                font-weight: bold;
            }

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

