USE [master]
GO
/****** Object:  Database [Trainning]    Script Date: 2021/11/18 下午 01:34:35 ******/
CREATE DATABASE [Trainning]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Trainning', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Trainning.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Trainning_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Trainning_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Trainning] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Trainning].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Trainning] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Trainning] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Trainning] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Trainning] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Trainning] SET ARITHABORT OFF 
GO
ALTER DATABASE [Trainning] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Trainning] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Trainning] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Trainning] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Trainning] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Trainning] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Trainning] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Trainning] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Trainning] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Trainning] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Trainning] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Trainning] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Trainning] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Trainning] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Trainning] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Trainning] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Trainning] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Trainning] SET RECOVERY FULL 
GO
ALTER DATABASE [Trainning] SET  MULTI_USER 
GO
ALTER DATABASE [Trainning] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Trainning] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Trainning] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Trainning] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Trainning] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Trainning] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Trainning', N'ON'
GO
ALTER DATABASE [Trainning] SET QUERY_STORE = OFF
GO
USE [Trainning]
GO
/****** Object:  Table [dbo].[CommonQuestionInfo]    Script Date: 2021/11/18 下午 01:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommonQuestionInfo](
	[CommonQuestionID] [int] IDENTITY(1,1) NOT NULL,
	[CommonQuestionTitle] [nvarchar](50) NOT NULL,
	[CommonQuestionName] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Required] [int] NOT NULL,
	[Answer] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CommonQuestionInfo] PRIMARY KEY CLUSTERED 
(
	[CommonQuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListInfo]    Script Date: 2021/11/18 下午 01:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListInfo](
	[ListID] [int] IDENTITY(1,1) NOT NULL,
	[ID] [uniqueidentifier] NOT NULL,
	[ListName] [nvarchar](50) NOT NULL,
	[ListContent] [nvarchar](100) NOT NULL,
	[Status] [nvarchar](50) NULL,
	[StartTime] [date] NOT NULL,
	[Endtime] [date] NOT NULL,
 CONSTRAINT [PK_ListInfo_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionInfo]    Script Date: 2021/11/18 下午 01:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionInfo](
	[QuestionListID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[ID] [uniqueidentifier] NOT NULL,
	[QuestionName] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Required] [int] NOT NULL,
	[Answer] [nvarchar](50) NULL,
 CONSTRAINT [PK_QuestionInfo] PRIMARY KEY CLUSTERED 
(
	[QuestionListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReplyInfo]    Script Date: 2021/11/18 下午 01:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReplyInfo](
	[ReplyID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Phone] [int] NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Age] [int] NOT NULL,
	[ReplyTime] [datetime] NOT NULL,
	[ListID] [uniqueidentifier] NOT NULL,
	[QuestionID] [int] NOT NULL,
	[ReplyAnswer] [nvarchar](50) NULL,
 CONSTRAINT [PK_ReplyInfo] PRIMARY KEY CLUSTERED 
(
	[ReplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2021/11/18 下午 01:34:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInfo](
	[UserID] [uniqueidentifier] NOT NULL,
	[Account] [varchar](50) NOT NULL,
	[PWD] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CommonQuestionInfo] ON 

INSERT [dbo].[CommonQuestionInfo] ([CommonQuestionID], [CommonQuestionTitle], [CommonQuestionName], [Type], [Required], [Answer]) VALUES (1, N'常用問題1', N'Choose One', N'單選方塊', 0, N'1;2;3')
INSERT [dbo].[CommonQuestionInfo] ([CommonQuestionID], [CommonQuestionTitle], [CommonQuestionName], [Type], [Required], [Answer]) VALUES (2, N'常用問題2', N'Choose One(必填)', N'單選方塊', 1, N'1;2;3')
INSERT [dbo].[CommonQuestionInfo] ([CommonQuestionID], [CommonQuestionTitle], [CommonQuestionName], [Type], [Required], [Answer]) VALUES (3, N'常用問題3', N'Write(必填)', N'文字方塊', 0, N'?')
INSERT [dbo].[CommonQuestionInfo] ([CommonQuestionID], [CommonQuestionTitle], [CommonQuestionName], [Type], [Required], [Answer]) VALUES (4, N'常用問題4', N'Choose One', N'單選方塊', 0, N'?A;B;C;D')
INSERT [dbo].[CommonQuestionInfo] ([CommonQuestionID], [CommonQuestionTitle], [CommonQuestionName], [Type], [Required], [Answer]) VALUES (5, N'常用問題5', N'Choose One(必填)', N'單選方塊', 0, N'1;2;3')
INSERT [dbo].[CommonQuestionInfo] ([CommonQuestionID], [CommonQuestionTitle], [CommonQuestionName], [Type], [Required], [Answer]) VALUES (6, N'常用問題6', N'Choose One', N'單選方塊', 0, N'1;2;3')
INSERT [dbo].[CommonQuestionInfo] ([CommonQuestionID], [CommonQuestionTitle], [CommonQuestionName], [Type], [Required], [Answer]) VALUES (7, N'常用問題7', N'Write', N'文字方塊', 0, N'')
INSERT [dbo].[CommonQuestionInfo] ([CommonQuestionID], [CommonQuestionTitle], [CommonQuestionName], [Type], [Required], [Answer]) VALUES (9, N'常用問題8', N'Write2', N'文字方塊', 0, N'')
INSERT [dbo].[CommonQuestionInfo] ([CommonQuestionID], [CommonQuestionTitle], [CommonQuestionName], [Type], [Required], [Answer]) VALUES (10, N'常用問題9', N'Write3', N'文字方塊', 0, N'')
INSERT [dbo].[CommonQuestionInfo] ([CommonQuestionID], [CommonQuestionTitle], [CommonQuestionName], [Type], [Required], [Answer]) VALUES (11, N'常用問題10', N'Choose One', N'單選方塊', 0, N'A;B;C')
SET IDENTITY_INSERT [dbo].[CommonQuestionInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[ListInfo] ON 

INSERT [dbo].[ListInfo] ([ListID], [ID], [ListName], [ListContent], [Status], [StartTime], [Endtime]) VALUES (2, N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', N'XXXXX', N'111111111111111111111111111111111111111111111111', N'投票中', CAST(N'2021-11-10' AS Date), CAST(N'2021-12-23' AS Date))
INSERT [dbo].[ListInfo] ([ListID], [ID], [ListName], [ListContent], [Status], [StartTime], [Endtime]) VALUES (4, N'66687f41-98e8-481e-9756-15ee5c571bb6', N'ZZZ', N'XXX', N'投票中', CAST(N'2021-10-24' AS Date), CAST(N'2021-10-30' AS Date))
INSERT [dbo].[ListInfo] ([ListID], [ID], [ListName], [ListContent], [Status], [StartTime], [Endtime]) VALUES (3, N'4b538469-1cfd-4f43-8e4c-352d20d2fd37', N'AAAA', N'BBBB', N'投票中', CAST(N'2021-10-24' AS Date), CAST(N'2021-10-29' AS Date))
INSERT [dbo].[ListInfo] ([ListID], [ID], [ListName], [ListContent], [Status], [StartTime], [Endtime]) VALUES (1, N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', N'123', N'12345', N'投票中', CAST(N'2021-10-21' AS Date), CAST(N'2021-11-11' AS Date))
INSERT [dbo].[ListInfo] ([ListID], [ID], [ListName], [ListContent], [Status], [StartTime], [Endtime]) VALUES (6, N'79a08e85-a9a0-40f1-bf76-cb852bb72f2e', N'XXX', N'XXXXX', N'投票中', CAST(N'2021-11-10' AS Date), CAST(N'2021-12-01' AS Date))
SET IDENTITY_INSERT [dbo].[ListInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[QuestionInfo] ON 

INSERT [dbo].[QuestionInfo] ([QuestionListID], [QuestionID], [ID], [QuestionName], [Type], [Required], [Answer]) VALUES (1, 1, N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', N'Write', N'文字方塊', 0, N'?')
INSERT [dbo].[QuestionInfo] ([QuestionListID], [QuestionID], [ID], [QuestionName], [Type], [Required], [Answer]) VALUES (2, 2, N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', N'Choose One(必填)', N'單選方塊', 1, N'A;B;C')
INSERT [dbo].[QuestionInfo] ([QuestionListID], [QuestionID], [ID], [QuestionName], [Type], [Required], [Answer]) VALUES (3, 3, N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', N'Choose (必填)', N'複選方塊', 1, N'A;B;C;D')
INSERT [dbo].[QuestionInfo] ([QuestionListID], [QuestionID], [ID], [QuestionName], [Type], [Required], [Answer]) VALUES (4, 1, N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', N'Write', N'文字方塊', 0, N'?')
INSERT [dbo].[QuestionInfo] ([QuestionListID], [QuestionID], [ID], [QuestionName], [Type], [Required], [Answer]) VALUES (5, 2, N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', N'Write2', N'文字方塊', 0, N'')
INSERT [dbo].[QuestionInfo] ([QuestionListID], [QuestionID], [ID], [QuestionName], [Type], [Required], [Answer]) VALUES (6, 3, N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', N'Choose One1(必填)', N'單選方塊', 1, N'A;B;C')
INSERT [dbo].[QuestionInfo] ([QuestionListID], [QuestionID], [ID], [QuestionName], [Type], [Required], [Answer]) VALUES (7, 4, N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', N'Choose One2', N'單選方塊', 0, N'A;B;C')
INSERT [dbo].[QuestionInfo] ([QuestionListID], [QuestionID], [ID], [QuestionName], [Type], [Required], [Answer]) VALUES (8, 5, N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', N'Choose1(必填)', N'複選方塊', 1, N'A;B;C;D')
INSERT [dbo].[QuestionInfo] ([QuestionListID], [QuestionID], [ID], [QuestionName], [Type], [Required], [Answer]) VALUES (9, 6, N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', N'Choose2', N'複選方塊', 0, N'A;B;C;D')
INSERT [dbo].[QuestionInfo] ([QuestionListID], [QuestionID], [ID], [QuestionName], [Type], [Required], [Answer]) VALUES (10, 1, N'79a08e85-a9a0-40f1-bf76-cb852bb72f2e', N'Write', N'文字方塊', 0, N'?')
SET IDENTITY_INSERT [dbo].[QuestionInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[ReplyInfo] ON 

INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (1, N'XXX', 988888888, N'XXX@XXX.com', 22, CAST(N'2021-11-05T00:00:00.000' AS DateTime), N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', 1, N'123')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (2, N'XXX', 988888888, N'XXX@XXX.com', 22, CAST(N'2021-11-05T00:00:00.000' AS DateTime), N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', 2, N'A')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (3, N'XXX', 988888888, N'XXX@XXX.com', 22, CAST(N'2021-11-05T00:00:00.000' AS DateTime), N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', 3, N'A,B')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (4, N'AAA', 900000000, N'AAA@AAA.com', 22, CAST(N'2021-11-10T08:48:50.253' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 1, N'123')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (5, N'AAA', 900000000, N'AAA@AAA.com', 22, CAST(N'2021-11-10T08:48:50.257' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 2, N'321')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (6, N'AAA', 900000000, N'AAA@AAA.com', 22, CAST(N'2021-11-10T08:48:50.260' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 3, N'A')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (7, N'AAA', 900000000, N'AAA@AAA.com', 22, CAST(N'2021-11-10T08:48:50.267' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 4, N'B')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (8, N'AAA', 900000000, N'AAA@AAA.com', 22, CAST(N'2021-11-10T08:48:50.270' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 5, N'?A,B')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (9, N'AAA', 900000000, N'AAA@AAA.com', 22, CAST(N'2021-11-10T08:48:50.273' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 6, N'C,D')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (10, N'BBB', 911111111, N'BBB@BBB.com', 23, CAST(N'2021-11-10T08:58:55.057' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 1, N'321')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (11, N'BBB', 911111111, N'BBB@BBB.com', 23, CAST(N'2021-11-10T08:58:55.067' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 2, N'123')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (12, N'BBB', 911111111, N'BBB@BBB.com', 23, CAST(N'2021-11-10T08:58:55.070' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 3, N'C')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (13, N'BBB', 911111111, N'BBB@BBB.com', 23, CAST(N'2021-11-10T08:58:55.077' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 4, N'B')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (14, N'BBB', 911111111, N'BBB@BBB.com', 23, CAST(N'2021-11-10T08:58:55.077' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 5, N'C,D')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (15, N'BBB', 911111111, N'BBB@BBB.com', 23, CAST(N'2021-11-10T08:58:55.080' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 6, N'?A,B')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (16, N'CCC', 911111111, N'CCC@CCC.com', 24, CAST(N'2021-11-13T17:35:53.947' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 1, N'456')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (17, N'CCC', 911111111, N'CCC@CCC.com', 24, CAST(N'2021-11-13T17:35:53.950' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 2, N'654')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (18, N'CCC', 911111111, N'CCC@CCC.com', 24, CAST(N'2021-11-13T17:35:53.953' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 3, N'B')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (19, N'CCC', 911111111, N'CCC@CCC.com', 24, CAST(N'2021-11-13T17:35:53.953' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 4, N'C')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (20, N'CCC', 911111111, N'CCC@CCC.com', 24, CAST(N'2021-11-13T17:35:53.953' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 5, N'C,D')
INSERT [dbo].[ReplyInfo] ([ReplyID], [Name], [Phone], [Email], [Age], [ReplyTime], [ListID], [QuestionID], [ReplyAnswer]) VALUES (21, N'CCC', 911111111, N'CCC@CCC.com', 24, CAST(N'2021-11-13T17:35:53.957' AS DateTime), N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', 6, N'C,D')
SET IDENTITY_INSERT [dbo].[ReplyInfo] OFF
GO
INSERT [dbo].[UserInfo] ([UserID], [Account], [PWD], [Name], [Email]) VALUES (N'785f3020-a505-4770-887a-dd8643b2ea76', N'XXX', N'12345', N'XXX', N'XXX@gmail.com')
GO
ALTER TABLE [dbo].[ListInfo] ADD  CONSTRAINT [DF_ListInfo_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[ReplyInfo] ADD  CONSTRAINT [DF_ReplyInfo_ReplyTime]  DEFAULT (getdate()) FOR [ReplyTime]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_ID]  DEFAULT (newid()) FOR [UserID]
GO
USE [master]
GO
ALTER DATABASE [Trainning] SET  READ_WRITE 
GO
