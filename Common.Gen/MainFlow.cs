using Common.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Common.Gen
{
    public static class HelperFlow
    {
        public static void Flow(string[] args, Func<IEnumerable<ExternalResource>> GetConfigExternarReources, HelperSysObjectsBase sysObject)
        {
            MainFlow.Flow(args, GetConfigExternarReources, sysObject);
        }
    }
    public static class MainFlow
    {

        public enum Eflows
        {

            AtualizarAplicacao = 1,
            AtualizarRepository = 2,
            GerarCodigo,
            GerarCodigoEspecifico,
            AtualizarAplicacaoSemcopiar,
            AbrirLocalDoProjeto,
            AtualizarAplicaoParcialmente,
            GerarEstruturaParaNovoContexto,
            RenomearProjeto,
            RenomearUmaEntidadeDoSistema,
            RenomearEntidadesDoSistema,
            ImportarArquivosDeOutroProjeto,
            LimparDadosSeed,
            ReduzirArquivosParaCrudBasic,
            FixFileNameToExtInApp = 98,
            Sair = 99

        }

        public enum ERepositoy
        {

            template_gerador_back_core20_DDD,
            framework_core20_common,
            template_gerador_front_coreui_angular60,
            framework_angular60_CRUD,
            Seed_layout_front_coreui_angular60

        }

        public static void Flow(string[] args, HelperSysObjectsBase sysObject)
        {
            var executeFlow = FlowOptionsClassic(args, sysObject);

            if (args.FirstOrDefault() != "?")
            {
                if (!executeFlow)
                    PrinstScn.WriteLine("Fluxo Não implementado #");

                Flow(new string[] { "?" }, sysObject);
            }
        }

        public static void Flow(string[] args, Func<IEnumerable<ExternalResource>> GetConfigExternarReources, HelperSysObjectsBase sysObject)
        {

            if (sysObject.DisableCompleteFlow)
            {
                PrinstScn.WriteWarningLine("Fluxo completo foi desabilitado para essa aplicação");
                Flow(args, sysObject);
            }
            else
            {
                var executeFlow = FlowOptions(args, GetConfigExternarReources, sysObject);
                if (args.FirstOrDefault() != "?")
                {
                    if (!executeFlow)
                        PrinstScn.WriteLine("Fluxo Não implementado #");
                }

                Flow(new string[] { "?" }, GetConfigExternarReources, sysObject);
            }

            //MainFlow.Flow(new string[] { }, GetConfigExternarReources, sysObject);

        }

        private static bool FlowOptionsClassic(string[] args, HelperSysObjectsBase sysObject)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            var flow = string.Empty;
            if (args.Length == 0)
            {
                OptionsClassic();
                flow = Console.ReadLine();
            }
            else if (args.FirstOrDefault() == "?")
                OptionsClassic();
            else
                flow = args.FirstOrDefault();

            Console.ForegroundColor = ConsoleColor.White;
            return FlowOptionsClassic(args, sysObject, flow);

        }

        private static bool FlowOptions(string[] args, Func<IEnumerable<ExternalResource>> GetConfigExternarReources, HelperSysObjectsBase sysObject)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            var flow = string.Empty;
            if (args.Length == 0)
            {
                Options();
                flow = Console.ReadLine();
            }
            else if (args.FirstOrDefault() == "?")
            {
                Options();
                flow = Console.ReadLine();
            }
            else
                flow = args.FirstOrDefault();


            var result = FlowOptionsClassic(args, sysObject, flow);

            if (flow == ((int)Eflows.AtualizarAplicacao).ToString())
            {
                PrinstScn.WriteLine("Clonar e Copiar para aplicação");
                HelperExternalResources.CloneAndCopy(GetConfigExternarReources());
                result = true;
            }

            if (flow == ((int)Eflows.ReduzirArquivosParaCrudBasic).ToString())
            {
                PrinstScn.WriteLine("Essa opção deleta diversos arquivos do Front SPA, tem certeza que deseja fazer isso? [S=Sim, N=Não]");
                var confirmDelete = Console.ReadLine();
                if (confirmDelete.ToLower() == "s")
                {
                    HelperCrudBasicDelete.Fix(sysObject);

                    var usePathProjects = confirmDelete;
                    MainWithOutConfirmation(args, usePathProjects.ToLower() == "s", sysObject);
                }
                result = true;
            }

            if (flow == ((int)Eflows.AtualizarAplicaoParcialmente).ToString())
            {
                Console.WriteLine("Qual dos seguintes Projetos você deseja Atualizar:");

                Console.WriteLine("[1 = >> template-gerador-back-core2.0-DDD]");
                Console.WriteLine("[2 = >> framework-core2.0-common]");
                Console.WriteLine("[3 = >> template-gerador-front-coreui-angular6.0]");
                Console.WriteLine("[4 = >> framework-angular6.0-CRUD]");
                Console.WriteLine("[5 = >> Seed-layout-front-coreui-angular6.0]");
                Console.WriteLine("[6 = >> Gerador]");

                var resouceRepositoryNumberRead = Console.ReadLine();
                HelperExternalResources.CloneAndCopy(FilterRepository(GetConfigExternarReources, resouceRepositoryNumberRead));
                result = true;
            }

            if (flow == ((int)Eflows.GerarEstruturaParaNovoContexto).ToString())
            {
                PrinstScn.WriteWarningLine("ATENÇÃO ESSE PROCEDIMENTO VAI BAIXAR DO GIT PROJETOS INICIAIS NAS PASTAS DE OUTPUT DO SEU PROJETO ATUAL. CASO SEU PROJETO AINDA ESTEJA COM O NOME DEFAULT SEED, ELE SERA SOBRESCRITO. AS CONFIGURAÇÕES DE OUTPUT ESTÃO NO APP.CONFIG DO GERADOR. TEM CERTEZA QUE DESEJA FAZER ISSO? (S=SIM/N=NÃO)");
                var continueClony = Console.ReadLine();
                if (continueClony.ToLower() == "s")
                {
                    var projectFiles = HelperExternalResources.CloneAndCopyStructureForNewContext(GetConfigExternarReources());

                    Console.WriteLine("Digite o nome do novo Contexto");
                    var contextName = Console.ReadLine();
                    FixRenameSeed.Fix(sysObject.GetOutputClassRoot(), contextName, false);

                    var seedFile = HelperExternalResources.CopySolutionFile(GetConfigExternarReources(), contextName);

                    var projectFilesFix = FixRenameSeed.FixCollectionFile(projectFiles, contextName);

                    var filesFixContent = new List<string>();
                    filesFixContent.AddRange(projectFilesFix);
                    filesFixContent.Add(seedFile);

                    foreach (var file in filesFixContent)
                        FixRenameSeed.FixContentFile(contextName, file);

                }

                result = true;
            }

            if (flow == ((int)Eflows.AtualizarAplicacaoSemcopiar).ToString())
            {
                PrinstScn.WriteLine("Clonar apenas");
                HelperExternalResources.CloneOnly(GetConfigExternarReources());
                result = true;
            }


            if (flow == ((int)Eflows.AtualizarRepository).ToString())
            {
                PrinstScn.WriteLine("Atualizar repositorio local com arquivos da aplicação");
                HelperExternalResources.UpdateLocalRepository(GetConfigExternarReources());
                result = true;
            }


            if (flow == ((int)Eflows.FixFileNameToExtInApp).ToString())
            {
                PrinstScn.WriteLine("Atualizar nomes dos Arquivos da aplicação para .Ext");
                FixRenameExt.Fix(sysObject);
                result = true;
            }

            if (flow == ((int)Eflows.RenomearProjeto).ToString())
            {
                Console.WriteLine("[1 = >> Apenas Sistema de Arquivos]");
                Console.WriteLine("[2 = >> Sistema de Arquivos e Conteúdo]");

                var Renameprocess = Console.ReadLine();
                if (Renameprocess != "1" && Renameprocess != "2")
                {
                    Console.WriteLine("fluxo Não disponível");
                    return true;
                }
                var replaceinContentFile = Renameprocess == "1" ? false : true;


                PrinstScn.WriteLine("Definir nome do Projeto");
                var projectName = Console.ReadLine();

                FixRenameSeed.Fix(sysObject.GetOutputClassRoot(), projectName, replaceinContentFile);
                result = true;
            }

            if (flow == ((int)Eflows.RenomearUmaEntidadeDoSistema).ToString())
            {
                Console.WriteLine("[1 = >> Apenas Sistema de Arquivos (Ignorar Case)]");
                Console.WriteLine("[2 = >> Sistema de Arquivos e Conteúdo (Considerar Case)]");

                var Renameprocess = Console.ReadLine();
                if (Renameprocess != "1" && Renameprocess != "2")
                {
                    Console.WriteLine("fluxo Não disponível");
                    return true;
                }
                var replaceinContentFile = Renameprocess == "1" ? false : true;

                Console.WriteLine("Digite o nome da Entidade que Dejeja Renomear");
                var termOrigin = Console.ReadLine();

                Console.WriteLine("Digite o novo Nome");
                var termDestination = Console.ReadLine();

                var _fromTo = new Dictionary<string, string> {
                    { termOrigin, termDestination }
                };

                FixRenameFiles.Fix(sysObject.GetOutputClassRoot(), _fromTo, replaceinContentFile);
                result = true;
            }

            if (flow == ((int)Eflows.RenomearEntidadesDoSistema).ToString())
            {
                Console.WriteLine("[1 = >> Apenas Sistema de Arquivos]");
                Console.WriteLine("[2 = >> Sistema de Arquivos e Conteúdo]");

                Console.WriteLine("<<<< Atenção esse processo é Case Sensitive >>>>>");

                var Renameprocess = Console.ReadLine();
                if (Renameprocess != "1" && Renameprocess != "2")
                {
                    Console.WriteLine("fluxo Não disponível");
                    return true;
                }
                var replaceinContentFile = Renameprocess == "1" ? false : true;

                Console.WriteLine("Digite o Caminho do Arquivo , com a relação de Para separado por ;");
                var filePath = Console.ReadLine();
                var content = File.ReadAllLines(filePath);

                var _fromTo = new Dictionary<string, string>();
                foreach (var item in content)
                {
                    _fromTo.Add(item.Split(';').FirstOrDefault(), item.Split(';').LastOrDefault());
                }

                var fronToFixed = PrepareFromTo(_fromTo);

                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var item in fronToFixed)
                    Console.WriteLine($"{item.Key} >> {item.Value}");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Confira as trocas e a ordenação e escolha sim ou não (s=Sim/n=Não)");
                var done = Console.ReadLine();

                if (done == "s")
                    FixRenameFiles.Fix(sysObject.GetOutputClassRoot(), fronToFixed, replaceinContentFile);

                result = true;
            }

            if (flow == ((int)Eflows.ImportarArquivosDeOutroProjeto).ToString())
            {
                Console.WriteLine("Digite a Namespace do projeto de Origem");
                var originNamespace = Console.ReadLine();

                Console.WriteLine("Digite o Caminho do projeto de Origem");
                var originPathRoot = Console.ReadLine();

                HelperMigationToAnotherProject.DoMigation(sysObject, originPathRoot, originNamespace);
                result = true;
            }

            if (flow == ((int)Eflows.LimparDadosSeed).ToString())
            {

                PrinstScn.WriteLine("Limpar Diretorios do Seed");
                FixRenameSeed.ClearEnd(sysObject.GetOutputClassRoot());
                result = true;
            }
            Console.ForegroundColor = ConsoleColor.White;
            return result;
        }

        private static void OptionsClassic()
        {
            Console.WriteLine("Escolha as opções;");
            Console.WriteLine("[{0} = >> Gerar Código ]", (int)Eflows.GerarCodigo);
            Console.WriteLine("[{0} = >> Gerar Código de um Contexto ou Classe Especifica]", (int)Eflows.GerarCodigoEspecifico);
            Console.WriteLine("[{0} = >> Abrir local do projeto", (int)Eflows.AbrirLocalDoProjeto);
            Console.WriteLine("[{0} = >> Sair", (int)Eflows.Sair);
        }


        private static void Options()
        {
            Console.WriteLine("Escolha as opções;");
            Console.WriteLine("[{0} = >> Clonar e Copiar para aplicação]", (int)Eflows.AtualizarAplicacao);
            Console.WriteLine("[{0} = >> Atualizar repositorio local com arquivos da aplicação]", (int)Eflows.AtualizarRepository);
            Console.WriteLine("[{0} = >> Gerar Código ]", (int)Eflows.GerarCodigo);
            Console.WriteLine("[{0} = >> Gerar Código de um Contexto ou Classe Especifica]", (int)Eflows.GerarCodigoEspecifico);
            Console.WriteLine("[{0} = >> Clonar apenas]", (int)Eflows.AtualizarAplicacaoSemcopiar);
            Console.WriteLine("[{0} = >> Abrir local do projeto]", (int)Eflows.AbrirLocalDoProjeto);
            Console.WriteLine("[{0} = >> Clonar e Copiar para aplicação parcialmente]", (int)Eflows.AtualizarAplicaoParcialmente);
            Console.WriteLine("[{0} = >> Clonar e Criar novo Contexto]", (int)Eflows.GerarEstruturaParaNovoContexto);
            //Console.WriteLine("[{0} = >> Setar Arquivos Extensiveis como .ext]", (int)Eflows.FixFileNameToExtInApp);
            Console.WriteLine("[{0} = >> Definir nome do Projeto]", (int)Eflows.RenomearProjeto);
            Console.WriteLine("[{0} = >> Renomear Uma Entidade do Sistema]", (int)Eflows.RenomearUmaEntidadeDoSistema);
            Console.WriteLine("[{0} = >> Renomear Entidades do Sistema]", (int)Eflows.RenomearEntidadesDoSistema);
            Console.WriteLine("[{0} = >> Importar Arquivos de outro Projeto]", (int)Eflows.ImportarArquivosDeOutroProjeto);
            Console.WriteLine("[{0} = >> Limpar Diretorios Sedd]", (int)Eflows.LimparDadosSeed);
            Console.WriteLine("[{0} = >> Reduzir arquivos para Crud basic]", (int)Eflows.ReduzirArquivosParaCrudBasic);
            Console.WriteLine("[{0} = >> Sair", (int)Eflows.Sair);
        }

        private static IDictionary<string, string> PrepareFromTo(Dictionary<string, string> _fromTo)
        {
            var _fromToOriginal = new Dictionary<string, string>();
            var _fromToLower = new Dictionary<string, string>();
            var _fromToAll = new Dictionary<string, string>();

            foreach (var item in _fromTo)
            {
                _fromToOriginal.Add(item.Key, item.Value);
                _fromToLower.Add(item.Key.ToLower(), item.Value.ToLower());
            }

            foreach (var item in _fromToOriginal)
            {
                if (_fromToAll.Where(_ => _.Key == item.Key).IsNotAny())
                    _fromToAll.Add(item.Key, item.Value);
            }

            foreach (var item in _fromToLower)
            {
                if (_fromToAll.Where(_ => _.Key == item.Key).IsNotAny())
                    _fromToAll.Add(item.Key, item.Value);
            }

            var _fromToAllOrdered = _fromToAll.OrderByDescending(_ => _.Key.Length);

            return _fromToAllOrdered.ToDictionary(x => x.Key, x => x.Value);
        }

        private static IEnumerable<ExternalResource> FilterRepository(Func<IEnumerable<ExternalResource>> GetConfigExternarReources, string resouceRepositoryNumberRead)
        {
            Int32.TryParse(resouceRepositoryNumberRead, out int resouceRepositoryNumber);
            if (resouceRepositoryNumber <= 0 || resouceRepositoryNumber > 6)
            {
                PrinstScn.WriteLine("Repositório inexistente");
                return null;
            }

            var repositories = new string[] { "template-gerador-back-core2.0-DDD", "framework-core2.0-common", "template-gerador-front-coreui-angular6.0", "framework-angular6.0-CRUD", "project-base-layout-front-coreui-angular6.0", "solution-base-core2.0-ddd-project-with-gerador-empty" };
            var resouceRepositoryName = repositories[resouceRepositoryNumber - 1];
            PrinstScn.WriteLine(string.Format("Clonar e Copiar parcialmente para aplicação o repositorio: {0}", resouceRepositoryName));
            var result = GetConfigExternarReources().Where(_ => _.ResouceRepositoryName == resouceRepositoryName);

            //if (resouceRepositoryNumber == 6)
            //{
            //    result.SingleOrDefault().OnlyThisFiles = new List<string> { "Common.dll", "Common.Domain.dll", "Common.Gen.dll" };
            //}


            return result;
        }



        private static bool FlowOptionsClassic(string[] args, HelperSysObjectsBase sysObject, string flow)
        {
            var result = false;
            if (flow == ((int)Eflows.GerarCodigo).ToString())
            {
                PrinstScn.WriteLine("Gerar direto na pasta dos projetos? [S=Sim, N=Não]");
                var usePathProjects = Console.ReadLine();

                MainWithOutConfirmation(args, usePathProjects.ToLower() == "s", sysObject);

                result = true;
            }

            if (flow == ((int)Eflows.GerarCodigoEspecifico).ToString())
            {

                if (args.Count() == 3)
                    MainEspecificClass(args, args.LastOrDefault().ToLower() == "s", sysObject);
                else
                {
                    PrinstScn.WriteLine("Gerar direto na pasta dos projetos? [S=Sim, N=Não]");
                    var usePathProjects = Console.ReadLine();
                    MainWithConfirmation(args, usePathProjects.ToLower() == "s", sysObject);
                }

                result = true;
            }

            if (flow == ((int)Eflows.AbrirLocalDoProjeto).ToString())
            {
                HelperCmd.ExecuteCommand(string.Format("explorer {0}", "."), 10000);
                result = true;
            }



            if (flow == ((int)Eflows.Sair).ToString())
            {
                Environment.Exit(0);
                result = true;
            }

            return result;
        }

        private static void MainWithConfirmation(string[] args, bool UsePathProjects, HelperSysObjectsBase sysObject)
        {
            PrinstScn.WriteLine("Atualizando / Criando Contextos ou classes especificas!");
            foreach (var item in sysObject.Contexts)
            {

                PrinstScn.WriteLine("Deseja Atualizar/Criar o Contexto? {0} [S=Sim, N=Não]", item.Namespace);
                var accept = Console.ReadLine();
                if (accept.ToLower() == "s")
                {
                    PrinstScn.WriteLine("Deseja Escolher uma classe, Digite o nome dela?");
                    var className = Console.ReadLine();

                    if (!string.IsNullOrEmpty(className))
                        sysObject.MakeClass(item, className, UsePathProjects);
                    else
                        sysObject.MakeClass(item);
                }

            }

        }


        private static void MainEspecificClass(string[] args, bool UsePathProjects, HelperSysObjectsBase sysObject)
        {
            PrinstScn.WriteLine($"Atualizando / Criando Contextos ou classes especificas! [{args.LastOrDefault()}]");
            foreach (var item in sysObject.Contexts)
            {
                sysObject.MakeClass(item, args[1], UsePathProjects);
            }

        }

        private static void MainWithOutConfirmation(string[] args, bool UsePathProjects, HelperSysObjectsBase sysObject)
        {
            PrinstScn.WriteLine("Atualizando / Criando todos os Contextos");

            foreach (var item in sysObject.Contexts)
            {
                PrinstScn.WriteLine("{0} - {1}", item.Namespace, item.Arquiteture);
                sysObject.MakeClass(item, UsePathProjects);
            }
        }

    }
}
