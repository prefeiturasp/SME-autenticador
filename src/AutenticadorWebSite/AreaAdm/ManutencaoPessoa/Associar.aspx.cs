using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;

public partial class AreaAdm_ManutencaoPessoa_Associar : MotherPageLogado
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
            sm.Services.Add(new ServiceReference("~/WSServicos.asmx"));
        }

        UCPessoas1.Paginacao = ApplicationWEB._Paginacao;
        UCPessoas1.ContainerName = "divBuscaPessoa";
        UCPessoas1.ReturnValues += UCPessoas1BuscaPessoa;

        if (!IsPostBack)
        {
            if ((PreviousPage != null) && (PreviousPage.IsCrossPagePostBack))
            {
                LoadFromEntity(PreviousPage.EditItem);
            }

            Page.Form.Enctype = "multipart/form-data";

            if (Session["ManutencaoPessoa_dtAssociarPessoa"] != null)
            {
                try
                {
                    _VS_AssociarPessoas = (DataTable)(Session["ManutencaoPessoa_dtAssociarPessoa"]);
                    Session.Remove("ManutencaoPessoa_dtAssociarPessoa");

                    Guid tdo_id;
                    SYS_TipoDocumentacao tdo = new SYS_TipoDocumentacao();

                    string tipoDocCPF = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TIPO_DOCUMENTACAO_CPF);
                    if (!string.IsNullOrEmpty(tipoDocCPF))
                    {
                        tdo_id = new Guid(tipoDocCPF);
                        tdo.tdo_id = tdo_id;
                        SYS_TipoDocumentacaoBO.GetEntity(tdo);
                        _grvAssociarPessoas.Columns[2].HeaderText = tdo.tdo_sigla;
                    }
                    else
                    {
                        _grvAssociarPessoas.Columns[2].HeaderText = string.Empty;
                        _grvAssociarPessoas.Columns[2].HeaderStyle.CssClass = "hide";
                        _grvAssociarPessoas.Columns[2].ItemStyle.CssClass = "hide";
                    }

                    string tipoDocRG = SYS_ParametroBO.ParametroValor(SYS_ParametroBO.eChave.TIPO_DOCUMENTACAO_RG);
                    if (!string.IsNullOrEmpty(tipoDocRG))
                    {
                        tdo_id = new Guid(tipoDocRG);
                        tdo.tdo_id = tdo_id;
                        SYS_TipoDocumentacaoBO.GetEntity(tdo);
                        _grvAssociarPessoas.Columns[3].HeaderText = tdo.tdo_sigla;
                    }
                    else
                    {
                        _grvAssociarPessoas.Columns[3].HeaderText = string.Empty;
                        _grvAssociarPessoas.Columns[3].HeaderStyle.CssClass = "hide";
                        _grvAssociarPessoas.Columns[3].ItemStyle.CssClass = "hide";
                    }

                    _CarregarGridPessoa();
                }
                catch (Exception ex)
                {
                    ApplicationWEB._GravaErro(ex);
                    _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Erro);
                }
            }

            _btnSalvar.Visible = false;
            Page.Form.DefaultFocus = UCCadastroPessoa1._txtNome.ClientID;
            Page.Form.DefaultButton = _btnSalvar.UniqueID;
        }

        #region INICIALIZACAO DOS DELEGATES

        UCCadastroPessoa1._Selecionar += UCCadastroPessoa1__Seleciona;

        #endregion INICIALIZACAO DOS DELEGATES
    }

    #region PROPRIEDADES

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

    private byte[] _VS_pes_foto
    {
        get
        {
            if (ViewState["_VS_pes_foto"] != null)
                return (byte[])ViewState["_VS_pes_foto"];
            return null;
        }
        set
        {
            ViewState["_VS_pes_foto"] = value;
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

    public DataTable _VS_AssociarPessoas
    {
        get
        {
            if (ViewState["_VS_AssociarPessoas"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("pes_id");
                dt.Columns.Add("pes_nome");
                dt.Columns.Add("pes_dataNascimento");
                dt.Columns.Add("tipo_documentacao_cpf");
                dt.Columns.Add("tipo_documentacao_rg");
                ViewState["_VS_AssociarPessoas"] = dt;
            }
            return (DataTable)ViewState["_VS_AssociarPessoas"];
        }
        set
        {
            ViewState["_VS_AssociarPessoas"] = value;
        }
    }

    private int _VS_linha_marcada
    {
        get
        {
            if (ViewState["_VS_linha_marcada"] != null)
                return Convert.ToInt32(ViewState["_VS_linha_marcada"]);
            return -1;
        }
        set
        {
            ViewState["_VS_linha_marcada"] = value;
        }
    }

    protected Guid _VS_end_idAntigo
    {
        get
        {
            if (ViewState["_VS_end_idAntigo"] != null)
                return new Guid(ViewState["_VS_end_idAntigo"].ToString());
            return Guid.Empty;
        }
        set
        {
            ViewState["_VS_end_idAntigo"] = value;
        }
    }

    /// <summary>
    /// Indica se é uma alteração ou inclusão de endereço
    /// </summary>
    public bool _VS_IsNew_end_id
    {
        get
        {
            return Convert.ToBoolean(ViewState["_VS_IsNew_end_id"]);
        }
        set
        {
            ViewState["_VS_IsNew_end_id"] = value;
        }
    }

    #endregion PROPRIEDADES

    #region DELEGATES

    #region BUSCAS

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

    #endregion BUSCAS

    #endregion DELEGATES

    #region METODOS

    private void _CarregarGridPessoa()
    {
        _grvAssociarPessoas.DataSource = _VS_AssociarPessoas;
        _grvAssociarPessoas.DataBind();

        _updGridPessoas.Update();
    }

    private void _MarcaLinhaPadrao(int novaLinha)
    {
        if (_VS_linha_marcada >= 0)
        {
            _grvAssociarPessoas.Rows[_VS_linha_marcada].Style.Remove("background");
            _grvAssociarPessoas.Rows[novaLinha].Style.Add("background", "#F8F7CB");
            _VS_linha_marcada = novaLinha;
        }
        else
        {
            _grvAssociarPessoas.Rows[novaLinha].Style.Add("background", "#F8F7CB");
            _VS_linha_marcada = novaLinha;
        }
    }

    private void _CarregarPessoa()
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

            //Exibe dados gerias da pessoa
            UCCadastroPessoa1._txtDataNasc.Text = (pes.pes_dataNascimento != new DateTime()) ? pes.pes_dataNascimento.ToString("dd/MM/yyyy") : string.Empty;
            UCCadastroPessoa1._ComboEstadoCivil.SelectedValue = (pes.pes_estadoCivil > 0 ? pes.pes_estadoCivil.ToString() : "-1");
            UCCadastroPessoa1._ComboSexo.SelectedValue = (pes.pes_sexo > 0) ? pes.pes_sexo.ToString() : "-1";

            UCCadastroPessoa1._ComboNacionalidade.SelectedValue = (pes.pai_idNacionalidade != Guid.Empty ? pes.pai_idNacionalidade.ToString() : Guid.Empty.ToString());
            UCCadastroPessoa1._chkNaturalizado.Checked = pes.pes_naturalizado;
            UCCadastroPessoa1._ComboRacaCor.SelectedValue = (pes.pes_racaCor > 0 ? pes.pes_racaCor.ToString() : "-1");
            UCCadastroPessoa1._VS_pes_idFiliacaoPai = pes.pes_idFiliacaoPai;
            UCCadastroPessoa1._VS_pes_idFiliacaoMae = pes.pes_idFiliacaoMae;
            UCCadastroPessoa1._ComboEscolaridade.SelectedValue = (pes.tes_id != Guid.Empty ? pes.tes_id.ToString() : Guid.Empty.ToString());

            //Carregar tipo de deficiência cadastrada para a pessoa
            DataTable dtPessoaDeficiencia = PES_PessoaDeficienciaBO.GetSelect(_VS_pes_id, false, 1, 1);
            UCCadastroPessoa1._ComboTipoDeficiencia.SelectedValue = dtPessoaDeficiencia.Rows.Count > 0 ? dtPessoaDeficiencia.Rows[0]["tde_id"].ToString() : Guid.Empty.ToString();

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
            DataTable dtEndereco = PES_PessoaEnderecoBO.GetSelect(pes.pes_id, false, 1, 1);

            if (dtEndereco.Rows.Count == 0)
                dtEndereco = null;

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

            UCCadastroPessoa1._updCadastroPessoas.Update();
            UCGridContato1._updGridContatos.Update();
            UCGridDocumento1._updGridDocumentos.Update();
            UCGridCertidaoCivil1._updGridCertidaoCivil.Update();

            _btnSalvar.Visible = true;
            _updBotoes.Update();
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
    private void _Associar()
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
                pai_idNacionalidade = new Guid(UCCadastroPessoa1._ComboNacionalidade.SelectedValue)
                ,
                pes_naturalizado = UCCadastroPessoa1._chkNaturalizado.Checked
                ,
                cid_idNaturalidade = UCCadastroPessoa1._VS_cid_id
                ,
                pes_dataNascimento = (String.IsNullOrEmpty(UCCadastroPessoa1._txtDataNasc.Text.Trim())? new DateTime() : Convert.ToDateTime(UCCadastroPessoa1._txtDataNasc.Text.Trim()))
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

                    if (UCCadastroPessoa1._iptFoto.PostedFile.FileName.Substring(UCCadastroPessoa1._iptFoto.PostedFile.FileName.Length - 3, 3).ToUpper() != "JPG")
                    {
                        throw new ArgumentException("Foto tem que estar no formato \".jpg\".");
                    }
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

            XmlDocument xDoc = new XmlDocument();
            XmlNode xElem = xDoc.CreateNode(XmlNodeType.Element, "Coluna", string.Empty);
            XmlNode xNodeCoor = xDoc.CreateNode(XmlNodeType.Element, "ColunaValorAntigo", string.Empty);
            XmlNode xNode;

            for (int i = 0; i < _VS_AssociarPessoas.Rows.Count; i++)
            {
                if ((_VS_AssociarPessoas.Rows[i]["pes_id"].ToString()) != _VS_pes_id.ToString())
                {
                    xNodeCoor = xDoc.CreateNode(XmlNodeType.Element, "ColunaValorAntigo", "");
                    xNode = xDoc.CreateNode(XmlNodeType.Element, "valor", "");
                    xNode.InnerText = _VS_AssociarPessoas.Rows[i]["pes_id"].ToString();
                    xNodeCoor.AppendChild(xNode);
                    xElem.AppendChild(xNodeCoor);
                }
            }
            xDoc.AppendChild(xElem);

            if (PES_PessoaBO.AssociarPessoas(entityPessoa, entityPessoaDeficiencia, UCEnderecos1._VS_enderecos, UCGridContato1._VS_contatos, UCGridDocumento1.RetornaDocumentoSave(), UCGridCertidaoCivil1._VS_certidoes, _VS_pai_idAntigo, _VS_cid_idAntigo, _VS_pes_idPaiAntigo, _VS_pes_idMaeAntigo, _VS_tes_idAntigo, _VS_tde_idAntigo, xDoc))
            {
                ApplicationWEB._GravaLogSistema(LOG_SistemaTipo.Update, "pes_id: " + entityPessoa.pes_id);
                __SessionWEB.PostMessages = UtilBO.GetErroMessage(String.Format("Pessoas associadas com sucesso."), UtilBO.TipoMensagem.Sucesso);

                Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoPessoa/Busca.aspx", false);
            }
            else
            {
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar associar as pessoas.", UtilBO.TipoMensagem.Erro);
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
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar associar as pessoas.", UtilBO.TipoMensagem.Erro);
        }
        finally
        {
            _updGridPessoas.Update();
        }
    }

    private void LoadFromEntity(Guid pes_id)
    {
        UCEnderecos1.Inicializar(true, false, "Endereco");

        //Carrega dados dos endereços da pessoa
        DataTable dtEndereco = PES_PessoaEnderecoBO.GetSelect(pes_id, false, 1, 1);
        UCEnderecos1.CarregarEnderecosBanco(dtEndereco);

        UCGridContato1._CarregarContato();
        UCGridDocumento1._CarregarDocumento(Guid.Empty);
        UCGridCertidaoCivil1._CarregarCertidaoCivil();
    }

    #region ENDERECOS

    //private bool _ValidarEndereco()
    //{
    //    if (UCCadastroEndereco1._txtCEP.Text.Trim() == string.Empty)
    //    {
    //        UCCadastroEndereco1._lblMensagem.Text = UtilBO.GetErroMessage("CEP é obrigatório.", UtilBO.TipoMensagem.Alerta);
    //        return false;
    //    }

    //    UCCadastroEndereco1._revCEP.Validate();

    //    if (!UCCadastroEndereco1._revCEP.IsValid)
    //    {
    //        UCCadastroEndereco1._lblMensagem.Text = UtilBO.GetErroMessage("CEP inválido.", UtilBO.TipoMensagem.Alerta);
    //        return false;
    //    }

    //    if (UCCadastroEndereco1._txtLogradouro.Text.Trim() == string.Empty)
    //    {
    //        UCCadastroEndereco1._lblMensagem.Text = UtilBO.GetErroMessage("Logradouro é obrigatório.", UtilBO.TipoMensagem.Alerta);
    //        return false;
    //    }

    //    if (UCCadastroEndereco1._txtNumero.Text.Trim() == string.Empty)
    //    {
    //        UCCadastroEndereco1._lblMensagem.Text = UtilBO.GetErroMessage("Número é obrigatório.", UtilBO.TipoMensagem.Alerta);
    //        return false;
    //    }

    //    if (UCCadastroEndereco1._txtBairro.Text.Trim() == string.Empty)
    //    {
    //        UCCadastroEndereco1._lblMensagem.Text = UtilBO.GetErroMessage("Bairro é obrigatório.", UtilBO.TipoMensagem.Alerta);
    //        return false;
    //    }

    //    if (UCCadastroEndereco1._txtCidade.Text.Trim() == string.Empty)
    //    {
    //        UCCadastroEndereco1._lblMensagem.Text = UtilBO.GetErroMessage("Cidade é obrigatório.", UtilBO.TipoMensagem.Alerta);
    //        return false;
    //    }

    //    UCCadastroEndereco1._lblMensagem.Visible = false;
    //    return true;
    //}

    //private void _IncluirEndereco()
    //{
    //    DataRow dr = UCCadastroEndereco1._VS_enderecos.NewRow();

    //    dr["id"] = Guid.NewGuid();
    //    dr["end_id"] = UCCadastroEndereco1._VS_end_id;
    //    dr["end_cep"] = UCCadastroEndereco1._txtCEP.Text;
    //    dr["end_logradouro"] = UCCadastroEndereco1._txtLogradouro.Text;
    //    dr["end_distrito"] = UCCadastroEndereco1._txtDistrito.Text;
    //    dr["end_zona"] = UCCadastroEndereco1._ComboZona.SelectedValue == "-1" ? Convert.ToByte(0) : Convert.ToByte(UCCadastroEndereco1._ComboZona.SelectedValue);
    //    dr["end_bairro"] = UCCadastroEndereco1._txtBairro.Text;
    //    dr["cid_id"] = UCCadastroEndereco1._VS_cid_id;
    //    dr["cid_nome"] = UCCadastroEndereco1._txtCidade.Text;
    //    dr["numero"] = UCCadastroEndereco1._txtNumero.Text;
    //    dr["complemento"] = UCCadastroEndereco1._txtComplemento.Text;

    //    UCCadastroEndereco1._VS_enderecos.Rows.Add(dr);

    //    UCGridEndereco1._grvEnderecos.DataSource = UCCadastroEndereco1._VS_enderecos;
    //    UCGridEndereco1._grvEnderecos.DataBind();
    //    UCGridEndereco1._updGridEnderecos.Update();

    //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "CadastroEndereco", "$('#divEnderecos').dialog('close');", true);

    //    UCCadastroEndereco1._LimparCampos();
    //    _btnIncluirEndereco.Text = "Incluir";
    //}

    //private void _AlterarEndereco(Guid id)
    //{
    //    for (int i = 0; i < UCCadastroEndereco1._VS_enderecos.Rows.Count; i++)
    //    {
    //        if (UCCadastroEndereco1._VS_enderecos.Rows[i].RowState != DataRowState.Deleted)
    //        {
    //            if (UCCadastroEndereco1._VS_enderecos.Rows[i]["id"].ToString() == Convert.ToString(id))
    //            {
    //                UCCadastroEndereco1._VS_enderecos.Rows[i]["end_id"] = UCCadastroEndereco1._VS_end_id.ToString();
    //                UCCadastroEndereco1._VS_enderecos.Rows[i]["end_cep"] = UCCadastroEndereco1._txtCEP.Text;
    //                UCCadastroEndereco1._VS_enderecos.Rows[i]["end_logradouro"] = UCCadastroEndereco1._txtLogradouro.Text;
    //                UCCadastroEndereco1._VS_enderecos.Rows[i]["end_distrito"] = UCCadastroEndereco1._txtDistrito.Text;
    //                UCCadastroEndereco1._VS_enderecos.Rows[i]["end_zona"] = UCCadastroEndereco1._ComboZona.SelectedValue == "-1" ? Convert.ToByte(0) : Convert.ToByte(UCCadastroEndereco1._ComboZona.SelectedValue);
    //                UCCadastroEndereco1._VS_enderecos.Rows[i]["end_bairro"] = UCCadastroEndereco1._txtBairro.Text;
    //                UCCadastroEndereco1._VS_enderecos.Rows[i]["cid_id"] = UCCadastroEndereco1._VS_cid_id.ToString();
    //                UCCadastroEndereco1._VS_enderecos.Rows[i]["cid_nome"] = UCCadastroEndereco1._txtCidade.Text;
    //                UCCadastroEndereco1._VS_enderecos.Rows[i]["numero"] = UCCadastroEndereco1._txtNumero.Text;
    //                UCCadastroEndereco1._VS_enderecos.Rows[i]["complemento"] = UCCadastroEndereco1._txtComplemento.Text;

    //                UCGridEndereco1._grvEnderecos.DataSource = UCCadastroEndereco1._VS_enderecos;
    //                UCGridEndereco1._grvEnderecos.DataBind();
    //                UCGridEndereco1._updGridEnderecos.Update();

    //                ScriptManager.RegisterClientScriptBlock(this, GetType(), "CadastroEndereco", "$('#divEnderecos').dialog('close');", true);

    //                UCCadastroEndereco1._LimparCampos();
    //                _btnIncluirEndereco.Text = "Incluir";
    //            }
    //        }
    //    }
    //}

    #endregion ENDERECOS

    #endregion METODOS

    protected void _grvAssociarPessoas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnSelecionar = (LinkButton)e.Row.FindControl("_btnSelecionar");
            if (btnSelecionar != null)
            {
                btnSelecionar.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }

    protected void _grvAssociarPessoas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Selecionar")
        {
            int index = int.Parse(e.CommandArgument.ToString());
            _MarcaLinhaPadrao(index);

            _VS_pes_id = new Guid(_grvAssociarPessoas.DataKeys[index].Values[0].ToString());

            _CarregarPessoa();
        }
    }

    protected void _btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect(__SessionWEB._AreaAtual._Diretorio + "ManutencaoPessoa/Busca.aspx", false);
    }

    protected void _btnSalvar_Click(object sender, EventArgs e)
    {
        _Associar();
    }
}