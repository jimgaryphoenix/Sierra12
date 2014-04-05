USE [ProgressTen]
GO

/****** Object:  Table [dbo].[SeriesClass]    Script Date: 12/11/2011 11:08:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SeriesClass](
	[SeriesClassId] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[SeriesId] [int] NOT NULL,
 CONSTRAINT [PK_SeriesClass] PRIMARY KEY CLUSTERED 
(
	[SeriesClassId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SeriesClass]  WITH CHECK ADD  CONSTRAINT [FK_SeriesClass_Class] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([ClassId])
GO

ALTER TABLE [dbo].[SeriesClass] CHECK CONSTRAINT [FK_SeriesClass_Class]
GO

ALTER TABLE [dbo].[SeriesClass]  WITH CHECK ADD  CONSTRAINT [FK_SeriesClass_Series] FOREIGN KEY([SeriesId])
REFERENCES [dbo].[Series] ([SeriesId])
GO

ALTER TABLE [dbo].[SeriesClass] CHECK CONSTRAINT [FK_SeriesClass_Series]
GO


