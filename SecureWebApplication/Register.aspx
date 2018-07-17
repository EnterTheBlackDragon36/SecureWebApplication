<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SecureWebApplication._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
  
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    <asp:Table runat="server" ID="RegisrationTbl">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>Registration</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="firstnamelbl" runat="server" Text="First Name:" />
            </asp:TableCell><asp:TableCell>
                <asp:TextBox ID="firstname" runat="server" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lastnamelbl" runat="server" Text="Last Name:" />
            </asp:TableCell><asp:TableCell>
                <asp:TextBox ID="lastname" runat="server" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="usernamelbl" runat="server" Text="User Name:" />
            </asp:TableCell><asp:TableCell>
                <asp:TextBox ID="username" runat="server" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="passwordlbl" runat="server" Text="Password:" />
            </asp:TableCell><asp:TableCell>
                <asp:TextBox ID="password" runat="server" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="emaillbl" runat="server" Text="User Name:" />
            </asp:TableCell><asp:TableCell>
                <asp:TextBox ID="email" runat="server" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                <asp:Button ID="SubmitRegistration" Text="Register" width="250" OnClick="SubmitRegistration_Click" runat="server" />
                
            </asp:TableCell></asp:TableRow></asp:Table><asp:Label ID="msglbl" runat="server" ForeColor="Black" />
    
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:UserAccountsConnectionString %>" SelectCommand="SELECT * FROM [Users]"></asp:SqlDataSource>
</asp:Content>