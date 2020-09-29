using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public static class HelperControlHtml
    {

        #region Form Fields

        public static string MakeInputHtml(TableInfo tableInfo, Info info, bool isStringLengthBig, string propertyName)
        {
            var ng_required = info.IsNullable == 0 ? "ng-required='true'" : string.Empty;

            var _isPassword = HelperFieldConfig.IsPassword(tableInfo, propertyName);
            var _isPasswordConfirmation = HelperFieldConfig.IsPasswordConfirmation(tableInfo, propertyName);
            var _isEmail = HelperFieldConfig.IsEmail(tableInfo, propertyName);
            var _length = info.Length;



            if (isStringLengthBig)
                return MakeTextArea(ng_required);

            if (_isPassword)
                return MakeInputType(ng_required, "password");

            if (_isPasswordConfirmation)
                return MakeInputPasswordConfirmation(ng_required, "password");

            if (_isEmail)
                return MakeInputType(ng_required, "email");

            var attr = DefineAttr(tableInfo, info, propertyName);

            if (Convert.ToInt32(_length) > 0 && info.Type == "string")
            {
                if (!attr.Contains("maxlength"))
                    attr += " maxlength='" + _length + "'";
            }

            return MakeInputType(ng_required, "text", attr);

        }

        private static string MakeInputType(string ng_required, string type, string attr = null)
        {
            return @"                <div class='form-group'>
                    <label ng-show='vm.formCrud.<#propertyName#>.$valid || vm.formCrud.<#propertyName#>.$pristine'>{{ ::vm.Labels.<#propertyName#> }}</label>
                    <label class='alert alert-danger' ng-show='vm.formCrud.<#propertyName#>.$invalid && vm.formCrud.<#propertyName#>.$dirty'>Campo {{ ::vm.Labels.<#propertyName#> }} é obrigatório</label>
                    <input type='" + type + @"' class='form-control' ng-model='vm.Model.<#propertyName#>' name='<#propertyName#>' " + ng_required + @" attributes-c attributes-container='<#className#>' attributes-field='<#propertyName#>' " + attr + @" />
                </div>";
        }

        private static string MakeInputPasswordConfirmation(string ng_required, string type)
        {

            var password = @"                <div class='form-group'>
                    <label ng-show='vm.formCrud.<#propertyName#>.$valid || vm.formCrud.<#propertyName#>.$pristine'>{{ ::vm.Labels.<#propertyName#> }}</label>
                    <label class='alert alert-danger' ng-show='vm.formCrud.<#propertyName#>.$invalid && vm.formCrud.<#propertyName#>.$dirty'>Campo {{ ::vm.Labels.<#propertyName#> }} é obrigatório</label>
                    <input type='" + type + @"' class='form-control' ng-model='vm.Model.<#propertyName#>' name='<#propertyName#>' " + ng_required + @" attributes-c attributes-container='<#className#>' attributes-field='<#propertyName#>' />
                </div>";

            var confPassword = @"                <div class='form-group'>
                    <label ng-show='vm.Model.<#propertyName#> == vm.Model.confSenha || vm.Model.confSenha == null'>Confirmar {{ ::vm.Labels.<#propertyName#> }}</label>
                    <label class='alert alert-danger' ng-show='vm.Model.<#propertyName#> != vm.Model.confSenha && vm.Model.confSenha != null'>Confirme sua  {{ ::vm.Labels.<#propertyName#> }}</label>
                    <input type='" + type + @"' class='form-control' ng-model='vm.Model.confSenha' name='confSenha' " + ng_required + @" attributes-c attributes-container='<#className#>' attributes-field='<#propertyName#>' />
                </div>";

            return string.Format("{0}{1}{2}", password, System.Environment.NewLine, confPassword);
        }

        private static string MakeTextArea(string ng_required)
        {
            return @"                <div class='form-group'>
                    <label ng-show='vm.formCrud.<#propertyName#>.$valid || vm.formCrud.<#propertyName#>.$pristine'>{{ ::vm.Labels.<#propertyName#> }}</label>
                    <label class='alert alert-danger' ng-show='vm.formCrud.<#propertyName#>.$invalid && vm.formCrud.<#propertyName#>.$dirty'>Campo {{ ::vm.Labels.<#propertyName#> }} é obrigatório</label>
                    <textArea class='form-control' ng-model='vm.Model.<#propertyName#>' name='<#propertyName#>' " + ng_required + @"rows=10 ></textArea>
                </div>";
        }

        public static string MakeCtrl(string ctrl)
        {

            return @"                <div class='form-group'>
                    <label ng-show='vm.formCrud.<#propertyName#>.$valid || vm.formCrud.<#propertyName#>.$pristine'>{{ ::vm.Labels.<#propertyName#> }}</label>
                    <label class='alert alert-danger' ng-show='vm.formCrud.<#propertyName#>.$invalid && vm.formCrud.<#propertyName#>.$dirty'>Campo {{ ::vm.Labels.<#propertyName#> }} é obrigatório</label>
                    " + ctrl + @"
                </div>";
        }

        public static string MakeCheckbox(bool isRequired)
        {
            var ng_required = string.Empty;

            return @"                <div class='checkbox'>
                    <label>
                        <input type='checkbox' ng-model='vm.Model.<#propertyName#>' " + ng_required + @" name='<#propertyName#>' /> {{ ::vm.Labels.<#propertyName#> }}?
                    </label>
                </div>";
        }

        public static string MakeRadio(bool isRequired)
        {
            var ng_required = string.Empty;

            return @"                <div class='option'>
                    <label>
                        {{ ::vm.Labels.<#propertyName#> }}? 
                    </label><br>
                    <input type='radio' ng-model='vm.Model.<#propertyName#>'" + ng_required + @" name='<#propertyName#>' ng-value='true'/> Sim 
                    <input type='radio' ng-model='vm.Model.<#propertyName#>'" + ng_required + @" name='<#propertyName#>' ng-value='false'/> Não 
                </div>";
        }

        public static string MakeDropDown(bool isRequired)
        {
            var ng_required = isRequired ? "ng-required='true'" : string.Empty;

            return @"                <div class='form-group'>
                    <label ng-show='vm.formCrud.<#propertyName#>.$valid || vm.formCrud.<#propertyName#>.$pristine'>{{ ::vm.Labels.<#propertyName#> }}</label>
                    <label class='alert alert-danger' ng-show='vm.formCrud.<#propertyName#>.$invalid && vm.formCrud.<#propertyName#>.$dirty'>Campo {{ ::vm.Labels.<#propertyName#> }} é obrigatório</label>
                    <make-select model='vm.Model.<#propertyName#>' dataitem='<#ReletedClass#>' label='Selecione' name='<#propertyName#>' " + ng_required + @"></make-select>
                </div>";
        }

        public static string MakeDropDownSeach(bool isRequired)
        {
            var ng_required = isRequired ? "ng-required='true'" : string.Empty;

            return @"                <div class='form-group' ng-show='vm.DataItem<#ReletedClass#>.length > 0'>
                    <label ng-show='vm.formCrud.<#propertyName#>.$valid || vm.formCrud.<#propertyName#>.$pristine'>{{ ::vm.Labels.<#propertyName#> }}</label>
                    <label class='alert alert-danger' ng-show='vm.formCrud.<#propertyName#>.$invalid && vm.formCrud.<#propertyName#>.$dirty'>Campo {{ ::vm.Labels.<#propertyName#> }} é obrigatório</label>
                    <ui-select ng-model='vm.selected<#ReletedClass#>' title='Selecione' on-select='vm.onSelectCallback($item, vm," + "\"" + "<#propertyName#>" + "\"" + @")'>
                        <ui-select-match placeholder='Digite para pesquisar...'>{{$select.selected.name}}</ui-select-match>
                        <ui-select-choices repeat='item in (vm.DataItem<#ReletedClass#> | filter: $select.search) track by item.id'>
                            <div ng-bind-html='item.name | highlight: $select.search'></div>
                        </ui-select-choices>
                    </ui-select>
                    <input type='hidden' ng-model='vm.Model.<#propertyName#>' name='<#propertyName#>'" + ng_required + @">
                </div>";
        }

        public static string MakeTextEditor(bool isRequired)
        {
            var ng_required = isRequired ? "ng-required='true'" : string.Empty;

            return @"                   <label>{{ ::vm.Labels.<#propertyName#>}}</label>
                    <fieldset>
                        <text-angular ng-model='vm.Model.<#propertyName#>'>" + @"</text-angular>
                    </fieldset>";
        }

        public static string MakeTextEditorBasic(bool isRequired)
        {
            var ng_required = isRequired ? "ng-required='true'" : string.Empty;

            return @"                   <label>{{ ::vm.Labels.<#propertyName#>}}</label>
                    <fieldset>
                        <text-angular ng-model='vm.Model.<#propertyName#>' ta-required='true' ta-toolbar=" + "\"[['html'],['bold', 'italics'],['h1', 'h2', 'h3', 'h4', 'h5', 'h6', 'p', 'pre', 'quote'],['justifyLeft', 'justifyCenter', 'justifyRight', 'indent', 'outdent']]\">" + @"</text-angular>
                    </fieldset>";
        }

        public static string MakeTextStyle(bool isRequired)
        {
            var ng_required = isRequired ? "ng-required='true'" : string.Empty;


            return @"                <div class='form-group'>
                    <label ng-show='vm.formCrud.<#propertyName#>.$valid || vm.formCrud.<#propertyName#>.$pristine'>{{ ::vm.Labels.<#propertyName#> }}</label>
                    <label class='alert alert-danger' ng-show='vm.formCrud.<#propertyName#>.$invalid && vm.formCrud.<#propertyName#>.$dirty'>Campo {{ ::vm.Labels.<#propertyName#> }} é obrigatório</label>
                    <input type='text' class='form-control' ng-model='vm.Model.<#propertyName#>' name='<#propertyName#>' " + ng_required + @" attributes-c attributes-container='<#className#>' attributes-field='<#propertyName#>' />
                    <fieldset>
                          <legend> Estilos </legend >
                          <dl>
                              <dt> Alinhar na Esquerda</dt>
                                 <dd>
                                     <button class='btn' ngclipboard data-clipboard-text='float:left;' uib-tooltip='copiar'>
                                      <em class='text-muted'>float:left;</em> <i class='fa fa-copy'></i>
                                  </button>
                              </dd>
                              <dt>Alinhar na Direita</dt>
                              <dd>
                                  <button class='btn' ngclipboard data-clipboard-text='float:right;' uib-tooltip='Copiar'>
                                      <em class='text-muted'>float:right;</em> <i class='fa fa-copy'></i>
                                  </button>
                              </dd>
                              <dt>Alinhar no Centro</dt>
                              <dd>
                                  <button class='btn' ngclipboard data-clipboard-text='display: block;margin: 0 auto;' uib-tooltip='Copiar'>
                                      <em class='text-muted'>display: block;margin: 0 auto;</em> <i class='fa fa-copy'></i>
                                  </button>
                              </dd>
                              <dt>Esconder</dt>
                              <dd>
                                  <button class='btn' ngclipboard data-clipboard-text='display: none;' uib-tooltip='Copiar'>
                                      <em class='text-muted'>display: none;</em> <i class='fa fa-copy'></i>
                                  </button>
                              </dd>
                          </dl>
                      </fieldset>
                </div>";




        }

        public static string MakeDatepiker(bool isRequired)
        {
            var ng_required = isRequired ? "ng-required='true'" : string.Empty;

            return @"                <div class='form-group'>
                    <label>{{ ::vm.Labels.<#propertyName#> }}</label>
                    <make-Datepicker model='vm.Model.<#propertyName#>' " + ng_required + @"></make-Datepicker>
                </div>";
        }

        public static string MakeDatetimepiker(bool isRequired)
        {
            var ng_required = isRequired ? "ng-required='true'" : string.Empty;

            return @"                <div class='form-group'>
                    <label>{{ ::vm.Labels.<#propertyName#> }}</label>
                    <p class='input-group'>
                        <make-datetime-picker model='vm.Model.<#propertyName#>' model-isopen='vm.<#propertyName#>isOpen'></make-datetime-picker>
                        <span class='input-group-btn'>
                            <button type='button' class='btn btn-default' ng-click='vm.openCalendar($event, vm," + "\"" + "<#propertyName#>isOpen" + "\"" + @")'><i class='fa fa-calendar'></i></button>
                        </span>
                    </p>
                </div>";
        }

        public static string MakeUpload(Info info)
        {

            var ng_required = info.IsNullable == 0 ? "ng-required='true'" : string.Empty;

            return @"                <div class='form-group'>
                    <label ng-show='vm.formCrud.<#propertyName#>.$valid || vm.formCrud.<#propertyName#>.$pristine'>{{ ::vm.Labels.<#propertyName#> }}</label>
                    <label class='alert alert-danger' ng-show='vm.formCrud.<#propertyName#>.$invalid && vm.formCrud.<#propertyName#>.$dirty'>Campo {{ ::vm.Labels.<#propertyName#> }} é obrigatório</label>
                    <p class='input-group'>
                        <input type = 'text' name='<#propertyName#>' ng-model='vm.Model.<#propertyName#>' class='form-control' placeholder='Selecionar arquivo...' " + ng_required + @">
                        <span class='input-group-btn'>
                            <label class='btn btn-default btn-file'>
                                Procurar<input type='file' ngf-select=""vm.upload($files,vm.Model,'<#propertyName#>')"" multiple>
                            </label>
                            <button class='btn btn-default' type='button' ng-click=""vm.delete(vm.Model.<#propertyName#>, vm.Model,'<#propertyName#>')"">Excluir</button>
                        </span>
                     </p>
                    <a ng-show='vm.Model.<#propertyName#> != null' href='{{vm.uploadUri}}/<#className#>/{{vm.Model.<#propertyName#>}}' target='_blank'><img src='{{vm.uploadUri}}/<#className#>/{{vm.Model.<#propertyName#>}}' style='width: 100px' onerror=""this.onerror = null; this.src = '/images/file.png';"" title='ver'/></a>
                         <p ng-show='vm.Model.<#propertyName#> != null' style='margin-top: 10px'>
                         <button class='btn' ngclipboard data-clipboard-text='{{vm.uploadUri}}/<#className#>/{{vm.Model.<#propertyName#>}}' uib-tooltip='copiar' style='width: 100%'>
                            <em class='text-muted'>{{vm.uploadUri}}/<#className#>/{{vm.Model.<#propertyName#>}}</em> <i class='fa fa-copy'></i>
                        </button>
                    </p>
                </div>";

        }

        private static string DefineAttr(TableInfo tableInfo, Info info, string propertyName)
        {

            var attrFieldConfig = HelperFieldConfig.GetAttr(tableInfo, propertyName);
            var attr = string.Format("{0}", attrFieldConfig);
            return attr;
        }



        #endregion

        #region Filter Fields

        public static string MakeInputFilterHtml()
        {

            return @"<div class='form-group'>
                    <label>{{ ::vm.Labels.<#propertyName#> }}</label>
                    <input type='text' class='form-control' ng-model='vm.crud.ModelFilter.<#propertyName#>' name='<#propertyName#>' attributes-c attributes-container='<#ClassName#>' attributes-field='<#propertyName#>' />
                </div>";

        }

        public static string MakeCtrlFilter(string ctrl)
        {
            return @"<div class='form-group'>
                    <label>{{ ::vm.Labels.<#propertyName#> }}</label>
                    " + ctrl + @"
                </div>";
        }

        public static string MakeDropDownFilter()
        {
            return @"<div class='form-group'>
                    <label>{{ ::vm.Labels.<#propertyName#> }}</label>
                    <make-select model='vm.crud.ModelFilter.<#propertyName#>' dataitem='<#ReletedClass#>' label='Todos' name='<#propertyName#>'></make-select>
                </div>";

        }

        public static string MakeDatapikerFilter(string propertyName = "<#propertyName#>")
        {
            return @"<div class='form-group'>
                    <label>{{ ::vm.Labels.<#propertyName#> }}</label>" +
                    string.Format(@"<make-Datepicker model='vm.crud.ModelFilter.{0}'></make-Datepicker>", propertyName) +
                "</div>";
        }

        public static string MakeCheckboxFilter()
        {

            return @"                <div class='checkbox'>
                    <label>
                        <input type='checkbox' ng-model='vm.crud.ModelFilter.<#propertyName#>' name='<#propertyName#>' /> {{ ::vm.Labels.<#propertyName#> }}?
                    </label>
                </div>";
        }

        #endregion

    }
}
