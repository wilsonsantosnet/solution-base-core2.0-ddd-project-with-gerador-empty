using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public static class HelperControlHtmlAngular20
    {

        #region Form Fields

        public static string MakeInputHtml(Context configContext, TableInfo tableInfo, Info info, bool isStringLengthBig, string propertyName)
        {

            var _isPassword = HelperFieldConfig.IsPassword(tableInfo, propertyName);
            var _isPasswordConfirmation = HelperFieldConfig.IsPasswordConfirmation(tableInfo, propertyName);
            var _isEmail = HelperFieldConfig.IsEmail(tableInfo, propertyName);
            var _isRequired = info.IsNullable == 0;


            var required = _isRequired ? "required" : string.Empty;
            var attr = DefineAttr(configContext, tableInfo, info, propertyName);

            if (isStringLengthBig)
                return MakeTextArea(required);

            if (_isPassword)
                return MakeInputType(required, "password", info.Type);

            if (_isPasswordConfirmation)
                return MakeInputPasswordConfirmation(required, "password");

            if (_isEmail)
                return MakeInputType(required, "email", "email");


            if (info.PropertyName.ToLower().Contains("email"))
                return MakeInputType(required, "email", "email");

            if (info.PropertyName.ToLower().Contains("cep") ||
                info.PropertyName.ToLower().Contains("zipcode"))
                return MakeCEP(_isRequired, attr);

            if (info.PropertyName.ToLower().Contains("cpfcnpj") ||
                info.PropertyName.ToLower().Contains("cpf_cnpj") ||
                info.PropertyName.ToLower().Contains("cpf-cnpj"))
                return MakeCPFCNPJTab(required, attr);

            return MakeInputType(required, "text", attr);

        }

        private static string DefineAttr(Context configContext, TableInfo tableInfo, Info info, string propertyName)
        {

            var mask = string.Empty;

            if (configContext.ApplyMasksDefault)
            {
                mask = DefineMask(info.Type);

                if (info.PropertyName.ToLower().Contains("cpf"))
                    mask = "[textMask]='{ mask: vm.masks.maskCPF }'";

                if (info.PropertyName.ToLower().Contains("cnpj"))
                    mask = "[textMask]='{ mask: vm.masks.maskCNPJ }'";

                if (info.PropertyName.ToLower().Contains("telefone") ||
                    info.PropertyName.ToLower().Contains("phone"))
                    mask = "[textMask]='{ mask: vm.masks.maskTelefone }'";

                if (info.PropertyName.ToLower().Contains("celular") ||
                    info.PropertyName.ToLower().Contains("cellphone"))
                    mask = "[textMask]='{ mask: vm.masks.maskCelular }'";
            }

            var maxLength = "";
            if (info.Type == "string")
                maxLength = " maxlength=" + info.Length;

            var attrFieldConfig = HelperFieldConfig.GetAttr(tableInfo, propertyName);
            if (attrFieldConfig.Contains("maskm")) mask = "";
            if (attrFieldConfig.Contains("textMask")) mask = "";
            if (attrFieldConfig.Contains("maski")) mask = "";


            var attr = string.Format("{0} {1} {2}", attrFieldConfig, mask, maxLength);
            return attr;
        }

        private static string MakeInputType(string required, string type, string attr = "")
        {
            return @"    <div class='form-group'>
      <label>{{ vm.infos | traduction:'<#propertyName#>' }}</label>
      <input type='" + type + @"' class='form-control' [(ngModel)]='vm.model.<#propertyName#>' name='<#propertyName#>' " + required + @" formControlName='<#propertyName#>' " + attr + @" autocomplete='off'/>
    </div>";
        }

        private static string MakeCPFCNPJTab(string required, string attr)
        {
            return @"    <div class='form-group'>
      <label>{{ vm.infos | traduction:'<#propertyName#>' }}</label>
      <tabset>
        <tab heading='Pessoa Física'>
          <input type='text' class='form-control' [(ngModel)]='vm.model.<#propertyName#>' name='<#propertyName#>' formControlName='<#propertyName#>' [textMask]='{mask: vm.masks.maskCPF}' " + required + @" " + attr + @" />
        </tab>
        <tab heading='Pessoa Jurídica'>
          <input type='text' class='form-control' [(ngModel)]='vm.model.<#propertyName#>' name='<#propertyName#>' formControlName='<#propertyName#>' [textMask]='{mask: vm.masks.maskCNPJ}' " + required + @" " + attr + @" />
        </tab>
      </tabset>
    </div>";
        }


        private static string MakeCEP(bool required, string attr)
        {
            return @"<div class='form-group'>
                      <label>{{ vm.infos | traduction:'<#propertyName#>' }}</label>
                      <cep [vm]='vm' [fieldName]=" + "\"'<#propertyName#>'\"" + @"[isRequired]='" + required.ToString().ToLower() + "' " + attr + @"></cep>
                    </div>";
        }

        private static string DefineMask(string dataType)
        {
            if (dataType == "int" || dataType == "int?")
                return "maski='9999999999'";

            if (dataType == "decimal" || dataType == "decimal?")
                return "[textMask]='{ mask: vm.masks.maskDecimal }'";

            if (dataType == "datetime")
                return "maski='99/99/9999 99:99'";

            return "";
        }

        private static string MakeInputPasswordConfirmation(string required, string type)
        {

            var password = @"    <div class='form-group'>
      <label>{{ vm.infos | traduction:'<#propertyName#>' }}</label>
      <input type='" + type + @"' class='form-control' [(ngModel)]='vm.model.<#propertyName#>' name='<#propertyName#>' " + required + @" formControlName='<#propertyName#>'  />
    </div>";

            var confPassword = @"    <div class='form-group'>
      <label *ngIf='vm.model.<#propertyName#> == vm.model.confSenha || vm.model.confSenha == null'>Confirmar {{ vm.infos | traduction:'<#propertyName#>' }}</label>
      <label class='alert alert-danger' *ngIf='vm.model.<#propertyName#> != vm.model.confSenha && vm.model.confSenha != null'>Confirme sua  {{ vm.infos | traduction:'<#propertyName#>' }}</label>
      <input type='" + type + @"' class='form-control'  [(ngModel)]='vm.model.confSenha' name='confSenha' " + required + @" formControlName='confSenha' />
    </div>";

            return string.Format("{0}{1}{2}", password, System.Environment.NewLine, confPassword);
        }

        private static string MakeTextArea(string required, string attr = null)
        {
            return @"    <div class='form-group'>
      <label>{{ vm.infos | traduction:'<#propertyName#>' }}</label>
      <textarea class='form-control'  [(ngModel)]='vm.model.<#propertyName#>' name='<#propertyName#>' " + required + @" rows='5' formControlName='<#propertyName#>' " + attr + @"></textarea>
    </div>";
        }

        public static string MakeCtrl(string ctrl)
        {

            return @"    <div class='form-group'>
      <label>{{ vm.infos | traduction:'<#propertyName#>' }}</label>
      " + ctrl + @"
    </div>";
        }

        public static string MakeCheckbox(bool isRequired, string attr)
        {
            var required = string.Empty;

            return @"    <div class='checkbox pt-4'>
      <label>
          <input type='checkbox'  [(ngModel)]='vm.model.<#propertyName#>' " + required + @" name='<#propertyName#>' formControlName='<#propertyName#>' " + attr + @" /> {{ vm.infos | traduction:""<#propertyName#>"" }}?
      </label>
    </div>";
        }

        public static string MakeRadio(bool isRequired, string attr)
        {
            var required = string.Empty;

            return @"<label>{{ vm.infos | traduction:'<#propertyName#>' }}</label>
<div class='form-group'>
    <label class='radio-inline'><input type='radio' [(ngModel)]='vm.model.<#propertyName#>' " + required + @" name='<#propertyName#>' formControlName='<#propertyName#>' " + attr + @" value='true' [checked]='vm.model.<#propertyName#>' (change)='vm.model.<#propertyName#>=true'> Sim</label>
    <label class='radio-inline'><input type='radio' [(ngModel)]='vm.model.<#propertyName#>' " + required + @" name='<#propertyName#>' formControlName='<#propertyName#>' " + attr + @" value='false' [checked]='!vm.model.<#propertyName#>' (change)='vm.model.<#propertyName#>=false'> Não</label>
</div>";

        }

        public static string MakeDropDown(bool isRequired, string attr)
        {

            var required = isRequired ? "required" : string.Empty;

            return @"    <div class='form-group'>
      <label class='mr-1'>{{ vm.infos | traduction:'<#propertyName#>' }}</label> <#componentNameBtn#>
        <select class='form-control'  name='<#propertyName#>' [(ngModel)]='vm.model.<#propertyName#>' " + required + @" formControlName='<#propertyName#>' datasource [dataitem]=" + "\"'<#ReletedClass#>'\"" + " [fieldFilterName]=\"'<#fieldFilterName#>'\" " + attr + @" ></select>
      </div>" + @"
        <#componentModal#>";
        }

        public static string MakeDropDownSeach(bool isRequired, string attr)
        {
            return MakeDropDown(isRequired, attr);
        }

        public static string MakeTextEditor(bool isRequired)
        {
            var required = isRequired ? "required='true'" : string.Empty;
            var id = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            return MakeTextArea(required, string.Format("id='<#propertyName#>{0}' editorhtml='<#propertyName#>{0}' (editorKeyup)='onEditorKeyup<#propertyName#>($event)'", id));
        }

        public static string MakeTextTag(bool isRequired)
        {
            var required = isRequired ? "required='true'" : string.Empty;
            return @"    <div class='form-group'>
      <label>{{ vm.infos | traduction:'<#propertyName#>' }}</label>
      <tag-custom [ngModel] =" + "\"vm.model.<#propertyName#>\" (tagChange)=\"vm.model.<#propertyName#>=$event\" formControlName='<#propertyName#>' " + required + @"></tag-custom>
    </div>";

        }

        public static string MakeTextEditorBasic(bool isRequired)
        {
            var required = isRequired ? "required='true'" : string.Empty;
            return MakeTextArea(required, "(editorKeyup)='onEditorKeyup<#propertyName#>($event)'");
        }

        public static string MakeTextStyle(bool isRequired)
        {
            var required = isRequired ? "required='true'" : string.Empty;

            return @"    <div class='form-group'>
      <label>{{ vm.infos | traduction:'<#propertyName#>' }}</label>
      <input type='text' class='form-control'  [(ngModel)]='vm.model.<#propertyName#>' [name]='<#propertyName#>' formControlName='<#propertyName#>'" + required + @" />
      <fieldset>
        <legend> Estilos </legend>
        <dl>
          <dt> Alinhar na Esquerda</dt>
          <dd>
            <em class='text-muted'>float:left;</em>
          </dd>
          <dt>Alinhar na Direita</dt>
          <dd>
            <em class='text-muted'>float:right;</em>
          </dd>
          <dt>Alinhar no Centro</dt>
          <dd>
            <em class='text-muted'>display: block;margin: 0 auto;</em>
          </dd>
          <dt>Esconder</dt>
          <dd>
            <em class='text-muted'>display: none;</em>
          </dd>
        </dl>
      </fieldset>
    </div>";
        }

        public static string MakeDatePiker(bool isRequired, string attr)
        {
            var required = isRequired ? "required" : string.Empty;
            var attrCompose = $"{attr} datepicker";
            return MakeInputType(required, "text", attrCompose);
        }

        public static string MakeDateTimePiker(bool isRequired, string attr)
        {
            var required = isRequired ? "required" : string.Empty;
            var attrCompose = $"{attr} datetimepicker";
            return MakeInputType(required, "text", attrCompose);
        }

        public static string MakeUpload(Info info)
        {

            var required = info.IsNullable == 0 ? "required='true'" : string.Empty;
            return "    <upload-custom [(vm)]='vm' [ctrlName]='\"<#propertyName#>\"' [label]='vm.infos | traduction:\"<#propertyName#>\"' [folder]='\"" + info.ClassName.ToLower() + "\"'></upload-custom>";
        }

        #endregion

        #region Filter Fields

        public static string MakeInputFilterHtml(string attr = "")
        {

            return @"<div class='form-group'>
      <label>{{ vm.infos | traduction:'<#propertyName#>' }}</label>
      <input type='text' class='form-control'  [(ngModel)]='vm.modelFilter.<#propertyName#>' name='<#propertyName#>'" + attr + @" />
    </div>";

        }

        public static string MakeCtrlFilter(string ctrl)
        {
            return @"<div class='form-group'>
      <label>{{ vm.infos | traduction:'<#propertyName#>' }}</label>
      " + ctrl + @"
    </div>";
        }

        public static string MakeDropDownFilter(string attr = "")
        {
            return @"<div class='form-group'>
      <label>{{ vm.infos | traduction:'<#propertyName#>'}}</label>
      <select class='form-control'  name='<#propertyName#>' [(ngModel)]='vm.modelFilter.<#propertyName#>' datasource [dataitem]=" + "\"'<#ReletedClass#>'\"" + " [fieldFilterName]=\"'<#fieldFilterName#>'\"" + attr + @"></select>
    </div>";

        }

        public static string MakeDatePikerFilter(string propertyName = "<#propertyName#>")
        {
            return MakeInputFilterHtml("datepicker");
        }

        public static string MakeCheckboxFilter()
        {
            return @"<div class='checkbox'>
      <label>
          <input type='checkbox'  [(ngModel)]='vm.modelFilter.<#propertyName#>' name='<#propertyName#>' /> {{ vm.infos | traduction:'<#propertyName#>' }}?
      </label>
    </div>";
        }

        public static string MakeRadioFilter()
        {
            return @"<label>{{ vm.infos | traduction:'<#propertyName#>' }}</label>
<div class='form-group'>
    <label class='radio-inline'><input type='radio' name='<#propertyName#>' [(ngModel)]='vm.modelFilter.<#propertyName#>'> Todos</label>
    <label class='radio-inline'><input type='radio' name='<#propertyName#>' [(ngModel)]='vm.modelFilter.<#propertyName#>' value='true'> Sim</label>
    <label class='radio-inline'><input type='radio' name='<#propertyName#>' [(ngModel)]='vm.modelFilter.<#propertyName#>' value='false'> Não</label>
</div>";
        }

        public static string MakeMultiSelectFilter()
        {
            return @"<div class='form-group'>
        <label>{{ vm.infos | traduction:'<#propertyName#>' }}</label>
        <multiselect [dataitem]=""'<#ReletedClass#>'"" [ctrlName]=""'collection<#ReletedClass#>'""  [vm]=""vm""></multiselect>
    </div>";
        }

        public static string MakeDropDownSeachFilter()
        {

            return MakeDropDownFilter();
        }

        #endregion

    }
}
