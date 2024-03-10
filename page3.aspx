<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page3.aspx.cs" Inherits="Lab5.page3" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>НОВЕ ЗАМОВЛЕННЯ - КРОК 2</title>
    <style>
        /* Chrome, Safari, Edge, Opera */
        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
          -webkit-appearance: none;
          margin: 0;
        }

        /* Firefox */
        input[type=number] {
          -moz-appearance: textfield;
}       #ClassSelection, #AgeSelection{
            max-width: 60%;
            margin-bottom: 25px;
        }
        #LuggageCheckbox{
            font-size: 25px;
        }

    </style>
    <script type="text/javascript" src="main.js"></script>
    <script type="text/javascript">
        function updateCounters(isChild, isIncrease) {
            var counterId = isChild ? 'ChildCount' : 'AdultCount';
            var counter = document.getElementById(counterId);
            var currentValue = parseInt(counter.value, 10);
            if (isIncrease) {
                counter.value = currentValue + 1;
            } else if (currentValue > 0) {
                counter.value = currentValue - 1;
            }

            OnDataChange();
        }

        function toggleElements(isBusinessClass) {
            var radioButtons = document.querySelectorAll('#AgeSelection input[type="radio"]');
            for (var i = 0; i < radioButtons.length; i++) {
                radioButtons[i].disabled = isBusinessClass;
            }
            var luggageCheckbox = document.getElementById('LuggageCheckbox');
            luggageCheckbox.disabled = isBusinessClass;
            OnDataChange();
        }

        function calculateTicketPrice(departureCity, arrivalCity, isBusinessClass, isAdult, accompanyingAdult, accompanyingChild, luggage, isReturnTicket) {
            function getBasePrice(departureCity, arrivalCity) {
                const route = (departureCity.toLowerCase() + "-" + arrivalCity.toLowerCase());

                switch (route) {
                    case "київ-гамбург":
                    case "гамбург-київ":
                        return 1000;
                    case "київ-тбілісі":
                    case "тбілісі-київ":
                        return 1500;
                    case "київ-шарм-ель-шейх":
                    case "шарм-ель-шейх-київ":
                        return 2000;
                    case "гамбург-тбілісі":
                    case "тбілісі-гамбург":
                        return 2500; 
                    case "гамбург-шарм-ель-шейх":
                    case "шарм-ель-шейх-гамбург":
                        return 2500; 
                    case "тбілісі-шарм-ель-шейх":
                    case "шарм-ель-шейх-тбілісі":
                        return 1500; 
                    default:
                        throw new ArgumentException("Невідомий маршрут");
                }
            }
            let basePrice = getBasePrice(departureCity, arrivalCity)
            let finalPrice = basePrice;

            if (isBusinessClass) {
                finalPrice *= 3; // Надбавка за бізнес-клас для основного пасажира
            } else {
                finalPrice = isAdult ? basePrice : 0.8 * basePrice; // Розрахунок базової ціни
            }

            // Додавання супроводжуючих осіб
            if (accompanyingAdult > 0) {
                finalPrice += accompanyingAdult * (isBusinessClass ? 2.5 * basePrice : 0.9 * basePrice);
            }
            if (accompanyingChild > 0) {
                finalPrice += accompanyingChild * (isBusinessClass ? 3 * 0.7 * basePrice : 0.7 * basePrice);
            }

            // Надбавка за багаж
            if (luggage && !isBusinessClass) {
                finalPrice *= 1.25;
            }

            // Перевірка на квиток в обидва кінці
            if (isReturnTicket) {
                finalPrice *= 2;
            }

            return Math.round(finalPrice);
        }

        var accompanyingAdult = 0, accompanyingChild = 0, isBusinessClass = false, isAdult = true, luggage = false;
        function OnDataChange() {
            accompanyingAdult = parseInt(document.getElementById("<%= AdultCount.ClientID %>").value);
            accompanyingChild = parseInt(document.getElementById("<%= ChildCount.ClientID %>").value);
            isBusinessClass = document.querySelector("input[name='ClassSelection']:checked").value === "business";
            isAdult = document.querySelector("input[name='AgeSelection']:checked").value === "adult";
            luggage = document.getElementById("<%= LuggageCheckbox.ClientID %>").checked;
            console.log("AccompChild: " + accompanyingChild);
            console.log("AccompAdult: " + accompanyingAdult);
            console.log("isBuss:" + isBusinessClass);
            console.log("luggage: " + luggage);
            updatePrice();
        }

        function updatePrice() {
            var price = calculateTicketPrice(departureCity, arrivalCity, isBusinessClass, isAdult, accompanyingAdult, accompanyingChild, luggage, isReturnTicket);
            document.getElementById("price").innerHTML = price + "₴";
        }

        window.onload = function () {
            toggleElements(false);
            updateCounters();
        };
        
    </script>
    <link rel="stylesheet" href="Style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>НОВЕ ЗАМОВЛЕННЯ - КРОК 2</h2>
            <div>
                <asp:Image ID="ProfileImage" runat="server" AlternateText="Profile photo" />
            </div>
            <div>
                <asp:Label ID="NameLabel" runat="server" />
            </div>
            <div>
                <asp:RadioButtonList ID="ClassSelection" runat="server" RepeatDirection="Horizontal" AutoPostBack="false">
                    <asp:ListItem Text="ЕКОНОМ-КЛАС" onclick="toggleElements(false)" Selected="True" Value="economy"></asp:ListItem>
                    <asp:ListItem Text="БІЗНЕС-КЛАС" onclick="toggleElements(true)" Value="business"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div>
                <asp:RadioButtonList ID="AgeSelection" runat="server">
                    <asp:ListItem Text="від 2 до 7 років (дитина)" onclick ="OnDataChange()" Value="child"></asp:ListItem>
                    <asp:ListItem Text="від 7 років (дорослий)" onclick = "OnDataChange()" Value="adult" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div>
                <asp:CheckBox ID="LuggageCheckbox" runat="server" Text="Наявність багажу" onclick="OnDataChange()" />
            </div>
            <div>
                <label>Кількість супроводжувальних осіб віком від 2 до 7 років:</label>
                <input type="button" value="-" onclick="updateCounters(true, false)" />
                <asp:TextBox ID="ChildCount" runat="server" Text="0" type="number" />
                <input type="button" value="+" onclick="updateCounters(true, true)" />
            </div>
            <div>
                <label>Кількість супроводжувальних осіб віком від 7 років:</label>
                <input type="button" value="-" onclick="updateCounters(false, false)" />
                <asp:TextBox ID="AdultCount" runat="server" Text="0" type="number" />
                <input type="button" value="+" onclick="updateCounters(false, true)" />
            </div>
            <div>
                <h2>Вартість замовлення: <span id="price"></span></h2>
            </div>
            <div class="buttons">
                <asp:Button ID="PrevBtn" OnClientClick="hideSubmitButtons()" UseSubmitBehavior="false" CausesValidation="False" runat="server" Text="НАЗАД" OnClick="PrevBtn_Click" />
                <asp:Button ID="NextBtn" runat="server" Text="ДАЛІ" OnClick="NextBtn_Click" />
            </div>
        </div>
    </form>
</body>
</html>