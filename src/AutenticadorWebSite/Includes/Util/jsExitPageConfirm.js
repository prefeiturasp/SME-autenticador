//jsTodo: adicionar o jsExitPageConfirm em todos os lugares que chamar a função ExitPageConfirmer()

//Função para mostrar confirmação ao sair da página
function ExitPageConfirmer() {
    var needToConfirmExit = false;
    
    //Seta a variável para mostrar confirmação ao sair da página como true no cabeçalho e false no conteúdo
    $('#hd a, #hd input[type="submit"]').unbind('click.confirmExit').bind('click.confirmExit', function() {
        needToConfirmExit = true;
    });
    $('.m a, .m input[type="submit"]').unbind('click.confirmExit').bind('click.confirmExit', function() {
        needToConfirmExit = true;
    });
    $('.breadCrumb a, .breadCrumb input[type="submit"]').unbind('click.confirmExit').bind('click.confirmExit', function() {
        needToConfirmExit = true;
    });
    $('#bd a').unbind('click.confirmExit').bind('click.confirmExit', function() {
        needToConfirmExit = false;
    });
    $('#bd .btn').unbind('click.confirmExit').bind('click.confirmExit', function() {
        needToConfirmExit = false;
    });
    $('#bd .btnMensagemUnload').unbind('click.confirmExit').bind('click.confirmExit', function() {
        needToConfirmExit = true;
    });

    var Mensagem = "Atenção, caso você saia da página todas as informações não salvas serão perdidas.";
    window.onbeforeunload = function() {
        if (needToConfirmExit) {
            return Mensagem;
        }
    }
}
