(function($){var availableStylesheets=[];var activeStylesheetIndex=0;$.stylesheetToggle=function(){activeStylesheetIndex++;activeStylesheetIndex%=availableStylesheets.length;$.stylesheetSwitch(availableStylesheets[activeStylesheetIndex]);};$.stylesheetSwitch=function(styleName){$('link[@rel*=style][title]').each(function(i){this.disabled=true;if(this.getAttribute('title')==styleName){this.disabled=false;activeStylesheetIndex=i;}});createCookie('style',styleName,365);};$.stylesheetInit=function(){$('link[rel*=style][title]').each(function(i){availableStylesheets.push(this.getAttribute('title'));});var c=readCookie('style');if(c){if(c=="altoContraste"){$(".styleSwitch").removeClass("lnkAltoContraste").addClass("lnkNormal").attr("rel","css").attr("title","Mudar esquema de cores Normal").html("N");}
$.stylesheetSwitch(c);}};})(jQuery);