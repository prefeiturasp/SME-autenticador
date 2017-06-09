using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Autenticador.WebServices.Consumer
{
    #region Enumerador

    public enum eTipoUserLive
    {
        Aluno = 1
        ,
        Docente
        ,
        Colaborador
    }

    #endregion

    #region Estruturas dados do usuário

    public struct DadosUserDocente
    {
        public string nome { get; set; }
        public string matricula { get; set; }
        public string escola { get; set; }
        public string turma { get; set; }
        public string serie { get; set; }
        public string CPF { get; set; }
        public string disciplina { get; set; }
    }

    public struct DadosUserAluno
    {
        public string nome { get; set; }
        public string matricula { get; set; }
        public string escola { get; set; }
        public string turma { get; set; }
        public string serie { get; set; }
    }

    public struct DadosUserColaborador
    {
        public string nome { get; set; }
        public string CPF { get; set; }
        public string cargo { get; set; }
        public string funcao { get; set; }
        public string setor { get; set; }
    }

    #endregion

    public class UserLive
    {
        #region Propriedades

        private eTipoUserLive tipoUserLive;

        public eTipoUserLive TipoUserLive
        {
            get { return tipoUserLive; }
        }
       
        public string login { get; set; }

        public string email { get; set; }

        public string senha { get; set; }

        public byte situacao { get; set; }

        public DadosUserAluno dadosUserAluno { get; set; }

        public DadosUserDocente dadosUserDocente { get; set; }

        public DadosUserColaborador dadosUserColaborador { get; set; }

        #endregion

        #region Construtores

        public UserLive(eTipoUserLive tipo)
        {
            tipoUserLive = tipo;
        }

        public UserLive()
        {
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Valida os dados do usuário
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            switch (TipoUserLive)
            {
                case eTipoUserLive.Aluno:
                    {
                        return ValidateAluno();
                    }
                case eTipoUserLive.Docente:
                    {
                        return ValidateDocente();
                    }
                case eTipoUserLive.Colaborador:
                    {
                        return ValidateColaborador();
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }

        /// <summary>
        /// Valida os dados do usuário Aluno
        /// </summary>
        /// <returns></returns>
        private bool ValidateAluno()
        {
            return (!string.IsNullOrEmpty(dadosUserAluno.nome) && 
                    !string.IsNullOrEmpty(dadosUserAluno.matricula));
        }

        /// <summary>
        /// Valida os dados do usuário Professor
        /// </summary>
        /// <returns></returns>
        private bool ValidateDocente()
        {
            return (!string.IsNullOrEmpty(dadosUserDocente.nome) &&
                    !string.IsNullOrEmpty(dadosUserDocente.CPF));
        }

        /// <summary>
        /// Valida os dados do usuário Funcionário
        /// </summary>
        /// <returns></returns>
        private bool ValidateColaborador()
        {
            return (!string.IsNullOrEmpty(dadosUserColaborador.nome) &&
                    !string.IsNullOrEmpty(dadosUserColaborador.CPF));
        }

        #endregion
    }
}
