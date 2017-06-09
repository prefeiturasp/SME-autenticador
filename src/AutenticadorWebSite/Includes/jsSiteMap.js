function toggleSiteMap(node) {
    var itens = $(node).parent().find("ul");
    if (itens.is(":hidden")) {
        $(node)
            .removeClass("ui-icon-plus")
            .addClass("ui-icon-minus")            
    }
    else {
        $(node)
            .removeClass("ui-icon-minus")
            .addClass("ui-icon-plus")
    }
    itens.slideToggle("slow");
}