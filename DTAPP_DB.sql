USE [master]
GO

/****** Object:  Database [New_DTAPPV2]    Script Date: 2024-04-10 11:41:32 AM ******/
CREATE DATABASE [New_DTAPPV2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'New_DTAPPV2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.JIIFC\MSSQL\DATA\New_DTAPPV2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'New_DTAPPV2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.JIIFC\MSSQL\DATA\New_DTAPPV2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [New_DTAPPV2] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [New_DTAPPV2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [New_DTAPPV2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET ARITHABORT OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [New_DTAPPV2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [New_DTAPPV2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [New_DTAPPV2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [New_DTAPPV2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET RECOVERY FULL 
GO
ALTER DATABASE [New_DTAPPV2] SET  MULTI_USER 
GO
ALTER DATABASE [New_DTAPPV2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [New_DTAPPV2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [New_DTAPPV2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [New_DTAPPV2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [New_DTAPPV2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [New_DTAPPV2] SET QUERY_STORE = OFF
GO
USE [New_DTAPPV2]
GO
/****** Object:  User [New_DTAPPV2]    Script Date: 2024-07-22 8:45:50 AM ******/
CREATE USER [New_DTAPP] FOR LOGIN [New_DTAPP] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [New_DTAPP]
GO
ALTER ROLE [db_datareader] ADD MEMBER [New_DTAPP]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [New_DTAPP]
GO
/****** Object:  Table [dbo].[edit_file]    Script Date: 2024-07-22 8:45:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[edit_file](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[file_id] [int] NOT NULL,
	[transfer_id] [int] NOT NULL,
	[edit_date] [datetime] NOT NULL,
	[original] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[edit_transfer]    Script Date: 2024-07-22 8:45:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[edit_transfer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[edit_date] [datetime] NOT NULL,
	[original] [varchar](max) NOT NULL,
	[transfer_id] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[file]    Script Date: 2024-07-22 8:45:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[file](
	[file_id] [int] IDENTITY(1,1) NOT NULL,
	[file_name] [varchar](max) NULL,
	[file_size] [varchar](max) NOT NULL,
	[transfer_id] [int] NULL,
	[comment] [varchar](max) NULL,
	[file_sent] [bit] NOT NULL,
	[file_ext] [varchar](64) NOT NULL,
 CONSTRAINT [PK_file] PRIMARY KEY CLUSTERED 
(
	[file_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[operation]    Script Date: 2024-07-22 8:45:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[operation](
	[operation_id] [int] IDENTITY(1,1) NOT NULL,
	[operation_name] [varchar](max) NOT NULL,
	[archived] [bit] NOT NULL,
 CONSTRAINT [PK_operation] PRIMARY KEY CLUSTERED 
(
	[operation_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 2024-07-22 8:45:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [varchar](max) NOT NULL,
	[ordering] [int] NOT NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[system]    Script Date: 2024-07-22 8:45:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[system](
	[system_id] [int] IDENTITY(1,1) NOT NULL,
	[system_name] [varchar](max) NOT NULL,
	[archived] [bit] NOT NULL,
 CONSTRAINT [PK_system] PRIMARY KEY CLUSTERED 
(
	[system_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transfer]    Script Date: 2024-07-22 8:45:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transfer](
	[transfer_id] [int] IDENTITY(1,1) NOT NULL,
	[request_created_at] [datetime] NOT NULL,
	[sent_time] [datetime] NULL,
	[client_name] [varchar](max) NULL,
	[client_unit_id] [int] NULL,
	[operation_id] [int] NULL,
	[orig_system_id] [int] NOT NULL,
	[dest_system_id] [int] NOT NULL,
	[isso_approval] [bit] NOT NULL,
	[issue_reported] [bit] NOT NULL,
	[spill_prevented] [bit] NOT NULL,
	[spill_occurred] [bit] NOT NULL,
	[spill_id] [int] NULL,
	[comments] [varchar](max) NULL,
	[urgent] [bit] NOT NULL,
	[virus_definition_date] [datetime] NULL,
	[machine_name] [varchar](max) NULL,
	[reviewed] [bit] NOT NULL,
	[reviewed_user_id] [int] NULL,
	[reviewed_at] [datetime] NULL,
	[completed_user_id] [int] NULL,
	[completed_at] [datetime] NULL,
	[completed] [bit] NOT NULL,
	[transfer_type] [int] NOT NULL,
 CONSTRAINT [PK_transfer] PRIMARY KEY CLUSTERED 
(
	[transfer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transferType]    Script Date: 2024-07-22 8:45:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transferType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[transfer_type] [varchar](64) NOT NULL,
	[archived] [bit] NOT NULL,
	[ordering] [int] NOT NULL,
 CONSTRAINT [PK_transferType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[unit]    Script Date: 2024-07-22 8:45:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[unit](
	[unit_id] [int] IDENTITY(1,1) NOT NULL,
	[unit_name] [varchar](max) NOT NULL,
	[archived] [bit] NOT NULL,
 CONSTRAINT [PK_unit] PRIMARY KEY CLUSTERED 
(
	[unit_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 2024-07-22 8:45:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](max) NOT NULL,
	[first_name] [varchar](max) NOT NULL,
	[last_name] [varchar](max) NOT NULL,
	[title] [varchar](100) NULL,
	[email] [varchar](max) NOT NULL,
	[disabled] [bit] NOT NULL,
	[role_id] [int] NOT NULL,
	[alias] [varchar](max) NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[fileExtension]    Script Date: 2025-03-19 11:06:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fileExtension](
	[fileExtension_id] [int] IDENTITY(1,1) NOT NULL,
	[fileExtension_name] [varchar](max) NOT NULL,
	[archived] [bit] NOT NULL,
 CONSTRAINT [PK_fileExtension] PRIMARY KEY CLUSTERED 
(
	[fileExtension_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[spill]    Script Date: 2025-03-19 11:06:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[spill](
	[spill_id] [int] IDENTITY (1,1) NOT NULL,
	[spill_status_id] [int] NOT NULL,
	[cfnoc_incident_number] [varchar](max) NULL,
	[dgds_sim_incident_number] [varchar](max) NULL,
	[burned_and_annotated] [bit] NOT NULL,
	[isso_informed] [datetime] NULL,
	[manager_informed] [datetime] NULL,
	[nature_of_spill] [varchar](max) NULL,
	[transfer_request_completed] [bit] NOT NULL,
	[email_triple_deleted] [bit] NOT NULL,
	[client_informed] [bit] NOT NULL,
	[consideration_power_down] [bit] NOT NULL,
	[cdsent] [bit] NOT NULL,
	[date_of_spill] [datetime] NOT NULL,
	[time_of_spill] [datetime] NOT NULL,
	[time_identified_spill] [datetime] NOT NULL,
	[time_reported] [datetime] NOT NULL,
	[workstation_affected] [varchar](max) NULL,
	[workstation_asset_number] [varchar](max) NULL,
	[specialist_id] [int] NOT NULL,
	[reviewer_id] [int] NULL,
	[orig_system_id] [int] NOT NULL,
	[dest_system_id] [int] NOT NULL,
	[transfer_id] [int] NOT NULL,
 CONSTRAINT [PK_spill] PRIMARY KEY CLUSTERED
(
	[spill_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[spillStatus] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[spillStatus](
	[status_id] [int] IDENTITY(1,1) NOT NULL,
	[status] [varchar](MAX) NOT NULL,
	[archived] [bit] NOT NULL,
 CONSTRAINT [PK_spillStatus] PRIMARY KEY CLUSTERED 
(
	[status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/*DEFAULT DATA INSERT*/
SET IDENTITY_INSERT [dbo].[edit_file] ON 

INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (1, 234, 99, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"file_id":234,"file_name":"Manualy Added","file_size":"32 KB","transfer_id":99,"comment":"","file_sent":true,"file_ext":"pdf"}]')
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (2, 235, 99, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"file_id":235,"file_name":"Manualy Added","file_size":"32 KB","transfer_id":99,"comment":"","file_sent":true,"file_ext":"pdf"}]')
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (3, 236, 99, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"file_id":236,"file_name":"Manualy Added","file_size":"32 KB","transfer_id":99,"comment":"","file_sent":true,"file_ext":"pdf"}]')
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (4, 237, 99, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"file_id":237,"file_name":"","file_size":"32 KB","transfer_id":99,"comment":"","file_sent":true,"file_ext":"pdf"}]')
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (5, 238, 99, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"file_id":238,"file_name":"","file_size":"32 KB","transfer_id":99,"comment":"","file_sent":true,"file_ext":"pdf"}]')
SET IDENTITY_INSERT [dbo].[edit_file] OFF
GO
SET IDENTITY_INSERT [dbo].[edit_transfer] ON 

INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (6, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"transfer_id":99,"request_created_at":"2024-07-08T16:10:00","sent_time":"2024-07-09T16:10:00","client_name":"mcgee.s","client_unit_id":9,"orig_system_id":4,"dest_system_id":4,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:40.883","completed":false,"transfer_type":1}]', 99)
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (7, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"transfer_id":99,"request_created_at":"2024-07-08T16:10:00","sent_time":"2024-07-09T16:10:00","client_name":"mcgee.s","client_unit_id":8,"orig_system_id":4,"dest_system_id":3,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:40","completed":false,"transfer_type":1}]', 99)
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (8, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"transfer_id":99,"request_created_at":"2024-07-08T16:10:00","sent_time":"2024-07-09T16:10:00","client_name":"mcgee.s","client_unit_id":8,"orig_system_id":4,"dest_system_id":3,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:40","completed":false,"transfer_type":1}]', 99)
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (9, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"transfer_id":99,"request_created_at":"2024-07-08T16:10:00","sent_time":"2024-07-09T16:10:00","client_name":"mcgee.s","client_unit_id":8,"orig_system_id":4,"dest_system_id":3,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:40","completed":false,"transfer_type":1}]', 99)
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (10, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"transfer_id":99,"request_created_at":"2024-07-08T16:10:00","sent_time":"2024-07-09T16:10:00","client_name":"mcgee.s","client_unit_id":8,"orig_system_id":4,"dest_system_id":3,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:40","completed":false,"transfer_type":1}]', 99)
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (11, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"transfer_id":100,"request_created_at":"2024-07-08T16:10:00","sent_time":"2024-07-09T16:10:00","client_name":"Jess","client_unit_id":10,"orig_system_id":4,"dest_system_id":4,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:56.080","completed":false,"transfer_type":1}]', 100)
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (12, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"transfer_id":98,"request_created_at":"2024-07-08T16:09:00","sent_time":"2024-07-09T16:09:00","client_name":"butt.r","client_unit_id":8,"orig_system_id":4,"dest_system_id":3,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:01.983","completed":false,"transfer_type":1}]', 98)
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (13, CAST(N'2024-07-12T10:21:41.883' AS DateTime), N'[{"transfer_id":99,"request_created_at":"2024-07-08T16:10:00","sent_time":"2024-07-09T16:10:00","client_name":"mcgee.s","client_unit_id":8,"orig_system_id":4,"dest_system_id":3,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:40","completed":false,"transfer_type":1}]', 99)
SET IDENTITY_INSERT [dbo].[edit_transfer] OFF
GO
SET IDENTITY_INSERT [dbo].[file] ON 

INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (113, N'Program.cs', N'105 bytes', 63, N'fffffffffff', 0, N'.cs')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (141, N'20230702_135450.jpg', N'5.31 MB', 68, N'', 0, N'.jpg')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (142, N'20230702_135450.jpg', N'5.31 MB', 69, N'', 0, N'.jpg')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (143, N'20230710_072641-1 (1).mp4', N'1.37 MB', 70, N'', 1, N'.mp4')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (144, N'IMG-20240208-WA0004.jpg', N'167.87 KB', 71, N'Comment', 1, N'.jpg')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (147, N'ApprovedFileTypes (5).pdf', N'72.59 KB', 72, N'test', 1, N'.pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (149, N'ApprovedFileTypes (1).pdf', N'35.46 KB', 74, N'', 1, N'.pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (150, N'', N'34.51 KB', 75, N'', 1, N'.pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (151, N'', N'5.31 MB', 75, N'', 1, N'.jpg')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (152, N'', N'1.37 MB', 75, N'', 1, N'.xlsx')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (153, N'', N'1.37 MB', 75, N'', 1, N'.jpg')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (154, N'', N'5.13 MB', 75, N'', 1, N'.pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (161, N'', N'44 MB', 82, N'', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (164, N'', N'1.97 KB', 27, N'1', 0, N'.json')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (165, N'', N'1.12 KB', 27, N'2', 1, N'.props')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (166, N'', N'150 bytes', 27, N'3', 0, N'.targets')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (167, N'', N'1.88 KB', 27, N'4', 1, N'.json')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (168, N'', N'286 bytes', 27, N'5', 0, N'.cache')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (169, N'', N'32 MB', 83, N'', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (170, N'', N'44 MB', 83, N'', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (178, N'', N'32 MB', 85, N'', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (179, N'', N'32 MB', 85, N'', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (180, N'', N'44 KB', 88, N'', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (206, N'', N'32 KB', 89, N'', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (215, N'job.txt', N'104 bytes', 61, N'', 1, N'txt')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (222, N'', N'44 MB', 94, N'8', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (223, N'', N'44 MB', 94, N'not 8', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (224, N'', N'44 MB', 94, N'3', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (225, N'Manualy Added', N'32 bytes', 94, N'2', 1, N'exe')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (226, N'', N'44 MB', 92, N'5', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (227, N'', N'44 MB', 92, N'Test2', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (228, N'', N'44 MB', 92, N'Test3', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (229, N'Manualy Added', N'32 MB', 92, N'4', 1, N'png')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (230, N'', N'4 GB', 95, N'', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (233, N'', N'44 MB', 96, N'', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (239, N'Manualy Added', N'44 MB', 100, N'', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (240, N'Manualy Added', N'44 KB', 98, N'', 1, N'exe')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (241, N'', N'32 KB', 99, N'', 1, N'pdf')
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (242, N'', N'32 KB', 99, N'', 1, N'pdf')
SET IDENTITY_INSERT [dbo].[file] OFF
GO
SET IDENTITY_INSERT [dbo].[operation] ON 

INSERT [dbo].[operation] ([operation_id], [operation_name], [archived]) VALUES (1, N'operationsss', 0)
INSERT [dbo].[operation] ([operation_id], [operation_name], [archived]) VALUES (2, N'Zions', 1)
INSERT [dbo].[operation] ([operation_id], [operation_name], [archived]) VALUES (6, N'test', 0)
INSERT [dbo].[operation] ([operation_id], [operation_name], [archived]) VALUES (7, N'Laser', 0)
SET IDENTITY_INSERT [dbo].[operation] OFF
GO
SET IDENTITY_INSERT [dbo].[role] ON 

INSERT [dbo].[role] ([role_id], [role_name], [ordering]) VALUES (3, N'User', 1)
INSERT [dbo].[role] ([role_id], [role_name], [ordering]) VALUES (4, N'Admin', 3)
INSERT [dbo].[role] ([role_id], [role_name], [ordering]) VALUES (5, N'Supervisor', 2)
SET IDENTITY_INSERT [dbo].[role] OFF
GO
SET IDENTITY_INSERT [dbo].[system] ON 

INSERT [dbo].[system] ([system_id], [system_name], [archived]) VALUES (1, N'system', 0)
INSERT [dbo].[system] ([system_id], [system_name], [archived]) VALUES (2, N'JIIFC LAN', 0)
INSERT [dbo].[system] ([system_id], [system_name], [archived]) VALUES (3, N'DWAN', 0)
INSERT [dbo].[system] ([system_id], [system_name], [archived]) VALUES (4, N'CSNI', 0)
INSERT [dbo].[system] ([system_id], [system_name], [archived]) VALUES (6, N'New Sys444', 0)
SET IDENTITY_INSERT [dbo].[system] OFF
GO
SET IDENTITY_INSERT [dbo].[transfer] ON 

INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (19, CAST(N'2023-09-22T09:20:49.000' AS DateTime), NULL, N'hendley.j', 1, 1, 2, 1, 0, 0, 1, 0, NULL, 0, NULL, NULL, 0, 2, NULL, 2, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (27, CAST(N'2024-02-26T01:39:42.000' AS DateTime), CAST(N'2024-02-25T02:43:00.000' AS DateTime), N'Alain', 1, 1, 2, 1, 1, 1, 1, 1, N'Commentasss', 1, NULL, NULL, 0, 3, NULL, 1, CAST(N'2024-02-28T01:56:00.000' AS DateTime), 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (49, CAST(N'2024-03-22T09:11:40.000' AS DateTime), NULL, N'Joe ', 1, 1, 1, 2, 0, 0, 0, 0, N'None', 0, NULL, NULL, 0, NULL, NULL, 3, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (55, CAST(N'2024-03-22T10:28:44.590' AS DateTime), NULL, N'Joe', 2, 1, 2, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 3, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (56, CAST(N'2024-03-22T10:36:56.563' AS DateTime), NULL, N'Joe b', 1, 1, 2, 1, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 3, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (60, CAST(N'2024-03-22T10:49:31.943' AS DateTime), NULL, N'asd', 1, 6, 1, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 3, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (61, CAST(N'2024-03-22T10:53:40.000' AS DateTime), NULL, N'Ad2', 2, 1, 1, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 3, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (62, CAST(N'2024-03-22T11:00:08.597' AS DateTime), NULL, N'Ad2', 2, 1, 1, 4, 0, 0, 0, 0, N'None', 0, NULL, NULL, 0, NULL, NULL, 3, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (63, CAST(N'2024-03-22T11:02:19.000' AS DateTime), NULL, N'Joe ', 1, 6, 1, 2, 0, 0, 0, 0, N'hhhh', 0, NULL, NULL, 0, NULL, NULL, 3, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (64, CAST(N'2024-03-22T11:07:11.190' AS DateTime), NULL, N'Ad2', 2, 6, 1, 2, 0, 0, 1, 1, N'None', 0, NULL, NULL, 0, 1, NULL, 3, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (68, CAST(N'2024-03-26T10:43:47.787' AS DateTime), NULL, N'butt.r', 1, 6, 1, 2, 0, 0, 0, 0, N'test 1', 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (69, CAST(N'2024-03-26T10:56:27.697' AS DateTime), CAST(N'2024-03-27T10:57:00.000' AS DateTime), N'butt.r', 2, NULL, 1, 2, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (70, CAST(N'2024-03-26T11:00:38.800' AS DateTime), NULL, N'butt.r', 2, NULL, 1, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (71, CAST(N'2024-03-26T11:01:57.477' AS DateTime), NULL, N'mcgee.s', 2, NULL, 1, 3, 1, 1, 1, 1, NULL, 1, NULL, NULL, 1, 1, NULL, 2, NULL, 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (72, CAST(N'2024-04-03T12:28:59.863' AS DateTime), NULL, N'butt.r', 1, NULL, 1, 2, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (73, CAST(N'2024-04-03T13:29:42.840' AS DateTime), NULL, N'mcgee.s', 3, NULL, 1, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-03T13:29:42.900' AS DateTime), 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (74, CAST(N'2024-04-03T14:21:44.250' AS DateTime), CAST(N'2024-04-02T14:21:00.000' AS DateTime), N'Jess', 2, NULL, 1, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (75, CAST(N'2024-04-10T14:12:08.580' AS DateTime), CAST(N'2024-04-09T14:11:00.000' AS DateTime), N'butt.r', 2, NULL, 1, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-10T14:12:08.763' AS DateTime), 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (76, CAST(N'2024-04-11T02:33:48.000' AS DateTime), NULL, N'butt.r', 2, 1, 1, 1, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (77, CAST(N'2024-04-17T10:00:36.113' AS DateTime), CAST(N'2024-04-15T10:00:00.000' AS DateTime), N'mcgee.s', 2, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (78, CAST(N'2024-04-17T10:08:45.840' AS DateTime), CAST(N'2024-04-16T10:08:00.000' AS DateTime), N'mcgee.s', 2, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (79, CAST(N'2024-04-17T10:15:18.493' AS DateTime), CAST(N'2024-04-16T10:08:00.000' AS DateTime), N'mcgee.s', 2, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (80, CAST(N'2024-04-17T10:25:34.947' AS DateTime), CAST(N'2024-04-16T10:08:00.000' AS DateTime), N'mcgee.s', 2, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (81, CAST(N'2024-04-17T10:29:30.413' AS DateTime), NULL, N'mcgee.s', 2, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-17T10:29:39.317' AS DateTime), 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (82, CAST(N'2024-04-17T10:33:51.663' AS DateTime), NULL, N'mcgee.s', 2, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-17T10:33:54.640' AS DateTime), 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (83, CAST(N'2024-04-17T10:36:05.000' AS DateTime), NULL, N'Jess', 3, 1, 4, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-17T10:36:08.000' AS DateTime), 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (84, CAST(N'2024-04-17T11:27:19.000' AS DateTime), NULL, N'Mahdi', 1, 1, 1, 1, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-17T11:27:19.000' AS DateTime), 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (85, CAST(N'2024-04-17T10:24:00.000' AS DateTime), CAST(N'2024-04-18T10:24:00.000' AS DateTime), N'mcgee.s', 2, NULL, 4, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-19T10:25:02.720' AS DateTime), 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (86, CAST(N'2024-04-19T10:32:55.000' AS DateTime), CAST(N'2024-04-19T10:35:00.000' AS DateTime), N'Jess', 3, NULL, 1, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-19T10:33:27.440' AS DateTime), 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (88, CAST(N'2024-04-21T15:19:00.000' AS DateTime), CAST(N'2024-04-22T15:19:00.000' AS DateTime), N'buttter', 3, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-23T15:20:54.250' AS DateTime), 1, 2)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (89, CAST(N'2024-05-09T11:26:00.000' AS DateTime), CAST(N'2024-05-09T14:28:00.000' AS DateTime), N'mcgee.s', 2, 6, 1, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-05-10T01:25:31.000' AS DateTime), 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (90, CAST(N'2024-05-15T08:58:00.000' AS DateTime), CAST(N'2024-05-16T10:02:00.000' AS DateTime), N'Jess', 3, NULL, 1, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-05-16T12:59:11.000' AS DateTime), 1, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (91, CAST(N'2024-05-19T06:38:00.000' AS DateTime), CAST(N'2024-05-20T06:38:00.000' AS DateTime), N'Jess', 2, NULL, 4, 4, 0, 0, 0, 0, N'fdklafjdaskl', 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-05-21T02:38:48.000' AS DateTime), 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (92, CAST(N'2024-05-19T06:49:00.000' AS DateTime), CAST(N'2024-05-20T06:50:00.000' AS DateTime), N'Mahdi', 1, 7, 4, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, 2, NULL, 2, CAST(N'2024-05-21T02:50:21.000' AS DateTime), 0, 2)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (94, CAST(N'2024-06-13T12:27:00.000' AS DateTime), CAST(N'2024-06-14T12:27:00.000' AS DateTime), N'Jess', 1, 7, 4, 4, 0, 0, 0, 1, NULL, 0, NULL, NULL, 0, 2, NULL, 2, CAST(N'2024-06-14T08:28:37.000' AS DateTime), 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (95, CAST(N'2024-06-18T15:48:00.000' AS DateTime), CAST(N'2024-06-19T15:48:00.000' AS DateTime), N'butt.r', 2, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-06-20T11:48:41.157' AS DateTime), 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (96, CAST(N'2024-06-24T05:23:00.000' AS DateTime), CAST(N'2024-06-24T10:23:00.000' AS DateTime), N'butt.r', 3, NULL, 4, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-06-24T10:49:03.000' AS DateTime), 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (97, CAST(N'2024-06-24T02:59:00.000' AS DateTime), CAST(N'2024-06-25T03:59:00.000' AS DateTime), N'butt.r', 2, NULL, 4, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-06-24T11:00:08.000' AS DateTime), 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (98, CAST(N'2024-07-08T16:09:00.000' AS DateTime), CAST(N'2024-07-09T16:09:00.000' AS DateTime), N'butt.r', 8, NULL, 4, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-07-09T12:10:01.000' AS DateTime), 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (99, CAST(N'2024-07-08T16:10:00.000' AS DateTime), CAST(N'2024-07-09T16:10:00.000' AS DateTime), N'mcgee.s', 8, NULL, 4, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-07-09T12:10:40.000' AS DateTime), 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (100, CAST(N'2024-07-08T16:10:00.000' AS DateTime), CAST(N'2024-07-09T16:10:00.000' AS DateTime), N'Jess', 10, NULL, 4, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-07-09T12:10:56.000' AS DateTime), 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (101, CAST(N'2024-07-07T16:12:00.000' AS DateTime), CAST(N'2024-07-08T16:12:00.000' AS DateTime), N'Mahdi', 11, NULL, 4, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-07-09T12:12:12.057' AS DateTime), 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (102, CAST(N'2024-07-04T16:12:00.000' AS DateTime), CAST(N'2024-07-05T16:12:00.000' AS DateTime), N'buttter', 12, NULL, 4, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-07-09T12:12:25.633' AS DateTime), 0, 1)
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type]) VALUES (104, CAST(N'2024-07-03T16:12:00.000' AS DateTime), CAST(N'2024-07-09T16:12:00.000' AS DateTime), N'butt.r', 15, NULL, 4, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-07-09T12:12:55.837' AS DateTime), 0, 1)
SET IDENTITY_INSERT [dbo].[transfer] OFF
GO
SET IDENTITY_INSERT [dbo].[transferType] ON 

INSERT [dbo].[transferType] ([id], [transfer_type], [archived], [ordering]) VALUES (1, N'Transfer Request', 0, 1)
INSERT [dbo].[transferType] ([id], [transfer_type], [archived], [ordering]) VALUES (2, N'Standing Transfer', 0, 2)
INSERT [dbo].[transferType] ([id], [transfer_type], [archived], [ordering]) VALUES (3, N'Bulk Transfer', 0, 3)
INSERT [dbo].[transferType] ([id], [transfer_type], [archived], [ordering]) VALUES (4, N'Project', 1, 4)
INSERT [dbo].[transferType] ([id], [transfer_type], [archived], [ordering]) VALUES (5, N'Test', 0, 5)
SET IDENTITY_INSERT [dbo].[transferType] OFF
GO
SET IDENTITY_INSERT [dbo].[unit] ON 

INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (1, N'unit1u', 0)
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (2, N'JIIFC', 0)
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (3, N'CJOC', 0)
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (7, N'New one the is archived', 1)
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (8, N'a', 0)
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (9, N'b', 0)
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (10, N'c', 0)
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (11, N'd', 0)
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (12, N'e', 0)
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (13, N'f', 0)
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (14, N'SJS', 0)
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (15, N'g', 0)
SET IDENTITY_INSERT [dbo].[unit] OFF
GO
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([user_id], [username], [first_name], [last_name], [title], [email], [disabled], [role_id], [alias]) VALUES (1, N'hendley.j', N'Jonathan', N'Hendley', NULL, N'hendley.j@JIIFC.MIL.CA', 0, 3, NULL)
INSERT [dbo].[user] ([user_id], [username], [first_name], [last_name], [title], [email], [disabled], [role_id], [alias]) VALUES (2, N'mcgee.s', N'Stephen', N'McGee', NULL, N'mcgee.s@JIIFC.MIL.CA', 0, 4, NULL)
INSERT [dbo].[user] ([user_id], [username], [first_name], [last_name], [title], [email], [disabled], [role_id], [alias]) VALUES (3, N'lalonde.a', N'Alain', N'Lalonde', NULL, N'lalonde.a@JIIFC.MIL.CA', 0, 3, NULL)
INSERT [dbo].[user] ([user_id], [username], [first_name], [last_name], [title], [email], [disabled], [role_id], [alias]) VALUES (4, N'Ball.BM', N'Blake', N'Ball', NULL, N'Ball.BM@JIIFC.MIL.CA', 0, 4, NULL)
INSERT [dbo].[user] ([user_id], [username], [first_name], [last_name], [title], [email], [disabled], [role_id], [alias]) VALUES (5, N'Winder.DR', N'Damen', N'Winder', NULL, N'Winder.DR@JIIFC.MIL.CA', 0, 3, NULL)
INSERT [dbo].[user] ([user_id], [username], [first_name], [last_name], [title], [email], [disabled], [role_id], [alias]) VALUES (6, N'danic', N'Danick', N'Roussel', NULL, N'danick.roussel@forces.gc.ca', 0, 4, NULL)
--INSERT [dbo].[user] ([user_id], [username], [first_name], [last_name], [title], [email], [disabled], [role_id], [alias]) VALUES (6, N'danic', N'Danick', N'Roussel', NULL, N'danick.roussel@forces.gc.ca', 0, 4, NULL)
SET IDENTITY_INSERT [dbo].[user] OFF
GO
SET IDENTITY_INSERT [dbo].[spillStatus] ON
INSERT [dbo].[spillStatus] ([status_id], [status], [archived]) VALUES (1, 'Under Investigation', 0)
INSERT [dbo].[spillStatus] ([status_id], [status], [archived]) VALUES (2, 'Report Received', 0)
INSERT [dbo].[spillStatus] ([status_id], [status], [archived]) VALUES (3, 'No Spill Occurred', 0)
SET IDENTITY_INSERT [dbo].[spillStatus] OFF
GO
ALTER TABLE [dbo].[transfer] ADD  CONSTRAINT [DF_transfer_reviewed]  DEFAULT ((0)) FOR [reviewed]
GO
ALTER TABLE [dbo].[transfer] ADD  CONSTRAINT [DF_transfer_completed]  DEFAULT ((0)) FOR [completed]
GO
ALTER TABLE [dbo].[transferType] ADD  CONSTRAINT [DF_transferType_archived]  DEFAULT ((0)) FOR [archived]
GO
ALTER TABLE [dbo].[transferType] ADD  CONSTRAINT [DF_transferType_ordering]  DEFAULT ((1)) FOR [ordering]
GO
ALTER TABLE [dbo].[file]  WITH CHECK ADD  CONSTRAINT [FK_file_transfer] FOREIGN KEY([transfer_id])
REFERENCES [dbo].[transfer] ([transfer_id])
GO
ALTER TABLE [dbo].[file] CHECK CONSTRAINT [FK_file_transfer]
GO
ALTER TABLE [dbo].[transfer]  WITH CHECK ADD  CONSTRAINT [FK_transfer_operation] FOREIGN KEY([operation_id])
REFERENCES [dbo].[operation] ([operation_id])
GO
ALTER TABLE [dbo].[transfer] CHECK CONSTRAINT [FK_transfer_operation]
GO
ALTER TABLE [dbo].[transfer]  WITH CHECK ADD  CONSTRAINT [FK_transfer_system] FOREIGN KEY([orig_system_id])
REFERENCES [dbo].[system] ([system_id])
GO
ALTER TABLE [dbo].[transfer] CHECK CONSTRAINT [FK_transfer_system]
GO
ALTER TABLE [dbo].[transfer]  WITH CHECK ADD  CONSTRAINT [FK_transfer_system1] FOREIGN KEY([dest_system_id])
REFERENCES [dbo].[system] ([system_id])
GO
ALTER TABLE [dbo].[transfer] CHECK CONSTRAINT [FK_transfer_system1]
GO
ALTER TABLE [dbo].[transfer]  WITH CHECK ADD  CONSTRAINT [FK_transfer_transfer_type] FOREIGN KEY([transfer_type])
REFERENCES [dbo].[transferType] ([id])
GO
ALTER TABLE [dbo].[transfer] CHECK CONSTRAINT [FK_transfer_transfer_type]
GO
ALTER TABLE [dbo].[transfer]  WITH CHECK ADD  CONSTRAINT [FK_transfer_unit] FOREIGN KEY([client_unit_id])
REFERENCES [dbo].[unit] ([unit_id])
GO
ALTER TABLE [dbo].[transfer] CHECK CONSTRAINT [FK_transfer_unit]
GO
ALTER TABLE [dbo].[transfer]  WITH CHECK ADD  CONSTRAINT [FK_transfer_user] FOREIGN KEY([completed_user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[transfer] CHECK CONSTRAINT [FK_transfer_user]
GO
ALTER TABLE [dbo].[transfer]  WITH CHECK ADD  CONSTRAINT [FK_transfer_user1] FOREIGN KEY([reviewed_user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[transfer] CHECK CONSTRAINT [FK_transfer_user1]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_role] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([role_id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_role]
--GO
--ALTER TABLE [dbo].[transfer]  WITH CHECK ADD  CONSTRAINT [FK_spill] FOREIGN KEY([spill_id])
--REFERENCES [dbo].[spill] ([spill_id])
--GO
--ALTER TABLE [dbo].[transfer] CHECK CONSTRAINT [FK_spill]
--GO
--ALTER TABLE [dbo].[spill]  WITH CHECK ADD  CONSTRAINT [FK_spill1] FOREIGN KEY([transfer_id])
--REFERENCES [dbo].[transfer] ([transfer_id])
--GO
--ALTER TABLE [dbo].[spill] CHECK CONSTRAINT [FK_spill1]
GO
ALTER TABLE [dbo].[spill]  WITH CHECK ADD  CONSTRAINT [FK_spill_user] FOREIGN KEY([specialist_id]) -- Completed user
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[spill] CHECK CONSTRAINT [FK_spill_user]
GO
ALTER TABLE [dbo].[spill]  WITH CHECK ADD  CONSTRAINT [FK_spill_user1] FOREIGN KEY([reviewer_id]) -- Reviewer user
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[spill] CHECK CONSTRAINT [FK_spill_user1]
GO
ALTER TABLE [dbo].[spill]  WITH CHECK ADD  CONSTRAINT [FK_spill_system] FOREIGN KEY([orig_system_id]) -- Originating System
REFERENCES [dbo].[system] ([system_id])
GO
ALTER TABLE [dbo].[spill] CHECK CONSTRAINT [FK_spill_system]
GO
ALTER TABLE [dbo].[spill]  WITH CHECK ADD  CONSTRAINT [FK_spill_system1] FOREIGN KEY([dest_system_id]) -- Destination System
REFERENCES [dbo].[system] ([system_id])
GO
ALTER TABLE [dbo].[spill] CHECK CONSTRAINT [FK_spill_system1]
GO
ALTER TABLE [dbo].[spill]  WITH CHECK ADD  CONSTRAINT [FK_spill_status] FOREIGN KEY([spill_status_id]) -- Spill Status
REFERENCES [dbo].[spillStatus] ([status_id])
GO
ALTER TABLE [dbo].[spill] CHECK CONSTRAINT [FK_spill_status]
GO
USE [master]
GO
ALTER DATABASE [New_DTAPPV2] SET  READ_WRITE 
GO