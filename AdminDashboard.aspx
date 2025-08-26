<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="ElectricityBillProject.AdminDashboard" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8" />
    <title>Admin Dashboard - EB Utility</title>

    <!-- Bootstrap 5 CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        .sidebar {
            height: 100vh;
            background-color: #343a40;
            padding-top: 20px;
            color: white;
        }

        .sidebar a {
            color: white;
            display: block;
            padding: 10px 20px;
            text-decoration: none;
        }

        .sidebar a:hover {
            background-color: #495057;
        }

        .card-metric {
            text-align: center;
            padding: 20px;
            background-color: #f8f9fa;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <!-- Sidebar -->
                <div class="col-md-2 sidebar">
                    <h4 class="text-center">⚡ EB Admin</h4>
                    <hr />
                    <a href="AdminDashboard.aspx">🏠 Dashboard</a>
                    <a href="ConnectionsAdmin.aspx">📋 Connections</a>
                    <a href="ConcernsAdmin.aspx">📝 Concerns</a>
                    <a href="RevenueReport.aspx">💰 Revenue</a>
                    <a href="Notices.aspx">📢 Notices</a>
                    <a href="AddBill.aspx" >AddBill</a>
                    <asp:LinkButton ID="lnkViewConnections" runat="server" OnClick="lnkViewConnections_Click">
                      View Connections
                    </asp:LinkButton>
                    <asp:GridView ID="GridViewConnections" runat="server" AutoGenerateColumns="true" Visible="false" />

      
               
                    <a href="Default.aspx">🚪 Logout</a>
                </div>

                <!-- Main Content -->
                <div class="col-md-10 p-4">
                    <h3>📊 Admin Dashboard</h3>
                    <hr />

                    <div class="row">
                        <div class="col-md-3">
                            <div class="card card-metric shadow-sm">
                                <h5>Total Users</h5>
                                <asp:Label ID="lblUsers" runat="server" CssClass="h4 text-primary"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="card card-metric shadow-sm">
                                <h5>Total Revenue</h5>
                                <asp:Label ID="lblRevenue" runat="server" CssClass="h4 text-success"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="card card-metric shadow-sm">
                                <h5>Payments</h5>
                                <asp:Label ID="lblPayments" runat="server" CssClass="h4 text-info"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="card card-metric shadow-sm">
                                <h5>Pending Concerns</h5>
                                <asp:Label ID="lblConcerns" runat="server" CssClass="h4 text-danger"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <asp:Label ID="lblMsg" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
