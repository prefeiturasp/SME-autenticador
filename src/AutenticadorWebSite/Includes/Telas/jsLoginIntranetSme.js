function jsLoginIntranetSME() {
   heightBD();
   $(window).resize(function () {
      heightBD();
   });

   $("input[id$='_btnEnviar']").unbind("click").bind("click", function () {
       // remove validacoes repetidas
       $("div[id$='ValidationSummary2']").html(GetUnique($("div[id$='ValidationSummary2']").html().split('<br>')).join('<br>'));
       if ($("div[id$='ValidationSummary2']").text() != '') {
           $("div[id$='ValidationSummary2']").css('display', 'block');
       }
   });

   $("input[id$='_btnSalvar']").unbind("click").bind("click", function () {
       // remove validacoes repetidas
       $("div[id$='ValidationSummary3']").html(GetUnique($("div[id$='ValidationSummary3']").html().split('<br>')).join('<br>'));
       if ($("div[id$='ValidationSummary3']").text() != '') {
           $("div[id$='ValidationSummary3']").css('display', 'block');
       }
   });
}

function heightBD() {
   porcentagem = (95 * 100) / $(window).height();
   altura = $(window).height() - ($("#hd").outerHeight() + $("#ft").outerHeight() +62);
   $("#bd").css('height', altura); 
}

function GetUnique(inputArray) {
    var outputArray = [];
    for (var i = 0; i < inputArray.length; i++) {
        if ((jQuery.inArray(inputArray[i].trim(), outputArray)) == -1) {
            outputArray.push(inputArray[i].trim());
        }
    }
    return outputArray;
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
arrFNC.push(jsLoginIntranetSME);