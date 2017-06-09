using Autenticador.BLL;
using Autenticador.Entities;
using AutenticadorAPI.DTO.Entrada;
using CoreLibrary.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace AutenticadorAPI.Models
{
    public class Usuario
    {
        /// <summary>
        /// Método utilizado via Web API para criação de Usuários
        /// OBSERVACAO: Este metodo faz uma busca por nome, data de nascimento e CPF
        /// para tentar vincular uma pessoa já existente com estes dados ao usuario
        /// que esta sendo criado, sendo que apenas nome e data de nascimento são requeridos.
        /// </summary>
        /// <param name="data">Parametros de entrada: Id Entidade, Id Grupo,  ID Usuario, Nome, 
        /// CPF, Data de nascimento, E-mail, Senha</param>
        /// <param name="entityUsuarioAPI">Usuário da API usado para gravar log de ação</param>
        public static void Create(UsuarioEntradaDTO data, CFG_UsuarioAPI entityUsuarioAPI)
        {
            #region [ Validação de campos obrigatórios ]

            if (data.ent_id == Guid.Empty)
            {
                throw new ValidationException("Id da entidade é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(data.usu_login))
            {
                throw new ValidationException("Login do usuário é obrigatório.");
            }
            if (data.gru_id.Count() == 0)
            {
                throw new ValidationException("Ao menos um grupo deve ser informado.");
            }
            if (data.dataNascimento == new DateTime())
            {
                throw new ValidationException("Data de nascimento é obrigatória.");
            }
            if (data.sexo != null && data.sexo > 2)
            {
                throw new ValidationException("Para o sexo informe: 1 - masculino ou 2 - feminino");
            }

            #endregion

            SYS_Usuario entity = new SYS_Usuario
            {
                ent_id = data.ent_id
                ,
                usu_login = data.usu_login
            };
            SYS_UsuarioBO.GetSelectBy_ent_id_usu_login(entity);

            // Verifica se o id do usuário enviado existe na base de dados.
            if (entity.IsNew)
            {
                Guid? tdo_id = null;
                bool savePessoaReturn = false;
                PES_Pessoa entityPessoa = null;

                //Se não for informado nome e data de nascimento não cria a pessoa
                if (!string.IsNullOrWhiteSpace(data.nome) && data.dataNascimento != null)
                {
                    #region [Validações CPF]

                    //Se CPF existir, realiza validações
                    if (!string.IsNullOrWhiteSpace(data.CPF))
                    {
                        if (UtilBO._ValidaCPF(data.CPF))
                        {
                            //Recupera o tipo de documento CPF, utilizado para recuperar a pessoa
                            string tipoDocCPF = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TIPO_DOCUMENTACAO_CPF);
                            if (!string.IsNullOrEmpty(tipoDocCPF))
                            {
                                tdo_id = Guid.Parse(tipoDocCPF);
                            }
                        }
                        else
                            throw new ValidationException("CPF inválido.");
                    }

                    #endregion

                    //Recupera a pessoa
                    entityPessoa = PES_PessoaBO.SelecionaPorNomeNascimentoDocumento(data.nome, data.dataNascimento, tdo_id, data.CPF);

                    //Se pessoa não existir, faz o cadastro com as informações básicas
                    if (entityPessoa.pes_id == Guid.Empty)
                    {
                        #region [Cria Pessoa]

                        //Adiciona valores na entidade de pessoa

                        entityPessoa.pes_id = Guid.Empty;
                        entityPessoa.pes_nome = data.nome;
                        entityPessoa.pes_sexo = data.sexo;
                        entityPessoa.pes_nome_abreviado = string.Empty;
                        entityPessoa.pai_idNacionalidade = Guid.Empty;
                        entityPessoa.pes_naturalizado = false;
                        entityPessoa.cid_idNaturalidade = Guid.Empty;
                        entityPessoa.pes_dataNascimento = (String.IsNullOrEmpty(data.dataNascimento.ToString()) ? new DateTime() : Convert.ToDateTime(data.dataNascimento.ToString()));
                        entityPessoa.pes_racaCor = Convert.ToByte(null);
                        entityPessoa.pes_idFiliacaoPai = Guid.Empty;
                        entityPessoa.pes_idFiliacaoMae = Guid.Empty;
                        entityPessoa.tes_id = Guid.Empty;
                        entityPessoa.pes_estadoCivil = Convert.ToByte(null);
                        entityPessoa.pes_situacao = 1;

                        PES_PessoaDeficiencia entityPessoaDeficiencia = new PES_PessoaDeficiencia
                        {
                            pes_id = Guid.Empty,
                            tde_id = Guid.Empty,
                            IsNew = true
                        };

                        savePessoaReturn = PES_PessoaBO.Save(entityPessoa
                                                             , entityPessoaDeficiencia
                                                             , new DataTable() //dtEndereco
                                                             , new DataTable() //dtContato
                                                             , RetornaDocumento(data.CPF) //dtDocumento
                                                             , new DataTable() //dtCertidao
                                                             , Guid.Empty //pai_idAntigo
                                                             , Guid.Empty //cid_idAntigo
                                                             , Guid.Empty //pes_idPaiAntigo
                                                             , Guid.Empty //pes_idMaeAntigo
                                                             , Guid.Empty //tes_idAntigo
                                                             , Guid.Empty //tde_idAntigo
                                                             , null //arquivosPermitidos
                                                             , 0 //tamanhoMaximoKB
                                                             , null //entFoto
                                                             , false //ExcluirImagemAtual
                                                             );

                        #endregion
                    }
                }
                #region [ Cria usuário ]

                entity.ent_id = data.ent_id;
                entity.usu_id = Guid.Empty;
                entity.usu_login = data.usu_login;
                entity.usu_email = string.IsNullOrEmpty(data.email) ? string.Empty : data.email;
                entity.usu_senha = string.IsNullOrEmpty(data.senha) ? string.Empty : data.senha;

                //Se foi recuperado ou criado uma pessoa, vincula o pes_id
                if (entityPessoa != null)
                    entity.pes_id = entityPessoa.pes_id;

                entity.usu_criptografia = Convert.ToByte(eCriptografa.TripleDES);
                entity.usu_situacao = 1;
                entity.usu_dataAlteracao = DateTime.Now;
                entity.usu_dataCriacao = DateTime.Now;
                entity.usu_dominio = string.Empty;
                entity.usu_integracaoAD = (byte)SYS_UsuarioBO.eIntegracaoAD.NaoIntegrado;
                entity.IsNew = true;

                SortedDictionary<Guid, SYS_UsuarioBO.TmpGrupos> grupos = new SortedDictionary<Guid, SYS_UsuarioBO.TmpGrupos>();

                foreach (Guid gruId in data.gru_id)
                {
                    SYS_UsuarioBO.AddTmpGrupo(gruId, grupos, 1);
                }

                SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>> entidadeUA = new SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>>();

                SYS_UsuarioBO.Save(entity, grupos, entidadeUA, false, data.nome, string.Empty, string.Empty, string.Empty, null);

                #endregion
            }
            else
            {
                throw new ValidationException("Usuário já existe.");
            }

            #region [ Log de ação]

            LOG_UsuarioAPIBO.Save
            (
                new LOG_UsuarioAPI
                {
                    usu_id = entity.usu_id
                    ,
                    uap_id = entityUsuarioAPI.uap_id
                    ,
                    lua_dataHora = DateTime.Now
                    ,
                    lua_acao = (byte)LOG_UsuarioAPIBO.eAcao.CriacaoUsuario
                }
            );

            #endregion
        }

        /// <summary>
        /// Método utilizado via Web API para criação de Usuários
        /// OBSERVAÇÂO: Este metodo SEMPRE cria a pessoa.
        /// </summary>
        /// <param name="data">Parametros de entrada: Id Entidade, Id Grupo,  ID Usuario, Nome, 
        /// CPF, Data de nascimento, E-mail, Senha</param>
        /// <param name="entityUsuarioAPI">Usuário da API usado para gravar log de ação</param>
        public static void CreateV2(UsuarioEntradaDTO data, CFG_UsuarioAPI entityUsuarioAPI)
        {
            #region [ Validação de campos obrigatórios ]

            if (data.ent_id == Guid.Empty)
            {
                throw new ValidationException("Id da entidade é obrigatório.");
            }
            if (string.IsNullOrWhiteSpace(data.usu_login))
            {
                throw new ValidationException("Login do usuário é obrigatório.");
            }
            if (data.gru_id.Count() == 0)
            {
                throw new ValidationException("Ao menos um grupo deve ser informado.");
            }
            if (data.dataNascimento == new DateTime())
            {
                throw new ValidationException("Data de nascimento é obrigatória.");
            }
            if (data.sexo != null && data.sexo > 2)
            {
                throw new ValidationException("Para o sexo informe: 1 - masculino ou 2 - feminino");
            }

            #endregion

            SYS_Usuario entity = new SYS_Usuario
            {
                ent_id = data.ent_id
                ,
                usu_login = data.usu_login
            };
            SYS_UsuarioBO.GetSelectBy_ent_id_usu_login(entity);

            // Verifica se o id do usuário enviado existe na base de dados.
            if (entity.IsNew)
            {
                Guid? tdo_id = null;
                bool savePessoaReturn = false;
                PES_Pessoa entityPessoa = null;

                //Se não for informado nome e data de nascimento não cria a pessoa
                if (!string.IsNullOrWhiteSpace(data.nome) && data.dataNascimento != null)
                {
                    #region [Validações CPF]

                    //Se CPF existir, realiza validações
                    if (!string.IsNullOrWhiteSpace(data.CPF))
                    {
                        if (UtilBO._ValidaCPF(data.CPF))
                        {
                            //Recupera o tipo de documento CPF, utilizado para recuperar a pessoa
                            string tipoDocCPF = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TIPO_DOCUMENTACAO_CPF);
                            if (!string.IsNullOrEmpty(tipoDocCPF))
                            {
                                tdo_id = Guid.Parse(tipoDocCPF);
                            }
                        }
                        else
                            throw new ValidationException("CPF inválido.");
                    }

                    #endregion

                    #region [Cria Pessoa]

                    //Adiciona valores na entidade de pessoa

                    entityPessoa = new PES_Pessoa();
                    entityPessoa.pes_id = Guid.Empty;
                    entityPessoa.pes_nome = data.nome;
                    entityPessoa.pes_sexo = data.sexo;
                    entityPessoa.pes_nome_abreviado = string.Empty;
                    entityPessoa.pai_idNacionalidade = Guid.Empty;
                    entityPessoa.pes_naturalizado = false;
                    entityPessoa.cid_idNaturalidade = Guid.Empty;
                    entityPessoa.pes_dataNascimento = (String.IsNullOrEmpty(data.dataNascimento.ToString()) ? new DateTime() : Convert.ToDateTime(data.dataNascimento.ToString()));
                    entityPessoa.pes_racaCor = Convert.ToByte(null);
                    entityPessoa.pes_idFiliacaoPai = Guid.Empty;
                    entityPessoa.pes_idFiliacaoMae = Guid.Empty;
                    entityPessoa.tes_id = Guid.Empty;
                    entityPessoa.pes_estadoCivil = Convert.ToByte(null);
                    entityPessoa.pes_situacao = 1;

                    PES_PessoaDeficiencia entityPessoaDeficiencia = new PES_PessoaDeficiencia
                    {
                        pes_id = Guid.Empty,
                        tde_id = Guid.Empty,
                        IsNew = true
                    };

                    savePessoaReturn = PES_PessoaBO.Save(entityPessoa
                                                         , entityPessoaDeficiencia
                                                         , new DataTable() //dtEndereco
                                                         , new DataTable() //dtContato
                                                         , RetornaDocumento(data.CPF) //dtDocumento
                                                         , new DataTable() //dtCertidao
                                                         , Guid.Empty //pai_idAntigo
                                                         , Guid.Empty //cid_idAntigo
                                                         , Guid.Empty //pes_idPaiAntigo
                                                         , Guid.Empty //pes_idMaeAntigo
                                                         , Guid.Empty //tes_idAntigo
                                                         , Guid.Empty //tde_idAntigo
                                                         , null //arquivosPermitidos
                                                         , 0 //tamanhoMaximoKB
                                                         , null //entFoto
                                                         , false //ExcluirImagemAtual
                                                         );

                    #endregion
                }
                #region [ Cria usuário ]

                entity.ent_id = data.ent_id;
                entity.usu_id = Guid.Empty;
                entity.usu_login = data.usu_login;
                entity.usu_email = string.IsNullOrEmpty(data.email) ? string.Empty : data.email;
                entity.usu_senha = string.IsNullOrEmpty(data.senha) ? string.Empty : data.senha;

                //Se foi recuperado ou criado uma pessoa, vincula o pes_id
                if (entityPessoa != null)
                    entity.pes_id = entityPessoa.pes_id;

                entity.usu_criptografia = Convert.ToByte(eCriptografa.TripleDES);
                entity.usu_situacao = 1;
                entity.usu_dataAlteracao = DateTime.Now;
                entity.usu_dataCriacao = DateTime.Now;
                entity.usu_dominio = string.Empty;
                entity.usu_integracaoAD = (byte)SYS_UsuarioBO.eIntegracaoAD.NaoIntegrado;
                entity.IsNew = true;

                SortedDictionary<Guid, SYS_UsuarioBO.TmpGrupos> grupos = new SortedDictionary<Guid, SYS_UsuarioBO.TmpGrupos>();

                foreach (Guid gruId in data.gru_id)
                {
                    SYS_UsuarioBO.AddTmpGrupo(gruId, grupos, 1);
                }

                SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>> entidadeUA = new SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>>();

                SYS_UsuarioBO.Save(entity, grupos, entidadeUA, false, data.nome, string.Empty, string.Empty, string.Empty, null);

                #endregion
            }
            else
            {
                throw new ValidationException("Usuário já existe.");
            }

            #region [ Log de ação]

            LOG_UsuarioAPIBO.Save
            (
                new LOG_UsuarioAPI
                {
                    usu_id = entity.usu_id
                    ,
                    uap_id = entityUsuarioAPI.uap_id
                    ,
                    lua_dataHora = DateTime.Now
                    ,
                    lua_acao = (byte)LOG_UsuarioAPIBO.eAcao.CriacaoUsuario
                }
            );

            #endregion
        }

        /// <summary>
        /// Método utilizado via Web API para alteração de Usuários
        /// </summary>
        /// <param name="data">Parametros de entrada: Id Entidade, Id Grupo, ID Usuario, Nome, 
        /// CPF, Data de nascimento, E-mail, Senha</param>
        /// <param name="entityUsuarioAPI">Usuário da API usado para gravar log de ação</param>
        public static void Update(UsuarioEntradaDTO data, CFG_UsuarioAPI entityUsuarioAPI)
        {
            #region [ Validação de campos obrigatórios ]

            if (data.ent_id == Guid.Empty || string.IsNullOrWhiteSpace(data.usu_login))
            {
                throw new ValidationException("Id da entidade e login do usuário são obrigatórios.");
            }

            #endregion

            SYS_Usuario entity = new SYS_Usuario
            {
                ent_id = data.ent_id
                ,
                usu_login = data.usu_login
            };
            SYS_UsuarioBO.GetSelectBy_ent_id_usu_login(entity);

            PES_Pessoa entityPessoa = null;

            if (!entity.IsNew)
            {
                //Validação de usuário padrão do sistema
                if (entity.usu_situacao == (byte)SYS_UsuarioBO.eSituacao.Padrao_Sistema)
                    throw new ValidationException("Não é possível alterar dados do usuário padrão do sistema.");

                //Se o usuário recuperado não possuir pessoa, pula os passos de update de Pessoa
                if (entity.pes_id != Guid.Empty)
                {
                    #region [Pessoa]

                    entityPessoa = new PES_Pessoa { pes_id = entity.pes_id };
                    PES_PessoaBO.GetEntity(entityPessoa);

                    if (!string.IsNullOrWhiteSpace(data.nome) && entityPessoa.pes_nome.ToLower() != data.nome.ToLower())
                        entityPessoa.pes_nome = data.nome;

                    if (data.dataNascimento != new DateTime() && entityPessoa.pes_dataNascimento != data.dataNascimento)
                        entityPessoa.pes_dataNascimento = data.dataNascimento;

                    if (data.sexo == 1 || data.sexo == 2)
                        entityPessoa.pes_sexo = data.sexo;

                    entityPessoa.pes_dataAlteracao = DateTime.Now;

                    #region [Validações CPF]

                    Guid? tdo_id = null;
                    bool criarCPF = false;

                    //Recupera os documentos da pessoa
                    DataTable documentosPessoa = PES_PessoaDocumentoBO.GetSelect(entityPessoa.pes_id, false, 1, 1);

                    //Se CPF existir, realiza validações
                    if (!string.IsNullOrWhiteSpace(data.CPF))
                    {
                        if (UtilBO._ValidaCPF(data.CPF))
                        {
                            //Recupera o tipo de documento CPF
                            string tipoDocCPF = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TIPO_DOCUMENTACAO_CPF);
                            if (!string.IsNullOrEmpty(tipoDocCPF))
                            {
                                tdo_id = Guid.Parse(tipoDocCPF);
                                criarCPF = true;
                            }
                        }
                        else
                            throw new ValidationException("CPF inválido.");

                        if (documentosPessoa.Rows.Count > 0)
                        {
                            //Recupera o indice da linha que contém o documento do tipo CPF
                            var indiceRowCPF = documentosPessoa.AsEnumerable()
                                               .Select((row, index) => new { row, index })
                                               .Where(item => item.row.Field<Guid>("tdo_id") == tdo_id)
                                               .Select(item => item.index).ToArray();

                            //Se a pessoa possui um documento do tipo CPF, verifica se precisa alterar
                            if (indiceRowCPF.Count() > 0)
                            {
                                if (documentosPessoa.Rows[indiceRowCPF[0]]["numero"].ToString() != data.CPF)
                                    documentosPessoa.Rows[indiceRowCPF[0]]["numero"] = data.CPF;
                            }
                            else
                            {
                                //Pessoa ainda não possue CPF, nesse caso cria usando o datatable recuperado

                                DataRow rowDoc = documentosPessoa.NewRow();

                                rowDoc["tdo_id"] = tdo_id;
                                rowDoc["unf_idEmissao"] = Guid.Empty.ToString();
                                rowDoc["unf_idAntigo"] = Guid.Empty.ToString();
                                rowDoc["numero"] = data.CPF;
                                rowDoc["dataemissao"] = string.Empty;
                                rowDoc["orgaoemissao"] = string.Empty;
                                rowDoc["info"] = string.Empty;

                                documentosPessoa.Rows.Add(rowDoc);
                            }
                            criarCPF = false;
                        }

                        if (criarCPF)
                        {
                            if (tdo_id != null)
                            {
                                //Cria o datatable na estrutura necessária com o CPF enviado
                                documentosPessoa = RetornaDocumento(data.CPF);
                            }
                        }
                    }

                    #endregion

                    PES_PessoaDeficiencia entityPessoaDeficiencia = new PES_PessoaDeficiencia
                    {
                        pes_id = Guid.Empty,
                        tde_id = Guid.Empty,
                        IsNew = true
                    };

                    PES_PessoaBO.Save(entityPessoa
                                    , entityPessoaDeficiencia
                                    , new DataTable() //dtEndereco
                                    , new DataTable() //dtContato
                                    , documentosPessoa //dtDocumento
                                    , new DataTable() //dtCertidao
                                    , Guid.Empty //pai_idAntigo
                                    , Guid.Empty //cid_idAntigo
                                    , Guid.Empty //pes_idPaiAntigo
                                    , Guid.Empty //pes_idMaeAntigo
                                    , Guid.Empty //tes_idAntigo
                                    , Guid.Empty //tde_idAntigo
                                    , null //arquivosPermitidos
                                    , 0 //tamanhoMaximoKB
                                    , null //entFoto
                                    , false //ExcluirImagemAtual
                                    );

                    #endregion
                }

                #region [Usuário]

                //entity.usu_login = data.usu_login;

                if (!string.IsNullOrWhiteSpace(data.email) && entity.usu_email != data.email)
                    entity.usu_email = data.email;

                //Se não vier senha, seta a senha da entidade como vazia para o método do sistema
                //não encriptar novamente o que já estava encriptado
                if (string.IsNullOrWhiteSpace(data.senha))
                    entity.usu_senha = string.Empty;
                else
                    entity.usu_senha = data.senha;

                if (entityPessoa != null)
                    entity.pes_id = entityPessoa.pes_id;

                entity.usu_criptografia = Convert.ToByte(eCriptografa.TripleDES);
                entity.usu_dataAlteracao = DateTime.Now;

                SortedDictionary<Guid, SYS_UsuarioBO.TmpGrupos> grupos = new SortedDictionary<Guid, SYS_UsuarioBO.TmpGrupos>();
                SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>> entidadeUA = new SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>>();

                //Se vier grupos cria a lista com base nesses grupos
                if (data.gru_id.Count() > 0)
                {
                    foreach (Guid gruId in data.gru_id)
                    {
                        SYS_UsuarioBO.AddTmpGrupo(gruId, grupos, 1);
                    }
                }
                else
                {
                    //Senão, recupera os grupos do usuário para enviar ao método salvar
                    SYS_UsuarioBO.GetGruposUsuario(entity.usu_id, grupos, entidadeUA);
                }

                SYS_UsuarioBO.Save(entity, grupos, entidadeUA, false, data.nome, string.Empty, string.Empty, string.Empty, null);

                #endregion
            }
            else
            {
                throw new ValidationException("Usuário não existe.");
            }

            #region [ Log de ação]

            LOG_UsuarioAPIBO.Save
            (
                new LOG_UsuarioAPI
                {
                    usu_id = entity.usu_id
                    ,
                    uap_id = entityUsuarioAPI.uap_id
                    ,
                    lua_dataHora = DateTime.Now
                    ,
                    lua_acao = (byte)LOG_UsuarioAPIBO.eAcao.AlteracaoUsuario
                }
            );

            #endregion
        }

        /// <summary>
        /// Método utilizado via Web API para alteração de login de um usuário
        /// </summary>
        /// <param name="data">Parametros de entrada: Id Entidade, login antigo, login novo</param>
        /// <param name="entityUsuarioAPI">Usuário da API usado para gravar log de ação</param>
        public static void UpdateLogin(AlterarLoginEntradaDTO data, CFG_UsuarioAPI entityUsuarioAPI)
        {
            if (data.ent_id == Guid.Empty || string.IsNullOrWhiteSpace(data.usu_login_antigo) || string.IsNullOrWhiteSpace(data.usu_login_novo))
            {
                throw new ValidationException("Todos os campos são obrigatórios.");
            }

            SYS_Usuario entity = new SYS_Usuario
            {
                ent_id = data.ent_id,
                usu_login = data.usu_login_antigo
            };

            SYS_UsuarioBO.GetSelectBy_ent_id_usu_login(entity);

            if (!entity.IsNew)
            {
                //Validação de usuário padrão do sistema
                if (entity.usu_situacao == (byte)SYS_UsuarioBO.eSituacao.Padrao_Sistema)
                    throw new ValidationException("Não é possível alterar dados do usuário padrão do sistema.");

                entity.usu_login = data.usu_login_novo;
            }
            else
            {
                throw new ValidationException("Usuário não existe.");
            }

            SYS_UsuarioBO.Save(entity);

            #region [ Log de ação]

            LOG_UsuarioAPIBO.Save
            (
                new LOG_UsuarioAPI
                {
                    usu_id = entity.usu_id
                    ,
                    uap_id = entityUsuarioAPI.uap_id
                    ,
                    lua_dataHora = DateTime.Now
                    ,
                    lua_acao = (byte)LOG_UsuarioAPIBO.eAcao.AlteracaoLogin
                }
            );

            #endregion
        }

        /// <summary>
        /// Método utilizado via Web API para inclusão de novos grupos de usuário
        /// </summary>
        /// <param name="entityUsuarioAPI">Usuário da API usado para gravar log de ação</param>
        public static void AssociateUserGroup(Guid usu_id, AssociarUsuarioGrupoEntradaDTO data, CFG_UsuarioAPI entityUsuarioAPI)
        {
            #region [ Validação de campos obrigatórios ]

            if (data == null || data.usergroup == null || data.usergroup.Count(p => p.gru_id != Guid.Empty) == 0)
            {
                throw new ValidationException("Obrigatório no mínimo um Id de grupo do usuário.");
            }

            #endregion

            SYS_Usuario entity = SYS_UsuarioBO.GetEntity(new SYS_Usuario { usu_id = usu_id });
            entity.usu_senha = string.Empty;
            entity.usu_dataAlteracao = DateTime.Now;

            if (!entity.IsNew)
            {
                // Validação de usuário padrão do sistema
                if (entity.usu_situacao == (byte)SYS_UsuarioBO.eSituacao.Padrao_Sistema)
                    throw new ValidationException("Não é possível alterar dados do usuário padrão do sistema.");

                SortedDictionary<Guid, SYS_UsuarioBO.TmpGrupos> grupos = new SortedDictionary<Guid, SYS_UsuarioBO.TmpGrupos>();
                SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>> grupoUas = new SortedDictionary<Guid, List<SYS_UsuarioBO.TmpEntidadeUA>>();
                SYS_UsuarioBO.GetGruposUsuario(entity.usu_id, grupos, grupoUas);

                foreach (UsuarioGrupoDTO grupo in data.usergroup)
                {
                    // Adiciona o grupo
                    if (grupo.gru_id != Guid.Empty)
                    {
                        SYS_UsuarioBO.AddTmpGrupo(grupo.gru_id, grupos, 1);

                        // Adiciona a unidade administrativa ao grupo, caso necessário
                        if (grupo.uad_id != Guid.Empty)
                        {
                            List<SYS_UsuarioBO.TmpEntidadeUA> ltEntidadeUA = grupoUas.ContainsKey(grupo.gru_id) ?
                                grupoUas[grupo.gru_id] : new List<SYS_UsuarioBO.TmpEntidadeUA>();

                            SYS_UsuarioBO.AddTmpEntidadeUA(grupo.gru_id, entity.ent_id, grupo.uad_id, ltEntidadeUA);
                        }
                    }
                }

                SYS_UsuarioBO.Save(entity, grupos, grupoUas, false, string.Empty, string.Empty, string.Empty, string.Empty, null);
            }
            else
            {
                throw new ValidationException("Usuário não existe.");
            }

            #region [ Log de ação]

            LOG_UsuarioAPIBO.Save
            (
                new LOG_UsuarioAPI
                {
                    usu_id = entity.usu_id
                    ,
                    uap_id = entityUsuarioAPI.uap_id
                    ,
                    lua_dataHora = DateTime.Now
                    ,
                    lua_acao = (byte)LOG_UsuarioAPIBO.eAcao.AssociacaoUsuarioGrupo
                }
            );

            #endregion
        }

        /// <summary>
        /// Método utilizado via Web API para deleção de um usuário
        /// </summary>
        /// <param name="data">Parametros de entrada: Id Entidade, login, senha</param>
        /// <param name="entityUsuarioAPI">Usuário da API usado para gravar log de ação</param>
        public static HttpResponseMessage Delete(DeletarUsuarioDTO data, CFG_UsuarioAPI entityUsuarioAPI, HttpRequestMessage request)
        {
            try
            {
                //Verifica se todos os dados vieram
                if (data.ent_id == Guid.Empty || string.IsNullOrWhiteSpace(data.usu_login) || string.IsNullOrWhiteSpace(data.senha))
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Todos os campos são obrigatórios.");

                SYS_Usuario entity = new SYS_Usuario
                {
                    ent_id = data.ent_id,
                    usu_login = data.usu_login
                };

                SYS_UsuarioBO.GetSelectBy_ent_id_usu_login(entity);

                //Validação de usuário padrão do sistema
                if (entity.usu_situacao == (byte)SYS_UsuarioBO.eSituacao.Padrao_Sistema)
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Não é possível alterar um usuário padrão do sistema.");

                // Verifica se o id do usuário enviado existe na base de dados.
                if (entity.IsNew)
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, "Usuário não encontrado.");

                var senhaDescriptografada = new SymmetricAlgorithm(SymmetricAlgorithm.Tipo.TripleDES).Decrypt(entity.usu_senha);

                if (data.senha != senhaDescriptografada)
                    return request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Não foi possível excluir usuário. Acesso negado.");

                SYS_UsuarioBO.Delete(entity, null);

                #region [ Log de ação]

                LOG_UsuarioAPIBO.Save
                (
                    new LOG_UsuarioAPI
                    {
                        usu_id = entity.usu_id
                        ,
                        uap_id = entityUsuarioAPI.uap_id
                        ,
                        lua_dataHora = DateTime.Now
                        ,
                        lua_acao = (byte)LOG_UsuarioAPIBO.eAcao.DelecaoUsuario
                    }
                );

                #endregion

                return request.CreateResponse(HttpStatusCode.OK, "Usuário excluído com sucesso.");
            }
            catch (Exception ex)
            {
                Util.GravarErro(ex);
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Retorna dados do CPF.
        /// </summary>
        /// <returns> Data table com dados dos documentos.</returns>
        private static DataTable RetornaDocumento(string cpf)
        {
            try
            {
                DataTable dtDocumento = CriaDataTableDocumento();

                if (!string.IsNullOrEmpty(cpf))
                {

                    if (!UtilBO._ValidaCPF(cpf))
                        throw new ArgumentException("Número inválido para CPF");

                    string tdo_id = Guid.Empty.ToString();
                    string tipoDocCPF = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TIPO_DOCUMENTACAO_CPF);
                    if (!string.IsNullOrEmpty(tipoDocCPF))
                    {
                        tdo_id = tipoDocCPF;
                    }

                    DataRow rowDoc = dtDocumento.NewRow();

                    rowDoc["tdo_id"] = new Guid(tdo_id);
                    rowDoc["unf_idEmissao"] = Guid.Empty.ToString();
                    rowDoc["unf_idAntigo"] = Guid.Empty.ToString();
                    rowDoc["numero"] = cpf;
                    rowDoc["dataemissao"] = string.Empty;
                    rowDoc["orgaoemissao"] = string.Empty;
                    rowDoc["info"] = string.Empty;

                    dtDocumento.Rows.Add(rowDoc);
                }

                return dtDocumento;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Cria o dtDocumento com suas colunas.
        /// </summary>
        /// <returns>DataTable documento configurado.</returns>
        private static DataTable CriaDataTableDocumento()
        {
            DataTable dtDocumento = new DataTable();

            dtDocumento.Columns.Add("tdo_id");
            dtDocumento.Columns.Add("unf_idEmissao");
            dtDocumento.Columns.Add("unf_idAntigo");
            dtDocumento.Columns.Add("numero");
            dtDocumento.Columns.Add("dataemissao");
            dtDocumento.Columns.Add("orgaoemissao");
            dtDocumento.Columns.Add("info");

            return dtDocumento;
        }
    }
}