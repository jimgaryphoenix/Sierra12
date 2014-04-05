USE [ProgressTen]
GO

ALTER TABLE [dbo].[Club]  WITH CHECK ADD  CONSTRAINT [FK_Club_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO

ALTER TABLE [dbo].[Club] CHECK CONSTRAINT [FK_Club_Country]
GO


update Club set CountryId = 1
go

alter table Club alter column CountryId int not null
go