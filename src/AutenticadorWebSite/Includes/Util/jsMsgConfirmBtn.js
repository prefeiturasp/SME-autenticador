
function SetConfirmDialogButton(buttonId,message){$(buttonId).die('click').live('click',function(e){var IsValid=(typeof Page_IsValid!='undefined'?Page_IsValid:true);if(IsValid){btnAction="#"+this.id;if($("#divConfirm").length>0)
$("#divConfirm").remove();$("<div id=\"divConfirm\" title=\"Confirmação\">"+message+"</div>").dialog({bgiframe:true,autoOpen:false,resizable:false,modal:true,buttons:{"Sim":function(){$(this).dialog("close");execute=true;if($(btnAction).attr("href")){window.location.href=$(btnAction).attr("href");}
else{$(btnAction).click();}},"Não":function(){$(this).dialog("close");}}});if(!execute){$("#divConfirm").dialog("open");}
var result=execute;execute=false;return result;}});}
function fecharBusca(id){$(id).dialog('close');}