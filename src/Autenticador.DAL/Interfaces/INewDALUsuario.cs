using Autenticador.Entities;
using System;
using System.Collections.Generic;

namespace Autenticador.DAL.Interfaces
{
    public interface INewDALUsuario
    {
        List<SYS_Usuario> SelecionarUsuariosDaUnidadeAdministrativa(Guid idEntidade, Guid idUnidade);
    }
}