USE [master]
GO
/****** Object:  Database [New_DTAPP]    Script Date: 11/08/2025 13:33:12 ******/
CREATE DATABASE [New_DTAPP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'New_DTAPP', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.JIIFC\MSSQL\DATA\New_DTAPP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'New_DTAPP_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.JIIFC\MSSQL\DATA\New_DTAPP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [New_DTAPP] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [New_DTAPP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [New_DTAPP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [New_DTAPP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [New_DTAPP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [New_DTAPP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [New_DTAPP] SET ARITHABORT OFF 
GO
ALTER DATABASE [New_DTAPP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [New_DTAPP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [New_DTAPP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [New_DTAPP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [New_DTAPP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [New_DTAPP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [New_DTAPP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [New_DTAPP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [New_DTAPP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [New_DTAPP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [New_DTAPP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [New_DTAPP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [New_DTAPP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [New_DTAPP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [New_DTAPP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [New_DTAPP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [New_DTAPP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [New_DTAPP] SET RECOVERY FULL 
GO
ALTER DATABASE [New_DTAPP] SET  MULTI_USER 
GO
ALTER DATABASE [New_DTAPP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [New_DTAPP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [New_DTAPP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [New_DTAPP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [New_DTAPP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [New_DTAPP] SET QUERY_STORE = OFF
GO
USE [New_DTAPP]
GO
/****** Object:  User [New_DTAPP]    Script Date: 11/08/2025 13:33:13 ******/
CREATE USER [New_DTAPP] FOR LOGIN [New_DTAPP] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [New_DTAPP]
GO
ALTER ROLE [db_datareader] ADD MEMBER [New_DTAPP]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [New_DTAPP]
GO
/****** Object:  Table [dbo].[edit_file]    Script Date: 11/08/2025 13:33:14 ******/
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
/****** Object:  Table [dbo].[edit_spill]    Script Date: 11/08/2025 13:33:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[edit_spill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[edit_date] [datetime] NOT NULL,
	[original] [varchar](max) NOT NULL,
	[spill_id] [int] NOT NULL,
	[transfer_id] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[edit_transfer]    Script Date: 11/08/2025 13:33:14 ******/
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
/****** Object:  Table [dbo].[file]    Script Date: 11/08/2025 13:33:14 ******/
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
/****** Object:  Table [dbo].[fileExtension]    Script Date: 11/08/2025 13:33:14 ******/
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
/****** Object:  Table [dbo].[operation]    Script Date: 11/08/2025 13:33:14 ******/
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
/****** Object:  Table [dbo].[role]    Script Date: 11/08/2025 13:33:14 ******/
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
/****** Object:  Table [dbo].[spill]    Script Date: 11/08/2025 13:33:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[spill](
	[spill_id] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[spillStatus]    Script Date: 11/08/2025 13:33:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[spillStatus](
	[status_id] [int] IDENTITY(1,1) NOT NULL,
	[status] [varchar](max) NOT NULL,
	[archived] [bit] NOT NULL,
 CONSTRAINT [PK_spillStatus] PRIMARY KEY CLUSTERED 
(
	[status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[system]    Script Date: 11/08/2025 13:33:14 ******/
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
/****** Object:  Table [dbo].[transfer]    Script Date: 11/08/2025 13:33:14 ******/
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
	[spill_id] [int] NULL,
 CONSTRAINT [PK_transfer] PRIMARY KEY CLUSTERED 
(
	[transfer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transferType]    Script Date: 11/08/2025 13:33:14 ******/
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
/****** Object:  Table [dbo].[unit]    Script Date: 11/08/2025 13:33:14 ******/
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
/****** Object:  Table [dbo].[user]    Script Date: 11/08/2025 13:33:14 ******/
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
SET IDENTITY_INSERT [dbo].[edit_file] ON 
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (1, 234, 99, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"file_id":234,"file_name":"Manualy Added","file_size":"32 KB","transfer_id":99,"comment":"","file_sent":true,"file_ext":"pdf"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (2, 235, 99, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"file_id":235,"file_name":"Manualy Added","file_size":"32 KB","transfer_id":99,"comment":"","file_sent":true,"file_ext":"pdf"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (3, 236, 99, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"file_id":236,"file_name":"Manualy Added","file_size":"32 KB","transfer_id":99,"comment":"","file_sent":true,"file_ext":"pdf"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (4, 237, 99, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"file_id":237,"file_name":"","file_size":"32 KB","transfer_id":99,"comment":"","file_sent":true,"file_ext":"pdf"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (5, 238, 99, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"file_id":238,"file_name":"","file_size":"32 KB","transfer_id":99,"comment":"","file_sent":true,"file_ext":"pdf"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (6, 245, 107, CAST(N'2025-08-11T09:27:34.087' AS DateTime), N'[{"file_id":245,"file_name":"","file_size":"44 bytes","transfer_id":107,"comment":"","file_sent":true,"file_ext":"pdf"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (7, 164, 27, CAST(N'2025-08-11T13:12:26.417' AS DateTime), N'[{"file_id":164,"file_name":"","file_size":"1.97 KB","transfer_id":27,"comment":"1","file_sent":false,"file_ext":".json"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (8, 165, 27, CAST(N'2025-08-11T13:12:26.430' AS DateTime), N'[{"file_id":165,"file_name":"","file_size":"1.12 KB","transfer_id":27,"comment":"2","file_sent":true,"file_ext":".props"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (9, 166, 27, CAST(N'2025-08-11T13:12:26.430' AS DateTime), N'[{"file_id":166,"file_name":"","file_size":"150 bytes","transfer_id":27,"comment":"3","file_sent":false,"file_ext":".targets"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (10, 167, 27, CAST(N'2025-08-11T13:12:26.447' AS DateTime), N'[{"file_id":167,"file_name":"","file_size":"1.88 KB","transfer_id":27,"comment":"4","file_sent":true,"file_ext":".json"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (11, 168, 27, CAST(N'2025-08-11T13:12:26.463' AS DateTime), N'[{"file_id":168,"file_name":"","file_size":"286 bytes","transfer_id":27,"comment":"5","file_sent":false,"file_ext":".cache"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (12, 247, 27, CAST(N'2025-08-11T13:13:08.997' AS DateTime), N'[{"file_id":247,"file_name":"","file_size":"1.97 KB","transfer_id":27,"comment":"1","file_sent":false,"file_ext":".json"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (13, 248, 27, CAST(N'2025-08-11T13:13:09.013' AS DateTime), N'[{"file_id":248,"file_name":"","file_size":"1.12 KB","transfer_id":27,"comment":"2","file_sent":true,"file_ext":".props"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (14, 249, 27, CAST(N'2025-08-11T13:13:09.030' AS DateTime), N'[{"file_id":249,"file_name":"","file_size":"150 bytes","transfer_id":27,"comment":"3","file_sent":false,"file_ext":".targets"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (15, 250, 27, CAST(N'2025-08-11T13:13:09.030' AS DateTime), N'[{"file_id":250,"file_name":"","file_size":"1.88 KB","transfer_id":27,"comment":"4","file_sent":true,"file_ext":".json"}]')
GO
INSERT [dbo].[edit_file] ([id], [file_id], [transfer_id], [edit_date], [original]) VALUES (16, 251, 27, CAST(N'2025-08-11T13:13:09.043' AS DateTime), N'[{"file_id":251,"file_name":"","file_size":"286 bytes","transfer_id":27,"comment":"5","file_sent":false,"file_ext":".cache"}]')
GO
SET IDENTITY_INSERT [dbo].[edit_file] OFF
GO
SET IDENTITY_INSERT [dbo].[edit_spill] ON 
GO
INSERT [dbo].[edit_spill] ([id], [edit_date], [original], [spill_id], [transfer_id]) VALUES (18, CAST(N'2025-08-11T13:15:29.523' AS DateTime), N'[{"spill_id":18,"spill_status_id":1,"cfnoc_incident_number":"1234","dgds_sim_incident_number":"1234","burned_and_annotated":true,"isso_informed":"2025-07-01T00:00:00","manager_informed":"2025-07-01T00:00:00","nature_of_spill":"Testing Purposes","transfer_request_completed":true,"email_triple_deleted":true,"client_informed":true,"consideration_power_down":true,"cdsent":true,"date_of_spill":"2025-07-01T00:00:00","time_of_spill":"2025-07-01T00:00:00","time_identified_spill":"2025-07-01T00:00:00","time_reported":"2025-07-01T00:00:00","workstation_affected":"1234","workstation_asset_number":"1234","specialist_id":9,"reviewer_id":7,"orig_system_id":2,"dest_system_id":4,"transfer_id":27}]', 18, 27)
GO
SET IDENTITY_INSERT [dbo].[edit_spill] OFF
GO
SET IDENTITY_INSERT [dbo].[edit_transfer] ON 
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (6, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"transfer_id":99,"request_created_at":"2024-07-08T16:10:00","sent_time":"2024-07-09T16:10:00","client_name":"mcgee.s","client_unit_id":9,"orig_system_id":4,"dest_system_id":4,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:40.883","completed":false,"transfer_type":1}]', 99)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (7, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"transfer_id":99,"request_created_at":"2024-07-08T16:10:00","sent_time":"2024-07-09T16:10:00","client_name":"mcgee.s","client_unit_id":8,"orig_system_id":4,"dest_system_id":3,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:40","completed":false,"transfer_type":1}]', 99)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (8, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"transfer_id":99,"request_created_at":"2024-07-08T16:10:00","sent_time":"2024-07-09T16:10:00","client_name":"mcgee.s","client_unit_id":8,"orig_system_id":4,"dest_system_id":3,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:40","completed":false,"transfer_type":1}]', 99)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (9, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"transfer_id":99,"request_created_at":"2024-07-08T16:10:00","sent_time":"2024-07-09T16:10:00","client_name":"mcgee.s","client_unit_id":8,"orig_system_id":4,"dest_system_id":3,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:40","completed":false,"transfer_type":1}]', 99)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (10, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"transfer_id":99,"request_created_at":"2024-07-08T16:10:00","sent_time":"2024-07-09T16:10:00","client_name":"mcgee.s","client_unit_id":8,"orig_system_id":4,"dest_system_id":3,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:40","completed":false,"transfer_type":1}]', 99)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (11, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"transfer_id":100,"request_created_at":"2024-07-08T16:10:00","sent_time":"2024-07-09T16:10:00","client_name":"Jess","client_unit_id":10,"orig_system_id":4,"dest_system_id":4,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:56.080","completed":false,"transfer_type":1}]', 100)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (12, CAST(N'2024-07-12T00:00:00.000' AS DateTime), N'[{"transfer_id":98,"request_created_at":"2024-07-08T16:09:00","sent_time":"2024-07-09T16:09:00","client_name":"butt.r","client_unit_id":8,"orig_system_id":4,"dest_system_id":3,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:01.983","completed":false,"transfer_type":1}]', 98)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (13, CAST(N'2024-07-12T10:21:41.883' AS DateTime), N'[{"transfer_id":99,"request_created_at":"2024-07-08T16:10:00","sent_time":"2024-07-09T16:10:00","client_name":"mcgee.s","client_unit_id":8,"orig_system_id":4,"dest_system_id":3,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2024-07-09T12:10:40","completed":false,"transfer_type":1}]', 99)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (14, CAST(N'2025-08-11T09:27:34.510' AS DateTime), N'[{"transfer_id":107,"request_created_at":"2025-04-15T17:17:00","sent_time":"2025-04-29T17:17:00","client_name":"mcgee.s","client_unit_id":10,"orig_system_id":4,"dest_system_id":2,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":false,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2025-04-10T13:18:05.347","completed":false,"transfer_type":1}]', 107)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (15, CAST(N'2025-08-11T09:28:09.787' AS DateTime), N'[{"transfer_id":107,"request_created_at":"2025-04-15T17:17:00","sent_time":"2025-04-29T17:17:00","client_name":"mcgee.s","client_unit_id":10,"orig_system_id":4,"dest_system_id":2,"isso_approval":false,"issue_reported":false,"spill_prevented":false,"spill_occurred":true,"urgent":false,"reviewed":false,"completed_user_id":2,"completed_at":"2025-04-10T01:18:05","completed":false,"transfer_type":1}]', 107)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (16, CAST(N'2025-08-11T09:32:00.383' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (17, CAST(N'2025-08-11T09:32:00.403' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (18, CAST(N'2025-08-11T09:33:20.587' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (19, CAST(N'2025-08-11T10:50:07.357' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (20, CAST(N'2025-08-11T11:12:46.100' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1,"spill_id":3}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (21, CAST(N'2025-08-11T11:31:42.740' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (22, CAST(N'2025-08-11T11:32:00.507' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1,"spill_id":3}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (23, CAST(N'2025-08-11T11:48:42.117' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (24, CAST(N'2025-08-11T11:48:42.133' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1,"spill_id":6}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (25, CAST(N'2025-08-11T11:49:39.220' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1,"spill_id":7}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (26, CAST(N'2025-08-11T11:59:08.300' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (27, CAST(N'2025-08-11T11:59:21.973' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1,"spill_id":6}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (28, CAST(N'2025-08-11T13:03:04.460' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (29, CAST(N'2025-08-11T13:03:13.943' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1,"spill_id":8}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (30, CAST(N'2025-08-11T13:12:52.687' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T01:39:42","sent_time":"2024-02-25T02:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (31, CAST(N'2025-08-11T13:13:09.060' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T06:39:42","sent_time":"2024-02-27T07:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":false,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (32, CAST(N'2025-08-11T13:13:19.503' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T06:39:42","sent_time":"2024-02-27T07:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (33, CAST(N'2025-08-11T13:13:22.447' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T06:39:42","sent_time":"2024-02-27T07:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1,"spill_id":18}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (34, CAST(N'2025-08-11T13:15:29.537' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T06:39:42","sent_time":"2024-02-27T07:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1}]', 27)
GO
INSERT [dbo].[edit_transfer] ([id], [edit_date], [original], [transfer_id]) VALUES (35, CAST(N'2025-08-11T13:15:32.660' AS DateTime), N'[{"transfer_id":27,"request_created_at":"2024-02-26T06:39:42","sent_time":"2024-02-27T07:43:00","client_name":"Alain","client_unit_id":1,"operation_id":1,"orig_system_id":2,"dest_system_id":1,"isso_approval":true,"issue_reported":true,"spill_prevented":true,"spill_occurred":true,"comments":"Commentasss","urgent":true,"reviewed":false,"reviewed_user_id":3,"completed_user_id":1,"completed_at":"2024-02-28T01:56:00","completed":false,"transfer_type":1,"spill_id":18}]', 27)
GO
SET IDENTITY_INSERT [dbo].[edit_transfer] OFF
GO
SET IDENTITY_INSERT [dbo].[file] ON 
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (113, N'Program.cs', N'105 bytes', 63, N'fffffffffff', 0, N'.cs')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (141, N'20230702_135450.jpg', N'5.31 MB', 68, N'', 0, N'.jpg')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (142, N'20230702_135450.jpg', N'5.31 MB', 69, N'', 0, N'.jpg')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (143, N'20230710_072641-1 (1).mp4', N'1.37 MB', 70, N'', 1, N'.mp4')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (144, N'IMG-20240208-WA0004.jpg', N'167.87 KB', 71, N'Comment', 1, N'.jpg')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (147, N'ApprovedFileTypes (5).pdf', N'72.59 KB', 72, N'test', 1, N'.pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (149, N'ApprovedFileTypes (1).pdf', N'35.46 KB', 74, N'', 1, N'.pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (150, N'', N'34.51 KB', 75, N'', 1, N'.pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (151, N'', N'5.31 MB', 75, N'', 1, N'.jpg')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (152, N'', N'1.37 MB', 75, N'', 1, N'.xlsx')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (153, N'', N'1.37 MB', 75, N'', 1, N'.jpg')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (154, N'', N'5.13 MB', 75, N'', 1, N'.pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (161, N'', N'44 MB', 82, N'', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (169, N'', N'32 MB', 83, N'', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (170, N'', N'44 MB', 83, N'', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (178, N'', N'32 MB', 85, N'', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (179, N'', N'32 MB', 85, N'', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (180, N'', N'44 KB', 88, N'', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (206, N'', N'32 KB', 89, N'', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (215, N'job.txt', N'104 bytes', 61, N'', 1, N'txt')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (222, N'', N'44 MB', 94, N'8', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (223, N'', N'44 MB', 94, N'not 8', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (224, N'', N'44 MB', 94, N'3', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (225, N'Manualy Added', N'32 bytes', 94, N'2', 1, N'exe')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (226, N'', N'44 MB', 92, N'5', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (227, N'', N'44 MB', 92, N'Test2', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (228, N'', N'44 MB', 92, N'Test3', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (229, N'Manualy Added', N'32 MB', 92, N'4', 1, N'png')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (230, N'', N'4 GB', 95, N'', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (233, N'', N'44 MB', 96, N'', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (239, N'Manualy Added', N'44 MB', 100, N'', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (240, N'Manualy Added', N'44 KB', 98, N'', 1, N'exe')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (241, N'', N'32 KB', 99, N'', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (242, N'', N'32 KB', 99, N'', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (243, N'', N'10 bytes', 105, N'10', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (244, N'', N'0 bytes', 106, N'', 1, N'txt')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (246, N'', N'44 bytes', 107, N'', 1, N'pdf')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (252, N'', N'1.97 KB', 27, N'1', 0, N'.json')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (253, N'', N'1.12 KB', 27, N'2', 1, N'.props')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (254, N'', N'150 bytes', 27, N'3', 0, N'.targets')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (255, N'', N'1.88 KB', 27, N'4', 1, N'.json')
GO
INSERT [dbo].[file] ([file_id], [file_name], [file_size], [transfer_id], [comment], [file_sent], [file_ext]) VALUES (256, N'', N'286 bytes', 27, N'5', 0, N'.cache')
GO
SET IDENTITY_INSERT [dbo].[file] OFF
GO
SET IDENTITY_INSERT [dbo].[fileExtension] ON 
GO
INSERT [dbo].[fileExtension] ([fileExtension_id], [fileExtension_name], [archived]) VALUES (1, N'pdf', 0)
GO
INSERT [dbo].[fileExtension] ([fileExtension_id], [fileExtension_name], [archived]) VALUES (2, N'jpg', 0)
GO
INSERT [dbo].[fileExtension] ([fileExtension_id], [fileExtension_name], [archived]) VALUES (3, N'pddf', 1)
GO
SET IDENTITY_INSERT [dbo].[fileExtension] OFF
GO
SET IDENTITY_INSERT [dbo].[operation] ON 
GO
INSERT [dbo].[operation] ([operation_id], [operation_name], [archived]) VALUES (1, N'operationsss', 0)
GO
INSERT [dbo].[operation] ([operation_id], [operation_name], [archived]) VALUES (2, N'Zions', 1)
GO
INSERT [dbo].[operation] ([operation_id], [operation_name], [archived]) VALUES (6, N'test', 0)
GO
INSERT [dbo].[operation] ([operation_id], [operation_name], [archived]) VALUES (7, N'Laser', 0)
GO
SET IDENTITY_INSERT [dbo].[operation] OFF
GO
SET IDENTITY_INSERT [dbo].[role] ON 
GO
INSERT [dbo].[role] ([role_id], [role_name], [ordering]) VALUES (3, N'User', 1)
GO
INSERT [dbo].[role] ([role_id], [role_name], [ordering]) VALUES (4, N'Admin', 3)
GO
INSERT [dbo].[role] ([role_id], [role_name], [ordering]) VALUES (5, N'Supervisor', 2)
GO
SET IDENTITY_INSERT [dbo].[role] OFF
GO
SET IDENTITY_INSERT [dbo].[spill] ON 
GO
INSERT [dbo].[spill] ([spill_id], [spill_status_id], [cfnoc_incident_number], [dgds_sim_incident_number], [burned_and_annotated], [isso_informed], [manager_informed], [nature_of_spill], [transfer_request_completed], [email_triple_deleted], [client_informed], [consideration_power_down], [cdsent], [date_of_spill], [time_of_spill], [time_identified_spill], [time_reported], [workstation_affected], [workstation_asset_number], [specialist_id], [reviewer_id], [orig_system_id], [dest_system_id], [transfer_id]) VALUES (4, 1, N'123', N'123', 1, CAST(N'2025-08-11T09:27:00.000' AS DateTime), CAST(N'2025-08-11T09:27:00.000' AS DateTime), N'Testing', 1, 1, 1, 1, 1, CAST(N'2025-04-15T17:17:00.000' AS DateTime), CAST(N'2025-04-29T17:17:00.000' AS DateTime), CAST(N'2025-04-15T17:17:00.000' AS DateTime), CAST(N'2025-08-11T09:27:00.000' AS DateTime), N'123', N'123', 9, 2, 4, 2, 107)
GO
INSERT [dbo].[spill] ([spill_id], [spill_status_id], [cfnoc_incident_number], [dgds_sim_incident_number], [burned_and_annotated], [isso_informed], [manager_informed], [nature_of_spill], [transfer_request_completed], [email_triple_deleted], [client_informed], [consideration_power_down], [cdsent], [date_of_spill], [time_of_spill], [time_identified_spill], [time_reported], [workstation_affected], [workstation_asset_number], [specialist_id], [reviewer_id], [orig_system_id], [dest_system_id], [transfer_id]) VALUES (18, 1, N'1234', N'1234', 1, CAST(N'2025-07-01T00:00:00.000' AS DateTime), CAST(N'2025-07-01T00:00:00.000' AS DateTime), N'Testing Purposes', 1, 1, 1, 1, 1, CAST(N'2025-07-01T00:00:00.000' AS DateTime), CAST(N'2025-07-01T00:00:00.000' AS DateTime), CAST(N'2025-07-01T00:00:00.000' AS DateTime), CAST(N'2025-07-01T00:00:00.000' AS DateTime), N'1234', N'1234', 9, 7, 2, 4, 27)
GO
SET IDENTITY_INSERT [dbo].[spill] OFF
GO
SET IDENTITY_INSERT [dbo].[spillStatus] ON 
GO
INSERT [dbo].[spillStatus] ([status_id], [status], [archived]) VALUES (1, N'Testing', 0)
GO
SET IDENTITY_INSERT [dbo].[spillStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[system] ON 
GO
INSERT [dbo].[system] ([system_id], [system_name], [archived]) VALUES (1, N'system', 0)
GO
INSERT [dbo].[system] ([system_id], [system_name], [archived]) VALUES (2, N'JIIFC LAN', 0)
GO
INSERT [dbo].[system] ([system_id], [system_name], [archived]) VALUES (3, N'DWAN', 0)
GO
INSERT [dbo].[system] ([system_id], [system_name], [archived]) VALUES (4, N'CSNI', 0)
GO
INSERT [dbo].[system] ([system_id], [system_name], [archived]) VALUES (6, N'New Sys444', 0)
GO
SET IDENTITY_INSERT [dbo].[system] OFF
GO
SET IDENTITY_INSERT [dbo].[transfer] ON 
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (19, CAST(N'2023-09-22T09:20:49.000' AS DateTime), NULL, N'hendley.j', 1, 1, 2, 1, 0, 0, 1, 0, NULL, 0, NULL, NULL, 0, 2, NULL, 2, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (27, CAST(N'2024-02-26T06:39:42.000' AS DateTime), CAST(N'2024-02-27T07:43:00.000' AS DateTime), N'Alain', 1, 1, 2, 1, 1, 1, 1, 1, N'Commentasss', 1, NULL, NULL, 0, 3, NULL, 1, CAST(N'2024-02-28T01:56:00.000' AS DateTime), 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (49, CAST(N'2024-03-22T09:11:40.000' AS DateTime), NULL, N'Joe ', 1, 1, 1, 2, 0, 0, 0, 0, N'None', 0, NULL, NULL, 0, NULL, NULL, 3, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (55, CAST(N'2024-03-22T10:28:44.590' AS DateTime), NULL, N'Joe', 2, 1, 2, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 3, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (56, CAST(N'2024-03-22T10:36:56.563' AS DateTime), NULL, N'Joe b', 1, 1, 2, 1, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 3, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (60, CAST(N'2024-03-22T10:49:31.943' AS DateTime), NULL, N'asd', 1, 6, 1, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 3, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (61, CAST(N'2024-03-22T10:53:40.000' AS DateTime), NULL, N'Ad2', 2, 1, 1, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 3, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (62, CAST(N'2024-03-22T11:00:08.597' AS DateTime), NULL, N'Ad2', 2, 1, 1, 4, 0, 0, 0, 0, N'None', 0, NULL, NULL, 0, NULL, NULL, 3, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (63, CAST(N'2024-03-22T11:02:19.000' AS DateTime), NULL, N'Joe ', 1, 6, 1, 2, 0, 0, 0, 0, N'hhhh', 0, NULL, NULL, 0, NULL, NULL, 3, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (64, CAST(N'2024-03-22T11:07:11.190' AS DateTime), NULL, N'Ad2', 2, 6, 1, 2, 0, 0, 1, 1, N'None', 0, NULL, NULL, 0, 1, NULL, 3, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (68, CAST(N'2024-03-26T10:43:47.787' AS DateTime), NULL, N'butt.r', 1, 6, 1, 2, 0, 0, 0, 0, N'test 1', 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (69, CAST(N'2024-03-26T10:56:27.697' AS DateTime), CAST(N'2024-03-27T10:57:00.000' AS DateTime), N'butt.r', 2, NULL, 1, 2, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (70, CAST(N'2024-03-26T11:00:38.800' AS DateTime), NULL, N'butt.r', 2, NULL, 1, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (71, CAST(N'2024-03-26T11:01:57.477' AS DateTime), NULL, N'mcgee.s', 2, NULL, 1, 3, 1, 1, 1, 1, NULL, 1, NULL, NULL, 1, 1, NULL, 2, NULL, 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (72, CAST(N'2024-04-03T12:28:59.863' AS DateTime), NULL, N'butt.r', 1, NULL, 1, 2, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (73, CAST(N'2024-04-03T13:29:42.840' AS DateTime), NULL, N'mcgee.s', 3, NULL, 1, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-03T13:29:42.900' AS DateTime), 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (74, CAST(N'2024-04-03T14:21:44.250' AS DateTime), CAST(N'2024-04-02T14:21:00.000' AS DateTime), N'Jess', 2, NULL, 1, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (75, CAST(N'2024-04-10T14:12:08.580' AS DateTime), CAST(N'2024-04-09T14:11:00.000' AS DateTime), N'butt.r', 2, NULL, 1, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-10T14:12:08.763' AS DateTime), 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (76, CAST(N'2024-04-11T02:33:48.000' AS DateTime), NULL, N'butt.r', 2, 1, 1, 1, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (77, CAST(N'2024-04-17T10:00:36.113' AS DateTime), CAST(N'2024-04-15T10:00:00.000' AS DateTime), N'mcgee.s', 2, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (78, CAST(N'2024-04-17T10:08:45.840' AS DateTime), CAST(N'2024-04-16T10:08:00.000' AS DateTime), N'mcgee.s', 2, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (79, CAST(N'2024-04-17T10:15:18.493' AS DateTime), CAST(N'2024-04-16T10:08:00.000' AS DateTime), N'mcgee.s', 2, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (80, CAST(N'2024-04-17T10:25:34.947' AS DateTime), CAST(N'2024-04-16T10:08:00.000' AS DateTime), N'mcgee.s', 2, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, NULL, 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (81, CAST(N'2024-04-17T10:29:30.413' AS DateTime), NULL, N'mcgee.s', 2, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-17T10:29:39.317' AS DateTime), 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (82, CAST(N'2024-04-17T10:33:51.663' AS DateTime), NULL, N'mcgee.s', 2, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-17T10:33:54.640' AS DateTime), 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (83, CAST(N'2024-04-17T10:36:05.000' AS DateTime), NULL, N'Jess', 3, 1, 4, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-17T10:36:08.000' AS DateTime), 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (84, CAST(N'2024-04-17T11:27:19.000' AS DateTime), NULL, N'Mahdi', 1, 1, 1, 1, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-17T11:27:19.000' AS DateTime), 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (85, CAST(N'2024-04-17T10:24:00.000' AS DateTime), CAST(N'2024-04-18T10:24:00.000' AS DateTime), N'mcgee.s', 2, NULL, 4, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-19T10:25:02.720' AS DateTime), 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (86, CAST(N'2024-04-19T10:32:55.000' AS DateTime), CAST(N'2024-04-19T10:35:00.000' AS DateTime), N'Jess', 3, NULL, 1, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-19T10:33:27.440' AS DateTime), 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (88, CAST(N'2024-04-21T15:19:00.000' AS DateTime), CAST(N'2024-04-22T15:19:00.000' AS DateTime), N'buttter', 3, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-04-23T15:20:54.250' AS DateTime), 1, 2, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (89, CAST(N'2024-05-09T11:26:00.000' AS DateTime), CAST(N'2024-05-09T14:28:00.000' AS DateTime), N'mcgee.s', 2, 6, 1, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-05-10T01:25:31.000' AS DateTime), 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (90, CAST(N'2024-05-15T08:58:00.000' AS DateTime), CAST(N'2024-05-16T10:02:00.000' AS DateTime), N'Jess', 3, NULL, 1, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-05-16T12:59:11.000' AS DateTime), 1, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (91, CAST(N'2024-05-19T06:38:00.000' AS DateTime), CAST(N'2024-05-20T06:38:00.000' AS DateTime), N'Jess', 2, NULL, 4, 4, 0, 0, 0, 0, N'fdklafjdaskl', 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-05-21T02:38:48.000' AS DateTime), 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (92, CAST(N'2024-05-19T06:49:00.000' AS DateTime), CAST(N'2024-05-20T06:50:00.000' AS DateTime), N'Mahdi', 1, 7, 4, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, 2, NULL, 2, CAST(N'2024-05-21T02:50:21.000' AS DateTime), 0, 2, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (94, CAST(N'2024-06-13T12:27:00.000' AS DateTime), CAST(N'2024-06-14T12:27:00.000' AS DateTime), N'Jess', 1, 7, 4, 4, 0, 0, 0, 1, NULL, 0, NULL, NULL, 0, 2, NULL, 2, CAST(N'2024-06-14T08:28:37.000' AS DateTime), 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (95, CAST(N'2024-06-18T15:48:00.000' AS DateTime), CAST(N'2024-06-19T15:48:00.000' AS DateTime), N'butt.r', 2, NULL, 3, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-06-20T11:48:41.157' AS DateTime), 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (96, CAST(N'2024-06-24T05:23:00.000' AS DateTime), CAST(N'2024-06-24T10:23:00.000' AS DateTime), N'butt.r', 3, NULL, 4, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-06-24T10:49:03.000' AS DateTime), 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (97, CAST(N'2024-06-24T02:59:00.000' AS DateTime), CAST(N'2024-06-25T03:59:00.000' AS DateTime), N'butt.r', 2, NULL, 4, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-06-24T11:00:08.000' AS DateTime), 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (98, CAST(N'2024-07-08T16:09:00.000' AS DateTime), CAST(N'2024-07-09T16:09:00.000' AS DateTime), N'butt.r', 8, NULL, 4, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-07-09T12:10:01.000' AS DateTime), 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (99, CAST(N'2024-07-08T16:10:00.000' AS DateTime), CAST(N'2024-07-09T16:10:00.000' AS DateTime), N'mcgee.s', 8, NULL, 4, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-07-09T12:10:40.000' AS DateTime), 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (100, CAST(N'2024-07-08T16:10:00.000' AS DateTime), CAST(N'2024-07-09T16:10:00.000' AS DateTime), N'Jess', 10, NULL, 4, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-07-09T12:10:56.000' AS DateTime), 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (101, CAST(N'2024-07-07T16:12:00.000' AS DateTime), CAST(N'2024-07-08T16:12:00.000' AS DateTime), N'Mahdi', 11, NULL, 4, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-07-09T12:12:12.057' AS DateTime), 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (102, CAST(N'2024-07-04T16:12:00.000' AS DateTime), CAST(N'2024-07-05T16:12:00.000' AS DateTime), N'buttter', 12, NULL, 4, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-07-09T12:12:25.633' AS DateTime), 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (104, CAST(N'2024-07-03T16:12:00.000' AS DateTime), CAST(N'2024-07-09T16:12:00.000' AS DateTime), N'butt.r', 15, NULL, 4, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2024-07-09T12:12:55.837' AS DateTime), 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (105, CAST(N'2025-01-10T22:48:00.000' AS DateTime), CAST(N'2025-01-10T22:50:00.000' AS DateTime), N'bob', 11, 6, 4, 4, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 7, CAST(N'2025-01-10T12:50:23.310' AS DateTime), 0, 5, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (106, CAST(N'2025-03-10T16:31:00.000' AS DateTime), CAST(N'2025-03-11T16:31:00.000' AS DateTime), N'butt.r', 3, NULL, 4, 3, 0, 0, 0, 0, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2025-03-17T12:32:00.587' AS DateTime), 0, 1, NULL)
GO
INSERT [dbo].[transfer] ([transfer_id], [request_created_at], [sent_time], [client_name], [client_unit_id], [operation_id], [orig_system_id], [dest_system_id], [isso_approval], [issue_reported], [spill_prevented], [spill_occurred], [comments], [urgent], [virus_definition_date], [machine_name], [reviewed], [reviewed_user_id], [reviewed_at], [completed_user_id], [completed_at], [completed], [transfer_type], [spill_id]) VALUES (107, CAST(N'2025-04-15T17:17:00.000' AS DateTime), CAST(N'2025-04-29T17:17:00.000' AS DateTime), N'mcgee.s', 10, NULL, 4, 2, 0, 0, 0, 1, NULL, 0, NULL, NULL, 0, NULL, NULL, 2, CAST(N'2025-04-10T01:18:05.000' AS DateTime), 0, 1, 4)
GO
SET IDENTITY_INSERT [dbo].[transfer] OFF
GO
SET IDENTITY_INSERT [dbo].[transferType] ON 
GO
INSERT [dbo].[transferType] ([id], [transfer_type], [archived], [ordering]) VALUES (1, N'Transfer Request', 0, 1)
GO
INSERT [dbo].[transferType] ([id], [transfer_type], [archived], [ordering]) VALUES (2, N'Standing Transfer', 0, 2)
GO
INSERT [dbo].[transferType] ([id], [transfer_type], [archived], [ordering]) VALUES (3, N'Bulk Transfer', 0, 3)
GO
INSERT [dbo].[transferType] ([id], [transfer_type], [archived], [ordering]) VALUES (4, N'Project', 1, 4)
GO
INSERT [dbo].[transferType] ([id], [transfer_type], [archived], [ordering]) VALUES (5, N'Test', 0, 5)
GO
SET IDENTITY_INSERT [dbo].[transferType] OFF
GO
SET IDENTITY_INSERT [dbo].[unit] ON 
GO
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (1, N'unit1u', 0)
GO
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (2, N'JIIFC', 0)
GO
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (3, N'CJOC', 0)
GO
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (7, N'New one the is archived', 1)
GO
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (8, N'a', 0)
GO
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (9, N'b', 0)
GO
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (10, N'c', 0)
GO
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (11, N'd', 0)
GO
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (12, N'e', 0)
GO
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (13, N'f', 0)
GO
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (14, N'SJS', 0)
GO
INSERT [dbo].[unit] ([unit_id], [unit_name], [archived]) VALUES (15, N'g', 0)
GO
SET IDENTITY_INSERT [dbo].[unit] OFF
GO
SET IDENTITY_INSERT [dbo].[user] ON 
GO
INSERT [dbo].[user] ([user_id], [username], [first_name], [last_name], [title], [email], [disabled], [role_id], [alias]) VALUES (1, N'hendley.j', N'Jonathan', N'Hendley', NULL, N'hendley.j@JIIFC.MIL.CA', 0, 3, NULL)
GO
INSERT [dbo].[user] ([user_id], [username], [first_name], [last_name], [title], [email], [disabled], [role_id], [alias]) VALUES (2, N'mcgee.s', N'Stephen', N'McGee', NULL, N'mcgee.s@JIIFC.MIL.CA', 0, 4, NULL)
GO
INSERT [dbo].[user] ([user_id], [username], [first_name], [last_name], [title], [email], [disabled], [role_id], [alias]) VALUES (3, N'lalonde.a', N'Alain', N'Lalonde', NULL, N'lalonde.a@JIIFC.MIL.CA', 0, 3, NULL)
GO
INSERT [dbo].[user] ([user_id], [username], [first_name], [last_name], [title], [email], [disabled], [role_id], [alias]) VALUES (4, N'Ball.BM', N'Blake', N'Ball', NULL, N'Ball.BM@JIIFC.MIL.CA', 0, 4, NULL)
GO
INSERT [dbo].[user] ([user_id], [username], [first_name], [last_name], [title], [email], [disabled], [role_id], [alias]) VALUES (5, N'Winder.DR', N'Damen', N'Winder', NULL, N'Winder.DR@JIIFC.MIL.CA', 0, 3, NULL)
GO
INSERT [dbo].[user] ([user_id], [username], [first_name], [last_name], [title], [email], [disabled], [role_id], [alias]) VALUES (7, N'jennings.a', N'Aimee', N'Jennings', NULL, N'jennings.a@JIIFC.MIL.CA', 0, 4, NULL)
GO
INSERT [dbo].[user] ([user_id], [username], [first_name], [last_name], [title], [email], [disabled], [role_id], [alias]) VALUES (9, N'roussel.d', N'Danick', N'Roussel', NULL, N'roussel.d@JIIFC.MIL.CA', 0, 4, NULL)
GO
SET IDENTITY_INSERT [dbo].[user] OFF
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
ALTER TABLE [dbo].[spill]  WITH CHECK ADD  CONSTRAINT [FK_spill_status] FOREIGN KEY([spill_status_id])
REFERENCES [dbo].[spillStatus] ([status_id])
GO
ALTER TABLE [dbo].[spill] CHECK CONSTRAINT [FK_spill_status]
GO
ALTER TABLE [dbo].[spill]  WITH CHECK ADD  CONSTRAINT [FK_spill_system] FOREIGN KEY([orig_system_id])
REFERENCES [dbo].[system] ([system_id])
GO
ALTER TABLE [dbo].[spill] CHECK CONSTRAINT [FK_spill_system]
GO
ALTER TABLE [dbo].[spill]  WITH CHECK ADD  CONSTRAINT [FK_spill_system1] FOREIGN KEY([dest_system_id])
REFERENCES [dbo].[system] ([system_id])
GO
ALTER TABLE [dbo].[spill] CHECK CONSTRAINT [FK_spill_system1]
GO
ALTER TABLE [dbo].[spill]  WITH CHECK ADD  CONSTRAINT [FK_spill_user] FOREIGN KEY([specialist_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[spill] CHECK CONSTRAINT [FK_spill_user]
GO
ALTER TABLE [dbo].[spill]  WITH CHECK ADD  CONSTRAINT [FK_spill_user1] FOREIGN KEY([reviewer_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[spill] CHECK CONSTRAINT [FK_spill_user1]
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
GO
USE [master]
GO
ALTER DATABASE [New_DTAPP] SET  READ_WRITE 
GO
