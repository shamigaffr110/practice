<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ElectricityBillProject.Payment" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>JBVNL – Electricity Bill Payment</title>
    <style>
        body {
            font-family: 'Segoe UI', sans-serif;
            background-color: #f5f5f5;
            padding: 30px;
        }
        .container {
            background-color: white;
            padding: 25px;
            max-width: 500px;
            margin: auto;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            border-radius: 8px;
        }
        h2 {
            color: #333;
            margin-bottom: 20px;
        }
        label {
            display: block;
            margin-top: 10px;
            font-weight: bold;
        }
        input[type="text"], select {
            width: 100%;
            padding: 8px;
            margin-top: 5px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .btn {
            margin-top: 20px;
            padding: 10px 20px;
            background-color: #0078D7;
            color: white;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
        .btn:hover {
            background-color: #005fa3;
        }
        #lblStatus {
            margin-top: 15px;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        function showQR() {
            var qr = document.getElementById('<%= qrImage.ClientID %>');
            qr.style.display = 'block';
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Pay Your Electricity Bill</h2>

            <asp:Label ID="lblStatus" runat="server" ForeColor="Green" /><br />

            <label for="txtConsumer">Consumer Number</label>
            <asp:TextBox ID="txtConsumer" runat="server" />

            <label for="txtAmount">Amount (INR)</label>
            <asp:TextBox ID="txtAmount" runat="server" />

            <label for="ddlMethod">Payment Method</label>
            <asp:DropDownList ID="ddlMethod" runat="server">
                <asp:ListItem Text="UPI" Value="UPI" />
                <asp:ListItem Text="Net Banking" Value="NetBanking" />
                <asp:ListItem Text="Credit Card" Value="CreditCard" />
            </asp:DropDownList>

            <asp:Button ID="btnGenQR" runat="server" CssClass="btn" Text="Generate QR" OnClick="btnGenQR_Click" />
            <asp:Image ID="qrImage" runat="server" ImageUrl="~/Styles/myQR.png" Style="display:none; margin:20px auto; width:256px; height:256px;" />
            <asp:Button ID="btnPayNow" runat="server" CssClass="btn" Text="Payment Now" OnClick="btnPayNow_Click" />
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Submit Payment" OnClick="btnSubmit_Click" Enabled="false" />
        </div>
    </form>
</body>
</html>
