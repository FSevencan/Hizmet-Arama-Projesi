

$(document).ready(function () {

    function validate(input, event) {
        var isValid = true;

        if (input.attr('id') === 'FirstName' || input.attr('id') === 'LastName') {
            var name = input.val();
            var nameRegex = /^[a-zA-ZğüşöçİĞÜŞÖÇ]+$/;
            if (!nameRegex.test(name)) {
                if (event.type === 'focusout') {
                    $("#" + input.attr('id') + "Error").remove();
                    $("#validationErrors").append("<p id='" + input.attr('id') + "Error'>* Ad ve Soyad sadece harflerden oluşmalıdır.</p>");
                }
                isValid = false;
            } else {
                $("#" + input.attr('id') + "Error").remove();
            }
        }

        if (input.attr('id') === 'Email') {
            var email = input.val();
            var emailRegex = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
            if (!emailRegex.test(email) || $.trim(email) === "") {
                if (event.type === 'focusout') {
                    $("#emailError").remove();
                    $("#validationErrors").append("<p id='emailError'>* Email formatı yanlıştır.</p>");
                }
                isValid = false;
            } else {
                $("#emailError").remove();
            }
        }

        if (input.attr('id') === 'Password') {
            var password = input.val();
            var passwordRegex = /^(?=.*[A-Z])(?=.*[a-z]).{6,}$/;  // En az bir büyük harf, en az bir küçük harf ve en az 6 karakter uzunluğunda olmalı
            if (!passwordRegex.test(password)) {
                if (event.type === 'focusout') {
                    $("#passwordError").remove();
                    $("#validationErrors").append("<p id='passwordError'>* Şifreniz en az 6 karakter olmalı, en az bir büyük harf ve en az bir küçük harf içermeli.</p>");
                }
                isValid = false;
            } else {
                $("#passwordError").remove();
            }
        }

        if (input.attr('id') === 'PasswordConfirm') {
            var confirmPassword = input.val();
            var password = $("#Password").val();
            if (confirmPassword !== password) {
                if (event.type === 'focusout') {
                    $("#passwordConfirmError").remove();
                    $("#validationErrors").append("<p id='passwordConfirmError'>* Şifre aynı değildir.</p>");
                }
                isValid = false;
            } else {
                $("#passwordConfirmError").remove();
            }
        }

        return isValid;
    }

    $("#FirstName, #LastName, #Email, #Password, #PasswordConfirm").on("keyup focusout", function (event) {
        validate($(this), event);
    });

    $("#sign-up-form").on("submit", function (e) {
        var allValid = true;
        $("#FirstName, #LastName, #Email, #Password, #PasswordConfirm").each(function () {
            if (!validate($(this), e)) {
                allValid = false;
            }
        });

        if (!allValid) {
            e.preventDefault();
        }
    });
});





