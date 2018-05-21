using Common.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Domain.Base
{
    public class DomainBase
    {
        protected ValidationSpecificationResult _validationResult;

        protected ConfirmEspecificationResult _validationConfirm;

        protected WarningSpecificationResult _validationWarning;

        [NotMapped]
        public string AttributeBehavior { get; protected set; }

        [NotMapped]
        public string ConfirmBehavior { get; protected set; }

        public void SetConfirmBehavior(string value)
        {
            this.ConfirmBehavior = value;
        }

        public void SetAttributeBehavior(string value)
        {
            this.AttributeBehavior = value;
        }

        public virtual ValidationSpecificationResult GetDomainValidation(FilterBase filters = null)
        {
            return this._validationResult;
        }
        public virtual ConfirmEspecificationResult GetDomainConfirm(FilterBase filters = null)
        {
            return this._validationConfirm;
        }
        public virtual WarningSpecificationResult GetDomainWarning(FilterBase filters = null)
        {
            return this._validationWarning;
        }

        public virtual void SetDomainError(string error)
        {
            this.SetDomainValidation(new ValidationSpecificationResult
            {
                IsValid = false,
                Message = error,
                Errors = new List<string> { error },
            });
        }

        public virtual void SetDomainValidation(ValidationSpecificationResult value)
        {
            this._validationResult = value;
        }
        public virtual void SetDomainConfirm(ConfirmEspecificationResult value)
        {
            this._validationConfirm= value;
        }
        public virtual void SetDomainWarning(WarningSpecificationResult value)
        {
            this._validationWarning = value;
        }

    }
}
