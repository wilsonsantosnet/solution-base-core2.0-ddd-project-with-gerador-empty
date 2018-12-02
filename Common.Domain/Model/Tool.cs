using Common.Domain.Enums;
using System;

namespace Common.Domain.Model
{
    public class Tool
    {

        public Tool()
        {
            this.CanRead = true;
            this.CanDelete = true;
            this.CanEdit = true;
            this.CanSave = true;
        }

        public string Name { get; set; }
        public string Icon { get; set; }
        public string Route { get; set; }
        public string Key { get; set; }
        public string ParentKey { get; set; }
        public ETypeTools Type { get; set; }
        public Boolean CanRead { get; set; }
        public Boolean CanDelete { get; set; }
        public Boolean CanEdit { get; set; }
        public Boolean CanSave { get; set; }

        public bool CanWrite { get => this.CanEdit && this.CanSave; }


    }

    public static class ExtensionTools
    {

        public static Tool ReadOnly(this Tool source)
        {

            source.CanDelete = false;
            source.CanEdit = false;
            source.CanSave = false;
            source.CanRead = true;

            return source;
        }

        public static Tool EditOnly(this Tool source)
        {
            source.CanDelete = false;
            source.CanSave = false;
            source.CanEdit = true;
            source.CanRead = true;

            return source;
        }

        public static Tool CreateOnly(this Tool source)
        {
            source.CanDelete = false;
            source.CanSave = true;
            source.CanEdit = false;
            source.CanRead = false;

            return source;
        }

    }
}
