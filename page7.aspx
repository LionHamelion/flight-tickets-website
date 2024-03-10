<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page7.aspx.cs" Inherits="Lab5.page7" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Успішно видалено</title>
    <link rel="stylesheet" href="Style.css"; />
    <script type="text/javascript" src="main.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Замовлення видалено успішно!</h2>
            <asp:Literal ID="ReportContainer" runat="server"></asp:Literal>
            <asp:Button ID="HomeBtn" runat="server" Text="На головну" OnClick="HomeBtn_Click" />
        </div>
    </form>
</body>
</html>
