USE [New_DTAPP]
GO

/****** Object:  Table [dbo].[system]    Script Date: 2025-03-19 11:06:51 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


