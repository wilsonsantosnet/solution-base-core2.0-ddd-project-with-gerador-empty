# seed-core-ddd-project-with-gerador-empty
Seed vazio para projetos  SPA / DDD / Gerador

1-) Clonar Esse Rep na pasta C:\Projetos (git clone https://github.com/wilsonsantosnet/solution-base-core2.0-ddd-project-with-gerador-empty.git)

1-) Crie um banco de dados com o script da pasta \Projetos\seed-core-ddd-project-with-gerador-empty\Gerador.Gen\Scripts\Sample.Seed.sql

2-) abrir solution seed.sln

3-) compilar projeto

4-) Conferir arquivo ConfigExternalResources no greador para ver quais repositórios serão clonados, e em quais pastas os arquivos serão copiados

5-) Clicar com botão direito no projeto de gerador e rodar em debug

6-) Escolher a opção 1 (clonar e copiar para aplicação)

7-) Escolher o aopção 9 e renomear o projeto, feche a solution e a abra novamente sem salvar a solution ao fechar

8-) No projeto Greador.Gen Mostar Todos os Arquivos 

9-) Incluir a pasta template no projeto

10-) Selecionar todos os aquivos da pasta Back e Front, clicar com botão direito , opção property e marcar para Copiar Sempre (Copy Always)

11-) Compilar

12-) abrir prompt de comando entrar na pasta Seed.Spa.UI rodar npm install

13-) no gerador configurar a classe ConfigContext com as tabelas que serão geradas , veja o exemplo de Contexto no final das instruções

14-) Configurar connection string no gerador app.config apontando para o seu banco. 

15-) Verifica no arquivo App.Config os caminhos onde serão gerador os arquivos de Back e front variaves de appSettings

16-) Rodar gerador opção 3 (gerar código)

17-) no projeto Seed.Api pasta Services configurar a connectionstring do arquivo appsettings.json

18-) configure o ClientID na arquivo /src/app/global.service.ts, na classe chamanda AuthSettings, essa propriedade deve conter o valor  da propriedade ClientId, do arquivo Config.cs do projeto de Sso.Server.Api da pata SSO\Auth , o cliente que deve ser usado é de fluxo implicit flow

19-) No projeto de Seed.API da pasta Services no arquivo  Program descomentar  essa linha ".UseStartup<Startup>()"

20-) No projeto de Sso.Server.Api da pata SSO\Auth no arquivo UserCredentialServices descomentar código de autenticação defualt e retira o throw

21-) No projeto de Sso.Server.Api da pata SSO\Auth no arquivo Startup.cs na linha AddIdentityServer , remover o ponto e virgula e descomentar as linhas baixo

22-) o método AddSigningCredential carrega um certificadigital auto assinado contido na pasta pfx, verifique se esse aquivo existe, ou utilize um certificado  que desejar.

23-) Clicar com botão direito na Solution , item propertys, startup Project , escolher Multiple Startup Project e marcar como start os projetos de Seed.Api / Sso.Server.Api

24-) entra na pasta Seed.Spa.Ui e rodar no prompt de comando ng serve --open

25-) agora pode criar Outras tabelas , Alterar tabelas existentes que o gerador vai atualizar toda as Stack do projeto


Observe o Diagrama Abaixo ele demostra quais peças existem no gerador.

https://drive.google.com/file/d/1qE6RSNoJCipIbQMYFmT41_Y7GXW2WXds/view

# EXEMPLO CONFIG.CONTEXT 
### 1-) CRUD com customização de Campos, Component Basico sem back 

```
        private Context ConfigContextDefault()
        {
            var contextName = "Seed";

            return new Context
            {

                ConnectionString = ConfigurationManager.ConnectionStrings["Seed"].ConnectionString,

                Namespace = "Seed",
                ContextName = contextName,
                ShowKeysInFront = false,
                LengthBigField = 250,
                OverrideFiles = true,
                UseRouteGuardInFront = true,

                OutputClassDomain = ConfigurationManager.AppSettings[string.Format("outputClassDomain")],
                OutputClassInfra = ConfigurationManager.AppSettings[string.Format("outputClassInfra")],
                OutputClassDto = ConfigurationManager.AppSettings[string.Format("outputClassDto")],
                OutputClassApp = ConfigurationManager.AppSettings[string.Format("outputClassApp")],
                OutputClassApi = ConfigurationManager.AppSettings[string.Format("outputClassApi")],
                OutputClassFilter = ConfigurationManager.AppSettings[string.Format("outputClassFilter")],
                OutputClassSummary = ConfigurationManager.AppSettings[string.Format("outputClassSummary")],
                OutputAngular = ConfigurationManager.AppSettings["OutputAngular"],
                OutputClassSso = ConfigurationManager.AppSettings["OutputClassSso"],
                OutputClassCrossCustingAuth = ConfigurationManager.AppSettings["OutputClassCrossCustingAuth"],

                Arquiteture = ArquitetureType.DDD,
                CamelCasing = true,
                MakeFront = true,
                AlertNotFoundTable = true,
                MakeToolsProfile = true,

                Routes = new List<RouteConfig> {
                    new RouteConfig{ Route = "{ path: 'sampledash',  canActivate: [AuthGuard], loadChildren: './main/sampledash/sampledash.module#SampleDashModule' }" }
                },
                
                TableInfo = new UniqueListTableInfo
                {
                   new TableInfo().FromTable("Sample").MakeBack().MakeFront().AndConfigureThisFields(new List<FieldConfig> {
                       new FieldConfig
                       {
                           Name = "Valor",
                           Attributes = new List<string>{ "[textMask]='{mask: vm.masks.maskMoney}'" }
                       }
                   }),
                   new TableInfo().FromTable("SampleType").MakeBack().MakeFront(),
                   new TableInfo().FromClass("SampleDash").MakeFrontBasic(),
                }
            };
        }
```

### 2-) Método novo na controller 
```
.AndConfigureThisMethods(new List<MethodConfig>{
                        new MethodConfig
                        {
                            SignatureControllerTemplate = "ImportNew(string folder, System.Collections.Generic.ICollection<Microsoft.AspNetCore.Http.IFormFile> files)",
                            SignatureAppTemplate = "ImportNew(string rootPath, string folder,System.Collections.Generic.ICollection<Microsoft.AspNetCore.Http.IFormFile> files)",
                            ParameterReturn = "new System.Collections.Generic.List<ParticipantScoreDto>()",
                            CallTemplate = "ImportNew(this._env.ContentRootPath, folder, files)",
                            Dto = "System.Collections.Generic.List<ParticipantScoreDto>",
                            Route = "[HttpPost(\"ImportNew\")]",
                            Verb = "[HttpPost(\"Post\")]"
                        },
                    })
```
### 3-) Configuração do Crud para exibir os Campos em Abas (Grupos)
```
 new TableInfo().FromTable("CmsData").MakeBack().MakeFront()
                    .AndConfigureThisFields(new List<FieldConfig>{
                        new FieldConfig {
                            Name = "Title",
                            Order = 1,
                            Group = new Group("Geral","fa fa-table"),
                        },
                        new FieldConfig {
                            Name = "BodyText",
                            Order = 1,
                            Group = new Group("Geral","fa fa-table"),
                        }}
                    )
```
### 4-) Configuração do Crud para exibir um sub cadastro
```
 .AndConfigureThisGroups(new List<GroupComponent>() {
                        new GroupComponent("Page","fa fa-table","app-page","Page").MakeTagToGroup("CmsDataId")
  }),
```
### 5-) Configuração de Exibição de Campos
```
new TableInfo().FromTable("Carteira").MakeBack().MakeFront()
                       .AndConfigureThisFields(new List<FieldConfig>
                       {
                           new FieldConfig
                           {
                               Name="AssinanteId",
                               Edit = false,
                               Create = false,
                               Filter = false,
                               List = false,
                               Details = true
                           },
                           new FieldConfig
                           {
                               Name="Recomendacoes",
                               Edit = false,
                               Create = false,
                               Filter = true,
                               List = false,
                               Details = true
                           }
                       }),
```
### SQl para Obter todas as Tabelas do banco
```
Select 'new TableInfo().FromTable("'+ name + '").MakeBack().MakeFront(),'   from sys.objects where type = 'u'
and name <> 'sysdiagrams'
```
-- PRÉ REQUISITOS;

1-) git shell [https://git-for-windows.github.io/]

2-) node.js [https://nodejs.org/en/])

3-) npm install -g @angular/cli

3-) opcional [Conemu [https://www.fosshub.com/ConEmu.html/ConEmuSetup.161206.exe]]

5-) instalar .net core 2.0.X [https://www.microsoft.com/net/download/windows]

5.1-) instalar SDK installer [https://www.microsoft.com/net/download/thank-you/dotnet-sdk-2.1.200-windows-x64-installer]


