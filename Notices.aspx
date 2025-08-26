<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notices.aspx.cs" Inherits="ElectricityBillProject.Notices" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Notices</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-4" style="max-width:900px;">
        <div class="card shadow">
            <div class="card-header bg-warning">
                <h4 class="text-dark">Legal Notices - Unpaid Bills</h4>
            </div>
            <div class="card-body">

                <asp:Label ID="lblMsg" runat="server" CssClass="mb-3"></asp:Label>

                <asp:Button ID="btnFind" runat="server" Text="Load Unpaid Notices" CssClass="btn btn-warning mb-3" OnClick="btnFind_Click" />

                <asp:GridView ID="grid" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" EmptyDataText="No unpaid bills found">
                    <Columns>
                        <asp:BoundField DataField="consumer_number" HeaderText="Consumer Number" />
                        <asp:BoundField DataField="consumer_name" HeaderText="Consumer Name" />
                        <asp:BoundField DataField="bill_amount" HeaderText="Bill Amount" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="bill_date" HeaderText="Bill Date" DataFormatString="{0:yyyy-MM-dd}" />
                        <asp:BoundField DataField="status" HeaderText="Status" />
                    </Columns>
                </asp:GridView>

                <hr />

                <div class="row g-2">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtConsumer" runat="server" CssClass="form-control" Placeholder="Consumer Number to send notice" MaxLength="20" />
                    </div>
                    <div class="col-md-6">
                        <asp:Button ID="btnSend" runat="server" Text="Send Legal Notice" CssClass="btn btn-danger" OnClick="btnSend_Click" />
                    </div>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
