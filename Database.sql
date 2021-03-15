USE [master]
GO
/****** Object:  Database [TFPTestDatabase]    Script Date: 1/31/2021 6:18:52 PM ******/
CREATE DATABASE [TFPTestDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TFPTestDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQL2018\MSSQL\DATA\TFPTestDatabase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TFPTestDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQL2018\MSSQL\DATA\TFPTestDatabase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TFPTestDatabase] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TFPTestDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TFPTestDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TFPTestDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TFPTestDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TFPTestDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TFPTestDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TFPTestDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [TFPTestDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TFPTestDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TFPTestDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TFPTestDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TFPTestDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TFPTestDatabase] SET QUERY_STORE = OFF
GO
USE [TFPTestDatabase]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 1/31/2021 6:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccount](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[OriginType] [nvarchar](50) NULL,
	[ContactNumber] [nvarchar](50) NULL,
	[Nationality] [nvarchar](50) NULL,
	[Status] [bit] NOT NULL,
	[RegistrationDate] [datetime] NOT NULL,
	[IsActive] [bit] NULL,
	[ImagePath] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserAccount] ON 

INSERT [dbo].[UserAccount] ([UserId], [FullName], [Email], [Password], [OriginType], [ContactNumber], [Nationality], [Status], [RegistrationDate], [IsActive], [ImagePath]) VALUES (10, N'Rakib', N'rakib.featherit@gmail.com', N'123', N'Local', N'01715827404', N'Yemen', 1, CAST(N'2021-01-30T00:00:00.000' AS DateTime), 1, N'~/Images/20210131--EMP01001_resized.jpg')
INSERT [dbo].[UserAccount] ([UserId], [FullName], [Email], [Password], [OriginType], [ContactNumber], [Nationality], [Status], [RegistrationDate], [IsActive], [ImagePath]) VALUES (15, N'Rakib', N'rakibkhanphc@gmail.com', N'123', N'Local', N'01715827404', N'India', 1, CAST(N'2021-01-30T00:00:00.000' AS DateTime), 1, N'~/Images/20210131--EMP01001_resized.jpg')
SET IDENTITY_INSERT [dbo].[UserAccount] OFF
GO
USE [master]
GO
ALTER DATABASE [TFPTestDatabase] SET  READ_WRITE 
GO
