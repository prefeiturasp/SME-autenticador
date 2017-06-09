using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Autenticador.Adapters
{
    public class MenuAdapter : System.Web.UI.WebControls.Adapters.MenuAdapter
    {
        protected override void RenderBeginTag(HtmlTextWriter writer)
        {
            if (Control != null)
            {
                writer.Indent++;
                writer.WriteLine();
                writer.WriteBeginTag("ul");
                writer.WriteAttribute("class", "m");
                writer.Write(HtmlTextWriter.TagRightChar);
            }
        }

        protected override void RenderEndTag(HtmlTextWriter writer)
        {
            if (Control != null)
            {
                writer.WriteLine();
                writer.WriteEndTag("ul");
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Indent++;
            BuildItems(Control.Items, writer, true);
            writer.Indent--;
        }

        private void BuildItems(MenuItemCollection items, HtmlTextWriter writer, bool menu)
        {
            if (items.Count > 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    bool root = ((items[i].ChildItems != null) && (items[i].ChildItems.Count > 0));
                    string c = string.Empty;
                    string seta = String.Empty;

                    writer.WriteLine();
                    writer.WriteBeginTag("li");
                    //c += (root) ? "i" : null;
                    //c += (!(root) && (i == 0) ? " p" : null);
                    //c += (!(menu) && (i == (items.Count - 1)) ? " u" : null);
                    //c += ((root) && (menu) && (i == (items.Count - 1) && (items.Count >= 6)) ? " l" : null);

                    //Alterado para que os nós que não possuem filhos sejam formatados com o css
                    c += "i";
                    c += (i == 0) ? " p" : null;
                    c += (!(menu) && (i == (items.Count - 1)) ? " u" : null);
                    c += ((menu) && (i == (items.Count - 1) && (items.Count >= 6)) ? " l" : null);

                    writer.WriteAttribute("class", c);
                    writer.Write(HtmlTextWriter.TagRightChar);
                    writer.WriteBeginTag("a");
                    //writer.WriteAttribute("href", (root) ? "#" : Page.ResolveUrl(items[i].NavigateUrl));
                    //Alterado para adicionar o link em nó com filho, caso possua url configurada
                    writer.WriteAttribute("href", String.IsNullOrEmpty(items[i].NavigateUrl) ? "#" : Page.ResolveUrl(items[i].NavigateUrl));

                    writer.Write(HtmlTextWriter.TagRightChar);
                    if (items[i].ChildItems.Count > 0)
                        seta = "<span class=\"dDA\">" + ((menu) ? "&#9660;" : "&#9658;") + "</span>";
                    writer.Write(items[i].Text + seta);
                    writer.WriteEndTag("a");                  
                    if (root)
                    {
                        writer.Indent++;
                        writer.WriteLine();
                        writer.WriteBeginTag("ul");
                        writer.WriteAttribute("class", "s");
                        writer.Write(HtmlTextWriter.TagRightChar);

                        writer.Indent++;
                        BuildItems(items[i].ChildItems, writer, false);
                        writer.Indent--;

                        writer.WriteLine();
                        writer.WriteEndTag("ul");
                        writer.WriteLine();
                        writer.Indent--;
                    }
                    writer.WriteEndTag("li");
                }
            }
        }
    }
}
