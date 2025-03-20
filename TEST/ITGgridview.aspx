<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" Culture="auto" meta:resourcekey="PageResource1" CodeBehind="ITGgridview.aspx.cs" Inherits="TEST.ITGgridview" UICulture="auto" %>

<%@ Register Assembly="ITG_CustomControls" Namespace="ITG_CustomControls" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" runat="server">
    <title>Task1</title>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
        }


        .form-container {
            width: 80%;
            margin: 0 auto;
            background-color: #fff;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .header {
            text-align: center;
            margin-bottom: 20px;
        }

        .form-label {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 5px;
            display: inline-block;
        }

        .form-input {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ddd;
            border-radius: 5px;
            font-size: 14px;
            box-sizing: border-box;
        }

        .form-button {
            padding: 10px 20px;
            background-color: #3366CC;
            color: white;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
        }

            .form-button:hover {
                background-color: #2858a6;
            }

        .grid-container {
            width: 100%;
            margin-top: 20px;
            margin-bottom: 20px;
        }

            .grid-container table {
                width: 100%;
                border-collapse: collapse;
                border: 1px solid #ddd;
            }

            .grid-container th, .grid-container td {
                padding: 12px;
                text-align: left;
                border-bottom: 1px solid #ddd;
            }

            .grid-container th {
                background-color: #f5f5f5;
            }

        .error-message {
            color: red;
            font-size: 14px;
            text-align: center;
            margin-top: 10px;
        }
    </style>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="cphHeader" runat="server">
    <h2 id="title" runat="server"></h2>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cphcontent" runat="server">



    <div class="error-message">
        <asp:Label ID="lblError" runat="server" ForeColor="Red" meta:resourcekey="lblErrorResource1"></asp:Label>
    </div>

    <div class="grid-container">
        <cc1:ITG_GridView ID="gvAnnualReport" runat="server"
            DataKeyNames="id"
            OnRowCommand="gvAnnualReport_RowCommand"
            AutoGenerateColumns="False"
            OnRowDataBound="gvAnnualReport_RowDataBound"
            RowHighlightColor="Gainsboro"
            TotalRowsCountMessage="Total rows count: "
            TotalRowsCountMessageDirection="Left"
            InitialSortExpression="Birthday"
            InitialSortDirection="Descending">
            <Columns>
                <asp:BoundField Visible="false" DataField="id" HeaderText="ID" />
                <asp:TemplateField HeaderText="User Name" meta:resourcekey="TemplateFieldResource1">
                    <ItemTemplate>
                        <asp:Label ID="lblUserNameGrid" runat="server" Text='<%# Eval("userNAME") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="lblUserNameEdit" runat="server" Text='<%# Eval("userNAME") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Client Name" meta:resourcekey="TemplateFieldResource2">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("clientNAME") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbClientNameEdit" runat="server" Text='<%# Eval("clientNAME") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Birthday" SortExpression="Birthday" meta:resourcekey="TemplateFieldResource3">
                    <ItemTemplate>
                        <asp:Label ID="lblBirthday" runat="server" Text='<%# Convert.ToDateTime(Eval("Birthday")).ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en-US")) %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox
                            ID="tbBirthday"
                            TextMode="Date"
                            runat="server"
                            Text='<%# Eval("Birthday") != DBNull.Value 
            ? Convert.ToDateTime(Eval("Birthday")).ToString("yyyy-MM-dd", new System.Globalization.CultureInfo("en"))
            : "" %>'
                            onfocus="this.max = new Date().toISOString().split('T')[0]" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Gender" meta:resourcekey="TemplateFieldResource4">
                    <ItemTemplate>
                        <asp:Label ID="lblGender" runat="server"
                            Text='<%# Eval("Gender").ToString() == "Male" ? 
(GetLocalResourceObject("Male") ?? "Male") : 
(GetLocalResourceObject("Female") ?? "Female") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlGender" runat="server" meta:resourcekey="DropDownListResource5">

                            <asp:ListItem Text="Male" Value="Male" meta:resourcekey="ListItemResource8" />
                            <asp:ListItem Text="Female" Value="Female" meta:resourcekey="ListItemResource9" />
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="User Type" meta:resourcekey="TemplateFieldResource5">
                    <ItemTemplate>
                        <asp:Label ID="lblusertype" runat="server" Text='<%# Eval("usertype") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ReadOnly="True" ID="tbusertype" runat="server" Text='<%# Eval("usertype") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Language" meta:resourcekey="TemplateFieldResource6">
                    <ItemTemplate>
                        <asp:Label ID="lblLanguage" runat="server" Text='<%# Eval("Language") %>' meta:resourcekey="lblLanguageResource2" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="tbLanguage" runat="server" Text='<%# Eval("Language") %>' meta:resourcekey="tbLanguageResource2" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField meta:resourceKey="TemplateFieldResource1">
                    <HeaderTemplate>
                        <asp:Label ID="lblOperations" runat="server" SkinID="LabelHeaderCell" Text="Options"
                            meta:resourcekey="lblOperationsResource1"></asp:Label>
                    </HeaderTemplate>

                    <ItemTemplate>
                        <table class="tblOptions">
                            <tr>
                                <td id="tdEdit" runat="server">
                                    <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="EditAnnualReport"
                                        meta:resourceKey="lbtnEditRes"></asp:LinkButton>

                                </td>
                                <td id="tdDelete" runat="server">
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="DeleteAnnualReport" meta:resourceKey="lbtnDelete"></asp:LinkButton>
                                </td>


                                <td id="tdUpdate" runat="server">
                                    <asp:LinkButton ID="lbtnUpdate" runat="server" CausesValidation="False" CommandName="UpdateAnnualReport"
                                        meta:resourceKey="lbtnUpdateRes"></asp:LinkButton>

                                </td>
                                <td id="tdCancel" runat="server">
                                    <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="False" CommandName="CancelAnnualReport" meta:resourceKey="lbtnCancel"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>
        </cc1:ITG_GridView>
    </div>

    <asp:FileUpload ID="fileUploadControl" runat="server" />
    <asp:Button ID="btnUpload" runat="server" Text="رفع الصورة" OnClick="btnUpload_Click" />
    <br />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

</asp:Content>

