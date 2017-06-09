/**********Construtor**********/
var arrFNC = new Array();
var arrFNCSys = new Array();
var execute = false;
var controlDialog = '';
var isCoreUI = false;
if (typeof coreUI === 'object') {
    isCoreUI = true;
}
/**********Métodos**********/

//Itens a serem inicializados
function Util() {
    $.isFunction($.stylesheetToggle) && $.stylesheetInit();
    //lê cookie do tamanho da fonte
    var s = readCookie('tamanhoFonte'),
        normalFontSize = "62.5%",
        minFontSize = "50%",
        maxFontSize = "75%",
        el = $("body");

    if (isCoreUI) {
        normalFontSize = "100%";
        minFontSize = "90%";
        maxFontSize = "110%";
        el = $("html");
    }

    if (s) {
        (s == "lnkDiminuirFonte") ? el.css("font-size", minFontSize) : null;
        (s == "lnkFonteNormal") ? el.css("font-size", normalFontSize) : null;
        (s == "lnkAumentarFonte") ? el.css("font-size", maxFontSize) : null;
    }

    //bind dos itens de alterar fonte
    $(".lnkDiminuirFonte").unbind('click').click(function (a) { a.preventDefault(); el.css("font-size", minFontSize); createCookie('tamanhoFonte', $(this).attr("class"), 365); });
    $(".lnkFonteNormal").unbind('click').click(function (a) { a.preventDefault(); el.css("font-size", normalFontSize); createCookie('tamanhoFonte', $(this).attr("class"), 365); });
    $(".lnkAumentarFonte").unbind('click').click(function (a) { a.preventDefault(); el.css("font-size", maxFontSize); createCookie('tamanhoFonte', $(this).attr("class"), 365); });

    if (!isCoreUI) {

        //bind do item de alterar alto contraste
        $(".styleSwitch")
            .unbind('click')
            .click(
            function (e) {
                e.preventDefault();
                var s = this.getAttribute('rel');
                $.stylesheetSwitch(s);
                if (s == "altoContraste") {
                    $(this)
                    .removeClass("lnkAltoContraste")
                    .addClass("lnkNormal")
                    .attr("rel", "css")
                    .attr("title", "Mudar esquema de cores Normal")
                    .html("N");
                } else {
                    $(this)
                    .removeClass("lnkNormal")
                    .addClass("lnkAltoContraste")
                    .attr("rel", "altoContraste")
                    .attr("title", "Mudar esquema de cores para Alto Contraste")
                    .html("C");
                }
            });

        //Menu Hover IE6 fix
        if ($.browser.msie && ($.browser.version <= 6)) {
            $(".i").unbind('hover').hover(function () {
                $(this).children(".s").addClass("dB");
            }, function () {
                $(this).children(".s").removeClass("dB");
            });
        }

        //Mostra Menu on focus
        // .m = menu, .i = items, .s = submenu, dB = display:block;
        $(".i a").unbind('focus').focus(function () {
            $(".s").removeClass("dB"); 						// esconde todos submenus
            $(this).next(".s").addClass("dB"); 				// mostra submenus do item
            $(this).parents(".s").addClass("dB"); 			// mantem submenu anterior abertos
        });

        $(".i a").unbind('focusout').focusout(function () { $(".s").removeClass("dB"); });
        // Fim Mostra Menu on focus

        //Menu Sistemas
        var sistemas = $('.spUl');

        // Menu Sistemas
        $('.hplSistemas').unbind("click").bind("click", function (a) {
            a.stopPropagation();
            $(sistemas).fadeToggle(200);

            $(document).unbind("click").bind("click", function (b) {
                var allowedTargets = 'spUl,hplSistemas,fecharSistemas'.split(',');

                if (!$.inArray(b.target.className, allowedTargets))
                    return false;
                else {
                    $(sistemas).fadeOut(200);
                    $(this).unbind("click");
                }
            });
        });

        $('.fecharSistemas input').unbind("click").bind('click', function () {
            $(sistemas).fadeOut(200);
        });

        // Fim Menu Sistemas

        /*IE6 select z-index fix*/
        if ($.browser.msie && $.browser.version <= 6) {
            $(".menu").unbind('hover');
            $(".menu").hover(
            function () {
                $("select").css("visibility", "hidden");
            },
            function () {
                $("select").css("visibility", "visible");
            });
        }

        $(".msgInfo div, .msgInfo input[type='text'], .msgInfo select, .msgInfo textarea").unbind('focus').focus(function () {

            $(".msgInfo div, .msgInfo input[type='text'], .msgInfo select, .msgInfo textarea").unbind('focusout');

            var selector = ".msgInfo";

            if (($('.msgInfo').attr('tagName') != 'fieldset') &&
                ($('.msgInfo').attr('tagName') != 'FIELDSET'))
                selector += ' fieldset';

            var margin = ($.browser.msie ? "27" : "-10");
            $(selector).first().prepend('<div id="msgInformacao" class="msgInformacao" style="margin-top:' + margin + 'px;width:180px;">Não é necessário preencher todos os campos para realizar a pesquisa.</div>');

            $("#msgInformacao").fadeIn();

            $(this).unbind('focusout').focusout(function () {
                if ($("#msgInformacao").length > 0)
                    $("#msgInformacao").remove();
            });
        });
    }
}

// cookie functions http://www.quirksmode.org/js/cookies.html
function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}
function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
function eraseCookie(name) {
    createCookie(name, "", -1);
}

// Cria um JQuey Dialog na div passada pelo id.
function createDialog(id, width, heigth) {

    try {
        // Primeiro dá UnBind, se estiver instanciado anteriormente, limpa memória.
        $(id).unbind('dialog');
    }
    catch (e) {
    }

    $(id).dialog({
        bgiframe: true,
        autoOpen: false,
        resizable: false,
        modal: true,
        width: width,
        closeOnEscape: true,
        open: function (type, data) {
            $(id).parent().prependTo($("#aspnetForm"));

            if (!(($.browser.msie) && ($.browser.version <= 7))) {
                $(id).dialog("option", "position", "center");
                $(window).scroll(function () {
                    $(id).dialog("option", "position", "center");
                });
            }
            if (($.browser.msie) && ($.browser.version == 9)) {
                if (controlDialog == '') {
                    controlDialog = id;
                    $('body').css('overflow', 'hidden');
                }
            }
        },
        close: function (type, data) {
            $(window).unbind("scroll");

            if (($.browser.msie) && ($.browser.version == 9)) {
                if (controlDialog == id) {
                    controlDialog = '';
                    $('body').css('overflow', 'visible');
                }
            }
        },
        buttons: {}
    }).removeClass("hide");


    // Caso a resolução seja 800x600 diminui o valor da altura máxima
    if ($(window).height() < 500) {
        $('.ui-dialog').css("max-height", '380px');
        $('.ui-dialog-content').css("max-height", '340px');
    }

    // Seta heigth - não obrigatório.
    if (heigth > 0)
        $(id).dialog("option", "height", heigth);

    $(id).find('.subir').unbind('click.Subir').bind('click.Subir', function () {
        $(id).scrollTop(0);
    });

    // Workaround bug #6644: Select in Dialog causes slowness on IE8
    //(http://bugs.jqueryui.com/ticket/6644)
    if (($.browser.msie) && ($.browser.version < 9))
        $(id + ' select').unbind('mousedown.dialogSelect').bind('mousedown.dialogSelect', function (e) { e.stopPropagation(); return false; });
}

// Remove JQuey Dialog na div passada pelo id.
function RemoveConfirmDialogButton(buttonId) {
    $(buttonId)
        .die('click')
        .live('click', function (e) {
            //recebe do clientID do botão de excluir
            btnAction = "#" + this.id;
            //Cria alerta de confirmação para delete dos grids
            if ($("#divConfirm").length > 0)
                $("#divConfirm").remove();
            return true;
        });
}

function disposeTree(sender, args) {
    // http://support.microsoft.com/?kbid=2000262
    var elements = args.get_panelsUpdating();
    for (var i = elements.length - 1; i >= 0; i--) {
        var element = elements[i];
        var allnodes = element.getElementsByTagName('*'),
                length = allnodes.length;
        var nodes = new Array(length)
        for (var k = 0; k < length; k++) {
            nodes[k] = allnodes[k];
        }
        for (var j = 0, l = nodes.length; j < l; j++) {
            var node = nodes[j];
            if (node.nodeType === 1) {
                if (node.dispose && typeof (node.dispose) === "function") {
                    node.dispose();
                }
                else if (node.control && typeof (node.control.dispose) === "function") {
                    node.control.dispose();
                }

                var behaviors = node._behaviors;
                if (behaviors) {
                    behaviors = Array.apply(null, behaviors);
                    for (var k = behaviors.length - 1; k >= 0; k--) {
                        behaviors[k].dispose();
                    }
                }
            }
        }
        element.innerHTML = "";
    }
}

function Max(txarea, tamanho) {
    total = tamanho;
    tam = txarea.value.length;
    str = "";
    str = str + tam;
    if (tam > total) {
        aux = txarea.value;
        txarea.value = aux.substring(0, total);
    }
}

function MsgInformacao(idFieldset) {
    $(idFieldset + " div, " + idFieldset + " input, " + idFieldset + " select, " + idFieldset + " textarea").unbind('focus').focus(function () {

        $(idFieldset + " div, " + idFieldset + " input, " + idFieldset + " select, " + idFieldset + " textarea").unbind('focusout');

        var selector = idFieldset;

        if (($(idFieldset).attr('tagName') != 'fieldset') &&
            ($(idFieldset).attr('tagName') != 'FIELDSET'))
            selector += ' fieldset';

        var margin = ($.browser.msie ? "27" : "-10");
        $(selector).first().prepend('<div id="msgInformacao" class="msgInformacao" style="margin-top:' + margin + 'px;width:180px;">Não é necessário preencher todos os campos para realizar a pesquisa.</div>');

        $("#msgInformacao").fadeIn();

        $(this).unbind('focusout').focusout(function () {
            if ($("#msgInformacao").length > 0)
                $("#msgInformacao").remove();
        });
    });
}

// Remove os nós que o IE9 cria com texto vazio dentro das tabelas.
function RemoveNosTextoVazioTabelasIE9() {
    if (($.browser.msie) && ($.browser.version == 9)) {
        var x = $('#aspnetForm').find('table');
        // Remove os nós que o IE9 cria com texto vazio.
        RemoveNosTextoVazio(x);
    }
}

// Remove os nós que o IE9 cria com texto vazio (chamar somente para tables).
function RemoveNosTextoVazio(nodes) {
    for (var i = 0; i < nodes.length; i++) {
        if (nodes[i].hasChildNodes())
            RemoveNosTextoVazio(nodes[i].childNodes);

        // Nó de texto vazio.
        if ((nodes[i].nodeValue != undefined) && (nodes[i].tagName == undefined) && (nodes[i].nodeName == "#text") &&
            nodes[i].nodeValue.replace(/^\s*/, "").replace(/\s*$/, "") == "") {
            nodes[i].parentNode.removeChild(nodes[i]);
            i--;
        }
    }
}

//Insere as funções na lista de funcões
arrFNC.push(Util);
arrFNCSys.push(Util);