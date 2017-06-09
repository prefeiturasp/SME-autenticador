using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Autenticador.UserControlLibrary.Buscas
{
    public abstract class Abstract_UCBusca : System.Web.UI.UserControl
    {
        #region DELEGATE

        public delegate void DelReturnValues(IDictionary<string, object> parameters);
        public delegate void DelSetLogErro(Exception err);

        #endregion

        protected IDictionary<string, object> returns = new Dictionary<string, object>();
        protected IDictionary<string, object> param = new Dictionary<string, object>();

        #region PROPRIEDADES

        public int Paginacao { get; set; }
        /// <summary>
        /// Nome do container onde foi colocado a busca 
        /// se houver (DIV da busca).
        /// </summary>
        public string ContainerName { get; set; }
        /// <summary>
        /// Delegate para realização de uma ação após a 
        /// seleção de um resultado da busca.
        /// </summary>
        public DelReturnValues ReturnValues { get; set; }
        /// <summary>
        /// Delegate para realização de um ação em caso de erro.
        /// </summary>
        public DelSetLogErro SetLogErro { get; set; }
        /// <summary>
        /// Valores retornados pela busca
        /// </summary>
        public IDictionary<string, object> Returns
        {
            get { return returns; }
            set { this.returns = value; }
        }
        /// <summary>
        /// Parametros externos para construção da busca
        /// </summary>
        protected IDictionary<string, object> Params
        {
            get { return param; }
            set { this.param = value; }
        }
        /// <summary>
        /// Check se o é uma post back assincrono ou não (Ajax ou não).
        /// </summary>
        protected bool IsAsyncPostBack
        {
            get
            {
                bool retorno = false;
                var sm = ScriptManager.GetCurrent(Page);
                if (sm != null)
                    retorno = sm.IsInAsyncPostBack;
                return retorno;
            }
        }

        #endregion

        #region METODOS

        /// <summary>
        /// Adiciona os parametros no usercontrol
        /// </summary>
        /// <param name="key">chave</param>
        /// <param name="value">valor</param>
        public void AddParameters(string key, object value)
        {
            this.Params.Add(key, value);
        }
        /// <summary>
        /// Adiciona os retornos da busca
        /// </summary>
        /// <param name="key">chave</param>
        /// <param name="value">valor</param>
        protected void SetReturns(string key, object value)
        {
            this.Returns.Add(key, value);
        }
        /// <summary>
        /// Fecha o popup da consulta.
        /// </summary>
        public void Close()
        {
            if (this.IsAsyncPostBack)
                ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "CloseDialog", String.Format("$(\"#{0}\").dialog(\"close\");", this.ContainerName), true);
            else
            {
                if (!Page.ClientScript.IsClientScriptBlockRegistered("CloseDialog"))
                    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "CloseDialog", String.Format("$(\"#{0}\").dialog(\"close\");", this.ContainerName), true);
            }
        }

        #endregion

        #region INICIALIZE

        public void Inicialize()
        {
            this.Inicialize(0, String.Empty);
        }

        public void Inicialize(int paginacao)
        {
            this.Inicialize(paginacao, String.Empty);
        }

        public void Inicialize(int paginacao, string containerName)
        {
            this.Paginacao = paginacao;
            this.ContainerName = containerName;
        }

        #endregion
    }
}
