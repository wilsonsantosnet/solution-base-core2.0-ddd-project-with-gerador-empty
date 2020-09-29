using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Common.Gen
{
    public abstract class HelperSysObjectsBaseFront : HelperSysObjectsBase
    {
        protected string FormFieldReplace(Context configContext, TableInfo tableInfo, Info info, string textTemplateForm, string str = "")
        {

            var attrSectionFieldConfig = HelperFieldConfig.GetAttrSection(tableInfo, info.PropertyName);

            if (IsPropertyNavigationTypeInstance(tableInfo, info.PropertyName))
            {
                if (!attrSectionFieldConfig.Contains("ngIf"))
                {
                    var conditionalParent = "*ngIf=\"!vm.isParent || vm.ParentIdField != '<#propertyName#>'\"";
                    attrSectionFieldConfig += " " + conditionalParent;
                }
            }

            var cols = "col-md-12";

            if (tableInfo.TwoCols || configContext.TwoCols)
                cols = "col-md-6";

            if (IsStringLengthBig(info, configContext))
                cols = "col-md-12";

            var colSize = HelperFieldConfig.GetColSizeField(tableInfo, info.PropertyName);
            if (colSize.IsSent())
                cols = string.Format("col-md-{0}", colSize);

            return textTemplateForm
                .Replace("<#formField#>", str)
                .Replace("<#colformField#>", cols.ToString())
                .Replace("<#attrSection#>", attrSectionFieldConfig);
        }

        public virtual string GroupComponetTransformTagLink(Info info, ConfigExecutetemplate configExecutetemplate)
        {
            if (info.GroupComponents.IsNotAny())
                return string.Empty;

            var pathTemplateComponentGroup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(configExecutetemplate.TableInfo), "field.component.group.tpl");
            var textTemplateComponentGroup = Read.AllText(configExecutetemplate.TableInfo, pathTemplateComponentGroup, this._defineTemplateFolder);

            var component = string.Empty;
            foreach (var GroupComponent in info.GroupComponents)
            {
                var showConditional = string.Empty;
                if (GroupComponent.ShowAfterSelectedItem)
                    showConditional = string.Format("*ngIf=\"vm.model.{0}\"", GroupComponent.ParentIdField);
                else
                    showConditional = string.Format("*ngIf=\"!vm.model.{0}\"", GroupComponent.ParentIdField);


                component += string.Format("<a {3} class=\"m-1\" href=\"#\" data-toggle=\"modal\" (click)=\"$event.preventDefault();{0}Modal.show();show" + GroupComponent.ParentIdField + "Modal" + GroupComponent.LabelModalFieldKeyGenralInfo + "=true;\" title=\"{1}\"><i class=\"{2}\"></i></a>", GroupComponent.ComponentSelector.Replace("-", ""), "{{vm.generalInfo." + GroupComponent.LabelModalFieldKeyGenralInfo + ".label}}", GroupComponent.Icon, showConditional);
            }

            return component;
        }

        public virtual string GroupComponetTransformTagModal(Info info, ConfigExecutetemplate configExecutetemplate)
        {
            if (info.GroupComponents.IsNotAny())
                return string.Empty;

            var modais = string.Empty;
            foreach (var GroupComponent in info.GroupComponents)
            {
                var template = "field.component.group.tpl";
                if (GroupComponent.Modal == GroupComponent.EModal.Basic)
                    template = "field.component.group.basic.tpl";


                var pathTemplateComponentGroup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._defineTemplateFolder.Define(configExecutetemplate.TableInfo), template);
                var textTemplateComponentGroup = Read.AllText(configExecutetemplate.TableInfo, pathTemplateComponentGroup, this._defineTemplateFolder);


                modais += textTemplateComponentGroup
                    .Replace("<#componentId#>", GroupComponent.ComponentSelector.Replace("-", ""))
                    .Replace("<#componentTitle#>", GroupComponent.Name)
                    .Replace("<#componentTag#>", GroupComponent.ComponentTag);

            }

            return modais;

        }

        public override void DefineTemplateByTableInfo(Context config, TableInfo tableInfo) { }

        public override void DefineTemplateByTableInfoFields(Context config, TableInfo tableInfo, UniqueListInfo infos)
        {
            foreach (var item in infos)
            {
                var order = TypeConvertCSharp.OrderByType(item.Type);
                var orderFilter = TypeConvertCSharp.OrderByType(item.Type);

                var group = default(Group);
                var groupComponents = default(List<GroupComponent>);
                var ShowFieldIsKey = false;
                var CameCasingManual = "";
                var RelationOneToOne = false;
                var dateTimeComparation = config.DateTimeComparation;
                var htmlComponent = "";

                if (tableInfo.FieldsConfig.IsAny())
                {

                    var dateTimeComparationConfig = tableInfo.FieldsConfig
                        .Where(_ => _.Name.ToLower() == item.PropertyName.ToString().ToLower())
                        .SingleOrDefault();

                    if (dateTimeComparationConfig.IsNotNull() && dateTimeComparationConfig.DateTimeComparation.IsSent())
                        dateTimeComparation = dateTimeComparationConfig.DateTimeComparation;


                    var orderConfig = tableInfo.FieldsConfig
                        .Where(_ => _.Name.ToLower() == item.PropertyName.ToString().ToLower())
                        .SingleOrDefault();

                    if (orderConfig.IsNotNull() && orderConfig.Order.IsSent())
                        order = orderConfig.Order;


                    if (orderConfig.IsNotNull() && orderConfig.OrderFilter.IsSent())
                        orderFilter = orderConfig.OrderFilter;
                    else
                        orderFilter = int.MaxValue;

                    var groupConfig = tableInfo.FieldsConfig
                        .Where(_ => _.Name.ToLower() == item.PropertyName.ToString().ToLower())
                        .SingleOrDefault();

                    if (groupConfig.IsNotNull() && groupConfig.Group.IsNotNull())
                        group = groupConfig.Group;


                    var groupComponentConfig = tableInfo.FieldsConfig
                       .Where(_ => _.Name.ToLower() == item.PropertyName.ToString().ToLower())
                       .SingleOrDefault();

                    if (groupComponentConfig.IsNotNull() && groupComponentConfig.GroupComponents.IsAny())
                        groupComponents = groupConfig.GroupComponents;


                    var ShowFieldIsKeyConfig = tableInfo.FieldsConfig
                       .Where(_ => _.Name.ToLower() == item.PropertyName.ToString().ToLower())
                       .SingleOrDefault();

                    if (ShowFieldIsKeyConfig.IsNotNull() && ShowFieldIsKeyConfig.ShowFieldIsKey.IsSent())
                        ShowFieldIsKey = groupConfig.ShowFieldIsKey;

                    var RelationOneToOneConfig = tableInfo.FieldsConfig
                      .Where(_ => _.Name.ToLower() == item.PropertyName.ToString().ToLower())
                      .SingleOrDefault();

                    if (RelationOneToOneConfig.IsNotNull() && RelationOneToOneConfig.RelationOneToOne.IsSent())
                        RelationOneToOne = groupConfig.RelationOneToOne;


                    var CameCasingManualConfig = tableInfo.FieldsConfig
                       .Where(_ => _.Name.ToLower() == item.PropertyName.ToString().ToLower())
                       .SingleOrDefault();

                    if (CameCasingManualConfig.IsNotNull() && CameCasingManualConfig.CameCasingManual.IsSent())
                        CameCasingManual = groupConfig.CameCasingManual;

                    var textEditor = tableInfo.FieldsConfig
                      .Where(_ => _.Name.ToLower() == item.PropertyName.ToString().ToLower())
                      .SingleOrDefault();

                    if (textEditor.IsNotNull() && textEditor.TextEditor.IsSent())
                        htmlComponent = "TextEditor";

                    if (textEditor.IsNotNull() && textEditor.Upload.IsSent())
                        htmlComponent = "Upload";


                }


                item.Order = order;
                item.HtmlComponent = htmlComponent;
                item.OrderFilter = orderFilter;
                item.DateTimeComparation = dateTimeComparation;
                item.Group = group;
                item.GroupComponents = groupComponents;
                item.ShowFieldIsKey = ShowFieldIsKey;
                item.CameCasingManual = CameCasingManual;
                item.RelationOneToOne = RelationOneToOne;
            }

            base.CastOrdenabledToUniqueListInfo(infos);
        }

    }
}
