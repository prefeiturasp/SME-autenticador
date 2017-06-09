using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Autenticador.Web.WebProject
{
    public class MotherUserControl : CoreLibrary.Web.WebProject.MotherUserControl
    {
        #region PROPRIEDADES

        public new SessionWEB __SessionWEB
        {
            get
            {
                return (SessionWEB)Session[CoreLibrary.Web.WebProject.ApplicationWEB.SessSessionWEB];
            }
            set
            {
                Session[CoreLibrary.Web.WebProject.ApplicationWEB.SessSessionWEB] = value;
            }
        }

        #endregion

        #region MÉTODOS

        /// <summary>
        /// Percorre o CheckBoxList passado, e checa os items em que o Value for encontrado dentro 
        /// da lista de ids passada.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="chkList"></param>
        public void ChecarItensLista(List<string> ids, CheckBoxList chkList)
        {
            foreach (ListItem item in chkList.Items)
            {
                ListItem listItem = item;

                // Seleciona o item, caso encontrado na lista.
                item.Selected = (ids.Exists(s => s.Equals(listItem.Value, StringComparison.OrdinalIgnoreCase)));
            }
        }

        /// <summary>
        /// Método de validação de campos data para ser usado em Validators.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void ValidarData_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime d;
            args.IsValid = DateTime.TryParse(args.Value, out d);
        }

        #endregion
    }
}
