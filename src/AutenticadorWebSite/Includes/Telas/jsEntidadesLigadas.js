//$(document).ready(function() { arrFNC.push(jsEntidadesLigadas); jsEntidadesLigadas(); });
//Métodos
function jsEntidadesLigadas() {
    createDialog('#divEntidades', 550, 0);
}

// Insere as funções na lista de funcões - será chamado no Init.js.
arrFNC.push(jsEntidadesLigadas);