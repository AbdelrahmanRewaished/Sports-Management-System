-- System Admin Sub (Additional) stored procedures and functions

create proc addClub @name varchar(30) , @location varchar(30) 
as 
insert into Club values (@name, @location);
go

create proc deleteClub @name varchar(30)
as 
delete from club where name =  @name ;
go

create proc deleteStadium @name varchar(30)
as 
delete from Stadium where name = @name ;
go