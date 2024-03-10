<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page1.aspx.cs" Inherits="Lab5.page1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Літаєте з авіакомпанією "Крутий Штопор"!</title>
    <link rel="stylesheet" href="Style.css"; />
    <script src="main.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Літаєте з авіакомпанією "Крутий Штопор"!</h2>

            <label for="lastName">Прізвище/Ім'я латиницею:</label><br />
            <asp:TextBox ID="nameInput" runat="server"></asp:TextBox>
            <label for="email">Емейл-адреса:</label><br />
            <asp:TextBox ID="emailInput" runat="server"></asp:TextBox>
            <asp:Button ID="NewOfferBtn" runat="server" Text="Нове замовлення" OnClientClick="HandleButtonClick();" OnClick="NewOfferBtn_Click" />
            <asp:Button ID="ExistingOffersBtn" runat="server" Text="Існуючі замовлення" OnClientClick="HandleButtonClick();" OnClick="ExistingOffersBtn_Click" />
        </div>
    </form>
</body>
</html>
