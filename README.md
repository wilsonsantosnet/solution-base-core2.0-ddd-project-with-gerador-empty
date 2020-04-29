# seed-core-ddd-project-with-gerador-empty
Seed vazio para projetos  SPA / DDD / Gerador

1-) Clonar Esse Rep na pasta C:\Projetos (git clone https://github.com/wilsonsantosnet/solution-base-core2.0-ddd-project-with-gerador-empty.git)

2-) Crie um banco de dados com o script da pasta \Projetos\
solution-base-core2.0-ddd-project-with-gerador-empty\Gerador.Gen\Scripts\Sample.Seed.sql

3-) abrir solution seed.sln

4-) compilar projeto

5-) Conferir arquivo ConfigExternalResources no greador para ver quais repositórios serão clonados, e em quais pastas os arquivos serão copiados

6-) Clicar com botão direito no projeto de gerador e rodar em debug

7-) Escolher a opção 1 (clonar e copiar para aplicação)

8-) Escolher o aopção 9 e renomear o projeto, feche a solution e a abra novamente sem salvar a solution ao fechar

9-) No projeto Greador.Gen Mostar Todos os Arquivos 

10-) Incluir a pasta template no projeto

11-) Selecionar todos os aquivos da pasta Back e Front, clicar com botão direito , opção property e marcar para Copiar Sempre (Copy Always)

12-) Compilar

13-) abrir prompt de comando entrar na pasta Seed.Spa.UI rodar npm install

14-) no gerador configurar a classe ConfigContext com as tabelas que serão geradas , veja o exemplo de Contexto no final das instruções

15-) Configurar connection string no gerador app.config apontando para o seu banco. 

16-) Verifica no arquivo App.Config os caminhos onde serão gerador os arquivos de Back e front variaves de appSettings

17-) Rodar gerador opção 3 (gerar código)

18-) no projeto Seed.Api pasta Services configurar a connectionstring do arquivo appsettings.json

19-) configure o ClientID na arquivo /src/app/global.service.ts, na classe chamanda AuthSettings, essa propriedade deve conter o valor  da propriedade ClientId, do arquivo Config.cs do projeto de Sso.Server.Api da pata SSO\Auth , o cliente que deve ser usado é de fluxo implicit flow

20-) No projeto de Seed.API da pasta Services no arquivo  Program descomentar  essa linha ".UseStartup<Startup>()"

21-) No projeto de Sso.Server.Api da pata SSO\Auth no arquivo UserCredentialServices descomentar código de autenticação defualt e retira o throw

22-) No projeto de Sso.Server.Api da pata SSO\Auth no arquivo Startup.cs na linha AddIdentityServer , remover o ponto e virgula e descomentar as linhas baixo

23-) o método AddSigningCredential carrega um certificadigital auto assinado contido na pasta pfx, verifique se esse aquivo existe, ou utilize um certificado  que desejar.

24-) Clicar com botão direito na Solution , item propertys, startup Project , escolher Multiple Startup Project e marcar como start os projetos de Seed.Api / Sso.Server.Api

25-) entra na pasta Seed.Spa.Ui e rodar no prompt de comando ng serve --open

26-) agora pode criar Outras tabelas , Alterar tabelas existentes que o gerador vai atualizar toda as Stack do projeto

# EXEMPLO CONFIG.CONTEXT 

### 1-) CRUD Completo com customização de Campos, Rota e Component Basico sem back , e Crud mínimo

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
                   new TableInfo().FromTable("SampleType").MakeBack().MakeCrudBasic(),
                   new TableInfo().FromClass("SampleDash").MakeFrontBasic(),
                }
            };
        }
```

### 2-) Configuração para não Sobrepor dados do Menu e raiz dos Componentes Angular

```
        private Context ConfigContextDefault()
        {
            var contextName = "Seed";

            return new Context
            {
                //Sobreescreve apenas os arquivos que tem realação com Campos 
                OverrideFiles = false,
                // Matem os dados Gerados na classe  ProfileCustom da camanda da infra CrossCuting.Auth
                MakeToolsProfile = false,
            };
        }
```

### 3-) Método novo na controller 
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
### 4-) Configuração do Crud para exibir os Campos em Abas (Grupos)
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
### 4.1-) Definir Grupo Padrão
```
                          .AndConfigureThisGroups(new List<GroupComponent>{
                            new GroupComponent("Meta Data", "fa fa-tags","list-value","Content")
                                .SetTag("<list-metadata [(vm)]='vm' [ctrlName]=\"'collectionTaxCouponMetaData'\" [MetadataId]=\"'attributesMetaDataId'\" [MetadataDescription]=\"'attributesMetaData.name'\" [MetadataValue]=\"'value'\" *ngIf='vm.model.TaxCouponId | existsRequest:\"TaxCoupon\" | async'></list-metadata>")
                        }).DefineDefaultGroup(new Group("Incluir Cupons","fa fa-calculator"))
```
### 5-) Configuração do Crud para exibir um sub cadastro
```
 .AndConfigureThisGroups(new List<GroupComponent>() {
                        new GroupComponent("Page","fa fa-table","app-page","Page").MakeTagToGroup("CmsDataId")
  }),
```
### 6-) Configuração de Exibição de Campos
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
### 7-) Configuração de DataItem com RadioButtom
```
 new TableInfo { TableName = "Parceiro", MakeDomain = true, MakeApp = true, MakeDto = true, MakeCrud = true, MakeApi= true, MakeSummary = true , MakeFront = true ,
                    FieldsConfig = new List<FieldConfig>
                        {
                            new FieldConfig()
                            {
                                Name ="tipoPessoaId" ,
                                DataItem = new Dictionary<string, string> {
                                    { "1" ,"PF"},
                                    { "2" ,"PJ"}
                                },
                            }.init(TypeCtrl.Radio),

                            new FieldConfig()
                            {
                                Name ="assinanteId", Edit = false, Create = false , Filter=  false,
                            },
                        }
                    },
```
### 8-) Configuração de DataItem com Select
```
new TableInfo { TableName = "MeioPagamento", MakeDomain = true, MakeApp = true, MakeDto = true, MakeCrud = true, MakeApi= true, MakeSummary = true , MakeFront= true,
                    FieldsConfig = new List<FieldConfig>
                        {
                            new FieldConfig()
                            {
                                Name ="Bandeira" ,
                                DataItem = new Dictionary<string, string> {
                                    { "'visa'" ,"visa"},
                                    { "'mastercard'" ,"mastercard"},
                                    { "'discover'" ,"discover"},
                                    { "'amex'" ,"amex"},
                                },
                            }.init(TypeCtrl.Select),
                            new FieldConfig()
                            {
                                Name ="mesExpiracao" ,
                                DataItem = new Dictionary<string, string> {
                                    { "1" ,"Janeiro"},
                                    { "2" ,"Fevereioro"},
                                    { "3" ,"Março"},
                                    { "4" ,"Abril"},
                                    { "5" ,"Maio"},
                                    { "6" ,"Junho"},
                                    { "7" ,"Julho"},
                                    { "8" ,"Agosto"},
                                    { "9" ,"Setembro"},
                                    { "10" ,"Outubro"},
                                    { "11" ,"Novembro"},
                                    { "12" ,"Dezembro"},
                                },
                            }.init(TypeCtrl.Select),
                            new FieldConfig()
                            {
                                Name ="anoExpiracao" ,
                                DataItem = new Dictionary<string, string> {
                                    { "	2018","2018	"},
                                    { "2019","2019"},
                                    { "2020","2020"},
                                    { "2021","2021"},
                                    { "2022","2022"},
                                    { "2023","2023"},
                                    { "2024","2024"},
                                    { "2025","2025"},
                                    { "2026","2026"},
                                    { "2027","2027"},
                                    { "2028","2028"},
                                    { "2029","2029"},
                                    { "2030","2030"},
                                    { "2031","2031"},
                                    { "2032","2032"},
                                    { "2033","2033"},
                                    { "2034","2034"},
                                    { "2035","2035"},
                                    { "2036","2036"},
                                    { "2037","2037"},
                                    { "2038","2038"},
                                    { "2039","2039"},
                                    { "2040","2040"},
                                    { "2041","2041"},
                                    { "2042","2042"},
                                    { "2043","2043"},
                                    { "2044","2044"},
                                    { "2045","2045"},
                                    { "2046","2046"},
                                    { "2047","2047"},
                                    { "2048","2048"},
                                    { "2049","2049"},
                                    { "2050","2050"},
                                },
                            }.init(TypeCtrl.Select),
```
### 9) Configuração de um campo para Usar um Component 
```
new FieldConfig {
    Name = "DocumentNumber",
    HTML = new HtmlCtrl() {
        HtmlField = "<cpf-cnpj [vm]=\"vm\" [fieldName]=\"'documentNumber'\" [isRequired]=\"true\"></cpf-cnpj>",
        HtmlFilter = "<input type='text' class='form-control' [(ngModel)]='vm.modelFilter.documentNumber' name=\"documentNumber\"/>"
}
```

### 10) Configuração de tabela ManyToMany Exibir Campos no Crud
```
new List<FieldConfig>{
        new FieldConfig { Name = "UserId", ShowFieldIsKey = true },
        new FieldConfig { Name = "RoleId", ShowFieldIsKey  = true },
}
```
### 11) Configuração para fazer upload de arquivos
```
new TableInfo().FromTable("Banner").MakeBack().MakeFront()
    .AndConfigureThisFields(new List<FieldConfig> {
        new FieldConfig {
            Name="imagem",
            Upload = true,
        },
    }),
```
### 12) Configuração para tornar uma controller publica
```
TableInfo = new UniqueListTableInfo
{
   new TableInfo().FromTable("SampleType").MakeBack().MakeFront()
   .AndDisableAuth(),
}
,
```
# DSL Gerar Back e Front
### Sql para obter todas as tabelas do banco e gerar a configuraçõa necessária para geração de codigo do back e do front
```
Select 'new TableInfo().FromTable("'+ name + '").MakeBack().MakeFront(),'   from sys.objects where type = 'u'
and name <> 'sysdiagrams'
```
# EXEMPLO ProfileCustom (Menu)
```
 public static Dictionary<string, object> ClaimsForAdmin()
        {
            var tools = new List<dynamic>
            {

                new Tool { Icon = "fa fa-file", Name = "Account",  Key = "Account" , Type = ETypeTools.Menu },
                new Tool { Icon = "fa fa-edit", Name = "UnityStatus", Route = "/unitystatus", Key = "UnityStatus" , Type = ETypeTools.Menu, ParentKey="Account" },
                new Tool { Icon = "fa fa-edit", Name = "Unity", Route = "/unity", Key = "Unity" , Type = ETypeTools.Menu, ParentKey="Account" },

                new Tool { Icon = "fa fa-file", Name = "Cms",  Key = "Cms" , Type = ETypeTools.Menu },
                new Tool { Icon = "fa fa-edit", Name = "CmsData", Route = "/cmsdata", Key = "CmsData" , Type = ETypeTools.Menu, ParentKey="Cms" },
                new Tool { Icon = "fa fa-edit", Name = "TypeCmsData", Route = "/typecmsdata", Key = "TypeCmsData" , Type = ETypeTools.Menu, ParentKey="Cms" },
                new Tool { Icon = "fa fa-edit", Name = "StatusCmsData", Route = "/statuscmsdata", Key = "StatusCmsData" , Type = ETypeTools.Menu, ParentKey="Cms" },

            };
            var _toolsForAdmin = JsonConvert.SerializeObject(tools);
            return new Dictionary<string, object>
            {
                { "tools", _toolsForAdmin }
            };
        }
```
# MASCARAS 
### DISPONÍVEIS COM REGEX;

```
      maskUF: [/\D/, /\D/,],
      maskCEP: [/\d/, /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/],
      maskCPF: [/\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '-', /\d/, /\d/],
      maskCNPJ: [/\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '/', /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/],
      maskDDD: ['(', /\d/, /\d/, ')'],
      maskOnlyTelefone: [/\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/, /\d/],
      maskTelefone: ['(', /\d/, /\d/, ')', /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/],
      maskCelular: ['(', /\d/, /\d/, ')', /\d/, /\d/, /\d/, /\d/, /\d/,'-', /\d/, /\d/, /\d/, /\d/],
      maskHorario: [/\d/, /\d/, ':', /\d/, /\d/],
      cartaoCredito: [/\d/, /\d/, '.', /\d/, /\d/, /\d/, '.', /\d/, /\d/, /\d/, '/', /\d/, /\d/, /\d/, /\d/, '-', /\d/, /\d/],
      maskDecimal: decimalMask,
      maskLogitude: logitudeMask    
```

### Como Attributo;

```
<input type='text' class='form-control' [(ngModel)]='vm.model.serieStart' name='serieStart' required formControlName='serieStart'  maski='9999999999'  autocomplete='off'/>
```

### PRÉ REQUISITOS;

1-) git shell [https://git-for-windows.github.io/]

2-) node.js [https://nodejs.org/en/])

3-) npm install -g @angular/cli

3-) opcional [Conemu [https://www.fosshub.com/ConEmu.html/ConEmuSetup.161206.exe]]

4-) Instalar o dotnet (sdk / runtime) https://dotnet.microsoft.com/download/dotnet-core/2.2

#update
npm uninstall -g angular-cli
npm cache clean --force
npm install -g @angular/cli

### Links;

[Diagrama Gerador](https://drive.google.com/file/d/1qE6RSNoJCipIbQMYFmT41_Y7GXW2WXds/view)

[Artigo Gerador de Codigo](https://medium.com/@wilsonsantos_66971/gerador-de-c%C3%B3digo-7e3c08981e43?source=friends_link&sk=e0b14bb1a0f656873b0ae619b4bbd4e9)

[Gerador, Foque no Domínio](https://medium.com/@wilsonsantos_66971/foque-no-dom%C3%ADnio-628e284c3703)
