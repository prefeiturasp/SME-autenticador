using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Data;
using Autenticador.BLL;
using Autenticador.Entities;
using Autenticador.Web.WebProject;

public partial class AreaAdm_LogErro_Busca : MotherPageLogado
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.DefaultButton = _btnPesquisa.UniqueID;

        ScriptManager sm = ScriptManager.GetCurrent(this);
        if (sm != null)
        {
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JQueryValidation));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.JqueryMask));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.MascarasCampos));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.CamposData));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.UiAriaTabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.Tabs));
            sm.Scripts.Add(new ScriptReference(ArquivoJS.PastaScriptRaiz + "Telas/jsModuloSeguranca.js"));
        }

        if (String.IsNullOrEmpty(_VS_Root))
        {
            _VS_Root = String.Concat(ApplicationWEB._DiretorioFisico, @"\Log\");
            _VS_Path = String.Empty;
        }        

        if (!IsPostBack)
        {
            _txtDtInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
            _txtDtFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDataInicial.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDataFinal.Text = DateTime.Now.ToString("dd/MM/yyyy");

            try
            {
                UCComboSistema1.Inicialize("Sistema");
                UCComboSistema1._ShowSelectMessage = true;
                UCComboSistema1._EnableValidator = false;
                UCComboSistema1._Load();

                _caminho = String.Concat(_VS_Root, _VS_Path);
                _lblEndereco.Text = String.Format("<b>Endereço:</b> {0}", String.Concat(@"..\", _VS_Path));
                _CarregaGridDir();
                _CarregaGridFiles();
                _dgvDatas.PageSize =
                _dgvLogs.PageSize = ApplicationWEB._Paginacao;

                VerificaBusca();
            }
            catch (Exception err)
            {
                ApplicationWEB._GravaErro(err);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar o sistema.", UtilBO.TipoMensagem.Alerta);
            }
        }
    }

    #region CONSTANTES
    
    //Constantes que determinam os índeces de algumas colunas do Grid(dgvQtdErro) localizado 3ª aba.
    // A nomenclatura segue o seguinte padrão --> Exemplo: COL_DATA => "Coluna do Grid(dgvQtdErro) denominada data".
    
    private const int COL_DATA = 0;
    private const int COL_TIPO_ERRO = 1;
    private const int COL_ID_SISTEMA = 3;
    
    #endregion


    #region ATRIBUTOS

    private string _caminho;
    private string[] _strFiles;
    private string[] _strDirs;

    #endregion

    #region PROPRIEDADES

    protected string _VS_Root
    {
        get
        {
            if (ViewState["root"] == null)
                return String.Empty;
            return ViewState["root"].ToString();
        }
        set
        {
            ViewState["root"] = value;
        }
    }

    protected string _VS_sis_id
    {
        get
        {
            if (ViewState["_VS_sis_id"] == null)
                return String.Empty;
            return ViewState["_VS_sis_id"].ToString();
        }
        set
        {
            ViewState["_VS_sis_id"] = value;
        }
    }

    protected string _VS_Path
    {
        get
        {
            if (ViewState["path"] == null)
                return String.Empty;
            return ViewState["path"].ToString();
        }
        set
        {
            ViewState["path"] = value;
        }
    }

    #endregion

    #region METODOS

    private void _Pesquisar()
    {
        try
        {
            Dictionary<string, string> filtros = new Dictionary<string, string>();
            DateTime dataInicial;
            string dataStringInicial = string.Empty;

            DateTime dataFinal;
            string dataStringFinal = string.Empty;

            _dgvDatas.PageIndex = 0;
            odsDatas.SelectParameters.Clear();

            if (!string.IsNullOrEmpty(_txtDtInicio.Text))
            {
                dataInicial = Convert.ToDateTime(_txtDtInicio.Text);
                dataStringInicial = dataInicial.ToString("yyyy-MM-dd");
            }

            if (!string.IsNullOrEmpty(_txtDtFinal.Text))
            {
                dataFinal = Convert.ToDateTime(_txtDtFinal.Text);
                dataStringFinal = dataFinal.ToString("yyyy-MM-dd");
            }

            odsDatas.SelectParameters.Add("sis_id", UCComboSistema1._Combo.SelectedValue);
            odsDatas.SelectParameters.Add("dataInicio", dataStringInicial);
            odsDatas.SelectParameters.Add("dataTermino", dataStringFinal);            
            odsDatas.SelectParameters.Add("usu_login", _txtUsuario.Text);
            odsDatas.DataBind();

            #region Salvar busca realizada com os parâmetros do ODS.

            foreach (Parameter param in odsDatas.SelectParameters)
            {
                filtros.Add(param.Name, param.DefaultValue);
            }

            __SessionWEB.BuscaRealizada = new Busca
            {
                PaginaBusca = Pagina.AreaAdm_LogErro
                ,
                Filtros = filtros
            };

            #endregion

            _dgvDatas.DataBind();
        }
        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar os logs de erro.", UtilBO.TipoMensagem.Erro);
        }
    }

    private void PesquisarQtdErros()
    {
        try
        {
            dgvQtdErros.PageSize = ApplicationWEB._Paginacao;
            dgvQtdErros.PageIndex = 0;
           // odsQtdErros.SelectParameters.Clear();

            //odsQtdErros.SelectParameters.Add("dataInicio", Convert.ToDateTime(txtDataInicial.Text).ToString("yyyy-MM-dd"));
            //odsQtdErros.SelectParameters.Add("dataTermino", Convert.ToDateTime(txtDataFinal.Text).ToString("yyyy-MM-dd"));
            //odsQtdErros.DataBind();
            
            dgvQtdErros.DataBind();
                       
        }

        catch (Exception ex)
        {
            ApplicationWEB._GravaErro(ex);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar carregar os logs de erro.", UtilBO.TipoMensagem.Erro);
        }
    }

    
    /// Verifica se tem busca salva na sessão e realiza automaticamente, caso positivo.
    /// </summary>
    private void VerificaBusca()
    {
        if (__SessionWEB.BuscaRealizada.PaginaBusca == Pagina.AreaAdm_LogErro)
        {
            // Recuperar busca realizada e pesquisar automaticamente
            string valor;            
            
            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("sis_id", out valor);
            if (!string.IsNullOrEmpty(valor) && valor != (-1).ToString())
            {
                UCComboSistema1.SetaEventoSource();
                UCComboSistema1._Combo.DataBind();
                UCComboSistema1._Combo.SelectedValue = valor;
            }            

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("dataInicio", out valor);
            if (!string.IsNullOrEmpty(valor))
                _txtDtInicio.Text = Convert.ToDateTime(valor).ToString("dd/MM/yyyy");

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("dataTermino", out valor);
            if (!string.IsNullOrEmpty(valor))
                _txtDtFinal.Text = Convert.ToDateTime(valor).ToString("dd/MM/yyyy");

            __SessionWEB.BuscaRealizada.Filtros.TryGetValue("usu_login", out valor);
            _txtUsuario.Text = valor;

            _Pesquisar();
        }
        else
        {
            fdsResultado.Visible = false;
        }
    }

    /// <summary>
    /// Força o download de um arquivo
    /// </summary>
    /// <param name="pathArquivo">Caminho do arquivo no servidor</param>
    /// <param name="nomeArquivoDownload">Nome do arquivo retornado</param>
    protected void _DownloadArquivo(string pathArquivo, string nomeArquivoDownload)
    {
        System.IO.FileStream fs = new System.IO.FileStream(pathArquivo, System.IO.FileMode.Open, System.IO.FileAccess.Read);

        try
        {
            byte[] buffer = new byte[(int)fs.Length];
            fs.Read(buffer, 0, (int)fs.Length);

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + nomeArquivoDownload);
            Response.AddHeader("Content-Length", fs.Length.ToString());
            Response.ContentType = "application/octet-stream";

            Response.BinaryWrite(buffer);
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        finally
        {
            fs.Close();
        }
    }

    protected void _CarregaGridDir()
    {
        if (System.IO.Directory.Exists(_caminho))
            _strDirs = System.IO.Directory.GetDirectories(_caminho);

        DataTable dtDirs = new DataTable();
        dtDirs.Columns.Add("Directories");

        DataRow drDirs;
        if (_strDirs != null)
        {
            for (int i = 0; i < _strDirs.Length; i++)
            {
                _strDirs[i] = _strDirs[i].Replace(_caminho, "");
                drDirs = dtDirs.NewRow();
                drDirs[0] = _strDirs[i];
                dtDirs.Rows.Add(drDirs);
            }
        }

        if (!_caminho.EndsWith(@"\Log\"))
        {
            drDirs = dtDirs.NewRow();
            drDirs["Directories"] = "..";
            dtDirs.Rows.InsertAt(drDirs, 0);
        }
        if (dtDirs.Rows.Count > 0)
        {
            DataView dvDirs = dtDirs.DefaultView;
            dvDirs.Sort = "Directories";
            _dgvPastas.PageSize = ApplicationWEB._Paginacao;
            _dgvPastas.DataSource = dvDirs;
            _dgvPastas.DataBind();
        }
    }

    protected void _CarregaGridFiles()
    {
        if (System.IO.Directory.Exists(_caminho))
            _strFiles = System.IO.Directory.GetFiles(_caminho);

        DataTable dtFiles = new DataTable();
        dtFiles.Columns.Add("Files", Type.GetType("System.String"));
        dtFiles.Columns.Add("DateCreate", Type.GetType("System.DateTime"));

        if (_strFiles != null)
        {
            for (int i = 0; i < _strFiles.Length; i++)
            {
                DateTime dtCreate = System.IO.File.GetCreationTime(_strFiles[i]);
                _strFiles[i] = _strFiles[i].Replace(_caminho, "");
                DataRow drFiles = dtFiles.NewRow();
                drFiles[0] = _strFiles[i];
                drFiles[1] = dtCreate;
                dtFiles.Rows.Add(drFiles);
            }
        }

        DataView dvFiles = dtFiles.DefaultView;
        dvFiles.Sort = "DateCreate DESC";
        _dgvArquivos.PageSize = ApplicationWEB._Paginacao;
        _dgvArquivos.DataSource = dtFiles;
        _dgvArquivos.DataBind();
    }

    protected void _Exportar_ArquivoTXT(List<LOG_Erros> erros, string fileName)
    {
        try
        {
            CoreLibrary.Web.Util.GeraHTML.GeraHTML gera = new CoreLibrary.Web.Util.GeraHTML.GeraHTML();
            gera._FileName = System.IO.Path.GetFileNameWithoutExtension(fileName);
            gera._FileExtension = System.IO.Path.GetExtension(fileName);
            gera._Encoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

            foreach(LOG_Erros erro in erros)
            {
                gera._Add(erro.err_descricao);
                gera._Add("\\r\\n");
            }
            gera._GenerateForDownload();
        }
        catch (Exception)
        {
            throw;
        }
    }

    #endregion

    #region EVENTOS

    protected void _btnPesquisa_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            fdsResultado.Visible = false;
        else
        {
            cvDatas.Visible = true;
            cvDatas.Validate();
            if (Page.IsValid)
            {
                if (!string.IsNullOrEmpty(_txtDtInicio.Text) && !string.IsNullOrEmpty(_txtDtFinal.Text))
                {
                    if (Convert.ToDateTime(_txtDtFinal.Text) >= Convert.ToDateTime(_txtDtInicio.Text).AddDays(30))
                    {
                        _lblMessage.Text = UtilBO.GetErroMessage("O intervalo de datas não pode ser maior que 30 dias.", UtilBO.TipoMensagem.Alerta);
                        fdsResultado.Visible = false;
                    }
                    else
                    {
                        fdsResultado.Visible = true;
                        _Pesquisar();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(_txtDtInicio.Text))
                        _lblMessage.Text = UtilBO.GetErroMessage("Data inicial é obrigatório.", UtilBO.TipoMensagem.Alerta);
                    else if (string.IsNullOrEmpty(_txtDtFinal.Text))
                        _lblMessage.Text = UtilBO.GetErroMessage("Data final é obrigatório.", UtilBO.TipoMensagem.Alerta);

                    fdsResultado.Visible = false;
                }
            }
            else
                fdsResultado.Visible = false;
            cvDatas.Visible = false;
        }

        _dgvLogs.Visible = false;
    }
    
    //Botão pesquisar da terceira aba
    protected void btnPesquisarLog_Click(object sender, EventArgs e)
    {          
        if (!Page.IsValid)
            fdsResultadoLog.Visible = false;
        else
        {
            cvDatasAba3.Visible = true;
            cvDatasAba3.Validate();
            
            if (Page.IsValid)
            {
                if (!string.IsNullOrEmpty(txtDataInicial.Text) && !string.IsNullOrEmpty(txtDataFinal.Text))
                {
                    if (Convert.ToDateTime(txtDataFinal.Text) >= Convert.ToDateTime(txtDataInicial.Text).AddDays(30))
                    {
                        _lblMessage.Text = UtilBO.GetErroMessage("O intervalo de datas não pode ser maior que 30 dias.", UtilBO.TipoMensagem.Alerta);
                        fdsResultadoLog.Visible = false;
                    }
                    else
                    {
                        fdsResultadoLog.Visible = true;
                        PesquisarQtdErros();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtDataInicial.Text))
                        _lblMessage.Text = UtilBO.GetErroMessage("Data inicial é obrigatório.", UtilBO.TipoMensagem.Alerta);
                    else if (string.IsNullOrEmpty(txtDataFinal.Text))
                        _lblMessage.Text = UtilBO.GetErroMessage("Data final é obrigatório.", UtilBO.TipoMensagem.Alerta);

                    fdsResultadoLog.Visible = false;
                }
            }
            else
                fdsResultadoLog.Visible = false;
          
        }
    }
        
    protected void ValidarDatas_ServerValidate(object source, ServerValidateEventArgs args)
    {         
        bool flag = true;

        DateTime dtIni = Convert.ToDateTime(_txtDtInicio.Text);
        DateTime dtFim = Convert.ToDateTime(_txtDtFinal.Text);

        if(dtIni > dtFim)
            flag = false;

        args.IsValid = flag;
    }

    protected void ValidarDatas_ServerValidate2(object source, ServerValidateEventArgs args)
    {                
            bool flag = true;

            DateTime dtIni = Convert.ToDateTime(txtDataInicial.Text);
            DateTime dtFim = Convert.ToDateTime(txtDataFinal.Text);

            if (dtIni > dtFim)
                flag = false;

            args.IsValid = flag;              
    }
    
    protected void odsQtdErros_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }
   
    protected void odsTipoErro_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount) 
            e.InputParameters.Clear();
    }

    protected void odsDatas_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount)
            e.InputParameters.Clear();
    }

    protected void odsLogs_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        if (e.ExecutingSelectCount) 
            e.InputParameters.Clear();
    }

    protected void dgvQtdErros_RowCommand(object sender, GridViewCommandEventArgs e)
    {        
        if (e.CommandName.Equals("SelectTipoErro"))
        {

            try
            {
                //this.dgvQtdErros.SelectedDataKey["sis_id"]
                int index = int.Parse(e.CommandArgument.ToString());
                string sis_id = dgvQtdErros.Rows[index].Cells[COL_ID_SISTEMA].Text.Equals("&nbsp;") ? null : dgvQtdErros.Rows[index].Cells[COL_ID_SISTEMA].Text;
                string data = dgvQtdErros.Rows[index].Cells[COL_DATA].Text;
                string err_tipoErro = dgvQtdErros.Rows[index].Cells[COL_TIPO_ERRO].Text;
               
                                
                dgvTipoErro.PageIndex = 0;
                odsTipoErro.SelectParameters.Clear();

                odsTipoErro.SelectParameters.Add("sis_id", TypeCode.Int32, sis_id);
                odsTipoErro.SelectParameters.Add("data", TypeCode.DateTime, data);
                odsTipoErro.SelectParameters.Add("err_tipoErro", TypeCode.String, err_tipoErro);
                odsTipoErro.DataBind();

                dgvTipoErro.Visible = true;
            }
            catch (Exception err)
            {
                ApplicationWEB._GravaErro(err); 
                _lblMessage.Text = UtilBO.GetErroMessage(String.Format("Erro ao tentar selecionar a data {0}.", e.CommandArgument), UtilBO.TipoMensagem.Alerta);
            }
        }
    }
        
    protected void _dgvDatas_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("Select"))
        {
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                string sis_id = _dgvDatas.Rows[index].Cells[4].Text.Equals("&nbsp;") ? null : _dgvDatas.Rows[index].Cells[4].Text;

                string data = _dgvDatas.Rows[index].Cells[0].Text;

                _dgvLogs.PageIndex = 0;
                odsLogs.SelectParameters.Clear();
                odsLogs.SelectParameters.Add("data", TypeCode.DateTime,data);
                odsLogs.SelectParameters.Add("sis_id", TypeCode.Int32, sis_id);
                odsLogs.SelectParameters.Add("usu_login", TypeCode.String, _txtUsuario.Text);
                odsLogs.SelectParameters.Add("paginado", TypeCode.Boolean, "true");
                odsLogs.DataBind();
                _dgvLogs.Visible = true;
            }
            catch (Exception err)
            {
                ApplicationWEB._GravaErro(err);
                _lblMessage.Text = UtilBO.GetErroMessage(String.Format("Erro ao tentar selecionar a data {0}.", e.CommandArgument), UtilBO.TipoMensagem.Alerta);
            }
        }
        else if (e.CommandName.Equals("Download"))
        {
            try
            {
                int sis_id = Convert.ToInt32(UCComboSistema1._Combo.SelectedValue);
                DateTime data = Convert.ToDateTime(e.CommandArgument);
                string fileName = String.Format("Log_{0}_{1}_{2}.txt"
                                , data.Year
                                , data.Month
                                , data.Day);
                List<LOG_Erros> lt = LOG_ErrosBO.GetSelectBy_dia2(sis_id, data, _txtUsuario.Text, false, 1, 1);
                _Exportar_ArquivoTXT(lt, fileName);
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception err)
            {
                ApplicationWEB._GravaErro(err);
                _lblMessage.Text = UtilBO.GetErroMessage(String.Format("Erro ao tentar baixar o log de erros da data {0}.", e.CommandArgument), UtilBO.TipoMensagem.Alerta);
            }
        }
    }

    protected void _dgvLogs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Download"))
        {
            try
            {
                Guid guid = new Guid(e.CommandArgument.ToString());
                LOG_Erros entity = new LOG_Erros
                {
                    err_id = guid
                };
                LOG_ErrosBO.GetEntity(entity);
                string fileName = String.Format("Log_{0}_{1}_{2}.txt"
                                , entity.err_dataHora.Year
                                , entity.err_dataHora.Month
                                , entity.err_dataHora.Day);
                List<LOG_Erros> lt = new List<LOG_Erros>();
                lt.Add(entity);
                _Exportar_ArquivoTXT(lt, fileName);
            }
            catch (ThreadAbortException)
            {
            }
            catch(Exception err)
            {
                ApplicationWEB._GravaErro(err);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar baixar o log de erros.", UtilBO.TipoMensagem.Alerta);
            }
        }
    }

    protected void _dgvPastas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
        {
            try
            {
                if (e.CommandArgument.ToString() == "..")
                {
                    _VS_Path = _VS_Path.Remove(_VS_Path.LastIndexOf(@"\"));
                    _VS_Path = _VS_Path.Remove(_VS_Path.LastIndexOf(@"\") + 1);
                }
                else
                    _VS_Path += String.Concat(e.CommandArgument, @"\");
                _caminho = String.Concat(_VS_Root, _VS_Path);
                _lblEndereco.Text = String.Format("<b>Endereço:</b> {0}", String.Concat(@"..\", _VS_Path));
                _CarregaGridDir();
                _CarregaGridFiles();
            }
            catch (Exception err)
            {
                ApplicationWEB._GravaErro(err);
                _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar selecionar o diretório.", UtilBO.TipoMensagem.Alerta);
            }
        }
    }

    protected void _dgvArquivos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Download"))
        {
            try
            {
                string caminho = String.Concat(_VS_Root, _VS_Path, e.CommandArgument);
                string nome = e.CommandArgument.ToString();
                _DownloadArquivo(caminho, nome);
            }
            catch (Exception err)
            {
                ApplicationWEB._GravaErro(err);
                _lblMessage.Text = UtilBO.GetErroMessage(String.Format("Erro ao tentar baixar o arquivo {0}.", e.CommandArgument), UtilBO.TipoMensagem.Alerta);
            }
        }
    }

    protected void _dgvArquivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            txtSelectedTab.Value = "2";
            _caminho = String.Concat(_VS_Root, _VS_Path);
            _dgvArquivos.PageIndex = e.NewPageIndex;
            _CarregaGridFiles();
        }
        catch (Exception err)
        {
            ApplicationWEB._GravaErro(err);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar trocar a página.", UtilBO.TipoMensagem.Alerta);
        }
    }

    protected void _dgvPastas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            txtSelectedTab.Value = "2";
            _caminho = String.Concat(_VS_Root, _VS_Path);
            _dgvPastas.PageIndex = e.NewPageIndex;
            _CarregaGridDir();
        }
        catch (Exception err)
        {
            ApplicationWEB._GravaErro(err);
            _lblMessage.Text = UtilBO.GetErroMessage("Erro ao tentar trocar a página.", UtilBO.TipoMensagem.Alerta);
        }
    }
    
    protected void dgvTipoErro_RowDataBound(object sender, GridViewRowEventArgs e) 
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lkbDescricao = (LinkButton)e.Row.FindControl("lkbDescricao");
            if (lkbDescricao != null)
            {
                lkbDescricao.CommandArgument = e.Row.RowIndex.ToString();

                Guid err_id = new Guid(dgvTipoErro.DataKeys[e.Row.RowIndex].Value.ToString());
                LOG_Erros entity = new LOG_Erros
                {
                    err_id = err_id
                };
                LOG_ErrosBO.GetEntity(entity);

                string descricao = entity.err_descricao;
                descricao = descricao.Replace("\r\n", "\\n");
                descricao = descricao.Replace("\\", "\\\\");
                descricao = descricao.Replace("\\\\n", "\\n");
                descricao = descricao.Replace("'", "\"");

                lkbDescricao.OnClientClick = "javascript:ExibirDescricao(this, '" + descricao + "'); return false;";
            }
        }
    }

    protected void _dgvLogs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnAlterar = (LinkButton)e.Row.FindControl("_btnAlterar");
            if (btnAlterar != null)
            {                          
                btnAlterar.CommandArgument = e.Row.RowIndex.ToString();

                Guid err_id = new Guid(_dgvLogs.DataKeys[e.Row.RowIndex].Value.ToString());
                LOG_Erros entity = new LOG_Erros
                {
                    err_id = err_id
                };
                LOG_ErrosBO.GetEntity(entity);

                string descricao = entity.err_descricao;
                descricao = descricao.Replace("\r\n", "\\n");
                descricao = descricao.Replace("\\", "\\\\");
                descricao = descricao.Replace("\\\\n", "\\n");
                descricao = descricao.Replace("'", "\"");
                                
                btnAlterar.OnClientClick = "javascript:ExibirDescricao(this, '" + descricao + "'); return false;";
            }
        }
    }
        
    protected void dgvQtdErros_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lkbTipoErro = (LinkButton)e.Row.FindControl("lkbTipoErro");
            if (lkbTipoErro != null)
            {
                lkbTipoErro.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }
     
    protected void _dgvDatas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lkbVisualizar = (LinkButton)e.Row.FindControl("_lkbVisualizar");
            if (lkbVisualizar != null)
            {
                lkbVisualizar.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }

    #endregion

  
   
}
