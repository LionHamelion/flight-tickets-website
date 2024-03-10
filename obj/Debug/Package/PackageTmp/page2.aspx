<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page2.aspx.cs" Inherits="Lab5.page2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Нове замовлення - Крок 1</title>
    <link rel="stylesheet" href="Style.css"; />
    <script type="text/javascript">
    var allCities = []; // Масив для зберігання всіх міст

    function initializeCities() {
        var departure = document.getElementById('<%= DepartureCity.ClientID %>');
        var arrival = document.getElementById('<%= ArrivalCity.ClientID %>');

        // Запам'ятовуємо всі доступні міста
        for (var i = 0; i < departure.options.length; i++) {
            allCities.push(departure.options[i].value);
        }

        // Встановлюємо різні міста за замовчуванням для списків
        if (allCities.length > 1) {
            departure.value = allCities[0];
            arrival.value = allCities[1];
        }
    }

    function updateCityList() {
        var departure = document.getElementById('<%= DepartureCity.ClientID %>');
        var arrival = document.getElementById('<%= ArrivalCity.ClientID %>');

        var selectedDeparture = departure.value;
        var selectedArrival = arrival.value;

        // Оновлення списку міста прибуття
        updateOptions(arrival, selectedDeparture, selectedArrival);

        // Оновлення списку міста відправлення
        updateOptions(departure, selectedArrival, selectedDeparture);
    }

    function updateOptions(list, excludeCity, selectedCity) {
        list.options.length = 0; // Очищаємо список
        for (var i = 0; i < allCities.length; i++) {
            if (allCities[i] !== excludeCity) {
                var option = new Option(allCities[i], allCities[i]);
                option.selected = allCities[i] === selectedCity;
                list.options.add(option);
            }
        }
    }

    // Функція для встановлення доступності поля для вибору дати зворотнього рейсу
    function toggleReturnDateAvailability() {
        var tripType = document.querySelector('input[name="<%= TripType.UniqueID %>"]:checked').value;
        var returnDateInput = document.getElementById('<%= ReturnDate.ClientID %>');
        returnDateInput.disabled = (tripType !== 'roundtrip');
    }


    window.onload = function () {
        initializeCities();
        toggleReturnDateAvailability(); // Встановити початковий стан поля для дати зворотнього рейсу
    };
    </script>


</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <h2>НОВЕ ЗАМОВЛЕННЯ - КРОК 1</h2>
        <div>
            <label>Прізвище/Ім'я латиницею:</label>
            <asp:TextBox ID="NameTextBox" runat="server" CssClass="form-control" />
        </div>
        <div>
            <label>ФОТОГРАФІЯ (JPEG/PNG, min 100x150, max 200x300):</label>
            <asp:FileUpload ID="PhotoUpload" runat="server" CssClass="form-control" />
        </div>
        <div>
    <label for="DepartureCity">ЗВІДКИ:</label>
    <asp:DropDownList ID="DepartureCity" runat="server" onchange="updateCityList()" />
        </div>
        <div>
    <label for="ArrivalCity">КУДИ:</label>
    <asp:DropDownList ID="ArrivalCity" runat="server" onchange="updateCityList()" />
        </div>
        <div>
            <asp:RadioButtonList ID="TripType" runat="server" RepeatDirection="Horizontal" CssClass="form-control" onchange="toggleReturnDateAvailability()">
                <asp:ListItem Text="ТУДИ І НАЗАД" Value="roundtrip" Selected="True"></asp:ListItem>
                <asp:ListItem Text="В ОДИН КІНЕЦЬ" Value="oneway"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div>
            <label>Дата прямого рейсу:</label>
            <asp:TextBox ID="FlightDate" runat="server" CssClass="form-control" TextMode="Date" />
        </div>
        <div>
            <label>Дата зворотнього рейсу:</label>
            <asp:TextBox ID="ReturnDate" runat="server" CssClass="form-control" TextMode="Date" />
        </div>
        <div class="buttons">
        <asp:Button ID="PrevBtn" runat="server" Text="НАЗАД" OnClick="PrevBtn_Click" />
        <asp:Button ID="NextBtn" runat="server" Text="ДАЛІ" OnClick="NextBtn_Click" />
        </div>
    </div>
    </form>
</body>
</html>
