function valido() {
    document.getElementById("email").required = true;
    document.getElementById("text-holder").required = true;

    var x = document.forms["forma_ime"]["email"].value;
    var atpos = x.indexOf("@");
    var dotpos = x.lastIndexOf(".");

    if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
        alert("Adresa nuk eshte e vlefshme");
        return false;
    }
}