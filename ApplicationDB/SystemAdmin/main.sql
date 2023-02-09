-- System Admin mainly used stored procedures and functions

create proc  addStadium @name varchar(30) , @location varchar(30) , @capacity int 
as 
insert into Stadium values (@name, @location ,@capacity , 1);
go

---------------------------------------------------------------------------------------------------

create proc blockFan @id varchar(30)
as 
update Fan set status= 0 where national_ID = @id ;
go

---------------------------------------------------------------------------------------------------

create proc unblockFan @id varchar(30)
as 
update Fan set status= 1 where national_ID=@id ;
go

---------------------------------------------------------------------------------------------------
