create database mds_Core01
go
use mds_Core01
go
create table Formats(
FormatId int primary key identity(1,1),
FormatName Nvarchar(100) not null
)
go
insert into Formats (FormatName) values ('ODI')
insert into Formats (FormatName) values ('Test')
insert into Formats (FormatName) values ('T20')
insert into Formats (FormatName) values ('T10')
Go

create table Players(
PlayerId int primary key identity(1,1),
PlayerName Nvarchar(100) not null,
BirthDate datetime not null,
Phone Nvarchar(100) not null,
Picture Nvarchar(max) not null,
MaritalStatus bit not null
)
go


create table SeriesEntry(
SeriesEntryId int primary key identity(1,1),
PlayerId int references Players (PlayerId) not null,
FormatId int references Formats (FormatId)not null
)
go

