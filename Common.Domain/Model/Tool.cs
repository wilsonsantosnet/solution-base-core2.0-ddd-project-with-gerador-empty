using Common.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Domain.Model
{
    public class Tool
    {
        private bool canWrite;

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
        public bool CanWrite { get => this.CanEdit && this.CanSave; set => this.SetCanWrite(value); }

        private void SetCanWrite(Boolean value)
        {
            this.CanEdit = value;
            this.CanSave = value;
        }
    }
}
