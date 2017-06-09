var mostraMsgTabs=true;function createTabs(idTab,idSelectedTab,mostraMsgNavegacao){mostraMsgTabs=true;try{$(idTab).unbind('tabs');}
catch(e){}
if(idSelectedTab==''){$(idTab).tabs({select:function(event,ui){$(idSelectedTab).val(ui.index);if(mostraMsgNavegacao)
$(ui.tab.hash+'-tab').focus();}});}
else{$(idTab).tabs({selected:$(idSelectedTab).val(),select:function(event,ui){$(idSelectedTab).val(ui.index);if(mostraMsgNavegacao)
$(ui.tab.hash+'-tab').focus();}});}
$(idTab).find(".ui-tabs-nav.hide, .ui-tabs-panel.hide").removeClass("hide");if(mostraMsgNavegacao){$(idTab+" ul, "+idTab+" ul li").unbind('focus').focus(function(){$(idTab+" ul, "+idTab+" ul li").unbind('focusout');if(mostraMsgTabs){$(idTab+" ul:not(.semMensagem)").append('<div id="msgTabs" class="msgTabs">Navegue entre as abas utilizando as setas.</div>');$("#msgTabs").fadeIn();mostraMsgTabs=false;}
$(this).unbind('focusout').focusout(function(){if($("#msgTabs").length>0)
$("#msgTabs").remove();mostraMsgTabs=true;});});$(idTab+" div, "+idTab+" input, "+idTab+" select, "+idTab+" textarea").unbind('focus').focus(function(){$(idTab+" div, "+idTab+" input, "+idTab+" select, "+idTab+" textarea").unbind('focusout');if(mostraMsgTabs){$(idTab+" ul:not(.semMensagem)").append('<div id="msgTabs" class="msgTabs">Para navegar entre as abas clique Ctrl+seta para cima.</div>');$("#msgTabs").fadeIn();mostraMsgTabs=false;}
$(this).unbind('focusout').focusout(function(){if($("#msgTabs").length>0)
$("#msgTabs").remove();mostraMsgTabs=true;});});}}