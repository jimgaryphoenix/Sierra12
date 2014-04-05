

use ProgressTen
go

alter table Result add DateCreated datetime null
go

update Result set DateCreated = '2011-01-22 08:00:17.883'
go

alter table Result alter column DateCreated datetime not null
go