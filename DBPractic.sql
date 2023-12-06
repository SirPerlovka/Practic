USE [master]
GO
/****** Object:  Database [PP]    Script Date: 06.12.2023 2:56:10 ******/
CREATE DATABASE [PP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PP', FILENAME = N'F:\SqlServer\MSSQL15.SQLEXPRESS\MSSQL\DATA\PP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PP_log', FILENAME = N'F:\SqlServer\MSSQL15.SQLEXPRESS\MSSQL\DATA\PP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PP] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PP] SET ARITHABORT OFF 
GO
ALTER DATABASE [PP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PP] SET  MULTI_USER 
GO
ALTER DATABASE [PP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PP] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PP] SET QUERY_STORE = OFF
GO
USE [PP]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 06.12.2023 2:56:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[CodeClient] [int] NOT NULL,
	[Lastname] [nvarchar](50) NULL,
	[Firstname] [nvarchar](50) NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Passport] [nvarchar](12) NULL,
	[Birthday] [date] NULL,
	[Address] [nvarchar](250) NULL,
	[Email] [nvarchar](250) NULL,
	[passwort] [nvarchar](10) NULL,
 CONSTRAINT [PK__Clients__87A2B3CAF839C636] PRIMARY KEY CLUSTERED 
(
	[CodeClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employes]    Script Date: 06.12.2023 2:56:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employes](
	[CodeEmploye] [int] NOT NULL,
	[Post] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
	[Firstname] [nvarchar](50) NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Login] [nvarchar](250) NULL,
	[Password] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[CodeEmploye] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enter]    Script Date: 06.12.2023 2:56:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enter](
	[CodeEmployee] [int] NOT NULL,
	[LastEnter] [datetime2](7) NULL,
	[TypeEnter] [nvarchar](40) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 06.12.2023 2:56:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ID] [int] NOT NULL,
	[DateCreate] [date] NULL,
	[TimeOrder] [time](7) NULL,
	[CodeClient] [int] NULL,
	[StatusOrder] [nvarchar](20) NULL,
	[DateClose] [date] NULL,
	[TimeRental] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderService]    Script Date: 06.12.2023 2:56:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderService](
	[ID] [int] NOT NULL,
	[ID_Order] [int] NULL,
	[ID_Service] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 06.12.2023 2:56:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[ID] [int] NOT NULL,
	[NameService] [nvarchar](100) NULL,
	[CodeService] [nvarchar](15) NULL,
	[CostService] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Enter]  WITH CHECK ADD  CONSTRAINT [FK_Enter_Employes] FOREIGN KEY([CodeEmployee])
REFERENCES [dbo].[Employes] ([CodeEmploye])
GO
ALTER TABLE [dbo].[Enter] CHECK CONSTRAINT [FK_Enter_Employes]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK__Orders__CodeClie__267ABA7A] FOREIGN KEY([CodeClient])
REFERENCES [dbo].[Clients] ([CodeClient])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK__Orders__CodeClie__267ABA7A]
GO
ALTER TABLE [dbo].[OrderService]  WITH CHECK ADD FOREIGN KEY([ID_Order])
REFERENCES [dbo].[Orders] ([ID])
GO
ALTER TABLE [dbo].[OrderService]  WITH CHECK ADD FOREIGN KEY([ID_Service])
REFERENCES [dbo].[Services] ([ID])
GO
USE [master]
GO
ALTER DATABASE [PP] SET  READ_WRITE 
GO
