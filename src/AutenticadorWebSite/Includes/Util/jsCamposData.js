//jsTodo: Adicionar arquivo nas telas onde tiver campo com a classe "calendar".

function jsCamposData() {
    //Criar objeto calendário
    $(".calendar:not(:disabled)").unbind('datepicker').datepicker({
        showOn: "button",
        buttonImageOnly: true,
        buttonText: "Abrir Calendário",
        dateFormat: 'dd/mm/yy'
        , onSelect: function(dateText, inst) { this.fireEvent && this.fireEvent('onchange') || $(this).change(); }
        , dayNamesMin: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab']
        , monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro']
    });

    try {
        if (diretorioVirtual) {
            $(".calendar").datepicker("option", "buttonImage", diretorioVirtual + "icoCalendar.png");
        }
    }
    catch (e) {
    }
}

//Insere as funções na lista de funcões
arrFNC.push(jsCamposData);
arrFNCSys.push(jsCamposData);