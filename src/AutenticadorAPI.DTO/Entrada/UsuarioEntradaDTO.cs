using System;

namespace AutenticadorAPI.DTO.Entrada
{
    public class UsuarioEntradaDTO
    {
        /// <summary>
        /// ID da Entidade do usuário.
        /// </summary>
        public Guid ent_id { get; set; }

        /// <summary>
        /// Login do usuário.
        /// </summary>
        public string usu_login { get; set; }

        /// <summary>
        /// ID do grupo do usuário
        /// </summary>
        public Guid[] gru_id { get; set; }

        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        public string nome { get; set; }

        /// <summary>
        /// CPF do usuário
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Data de nascimento do usuário
        /// </summary>
        public DateTime dataNascimento { get; set; }

        /// <summary>
        /// Email do usuário
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Senha do Usuário
        /// </summary>
        public string senha { get; set; }

        /// <summary>
        /// ID da unidade administrativa vinculada ao usuário.
        /// </summary>
        public Guid uad_id { get; set; }

        /// <summary>
        /// Código da unidade administrativa vinculada ao usuário.
        /// </summary>
        public string uad_codigo { get; set; }

        /// <summary>
        /// Indica se deve trazer a foto.
        /// </summary>
        public bool trazerFoto { get; set; }

        /// <summary>
        /// ID do usuário.
        /// </summary>
        public Guid usu_id { get; set; }

        /// <summary>
        /// ID da pessoa vinculada ao usuário.
        /// </summary>
        public Guid pes_id { get; set; }

        /// <summary>
        /// Data de alteração mínima do registros retornados.
        /// </summary>
        public DateTime dataBase { get; set; }

        /// <summary>
        /// sexo do usuário. 1- Masculino / 2 - Feminino
        /// </summary>
        public byte sexo { get; set; }
    }
}
