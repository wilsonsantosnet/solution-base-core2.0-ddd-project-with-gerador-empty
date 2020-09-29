using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{

    public class HtmlCtrl
    {
        public string HtmlField { get; set; }
        public string HtmlFilter { get; set; }

    }

    public static class FieldsHtml
    {

        public static HtmlCtrl Radio(Dictionary<string, string> dataItem, string name)
        {
            var htmlField = RadioMake(dataItem, name.FirstCharToLower(), string.Format("vm.model.{0}", name.FirstCharToLower()), true);
            var htmlFilter = RadioMake(dataItem, name.FirstCharToLower(), string.Format("vm.modelFilter.{0}", name.FirstCharToLower()), false);

            return new HtmlCtrl
            {
                HtmlField = htmlField,
                HtmlFilter = htmlFilter
            };

        }

        public static HtmlCtrl Select(Dictionary<string, string> dataItem, string name)
        {
            var htmlField = SelectMake(dataItem, name.FirstCharToLower(), string.Format("vm.model.{0}", name.FirstCharToLower()), true, "[(ngModel)]");
            var htmlFilter = SelectMake(dataItem, name.FirstCharToLower(), string.Format("vm.modelFilter.{0}", name.FirstCharToLower()), false, "[(ngModel)]");

            return new HtmlCtrl
            {
                HtmlField = htmlField,
                HtmlFilter = htmlFilter
            };

        }

        public static HtmlCtrl SelectJs(Dictionary<string, string> dataItem, string name)
        {
            var htmlField = SelectMakeJs(dataItem, name.FirstCharToLower(), string.Format("vm.Model.{0}", name.FirstCharToLower()), false, "ng-model");
            var htmlFilter = SelectMakeJs(dataItem, name.FirstCharToLower(), string.Format("vm.ModelFilter.{0}", name.FirstCharToLower()), false, "ng-model");

            return new HtmlCtrl
            {
                HtmlField = htmlField,
                HtmlFilter = htmlFilter
            };

        }

        private static string RadioMake(Dictionary<string, string> dataItem, string name, string model, bool isFormControl)
        {
            var html = string.Empty;
            foreach (var item in dataItem)
            {
                if (isFormControl)
                    html += "<div class='radio'><label><input type='radio' name='" + name + "' value='" + item.Key.Replace("'", "") + "' [(ngModel)]='" + model + "' formControlName='" + name + "' [checked]=\"" + model + " == '" + item.Key.Replace("'", "") + "'\"/> " + item.Value + "</label></div>";
                else
                    html += "<div class='radio'><label><input type='radio' name='" + name + "' value='" + item.Key.Replace("'", "") + "' [(ngModel)]='" + model + "' [checked]=\"" + model + " == '" + item.Key.Replace("'", "") + "'\"/> " + item.Value + "</label></div>";
            }

            return html;
        }

        private static string SelectMakeJs(Dictionary<string, string> dataItem, string name, string model, bool isFormControl, string attrNgModel)
        {
            var html = string.Empty;



            var formControl = string.Empty;
            if (isFormControl)
                formControl = "formControlName = '" + name + "'";

            html = "<select class='form-control' name='" + name + "' " + attrNgModel + "='" + model + " '" + formControl + ">";
            foreach (var item in dataItem)
            {
                html += "<option value='" + item.Key.Replace("'", "") + "' ng-selected='{{" + model + "==" + item.Key.Replace("'", "") + "}}' >" + item.Value.Replace("'", "") + "</option>";
            }
            html += "</select>";
            return html;
        }

        private static string SelectMake(Dictionary<string, string> dataItem, string name, string model, bool isFormControl, string attrNgModel)
        {
            var html = string.Empty;

            var formControl = string.Empty;
            if (isFormControl)
                formControl = "formControlName = '" + name + "'";

            html = "<select class='form-control' name='" + name + "' " + attrNgModel + "='" + model + " '" + formControl + ">";
            foreach (var item in dataItem)
            {
                html += "<option value='" + item.Key.Replace("'", "") + "'>" + item.Value.Replace("'", "") + "</option>";
            }
            html += "</select>";
            return html;
        }
    }
    public enum TypeCtrl
    {
        Radio,
        Select,
        SelectJs

    }

    public class RouteConfig
    {
        public string Route { get; set; }
    }


    public class CommandConfig
    {

        public string Name { get; set; }


    }

    public class SpecificationConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public enum SpecificationType
        {

            [Description("Validade persistente Data")]
            ///Validade persistente Data
            Suitable,
            [Description("Validade memory Data")]
            //Validade memory Data
            Consistent,
            [Description("Warning not abort process")]
            //Warning not abort process
            Warning,
        }

        public string Name { get; set; }
        public SpecificationType Type { get; set; }

    }

    public class MethodConfig
    {
        public string SignatureControllerTemplate { get; set; }
        public string SignatureAppTemplate { get; set; }
        public string Verb { get; set; }
        public string CallTemplate { get; set; }
        public string ParameterReturn { get; set; }
        public string Route { get; set; }
        public string Dto { get; set; }

    }

    public class Group
    {
        public Group(string name, string icon)
        {
            this.Name = name;
            this.Icon = icon;
        }
        public string Name { get; set; }
        public string Icon { get; set; }

        public int Order { get; set; }
    }

    public class GroupComponent : Group
    {
        private string _parentIdField;
        private bool _disableChkParent;

        public enum EModal
        {
            Root,
            Basic,
        }

        public GroupComponent(string name, string icon, string selector, string module = "", EModal modal = EModal.Root) : base(name, icon)
        {
            this.ComponentSelector = selector;
            this.ComponentModule = module;
            this.Modal = modal;
            this.LabelModalFieldKeyGenralInfo = "abrir";
        }

        public EModal Modal { get; set; }
        public string ComponentTag { get; set; }
        public string ComponentSelector { get; set; }
        public string ComponentModule { get; set; }
        public bool ShowAfterSelectedItem { get; set; }
        public string LabelModalFieldKeyGenralInfo { get; set; }
        public string ParentIdField { get => GetParentIdField(); }
        public Boolean DisableChkParent { get => this._disableChkParent; }

        private string GetParentIdField()
        {
            return _parentIdField.FirstCharToLower();
        }

        public GroupComponent MakeTagToModalNew(string parentIdField, string attr = "")
        {
            this._parentIdField = parentIdField;
            this.MakeTagToModal(parentIdField, attr);
            return this;
        }

        public GroupComponent MakeTagToModalEdit(string parentIdField, string attr = "")
        {
            this._parentIdField = parentIdField;
            this.MakeTagToModalShowAfterSelectedItem(parentIdField, attr);
            return this;
        }

        public GroupComponent MakeTagToModalShowAfterSelectedItem(string parentIdField, string attr = "", string key = "editarItem")
        {
            this.ShowAfterSelectedItem = true;
            this._parentIdField = parentIdField;
            var attrCompose = string.Format("{0} {1}", string.Format("*ngIf=\"vm.model.{0} && show" + this.GetParentIdField() + "Modal" + key + "\"", this.GetParentIdField()), attr);
            return this.MakeTagToModalChild(this.GetParentIdField(), attrCompose);
        }
        public GroupComponent MakeTagToModal(string parentIdField, string attr = "", string key = "novoItem")
        {
            this._parentIdField = parentIdField;
            this.LabelModalFieldKeyGenralInfo = key;
            this.ComponentTag = string.Format("<{0} *ngIf=\"show" + this.GetParentIdField() + "Modal" + key + "\" [isParent]=\"'true'\" (saveEnd)=\"onSaveEnd($event)\" (backEnd)=\"onBackEnd($event)\" {1} ></{0}>", this.ComponentSelector, attr);
            return this;
        }

        public GroupComponent MakeTagToModalChild(string parentIdField, string attr = "", string key = "editarItem")
        {
            this.LabelModalFieldKeyGenralInfo = key;
            this._parentIdField = parentIdField;
            this.ComponentTag = string.Format("<{0} [isParent]=\"'true'\" (saveEnd)=\"onSaveEnd($event)\" (backEnd)=\"onBackEnd($event)\" [parentIdValue]='vm.model.{1}' [parentIdField]=\"'{1}'\" {2} ></{0}>", this.ComponentSelector, this.GetParentIdField(), attr);

            return this;
        }

        public GroupComponent MakeTagToGroup(string parentIdField, string attr = "", bool disableChkParent = false)
        {
            this._parentIdField = parentIdField;
            this._disableChkParent = disableChkParent;
            this.ComponentTag = string.Format("<{0} [parentIdValue]='vm.model.{1}' [parentIdField]=\"'{1}'\" [isParent]=\"'true'\" <#fieldItemsNavHeadShow#> {2} ></{0}>", this.ComponentSelector, this.GetParentIdField(), attr);
            return this;
        }

        public GroupComponent SetTag(string tag, bool disableChkParent = false)
        {
            this._disableChkParent = disableChkParent;
            this.ComponentTag = tag;
            return this;
        }


    }


    public static class GroupComponentExtension
    {
    }

    public static class FieldConfigExtension
    {
        public static FieldConfig HideAll(this FieldConfig source)
        {
            source.Create = false;
            source.Edit = false;
            source.Details = false;
            source.Filter = false;
            source.List = false;

            return source;

        }
    }

    public class FieldConfig
    {

        public FieldConfig init(TypeCtrl type, Dictionary<string, string> dataItem = null)
        {
            if (dataItem.IsNotNull())
                this.DataItem = dataItem;

            this.TypeCtrl = type;
            switch (this.TypeCtrl)
            {
                case TypeCtrl.Radio:
                    this.HTML = FieldsHtml.Radio(this.DataItem, this.Name);
                    break;
                case TypeCtrl.Select:
                    this.HTML = FieldsHtml.Select(this.DataItem, this.Name);
                    break;
                case TypeCtrl.SelectJs:
                    this.HTML = FieldsHtml.SelectJs(this.DataItem, this.Name);
                    break;
                default:
                    break;
            }

            return this;
        }

        public FieldConfig()
        {
            this.Create = true;
            this.Edit = true;
            this.List = true;
            this.Details = true;
            this.Filter = true;
            this.DataItem = new Dictionary<string, string>();
            this.Upload = false;
            this.Radio = false;
            this.Attributes = new List<string>();
            this.AttributesSection = new List<string>();
            this.AttributesFilters = new List<string>();
        }


        public List<string> Attributes { get; set; }
        public List<string> AttributesSection { get; set; }
        public List<string> AttributesFilters { get; set; }

        public string InsertHtmlAfterSection { get; set; }

        public string Name { get; set; }
        public bool Create { get; set; }
        public bool Edit { get; set; }
        public bool List { get; set; }
        public bool Details { get; set; }
        public bool Filter { get; set; }
        public int Order { get; set; }
        public int OrderFilter { get; set; }
        public TypeCtrl TypeCtrl { get; set; }
        public HtmlCtrl HTML { get; set; }
        public Dictionary<string, string> DataItem { get; set; }
        public bool Upload { get; set; }
        public bool Radio { get; set; }
        public bool SelectSearch { get; set; }
        public bool Tags { get; set; }
        public bool TextEditor { get; set; }
        public bool TextStyle { get; set; }
        public bool Password { get; set; }
        public bool PasswordConfirmation { get; set; }
        public bool Email { get; set; }
        public bool MultiSelectFilter { get; set; }
        public int ColSize { get; set; }
        public bool IgnoreBigLength { get; set; }
        public string TypeCustom { get; set; }
        public Group Group { get; set; }
        public bool ShowFieldIsKey { get; set; }
        public bool RelationOneToOne { get; set; }
        public string CameCasingManual { get; set; }
        public List<GroupComponent> GroupComponents { get; set; }
        public string DateTimeComparation { get; set; }
    }

    /// <summary>
    ///  Define o modelo de exibição dos campos nas telas
    /// </summary>
    public enum FieldConfigShow
    {
        /// <summary>
        ///  ShowAll - Mostra todos e o usuário configura os que devem ser escondidos
        /// </summary>
        ShowAll,
        /// <summary>
        ///  HideAll - Esconde todos e o usuário configura os que devem ser mostrados
        /// </summary>
        HideAll
    }




    public static class TableInfoExtension
    {
        public static TableInfo MakeBack(this TableInfo source)
        {
            source.MakeDomain = true;
            source.MakeApp = true;
            source.MakeDto = true;
            source.MakeCrud = true;
            source.MakeApi = true;
            source.MakeSummary = true;
            return source;
        }
        public static TableInfo DisableFront(this TableInfo source)
        {
            source.MakeFront = false;
            source.MakeFrontCrudBasic = false;
            return source;
        }
        public static TableInfo DisablePrint(this TableInfo source)
        {
            source.MakeFrontCrudNoPrint = true;
            return source;
        }
        public static TableInfo MakeFront(this TableInfo source)
        {
            source.MakeFront = true;
            return source;
        }

        public static TableInfo MakeFrontCrudBasic(this TableInfo source)
        {
            source.MakeFrontCrudBasic = true;
            source.MakeFrontCrudNoPrint = true;
            return source;
        }

        public static TableInfo MakeFrontBasic(this TableInfo source)
        {
            source.MakeFront = true;
            source.MakeFrontBasic = true;
            source.Scaffold = false;
            source.UsePathStrategyOnDefine = false;

            return source;
        }

        public static TableInfo AndDisableAuth(this TableInfo source)
        {
            source.Authorize = false;
            return source;
        }

        public static TableInfo IsAuth(this TableInfo source, Boolean authorize = false)
        {
            source.Authorize = authorize;
            return source;
        }


        public static TableInfo FromTable(this TableInfo source, string tableName)
        {
            source.TableName = tableName;
            return source;
        }

        public static TableInfo FromClass(this TableInfo source, string className)
        {
            source.ClassName = className;
            return source;
        }

        public static TableInfo DefineDefaultGroup(this TableInfo source, Group group)
        {
            source.GroupNameOthers = group;
            return source;
        }

        public static TableInfo AndConfigureThisFields(this TableInfo source, IEnumerable<FieldConfig> fieldsConfig)
        {
            source.FieldsConfig = fieldsConfig.ToList();
            return source;
        }

        public static TableInfo AndConfigureThisGroups(this TableInfo source, IEnumerable<GroupComponent> groups)
        {
            source.GroupComponent = groups.ToList();
            return source;
        }

        public static TableInfo AndConfigureShow(this TableInfo source, FieldConfigShow fieldConfigShow)
        {
            source.FieldsConfigShow = fieldConfigShow;
            return source;
        }

        public static TableInfo AndConfigureThisMethods(this TableInfo source, IEnumerable<MethodConfig> methods)
        {
            source.MethodConfig = methods.ToList();
            return source;
        }

        

        public static TableInfo AndConfigureThisValidations(this TableInfo source, IEnumerable<SpecificationConfig> validations)
        {
            source.SpecificationConfig = validations.ToList();
            return source;
        }

    }

    public class OverrideFile
    {
        public OverrideFile()
        {

        }
        public bool Grid { get; set; }
        public bool ServiceTs { get; set; }
        public bool ComponentHtml { get; set; }
        public bool SubComponentPrintTs { get; set; }
    }

    public class TableInfo
    {
        public enum ENavigation
        {
            Modal,
            NewWindow
        }
        public TableInfo()
        {
            this.CodeCustomImplemented = false;
            this.MovedBaseClassToShared = false;
            this.MakeCrud = false;
            this.MakeApp = false;
            this.MakeApi = false;
            this.MakeDomain = false;
            this.MakeTest = false;
            this.MakeFront = false;
            this.MakeFrontBasic = false;
            this.MakeFrontCrudNoPrint = false;
            this.MakeFrontCrudBasic = false;
            this.Scaffold = true;
            this.TwoCols = false;
            this.Authorize = true;
            this.FieldsConfigShow = FieldConfigShow.ShowAll;
            this.UsePathStrategyOnDefine = true;
            this.IsAuditField = false;
            this.FieldsConfig = new List<FieldConfig>();
            this.CommandConfig = new List<CommandConfig>();
            this.SpecificationConfig = new List<SpecificationConfig>();
            this.GroupNameOthers = new Group("Outros", "fa fa-bars") { Order = -1 };
            this.Navigation = ENavigation.Modal;
            this.Schema = "dbo";
        }

        public ENavigation Navigation { get; set; }

        public Group GroupNameOthers { get; set; }

        public bool IsAuditField { get; set; }

        public bool UsePathStrategyOnDefine { get; set; }

        public string DataItemFieldName { get; set; }

        public FieldConfigShow FieldsConfigShow { get; set; }

        public bool Authorize { get; set; }

        public List<FieldConfig> FieldsConfig { get; set; }

        public List<MethodConfig> MethodConfig { get; set; }

        public List<CommandConfig> CommandConfig { get; set; }

        public List<GroupComponent> GroupComponent { get; set; }

        public List<SpecificationConfig> SpecificationConfig { get; set; }

        public bool InheritQuery { get; set; }

        public bool ModelBase { get; set; }

        public bool ModelBaseWithoutGets { get; set; }

        private string _InheritClassName;

        private string _tableName;

        public string InheritClassName
        {
            get
            {
                return _InheritClassName.IsSent() ? _InheritClassName : this.ClassName;
            }
            set { _InheritClassName = value; }
        }

        public string BoundedContext { get; set; }

        public bool MakeFront { get; set; }

        public bool MakeFrontCrudNoPrint { get; set; }

        public bool MakeFrontCrudBasic { get; set; }

        public bool MakeFrontBasic { get; set; }

        public bool Scaffold { get; set; }

        public string Schema { get; set; }

        public string TableName
        {
            get
            {
                return this._tableName.IsNull() ? this.ClassName : this._tableName;
            }
            set
            {
                this._tableName = value;
            }
        }

        public string ClassNameFormated { get; set; }

        public string ClassName { get; set; }

        public string ToolsName { get; set; }

        public bool IsCompositeKey { get { return Keys != null ? Keys.Count() > 1 : false; } }

        public IEnumerable<string> Keys { get; set; }

        public string KeyName { get { return Keys.IsAny() ? Keys.FirstOrDefault() : string.Empty; } }

        public IEnumerable<string> KeysTypes { get; set; }

        public bool MakeCrud { get; set; }

        public bool MakeTest { get; set; }

        public bool MakeApp { get; set; }

        public bool MakeApi { get; set; }

        public bool MakeDomain { get; set; }

        public bool MakeSummary { get; set; }

        public bool MakeDto { get; set; }

        public bool MakeCustom { get; set; }

        public bool MovedBaseClassToShared { get; set; }

        public bool CodeCustomImplemented { get; set; }

        public IEnumerable<Info> ReletedClass { get; set; }

        public bool TwoCols { get; set; }


        #region Obsolet

        public string ClassNameRigth { get; set; }
        public string TableHelper { get; set; }
        public string LeftKey { get; set; }
        public string RightKey { get; set; }


        #endregion

    }
}
