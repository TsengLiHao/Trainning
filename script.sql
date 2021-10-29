USE [Trainning]
GO
/****** Object:  Table [dbo].[AnswerInfo]    Script Date: 2021/10/29 下午 10:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnswerInfo](
	[AnswerID] [int] IDENTITY(1,1) NOT NULL,
	[ID] [uniqueidentifier] NOT NULL,
	[QuestionID] [int] NOT NULL,
	[Answer] [nvarchar](50) NULL,
 CONSTRAINT [PK_AnswerInfo] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListInfo]    Script Date: 2021/10/29 下午 10:36:51 ******/
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
/****** Object:  Table [dbo].[QuestionInfo]    Script Date: 2021/10/29 下午 10:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionInfo](
	[QuestionID] [int] NOT NULL,
	[ID] [uniqueidentifier] NOT NULL,
	[QuestionName] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Required] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReplyInfo]    Script Date: 2021/10/29 下午 10:36:51 ******/
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
	[ReplyTime] [date] NOT NULL,
	[ListID] [uniqueidentifier] NOT NULL,
	[QuestionID] [int] NOT NULL,
 CONSTRAINT [PK_ReplyInfo] PRIMARY KEY CLUSTERED 
(
	[ReplyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2021/10/29 下午 10:36:51 ******/
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
SET IDENTITY_INSERT [dbo].[AnswerInfo] ON 

INSERT [dbo].[AnswerInfo] ([AnswerID], [ID], [QuestionID], [Answer]) VALUES (1, N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', 3, N'A')
INSERT [dbo].[AnswerInfo] ([AnswerID], [ID], [QuestionID], [Answer]) VALUES (2, N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', 3, N'B')
INSERT [dbo].[AnswerInfo] ([AnswerID], [ID], [QuestionID], [Answer]) VALUES (4, N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', 2, N'A')
INSERT [dbo].[AnswerInfo] ([AnswerID], [ID], [QuestionID], [Answer]) VALUES (5, N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', 2, N'B')
SET IDENTITY_INSERT [dbo].[AnswerInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[ListInfo] ON 

INSERT [dbo].[ListInfo] ([ListID], [ID], [ListName], [ListContent], [Status], [StartTime], [Endtime]) VALUES (2, N'8b4fa82d-57f8-4220-92fd-0a91bd0ffaa5', N'AAA', N'BBB', N'已結束', CAST(N'2021-10-24' AS Date), CAST(N'2021-10-26' AS Date))
INSERT [dbo].[ListInfo] ([ListID], [ID], [ListName], [ListContent], [Status], [StartTime], [Endtime]) VALUES (4, N'66687f41-98e8-481e-9756-15ee5c571bb6', N'ZZZ', N'XXX', N'投票中', CAST(N'2021-10-24' AS Date), CAST(N'2021-10-30' AS Date))
INSERT [dbo].[ListInfo] ([ListID], [ID], [ListName], [ListContent], [Status], [StartTime], [Endtime]) VALUES (3, N'4b538469-1cfd-4f43-8e4c-352d20d2fd37', N'AAAA', N'BBBB', N'投票中', CAST(N'2021-10-24' AS Date), CAST(N'2021-10-29' AS Date))
INSERT [dbo].[ListInfo] ([ListID], [ID], [ListName], [ListContent], [Status], [StartTime], [Endtime]) VALUES (1, N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', N'123', N'12345', N'投票中', CAST(N'2021-10-21' AS Date), CAST(N'2021-11-11' AS Date))
INSERT [dbo].[ListInfo] ([ListID], [ID], [ListName], [ListContent], [Status], [StartTime], [Endtime]) VALUES (5, N'c54c5196-d5ab-478d-83c7-70fd9955e7b6', N'ABC', N'ABC', N'未開放', CAST(N'2021-10-30' AS Date), CAST(N'2021-11-02' AS Date))
SET IDENTITY_INSERT [dbo].[ListInfo] OFF
GO
INSERT [dbo].[QuestionInfo] ([QuestionID], [ID], [QuestionName], [Type], [Required]) VALUES (1, N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', N'123', N'文字方塊', 0)
INSERT [dbo].[QuestionInfo] ([QuestionID], [ID], [QuestionName], [Type], [Required]) VALUES (2, N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', N'321', N'單選方塊', 0)
INSERT [dbo].[QuestionInfo] ([QuestionID], [ID], [QuestionName], [Type], [Required]) VALUES (3, N'97e6d3d4-a844-4714-9644-3e5cb9bd0a23', N'Choose One(必填)', N'多選方塊', 1)
GO
INSERT [dbo].[UserInfo] ([UserID], [Account], [PWD], [Name], [Email]) VALUES (N'785f3020-a505-4770-887a-dd8643b2ea76', N'XXX', N'12345', N'XXX', N'XXX@gmail.com')
GO
ALTER TABLE [dbo].[ListInfo] ADD  CONSTRAINT [DF_ListInfo_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_ID]  DEFAULT (newid()) FOR [UserID]
GO
