function JS_CadastroPermissoes() {
    $('input[class=selecionarTodos]').click(function () {
        var tabela = $(this).parent().parent().parent();
        tabela.find('[id*=chk' + $(this).attr('id') + ']').attr('checked', $(this).attr('checked'));
        tabela.find('[id*=' + $(this).attr('id') + ']').attr('checked', $(this).attr('checked'));
    });

    $('a[id*=lkbExpandir]').click(function () {
        var tabela = $(this).parent().parent().next().find('table');

        if ($(this).hasClass('ui-icon-circle-triangle-s'))
            tabela.css('display', '');
    });
}

arrFNC.push(JS_CadastroPermissoes);