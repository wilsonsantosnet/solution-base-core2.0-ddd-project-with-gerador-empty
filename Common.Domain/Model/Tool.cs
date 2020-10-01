using Common.Domain.Enums;
using System;

namespace Common.Domain.Model
{
    public class Tool
    {

        public Tool()
        {
            this.CanReadAll = true;
            this.CanDelete = true;
            this.CanEdit = true;
            this.CanSave = true;
            this.CanReadOne = true;
            this.CanReadCustom = true;
            this.CanReadDataItem = true;
        }

        public string Name { get; set; }
        public string Icon { get; set; }
        public string Route { get; set; }
        public string Key { get; set; }
        public string ParentKey { get; set; }
        public ETypeTools Type { get; set; }

        public Boolean CanReadDataItem { get; set; }
        public Boolean CanReadOne { get; set; }
        public Boolean CanReadCustom { get; set; }
        public Boolean CanReadAll { get; set; }
        public Boolean CanDelete { get; set; }
        public Boolean CanEdit { get; set; }
        public Boolean CanSave { get; set; }

        public bool CanWrite { get => this.CanEdit && this.CanSave; }


    }

    public static class ExtensionTools
    {

        public static Tool BlockAll(this Tool source)
        {
            source.CanReadAll = false;
            source.CanDelete = false;
            source.CanEdit = false;
            source.CanSave = false;
            source.CanReadOne = false;
            source.CanReadCustom = false;
            source.CanReadDataItem = false;

            return source;
        }
        public static Tool ReadOne(this Tool source)
        {

            source.CanDelete = false;
            source.CanEdit = false;
            source.CanSave = false;
            source.CanReadAll = false;
            source.CanReadOne = true;

            return source;
        }
        public static Tool ReadOnly(this Tool source)
        {

            source.CanDelete = false;
            source.CanEdit = false;
            source.CanSave = false;
            source.CanReadAll = true;
            source.CanReadOne = true;
            source.CanReadDataItem = true;
            source.CanReadCustom = true;

            return source;
        }

        public static Tool EditOnly(this Tool source)
        {
            source.CanDelete = false;
            source.CanSave = false;
            source.CanEdit = true;
            source.CanReadOne = true;
            source.CanReadAll = false;
            source.CanReadCustom = false;
            source.CanReadDataItem = false;

            return source;
        }

        public static Tool CreateOnly(this Tool source)
        {
            source.CanDelete = false;
            source.CanSave = true;
            source.CanEdit = false;
            source.CanReadAll = false;
            source.CanReadOne = true;
            source.CanReadCustom = false;
            source.CanReadDataItem = false;

            return source;
        }

        public static Tool WriteOnly(this Tool source)
        {
            source.CanDelete = false;
            source.CanSave = true;
            source.CanEdit = true;
            source.CanReadAll = false;
            source.CanReadOne = false;
            source.CanReadCustom = false;
            source.CanReadDataItem = false;

            return source;
        }

        public static Tool ReadWrite(this Tool source)
        {

            source.CanDelete = false;
            source.CanSave = true;
            source.CanEdit = true;
            source.CanReadAll = true;
            source.CanReadOne = true;
            source.CanReadCustom = true;
            source.CanReadDataItem = true;

            return source;
        }



        public static Tool SaveOnly(this Tool source)
        {
            source.CanDelete = false;
            source.CanSave = true;
            source.CanEdit = true;
            source.CanReadAll = false;
            source.CanReadOne = true;
            source.CanReadCustom = false;
            source.CanReadDataItem = false;

            return source;
        }

        public static Tool AndCanDelete(this Tool source)
        {
            source.CanDelete = true;
            return source;
        }

        public static Tool AndCanSave(this Tool source)
        {
            source.CanSave = true;
            return source;
        }

        public static Tool AndCanEdit(this Tool source)
        {
            source.CanEdit = true;
            return source;
        }

        public static Tool AndCanReadAll(this Tool source)
        {
            source.CanReadAll = true;
            return source;
        }

        public static Tool AndCanReadOne(this Tool source)
        {
            source.CanReadOne = true;
            return source;
        }

        public static Tool AndCanReadCustom(this Tool source)
        {
            source.CanReadCustom = true;
            return source;
        }

        public static Tool AndCanReadDataItem(this Tool source)
        {
            source.CanReadDataItem = true;
            return source;
        }


    }
}
