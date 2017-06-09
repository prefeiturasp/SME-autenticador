//jsTodo: adicionar o arquivo jsMsgConfirmBtn em todas as páginas que usarem a função SetConfirmDialogButton()

//Inicio de caixa de confirmação
function SetConfirmDialogButton(buttonId, message) {
    $(buttonId)
        .die('click')
        .live('click', function(e) {
            var IsValid = (typeof Page_IsValid != 'undefined' ? Page_IsValid : true);
            if (IsValid) {
                //recebe do clientID do botão de excluir
                btnAction = "#" + this.id;
                //Cria alerta de confirmação para delete dos grids
                if ($("#divConfirm").length > 0)
                    $("#divConfirm").remove();
                $("<div id=\"divConfirm\" title=\"Confirmação\">" + message + "</div>").dialog({
                    bgiframe: true,
                    autoOpen: false,
                    resizable: false,
                    modal: true,
                    buttons: {
                        "Sim": function() {
                            $(this).dialog("close");
                            execute = true;
                            if ($(btnAction).attr("href")) {
                                window.location.href = $(btnAction).attr("href");
                            }
                            else {
                                $(btnAction).click();
                            }
                        },
                        "Não": function() {
                            $(this).dialog("close");
                        }
                    }
                });
                //Abre a mensagem de confirmação
                if (!execute) {
                    $("#divConfirm").dialog("open");
                }
                //If execute is true, it means that it was set by the yes callback
                //and so we should return true in order to not interfer with the form submission
                var result = execute;
                execute = false;
                return result;
            }
        });
}

function fecharBusca(id) {
    $(id).dialog('close');
}