

use ProgressTen
go

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Club_Country]') AND parent_object_id = OBJECT_ID(N'[dbo].[Club]'))
ALTER TABLE [dbo].[Club] DROP CONSTRAINT [FK_Club_Country]
GO

alter table Club drop column CountryId
go

alter table Club add Country varchar(4) null
go

update Club set Country = 'US'
go

alter table Club alter column Country varchar(4) not null
go