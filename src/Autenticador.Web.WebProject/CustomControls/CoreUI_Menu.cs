using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace Autenticador.Web.WebProject.CustomControls
{
    [DefaultProperty("MenuXMLDataSource"),
        ToolboxData("<{0}:CoreUI_Menu runat=server></{0}:CoreUI_Menu>")]
    public class CoreUI_Menu : WebControl
    {
        #region Propriedades

        private string textoVoltar;

        /// <summary>
        /// Texto que será renderizado nos submenus para voltar.
        /// </summary>
        public string TextoVoltar
        {
            get
            {
                if (string.IsNullOrEmpty(textoVoltar))
                {
                    return "Voltar";
                }

                return textoVoltar;
            }
            set
            {
                textoVoltar = value;
            }
        }

        private string textoInicio;

        /// <summary>
        /// Texto que será renderizado nos submenus para voltar.
        /// </summary>
        public string TextoInicio
        {
            get
            {
                if (string.IsNullOrEmpty(textoInicio))
                {
                    return "Início";
                }

                return textoInicio;
            }
            set
            {
                textoInicio = value;
            }
        }

        /// <summary>
        /// DataSource de Menu.
        /// </summary>
        public XmlDataSource MenuXMLDataSource { private get; set; }

        /// <summary>
        /// Retorna uma instância da classe de menus.
        /// </summary>
        private menus EstruturaMenu
        {
            get
            {
                if (ViewState["CoreUI_Menu_EstruturaMenu"] != null)
                {
                    // Se está em ViewState, retorna o item do ViewState.
                    return (menus)ViewState["CoreUI_Menu_EstruturaMenu"];
                }

                if (MenuXMLDataSource == null)
                {
                    return null;
                }

                string xml = MenuXMLDataSource.GetXmlDocument().InnerXml;
                if (string.IsNullOrEmpty(xml))
                {
                    return new menus();
                }

                using (MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(menus));
                    menus menu = (menus)serializer.Deserialize(memStream);

                    return menu;
                }
            }
        }

        #endregion Propriedades

        #region Métodos

        /// <summary>
        /// Renderiza um item de menu, verificando se possui filhos.
        /// Chama recursivamente os filhos.
        /// </summary>
        /// <param name="writer">Conteúdo HTML</param>
        /// <param name="pai">Módulo pai</param>
        private void RenderizaItem(HtmlTextWriter writer, menusMenu pai)
        {
            bool possuiFilho = (pai.item != null && pai.item.Count() > 0);
            if (possuiFilho)
            {
                writer.Write("<li class='has-submenu'>");
            }
            else
            {
                writer.Write("<li>");
            }

            writer.Write("<a href=\"" + Page.ResolveUrl(pai.url) + "\">" + pai.id + "</a>");

            if (possuiFilho)
            {
                // Renderizar os itens filhos - nível 1.
                writer.Write("<ul class='left-submenu'>");

                // Link voltar.
                writer.Write("<li class='back'>");
                writer.Write("<a href=\"#\">" + TextoVoltar + "</a>");
                writer.Write("</li>");

                // Título do menu pai.
                //writer.Write("<li>");
                //writer.Write("<label>" + pai.id + "</label>");
                //writer.Write("</li>");

                // Renderizar filhos.
                foreach (menusMenuItem filho in pai.item.OrderBy(p => Convert.ToInt32(p.ordem)))
                {
                    menusMenu menuFilho = new menusMenu
                    {
                        id = filho.id
                         ,
                        ordem = filho.ordem
                         ,
                        url = filho.url
                         ,
                        item = filho.subitem
                    };
                    RenderizaItem(writer, menuFilho);
                }

                writer.Write("</ul>");
            }

            writer.Write("</li>");
        }

        #endregion Métodos

        #region Eventos sobrescritos

        /// <summary>
        /// Retorna o objeto que será salvo no ViewState.
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            return EstruturaMenu;
        }

        /// <summary>
        /// Seta o objeto salvo no ViewState.
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            ViewState["CoreUI_Menu_EstruturaMenu"] = savedState;
        }

        /// <summary>
        /// Renderiza o conteúdo HTML do controle.
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            menus estruturaMenu = EstruturaMenu;

            if (estruturaMenu != null && estruturaMenu.Items != null && estruturaMenu.Items.Count() > 0)
            {
                writer.Write("<aside class='left-off-canvas-menu'>");
                writer.Write("<ul class='off-canvas-list'>");
                // Escreve o primeiro item "Início".
                //writer.Write("<li>");
                //writer.Write("<label>" + TextoInicio + "</label>");
                //writer.Write("</li>");

                foreach (menusMenu pai in estruturaMenu.Items.OrderBy(p => Convert.ToInt32(p.ordem)))
                {
                    RenderizaItem(writer, pai);
                }

                writer.Write("</ul>");
                writer.Write("</aside>");
            }
        }

        #endregion Eventos sobrescritos
    }

    #region Classe HierarquiaMenu

    //------------------------------------------------------------------------------
    // <auto-generated>
    //     O código foi gerado por uma ferramenta.
    //     Versão de Tempo de Execução:4.0.30319.18444
    //
    //     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
    //     o código for gerado novamente.
    // </auto-generated>
    //------------------------------------------------------------------------------
    //
    // This source code was auto-generated by xsd, Version=4.0.30319.18020.
    //

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class menus
    {
        private menusMenu[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("menu", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public menusMenu[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class menusMenu
    {
        private menusMenuItem[] itemField;

        private string idField;

        private string urlField;

        private string ordemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("item", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public menusMenuItem[] item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ordem
        {
            get
            {
                return this.ordemField;
            }
            set
            {
                this.ordemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class menusMenuItem
    {
        private menusMenuItem[] subitemField;

        private string idField;

        private string urlField;

        private string ordemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("subitem", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public menusMenuItem[] subitem
        {
            get
            {
                return this.subitemField;
            }
            set
            {
                this.subitemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ordem
        {
            get
            {
                return this.ordemField;
            }
            set
            {
                this.ordemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class menusMenuItemSubitem
    {
        private menusMenuItemSubitemSubitem2[] subitem2Field;

        private string idField;

        private string urlField;

        private string ordemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("subitem2", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public menusMenuItemSubitemSubitem2[] subitem2
        {
            get
            {
                return this.subitem2Field;
            }
            set
            {
                this.subitem2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ordem
        {
            get
            {
                return this.ordemField;
            }
            set
            {
                this.ordemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class menusMenuItemSubitemSubitem2
    {
        private string idField;

        private string urlField;

        private string ordemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ordem
        {
            get
            {
                return this.ordemField;
            }
            set
            {
                this.ordemField = value;
            }
        }
    }

    #endregion Classe HierarquiaMenu
}
