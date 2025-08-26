<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillReceipt.aspx.cs" Inherits="ElectricityBillProject.BillReceipt" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Receipt</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-success text-white">
                <h4>Payment Receipt</h4>
            </div>
            <div class="card-body">
                <asp:Literal ID="litReceipt" runat="server"></asp:Literal>
                <asp:Button ID="btnDownloadPdf" runat="server" Text="Download PDF" CssClass="btn btn-primary mt-3" OnClick="btnDownloadPdf_Click" />
            </div>
        </div>
    </form>
</body>
</html>
