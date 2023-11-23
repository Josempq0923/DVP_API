USE [master]
GO
/****** Object:  Database [PruebaTecnicaDVP]    Script Date: 23/11/2023 12:03:07 a. m. ******/
CREATE DATABASE [PruebaTecnicaDVP]
GO
USE [PruebaTecnicaDVP]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 23/11/2023 12:03:07 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[IdentificationNumber] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[RegisterDate] [datetime] NULL,
	[FullName] [nvarchar](max) NULL,
	[FullIdentification] [nvarchar](100) NULL,
	[TypeIdentification] [nvarchar](20) NULL,
 CONSTRAINT [PK__Persons__3214EC07F863D448] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 23/11/2023 12:03:07 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](100) NULL,
	[RegisterDate] [datetime] NULL,
 CONSTRAINT [PK__Users__3214EC0753EA2091] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Persons] ON 
GO
INSERT [dbo].[Persons] ([Id], [FirstName], [LastName], [IdentificationNumber], [Email], [RegisterDate], [FullName], [FullIdentification], [TypeIdentification]) VALUES (1009, N'José Manuel', N'Pérez Quiceno', N'1000566165', N'josemanuelperez0923@gmail.com', NULL, N'José Manuel Pérez Quiceno', N'CC 1000566165', N'CC')
GO
SET IDENTITY_INSERT [dbo].[Persons] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [UserName], [Password], [RegisterDate]) VALUES (1, N'jmperezq', N'$2b$10$kqOcCC3aCLdim/GTPp2YcuLNcf22Wh20YtMUOmcknra5TGN/4LS7u', CAST(N'2023-11-21T03:38:34.237' AS DateTime))
GO
INSERT [dbo].[Users] ([Id], [UserName], [Password], [RegisterDate]) VALUES (9, N'DVP', N'$2b$10$Jm8dINr.m2DK9PyigYE1/Os/nmxhxLAnhjfxC1sJkV30TlkB1LY5G', CAST(N'2023-11-23T00:00:34.460' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users]  DEFAULT (getdate()) FOR [RegisterDate]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetPersons]    Script Date: 23/11/2023 12:03:07 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetPersons]
AS
BEGIN
    SELECT 
	P.Id,
	P.FirstName,
	P.LastName,
    (P.FirstName + ' ' + P.LastName) AS FullName,
	(P.TypeIdentification + ' ' + P.IdentificationNumber) AS FullIdentification,
	P.Email,
	P.IdentificationNumber,
	P.RegisterDate,
	P.TypeIdentification
	FROM [dbo].[Persons] P
END


GO
USE [master]
GO
ALTER DATABASE [PruebaTecnicaDVP] SET  READ_WRITE 
GO
