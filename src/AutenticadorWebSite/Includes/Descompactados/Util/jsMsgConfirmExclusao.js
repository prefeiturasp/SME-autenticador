//jsTodo: Adicionar esse arquivo onde tiver CssClass="btExcluir" ou SkinID="btExcluir".

var btnAction = "";
var executeExcluir = false;

function jsMsgConfirm() {
    //Cria janela de confirmação para o delete
    $('.btExcluir')
        .die('click')
        .live('click', function() {
            //recebe do clientID do botão de excluir
            btnAction = "#" + this.id;
            //Cria alerta de confirmação para delete dos grids
            if ($("#divConfirm").length > 0)
                $("#divConfirm").remove();
            $("<div id=\"divConfirm\" title=\"Confirmação\">Confirma a exclusão?</div>").dialog({
                bgiframe: true,
                autoOpen: false,
                resizable: false,
                modal: true,
                buttons: {
                    "Sim": function() {
                        $(this).dialog("close");
                        executeExcluir = true;
                        $(btnAction).click();
                    },
                    "Não": function() {
                        $(this).dialog("close");
                    }
                }
            });
            //Abre a mensagem de confirmação
            if (!executeExcluir) {
                $("#divConfirm").dialog("open");
            }
            //If execute is true, it means that it was set by the yes callback
            //and so we should return true in order to not interfer with the form submission
            var result = executeExcluir;
            executeExcluir = false;
            return result;
        });
    }

//Insere as funções na lista de funcões
arrFNC.push(jsMsgConfirm);
