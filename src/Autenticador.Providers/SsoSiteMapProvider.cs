using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using CoreLibrary.Data.Common;
using CoreLibrary.Providers;
using CoreLibrary.Providers.Entities;
using Autenticador.DAL;

namespace Autenticador.Providers
{
    public class SsoSiteMapProvider : MSSiteMapProvider
    {
        #region ATRIBUTOS

        private int sis_id;

        #endregion

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection attributes)
        {
            if (IsInitialized)
                return;

            base.Initialize(name, attributes);

            if (attributes["sistemaID"] != null)
                sis_id = Convert.ToInt32(attributes["sistemaID"]);
            else
            {
                initialized = false;
                throw new ArgumentNullException("attributes","O atributo attributes[\"sistemaID\"] é obrigatório na tag siteMap do web.config.\r\nProvider inválido!");
            }
        }

        public override IList<SiteMapPath> LoadDataSet()
        {
            DataTable dt = new DataTable();
            List<SiteMapPath> lt = new List<SiteMapPath>();
            SYS_ModuloSiteMapDAO dal = new SYS_ModuloSiteMapDAO();
            try
            {
                dt = dal.SelectBy_sis_id(sis_id);
                foreach (DataRow dr in dt.Rows)
                {
                    SiteMapPath smp = new SiteMapPath()
                    {
                        key = dr["mod_id"].ToString()
                        , url = ((dr["msm_url"] != DBNull.Value) ? dr["msm_url"].ToString() : null)
                        , title = dr["msm_nome"].ToString()
                        , description = ((dr["msm_descricao"] != DBNull.Value) ? dr["msm_descricao"].ToString() : null)
                        , fatherKey = ((dr["mod_idPai"] != DBNull.Value) ? dr["mod_idPai"].ToString() : null)
                    };

                    // Só adiciona na lista se não existir outro sitemap com a url igual.
                    if (!lt.Exists(p=>(p.url ?? "").Equals((smp.url ?? ""), StringComparison.OrdinalIgnoreCase)))
                        lt.Add(smp);
                }

                return lt;
            }
            catch
            {
                throw;
            }
        }
    }
}
