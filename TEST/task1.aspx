<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="task1.aspx.cs" Inherits="TEST.task1" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="ITG_CustomControls" Namespace="ITG_CustomControls" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" runat="server">
    <title>Task1</title>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
        }


        .form-container {
            width: auto;
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


        /* Styling for collapsible sections */
        .collapsible-section {
            display: none; /* Hide by default */
            background-color: #fff;
            padding: 20px;
            margin-top: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
        }

        /* Styling for toggle buttons */
        .toggle-btn {
            display: block;
            width: 100%;
            background-color: #007bff;
            color: white;
            padding: 10px;
            font-size: 16px;
            text-align: left;
            border: none;
            cursor: pointer;
            border-radius: 5px;
            transition: background 0.3s ease-in-out;
        }

            .toggle-btn:hover {
                background-color: #0056b3;
            }
    </style>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="cphHeader" runat="server">
    <h2 id="title" runat="server">&nbsp;</h2>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cphcontent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="form-container">

<asp:Button 
    ID="btnSearchUsers" 
    runat="server" 
    class="toggle-btn" 
 meta:resourcekey="btnSearchUsers"
    OnClientClick="toggleSection('searchSection'); return false;" />
        <div id="searchSection" class="collapsible-section">
            <asp:Label ID="lbSearch" SkinID="LabelSkinID" runat="server" Text=" " meta:resourcekey="lbSearchResource1"></asp:Label>

            <div class="form-row">
                <asp:Label ID="lblSearchUserName" SkinID="LabelSkinID" runat="server" Text="User Name:" CssClass="form-label" meta:resourcekey="lblSearchUserNameResource1"></asp:Label>
                <asp:TextBox ID="txtSearchUserName" SkinID="TextBoxSkinID"  runat="server" CssClass="form-input" meta:resourcekey="txtSearchUserNameResource1"></asp:TextBox>
            </div>

            <div class="form-row">
                <asp:Label ID="lblSearchGender" runat="server" SkinID="LabelSkinID" Text="Gender:" CssClass="form-label" meta:resourcekey="lblSearchGenderResource1"></asp:Label>
                <asp:DropDownList ID="ddlSearchGender" runat="server" CssClass="form-input" meta:resourcekey="ddlSearchGenderResource1">
                    <asp:ListItem Value="" Text="Select Gender" meta:resourcekey="ListItemResource3" />
                    <asp:ListItem Value="Male" Text="Male" meta:resourcekey="ListItemResource4" />
                    <asp:ListItem Value="Female" Text="Female" meta:resourcekey="ListItemResource5" />
                </asp:DropDownList>
            </div>

            <div class="form-row">
                <asp:Button ID="btnSearch" SkinID="ButtonSkinID" runat="server" Text="Search" CssClass="form-button" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1" />
            </div>
        </div>
<asp:Button 
    ID="btnaddusers" 
    runat="server" 
    meta:resourcekey="btnaddusers" 
    class="toggle-btn" 
  
    OnClientClick="toggleSection('addUserSection'); return false;" />
        <div id="addUserSection" class="collapsible-section">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblUserName" SkinID="LabelSkinID" runat="server" CssClass="form-label" meta:resourcekey="lblUserNameResource1"></asp:Label>
                        <asp:RequiredFieldValidator SkinID="RequiredField" ValidationGroup="AddUserGroup" ID="RequiredFieldValidator1" runat="server" meta:resourcekey="reqUserNameResource1" ControlToValidate="tbUserName" ErrorMessage="User name is Required" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:TextBox SkinID="TextBoxSkinID" ID="tbUserName" MaxLength="250" runat="server" CssClass="form-input" meta:resourcekey="tbUserNameResource1"></asp:TextBox>
                    </td>


                    <td>
                        <asp:Label ID="lbNationalId" SkinID="LabelSkinID" runat="server" meta:resourcekey="lbNationalIdResource" CssClass="form-label" Text="National ID"></asp:Label>
                        <asp:RegularExpressionValidator ID="revNationalId" runat="server" ControlToValidate="tbNationalId" ValidationExpression="^\d{9}$" ErrorMessage="National ID must be exactly 9 digits." ForeColor="Red" Display="Dynamic" ValidationGroup="AddUserGroup"></asp:RegularExpressionValidator>

                        <asp:RequiredFieldValidator meta:resourcekey="reqidResource23" ID="rfvNationalId" runat="server" ControlToValidate="tbNationalId"  ErrorMessage="National ID is required." ForeColor="Red" Display="Dynamic" ValidationGroup="AddUserGroup"></asp:RequiredFieldValidator>
                        <asp:TextBox SkinID="TextBoxSkinID" ID="tbNationalId" runat="server" TextMode="Number" MaxLength="9" CssClass="form-input" meta:resourcekey="tbUserNameResource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lbEmail" SkinID="LabelSkinID" meta:resourcekey="lbEmailResource"  runat="server" CssClass="form-label" Text="Email"></asp:Label>
                        <asp:RequiredFieldValidator ValidationGroup="AddUserGroup" meta:resourcekey="reqemail1"  ID="RequiredFieldValidator7" EnableClientScript="True" runat="server" ControlToValidate="tbEmail" ErrorMessage="Email  is Required" ForeColor="Red"></asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator
                            ID="RegexEmailValidator"
                            runat="server"
                            ControlToValidate="tbEmail"
                            ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
                            EnableClientScript="True"
                            ErrorMessage="Please enter a valid email address."
                            ForeColor="Red"
                            Display="Dynamic"
                            ValidationGroup="AddUserGroup" />

                        <asp:TextBox ID="tbEmail" SkinID="TextBoxSkinID" MaxLength="250" runat="server" CssClass="form-inputt" meta:resourcekey="tbUserNameResource1"></asp:TextBox>
                    </td>

                    <td>
                        <asp:Label ID="lblGender" SkinID="LabelSkinID" runat="server" Text="Gender:" CssClass="form-label" meta:resourcekey="lblGenderResource1"></asp:Label>

                        <asp:RequiredFieldValidator meta:resourcekey="reqender2" SkinID="RequiredField" ValidationGroup="AddUserGroup" ID="RequiredFieldValidator6" runat="server" ControlToValidate="rblGender" ErrorMessage="Gender is Required"></asp:RequiredFieldValidator>

                        <asp:RadioButtonList ID="rblGender" runat="server" CssClass="form-input" meta:resourcekey="rblGenderResource1">
                            <asp:ListItem Value="Male" Text="Male" meta:resourcekey="ListItemResource6" />
                            <asp:ListItem Value="Female" Text="Female" meta:resourcekey="ListItemResource7" />
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator SkinID="RequiredField" ValidationGroup="AddUserGroup" ID="RequiredFieldValidator15" runat="server"  ControlToValidate="fileUploadControl" meta:resourcekey="reImage2" ErrorMessage="IMG is Required"></asp:RequiredFieldValidator>

                        <asp:FileUpload ID="fileUploadControl" runat="server" />
                        <br />
                        <asp:Label  ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblClientName" SkinID="LabelSkinID" runat="server" Text="Client Name:" CssClass="form-label" meta:resourcekey="lblClientNameResource1"></asp:Label>
                        <asp:RequiredFieldValidator  meta:resourcekey="reClientName2" SkinID="RequiredField" ValidationGroup="AddUserGroup" ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbClientName" ErrorMessage="Client Name is Required"></asp:RequiredFieldValidator>
                        <asp:TextBox SkinID="TextBoxSkinID" MaxLength="250" ID="tbClientName" runat="server" CssClass="form-input" meta:resourcekey="tbClientNameResource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblBirthday" SkinID="LabelSkinID" runat="server" Text="Birthday:" CssClass="form-label" meta:resourcekey="lblBirthdayResource1"></asp:Label>
                        <asp:RequiredFieldValidator  meta:resourcekey="reBirthDay2" SkinID="RequiredField" ValidationGroup="AddUserGroup" ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbBirthday" ErrorMessage="Birthday is Required"></asp:RequiredFieldValidator>
                        <asp:TextBox SkinID="TextBoxSkinID" ID="tbBirthday" runat="server" TextMode="Date" CssClass="form-input" meta:resourcekey="tbBirthdayResource1"
                            onfocus="this.setAttribute('max', new Date().toISOString().split('T')[0]);" />
                    </td>

                </tr>
                <tr>

                    <td>
                       <asp:RequiredFieldValidator SkinID="RequiredField" ValidationGroup="AddUserGroup" ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownListLanguages" ErrorMessage="language is Required"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="DropDownListLanguages" runat="server" CssClass="form-control form-input custom-dropdown">
                            <asp:ListItem Text="--  Select language --" Value="0" Selected="True" Disabled="True"></asp:ListItem>
                        </asp:DropDownList>


                        <%-- <asp:Label ID="lblLanguage" runat="server" SkinID="LabelSkinID" Text="Language:" CssClass="form-label" meta:resourcekey="lblLanguageResource1"></asp:Label>
                    <asp:RequiredFieldValidator SkinID="RequiredField" ValidationGroup="AddUserGroup" ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbLanguage" ErrorMessage="language is Required"></asp:RequiredFieldValidator>
                    <asp:TextBox SkinID="TextBoxSkinID" ID="tbLanguage" runat="server" CssClass="form-input" meta:resourcekey="tbLanguageResource1"></asp:TextBox>--%>
                    </td>
                    <td>
                        <asp:Label ID="lbpassword" SkinID="LabelSkinID" runat="server" Text="Password:" CssClass="form-label" meta:resourcekey="lbpasswordResource1"></asp:Label>
                        <asp:RequiredFieldValidator meta:resourcekey="reqpassword2" SkinID="RequiredField" ValidationGroup="AddUserGroup" ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbpassword" ErrorMessage="Password is Required"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="tbpassword" MaxLength="250" SkinID="TextBoxSkinID" TextMode="Password" runat="server" CssClass="form-input" meta:resourcekey="tbpasswordResource1"></asp:TextBox>
                    </td>
                </tr>

            </table>

            <div class="form-row">
                <asp:Button ID="btnAdd" SkinID="ButtonSkinID" runat="server" OnClick="btnAdd_Click" Text="Add" CssClass="form-button" ValidationGroup="AddUserGroup" meta:resourcekey="btnAddResource1" />
            </div>
        </div>


        <div class="error-message">
            <asp:Label ID="lblError" runat="server" ForeColor="Red" meta:resourcekey="lblErrorResource1"></asp:Label>
        </div>

        <div class="grid-container">
            <asp:Button ID="btnDeleteSelected" runat="server" Text="Delete Selected"
                CssClass="form-button"  meta:resourcekey="btndeleteselect"  OnClientClick="DeleteSelectedUsers(); return false;" />
            <asp:UpdatePanel runat="server" ID="uptest">
                <ContentTemplate>
                    <div class="grid-container">
                        <cc1:ITG_GridView ID="gvAnnualReport" runat="server"
                            DataKeyNames="id"
                            OnRowCommand="gvAnnualReport_RowCommand"
                            AutoGenerateColumns="False"
                            OnRowDataBound="gvAnnualReport_RowDataBound"
                            RowHighlightColor="Gainsboro"
                            TotalRowsCountMessage="Total rows count: "
                            TotalRowsCountMessageDirection="Left"
                            InitialSortExpression="UserName"
                            InitialSortDirection="Descending"
                            OnPageIndexChanging="gvAnnualReport_PageIndexChanging"
                            AllowPaging="True" PageSize="3">

                            <Columns>






<asp:TemplateField HeaderText="Select">
    <HeaderTemplate>
        <input type="checkbox" id="chkSelectAll" onclick="toggleSelectAll(this)" />
    </HeaderTemplate>
    <ItemTemplate>
        <input type="checkbox" class="chkSelect" data-userid='<%# Eval("id") %>' onclick="updateSelectedUsers(this)" />
    </ItemTemplate>
</asp:TemplateField>


                                <asp:TemplateField HeaderText="National_ID" meta:resourcekey="National_IDResource1">
                                    <ItemTemplate>
                                        <asp:Label ID="lbNational_Id" runat="server" Text='<%# Eval("National_ID") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
    <asp:RegularExpressionValidator
        ID="revNationalId"
        runat="server"
        ControlToValidate="tbNational_Id"
        ValidationExpression="^\d{9}$"
        ErrorMessage="❌ National ID must be exactly 9 digits."
        ForeColor="Red"
        Display="Dynamic"
        ValidationGroup="AddUserGroup">
    </asp:RegularExpressionValidator>

    <asp:RequiredFieldValidator 
        ValidationGroup="editvalidation" 
        ID="RequiredFieldValidatorNational_ID3" 
        EnableClientScript="True" 
        runat="server" 
        ControlToValidate="tbNational_Id" 
        ErrorMessage="*"
        CssClass="error-message">
    </asp:RequiredFieldValidator>

    <asp:TextBox 
        MaxLength="9" 
        ID="tbNational_Id" 
        runat="server" 
        Text='<%# Eval("National_ID") %>' 
        onkeypress="return isNumberKey(event);" 
        CssClass="form-input" />
</EditItemTemplate>

                                </asp:TemplateField>
                                <asp:BoundField Visible="false" DataField="id" HeaderText="ID" />
                                <asp:TemplateField HeaderText="Email" meta:resourcekey="EmailResource1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:RequiredFieldValidator ValidationGroup="editvalidation" ID="RequiredFieldValidator2" EnableClientScript="True" runat="server" ControlToValidate="tbEmail" ErrorMessage="*" CssClass="error-message"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="tbEmail" runat="server" Text='<%# Eval("Email") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <%--     <asp:TemplateField HeaderText="User Name" meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                                <asp:Label ID="lblUserNameGrid" runat="server" Text='<%# Eval("userNAME") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="lblUserNameEdit" ReadOnly runat="server" Text='<%# Eval("userNAME") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:BoundField ReadOnly meta:resourcekey="TemplateFieldResource1" DataField="UserName" HeaderText="UserName" SortExpression="UserName" />


                                <asp:TemplateField HeaderText="Client Name" meta:resourcekey="TemplateFieldResource2">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("clientNAME") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:RequiredFieldValidator ValidationGroup="editvalidation" ID="RequiredFieldValidator3" EnableClientScript="True" runat="server" ControlToValidate="tbClientNameEdit" ErrorMessage="*" CssClass="error-message"></asp:RequiredFieldValidator>
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
                                        <asp:RequiredFieldValidator ValidationGroup="editvalidation" ID="RequiredFieldValidator6" EnableClientScript="True" runat="server" ControlToValidate="ddlGender" ErrorMessage="*" CssClass="error-message"></asp:RequiredFieldValidator>

                                        <asp:DropDownList ID="ddlGender" runat="server" meta:resourcekey="DropDownListResource5">
                                            <asp:ListItem Text="Male" Value="Male" meta:resourcekey="ListItemResource8" />
                                            <asp:ListItem Text="Female" Value="Female" meta:resourcekey="ListItemResource9" />
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <%--                                <asp:TemplateField HeaderText="User Type" meta:resourcekey="TemplateFieldResource5">
                                    <ItemTemplate>
                                        <asp:Label ID="lblusertype" runat="server" Text='<%# Eval("usertype") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ReadOnly="True" ID="tbusertype" runat="server" Text='<%# Eval("usertype") %>' />
                                    </EditItemTemplate>
                                </asp:TemplateField>--%>


                                <asp:BoundField ReadOnly DataField="usertype" meta:resourcekey="TemplateFieldResource5" HeaderText="usertype" />


                                <asp:TemplateField HeaderText="Language" meta:resourcekey="TemplateFieldResource6">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLanguage" runat="server" Text='<%# Eval("LanguageName") %>' meta:resourcekey="lblLanguageResource2" />
                                    </ItemTemplate>

                                    <EditItemTemplate>

                                        <asp:RequiredFieldValidator ValidationGroup="editvalidation" InitialValue="0" ID="RequiredFieldlanguage" runat="server" ControlToValidate="ddlLanguages" ErrorMessage="*" ForeColor="Red" Display="Static" meta:resourcekey="RequiredFieldlanguageResource1"></asp:RequiredFieldValidator>


                                        <asp:DropDownList
                                            ID="ddlLanguages"
                                            runat="server"
                                            CssClass="form-control form-input custom-dropdown">
                                        </asp:DropDownList>


                                        <%--                                    <asp:RequiredFieldValidator ValidationGroup="editvalidation" ID="RequiredFieldValidator5" EnableClientScript="True" runat="server" ControlToValidate="tbLanguage" ErrorMessage="*" CssClass="error-message"></asp:RequiredFieldValidator>

                                        <asp:TextBox ID="tbLanguage" runat="server" Text='<%# Eval("LanguageName") %>' meta:resourcekey="tbLanguageResource2" />--%>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField meta:resourcekey="ImageResource6" HeaderText="Image">
                                    <ItemTemplate>
                                        <asp:Image ID="imgUser" runat="server" Width="100px" Height="100px" />





                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Image ID="imgUser" runat="server" Width="100px" Height="100px" />
                                    </EditItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField meta:resourcekey="actionResource6" HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:HyperLink
                                            ID="lnkViewDetails"
                                            runat="server"
                                            NavigateUrl='<%# "UserInfoPreview.aspx?userId=" + Eval("id") %>'
                                            Text=" View Details"
                                            meta:resourcekey="ViewDetailsResource"
                                            CssClass="btn btn-info">
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>




                                <asp:TemplateField meta:resourceKey="TemplateFieldResource1">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblOperations" runat="server" Text="Options"
                                            meta:resourcekey="lblOperationsResource1"></asp:Label>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <table class="tblOptions">
                                            <tr>
                                                <td id="tdEdit" runat="server">
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="False" CommandName="EditAnnualReport"
                                                        meta:resourceKey="lbtnEditRes">
                                                    </asp:LinkButton>
                                                </td>
                                                <td id="tdDelete" runat="server">
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="DeleteAnnualReport"
                                                        meta:resourceKey="lbtnDelete">
                                                    </asp:LinkButton>
                                                </td>
                                                <td id="tdUpdate" runat="server">
                                                    <asp:LinkButton ID="lbtnUpdate" runat="server" ValidationGroup="editvalidation" CausesValidation="True" CommandName="UpdateAnnualReport"
                                                        meta:resourceKey="lbtnUpdateRes">
                                                    </asp:LinkButton>
                                                </td>
                                                <td id="tdCancel" runat="server">
                                                    <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="False" CommandName="CancelAnnualReport"
                                                        meta:resourceKey="lbtnCancel">
                                                    </asp:LinkButton>
                                                </td>
                                                <td id="td1" runat="server">
                                                    <asp:LinkButton ID="lbtnEditinsite" runat="server" CausesValidation="False" CommandName="Editinside"
                                                        meta:resourceKey="lbtnEditRes">
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </cc1:ITG_GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

            <link href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" rel="stylesheet" />
            <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>

            <!-- ✅ تحميل مكتبة SweetAlert2 -->
            <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>


            <script type="text/javascript">
                let selectedUsers = new Set();

                function toggleSelectAll(checkbox) {
                    $(".chkSelect").each(function () {
                        this.checked = checkbox.checked;
                        let userId = $(this).data("userid");
                        if (checkbox.checked) {
                            selectedUsers.add(userId);
                        } else {
                            selectedUsers.delete(userId);
                        }
                    });
                }

                function updateSelectedUsers(checkbox) {
                    let userId = $(checkbox).data("userid");
                    if (checkbox.checked) {
                        selectedUsers.add(userId);
                    } else {
                        selectedUsers.delete(userId);
                    }

                    // تحديث "تحديد الكل" بناءً على الاختيارات الحالية
                    $("#chkSelectAll").prop("checked", $(".chkSelect:checked").length === $(".chkSelect").length);
                }

                // عند تغيير الصفحة، تأكد من إعادة تطبيق التحديدات
                $(document).on("click", ".pagination a", function () {
                    setTimeout(() => {
                        $(".chkSelect").each(function () {
                            $(this).prop("checked", selectedUsers.has($(this).data("userid")));
                        });

                        $("#chkSelectAll").prop("checked", $(".chkSelect:checked").length === $(".chkSelect").length);
                    }, 500);
                });





                function DeleteSelectedUsers() {
                    var selectedIds = [];

                    $(".chkSelect:checked").each(function () {
                        selectedIds.push($(this).data("userid"));
                    });

                    if (selectedIds.length === 0) {
                        Swal.fire({
                            icon: 'warning',
                            title: '⚠️ No Users Selected',
                            text: 'Please select at least one user to delete.',
                            confirmButtonColor: '#ffcc00'
                        });
                        return;
                    }

                    Swal.fire({
                        title: "Are you sure?",
                        text: "You will not be able to recover these users!",
                        icon: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#d33",
                        cancelButtonColor: "#3085d6",
                        confirmButtonText: "Yes, delete them!",
                        cancelButtonText: "Cancel"
                    }).then((result) => {
                        if (result.isConfirmed) {
                            $.ajax({
                                type: "POST",
                                url: "task1.aspx/DeleteUsers",
                                data: JSON.stringify({ userIds: selectedIds }),
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (response) {
                                    Swal.fire({
                                        icon: "success",
                                        title: "Deleted!",
                                        text: response.d,
                                        confirmButtonColor: "#28a745"
                                    }).then(() => {
                                        location.reload();
                                    });
                                },
                                error: function (xhr, status, error) {
                                    Swal.fire({
                                        icon: "error",
                                        title: "❌ Error!",
                                        text: "An error occurred: " + error,
                                        confirmButtonColor: "#d33"
                                    });
                                }
                            });
                        }
                    });
                }


                function toggleSection(sectionId) {
                    $("#" + sectionId).slideToggle(300);
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
            <script type="text/javascript">
                function isNumberKey(evt) {
                    var charCode = (evt.which) ? evt.which : evt.keyCode;
                    // السماح فقط بالأرقام (0–9)
                    if (charCode >= 48 && charCode <= 57)
                        return true;
                    return false;
                }
            </script>


            <script type="text/javascript">
                window.history.forward();
                function noBack() { window.history.forward(); }
            </script>
        </div>
    </div>

</asp:Content>

