USE [pageDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/17/2021 12:11:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Detalji]    Script Date: 4/17/2021 12:11:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Detalji](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[BrojIndeksa] [nvarchar](max) NULL,
	[Ime] [nvarchar](max) NULL,
	[Prezime] [nvarchar](max) NULL,
	[Godina] [int] NOT NULL,
	[NazivStatusa] [nvarchar](max) NULL,
 CONSTRAINT [PK_Detalji] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Korisnici]    Script Date: 4/17/2021 12:11:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Korisnici](
	[KorisnikId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NULL,
	[PasswordHash] [varbinary](max) NULL,
	[PasswordSalt] [varbinary](max) NULL,
	[Role] [nvarchar](max) NULL,
 CONSTRAINT [PK_Korisnici] PRIMARY KEY CLUSTERED 
(
	[KorisnikId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kursevi]    Script Date: 4/17/2021 12:11:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kursevi](
	[KursId] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [nvarchar](max) NULL,
 CONSTRAINT [PK_Kursevi] PRIMARY KEY CLUSTERED 
(
	[KursId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KursStudenti]    Script Date: 4/17/2021 12:11:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KursStudenti](
	[KursId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK_KursStudenti] PRIMARY KEY CLUSTERED 
(
	[KursId] ASC,
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status_Studenta]    Script Date: 4/17/2021 12:11:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status_Studenta](
	[StatusStudentaId] [int] IDENTITY(1,1) NOT NULL,
	[NazivStatusa] [nvarchar](max) NULL,
 CONSTRAINT [PK_Status_Studenta] PRIMARY KEY CLUSTERED 
(
	[StatusStudentaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Studenti]    Script Date: 4/17/2021 12:11:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Studenti](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[BrojIndeksa] [nvarchar](max) NULL,
	[Ime] [nvarchar](max) NULL,
	[Prezime] [nvarchar](max) NULL,
	[Godina] [int] NOT NULL,
	[StatusStudentaId] [int] NULL,
 CONSTRAINT [PK_Studenti] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Korisnici] ADD  DEFAULT (N'Korisnik') FOR [Role]
GO
ALTER TABLE [dbo].[KursStudenti]  WITH CHECK ADD  CONSTRAINT [FK_KursStudenti_Kursevi_KursId] FOREIGN KEY([KursId])
REFERENCES [dbo].[Kursevi] ([KursId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KursStudenti] CHECK CONSTRAINT [FK_KursStudenti_Kursevi_KursId]
GO
ALTER TABLE [dbo].[KursStudenti]  WITH CHECK ADD  CONSTRAINT [FK_KursStudenti_Studenti_StudentId] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Studenti] ([StudentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[KursStudenti] CHECK CONSTRAINT [FK_KursStudenti_Studenti_StudentId]
GO
ALTER TABLE [dbo].[Studenti]  WITH CHECK ADD  CONSTRAINT [FK_Studenti_Status_Studenta_StatusStudentaId] FOREIGN KEY([StatusStudentaId])
REFERENCES [dbo].[Status_Studenta] ([StatusStudentaId])
GO
ALTER TABLE [dbo].[Studenti] CHECK CONSTRAINT [FK_Studenti_Status_Studenta_StatusStudentaId]
GO
/****** Object:  StoredProcedure [dbo].[spGetStudentById]    Script Date: 4/17/2021 12:11:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetStudentById]
                                @Id int
                                AS
                                BEGIN
	                            SELECT StudentId ,BrojIndeksa ,Ime,Prezime,Godina ,
	                            NazivStatusa FROM Studenti,Status_Studenta 
	                            WHERE Studenti.StudentId=@Id AND 
                                Studenti.StatusStudentaId=Status_Studenta.StatusStudentaId;
                                END
GO
/****** Object:  StoredProcedure [dbo].[spGetStudents]    Script Date: 4/17/2021 12:11:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetStudents]
                                AS
                                BEGIN
	                            SELECT StudentId ,BrojIndeksa ,Ime,Prezime,Godina ,
	                            NazivStatusa FROM Studenti,Status_Studenta 
	                            WHERE
                                Studenti.StatusStudentaId=Status_Studenta.StatusStudentaId;
                                END
GO
