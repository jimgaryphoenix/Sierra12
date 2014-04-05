
use ProgressTen
go

alter table Event add NumberOfCourses int null
go

update Event set NumberOfCourses = 3 where EventTypeId = 1
go

update Event set NumberOfCourses = 6 where EventTypeId is null or EventTypeId = 3
go

alter table Event alter column NumberOfCourses int not null
go