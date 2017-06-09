
var btnAction="";var executeExcluir=false;function jsMsgConfirm(){$('.btExcluir').die('click').live('click',function(){btnAction="#"+this.id;if($("#divConfirm").length>0)
$("#divConfirm").remove();$("<div id=\"divConfirm\" title=\"Confirmação\">Confirma a exclusão?</div>").dialog({bgiframe:true,autoOpen:false,resizable:false,modal:true,buttons:{"Sim":function(){$(this).dialog("close");executeExcluir=true;$(btnAction).click();},"Não":function(){$(this).dialog("close");}}});if(!executeExcluir){$("#divConfirm").dialog("open");}
var result=executeExcluir;executeExcluir=false;return result;});}
arrFNC.push(jsMsgConfirm);