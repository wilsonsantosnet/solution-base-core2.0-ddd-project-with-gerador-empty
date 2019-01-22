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
