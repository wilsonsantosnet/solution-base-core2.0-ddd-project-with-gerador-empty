using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public static class DefineTemplateNameVue
    {
        public static string VueIndexComponent(TableInfo tableInfo)
        {
            return "index.vue.template";
        }

        public static string VueFormComponent(TableInfo tableInfo)
        {
            return "form.vue.template";
        }


        public static string VueFilterComponent(TableInfo tableInfo)
        {
            return "filter.vue.template";
        }

        public static string VueRouterComponent(TableInfo tableInfo)
        {
            return "router.js.template";
        }
        

        public static string VueFieldInput(TableInfo tableInfo)
        {
            return "field.input.template";
        }

        public static string VueFieldCheckbox(TableInfo tableInfo)
        {
            return "field.checkbox.template";
        }

        public static string VueFieldDate(TableInfo tableInfo)
        {
            return "field.date.template";
        }

        public static string VueFieldRadio(TableInfo tableInfo)
        {
            return "field.radio.template";
        }

        public static string VueFieldSelect(TableInfo tableInfo)
        {
            return "field.select.template";
        }

        public static string VueFieldHidden(TableInfo tableInfo)
        {
            return "field.hidden.template";
        }


        public static string VueTheadFields(TableInfo tableInfo)
        {
            return "thead.fields.template";
        }

        public static string VueTheadId(TableInfo tableInfo)
        {
            return "thead.Id.template";
        }

        public static string VueTbodyBoolean(TableInfo tableInfo)
        {
            return "tbody.boolean.template";
        }

        public static string VueTbodyDate(TableInfo tableInfo)
        {
            return "tbody.string.template";
        }

        public static string VueTbodyString(TableInfo tableInfo)
        {
            return "tbody.string.template";
        }

        public static string VueTbodyNumber(TableInfo tableInfo)
        {
            return "tbody.number.template";
        }

    }
}
