document.addEventListener('DOMContentLoaded', function () {
    var forms = document.querySelectorAll('form');

    // Додаємо обробник подій до всіх кнопок submit у формі
    forms.forEach(function (form){
        form.querySelectorAll('[type="submit"]').forEach(function (button) {
            button.addEventListener('click', function (event) {
                // Перевіряємо, чи форма дійсна
                if (form.checkValidity()) {
                    // Якщо форма дійсна, приховуємо кнопки
                    hideSubmitButtons();
                } else {
                    // Якщо форма не дійсна, зупиняємо подію, щоб кнопки не зникли
                    event.checkValidity();
                }
            });
        });
    });
});

function hideSubmitButtons() {
    document.querySelectorAll('[type="submit"]').forEach(function (button) {
        button.style.visibility = 'hidden';
    });
}