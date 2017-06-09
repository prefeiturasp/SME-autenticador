using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Reflection;
using CoreLibrary.Data.Common;
using CoreLibrary.Data.Common.Abstracts;
using Autenticador.Entities;

namespace Autenticador.DAL.Abstracts
{
    public abstract class Abstract_GestaoCore<T> : Abstract_DAL<T>, IRequiresSessionState
    {
        public Abstract_GestaoCore()
            : base()
        {
        }

        public Abstract_GestaoCore(TalkDBTransaction banco)
            : base(banco)
        {            
        }

        public override bool Delete(T entity)
        {
            try
            {
                this.GetSystemLogContext();
                if (sys_modulo != null && sys_modulo.mod_auditoria)
                    this.Carregar(entity);
                bool ret = base.Delete(entity);
                if (ret)
                {
                    if (sys_modulo != null && sys_modulo.mod_auditoria)
                    {
                        JavaScriptSerializer oSerializer = new JavaScriptSerializer();
                        string oldValue = oSerializer.Serialize(entity);
                        if (!this.Auditoria(log_id, TipoAuditoria.Delete, oldValue, String.Empty))
                            throw new Exception("Erro ao tentar salvar a auditoria");
                    }
                }
                return ret;
            }
            catch
            {
                throw;
            }
        }

        protected override bool Inserir(T entity)
        {
            try
            {
                bool ret = base.Inserir(entity);
                if (ret)
                {
                    this.GetSystemLogContext();
                    if (sys_modulo != null && sys_modulo.mod_auditoria)
                    {
                        JavaScriptSerializer oSerializer = new JavaScriptSerializer();
                        string newValue = oSerializer.Serialize(entity);
                        if (!this.Auditoria(log_id, TipoAuditoria.Insert, String.Empty, newValue))
                            throw new Exception("Erro ao tentar salvar a auditoria");
                    }
                }
                return ret;
            }
            catch
            {
                throw;
            }
        }

        protected override bool Alterar(T entity)
        {
            try
            {
                string oldValue = String.Empty;
                JavaScriptSerializer oSerializer = new JavaScriptSerializer();
                this.GetSystemLogContext();
                if (sys_modulo != null && sys_modulo.mod_auditoria)
                {
                    //Recebe os dados da entidade a ser alterada
                    T aux = Activator.CreateInstance<T>();
                    PropertyInfo[] properties = aux.GetType().GetProperties();
                    foreach (PropertyInfo p in properties)
                    {
                        //PropertyInfo prop = entity.GetType().GetProperty(p.Name);
                        p.SetValue(aux, p.GetValue(entity, null), null);
                    }
                    this.Carregar(aux);
                    oldValue = oSerializer.Serialize(aux);
                }
                bool ret = base.Alterar(entity);
                if (ret)
                {
                    if (sys_modulo != null && sys_modulo.mod_auditoria)
                    {
                        string newValue = oSerializer.Serialize(entity);
                        if (!this.Auditoria(log_id, TipoAuditoria.Update, oldValue, newValue))
                            throw new Exception("Erro ao tentar salvar a auditoria");
                    }
                }
                return ret;
            }
            catch
            {
                throw;
            }
        }

        #region AUDITORIA

        protected Guid log_id = Guid.Empty;
        protected SYS_Modulo sys_modulo { get; set; }
        protected enum TipoAuditoria
        {
            Insert,
            Update,
            Delete
        }
        /// <summary>
        /// Grava a auditoria caso necessário.
        /// </summary>
        /// <param name="log_id">ID do log de auditoria</param>
        /// <param name="operacao">Operação a ser realizada (Insert, update ou delete)</param>
        /// <param name="oldValues">Valor da entidade antes da operação</param>
        /// <param name="newValues">Valor da entidade após a operação</param>
        /// <returns>True se a auditoria foi gravada corretamente</returns>
        protected virtual bool Auditoria(Guid log_id, TipoAuditoria operacao, string oldValues, string newValues)
        {
            LOG_AuditoriaDAL dao = new LOG_AuditoriaDAL();
            try
            {
                LOG_Auditoria entity = new LOG_Auditoria()
                {
                    log_id = log_id,
                    aud_dataHora = DateTime.Now,
                    aud_entidade = typeof(T).Name,
                    aud_operacao = Enum.GetName(typeof(TipoAuditoria), operacao),
                    aud_entidadeNova = newValues,
                    aud_entidadeOriginal = oldValues
                };
                if (entity.Validate())
                    return dao.Salvar(entity);
                else
                    throw new ArgumentNullException("log_id", "log_id vázio.");
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Carrega o valor do log de sistema do atual context.
        /// </summary>
        protected virtual void GetSystemLogContext()
        {
            if (HttpContext.Current != null
                && (HttpContext.Current.Session[LOG_Sistema.SessionName] != null
                    && HttpContext.Current.Session[SYS_Modulo.SessionName] != null)
                && (HttpContext.Current.Session[LOG_Sistema.SessionName].GetType() == typeof(Guid)
                    && HttpContext.Current.Session[SYS_Modulo.SessionName].GetType() == typeof(SYS_Modulo)))
            {
                sys_modulo = (SYS_Modulo)HttpContext.Current.Session[SYS_Modulo.SessionName];
                log_id = new Guid(HttpContext.Current.Session[LOG_Sistema.SessionName].ToString());
            }
            else
            {
                sys_modulo = null;
                log_id = Guid.Empty;
            }
        }

        #endregion
    }
}
