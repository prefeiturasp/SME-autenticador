$(document).ready(function () {
    //Acessibilidade
    //var isCoreUI = false;
    //if (typeof coreUI === 'object') { isCoreUI = true; }

    var s = readCookie('tamanhoFonte'),
    normalFontSize = "100%",
    minFontSize = "90%",
    maxFontSize = "110%",
    el = $("html");

    /*if(isCoreUI){
        normalFontSize="100%";
        minFontSize="90%";
        maxFontSize="110%";
        el=$("html");
    }*/

    if (s) {
        (s == "lnkDiminuirFonte") ? el.css("font-size", minFontSize) : null;
        (s == "lnkFonteNormal") ? el.css("font-size", normalFontSize) : null;
        (s == "lnkAumentarFonte") ? el.css("font-size", maxFontSize) : null;
    }

    $(".lnkDiminuirFonte").unbind('click').click(function (a) {
        a.preventDefault();
        el.css("font-size", minFontSize);
        createCookie('tamanhoFonte', $(this).attr("class"), 365);
    });

    $(".lnkFonteNormal").unbind('click').click(function (a) {
        a.preventDefault();
        el.css("font-size", normalFontSize);
        createCookie('tamanhoFonte', $(this).attr("class"), 365);
    });

    $(".lnkAumentarFonte").unbind('click').click(function (a) {
        a.preventDefault();
        el.css("font-size", maxFontSize);
        createCookie('tamanhoFonte', $(this).attr("class"), 365);
    });

    //if (!isCoreUI) {
    $(".styleSwitch").unbind('click').click(function (e) {
        e.preventDefault();

        var s = this.getAttribute('rel');
        $.stylesheetSwitch(s);

        if (s == "altoContraste") {
            $(this).removeClass("lnkAltoContraste").addClass("lnkNormal").attr("rel", "css").attr("title", "Mudar esquema de cores Normal").html("N");
        } else {
            $(this).removeClass("lnkNormal").addClass("lnkAltoContraste").attr("rel", "altoContraste").attr("title", "Mudar esquema de cores para Alto Contraste").html("C");
        }
    });
    //}

    function createCookie(name, value, days) {
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            var expires = "; expires=" + date.toGMTString();
        }
        else var expires = "";
        document.cookie = name + "=" + value + expires + "; path=/";
    }

    function readCookie(name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ')
                c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0)
                return c.substring(nameEQ.length, c.length);
        }
        return null;
    }
});