USE [ProgressTen]
GO

ALTER TABLE [dbo].[Club]  WITH CHECK ADD  CONSTRAINT [FK_Club_Driver] FOREIGN KEY([RegisteringDriverId])
REFERENCES [dbo].[Driver] ([DriverId])
GO

ALTER TABLE [dbo].[Club] CHECK CONSTRAINT [FK_Club_Driver]
GO


