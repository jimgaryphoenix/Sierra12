


USE [ProgressTen]
GO

/****** Object:  Table [dbo].[Message]    Script Date: 11/26/2010 11:09:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Message](
	[MessageID] [int] IDENTITY(1,1) NOT NULL,
	[ToAddress] [varchar](50) NOT NULL,
	[FromAddress] [varchar](50) NOT NULL,
	[SenderName] [varchar](50) NOT NULL,
	[RccScreenName] [varchar](25) NULL,
	[MessageBody] [varchar](2000) NOT NULL,
	[DateSent] [datetime] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO




