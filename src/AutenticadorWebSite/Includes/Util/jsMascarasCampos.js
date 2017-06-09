//jsTodo: Adicionar o jsMascaraCampos quando utilizar as classes:
// numeric, currency, currencycomma, maskHora, maskData

// Define tipo de dados a serem preenchidos
function formatarCampos() {
    //Campos que permitem só números
    $(".numeric").unbind("validation").validation({ type: "int" });

    //Campos que permitem apenas valores
    $(".currency").unbind("validation").validation({ type: "int", add: ".," });
    $(".currencycomma").unbind("validation").validation({ type: "int", add: "," });
}

//Define mascaras padrão
function mascarasCampos() {
    //mascara de hora 'HH:mm'
    $(".maskHora").setMask({ mask: '29:59', selectCharsOnFocus: false, autoTab: false });

    //mascara de data 'dd/mm/yyyy'
    $(".maskData").setMask({ mask: '39/19/2999', selectCharsOnFocus: false, autoTab: false });

    // mascara do codigo de validacao do documento
    $(".maskCodValidacao").setMask({ mask: '****-****-****-****', selectCharsOnFocus: true, autoTab: false });
}

//Insere as funções na lista de funcões
arrFNC.push(formatarCampos);
arrFNCSys.push(formatarCampos);
arrFNC.push(mascarasCampos);
arrFNCSys.push(mascarasCampos);
