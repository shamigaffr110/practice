<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="ElectricityBillProject.AdminLogin" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <title>Admin Login - EB Utility</title>
    <!-- Bootstrap 5 CDN -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-5">
                    <div class="card shadow-sm">
                        <div class="card-body">
                            <h4 class="text-center mb-4">🔐 Admin Login</h4>

                            <div class="mb-3">
                                <label for="txtUser" class="form-label">Username</label>
                                <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" />
                            </div>

                            <div class="mb-3">
                                <label for="txtPass" class="form-label">Password</label>
                                <asp:TextBox ID="txtPass" runat="server" TextMode="Password" CssClass="form-control" />
                            </div>

                            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click" />

                            <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
