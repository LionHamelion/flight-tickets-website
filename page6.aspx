<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page6.aspx.cs" Inherits="Lab5.page6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Підтвердження вилучення замовлення</title>
    <link rel="stylesheet" href="Style.css"; />
    <script type="text/javascript" src="main.js"></script>
</head>
<body>
    <div class="container">
     <form id="form1" runat="server">
            <div>
                <h2>Ви дійсно хочете видалити це замовлення?</h2>
                <h2>Інформація про замовлення:</h2>
                <asp:Literal ID="ReportContainer" runat="server"></asp:Literal>
                <br />
                <asp:Button ID="btnDeleteAndEmail" runat="server" Text="ВИЛУЧИТИ" onclick="btnDeleteAndEmail_Click" />
            </div>
        </form>
    </div>
   
</body>
</html>
