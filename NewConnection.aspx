<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewConnection.aspx.cs" Inherits="ElectricityBillProject.NewConnection" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Apply for New Connection</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0">🔌 Apply for New Electricity Connection</h4>
            </div>
            <div class="card-body">
                <div class="row mb-3">
                    <div class="col-md-6">
                        <label for="txtName">Name</label>
                        <input type="text" id="txtName" name="txtName" class="form-control" required />
                    </div>
                    <div class="col-md-6">
                        <label for="txtPhone">Phone</label>
                        <input type="text" id="txtPhone" name="txtPhone" class="form-control" required />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6">
                        <label for="txtEmail">Email</label>
                        <input type="email" id="txtEmail" name="txtEmail" class="form-control" required />
                    </div>
                    <div class="col-md-6">
                        <label for="txtAddress">Address</label>
                        <input type="text" id="txtAddress" name="txtAddress" class="form-control" required />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6">
                        <label for="txtLocality">Locality</label>
                        <input type="text" id="txtLocality" name="txtLocality" class="form-control" required />
                    </div>
                    <div class="col-md-3">
                        <label for="ddlType">Type</label>
                        <select id="ddlType" name="ddlType" class="form-select" required>
                            <option value="">-- Select --</option>
                            <option value="Urban">Urban</option>
                            <option value="Rural">Rural</option>
                        </select>
                    </div>
                    <div class="col-md-3">
                        <label for="txtLoad">Load (kW)</label>
                        <input type="number" id="txtLoad" name="txtLoad" class="form-control" required />
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6">
                        <label for="ddlConnType">Connection Type</label>
                        <select id="ddlConnType" name="ddlConnType" class="form-select" required>
                            <option value="">-- Select --</option>
                            <option value="Single">Single</option>
                            <option value="Three">Three</option>
                        </select>
                    </div>
                    <div class="col-md-3">
                        <label for="fuId">ID Proof</label>
                        <input type="file" id="fuId" name="fuId" class="form-control" required />
                    </div>
                    <div class="col-md-3">
                        <label for="fuPhoto">Photo</label>
                        <input type="file" id="fuPhoto" name="fuPhoto" class="form-control" required />
                    </div>
                </div>

                <input type="submit" value="Apply" class="btn btn-success" />
                <span id="lblMsg" class="mt-3 d-block text-primary"></span>
            </div>
        </div>
    </form>
</body>
</html>
