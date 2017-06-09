using Autenticador.BLL.V2;
using Autenticador.Entities;
using System;
using System.Collections.Generic;

namespace AutenticadorAPI
{
    public static class AppContext //: IAppContext
    {
        private static List<SYS_Entidade> ListaDeEntidades { get; set; }

        public static Guid GetIdEntidadeSistema()
        {
            var entidadeSistema = new SYS_Entidade();
            if (ListaDeEntidades.Count == 1)
            {
                entidadeSistema = ListaDeEntidades[0];
            }
            else
            {
                //Multientidade Implementar Strategy
                entidadeSistema = RetornarEntidadeAtual();
            }

            return entidadeSistema.ent_id;
        }

        public static SYS_Entidade RetornarEntidadeAtual()
        {
            throw new NotImplementedException("Multi Entidade não suportada");
        }

        public static void Inicializar()
        {
            SelecionarEntidades();
        }

        public static void SelecionarEntidades()
        {
            ListaDeEntidades = EntidadeBO.SelecionarTodasLigadasPorSistemas();
        }
    }

    public interface IAppContext
    {
        Guid GetIdEntidadeSistema();

        SYS_Entidade RetornarEntidadeAtual();

        void SelecionarEntidades();

        void Inicializar();
    }
}