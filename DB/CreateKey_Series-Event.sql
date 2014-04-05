USE [ProgressTen]
GO

ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Series] FOREIGN KEY([SeriesId])
REFERENCES [dbo].[Series] ([SeriesId])
GO

ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Series]
GO

