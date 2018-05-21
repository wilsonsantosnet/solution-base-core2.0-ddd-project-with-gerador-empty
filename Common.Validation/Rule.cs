using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Validation
{
    public class Rule<T>
    {

        private ISpecification<T> _specification;
        private string _message;

        public Rule(ISpecification<T> specification, string message)
        {
            this._specification = specification;
            this._message = message;
        }

        public ISpecification<T> GetSpecification() {
            return _specification;
        }

        public string GetMessage()
        {
            return _message;
        }

    }
}
