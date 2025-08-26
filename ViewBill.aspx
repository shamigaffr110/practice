<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewBill.aspx.cs" Inherits="ElectricityBillProject.ViewBill" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Bills - JBVNL</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
    <form id="form1" runat="server" class="container py-5">
        <div class="card shadow-sm p-4 mx-auto" style="max-width: 700px;">
            <h3 class="mb-4 text-center">View Last N Bills</h3>

            <div class="input-group mb-3">
                <asp:TextBox ID="txtN" runat="server" CssClass="form-control" placeholder="Enter number of bills" />
                <button type="submit" runat="server" id="btnGo" onserverclick="btnGo_Click" class="btn btn-primary">Go</button>
            </div>

            <asp:Label ID="lblMsg" runat="server" CssClass="mb-3 d-block" />

            <asp:GridView ID="grid" runat="server" CssClass="table table-striped table-bordered" AutoGenerateColumns="false" EmptyDataText="No bills found">
                <Columns>
                    <asp:BoundField DataField="consumer_number" HeaderText="Consumer Number" />
                    <asp:BoundField DataField="consumer_name" HeaderText="Consumer Name" />
                    <asp:BoundField DataField="units_consumed" HeaderText="Units Consumed" />
                    <asp:BoundField DataField="bill_amount" HeaderText="Bill Amount (Rs.)" DataFormatString="{0:N2}" />
                    <asp:BoundField DataField="bill_date" HeaderText="Bill Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="status" HeaderText="Status" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
