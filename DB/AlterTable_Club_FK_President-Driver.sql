USE [ProgressTen]
GO

ALTER TABLE [dbo].[Club]  WITH CHECK ADD  CONSTRAINT [FK_ClubPresident_Driver] FOREIGN KEY([CurrentPresidentDriverId])
REFERENCES [dbo].[Driver] ([DriverId])
GO

ALTER TABLE [dbo].[Club] CHECK CONSTRAINT [FK_ClubPresident_Driver]
GO

