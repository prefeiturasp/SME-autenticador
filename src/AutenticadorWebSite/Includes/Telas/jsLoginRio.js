function jsLoginRio() {
    $("#login .container input").attr('autocomplete', 'off');
    $("#divAlterarSenha input").attr('autocomplete', 'off');
    
   
    $("#login .container input:password, #login .container input:text ").each(function () {
        VerificaLogin($(this));
    }).keyup(function(event) {
        VerificaLogin($(this));
    }).change(function (event) {
        VerificaLogin($(this));
    });
}

function VerificaLogin(campo) {
    ($(campo).val() != "") ? $(campo).prev("label").hide() : $(campo).prev("label").fadeIn("slow");
}

function cvSenhaAtual_ClientValidate(sender, args) {
    $.ajax({
        type: "POST",
        url: "Login.aspx/ValidarSenhaAtual",
        data: '{ "senhaAtual": "' + args.Value + '", "usu_id": "' + usu_id + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            args.IsValid = data.d;
        },
        error: function (data, success, error) {
            args.IsValid = false;
        }
    });

    return;
}

// Insere as funções na lista de funcões - será chamado no Init.js.
arrFNC.push(jsLoginRio);