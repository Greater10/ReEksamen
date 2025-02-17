USE [master]
GO
/****** Object:  Database [Delta]    Script Date: 07-07-2024 16:47:37 ******/
CREATE DATABASE [Delta]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Delta', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Delta.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Delta_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Delta_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Delta] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Delta].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Delta] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Delta] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Delta] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Delta] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Delta] SET ARITHABORT OFF 
GO
ALTER DATABASE [Delta] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Delta] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Delta] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Delta] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Delta] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Delta] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Delta] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Delta] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Delta] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Delta] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Delta] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Delta] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Delta] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Delta] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Delta] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Delta] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Delta] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Delta] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Delta] SET  MULTI_USER 
GO
ALTER DATABASE [Delta] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Delta] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Delta] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Delta] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Delta] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Delta] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Delta] SET QUERY_STORE = ON
GO
ALTER DATABASE [Delta] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Delta]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 07-07-2024 16:47:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[LocationId] [int] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 07-07-2024 16:47:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](100) NULL,
	[Address] [nchar](255) NULL,
	[WorkEmail] [nchar](255) NULL,
	[SocialSecurityNumber] [nchar](11) NULL,
	[Phone] [nchar](20) NULL,
	[WorkPhone] [nchar](20) NULL,
	[Password] [nchar](255) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 07-07-2024 16:47:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](255) NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 07-07-2024 16:47:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[TaskId] [int] IDENTITY(1,1) NOT NULL,
	[PatientSocialSecurityNumber] [nchar](11) NULL,
	[Room] [nchar](10) NULL,
	[Bed] [nchar](10) NULL,
	[Isolated] [bit] NULL,
	[Deaf] [bit] NULL,
	[Mute] [bit] NULL,
	[Inactive] [bit] NULL,
	[ForeignLanguage] [bit] NULL,
	[SpecialMedication] [bit] NULL,
	[Priority] [int] NULL,
	[TaskDate] [datetime] NULL,
	[Comments] [nchar](255) NULL,
	[PatientName] [nchar](255) NULL,
	[DepartmentId] [int] NULL,
	[EmployeeId] [int] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test]    Script Date: 07-07-2024 16:47:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[TestId] [int] IDENTITY(1,1) NOT NULL,
	[TestType] [int] NULL,
	[TaskId] [int] NOT NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (1, N'A1, mave-tarm-kirugisk', 3)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (2, N'S1, hjertemedicinsk', 3)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (3, N'S2, hjertemedicinsk', 3)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (4, N'NNN, neuro-, hoved-, halskirugisk', 3)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (5, N'O1, ortopædkirugisk', 3)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (6, N'O2, ortopædkirguisk', 3)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (7, N'Geriatrisk, ældremedicinsk', 3)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (8, N'A2, mave-tarm-kirugisk', 3)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (9, N'T, Thoraxkirugisk', 3)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (10, N'Notia, neuro- og tarme-intensiv', 3)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (11, N'9Ø, gastromedicinsk', 1)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (12, N'6V, lungemedicinsk', 1)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (13, N'7V, hæmatologisk', 1)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (14, N'7Ø, infektionsmedicinsk', 1)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (15, N'6Ø, apopleksi', 1)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (16, N'2Ø, intermediært', 1)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (17, N'R, alem intensivt', 1)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (18, N'ATC, akut traume center/skadestuen', 2)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (19, N'Grønt spor, patienterne hvor det ikke haster, men de skal ses af læge', 2)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (20, N'Blåt spor, brækkede knogler', 2)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (21, N'AMA 1, akutmodtageafsnit', 2)
INSERT [dbo].[Department] ([DepartmentId], [Name], [LocationId]) VALUES (22, N'AMA 2, akutmodtageafsnit', 2)
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([EmployeeId], [Name], [Address], [WorkEmail], [SocialSecurityNumber], [Phone], [WorkPhone], [Password]) VALUES (1, N'Mikkel Hansen                                                                                       ', N'Vidarsvej 17, 3600 Frederikssund                                                                                                                                                                                                                               ', N'mikkelmehrhansen123@gmail.com                                                                                                                                                                                                                                  ', N'123456-1234', N'30747930            ', N'112                 ', N'Mikkel123                                                                                                                                                                                                                                                      ')
INSERT [dbo].[Employee] ([EmployeeId], [Name], [Address], [WorkEmail], [SocialSecurityNumber], [Phone], [WorkPhone], [Password]) VALUES (2, N'Frida Hansen                                                                                        ', N'Vidarsvej 17, 3600 Frederikssund                                                                                                                                                                                                                               ', N'fridamehrhansen@hotmail.com                                                                                                                                                                                                                                    ', N'654321-1234', N'11223344            ', N'113                 ', N'Frida123                                                                                                                                                                                                                                                       ')
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[Location] ON 

INSERT [dbo].[Location] ([LocationId], [Name]) VALUES (1, N'Medicinerhuset                                                                                                                                                                                                                                                 ')
INSERT [dbo].[Location] ([LocationId], [Name]) VALUES (2, N'Skadestuen                                                                                                                                                                                                                                                     ')
INSERT [dbo].[Location] ([LocationId], [Name]) VALUES (3, N'Højhuset                                                                                                                                                                                                                                                       ')
SET IDENTITY_INSERT [dbo].[Location] OFF
GO
SET IDENTITY_INSERT [dbo].[Task] ON 

INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (1, N'010101-0001', N'101       ', N'A1        ', 0, 0, 0, 0, 1, 0, 1, CAST(N'2024-01-01T08:00:00.000' AS DateTime), N'Skift sengetøj og rengør                                                                                                                                                                                                                                       ', N'Lars Hansen                                                                                                                                                                                                                                                    ', 1, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (2, N'020202-0002', N'102       ', N'B1        ', 0, 0, 0, 0, 0, 0, 2, CAST(N'2024-01-02T09:00:00.000' AS DateTime), N'Giv patient medicin kl. 09:00                                                                                                                                                                                                                                  ', N'Mette Nielsen                                                                                                                                                                                                                                                  ', 2, 1)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (3, N'030303-0003', N'103       ', N'C1        ', 1, 0, 0, 0, 1, 1, 3, CAST(N'2024-01-03T10:00:00.000' AS DateTime), N'Overvåg patientens vitale tegn                                                                                                                                                                                                                                 ', N'Søren Andersen                                                                                                                                                                                                                                                 ', 3, 2)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (4, N'040404-0004', N'104       ', N'D1        ', 0, 1, 0, 0, 0, 0, 1, CAST(N'2024-01-04T11:00:00.000' AS DateTime), N'Tjek blodsukker niveauer                                                                                                                                                                                                                                       ', N'Kirsten Pedersen                                                                                                                                                                                                                                               ', 1, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (5, N'050505-0005', N'105       ', N'E1        ', 0, 0, 1, 0, 0, 1, 2, CAST(N'2024-01-05T12:00:00.000' AS DateTime), N'Forbered patient til operation                                                                                                                                                                                                                                 ', N'Ole Jensen                                                                                                                                                                                                                                                     ', 2, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (6, N'060606-0006', N'106       ', N'F1        ', 0, 0, 0, 1, 1, 0, 3, CAST(N'2024-01-06T13:00:00.000' AS DateTime), N'Administrer IV væsker                                                                                                                                                                                                                                          ', N'Hanne Christensen                                                                                                                                                                                                                                              ', 3, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (7, N'070707-0007', N'107       ', N'G1        ', 1, 0, 0, 0, 0, 1, 1, CAST(N'2024-01-07T14:00:00.000' AS DateTime), N'Patienten skal have en MR-scanning                                                                                                                                                                                                                             ', N'Niels Larsen                                                                                                                                                                                                                                                   ', 1, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (8, N'080808-0008', N'108       ', N'H1        ', 0, 1, 0, 0, 1, 0, 2, CAST(N'2024-01-08T15:00:00.000' AS DateTime), N'Giv antibiotika kl. 15:00                                                                                                                                                                                                                                      ', N'Inge Hansen                                                                                                                                                                                                                                                    ', 2, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (9, N'090909-0009', N'109       ', N'I1        ', 0, 0, 1, 0, 0, 1, 3, CAST(N'2024-01-09T16:00:00.000' AS DateTime), N'Tjek patientens sårpleje                                                                                                                                                                                                                                       ', N'Peter Sørensen                                                                                                                                                                                                                                                 ', 3, 1)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (10, N'101010-0010', N'110       ', N'J1        ', 0, 0, 0, 1, 1, 0, 1, CAST(N'2024-01-10T17:00:00.000' AS DateTime), N'Planlæg udskrivelse af patient                                                                                                                                                                                                                                 ', N'Bente Rasmussen                                                                                                                                                                                                                                                ', 1, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (11, N'111111-0011', N'111       ', N'K1        ', 1, 0, 0, 0, 0, 1, 2, CAST(N'2024-01-11T18:00:00.000' AS DateTime), N'Kontroller patientens kateter                                                                                                                                                                                                                                  ', N'Jørgen Kristensen                                                                                                                                                                                                                                              ', 2, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (12, N'121212-0012', N'112       ', N'L1        ', 0, 1, 0, 0, 1, 0, 3, CAST(N'2024-01-12T19:00:00.000' AS DateTime), N'Sørg for patientens ernæring                                                                                                                                                                                                                                   ', N'Grethe Poulsen                                                                                                                                                                                                                                                 ', 3, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (13, N'131313-0013', N'113       ', N'M1        ', 0, 0, 1, 0, 0, 1, 1, CAST(N'2024-01-13T20:00:00.000' AS DateTime), N'Skift forbindinger dagligt                                                                                                                                                                                                                                     ', N'Henrik Thomsen                                                                                                                                                                                                                                                 ', 1, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (14, N'141414-0014', N'114       ', N'N1        ', 0, 0, 0, 1, 1, 0, 2, CAST(N'2024-01-14T21:00:00.000' AS DateTime), N'Patienten skal have fysioterapi                                                                                                                                                                                                                                ', N'Else Madsen                                                                                                                                                                                                                                                    ', 2, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (15, N'151515-0015', N'115       ', N'O1        ', 1, 0, 0, 0, 0, 1, 3, CAST(N'2024-01-15T22:00:00.000' AS DateTime), N'Evaluér patientens smertebehandling                                                                                                                                                                                                                            ', N'Poul Mortensen                                                                                                                                                                                                                                                 ', 3, 1)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (16, N'161616-0016', N'116       ', N'P1        ', 0, 1, 0, 0, 1, 0, 1, CAST(N'2024-01-16T23:00:00.000' AS DateTime), N'Foretag daglig vægtkontrol                                                                                                                                                                                                                                     ', N'Anette Jørgensen                                                                                                                                                                                                                                               ', 1, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (17, N'171717-0017', N'117       ', N'Q1        ', 0, 0, 1, 0, 0, 1, 2, CAST(N'2024-01-17T00:00:00.000' AS DateTime), N'Giv patient smertestillende medicin                                                                                                                                                                                                                            ', N'Erik Johansen                                                                                                                                                                                                                                                  ', 2, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (18, N'181818-0018', N'118       ', N'R1        ', 0, 0, 0, 1, 1, 0, 3, CAST(N'2024-01-18T01:00:00.000' AS DateTime), N'Overvåg patientens diæt                                                                                                                                                                                                                                        ', N'Tove Frandsen                                                                                                                                                                                                                                                  ', 3, 2)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (19, N'191919-0019', N'119       ', N'S1        ', 1, 0, 0, 0, 0, 1, 1, CAST(N'2024-01-19T02:00:00.000' AS DateTime), N'Kontroller patientens blodtryk                                                                                                                                                                                                                                 ', N'Bent Petersen                                                                                                                                                                                                                                                  ', 1, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (20, N'202020-0020', N'120       ', N'T1        ', 0, 1, 0, 0, 1, 0, 2, CAST(N'2024-01-20T03:00:00.000' AS DateTime), N'Forbered patient til røntgen                                                                                                                                                                                                                                   ', N'Anna Holm                                                                                                                                                                                                                                                      ', 2, NULL)
INSERT [dbo].[Task] ([TaskId], [PatientSocialSecurityNumber], [Room], [Bed], [Isolated], [Deaf], [Mute], [Inactive], [ForeignLanguage], [SpecialMedication], [Priority], [TaskDate], [Comments], [PatientName], [DepartmentId], [EmployeeId]) VALUES (23, N'123456-1234', N'123       ', N'2         ', 1, 0, 0, 0, 0, 0, 1, CAST(N'2024-07-07T16:41:13.737' AS DateTime), N'srthrdthdrh                                                                                                                                                                                                                                                    ', N'Mikkel                                                                                                                                                                                                                                                         ', 6, NULL)
SET IDENTITY_INSERT [dbo].[Task] OFF
GO
SET IDENTITY_INSERT [dbo].[Test] ON 

INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (66, 2, 1)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (67, 3, 1)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (68, 4, 1)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (69, 1, 1)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (70, 1, 2)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (71, 3, 2)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (72, 2, 2)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (73, 4, 2)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (74, 3, 3)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (75, 4, 3)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (76, 3, 4)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (77, 2, 4)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (78, 4, 4)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (79, 1, 4)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (80, 1, 5)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (81, 2, 5)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (82, 3, 5)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (83, 2, 6)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (84, 1, 6)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (85, 4, 6)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (86, 3, 6)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (87, 1, 7)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (88, 3, 7)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (89, 2, 8)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (90, 1, 9)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (91, 2, 9)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (92, 2, 10)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (93, 4, 11)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (94, 2, 12)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (95, 3, 12)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (96, 1, 12)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (97, 2, 13)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (98, 4, 13)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (99, 2, 14)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (100, 4, 14)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (101, 3, 14)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (102, 2, 15)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (103, 1, 15)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (104, 4, 15)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (105, 2, 16)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (106, 1, 16)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (107, 4, 16)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (108, 3, 17)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (109, 4, 17)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (110, 3, 18)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (111, 2, 18)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (112, 4, 18)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (113, 1, 18)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (114, 1, 19)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (115, 3, 19)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (116, 4, 20)
INSERT [dbo].[Test] ([TestId], [TestType], [TaskId]) VALUES (117, 2, 20)
SET IDENTITY_INSERT [dbo].[Test] OFF
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Location]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Employee]
GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD  CONSTRAINT [FK_Test_Task] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([TaskId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Test] CHECK CONSTRAINT [FK_Test_Task]
GO
USE [master]
GO
ALTER DATABASE [Delta] SET  READ_WRITE 
GO
