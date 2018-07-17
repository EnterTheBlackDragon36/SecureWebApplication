<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="SecureWebApplication.Logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <fieldset>
                    <legend>Log in Form</legend>
                    <ol>
                        <li>
                            <asp:Label ID="Label1" runat="server" AssociatedControlID="txtUserName">User name</asp:Label>
                            <asp:TextBox runat="server" ID="txtUserName" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                        </li>
                        <li>
                            <asp:Label ID="Label2" runat="server" AssociatedControlID="txtPassword">Password</asp:Label>
                            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" CssClass="field-validation-error" ErrorMessage="The password field is required." />
                        </li>
                         <li>
                            <asp:Label ID="Label3" runat="server" AssociatedControlID="RoleList">Role</asp:Label>
                             <asp:DropDownList ID="RoleList" runat="server">
                                 <asp:ListItem Selected="True">Please Select</asp:ListItem>
                                 <asp:ListItem>User</asp:ListItem>
                                 <asp:ListItem>Administrator</asp:ListItem>


                             </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword" CssClass="field-validation-error" ErrorMessage="The password field is required." />
                        </li>
                      
                    </ol>
        <asp:Button ID="btnRegister" runat="server" CommandName="Register" Text="Register" OnClick="btnRegister_Click" />
                    <asp:Button ID="btnLogon" runat="server" CommandName="Login" Text="Log in" OnClick="btnLogon_Click" />
        <asp:Label ID="lblMessage" runat="server" />
                </fieldset>
    </div>
    </form>
</body>
</html>
