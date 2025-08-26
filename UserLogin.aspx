<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="ElectricityBillProject.UserLogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Login - JBVNL</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server" class="container d-flex justify-content-center align-items-center vh-100">
        <div class="card shadow p-4" style="width: 350px;">
            <h3 class="text-center mb-4">User Login</h3>
            <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mb-3" />

            <div class="mb-3">
                <asp:Label runat="server" AssociatedControlID="txtEmail" Text="Email" CssClass="form-label" />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter your email" />
            </div>
            <div class="mb-3">
                <asp:Label runat="server" AssociatedControlID="txtPass" Text="Password" CssClass="form-label" />
                <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter your password" />
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />
        </div>
    </form>
</body>
</html>
