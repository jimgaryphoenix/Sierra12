

use ProgressTen
go

alter table Class add ClubId int null
go

update Class set ClubId = 1
go

alter table Class alter column ClubId int not null
go