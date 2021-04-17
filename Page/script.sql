USE [pageDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/17/2021 11:47:49 AM ******/
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
/****** Object:  Table [dbo].[Detalji]    Script Date: 4/17/2021 11:47:49 AM ******/
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
/****** Object:  Table [dbo].[Korisnici]    Script Date: 4/17/2021 11:47:49 AM ******/
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
/****** Object:  Table [dbo].[Kursevi]    Script Date: 4/17/2021 11:47:49 AM ******/
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
/****** Object:  Table [dbo].[KursStudenti]    Script Date: 4/17/2021 11:47:49 AM ******/
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
/****** Object:  Table [dbo].[Status_Studenta]    Script Date: 4/17/2021 11:47:49 AM ******/
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
/****** Object:  Table [dbo].[Studenti]    Script Date: 4/17/2021 11:47:49 AM ******/
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
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210414122755_initialMigration', N'3.1.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210414123051_spGetStudentById', N'3.1.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210414123149_spGetStudents', N'3.1.14')
GO
SET IDENTITY_INSERT [dbo].[Korisnici] ON 

INSERT [dbo].[Korisnici] ([KorisnikId], [Username], [PasswordHash], [PasswordSalt], [Role]) VALUES (1, N'Admin', 0xBFAA4A6ECDEC7CA73919D19ECB5C2FA1D83E5017F15C457D40904BB6BE998C6D, 0xC82391F360D84C30153A1FA8664163BAD88F1C874F10310C85C91D6EBCBE62E822F34831DEBE9A46A8CD2FB6C2F1BAEAE6E724EDB0064F3E3836A9A211496661, N'Admin')
INSERT [dbo].[Korisnici] ([KorisnikId], [Username], [PasswordHash], [PasswordSalt], [Role]) VALUES (2, N'Korisnik', 0xA7DA96A783791AE590DF11D56D2C648CAD73A97384192A2ED160E665B1BFF7FF, 0xF676693B91583E80DAF0E18591C7661A27EB877EBC1F1763DFFDABBCF575E50CCC08C31A1978C0E306A5F0B3DF91851AAA2FE0F8DF8EE887A8814A6E4262EEAC, N'Korisnik')
INSERT [dbo].[Korisnici] ([KorisnikId], [Username], [PasswordHash], [PasswordSalt], [Role]) VALUES (3, N'Test', 0x0EBC15CDBA4706312BDD6211E7D3BA34EBB1438007A371B27DDE70525E5F87FF, 0x4FD229A1A26A286284B8BDF18F14C51633A09BD273607E76CD4BA8556B48F195A284E78AA171944722BD277F034B51B5EBB8B11D038FDDF178F76B9C63489B83, N'Korisnik')
SET IDENTITY_INSERT [dbo].[Korisnici] OFF
GO
SET IDENTITY_INSERT [dbo].[Kursevi] ON 

INSERT [dbo].[Kursevi] ([KursId], [Naziv]) VALUES (1, N'Internet tehnologije i programiranje')
INSERT [dbo].[Kursevi] ([KursId], [Naziv]) VALUES (2, N'ASP')
INSERT [dbo].[Kursevi] ([KursId], [Naziv]) VALUES (3, N'Objektno orijentisano programiranje')
INSERT [dbo].[Kursevi] ([KursId], [Naziv]) VALUES (4, N'Programski jezici')
INSERT [dbo].[Kursevi] ([KursId], [Naziv]) VALUES (5, N'Uvod u programiranje')
INSERT [dbo].[Kursevi] ([KursId], [Naziv]) VALUES (6, N'Osnove elektrotehnike')
INSERT [dbo].[Kursevi] ([KursId], [Naziv]) VALUES (7, N'Menadzment')
INSERT [dbo].[Kursevi] ([KursId], [Naziv]) VALUES (8, N'Matematika')
INSERT [dbo].[Kursevi] ([KursId], [Naziv]) VALUES (9, N'Matematika3')
INSERT [dbo].[Kursevi] ([KursId], [Naziv]) VALUES (10, N'Baze podataka')
INSERT [dbo].[Kursevi] ([KursId], [Naziv]) VALUES (11, N'Matematika2')
INSERT [dbo].[Kursevi] ([KursId], [Naziv]) VALUES (12, N'Tehnike i metode programiranja')
INSERT [dbo].[Kursevi] ([KursId], [Naziv]) VALUES (13, N'Arhitektura i organizacija racunara')
SET IDENTITY_INSERT [dbo].[Kursevi] OFF
GO
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (1, 1)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (2, 1)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (4, 1)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (5, 1)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (6, 1)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (8, 1)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (9, 1)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (11, 1)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (12, 1)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (1, 4)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (2, 4)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (4, 4)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (5, 4)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (7, 4)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (8, 4)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (9, 4)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (11, 4)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (12, 4)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (13, 4)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (1, 6)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (9, 6)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (11, 6)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (12, 6)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (13, 6)
INSERT [dbo].[KursStudenti] ([KursId], [StudentId]) VALUES (12, 15)
GO
SET IDENTITY_INSERT [dbo].[Status_Studenta] ON 

INSERT [dbo].[Status_Studenta] ([StatusStudentaId], [NazivStatusa]) VALUES (1, N'Redovan')
INSERT [dbo].[Status_Studenta] ([StatusStudentaId], [NazivStatusa]) VALUES (2, N'Vanredan')
INSERT [dbo].[Status_Studenta] ([StatusStudentaId], [NazivStatusa]) VALUES (3, N'Vanredan')
INSERT [dbo].[Status_Studenta] ([StatusStudentaId], [NazivStatusa]) VALUES (4, N'Redovan')
INSERT [dbo].[Status_Studenta] ([StatusStudentaId], [NazivStatusa]) VALUES (5, N'Vanredan')
INSERT [dbo].[Status_Studenta] ([StatusStudentaId], [NazivStatusa]) VALUES (6, N'Redovan')
SET IDENTITY_INSERT [dbo].[Status_Studenta] OFF
GO
SET IDENTITY_INSERT [dbo].[Studenti] ON 

INSERT [dbo].[Studenti] ([StudentId], [BrojIndeksa], [Ime], [Prezime], [Godina], [StatusStudentaId]) VALUES (1, N'1000', N'Marko', N'Jovanovic', 2, 2)
INSERT [dbo].[Studenti] ([StudentId], [BrojIndeksa], [Ime], [Prezime], [Godina], [StatusStudentaId]) VALUES (4, N'1810', N'Petar', N'Petrovic', 3, 2)
INSERT [dbo].[Studenti] ([StudentId], [BrojIndeksa], [Ime], [Prezime], [Godina], [StatusStudentaId]) VALUES (6, N'1201', N'Janko', N'Jankovic', 3, 1)
INSERT [dbo].[Studenti] ([StudentId], [BrojIndeksa], [Ime], [Prezime], [Godina], [StatusStudentaId]) VALUES (15, N'4567', N'Dragan', N'Draganic', 3, 2)
SET IDENTITY_INSERT [dbo].[Studenti] OFF
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
/****** Object:  StoredProcedure [dbo].[spGetStudentById]    Script Date: 4/17/2021 11:47:49 AM ******/
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
/****** Object:  StoredProcedure [dbo].[spGetStudents]    Script Date: 4/17/2021 11:47:49 AM ******/
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
