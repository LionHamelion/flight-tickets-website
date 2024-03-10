function HandleButtonClick() {
    var buttons = document.querySelectorAll('input[type="submit"]');
    for (var i = 0; i < buttons.length; i++) {
        buttons[i].style.display = "none";
    }
}