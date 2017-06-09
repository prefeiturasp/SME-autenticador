using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

namespace Autenticador.UserControlLibrary.Combos
{
    [Serializable()]
    public class UCComboItemMessage
    {
        #region ATRIBUTOS

        protected string _message;
        protected string _value;

        #endregion ATRIBUTOS

        #region METODOS

        /// <summary>
        /// Através do objeto retornado na consulta, checa o tipo de dados usado na propriedade
        /// DataValueField do combo e insere o seu representante nulo.
        /// </summary>
        /// <param name="obj">Objeto retornado na consulta</param>
        /// <param name="dataValueName">Nome do campo usado na propriedade DataValueField do combo</param>
        public void ChangeValueType(object obj, string dataValueName)
        {
            try
            {
                if (obj != null)
                {
                    var dt = obj as DataTable;
                    if (dt != null)
                        this.ChangeValueType(dt, dataValueName);
                    else if (obj.GetType().IsGenericType)
                        this.ChangeValueType(obj as IList, dataValueName);
                }
            }
            catch (TypeLoadException)
            {
                throw;
            }
        }

        /// <summary>
        /// Através do DataTable retornado na consulta, checa o tipo de dados usado na propriedade
        /// DataValueField do combo e insere o seu representante nulo.
        /// </summary>
        /// <param name="dt">DataTable retornado na consulta</param>
        /// <param name="dataValueName">Nome do campo usado na propriedade DataValueField do combo</param>
        public void ChangeValueType(DataTable dt, string dataValueName)
        {
            Type t = null;
            if (dt.Columns.Contains(dataValueName))
            {
                DataColumn dc = dt.Columns[dataValueName];
                t = dc.DataType;
            }
            this.ChangeValueType(t);
        }

        /// <summary>
        /// Através do objeto retornado na consulta, checa o tipo de dados usado na propriedade
        /// DataValueField do combo e insere o seu representante nulo.
        /// </summary>
        /// <param name="lt">List retornado na consulta</param>
        /// <param name="dataValueName">Nome do campo usado na propriedade DataValueField do combo</param>
        public void ChangeValueType(IList lt, string dataValueName)
        {
            Type t = lt.GetType().GetProperty("Item").PropertyType;
            if (t != null)
                t = t.GetProperty(dataValueName).PropertyType;
            this.ChangeValueType(t);
        }

        /// <summary>
        /// Através do tipo do dados ele seleciona qual será o value padrão para dados vazíos.
        /// </summary>
        /// <param name="t">Type do campo usado no DataValueField</param>
        protected virtual void ChangeValueType(Type t)
        {
            if (t == typeof(Guid))
                this._value = Guid.Empty.ToString();
            else if (t == typeof(Byte))
                this._value = "0";
            else if (t == typeof(DateTime))
            {
                DateTime data = new DateTime();
                this._value = data.ToShortDateString();
            }
            else
                this._value = "-1";
        }

        #endregion METODOS
    }

    [Serializable()]
    public class UCComboSelectItemMessage : UCComboItemMessage
    {
        #region CONSTRUTORES

        public UCComboSelectItemMessage()
        {
            this._message = "-- Selecione uma opção --";
            this._value = "-1";
        }

        public UCComboSelectItemMessage(string message)
        {
            this._message = message;
        }

        public UCComboSelectItemMessage(string message, string value)
            : this(message)
        {
            this._value = value;
        }

        #endregion CONSTRUTORES

        #region PROPRIEDADES

        public string _Message
        {
            get { return this._message; }
            set { this._message = value; }
        }

        public string _Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        #endregion PROPRIEDADES
    }

    [Serializable()]
    public class UCComboItemNotFoundMessage : UCComboItemMessage
    {
        #region CONSTRUTORES

        public UCComboItemNotFoundMessage()
        {
            this._message = "-- Selecione uma opção --";
            this._value = "-1";
        }

        public UCComboItemNotFoundMessage(string message)
        {
            this._message = message;
        }

        public UCComboItemNotFoundMessage(string message, string value)
            : this(message)
        {
            this._value = value;
        }

        #endregion CONSTRUTORES

        #region PROPRIEDADES

        public string _Message
        {
            get { return this._message; }
            set { this._message = value; }
        }

        public string _Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        #endregion PROPRIEDADES
    }

    public abstract class Abstract_UCCombo : System.Web.UI.UserControl
    {
        #region DELEGATE

        public delegate void SelectedIndexChange(object sender, EventArgs e);

        public SelectedIndexChange OnSelectedIndexChange { get; set; }

        #endregion DELEGATE

        #region ATRIBUTOS

        protected UCComboSelectItemMessage _ComboSelectItemMessage = new UCComboSelectItemMessage();
        protected bool _showSelectMessage = false;
        protected UCComboItemNotFoundMessage _ComboItemNotFoundMessage = new UCComboItemNotFoundMessage();
        protected bool _showNotFoundMessage = true;
        protected bool _selecionaAutomatico = true;

        #endregion ATRIBUTOS

        #region PROPRIEDADES

        public abstract DropDownList _Combo { get; set; }

        public abstract Label _Label { get; set; }

        public abstract CompareValidator _Validator { get; set; }

        protected abstract ObjectDataSource _Source { get; set; }

        /// <summary>
        /// Adciona e remove a mensagem "-- Selecione uma opção --" do dropdownlist.
        /// Por padrão é false e a mensagem "-- Selecione uma opção --" não é exibida.
        /// </summary>
        public bool _ShowSelectMessage
        {
            set
            {
                this._showSelectMessage = value;
                this._Combo.AppendDataBoundItems = value;
            }
        }

        /// <summary>
        /// Adciona e remove a mensagem "-- Selecione uma opção --" do dropdownlist.
        /// Por padrão é true e a mensagem "-- Selecione uma opção --" é exibida qdo
        /// não houver itens a selecionar.
        /// </summary>
        public bool _ShowNotFoundMessage
        {
            set
            {
                this._showNotFoundMessage = value;
            }
        }

        /// <summary>
        /// Altera o titulo do combo.
        /// </summary>
        public string _ChangeLabel
        {
            set
            {
                this._Label.Text = value;
            }
        }

        /// <summary>
        /// Mostra ou esconde o titulo do combo.
        /// </summary>
        public bool _ShowLabel
        {
            set
            {
                this._Label.Visible = value;
            }
        }

        /// <summary>
        /// Habilita ou não a validação do campo.
        /// </summary>
        public bool _EnableValidator
        {
            set
            {
                this._Validator.Enabled = value;
            }
        }

        /// <summary>
        /// Nome do grupo para validação.
        /// </summary>
        public string _ValidationGroup
        {
            set
            {
                this._Validator.ValidationGroup = value;
            }
        }

        /// <summary>
        /// Indica se será selecionado automaticamente o valor no combo, se a consulta retornar 1 registro.
        /// </summary>
        public bool _SelecionaAutomatico
        {
            get
            {
                return this._selecionaAutomatico;
            }
            set
            {
                this._selecionaAutomatico = value;
            }
        }

        #endregion PROPRIEDADES

        #region METODOS

        /// <summary>
        /// Altera a mensagem padrão exibida ao usuário para selecionar um item.
        /// </summary>
        /// <param name="message">Mensagem de exibição.</param>
        public void _ChangeSelectItemMessage(string message)
        {
            this._ChangeSelectItemMessage(message, this._ComboSelectItemMessage._Value);
        }

        /// <summary>
        /// Altera a mensagem padrão exibida ao usuário para selecionar um item e atribui um valor.
        /// </summary>
        /// <param name="message">Mensagem de exibição.</param>
        /// <param name="value">Valor do item quando selecionado.</param>
        public void _ChangeSelectItemMessage(string message, string value)
        {
            this._ComboSelectItemMessage._Value = value;
            this._ComboSelectItemMessage._Message = message;
        }

        /// <summary>
        /// Altera a mensagem padrão exibida ao usuário quando não houver nenhum item a selecionar.
        /// </summary>
        /// <param name="message">Mensagem de exibição</param>
        public void _ChangeItemNotFoundMessage(string message)
        {
            this._ChangeItemNotFoundMessage(message, this._ComboItemNotFoundMessage._Value);
        }

        /// <summary>
        /// Altera a mensagem padrão exibida ao usuário quando não houver nenhum item a selecionar
        /// e atribui um valor.
        /// </summary>
        /// <param name="message">Mensagem de exibição</param>
        /// <param name="value">Valor do item quando selecionado.</param>
        public void _ChangeItemNotFoundMessage(string message, string value)
        {
            this._ComboItemNotFoundMessage._Value = value;
            this._ComboItemNotFoundMessage._Message = message;
        }

        /// <summary>
        /// Altera a mensagem de erro do validator.
        /// Caso não for modificado a mensagem padrão
        /// será Nome do Campo (propriedade _Label) + é obrigatório.
        /// </summary>
        /// <param name="errorMessage">
        /// Propriedade ErrorMessage do componente asp:CompareValidator
        /// </param>
        public void _ChangeValidatorMessages(string errorMessage)
        {
            this._ChangeValidatorMessages(errorMessage, String.Empty);
        }

        /// <summary>
        /// Altera a mensagem de erro e o texto do validator.
        /// Caso não for modificado a mensagem de erro padrão
        /// será Nome do Campo (propriedade _Label) + é obrigatório e
        /// o texto *(asterisco).
        /// </summary>
        /// <param name="errorMessage">
        /// Propriedade ErrorMessage do componente asp:CompareValidator
        /// </param>
        /// <param name="text">
        /// Propriedade Text do componente asp:CompareValidator
        /// </param>
        public void _ChangeValidatorMessages(string errorMessage, string text)
        {
            this._ChangeValidatorMessages(errorMessage, text, String.Empty);
        }

        /// <summary>
        /// Altera a mensagem de erro e o texto do validator
        /// e o grupo para validação.
        /// Caso não for modificado a mensagem de erro padrão
        /// será Nome do Campo (propriedade _Label) + é obrigatório e
        /// o texto *(asterisco) e sem grupo para validação.
        /// </summary>
        /// <param name="errorMessage">
        /// Propriedade ErrorMessage do componente asp:CompareValidator
        /// </param>
        /// <param name="text">
        /// Propriedade Text do componente asp:CompareValidator
        /// </param>
        /// <param name="groupValidation">
        /// Propriedade ValidationGroup do componente asp:CompareValidator
        /// </param>
        public void _ChangeValidatorMessages(string errorMessage, string text, string groupValidation)
        {
            this._Validator.Text = text;
            this._Validator.ErrorMessage = errorMessage;
            this._Validator.ValidationGroup = groupValidation;
        }

        /// <summary>
        /// Seta o evento OnSelected do ObjectDataSource.
        /// </summary>
        public void SetaEventoSource()
        {
            try
            {
                this._Source.Selected += _Source_Selected;
            }
            catch (NotImplementedException)
            {
            }
        }

        #endregion METODOS

        #region EVENTOS

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            try
            {
                this._Combo.SelectedIndexChanged += new EventHandler(_Combo_SelectedIndexChanged);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            try
            {
                if (OnSelectedIndexChange != null)
                    this._Combo.AutoPostBack = true;
                else
                    this._Combo.AutoPostBack = false;
            }
            catch (Exception)
            {
                throw;
            }
            //Esse erro pode ser ignorado.
            try
            {
                this._Source.Selected += new ObjectDataSourceStatusEventHandler(_Source_Selected);
            }
            catch 
            {
            }
        }

        protected virtual void _Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnSelectedIndexChange != null)
                OnSelectedIndexChange(sender, e);
        }

        protected virtual void _Source_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            int rowscount = 0;
            if (e.ReturnValue != null)
            {
                if (e.ReturnValue is DataTable)
                    rowscount = ((DataTable)e.ReturnValue).Rows.Count;
                else if (e.ReturnValue.GetType().IsGenericType)
                    rowscount = ((IList)e.ReturnValue).Count;
            }
            if (rowscount > 0)
            {
                if (this._showSelectMessage)
                {
                    this._ComboSelectItemMessage.ChangeValueType(e.ReturnValue, this._Combo.DataValueField);
                    this._Combo.Items.Add(new ListItem(this._ComboSelectItemMessage._Message, this._ComboSelectItemMessage._Value));
                    this._Validator.ValueToCompare = this._ComboSelectItemMessage._Value;

                    // Após consultar o combo, verifica se tem só 1 item no combo, já seleciona ele.
                    if ((_SelecionaAutomatico) && (rowscount == 1) && (e.ReturnValue is DataTable))
                    {
                        _Combo.SelectedValue = Convert.ToString(((DataTable)e.ReturnValue).Rows[0][_Combo.DataValueField]);
                    }
                }
            }
            else
            {
                if (this._showNotFoundMessage)
                {
                    this._ComboItemNotFoundMessage.ChangeValueType(e.ReturnValue, this._Combo.DataValueField);
                    this._Combo.Items.Add(new ListItem(this._ComboItemNotFoundMessage._Message, this._ComboItemNotFoundMessage._Value));
                    this._Validator.ValueToCompare = this._ComboItemNotFoundMessage._Value;
                }
            }
        }

        #endregion EVENTOS

        #region INICIALIZES

        #region Label

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = true
        ///     Text = [parâmetro rótulo]
        /// - CompareValidator:
        ///     Enable = true
        ///     ErroMessage = "[parâmetro rótulo] é obrigatório."
        ///     Text = "*"
        ///     ValidationGroup = String.Empty
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        ///     Qdo não houver item para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        /// </summary>
        /// <param name="rotulo">Nome do campo</param>
        public void Inicialize(string rotulo)
        {
            this.Inicialize(
                true
                , rotulo
                , String.Format("{0} é obrigatório.", rotulo.Replace("*", string.Empty))
                , "*"
                , String.Empty
                , true
                , new UCComboSelectItemMessage()
                , true
                , new UCComboItemNotFoundMessage());
        }

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = true
        ///     Text = [parâmetro rótulo]
        /// - CompareValidator:
        ///     Enable = true
        ///     ErroMessage = "[parâmetro rótulo] é obrigatório."
        ///     Text = "*"
        ///     ValidationGroup = String.Empty
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        ///     Qdo não houver item para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        /// </summary>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="setaValidator">Indica se a propriedade _EnableValidator terá o valor padrão "true" ou não será setada no inicialize</param>
        public void Inicialize(string rotulo, bool setaValidator)
        {
            this.Inicialize(
                rotulo.Replace("*", string.Empty)
                , String.Format("{0} é obrigatório.", rotulo.Replace("*", string.Empty))
                , "*"
                , String.Empty
                , true
                , new UCComboSelectItemMessage()
                , true
                , new UCComboItemNotFoundMessage()
                , setaValidator);
        }

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = true
        ///     ErroMessage = "[parâmetro rótulo] é obrigatório."
        ///     Text = "*"
        ///     ValidationGroup = String.Empty
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        ///     Qdo não houver item para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        public void Inicialize(bool visibleRotulo, string rotulo)
        {
            this.Inicialize(
                 visibleRotulo
                 , rotulo
                 , String.Format("{0} é obrigatório.", rotulo)
                 , "*"
                 , String.Empty
                 , true
                 , new UCComboSelectItemMessage()
                 , true
                 , new UCComboItemNotFoundMessage());
        }

        #endregion Label

        #region Label/Validator

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = true
        ///     ErroMessage = [parâmetro errorMessage]
        ///     Text = "*"
        ///     ValidationGroup = String.Empty
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        ///     Qdo não houver item para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="errorMessage">Mensagem de erro do validator</param>
        public void Inicialize(bool visibleRotulo, string rotulo, string errorMessage)
        {
            this.Inicialize(
                visibleRotulo
                , rotulo
                , errorMessage
                , "*"
                , String.Empty
                , true
                , new UCComboSelectItemMessage()
                , true
                , new UCComboItemNotFoundMessage());
        }

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = [parãmetro enableValidation]
        ///     ErroMessage = [parâmetro errorMessage]
        ///     Text = "*"
        ///     ValidationGroup = String.Empty
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        ///     Qdo não houver item para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="enableValidation">se valida ou não o campo</param>
        /// <param name="errorMessage">Mensagem de erro do validator</param>
        public void Inicialize(bool visibleRotulo, string rotulo, bool enableValidation, string errorMessage)
        {
            this.Inicialize(
                visibleRotulo
                , rotulo
                , enableValidation
                , errorMessage
                , true
                , new UCComboSelectItemMessage()
                , true
                , new UCComboItemNotFoundMessage());
        }

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = true
        ///     ErroMessage = [parâmetro errorMessage]
        ///     Text = [parâmetro errorText]
        ///     ValidationGroup = String.Empty
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        ///     Qdo não houver item para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="errorMessage">Mensagem de erro do validator</param>
        /// <param name="errorText">Texto exibido no validator</param>
        public void Inicialize(bool visibleRotulo, string rotulo, string errorMessage, string errorText)
        {
            this.Inicialize(
                visibleRotulo
                , rotulo
                , errorMessage
                , errorText
                , String.Empty
                , true
                , new UCComboSelectItemMessage()
                , true
                , new UCComboItemNotFoundMessage());
        }

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = true
        ///     ErroMessage = [parâmetro errorMessage]
        ///     Text = [parâmetro errorText]
        ///     ValidationGroup = [parâmetro validationGroup]
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        ///     Qdo não houver item para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="errorMessage">Mensagem de erro do validator</param>
        /// <param name="errorText">Texto exibido no validator</param>
        /// <param name="validationGroup">Grupo de controles para validação</param>
        public void Inicialize(bool visibleRotulo, string rotulo, string errorMessage, string errorText, string validationGroup)
        {
            this.Inicialize(
                visibleRotulo
                , rotulo
                , errorMessage
                , errorText
                , validationGroup
                , true
                , new UCComboSelectItemMessage()
                , true
                , new UCComboItemNotFoundMessage());
        }

        #endregion Label/Validator

        #region Label/Validator/SelectedItem

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = [parãmetro enableValidation]
        ///     ErroMessage = [parâmetro errorMessage]
        ///     Text = "*"
        ///     ValidationGroup = String.Empty
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção:
        ///         Text = "[parâmetro selectItemMessage]"
        ///         value: "[parâmetro selectItemMessage]"
        ///     Qdo não houver item para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="enableValidation">se valida ou não o campo</param>
        /// <param name="errorMessage">Mensagem de erro do validator</param>
        /// <param name="selectItemMessage">Objeto com a mensagem e valor para a seleção de um item</param>
        public void Inicialize(bool visibleRotulo, string rotulo, bool enableValidation, string errorMessage, UCComboSelectItemMessage selectItemMessage)
        {
            this.Inicialize(
                visibleRotulo
                , rotulo
                , enableValidation
                , errorMessage
                , true
                , selectItemMessage
                , true
                , new UCComboItemNotFoundMessage());
        }

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = [parãmetro enableValidation]
        ///     ErroMessage = [parâmetro errorMessage]
        ///     Text = "*"
        ///     ValidationGroup = String.Empty
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção e parâmetro showSelectItemMessage = true:
        ///         Text = "[parâmetro selectItemMessage]"
        ///         value: "[parâmetro selectItemMessage]"
        ///     Qdo não houver item para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="enableValidation">se valida ou não o campo</param>
        /// <param name="errorMessage">Mensagem de erro do validator</param>
        /// <param name="selectItemMessage">Objeto com a mensagem e valor para a seleção de um item</param>
        /// <param name="showSelectItemMessage">Se mostra mensagem de selecione um item para o usuário ou não</param>
        public void Inicialize(bool visibleRotulo, string rotulo, bool enableValidation, string errorMessage, bool showSelectItemMessage, UCComboSelectItemMessage selectItemMessage)
        {
            this.Inicialize(
                visibleRotulo
                , rotulo
                , enableValidation
                , errorMessage
                , showSelectItemMessage
                , selectItemMessage
                , true
                , new UCComboItemNotFoundMessage());
        }

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = true
        ///     ErroMessage = [parâmetro errorMessage]
        ///     Text = [parâmetro errorText]
        ///     ValidationGroup = [parâmetro validationGroup]
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção e parâmetro showSelectItemMessage = true:
        ///         Text = "[parâmetro selectItemMessage]"
        ///         value: "[parâmetro selectItemMessage]"
        ///     Qdo não houver item para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="errorMessage">Mensagem de erro do validator</param>
        /// <param name="errorText">Texto exibido no validator</param>
        /// <param name="validationGroup">Grupo de controles para validação</param>
        /// <param name="selectItemMessage">Objeto com a mensagem e valor para a seleção de um item</param>
        /// <param name="showSelectItemMessage">Se mostra mensagem de selecione um item para o usuário ou não</param>
        public void Inicialize(bool visibleRotulo, string rotulo, string errorMessage, string errorText, string validationGroup, bool showSelectItemMessage, UCComboSelectItemMessage selectItemMessage)
        {
            this.Inicialize(
                visibleRotulo
                , rotulo
                , errorMessage
                , errorText
                , validationGroup
                , showSelectItemMessage
                , selectItemMessage
                , true
                , new UCComboItemNotFoundMessage());
        }

        #endregion Label/Validator/SelectedItem

        #region Label/Validator/ItemNotFound

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = [parãmetro enableValidation]
        ///     ErroMessage = [parâmetro errorMessage]
        ///     Text = "*"
        ///     ValidationGroup = String.Empty
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        ///     Qdo não houver item para seleção:
        ///         Text = "[parâmetro itemNotFoundMessage]"
        ///         value: "[parâmetro itemNotFoundMessage]"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="enableValidation">se valida ou não o campo</param>
        /// <param name="errorMessage">Mensagem de erro do validator</param>
        /// <param name="itemNotFoundMessage">Objeto com a mensagem e valor para nenhum item a selecionar</param>
        public void Inicialize(bool visibleRotulo, string rotulo, bool enableValidation, string errorMessage, UCComboItemNotFoundMessage itemNotFoundMessage)
        {
            this.Inicialize(
                visibleRotulo
                , rotulo
                , enableValidation
                , errorMessage
                , true
                , new UCComboSelectItemMessage()
                , true
                , itemNotFoundMessage);
        }

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = [parãmetro enableValidation]
        ///     ErroMessage = [parâmetro errorMessage]
        ///     Text = "*"
        ///     ValidationGroup = String.Empty
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        ///     Qdo não houver item para seleção e parâmetro showItemNotFoundMessage = true:
        ///         Text = "[parâmetro itemNotFoundMessage]"
        ///         value: "[parâmetro itemNotFoundMessage]"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="enableValidation">se valida ou não o campo</param>
        /// <param name="errorMessage">Mensagem de erro do validator</param>
        /// <param name="itemNotFoundMessage">Objeto com a mensagem e valor para nenhum item a selecionar</param>
        /// <param name="showItemNotFoundMessage">Mostra ou não a mensagem de não há item a selecionar</param>
        public void Inicialize(bool visibleRotulo, string rotulo, bool enableValidation, string errorMessage, bool showItemNotFoundMessage, UCComboItemNotFoundMessage itemNotFoundMessage)
        {
            this.Inicialize(
                visibleRotulo
                , rotulo
                , enableValidation
                , errorMessage
                , true
                , new UCComboSelectItemMessage()
                , showItemNotFoundMessage
                , itemNotFoundMessage);
        }

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = true
        ///     ErroMessage = [parâmetro errorMessage]
        ///     Text = [parâmetro errorText]
        ///     ValidationGroup = [parâmetro validationGroup]
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção:
        ///         Text = "-- Selecione uma opção --"
        ///         value: "-1"
        ///     Qdo não houver item para seleção e parâmetro showItemNotFoundMessage = true:
        ///         Text = "[parâmetro itemNotFoundMessage]"
        ///         value: "[parâmetro itemNotFoundMessage]"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="errorMessage">Mensagem de erro do validator</param>
        /// <param name="errorText">Texto exibido no validator</param>
        /// <param name="validationGroup">Grupo de controles para validação</param>
        /// <param name="itemNotFoundMessage">Objeto com a mensagem e valor para nenhum item a selecionar</param>
        /// <param name="showItemNotFoundMessage">Mostra ou não a mensagem de não há item a selecionar</param>
        public void Inicialize(bool visibleRotulo, string rotulo, string errorMessage, string errorText, bool showItemNotFoundMessage, UCComboItemNotFoundMessage itemNotFoundMessage)
        {
            this.Inicialize(
                visibleRotulo
                , rotulo
                , errorMessage
                , errorText
                , String.Empty
                , true
                , new UCComboSelectItemMessage()
                , showItemNotFoundMessage
                , itemNotFoundMessage);
        }

        #endregion Label/Validator/ItemNotFound

        #region Label/Validator/SelectedItem/ItemNotFound

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = [parãmetro enableValidation]
        ///     ErroMessage = [parâmetro errorMessage]
        ///     Text = "*"
        ///     ValidationGroup = String.Empty
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção:
        ///         Text = "[parâmetro selectItemMessage]"
        ///         value: "[parâmetro selectItemMessage]"
        ///     Qdo não houver item para seleção:
        ///         Text = "[parâmetro itemNotFoundMessage]"
        ///         value: "[parâmetro itemNotFoundMessage]"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="enableValidation">se valida ou não o campo</param>
        /// <param name="errorMessage">Mensagem de erro do validator</param>
        /// <param name="itemNotFoundMessage">Objeto com a mensagem e valor para nenhum item a selecionar</param>
        /// <param name="selectItemMessage">Objeto com a mensagem e valor para a seleção de um item</param>
        public void Inicialize(bool visibleRotulo, string rotulo, bool enableValidation, string errorMessage, UCComboSelectItemMessage selectItemMessage, UCComboItemNotFoundMessage itemNotFoundMessage)
        {
            this.Inicialize(
                visibleRotulo
                , rotulo
                , enableValidation
                , errorMessage
                , true
                , selectItemMessage
                , true
                , itemNotFoundMessage);
        }

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = [parãmetro enableValidation]
        ///     ErroMessage = [parâmetro errorMessage]
        ///     Text = "*"
        ///     ValidationGroup = String.Empty
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção e parâmetro showSelectItemMessage = true:
        ///         Text = "[parâmetro selectItemMessage]"
        ///         value: "[parâmetro selectItemMessage]"
        ///     Qdo não houver item para seleção e parâmetro showItemNotFoundMessage = true:
        ///         Text = "[parâmetro itemNotFoundMessage]"
        ///         value: "[parâmetro itemNotFoundMessage]"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="enableValidation">se valida ou não o campo</param>
        /// <param name="errorMessage">Mensagem de erro do validator</param>
        /// <param name="showItemNotFoundMessage">Mostra ou não a mensagem de não há item a selecionar</param>
        /// <param name="itemNotFoundMessage">Objeto com a mensagem e valor para nenhum item a selecionar</param>
        /// <param name="selectItemMessage">Objeto com a mensagem e valor para a seleção de um item</param>
        /// <param name="showSelectItemMessage">Se mostra mensagem de selecione um item para o usuário ou não</param>
        public void Inicialize(bool visibleRotulo, string rotulo, bool enableValidation, string errorMessage, bool showSelectItemMessage, UCComboSelectItemMessage selectItemMessage, bool showItemNotFoundMessage, UCComboItemNotFoundMessage itemNotFoundMessage)
        {
            this._ShowLabel = visibleRotulo;
            this._ChangeLabel = rotulo;
            this._EnableValidator = enableValidation;
            this._ChangeValidatorMessages(errorMessage, "*", String.Empty);
            this._ShowSelectMessage = showSelectItemMessage;
            this._ChangeSelectItemMessage(selectItemMessage._Message, selectItemMessage._Value);
            this._ShowNotFoundMessage = showItemNotFoundMessage;
            this._ChangeItemNotFoundMessage(itemNotFoundMessage._Message, itemNotFoundMessage._Value);
        }

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = true
        ///     ErroMessage = [parâmetro errorMessage]
        ///     Text = [parâmetro errorText]
        ///     ValidationGroup = [parâmetro validationGroup]
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção e parâmetro showSelectItemMessage = true:
        ///         Text = "[parâmetro selectItemMessage]"
        ///         value: "[parâmetro selectItemMessage]"
        ///     Qdo não houver item para seleção e parâmetro showItemNotFoundMessage = true:
        ///         Text = "[parâmetro itemNotFoundMessage]"
        ///         value: "[parâmetro itemNotFoundMessage]"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="errorMessage">Mensagem de erro do validator</param>
        /// <param name="errorText">Texto exibido no validator</param>
        /// <param name="validationGroup">Grupo de controles para validação</param>
        /// <param name="itemNotFoundMessage">Objeto com a mensagem e valor para nenhum item a selecionar</param>
        /// <param name="showItemNotFoundMessage">Mostra ou não a mensagem de não há item a selecionar</param>
        /// <param name="selectItemMessage">Objeto com a mensagem e valor para a seleção de um item</param>
        /// <param name="showSelectItemMessage">Se mostra mensagem de selecione um item para o usuário ou não</param>
        public void Inicialize(bool visibleRotulo, string rotulo, string errorMessage, string errorText, string validationGroup, bool showSelectItemMessage, UCComboSelectItemMessage selectItemMessage, bool showItemNotFoundMessage, UCComboItemNotFoundMessage itemNotFoundMessage)
        {
            this._ShowLabel = visibleRotulo;
            this._ChangeLabel = rotulo;
            this._EnableValidator = true;
            this._ChangeValidatorMessages(errorMessage, errorText, validationGroup);
            this._ShowSelectMessage = showSelectItemMessage;
            this._ChangeSelectItemMessage(selectItemMessage._Message, selectItemMessage._Value);
            this._ShowNotFoundMessage = showItemNotFoundMessage;
            this._ChangeItemNotFoundMessage(itemNotFoundMessage._Message, itemNotFoundMessage._Value);
        }

        /// <summary>
        /// Configurações:
        /// - Label:
        ///     Visible = [parâmetro visibleRotulo]
        ///     Text = [parâmetro rotulo]
        /// - CompareValidator:
        ///     Enable = true
        ///     ErroMessage = [parâmetro errorMessage]
        ///     Text = [parâmetro errorText]
        ///     ValidationGroup = [parâmetro validationGroup]
        /// - Combo:
        ///     AppendDataBoundItems = true
        ///     Item zero quando houver dados para seleção e parâmetro showSelectItemMessage = true:
        ///         Text = "[parâmetro selectItemMessage]"
        ///         value: "[parâmetro selectItemMessage]"
        ///     Qdo não houver item para seleção e parâmetro showItemNotFoundMessage = true:
        ///         Text = "[parâmetro itemNotFoundMessage]"
        ///         value: "[parâmetro itemNotFoundMessage]"
        /// </summary>
        /// <param name="visibleRotulo">Se mostra ou não o nome do campo</param>
        /// <param name="rotulo">Nome do campo</param>
        /// <param name="errorMessage">Mensagem de erro do validator</param>
        /// <param name="errorText">Texto exibido no validator</param>
        /// <param name="validationGroup">Grupo de controles para validação</param>
        /// <param name="itemNotFoundMessage">Objeto com a mensagem e valor para nenhum item a selecionar</param>
        /// <param name="showItemNotFoundMessage">Mostra ou não a mensagem de não há item a selecionar</param>
        /// <param name="selectItemMessage">Objeto com a mensagem e valor para a seleção de um item</param>
        /// <param name="showSelectItemMessage">Se mostra mensagem de selecione um item para o usuário ou não</param>
        /// <param name="setaValidator">Indica se a propriedade _EnableValidator terá o valor padrão "true" ou não será setada no inicialize</param>
        public void Inicialize(string rotulo, string errorMessage, string errorText, string validationGroup, bool showSelectItemMessage, UCComboSelectItemMessage selectItemMessage, bool showItemNotFoundMessage, UCComboItemNotFoundMessage itemNotFoundMessage, bool setaValidator)
        {
            this._ChangeLabel = rotulo;
            if (setaValidator)
                this._EnableValidator = true;
            this._ChangeValidatorMessages(errorMessage, errorText, validationGroup);
            this._ShowSelectMessage = showSelectItemMessage;
            this._ChangeSelectItemMessage(selectItemMessage._Message, selectItemMessage._Value);
            this._ShowNotFoundMessage = showItemNotFoundMessage;
            this._ChangeItemNotFoundMessage(itemNotFoundMessage._Message, itemNotFoundMessage._Value);
        }

        #endregion Label/Validator/SelectedItem/ItemNotFound

        #endregion INICIALIZES
    }
}