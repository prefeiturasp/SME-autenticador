//jsTodo: adicionar o arquivo jsBuscaDinamica em todas as páginas que utilizarem a função buscaDinamica()

//Criar busca dinamica
function buscaDinamica(buttonId, w, h) {
    $(buttonId)
        .unbind('click')
        .click(function(e) {
            e.preventDefault();
            var horizontalPadding = 30;
            var verticalPadding = 30;
            var idFrame = 'ifrm' + $(this).attr('id');
            $('<iframe frameborder="0" id="' + idFrame + '" src="' + (($(this).attr('alt')) ? $(this).attr('alt') : '#') + '"/>').dialog({
                title: (($(this).attr('title')) ? $(this).attr('title') : 'Busca'),
                autoOpen: true,
                resizable: true,
                modal: true,
                width: w,
                height: h,
                close: function(type, data) {
                    $(this).remove();
                }
            }).width(w - horizontalPadding).height(h - verticalPadding);
        });
}
