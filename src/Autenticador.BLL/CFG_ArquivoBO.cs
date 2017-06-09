/*
	Classe gerada automaticamente pelo Code Creator
*/

using System;
using System.ComponentModel;
using System.IO;
using System.Web;
using CoreLibrary.Business.Common;
using Autenticador.Entities;
using Autenticador.DAL;
using CoreLibrary.Data.Common;
using CoreLibrary.Validation.Exceptions;
using System.Linq;

namespace Autenticador.BLL
{

    #region Enumerador

    /// <summary>
    /// Situa��o do registro
    /// </summary>
    public enum CFG_ArquivoSituacao : byte
    {
        Ativo = 1
        , Excluido = 3
        , Temporario = 4
    }

    #endregion

    /// <summary>
	/// CFG_Arquivo Business Object 
	/// </summary>
	public class CFG_ArquivoBO : BusinessBase<CFG_ArquivoDAO,CFG_Arquivo>
    {
        #region M�todos de Salvar

        /// <summary>
        /// Valida se o arquivo � v�lido
        /// </summary>
        /// <param name="entity">entity CFG_Arquivo</param>
        /// <param name="tamanhoMaximoKB">Tamanho m�ximo permitido em KB</param>
        /// <param name="TiposArquivosPermitidos">Array com os arquivos permitidos</param>
        /// <returns>True se OK</returns>
        /// <exception cref="ValidationException">Throw quando arquivo for inv�lido</exception>
        private static bool ValidarArquivo(CFG_Arquivo entity, int tamanhoMaximoKB, string[] TiposArquivosPermitidos)
        {
            return ValidarTamanhoArquivo(entity, tamanhoMaximoKB) && ValidarTipoArquivo(entity, TiposArquivosPermitidos);
        }

        /// <summary>
        /// Valida se o arquivo � v�lido
        /// </summary>
        /// <param name="entity">entity CFG_Arquivo</param>
        /// <param name="tamanhoMaximoKB">Tamanho m�ximo permitido em KB</param>
        /// <returns>True se OK</returns>
        /// <exception cref="ValidationException">Throw quando o tamanho exceder o tamanho m�ximo.</exception>
        private static bool ValidarTamanhoArquivo(CFG_Arquivo entity, int tamanhoMaximoKB)
        {
            bool ret;
            int tamanhoBytes = 1024;
            int tamArquivo = entity.arq_data.Length / tamanhoBytes;

            if (tamArquivo <= tamanhoMaximoKB)
                ret = true;
            else
                throw new ValidationException(String.Format("O arquivo \"{0}\"  excede o limite de {1}MB para anexos.", entity.arq_nome, (tamanhoMaximoKB / 1024)));

            return ret;
        }

        /// <summary>
        /// Valida se o arquivo � v�lido
        /// </summary>
        /// <param name="entity">entity CFG_Arquivo</param>
        /// <param name="TiposArquivosPermitidos">Array com os arquivos permitidos</param>
        /// <returns>True se OK</returns>
        /// <exception cref="ValidationException">Throw quando a extens�o n�o � permitida.</exception>
        private static bool ValidarTipoArquivo(CFG_Arquivo entity, string[] TiposArquivosPermitidos)
        {
            bool ret;
            string extensao = Path.GetExtension(entity.arq_nome);

            if (TiposArquivosPermitidos.Contains(extensao))
                ret = true;
            else
                throw new ValidationException(String.Format("Arquivos do tipo \"{0}\" n�o s�o permitidos.", extensao));

            return ret;
        }

        /// <summary>
        /// Salva o objeto CFG_Arquivo
        /// </summary>
        /// <param name="entity">entity CFG_Arquivo</param>
        /// <param name="tamanhoMaximoKB">Tamanho m�ximo permitido em KB</param>
        /// <param name="TiposArquivosPermitidos">Array com os arquivos permitidos</param>
        /// <param name="banco">Conex�o com o Banco de dados</param>
        /// <returns>True se OK</returns>
        /// <exception cref="ValidationException">Throw quando arquivo for inv�lido</exception>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static bool Save(CFG_Arquivo entity, int tamanhoMaximoKB, string[] TiposArquivosPermitidos, TalkDBTransaction banco)
        {
            bool ret = false;

            if (ValidarArquivo(entity, tamanhoMaximoKB, TiposArquivosPermitidos))
                ret = Save(entity, banco);

            return ret;
        }

        /// <summary>
        /// Salva o objeto CFG_Arquivo
        /// </summary>
        /// <param name="entity">entity CFG_Arquivo</param>
        /// <param name="tamanhoMaximoKB">Tamanho m�ximo permitido em KB</param>
        /// <param name="TiposArquivosPermitidos">Array com os arquivos permitidos</param>        
        /// <returns>True se OK</returns>
        /// <exception cref="ValidationException">Throw quando arquivo for inv�lido</exception>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public static bool Save(CFG_Arquivo entity, int tamanhoMaximoKB, string[] TiposArquivosPermitidos)
        {
            CFG_ArquivoDAO dao = new CFG_ArquivoDAO();
            return Save(entity, tamanhoMaximoKB, TiposArquivosPermitidos, dao._Banco);
        }   

        #endregion

        #region M�todos �teis

        /// <summary>
        /// Monta uma entidade de arquivo de acordo com o documento passado.
        /// </summary>
        /// <param name="postedFile">Documento usado para upload.</param>
        /// <returns>Entidade de arquivo.</returns>
        public static CFG_Arquivo CriarEntidadeArquivo(HttpPostedFile postedFile)
        {
            CFG_Arquivo entityArquivo;

            if (!string.IsNullOrEmpty(postedFile.FileName))
            {
                string nome = Path.GetFileName(postedFile.FileName);

                entityArquivo = new CFG_Arquivo
                {
                    arq_nome = nome
                    ,
                    arq_tamanhoKB = postedFile.ContentLength
                    ,
                    arq_typeMime = postedFile.ContentType
                    ,
                    arq_data = GetBytesFromHttpPostedFile(postedFile)
                    ,
                    arq_situacao = (byte)CFG_ArquivoSituacao.Ativo
                    ,
                    arq_dataCriacao = DateTime.Now
                    ,
                    arq_dataAlteracao = DateTime.Now
                };

                return entityArquivo;
            }

            entityArquivo = null;
            return entityArquivo;
        }

        /// <summary>
        /// Retorna o array de Bytes do arquivo do HttpPostedFile
        /// </summary>
        /// <param name="postedFile">HttpPostedFile</param>
        /// <returns>Array de Bytes</returns>
        public static byte[] GetBytesFromHttpPostedFile(HttpPostedFile postedFile)
        {
            byte[] file = null;

            if ((postedFile != null) && (postedFile.InputStream != null))
            {
                int Tamanho = Convert.ToInt32(postedFile.InputStream.Length);
                file = new byte[Tamanho];


                if (postedFile.InputStream.Length == 0)
                    throw new ValidationException("O arquivo tem 0 bytes, por isso ele n�o ser� anexado.");

                postedFile.InputStream.Read(file, 0, Tamanho);

            }
            return file;
        }

        #endregion
    }
}