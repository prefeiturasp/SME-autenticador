using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Autenticador.WebServices.Adapter
{
    [Serializable]
    public class ContaLive
    {
        [XmlElement]
        public string email { get; set; }
        [XmlElement]
        public string senha { get; set; }
        [XmlElement]
        public byte situacao { get; set; }
        [XmlElement]
        public byte status { get; set; }
        [XmlElement]
        public string erro { get; set; }
    }

    public interface IServiceUserLive
    {
        ContaLive CriarContaEmailAluno(string nome, string matricula, string escola, string turma, string serie);

        ContaLive AlterarContaEmailSenha(string email, string novasenha, string ativo);

        ContaLive VerificarContaEmailExistente(string email);
    }
}
