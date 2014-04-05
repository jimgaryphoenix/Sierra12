USE [ProgressTen]
GO

ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Club] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Club] ([ClubId])
GO

ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Club]
GO


