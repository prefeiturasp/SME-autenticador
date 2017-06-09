using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLibrary.Business.Common;
using Autenticador.DAL;
using Autenticador.Entities;
using System.Data;
using System.ComponentModel;


namespace Autenticador.BLL
{
    public class PES_TipoDeficienciaBO : BusinessBase<PES_TipoDeficienciaDAO, PES_TipoDeficiencia>
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static DataTable GetSelect
        (
           Guid tde_id
           , string tde_nome
           , byte tde_situacao
           , bool paginado
           , int currentPage
           , int pageSize
        )
        {
            totalRecords = 0;
            PES_TipoDeficienciaDAO dal = new PES_TipoDeficienciaDAO();

            if (pageSize == 0)
                pageSize = 1;

            try
            {
                return dal.SelectBy_All(tde_id, tde_nome, tde_situacao, paginado, currentPage / pageSize, pageSize, out totalRecords);
            }
            catch
            {
                throw;
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static bool VerificaNomeExistente(Autenticador.Entities.PES_TipoDeficiencia entity)
        {
            PES_TipoDeficienciaDAO dal = new PES_TipoDeficienciaDAO();
            return dal.SelectBy_Nome(entity.tde_nome, entity.tde_id);
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Save(Autenticador.Entities.PES_TipoDeficiencia entity)
        {
            PES_TipoDeficienciaDAO dal = new PES_TipoDeficienciaDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                if (entity.Validate())
                {
                    if (VerificaNomeExistente(entity))
                        throw new DuplicateNameException("Já existe um tipo de deficiência cadastrado com este nome.");
                    else                                           
                        dal.Salvar(entity);
                }
                else
                {
                    throw new CoreLibrary.Validation.Exceptions.ValidationException(entity.PropertiesErrorList[0].Message);
                }
                return true;
            }
            catch (Exception err)
            {
                dal._Banco.Close(err);
                throw;
            }
            finally
            {
                dal._Banco.Close();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public new static bool Delete
        (
            Autenticador.Entities.PES_TipoDeficiencia entity
        )
        {
            PES_TipoDeficienciaDAO dal = new PES_TipoDeficienciaDAO();
            dal._Banco.Open(IsolationLevel.ReadCommitted);

            try
            {
                if (dal.Select_Integridade(entity.tde_id) > 0)
                {
                    throw new Exception("Não é possível excluir o tipo de deficiência pois possui outros registros ligados a ele.");
                }
                else
                {
                    dal.Delete(entity);                    
                }

                return true;
            }
            catch (Exception err)
            {
                dal._Banco.Close(err);
                throw;
            }
            finally
            {
                dal._Banco.Close();
            }
        }
    }
}
