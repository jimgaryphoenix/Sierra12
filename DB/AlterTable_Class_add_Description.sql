

use ProgressTen
go

alter table Class add [Description] varchar(500) null
go

alter table Class alter column Name varchar(50) null
go

alter table Class add DisplayOrder int null
go

update Class set DisplayOrder = 1
go

alter table Class alter column DisplayOrder int not null
go