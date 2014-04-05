
use ProgressTen

update Class set [Description] = 'Top level Nationals Qualifier class for 1.9 mini rock crawlers' where ClassId = 1
update Class set [Description] = 'Top level Nationals Qualifier class for 2.2 class comp crawlers' where ClassId = 2
update Class set [Description] = 'Top level Nationals Qualifier class for Super class crawlers' where ClassId = 3

update Class set Name = '2.2 Pro 1' where ClassId = 2

insert into Class (Name, [Description], DisplayOrder) values ('2.2 Pro 2', 'Relaxed format for newer or novice drivers of 2.2 comp crawlers, allows full-featured 2.2 crawlers, relaxed judging, and any other allowances a club might include', 1)
insert into Class (Name, [Description], DisplayOrder) values ('2.2 Novice (Spec)', 'Relaxed format for novice drivers of 2.2 crawlers where a club has decided on vehicle specs such as shaft drive only, or no dig system allowed', 1)
insert into Class (Name, [Description], DisplayOrder) values ('2.2 Sportsman Pro', 'Top level drivers class for USRCCA, shaft-driven, two-channel comp crawlers', 1)
insert into Class (Name, [Description], DisplayOrder) values ('2.2 Sportsman Novice', 'Relaxed format for novice drivers of USRCCA Sportsman crawlers', 1)
insert into Class (Name, [Description], DisplayOrder) values ('1.9 Novice', 'Relaxed format for novice drivers of 1.9 mini rock crawlers', 1)

select * from Class