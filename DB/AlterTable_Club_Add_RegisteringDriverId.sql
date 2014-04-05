

use ProgressTen
go

alter table Club add RegisteringDriverId int null
go

update Club set RegisteringDriverId = 73
go

alter table Club alter column RegisteringDriverId int not null
go


alter table Club add CurrentPresidentDriverId int null
go

update Club set CurrentPresidentDriverId = 73
go

alter table Club alter column CurrentPresidentDriverId int not null
go