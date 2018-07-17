<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecurePage.aspx.cs" Inherits="SecureWebApplication.Secure.SecurePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Welcome to your SECURE Page. View and manage all resource Files Here!</h1>
    </div>
        

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EmptyDataText = "No files uploaded">
    <Columns>
        <asp:BoundField DataField="Text" HeaderText="File Name" />
         
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnkDownload" Text = "Download" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "DownloadFile"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID = "lnkDelete" Text = "Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server" OnClick = "DeleteFile" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
        <asp:FileUpload ID="FileUpload1" runat="server" /><br /> <br />
        <asp:Button ID="UploadFile" Text="Upload File" Width="250" runat="server" OnClick="UploadFile_Click" />
    </form>
</body>
</html>
