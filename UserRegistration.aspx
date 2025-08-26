<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs" Inherits="ElectricityBillProject.UserRegistration" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Registration - JBVNL</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server" class="container d-flex justify-content-center align-items-center vh-100">
        <div class="card shadow p-4" style="width: 400px;">
            <h3 class="text-center mb-4">User Registration</h3>

            <asp:Label ID="lblMsg" runat="server" CssClass="mb-3" />

            <div class="mb-3">
                <asp:Label runat="server" AssociatedControlID="txtName" Text="Name" CssClass="form-label" />
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Your full name" />
            </div>

            <div class="mb-3">
                <asp:Label runat="server" AssociatedControlID="txtPhone" Text="Phone" CssClass="form-label" />
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Phone number" MaxLength="15" />
            </div>

            <div class="mb-3">
                <asp:Label runat="server" AssociatedControlID="txtEmail" Text="Email" CssClass="form-label" />
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email address" />
            </div>

            <div class="mb-3">
                <asp:Label runat="server" AssociatedControlID="txtDOB" Text="Date of Birth" CssClass="form-label" />
                <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" />
            </div>

            <div class="mb-3">
                <asp:Label runat="server" AssociatedControlID="txtPassword" Text="Password" CssClass="form-label" />
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password" />
            </div>

            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary w-100" OnClick="btnRegister_Click" />
        </div>
    </form>
</body>
</html>
