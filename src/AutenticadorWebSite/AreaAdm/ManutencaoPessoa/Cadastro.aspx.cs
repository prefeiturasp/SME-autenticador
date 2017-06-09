using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;

public partial class AreaAdm_ManutencaoPessoa_Cadastro : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.UiAriaTabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.Tabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsCadastroPessoa.js"));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsUCCadastroContato.js"));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsUCCadastroEndereco.js"));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsCadastroCertidaoCivil.js"));
            sm.Services.Add(new ServiceReference("~/WSServicos.asmx"));
        }

        UCPessoas1.Paginacao = ApplicationWEB._Paginacao;
        UCPessoas1.ContainerName = "divBuscaPessoa";
        UCPessoas1.ReturnValues += UCPessoas1BuscaPessoa;

        if (!IsPostBack)
        {
            // Inicializa componentes

            // Validação para utilização de múltiplos endereços para a manutenção de pessoas
            bool permitirMultiplosEnderecos = SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.PERMITIR_MULTIPLOS_ENDERECOS_PESSOA);
            UCEnderecos1.Inicializar(false, !permitirMultiplosEnderecos, "Pessoa");

            bool valor = SYS_ParametroBO.ParametroValorBooleano(SYS_ParametroBO.eChave.PERMITIR_TIPO_CONTATOS_DUPLICADOS);
            UCGridContato1.InicializarContato(valor);

            if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
            {
                _VS_pes_id = PreviousPage.EditItem;
                _LoadFromEntity();
            }
            else
            {
                string pais_padrao = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.PAIS_PADRAO_BRASIL);

                if (!string.IsNullOrEmpty(pais_padrao))
                    UCCadastroPessoa1._ComboNacionalidade.SelectedValue = pais_padrao;

                UCGridContato1._CarregarContato();
                UCGridDocumento1._CarregarDocumento(Guid.Empty);
                UCGridCertidaoCivil1._CarregarCertidaoCivil();

                _btnSalvar.Visible = __SessionWEB.__UsuarioWEB.GrupoPermissao.grp_inserir;
            }

            Page.Form.DefaultFocus = UCCadastroPessoa1._txtNome.ClientID;
            Page.Form.DefaultButton = _btnSalvar.UniqueID;
        }

        #region INICIALIZACAO DOS DELEGATES

        UCCadastroPessoa1._Selecionar += UCCadastroPessoa1__Seleciona;

        #endregion INICIALIZACAO DOS DELEGATES
    }

    #region PROPRIEDADES

    private Guid _VS_pes_id
    {
        get
        {
            if (ViewState["_VS_pes_id"] != null)
                return new Guid(ViewState["_VS_pes_id"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_pes_id"] = value;
        }
    }

    private Guid _VS_pai_idAntigo
    {
        get
        {
            if (ViewState["_VS_pai_idAntigo"] != null)
                return new Guid(ViewState["_VS_pai_idAntigo"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_pai_idAntigo"] = value;
        }
    }

    private Guid _VS_cid_idAntigo
    {
        get
        {
            if (ViewState["_VS_cid_idAntigo"] != null)
                return new Guid(ViewState["_VS_cid_idAntigo"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_cid_idAntigo"] = value;
        }
    }

    private Guid _VS_pes_idPaiAntigo
    {
        get
        {
            if (ViewState["_VS_pes_idPaiAntigo"] != null)
                return new Guid(ViewState["_VS_pes_idPaiAntigo"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_pes_idPaiAntigo"] = value;
        }
    }

    private Guid _VS_pes_idMaeAntigo
    {
        get
        {
            if (ViewState["_VS_pes_idMaeAntigo"] != null)
                return new Guid(ViewState["_VS_pes_idMaeAntigo"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_pes_idMaeAntigo"] = value;
        }
    }

    private Guid _VS_tes_idAntigo
    {
        get
        {
            if (ViewState["_VS_tes_idAntigo"] != null)
                return new Guid(ViewState["_VS_tes_idAntigo"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_tes_idAntigo"] = value;
        }
    }

    private Guid _VS_tde_idAntigo
    {
        get
        {
            if (ViewState["_VS_tde_idAntigo"] != null)
                return new Guid(ViewState["_VS_tde_idAntigo"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_tde_idAntigo"] = value;
        }
    }

    private long _VS_arq_idAntigo
    {
        get
        {
            if (ViewState["_VS_arq_idAntigo"] != null)
                return Convert.ToInt64(ViewState["_VS_arq_idAntigo"]);

            return -1;
        }
        set
        {
            ViewState["_VS_arq_idAntigo"] = value;
        }
    }

    #endregion PROPRIEDADES

    #region DELEGATES

    private void UCPessoas1BuscaPessoa(IDictionary<string, object> parameters)
    {
        if (UCCadastroPessoa1._VS_tipoBuscaPessoa == 1)
        {
            UCCadastroPessoa1._VS_pes_idFiliacaoPai = new Guid(parameters["pes_id"].ToString());
            UCCadastroPessoa1._txtPai.Text = parameters["pes_nome"].ToString();
            UCCadastroPessoa1._updCadastroPessoas.Update();
        }
        else if (UCCadastroPessoa1._VS_tipoBuscaPessoa == 2)
        {
            UCCadastroPessoa1._VS_pes_idFiliacaoMae = new Guid(parameters["pes_id"].ToString());
            UCCadastroPessoa1._txtMae.Text = parameters["pes_nome"].ToString();
            UCCadastroPessoa1._updCadastroPessoas.Update();
        }
    }

    private void UCCadastroPessoa1__Seleciona()
    {
        UCPessoas1._Limpar();
        ScriptManager.RegisterClientScriptBlock(Page, GetType(), "BuscaPessoa", "$('#divBuscaPessoa').dialog('open');", true);
        _updBuscaPessoa.Update();
    }

    #endregion DELEGATES

    #region METODOS

    /// <summary>
    /// Carrega os dados da pessoa nos controles caso seja alteração.
    /// </summary>
    private void _LoadFromEntity()
    {
        try
        {
            //Carrega entidade de pessoa
            PES_Pessoa pes = new PES_Pessoa
            {
                pes_id = _VS_pes_id
            };
            PES_PessoaBO.GetEntity(pes);

            _VS_arq_idAntigo = pes.arq_idFoto;

            //_VS_pes_foto = pes.pes_foto;
            UCCadastroPessoa1._imgFoto.ImageUrl = "~/Imagem.ashx?id=" + pes.arq_idFoto;

            CFG_Arquivo entArquivo = new CFG_Arquivo
            {
                arq_id = pes.arq_idFoto
            };
            CFG_ArquivoBO.GetEntity(entArquivo);

            //Exibe imagem caso exista
            if (!entArquivo.IsNew && entArquivo.arq_data.Length > 1)
            {
                System.Drawing.Image img;
                using (MemoryStream ms = new MemoryStream(entArquivo.arq_data, 0, entArquivo.arq_data.Length))
                {
                    ms.Write(entArquivo.arq_data, 0, entArquivo.arq_data.Length);
                    img = System.Drawing.Image.FromStream(ms, true);
                }

                int larguraMaxima = 200;
                int alturaMaxima = 200;
                int alt;
                int lar;

                decimal proporcaoOriginal = (decimal)((img.Height * 100) / img.Width) / 100;

                if (proporcaoOriginal > 1)
                {
                    proporcaoOriginal = (decimal)((img.Width * 100) / img.Height) / 100;
                    alt = alturaMaxima;
                    lar = Convert.ToInt32(alturaMaxima * proporcaoOriginal);
                }
                else
                {
                    lar = larguraMaxima;
                    alt = Convert.ToInt32(larguraMaxima * proporcaoOriginal);
                }

                UCCadastroPessoa1._imgFoto.Height = alt;
                UCCadastroPessoa1._imgFoto.Width = lar;
                UCCadastroPessoa1._imgFoto.Visible = true;
                UCCadastroPessoa1._chbExcluirImagem.Visible = true;
                UCCadastroPessoa1._chbExcluirImagem.Checked = false;
            }
            else
            {
                UCCadastroPessoa1._imgFoto.Visible = false;
                UCCadastroPessoa1._chbExcluirImagem.Visible = false;
            }

            UCCadastroPessoa1._VS_pes_id = pes.pes_id;
            UCCadastroPessoa1._txtNome.Text = pes.pes_nome;
            UCCadastroPessoa1._txtNomeAbreviado.Text = (!string.IsNullOrEmpty(pes.pes_nome_abreviado) ? pes.pes_nome_abreviado : string.Empty);
            UCCadastroPessoa1._txtNomeSocial.Text = (!string.IsNullOrEmpty(pes.pes_nomeSocial) ? pes.pes_nomeSocial : string.Empty);

            //Exibe cidade naturalidade da pessoa
            if (pes.cid_idNaturalidade != Guid.Empty)
            {
                END_Cidade cid = new END_Cidade
                {
                    cid_id = pes.cid_idNaturalidade
                };
                END_CidadeBO.GetEntity(cid);

                UCCadastroPessoa1._VS_cid_id = pes.cid_idNaturalidade;
                UCCadastroPessoa1._txtNaturalidade.Text = cid.cid_nome;
            }

            //Exibe dados gerais da pessoa
            UCCadastroPessoa1._txtDataNasc.Text = (pes.pes_dataNascimento != new DateTime()) ? pes.pes_dataNascimento.ToString("dd/MM/yyyy") : "";
            UCCadastroPessoa1._ComboEstadoCivil.SelectedValue = (pes.pes_estadoCivil > 0 ? pes.pes_estadoCivil.ToString() : "-1");
            UCCadastroPessoa1._ComboSexo.SelectedValue = (pes.pes_sexo > 0) ? pes.pes_sexo.ToString() : "-1";

            UCCadastroPessoa1._ComboNacionalidade.SelectedValue = pes.pai_idNacionalidade.ToString();
            UCCadastroPessoa1._chkNaturalizado.Checked = pes.pes_naturalizado;
            UCCadastroPessoa1._ComboRacaCor.SelectedValue = (pes.pes_racaCor > 0 ? pes.pes_racaCor.ToString() : "-1");
            UCCadastroPessoa1._VS_pes_idFiliacaoPai = pes.pes_idFiliacaoPai;
            UCCadastroPessoa1._VS_pes_idFiliacaoMae = pes.pes_idFiliacaoMae;
            UCCadastroPessoa1._ComboEscolaridade.SelectedValue = pes.tes_id.ToString();

            //Carregar tipo de deficiência cadastrada para a pessoa
            DataTable dtPessoaDeficiencia = PES_PessoaDeficienciaBO.GetSelect(_VS_pes_id, false, 1, 1);
            if (dtPessoaDeficiencia.Rows.Count > 0)
                UCCadastroPessoa1._ComboTipoDeficiencia.SelectedValue = dtPessoaDeficiencia.Rows[0]["tde_id"].ToString();

            //Armazena os os id's antigos em ViewState
            _VS_pai_idAntigo = pes.pai_idNacionalidade;
            _VS_cid_idAntigo = pes.cid_idNaturalidade;
            _VS_pes_idPaiAntigo = pes.pes_idFiliacaoPai;
            _VS_pes_idMaeAntigo = pes.pes_idFiliacaoMae;
            _VS_tes_idAntigo = pes.tes_id;
            _VS_tde_idAntigo = dtPessoaDeficiencia.Rows.Count > 0 ? new Guid(dtPessoaDeficiencia.Rows[0]["tde_id"].ToString()) : Guid.Empty;

            //Exibe dados do pai da pessoa
            PES_Pessoa pesFiliacaoPai = new PES_Pessoa { pes_id = pes.pes_idFiliacaoPai };
            PES_PessoaBO.GetEntity(pesFiliacaoPai);
            UCCadastroPessoa1._txtPai.Text = pesFiliacaoPai.pes_nome;

            //Exibe dados da mae da pessoa
            PES_Pessoa pesFiliacaoMae = new PES_Pessoa { pes_id = pes.pes_idFiliacaoMae };
            PES_PessoaBO.GetEntity(pesFiliacaoMae);
            UCCadastroPessoa1._txtMae.Text = pesFiliacaoMae.pes_nome;

            //Carrega dados dos endereços da pessoa

            // [OLD] DataTable dtEndereco = PES_PessoaEnderecoBO.GetSelect(pes.pes_id, false, 1, 1);
            DataTable dtEndereco = PES_PessoaEnderecoBO.CarregaEnderecos_Pessoa(pes.pes_id);
            UCEnderecos1.CarregarEnderecosBanco(dtEndereco);

            //Carrega dados dos contatos da pessoa
            DataTable dtContato = PES_PessoaContatoBO.GetSelect(pes.pes_id, false, 1, 1);

            if (dtContato.Rows.Count == 0)
                dtContato = null;

            UCGridContato1._VS_contatos = dtContato;
            UCGridContato1._CarregarContato();

            //Carrega dados dos documentos da pessoa
            UCGridDocumento1._CarregarDocumento(pes.pes_id);

            //Carrega dados da certidões da pessoa
            DataTable dtCertidao = PES_CertidaoCivilBO.GetSelect(pes.pes_id, false, 1, 1);

            if (dtCertidao.Rows.Count == 0)
                dtCertidao = null;

            UCGridCertidaoCivil1._VS_certidoes = dtCertidao;
            UCGridCertidaoCivil1._CarregarCertidaoCivil();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar a pessoa.", UtilBO.TipoMensagem.Erro);
        }
    }

    /// <summary>
    /// Insere e altera uma pessoa
    /// </summary>
    private void _Salvar()
    {
        try
        {
            string msgErro;
            bool mensagemEmBranco = String.IsNullOrEmpty(_lblMessage.Text.Trim());
            if (mensagemEmBranco && !UCGridContato1.SalvaConteudoGrid(out msgErro))
            {
                UCGridContato1._MensagemErro.Visible = false;
                _lblMessage.Text = msgErro;
                txtSelectedTab.Value = "2";
                return;
            }

            if (mensagemEmBranco && !UCGridCertidaoCivil1.AtualizaViewState(out msgErro))
            {
                _lblMessage.Text = msgErro;
                txtSelectedTab.Value = "3";
                return;
            }

            if (!UCGridDocumento1.ValidaConteudoGrid(out msgErro))
            {
                UCGridDocumento1._MensagemErro.Visible = false;
                _lblMessage.Text = UtilBO.GetErroMessage(msgErro, UtilBO.TipoMensagem.Alerta);
                txtSelectedTab.Value = "3";
                return;
            }

            //Adiciona valores na entidade de pessoa
            PES_Pessoa entityPessoa = new PES_Pessoa
            {
                pes_id = UCCadastroPessoa1._VS_pes_id
                ,
                pes_nome = UCCadastroPessoa1._txtNome.Text
                ,
                pes_nome_abreviado = UCCadastroPessoa1._txtNomeAbreviado.Text
                ,
                pes_nomeSocial = UCCadastroPessoa1._txtNomeSocial.Text
                ,
                pai_idNacionalidade = UCCadastroPessoa1._ComboNacionalidade.SelectedValue == "-1" ? Guid.Empty : new Guid(UCCadastroPessoa1._ComboNacionalidade.SelectedValue)
                ,
                pes_naturalizado = UCCadastroPessoa1._chkNaturalizado.Checked
                ,
                cid_idNaturalidade = UCCadastroPessoa1._VS_cid_id
                ,
                pes_dataNascimento = (String.IsNullOrEmpty(UCCadastroPessoa1._txtDataNasc.Text.Trim()) ? new DateTime() : Convert.ToDateTime(UCCadastroPessoa1._txtDataNasc.Text.Trim()))
                ,
                pes_racaCor = UCCadastroPessoa1._ComboRacaCor.SelectedValue == "-1" ? Convert.ToByte(null) : Convert.ToByte(UCCadastroPessoa1._ComboRacaCor.SelectedValue)
                ,
                pes_sexo = UCCadastroPessoa1._ComboSexo.SelectedValue == "-1" ? Convert.ToByte(null) : Convert.ToByte(UCCadastroPessoa1._ComboSexo.SelectedValue)
                ,
                pes_idFiliacaoPai = UCCadastroPessoa1._VS_pes_idFiliacaoPai
                ,
                pes_idFiliacaoMae = UCCadastroPessoa1._VS_pes_idFiliacaoMae
                ,
                tes_id = new Guid(UCCadastroPessoa1._ComboEscolaridade.SelectedValue)
                ,
                pes_estadoCivil = UCCadastroPessoa1._ComboEstadoCivil.SelectedValue == "-1" ? Convert.ToByte(null) : Convert.ToByte(UCCadastroPessoa1._ComboEstadoCivil.SelectedValue)
                ,
                pes_situacao = 1
                ,
                IsNew = (UCCadastroPessoa1._VS_pes_id != Guid.Empty) ? false : true
            };

            PES_PessoaDeficiencia entityPessoaDeficiencia = new PES_PessoaDeficiencia
            {
                pes_id = _VS_pes_id
                ,
                tde_id = new Guid(UCCadastroPessoa1._ComboTipoDeficiencia.SelectedValue)
                ,
                IsNew = true
            };

            CFG_Arquivo entArquivo = null;

            //armazema a imagem na entidade de pessoa
            if (!string.IsNullOrEmpty(UCCadastroPessoa1._iptFoto.PostedFile.FileName))
            {
                string tam = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TAMANHO_MAX_FOTO_PESSOA);

                if (!string.IsNullOrEmpty(tam))
                {
                    if (UCCadastroPessoa1._iptFoto.PostedFile.ContentLength > Convert.ToInt32(tam) * 1000)
                    {
                        throw new ArgumentException("Foto é maior que o tamanho máximo permitido.");
                    }
                }

                if (UCCadastroPessoa1._iptFoto.PostedFile.FileName.Substring(UCCadastroPessoa1._iptFoto.PostedFile.FileName.Length - 3, 3).ToUpper() != "JPG")
                {
                    throw new ArgumentException("Foto tem que estar no formato \".jpg\".");
                }

                entArquivo = CFG_ArquivoBO.CriarEntidadeArquivo(UCCadastroPessoa1._iptFoto.PostedFile);

                if (_VS_arq_idAntigo > 0)
                {
                    // Se já existia foto e vai ser alterada, muda só o conteúdo.
                    entArquivo.arq_id = _VS_arq_idAntigo;
                    entArquivo.IsNew = false;
                }
            }

            if (_VS_arq_idAntigo > 0)
            {
                entityPessoa.arq_idFoto = _VS_arq_idAntigo;
            }

            //Chama método salvar da pessoa
            /* [OLD]  if (PES_PessoaBO.Save(entityPessoa
                                , entityPessoaDeficiencia
                                , UCEnderecos1._VS_enderecos
                                , UCGridContato1._VS_contatos
                                , UCGridDocumento1.RetornaDocumentoSave()
                                , UCGridCertidaoCivil1._VS_certidoes
                                , _VS_pai_idAntigo
                                , _VS_cid_idAntigo
                                , _VS_pes_idPaiAntigo
                                , _VS_pes_idMaeAntigo
                                , _VS_tes_idAntigo
                                , _VS_tde_idAntigo
                                , ApplicationWEB.TipoImagensPermitidas
                                , ApplicationWEB.TamanhoMaximoArquivo
                                , entArquivo
                                , UCCadastroPessoa1._chbExcluirImagem.Checked
                                )
                   )*/
            END_Endereco entityEndereco = new END_Endereco();

            string msg;
            DataTable dtEndereco;

            bool cadastraEndereco = UCEnderecos1.RetornaEnderecoCadastrado(out dtEndereco, out msg);

            if (PES_PessoaBO.Save(entityPessoa
                           , entityPessoaDeficiencia
                           , dtEndereco
                           , UCGridContato1._VS_contatos
                           , UCGridDocumento1.RetornaDocumentoSave()
                           , UCGridCertidaoCivil1._VS_certidoes
                           , _VS_pai_idAntigo
                           , _VS_cid_idAntigo
                           , _VS_pes_idPaiAntigo
                           , _VS_pes_idMaeAntigo
                           , _VS_tes_idAntigo
                           , _VS_tde_idAntigo
                           , ApplicationWEB.TipoImagensPermitidas
                           , ApplicationWEB.TamanhoMaximoArquivo
                           , entArquivo
                           , UCCadastroPessoa1._chbExcluirImagem.Checked
                           )
              )
            {
                if (_VS_pes_id == Guid.Empty)
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Insert, "pes_id: " + entityPessoa.pes_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage(String.Format("Pessoa incluída com sucesso."), UtilBO.TipoMensagem.Sucesso);
                }
                else
                {
                    ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "pes_id: " + entityPessoa.pes_id);
                    __SessionWEB.PostMessages = UtilBO.GetErroMessage(String.Format("Pessoa alterada com sucesso."), UtilBO.TipoMensagem.Sucesso);
                }

                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoPessoa/Busca.aspx", false);
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a pessoa.", UtilBO.TipoMensagem.Erro);
            }
        }
        catch (CoreLibrary.Validation.Exceptions.ValidationException ex)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (ArgumentException ex)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (DuplicateNameException ex)
        {
            _lblMessage.Text = UtilBO.GetErroMessage(ex.Message, UtilBO.TipoMensagem.Alerta);
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar salvar a pessoa.", UtilBO.TipoMensagem.Erro);
        }
    }

    #endregion METODOS

    #region EVENTOS

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoPessoa/Busca.aspx", false);
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
            _Salvar();
    }

    #endregion EVENTOS
}