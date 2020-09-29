using System;
using System.Collections.Generic;

namespace Common.Gen
{
    public class TemplateField
    {
        public string TemplateName { get; set; }
        public string TagTemplate { get; set; }
    }


    public class TemplateClass
    {
        public string TemplateName { get; set; }
        public string TagTemplate { get; set; }
        public ETypeTemplateClass TypeTemplateClass { get; set; }
    }

    public class ConfigExecutetemplate
    {

        public ConfigExecutetemplate()
        {
            this.OverrideFile = true;
            this.TemplateFields = new List<TemplateField>();
            this.TemplateClassItem = new List<TemplateClass>();
            this.WithRestrictions = true;
            this.Layer = ELayer.Back;
        }

        public ELayer Layer { get; set; }
        public TableInfo TableInfo { get; set; }
        public Context ConfigContext { get; set; }
        public IEnumerable<Info> Infos { get; set; }
        public string PathOutput { get; set; }
        public string Template { get; set; }
        public List<TemplateField> TemplateFields { get; set; }
        public EOperation Operation { get; set; }
        public EFlowTemplate Flow { get; set; }
        public List<TemplateClass> TemplateClassItem { get; set; }
        public bool OverrideFile { get; set; }
        public bool WithRestrictions { get; set; }
        public Func<Context, string, string> ExecuteProcess { get; set; }
        public bool DisableGeneration { get; set; }


    }
}
