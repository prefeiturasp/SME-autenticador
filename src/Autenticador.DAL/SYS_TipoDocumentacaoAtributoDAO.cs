using Autenticador.DAL.Abstracts;
using CoreLibrary.Data.Common;
using System;
using System.Data;

namespace Autenticador.DAL
{
    /// <summary>
    /// DAO - SYS_TipoDocumentacaoAtributo
    /// </summary>
    public class SYS_TipoDocumentacaoAtributoDAO : Abstract_SYS_TipoDocumentacaoAtributoDAO
    {
        public DataTable SelecionarAtributos()
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoDocumentacaoAtributo_SELECT", this._Banco);
            try
            {
                qs.Execute();
                return qs.Return;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }

        public DataTable SelecionarStringAtributosDefault(bool exibirRetorno)
        {
            QuerySelectStoredProcedure qs = new QuerySelectStoredProcedure("NEW_SYS_TipoDocumentacaoAtributo_SelecionaInfoDefault", this._Banco);
            try
            {
                Param = qs.NewParameter();
                Param.DbType = DbType.Byte;
                Param.ParameterName = "@ExibirRetorno";
                Param.Size = 1;
                Param.Value = exibirRetorno;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@AtributosDefault";
                Param.Size = 512;
                Param.Value = DBNull.Value;
                Param.Direction = ParameterDirection.Output;
                qs.Parameters.Add(Param);

                Param = qs.NewParameter();
                Param.DbType = DbType.String;
                Param.ParameterName = "@NomeObjetosDefault";
                Param.Size = 1024;
                Param.Value = DBNull.Value;
                Param.Direction = ParameterDirection.Output;
                qs.Parameters.Add(Param);

                //Param = qs.NewParameter();
                //Param.DbType = DbType.AnsiString;
                //Param.ParameterName = "@tdo_nome";
                //Param.Size = 100;
                //Param.Value = entity.tdo_nome;
                //qs.Parameters.Add(Param);

                //Param = qs.NewParameter();
                //Param.DbType = DbType.AnsiString;
                //Param.ParameterName = "@tdo_sigla";
                //Param.Size = 10;
                //if (!string.IsNullOrEmpty(entity.tdo_sigla))
                //    Param.Value = entity.tdo_sigla;
                //else
                //    Param.Value = DBNull.Value;
                //qs.Parameters.Add(Param);

                //Param = qs.NewParameter();
                //Param.DbType = DbType.Byte;
                //Param.ParameterName = "@tdo_validacao";
                //Param.Size = 1;
                //if (entity.tdo_validacao > 0)
                //    Param.Value = entity.tdo_validacao;
                //else
                //    Param.Value = DBNull.Value;
                //qs.Parameters.Add(Param);

                //Param = qs.NewParameter();
                //Param.DbType = DbType.Byte;
                //Param.ParameterName = "@tdo_situacao";
                //Param.Size = 1;
                //Param.Value = entity.tdo_situacao;
                //qs.Parameters.Add(Param);

                //Param = qs.NewParameter();
                //Param.DbType = DbType.DateTime;
                //Param.ParameterName = "@tdo_dataAlteracao";
                //Param.Size = 8;
                //Param.Value = DateTime.Now;
                //qs.Parameters.Add(Param);

                //Param = qs.NewParameter();
                //Param.DbType = DbType.Int32;
                //Param.ParameterName = "@tdo_classificacao";
                //Param.Size = 1;
                //if (entity.tdo_classificacao > 0)
                //    Param.Value = entity.tdo_classificacao;
                //else
                //    Param.Value = DBNull.Value;
                //qs.Parameters.Add(Param);

                //Param = qs.NewParameter();
                //Param.DbType = DbType.String;
                //Param.ParameterName = "@tdo_atributos";
                //Param.Size = 1024;
                //if (!string.IsNullOrEmpty(entity.tdo_atributos))
                //    Param.Value = entity.tdo_atributos;
                //else
                //    Param.Value = DBNull.Value;
                //qs.Parameters.Add(Param);

                qs.Execute();
                return qs.Return;
            }
            catch
            {
                throw;
            }
            finally
            {
                qs.Parameters.Clear();
            }
        }
    }
}