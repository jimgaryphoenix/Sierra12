

use ProgressTen
go

alter table Driver add DateCreated datetime null
go

alter table Driver add DateActivated datetime null
go

alter table Driver add DateCancelled datetime null
go

update Driver set DateCreated = '1-1-2011'

alter table Driver alter column DateCreated datetime not null
go