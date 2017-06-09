for (var i in arrFNCSys) {
    if (Sys) {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(arrFNCSys[i]);
    }
}

if (Sys) {
    // http://support.microsoft.com/?kbid=2000262
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoading(disposeTree);

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function(sender, args) {
        if (args.get_error() && args.get_error().name === 'Sys.WebForms.PageRequestManagerTimeoutException') {
            args.set_errorHandled(true);
            $(".loader").hide();
        }
    });

    // Problema com ajax e google chrome:
    // http://seejoelprogram.wordpress.com/2008/09/04/adding-google-chrome-to-aspnet-ajax/
    Sys.Browser.Chrome = {};    
    if (navigator.userAgent.indexOf(' Chrome/') > -1) {
        Sys.Browser.agent = Sys.Browser.Chrome;
        Sys.Browser.version = parseFloat(navigator.userAgent.match(/ Chrome\/(\d+\.\d+)/)[1]);
        Sys.Browser.name = 'Chrome';
        Sys.Browser.hasDebuggerStatement = true;
    }
}

$(document).ready(function() {
    for (var i in arrFNC)
        arrFNC[i]();
});


// Função de correção de bug do IE 10, que existe no .Net 3.5 ou inferior.
// http://stackoverflow.com/questions/13299685/ie10-sending-image-button-click-coordinates-with-decimals-floating-point-values/15129393#15129393
$(function () {
    // Patch fractional .x, .y form parameters for IE10.
    if (typeof (Sys) !== 'undefined' && Sys.Browser.agent === Sys.Browser.InternetExplorer && Sys.Browser.version === 10) {
        Sys.WebForms.PageRequestManager.getInstance()._onFormElementActive = function Sys$WebForms$PageRequestManager$_onFormElementActive(element, offsetX, offsetY) {
            if (element.disabled) {
                return;
            }
            this._activeElement = element;
            this._postBackSettings = this._getPostBackSettings(element, element.name);
            if (element.name) {
                var tagName = element.tagName.toUpperCase();
                if (tagName === 'INPUT') {
                    var type = element.type;
                    if (type === 'submit') {
                        this._additionalInput = encodeURIComponent(element.name) + '=' + encodeURIComponent(element.value);
                    }
                    else if (type === 'image') {
                        this._additionalInput = encodeURIComponent(element.name) + '.x=' + Math.floor(offsetX) + '&' + encodeURIComponent(element.name) + '.y=' + Math.floor(offsetY);
                    }
                }
                else if ((tagName === 'BUTTON') && (element.name.length !== 0) && (element.type === 'submit')) {
                    this._additionalInput = encodeURIComponent(element.name) + '=' + encodeURIComponent(element.value);
                }
            }
        };
    }
});