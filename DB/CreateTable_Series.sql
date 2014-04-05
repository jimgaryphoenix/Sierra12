USE [ProgressTen]
GO

/****** Object:  Table [dbo].[Series]    Script Date: 12/04/2011 21:41:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Series](
	[SeriesId] [int] IDENTITY(1,1) NOT NULL,
	[ClubId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateCancelled] [datetime] NULL,
 CONSTRAINT [PK_Series] PRIMARY KEY CLUSTERED 
(
	[SeriesId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Series]  WITH CHECK ADD  CONSTRAINT [FK_Series_Club] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Club] ([ClubId])
GO

ALTER TABLE [dbo].[Series] CHECK CONSTRAINT [FK_Series_Club]
GO

