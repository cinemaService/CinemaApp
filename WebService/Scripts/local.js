function Correct() {
    alert("CinamaApp service:\n Rezerwacja została przyjęta do realizacji! \n Oczekuj wiadomości e-mail z potwierdzeniem.");
}

function checkIsEmpty(field) {
    var mystring = document.getElementById('Email').value;
    if (!mystring.match(/\S/)) {
        alert('Podaj adres e-mail!');
        return false;
    }
    Correct();
    return true;
}

function unblock() {
    var email = document.getElementById('Email');
    email.readOnly = false;
    email.focus();
}