using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.API
{


    public class ErrorMap
    {

        public class ErrorDictionary
        {

            public string KeyDefault { get; set; }
            public string KeyCustom { get; set; }
            public string MapError { get; set; }

        }


        protected List<ErrorDictionary> _errorTraductions;
        protected List<ErrorDictionary> _errorTraductionsCustom;

        public ErrorMap()
        {

            this._errorTraductions = new List<ErrorDictionary>();
            this._errorTraductionsCustom = new List<ErrorDictionary>();
            this._errorTraductions.Add(new ErrorDictionary
            {
                KeyDefault = "delete statement conflicted",
                MapError = "Erro ao exluir item, este item contem dados relacionados à ele, antes de excluí-lo, será necessário excluir os dados relacionados."
            });

        }

        private List<ErrorDictionary> GetAllErrorTraductions()
        {
            return this._errorTraductions;
        }

        private List<ErrorDictionary> GetAllErrorTraductionsCustom()
        {
            return this._errorTraductionsCustom;
        }

        public string GetTraduction(string erro)
        {
            var resultDefault = DefaultMap(erro);
            var resultCustom = CustomMap(erro);

            if (resultCustom.IsNotNull())
                return resultCustom;

            return resultDefault;
        }

        private string CustomMap(string erro)
        {
            var resultCustom = default(string);

            var _resultCustom = this.GetAllErrorTraductionsCustom()
             .Where(_ => erro.ToUpper().Contains(_.KeyDefault.ToUpper()))
             .Where(_ => erro.ToUpper().Contains(_.KeyCustom.ToUpper()))
             .SingleOrDefault();

            if (_resultCustom.IsNotNull())
                resultCustom = _resultCustom.MapError;

            return resultCustom;
        }

        private string DefaultMap(string erro)
        {
            var resultDefault = default(string);

            var _resultDefault = this.GetAllErrorTraductions()
             .Where(_ => erro.ToUpper().Contains(_.KeyDefault.ToUpper())).
             SingleOrDefault();

            if (_resultDefault.IsNotNull())
                resultDefault = _resultDefault.MapError;

            return resultDefault;
        }
    }
}
