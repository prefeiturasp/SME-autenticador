function jsConfigurarServico() {
    $("input[id$='chkDesativar']").click(function () {
        var checado = $(this).attr("checked");
        if (checado) {
            $(".divCampos").css("display", "none");
        } else {
            $(".divCampos").css("display", "");
        }
    });
}

arrFNC.push(jsConfigurarServico);
arrFNCSys.push(jsConfigurarServico);