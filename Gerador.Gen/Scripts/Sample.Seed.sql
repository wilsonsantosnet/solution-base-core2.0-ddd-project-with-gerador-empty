USE [Sample.Seed]
/****** Object:  Table [dbo].[ManySampleType]    Script Date: 31/03/2018 15:20:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManySampleType](
	[SampleId] [int] NOT NULL,
	[SampleTypeId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menu]    Script Date: 31/03/2018 15:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[IdMenu] [int] IDENTITY(1,1) NOT NULL,
	[NomeRota] [varchar](50) NOT NULL,
	[UrlRota] [varchar](100) NOT NULL,
	[IdMenuPai] [int] NOT NULL,
	[IconeMenu] [varchar](30) NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[IdMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuUnidade]    Script Date: 31/03/2018 15:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuUnidade](
	[IdMenuUnidade] [int] IDENTITY(1,1) NOT NULL,
	[IdMenu] [int] NOT NULL,
	[IdUnidade] [int] NOT NULL,
 CONSTRAINT [PK_MenuUnidade] PRIMARY KEY CLUSTERED 
(
	[IdMenuUnidade] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sample]    Script Date: 31/03/2018 15:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sample](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Descricao] [varchar](300) NULL,
	[SampleTypeId] [int] NOT NULL,
	[Ativo] [bit] NULL,
	[Age] [int] NULL,
	[Category] [int] NULL,
	[Datetime] [datetime] NULL,
	[Tags] [varchar](1000) NULL,
	[UserCreateId] [int] NOT NULL,
	[UserCreateDate] [datetime] NOT NULL,
	[UserAlterId] [int] NULL,
	[UserAlterDate] [datetime] NULL,
 CONSTRAINT [PK_Sample] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SampleType]    Script Date: 31/03/2018 15:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SampleType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
 CONSTRAINT [PK_SampleType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Unidade]    Script Date: 31/03/2018 15:21:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Unidade](
	[IdUnidade] [int] IDENTITY(1,1) NOT NULL,
	[CodUnidade] [varchar](500) NOT NULL,
	[DsUnidade] [varchar](500) NOT NULL,
	[IdUnidadeSuperior] [int] NULL,
	[NuAtributo1] [int] NULL,
	[NuAtributo2] [int] NULL,
	[NuAtributo3] [int] NULL,
	[NuAtributo4] [int] NULL,
	[NuAtributo5] [int] NULL,
	[NuAtributo6] [int] NULL,
	[NuAtributo7] [int] NULL,
	[DsAtributo1] [varchar](500) NULL,
	[DsAtributo2] [varchar](500) NULL,
	[DsAtributo3] [varchar](500) NULL,
	[DsAtributo4] [varchar](500) NULL,
	[DsAtributo5] [varchar](500) NULL,
	[DsAtributo6] [varchar](500) NULL,
	[DsAtributo7] [varchar](500) NULL,
	[EhAtivo] [bit] NOT NULL,
	[DtCadastro] [datetime] NOT NULL,
	[DtAtualizacao] [datetime] NOT NULL,
	[IdCargo] [int] NULL,
 CONSTRAINT [PK_EstruturaUnidades] PRIMARY KEY CLUSTERED 
(
	[IdUnidade] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ManySampleType]  WITH CHECK ADD  CONSTRAINT [FK_ManySampleType_Sample] FOREIGN KEY([SampleId])
REFERENCES [dbo].[Sample] ([Id])
GO
ALTER TABLE [dbo].[ManySampleType] CHECK CONSTRAINT [FK_ManySampleType_Sample]
GO
ALTER TABLE [dbo].[ManySampleType]  WITH CHECK ADD  CONSTRAINT [FK_ManySampleType_SampleType] FOREIGN KEY([SampleTypeId])
REFERENCES [dbo].[SampleType] ([Id])
GO
ALTER TABLE [dbo].[ManySampleType] CHECK CONSTRAINT [FK_ManySampleType_SampleType]
GO
ALTER TABLE [dbo].[Sample]  WITH CHECK ADD  CONSTRAINT [FK_Sample_SampleType] FOREIGN KEY([SampleTypeId])
REFERENCES [dbo].[SampleType] ([Id])
GO
ALTER TABLE [dbo].[Sample] CHECK CONSTRAINT [FK_Sample_SampleType]
GO
