function SetUniqueRadioButton(nameregex, current) {
    re = new RegExp(nameregex);
    for(i = 0; i < document.forms[0].elements.length; i++) {
        elm = document.forms[0].elements[i]
        if (elm.type == 'radio') {
            if (re.test(elm.name)) {
                elm.checked = false;
            }
        }
    }
    current.checked = true;
}

function JS_ModuloSeguranca() {

    createDialog('#divParametro', 735, 0);

    try {
        $('#divParametro').unbind('dialogopen');
    }
    catch (e) {
    }

    $('#divParametro').bind("dialogopen", function(event, ui) {
        try {
            $('.ui-dialog-titlebar-close').unbind('click');
        }
        catch (e) {
        }

        $('.ui-dialog-titlebar-close').click(function() {
            if ($("input[id$='_btnCancelar']").size() > 0) {
                $("#" + $("input[id$='_btnCancelar']")[0].id).trigger('click');
                return false;
            }
            $('#divParametro').dialog('close');
        });
    });

    createDialog('#divAddGrupos', 600, 0);

    createDialog('#divUsuario', 600, 0);

    createDialog('#divAddSiteMap', 550, 0);
    
    createDialog('#divUA', 550, 0);

    createDialog('#divLog', 600, 500);

    createDialog('#divParametroGrupoPerfil', 550, 250);

    createDialog('#divBuscaUA', 550, 0);

    createDialog('#divBuscaPessoa', 550, 0);
    
    createDialog('#divBuscaUsuario', 550, 0);

    try {
        $('#divParametroGrupoPerfil').unbind('dialogopen');
    }
    catch (e) {
    }
    
    $('#divParametroGrupoPerfil').bind("dialogopen", function(event, ui) {
        try {
            $('.ui-dialog-titlebar-close').unbind('click');
        }
        catch (e) {
        }

        $('.ui-dialog-titlebar-close').click(function() {
            $("#" + $("input[id$='btn_Cancelar']")[0].id).trigger('click');
            return false;
        });
    });

    createTabs('#divLogErro_BuscaTabs', '', false);

    $("#divModulos").accordion({
        autoHeight: false
    });
}

function expandcollapse(obj, row) {
    var div = document.getElementById(obj);
    var img = document.getElementById('img' + obj);

    if (div.style.display == "none") {
        div.style.display = "block";
        img.alt = "Ocultar";
    }
    else {
        div.style.display = "none";
        img.alt = "Detalhar";
    }
}

function expandcollapse2(obj, id) {
    var div = document.getElementById(obj);

    if (div.style.display == "none") {
        div.style.display = "block";
        $('#lbldiv' + id).html('v');
    }
    else {
        div.style.display = "none";
        $('#lbldiv' + id).html('>');
    }
}

function ExpandCollapse3(objID, btnID) {
    var obj = document.getElementById(objID);

    if (obj.style.display == "none") {
        obj.style.display = "block";
        $('#' + btnID).removeClass('ui-icon ui-icon-circle-triangle-e');
        $('#' + btnID).addClass('ui-icon ui-icon-circle-triangle-s');
    }
    else {
        obj.style.display = "none";
        $('#' + btnID).removeClass('ui-icon ui-icon-circle-triangle-s');
        $('#' + btnID).addClass('ui-icon ui-icon-circle-triangle-e');
    }
}

// Insere as funções na lista de funcões - será chamado no Init.js.
arrFNC.push(JS_ModuloSeguranca);
