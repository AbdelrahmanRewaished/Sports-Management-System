--CREATE DATABASE Champions_league_Db;

---------------------------------------------------------------------------------------------------

go
create procedure createAllTables 
AS
create table System_User2 ( username varchar(30), password varchar(60) , primary key (username))

---------------------------------------------------------------------------------------------------

create table Fan (national_ID varchar(20), name varchar(30), birth_date DATE, address varchar(30), 
phone_no varchar(20), status bit, username varchar(30), 
primary key (national_id), 
foreign key(username) references System_User2(username) on update cascade on delete cascade  )

---------------------------------------------------------------------------------------------------

create table Stadium (ID int identity, name varchar(30) unique, location varchar(30), capacity int,
status bit , primary key (ID) )

---------------------------------------------------------------------------------------------------

create table Stadium_Manager(ID int identity , name varchar(30), stadium_ID int, username varchar(30),
primary key (ID) , foreign key (stadium_ID) references Stadium (ID) on update cascade on delete cascade ,
foreign key (username) references System_User2(username) on update cascade on delete cascade )

---------------------------------------------------------------------------------------------------

create table Club (club_ID int identity, name varchar(30) unique, location varchar(30),primary key (club_ID))

---------------------------------------------------------------------------------------------------

create table Club_Representative(ID int identity, name varchar(30), club_ID int, username varchar(30)
, primary key (ID) ,foreign key (username) references System_User2 (username) on update cascade on delete cascade, foreign key(club_ID) references Club(club_ID) on update cascade on delete cascade)

---------------------------------------------------------------------------------------------------

create table Sports_association_manager(ID int identity, name varchar(30), username varchar(30) ,primary key(ID) , foreign key(username) references System_User2(username) on update cascade on delete cascade)

---------------------------------------------------------------------------------------------------

create table System_Admin(ID int identity, name varchar(30), username varchar(30)
, primary key (ID) ,foreign key (username) references System_User2 (username) on update cascade on delete cascade)

---------------------------------------------------------------------------------------------------

create table Match(match_ID int identity , start_time datetime, end_time datetime , host_club_ID int, guest_club_ID int, 
stadium_ID int,
primary key (match_ID) , foreign key (host_club_ID) references Club(club_ID) on update cascade on delete cascade ,
foreign key (stadium_ID) references Stadium (ID) on update cascade on delete cascade , foreign key (guest_club_ID) references Club(club_ID) on delete NO ACTION ON UPDATE NO ACTION,
check(start_time >= CURRENT_TIMESTAMP and start_time < end_time and host_club_ID <> guest_club_ID)
);

---------------------------------------------------------------------------------------------------

create table Host_Request (ID int identity,  representative_ID int, manager_ID int, match_ID int, status varchar(30) 
,primary key (ID) , foreign key (representative_ID) references club_Representative (ID) on update no action on delete no action ,
foreign key (manager_ID) references Stadium_Manager (ID) on update no action on delete no action, foreign key (match_ID) references Match(match_ID) on update cascade on delete cascade)

---------------------------------------------------------------------------------------------------

create table Ticket (ID int identity , status bit, match_ID int, primary key (ID) ,
foreign key (match_ID)references Match(match_ID) on update cascade on delete cascade)

---------------------------------------------------------------------------------------------------

create table Ticket_Buying_Transactions (fan_national_ID varchar(20), ticket_ID int , primary key(fan_national_ID, ticket_ID)
,foreign key (fan_national_ID )references Fan(national_ID) on update cascade on delete cascade, foreign key (ticket_ID) references ticket (ID) on update cascade on delete cascade)

---------------------------------------------------------------------------------------------------

go
exec createAllTables;

---------------------------------------------------------------------------------------------------

go
create function [getClubId](@name varchar(30))
returns int 
as 
begin
declare @id int 
select @id = club_ID 
from Club 
where name = @name
return @id
end
go

---------------------------------------------------------------------------------------------------

create function [getMatchId] (@host_club_name varchar(30), @guest_club_name varchar(30) , @start_time datetime )
returns int 
begin 
declare @matchId int, @hostId int, @guestId int
exec @hostId = dbo.getClubId @host_club_name
exec @guestId = dbo.getClubId @guest_club_name
select @matchId = m.match_ID
from Match m
where m.host_club_ID = @hostId
and m.guest_club_ID = @guestId
and m.start_time = @start_time
return @matchId
end;
go

---------------------------------------------------------------------------------------------------

CREATE FUNCTION [getStadiumID] (@stadium_name VARCHAR(30))
RETURNS INT 
BEGIN 
DECLARE @stadium_id INT 
select @stadium_id = ID from Stadium where name = @stadium_name 
return @stadium_id;
END
go

---------------------------------------------------------------------------------------------------

create function [getStadiumManagerId](@stadium_name varchar(30))
returns int 
begin 
declare @stadium_id int 
exec @stadium_id = dbo.getStadiumID @stadium_name
declare @manager_id int
select @manager_id = ID from Stadium_Manager where stadium_ID = @stadium_id
return @manager_id
end;
go

---------------------------------------------------------------------------------------------------

create function [getRepresentativeId] (@club_name varchar(30))
returns int 
begin 
declare @club_id int
exec @club_id = dbo.getClubId @club_name
declare @id int
select @id = Club_Representative.ID from Club_Representative where club_ID = @club_id
return @id
end;
go

---------------------------------------------------------------------------------------------------

create procedure addAssociationManager
@name varchar(30),@username varchar(30) , @password varchar(60)
as 
insert into System_User2 values(@username, @password);
insert into Sports_association_manager values (@name , @username)
go 

---------------------------------------------------------------------------------------------------

create proc addRepresentative
@name varchar(30), @clubname varchar(30) , @username varchar(30), @password varchar(60)
as
declare @id int 
exec @id = dbo.getClubId @clubname 
insert into System_User2 values(@username, @password);
insert into Club_Representative values (@name, @id, @username);
go

---------------------------------------------------------------------------------------------------

CREATE PROCEDURE addStadiumManager
@manager_name VARCHAR(30),
@stadium_name VARCHAR(30),
@username VARCHAR(30),
@password VARCHAR(60)
AS
insert into System_User2 values(@username, @password);
DECLARE @stadium_id INT
exec @stadium_id = dbo.getStadiumID @stadium_name
INSERT INTO Stadium_Manager VALUES(@manager_name, @stadium_id, @username)
GO

---------------------------------------------------------------------------------------------------

create proc addFan 
@fan_name varchar(30), @username varchar(30), @password varchar(60),
@national_id varchar(20), @birthdate date, @address varchar(30), @phone varchar(20)
as
insert into System_User2 values(@username, @password);
insert into Fan values(@national_id, @fan_name, @birthdate, @address, @phone, 1, @username) 
go

---------------------------------------------------------------------------------------------------

-- For Testing Purposes
insert into System_User2 values('Admin', '$2a$12$8ZJ1ZS07pq02sA3J1eSAWug2m97JRVaGMfYuWeK4xJ.OMciaDVC1q');
insert into System_Admin values('Admin', 'Admin');

-- To Delete All Entities Data
--exec clearAllTables