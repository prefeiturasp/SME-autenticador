using Autenticador.BLL;
using System;
using System.Data;

namespace ExpiraSenhaPadrao
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Executando...");
            try
            {
                // Busca todos os usuários dos sistema
                // que devem ter a senha alterada (grupos específicos que não estejam marcados para expirar senha).
                DataTable dtUsuarios = SYS_UsuarioBO.SelecionaUsuariosSenhaPadrao();
                string usuariosAtualizar = string.Empty;

                // Variáveis utilizadas para fazer a atualização aos poucos, porque senão não cabe na variável.
                int contador = 0;
                const int maxAtualizar = 100;

                // Percorre a lista de usuários
                foreach (DataRow drUsuario in dtUsuarios.Rows)
                {
                    contador++;

                    // Aplica a criptografia na senha padrão
                    eCriptografa criptografia = (eCriptografa)Enum.Parse(typeof(eCriptografa), drUsuario["usu_criptografia"].ToString(), true);
                    string senhaPadrao = UtilBO.CriptografarSenha(drUsuario["senhaPadrao"].ToString(), criptografia);

                    // Compara a criptografia com a senha do usuário
                    string senhaAtual = drUsuario["usu_senha"].ToString();
                    if (UtilBO.EqualsSenha(senhaAtual, senhaPadrao, criptografia))
                    {
                        // Se for a mesma, coloco o usuário para expirar a senha (usu_situacao = 5)
                        if (string.IsNullOrEmpty(usuariosAtualizar))
                        {
                            usuariosAtualizar = drUsuario["usu_id"].ToString();
                        }
                        else
                        {
                            usuariosAtualizar += string.Format(",{0}", drUsuario["usu_id"].ToString());
                        }
                    }

                    if (contador == maxAtualizar && !string.IsNullOrEmpty(usuariosAtualizar))
                    {
                        SYS_UsuarioBO.ExpiraUsuariosSenhaPadrao(usuariosAtualizar);
                        contador = 0;
                        usuariosAtualizar = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(usuariosAtualizar))
                {
                    SYS_UsuarioBO.ExpiraUsuariosSenhaPadrao(usuariosAtualizar);
                }

                Console.WriteLine("Usuários atualizados com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
