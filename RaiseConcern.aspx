<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RaiseConcern.aspx.cs" Inherits="ElectricityBillProject.RaiseConcern" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Raise Concern</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-5" style="max-width:600px;">
        <div class="card shadow">
            <div class="card-header bg-danger text-white">
                <h4>Raise a Concern</h4>
            </div>
            <div class="card-body">
                <asp:Label ID="lblMsg" runat="server" CssClass="mb-3"></asp:Label>

                <div class="mb-3">
                    <label for="txtConsumer" class="form-label">Consumer Number</label>
                    <asp:TextBox ID="txtConsumer" runat="server" CssClass="form-control" MaxLength="20" />
                </div>

                <div class="mb-3">
                    <label for="txtMessage" class="form-label">Message</label>
                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" MaxLength="200" />
                </div>

                <asp:Button ID="btnSend" runat="server" Text="Submit Concern" CssClass="btn btn-danger" OnClick="btnSend_Click" />
            </div>
        </div>
    </form>
</body>
</html>
