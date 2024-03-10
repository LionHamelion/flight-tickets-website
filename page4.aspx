<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page4.aspx.cs" Inherits="Lab5.page4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Style.css"; />
    <script type="text/javascript" src="main.js"></script>
</head>
<body>
    <div class="container">
    <form id="form1" runat="server">
            <div>
                <h2>Замовлення додано успішно!</h2>
                <asp:Literal ID="ReportContainer" runat="server"></asp:Literal>
                <asp:Button ID="HomeBtn" runat="server" Text="На головну" OnClick="HomeBtn_Click" />
            </div>
        </form>
    </div>
    
</body>
</html>
