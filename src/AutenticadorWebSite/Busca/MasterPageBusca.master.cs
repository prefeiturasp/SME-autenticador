using System;
using System.Web.UI;
using Autenticador.BLL;

public partial class Busca_MasterPage : MasterPage
{
    protected override void OnInit(EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(Page);
        if (sm != null)
        {
            // Adiciona scripts.
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryCore));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryUI));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JqueryMask));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.Json));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryValidation));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryScrollTo));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.StylesheetToggle));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.UiAriaTabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.Util));
        }
        
        base.OnInit(e);
    }

    
}
