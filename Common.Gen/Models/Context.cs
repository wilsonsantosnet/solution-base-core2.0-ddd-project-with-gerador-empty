using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Common.Gen.Helpers;

namespace Common.Gen
{
    public enum ArquitetureType
    {

        TableModel,
        DDD,
        TransactionScript,
        ReadOnly

    }
    public class Context
    {

        public Context()
        {
            this.Arquiteture = ArquitetureType.TableModel;
            this.ClearAllFiles = false;
            this.MakeNavigationPropertys = false;
            this.DeleteFilesNotFoundTable = false;
            this.MakeFront = false;
            this.MakeBack = true;
            this.TwoCols = false;
            this.LengthBigField = 150;
            this.OverrideFiles = false;
            this.ApplyMasksDefault = true;
            this.ShowKeysInForms = false;
            this.ShowKeysInGrid = false;
            this.ShowKeysInFront = false;
            this.OverrideFile = new OverrideFile();
            this.DateTimeComparation = "==";
            this.ApiRetrhow = false;
            this.RunOnlyThisClass = false;
            this.UsePathProjects = true;

            if (this.Arquiteture == ArquitetureType.TableModel)
                this.TemplatePathBack = HelperUri.CombineAbsoluteUri(AppDomain.CurrentDomain.BaseDirectory, @"Template\Back");

            if (this.Arquiteture == ArquitetureType.DDD)
                this.TemplatePathBack = HelperUri.CombineAbsoluteUri(AppDomain.CurrentDomain.BaseDirectory, @"Template\Back");

            if (this.Arquiteture == ArquitetureType.TransactionScript)
                this.TemplatePathBack = HelperUri.CombineAbsoluteUri(AppDomain.CurrentDomain.BaseDirectory, @"Template\BackTransaction");

            if (this.Arquiteture == ArquitetureType.ReadOnly)
                this.TemplatePathBack = HelperUri.CombineAbsoluteUri(AppDomain.CurrentDomain.BaseDirectory, @"Template\ReadOnly");

            this.TemplatePathFront = HelperUri.CombineAbsoluteUri(AppDomain.CurrentDomain.BaseDirectory, @"Template\Front");

        }

        private string _namespace;

        private string _module;

        private string _contextName;


        #region propertys


        public bool UsePathProjects { get; set; }
        public bool RunOnlyThisClass { get; set; }

        public string DateTimeComparation { get; set; }

        public OverrideFile OverrideFile { get; set; }

        public bool OverrideFiles { get; set; }

        public bool UseRouteGuardInFront { get; set; }

        public bool ShowKeysInFront { get; set; }

        public bool ShowKeysInForms { get; set; }

        public bool ShowKeysInGrid { get; set; }

        public bool MakeToolsProfile { get; set; }

        public bool ApplyMasksDefault { get; set; }

        public List<TableInfo> TableInfo { get; set; }

        public Dictionary<string, string> DictionaryFields { get; set; }

        public List<RouteConfig> Routes { get; set; }

        public ArquitetureType Arquiteture { get; set; }

        public bool ApiRetrhow { get; set; }

        public string Module
        {
            get
            {
                if (_module == null)
                    return string.Empty;
                return _module;

            }
            set { _module = value; }
        }

        public string ContextName
        {
            get
            {
                if (_contextName == null)
                    return this.Module;

                //var valueSafe = RemoveSpecialCharacters(_contextName);
                return _contextName.Split('.').IsAny() ? _contextName.Split('.').FirstOrDefault() : _contextName;

            }
            set { _contextName = value; }
        }

        public string Namespace
        {
            get
            {

                if (!String.IsNullOrEmpty(_namespace) && !String.IsNullOrEmpty(Module))
                    return string.Format("{0}.{1}", _namespace, Module);

                return _namespace;

            }
            set { _namespace = value; }
        }

        public string NamespaceRoot
        {
            get { return _namespace; }
        }

        public string NamespaceDomainSource
        {
            get
            {

                if (!String.IsNullOrEmpty(_namespace) && !String.IsNullOrEmpty(DomainSource))
                    return string.Format("{0}.{1}", _namespace, DomainSource);

                return _namespace;

            }
        }

        public string TemplatePathBack { get; set; }

        public string TemplatePathFront { get; set; }


        public bool TwoCols { get; set; }

        public int LengthBigField { get; set; }

        public string Company { get; set; }

        public string ProjectName { get; set; }

        public bool ClearAllFiles { get; set; }

        public bool DeleteFilesNotFoundTable { get; set; }

        public bool AlertNotFoundTable { get; set; }

        public bool MakeNavigationPropertys { get; set; }

        public string DomainSource { get; set; }

        public bool MakeFront { get; set; }

        public bool MakeBack { get; set; }

        public bool MakeBackApiOnly { get; set; }

        public bool CamelCasing { get; set; }

        public bool DisabledCamelCasingException { get; set; }

        public bool UpdateFromGit { get; set; }

        public bool DisableRoutesFront { get; set; }

        public string OutputClassSso { get; set; }

        public string OutputClassEnum { get; set; }

        public string OutputClassCrossCustingAuth { get; set; }

        public string OutputAngular { get; set; }

        public string ConnectionString { get; set; }


        public string OutputClassRoot { get; set; }

        public string OutputClassDomain { get; set; }

        public string OutputClassApp { get; set; }

        public string OutputClassUri { get; set; }

        public string OutputClassTestsApp { get; set; }

        public string OutputClassTestsApi { get; set; }

        public string OutputClassApi { get; set; }

        public string OutputClassDto { get; set; }

        public string OutputClassCustom { get; set; }

        public string OutputClassSummary { get; set; }

        public string OutputClassFilter { get; set; }

        public string OutputClassInfra { get; set; }

        #endregion

        private static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }

    }
}
