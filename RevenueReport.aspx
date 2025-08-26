<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RevenueReport.aspx.cs" Inherits="ElectricityBillProject.RevenueReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Revenue Report</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-4" style="max-width:900px;">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h4>Revenue Report</h4>
            </div>
            <div class="card-body">
                <div class="row mb-3 g-3">
                    <div class="col-md-3">
                        <asp:Label runat="server" AssociatedControlID="txtFrom" Text="From Date" CssClass="form-label" />
                        <asp:TextBox ID="txtFrom" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" />
                    </div>
                    <div class="col-md-3">
                        <asp:Label runat="server" AssociatedControlID="txtTo" Text="To Date" CssClass="form-label" />
                        <asp:TextBox ID="txtTo" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" />
                    </div>
                    <div class="col-md-4">
                        <asp:Label runat="server" AssociatedControlID="txtName" Text="Consumer Name (optional)" CssClass="form-label" />
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Search by consumer name" />
                    </div>
                    <div class="col-md-2 d-flex align-items-end">
                        <asp:Button ID="btnLoad" runat="server" Text="Load Report" CssClass="btn btn-primary w-100" OnClick="btnLoad_Click" />
                    </div>
                </div>

                <asp:GridView ID="grid" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No records found">
                    <Columns>
                        <asp:BoundField DataField="payment_id" HeaderText="Payment ID" />
                        <asp:BoundField DataField="consumer_number" HeaderText="Consumer Number" />
                        <asp:BoundField DataField="amount" HeaderText="Amount (Rs)" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="payment_date" HeaderText="Payment Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" />
                        <asp:BoundField DataField="method" HeaderText="Method" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                    </Columns>
                </asp:GridView>

                <asp:Label ID="lblTotal" runat="server" CssClass="mt-3 fw-bold"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
