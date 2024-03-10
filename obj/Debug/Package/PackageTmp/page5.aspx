<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page5.aspx.cs" Inherits="Lab5.page5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Список замовлень</title>
    <link rel="stylesheet" href="Style.css"; />
    <style>
        body {
            font-size: 16px;
            font-weight: bold;
            background-color: #f5f5f5;
        }
        table {
            width: 100%;
            border-collapse: collapse;
        }
        td, th {
            border: 1px solid black;
            padding: 10px;
        }
        img {
            max-width: 100px;
            max-height: 100px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="OrdersGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCommand="OrdersGrid_RowCommand">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Order ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="DepartureCity" HeaderText="Departure" />
                <asp:BoundField DataField="ArrivalCity" HeaderText="Arrival" />
                <asp:TemplateField HeaderText="Photo">
                    <ItemTemplate>
                        <img src='<%# ResolveUrl("~/photos/" + Eval("PhotoPath")) %>' alt="Фото" style="height:100px;" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                    <asp:Button ID="DeleteButton" runat="server" CommandName="DeleteOrder" CommandArgument='<%# Eval("Id") %>' Text="Видалити" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
