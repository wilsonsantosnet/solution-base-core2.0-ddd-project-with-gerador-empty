using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Gen
{
    public enum NavigationType { 
        
        Instance,
        Collettion
    
    }
    public class Info
    {
        public string FieldFilterDefault { get; set; }

        public string Table { get; set; }

        public string ClassName { get; set; }

        public string PropertyName { get; set; }

        public string DateTimeComparation { get; set; }

        public string ColumnName { get; set; }

        public string Length { get; set; }

        public int IsKey { get; set; }

        public int IsNullable { get; set; }

        public string Type { get; set; }

        public string TypeOriginal { get; set; }

        public string TypeCustom { get; set; }

        public string Module { get; set; }

        public string Namespace { get; set; }

        public string NamespaceApp { get; set; }

        public string NamespaceDto { get; set; }

        public string PropertyNameFk { get; set; }

        public string PropertyNamePk { get; set; }

        public NavigationType NavigationType { get; set; }

        public string HtmlComponent { get; set; }

        public int Order { get; set; }

        public int OrderFilter { get; set; }

        public Group Group { get; set; }

        public bool ShowFieldIsKey { get; set; }

        public bool RelationOneToOne { get; set; }

        public string CameCasingManual { get; set; }

        public List<GroupComponent> GroupComponents { get; set; }

    }
}
