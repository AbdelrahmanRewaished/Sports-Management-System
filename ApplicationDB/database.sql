﻿--CREATE DATABASE Champions_league_Db;

go
create procedure createAllTables 
AS
create table System_User2 ( username varchar(30), password varchar(30) , primary key (username))

create table Fan (national_ID varchar(20), name varchar(30), birth_date DATE, address varchar(30), 
phone_no varchar(20), status bit, username varchar(30), 
primary key (national_id), 
foreign key(username) references System_User2(username) on update cascade on delete cascade  )

create table Stadium (ID int identity, name varchar(30) unique, location varchar(30), capacity int,
status bit , primary key (ID) )

create table Stadium_Manager(ID int identity , name varchar(30), stadium_ID int, username varchar(30),
primary key (ID) , foreign key (stadium_ID) references Stadium (ID) on update cascade on delete cascade ,
foreign key (username) references System_User2(username) on update cascade on delete cascade )

create table Club (club_ID int identity, name varchar(30) unique, location varchar(30),primary key (club_ID))

create table Club_Representative(ID int identity, name varchar(30), club_ID int, username varchar(30)
, primary key (ID) ,foreign key (username) references System_User2 (username) on update cascade on delete cascade, foreign key(club_ID) references Club(club_ID) on update cascade on delete cascade)

create table Sports_association_manager(ID int identity, name varchar(30), username varchar(30) ,primary key(ID) , foreign key(username) references System_User2(username) on update cascade on delete cascade)

create table System_Admin(ID int identity, name varchar(30), username varchar(30)
, primary key (ID) ,foreign key (username) references System_User2 (username) on update cascade on delete cascade)

create table Match(match_ID int identity , start_time datetime, end_time datetime , host_club_ID int, guest_club_ID int, 
stadium_ID int,
primary key (match_ID) , foreign key (host_club_ID) references Club(club_ID) on update cascade on delete cascade ,
foreign key (stadium_ID) references Stadium (ID) on update cascade on delete cascade , foreign key (guest_club_ID) references Club(club_ID) on delete NO ACTION ON UPDATE NO ACTION,
check(start_time >= CURRENT_TIMESTAMP and start_time < end_time)
);

create table Host_Request (ID int identity,  representative_ID int, manager_ID int, match_ID int, status varchar(30) 
,primary key (ID) , foreign key (representative_ID) references club_Representative (ID) on update no action on delete no action ,
foreign key (manager_ID) references Stadium_Manager (ID) on update no action on delete no action, foreign key (match_ID) references Match(match_ID) on update cascade on delete cascade)


create table Ticket (ID int identity , status bit, match_ID int, primary key (ID) ,
foreign key (match_ID)references Match(match_ID) on update cascade on delete cascade)

create table Ticket_Buying_Transactions (fan_national_ID varchar(20), ticket_ID int , primary key(fan_national_ID, ticket_ID)
,foreign key (fan_national_ID )references Fan(national_ID) on update cascade on delete cascade, foreign key (ticket_ID) references ticket (ID) on update cascade on delete cascade)

go

create procedure dropAllTables
as
drop table Host_Request
drop table Stadium_Manager
drop table Club_Representative
drop table Ticket_Buying_Transactions
drop table Fan
drop table Ticket
drop table Match
drop table Club
drop table System_Admin
drop table Sports_association_manager
drop table Stadium
drop table System_User2
go


create procedure clearAllTables 
as
delete from Host_Request
delete from Stadium_Manager
delete from Club_Representative
delete from Ticket_Buying_Transactions
delete from Fan
delete from Ticket
delete from Match
delete from Club
delete from System_Admin
delete from Sports_association_manager
delete from Stadium
delete from System_User2
go 

exec createAllTables;

go
create view allAssocManagers
as
select s.username, su.password, s.name 
from Sports_association_manager s , System_User2  su
where s.username = su.username
go 

create view allClubRepresentatives
AS
select cr.username,su.password , cr.name , c.name as club_name
from Club_Representative cr ,  System_User2  su ,Club c
where cr.username = su.username and cr.ID = c.club_ID 
go 

create view allStadiumManagers
AS
select  sm.username, su.password, sm.name, s.name as stadium_name
from Stadium_Manager sm , System_User2  su , Stadium s 
where sm.username = su.username and s.ID = sm.Stadium_ID
go 

create view allFans
AS
select f.username , su.password , f.name , f.national_ID , f.birth_date, f.status
from Fan f , System_User2  su 
where f.username= su.username 
go 

create view allMatches 
AS
select c.name as host_club , c2.name as guest_club, m.start_time, m.end_time
from Match m , Club c ,Club c2 
where m.host_club_ID = c.club_ID and 
m.guest_club_ID = c2.club_ID 
go 

create view allTickets
AS
select c.name as host_club ,c2.name as guest_club, s.name ,m.start_time
from Match m
inner join Ticket t 
on t.match_ID = m.match_ID
inner join Club c
on m.host_club_ID = c.club_ID
inner join Club c2
on m.guest_club_ID = c2.club_ID
inner join Stadium s
on s.ID = m.stadium_ID
go 

create view allClubs 
AS
select name, location
from Club
go 

create view allStadiums
as 
select name, location, capacity, status
from Stadium
go 

create view allRequests
as
select cr.username as club_representative ,sm.username as stadium_manager, h.status as request_status
from Host_Request h , Club_Representative cr , Stadium_Manager sm
where h.representative_ID = cr.ID and h.manager_ID = sm.ID
go 


create procedure addAssociationManager
@name varchar(30),@username varchar(30) , @password varchar(30)
as 
insert into System_User2 values (@username ,  @password) 
insert into Sports_association_manager values (@name , @username)
go 

create procedure addNewMatch 
@host_club_name varchar(30) , @guest_club_name varchar(30),@start_time datetime ,@end_time datetime 
as
declare @idh int
select @idh = club_ID from Club where name =@host_club_name
--exec @idh = dbo.getClubId @host_club_name

declare @idg int 
select @idg = club_ID from Club where name =@guest_club_name
--exec @idg = dbo.getClubId @guest_club_name

insert into match values (@start_time, @end_time, @idh, @idg, null)
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

create procedure deleteMatch @hostclub varchar(30) , @guestclub varchar(30) 
as 
delete from match where match.host_club_id = dbo.getClubId(@hostclub) and match.guest_club_ID = dbo.getClubId(@guestclub)

go
create view clubsWithNoMatches
as
select c.name 
from Club c
where c.club_ID not in ((select host_club_ID from match ) 
union 
(select guest_club_ID from match))
go


create proc deleteMatchesOnStadium @sname varchar(30) 
as
declare @id int 
select @id = id from Stadium where @sname = name 
delete from Match where Match.stadium_Id = @id and Match.start_time > CURRENT_TIMESTAMP;
go

create proc addClub @name varchar(30) , @location varchar(30) 
as 
insert into Club values (@name, @location);
go

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


create proc addTicket @host_club varchar(30) , @guest_club varchar(30) , @start_time datetime 
as
declare @matchId int 
exec @matchId = dbo.getMatchId @host_club ,@guest_club, @start_time
insert into Ticket values (1, @matchId);
go


create proc deleteClub @name varchar(30)
as 
delete from club where name =  @name ;
go

create proc  addStadium @name varchar(30) , @location varchar(30) , @capacity int 
as 
insert into Stadium values (@name, @location ,@capacity , 1);
go


create proc deleteStadium @name varchar(30)
as 
delete from Stadium where name = @name ;
go

create proc blockFan @id varchar(30)
as 
update Fan set status= 0 where national_ID = @id ;
go

create proc unblockFan @id varchar(30)
as 
update Fan set status= 1 where national_ID=@id ;
go

create proc addRepresentative
@name varchar(30), @clubname varchar(30) , @username varchar(30), @password varchar(30)
as
declare @id int 
exec @id = dbo.getClubId @clubname 
insert into System_User2 values (@username,@password)
insert into Club_Representative values (@name, @id, @username);
go

create function viewAvailableStadiumsOn
(@HostClub varchar(30), @GuestClub varchar(30),@StartTime datetime)
returns @T table (Name varchar(30) , Location varchar(30) , Capacity int)
as
begin
declare @matchId int, @EndTime datetime
exec @matchId = dbo.getMatchId @HostClub, @GuestClub, @StartTime
select @EndTime = m.end_time from Match m where m.match_ID = @matchId
insert into @T
select s.name, s.location, s.capacity
from Stadium s, Stadium_Manager sm
where sm.stadium_ID = s.ID and s.status = 1 
except
(select s1.name, s1.location, s1.capacity from Stadium s1, Match m, Host_Request hr, Stadium_Manager sm
where 
(s1.ID = m.stadium_ID and
(@StartTime between m.start_time and m.end_time
or 
@EndTime between m.start_time and m.end_time
)
and 
m.match_ID <> @matchId)
)
return 
end;
go


create function getAllHostRequestsSentBy
(@repId int)
returns @T table(HostClub varchar(30), GuestClub varchar(30),
StartTime datetime, Stadium varchar(30), Status varchar(30))
as
begin
insert into @T
select c1.name, c2.name, m.start_time, s.name, hr.status
from Host_Request hr, Match m, Club c1, Club c2, Stadium s, Stadium_Manager sm
where hr.representative_ID = @repId 
and hr.match_ID = m.match_ID
and m.host_club_ID = c1.club_ID
and m.guest_club_ID = c2.club_ID
and hr.manager_ID = sm.ID
and sm.stadium_ID = s.ID
return 
end

go
CREATE FUNCTION [getStadiumID] (@stadium_name VARCHAR(30))
RETURNS INT 
BEGIN 
DECLARE @stadium_id INT 
select @stadium_id = ID from Stadium where name = @stadium_name 
return @stadium_id;
END
go
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

create proc addHostRequest @club_name varchar(30) ,@stadium_name varchar(30) , @start_time datetime
as
declare @representative_id int , @manager_id int ,@match_id int, @club_id int
exec @representative_id = dbo.getRepresentativeId @club_name 
exec @club_id = dbo.getClubId @club_name
exec @manager_id= dbo.getStadiumManagerId @stadium_name 
select @match_id = m.match_ID from Match m where  m.start_time = @start_time and 
(m.host_club_ID = @club_id or m.guest_club_ID = @club_id)
insert into Host_Request values(@representative_id, @manager_id, @match_id, 'unhandled');
go


create function [allUnassignedMatches](@club_name varchar(30))
returns @T table (guest_club varchar(30), start_time datetime)
as 
begin 
declare @id int
exec @id =  dbo.getClubId @club_name  
insert into @T 
select c.name, m.start_time
from Club c, Match m
where c.club_ID = m.guest_club_ID and m.host_club_ID = @id  
and m.start_time = 
(select m1.start_time from Match m1 where m1.host_club_ID = @id 
and c.club_ID = m1.guest_club_ID) and m.stadium_ID is null
return
end;
go

create function [allPendingRequests] (@username varchar(30))
returns @T table(
RepresentativeName varchar(30), HostClub varchar(30),GuestClub varchar(30) ,StartTime datetime, EndTime datetime)
as 
begin 
declare @stadium_manager_id int
select @stadium_manager_id = id from Stadium_Manager where username = @username
insert into @T 
select cr.name as representative_name, c1.name as host_club, c2.name as guest_club ,
m.start_time as startTime, m.end_time as endTime
from Club_Representative cr , Club c1, Club c2 , Match m , Host_Request hr
where
hr.manager_ID = @stadium_manager_id and 
hr.representative_ID = cr.ID and
hr.match_ID = m.match_ID and
c1.club_ID = m.host_club_ID and
c2.club_ID = m.guest_club_ID and 
hr.status ='unhandled'
return 
end 
go

CREATE PROCEDURE addStadiumManager
@manager_name VARCHAR(30),
@stadium_name VARCHAR(30),
@username VARCHAR(30),
@password VARCHAR(30)
AS
INSERT INTO System_User2 VALUES(@username, @password);
DECLARE @stadium_id INT
exec @stadium_id = dbo.getStadiumID @stadium_name
INSERT INTO Stadium_Manager VALUES(@manager_name, @stadium_id, @username)
GO

create proc acceptRequest
@username varchar(30),
@hosting_club_name varchar(30),
@guest_club_name varchar(30),
@match_start_time varchar(30)
as
declare @matchId int, @managerId int, @hostRequestId int, @representedClub varchar(30)
exec @matchId = dbo.getMatchId @hosting_club_name, @guest_club_name, @match_start_time
declare @stadiumId int
select @managerId = sm.ID from Stadium_Manager sm where sm.username = @username
select @stadiumId = s.ID from Stadium s, Stadium_Manager sm where s.ID = sm.stadium_ID and sm.ID = @managerId;
select @hostRequestId = hr.ID from Host_Request hr where hr.manager_ID = @managerId and hr.match_ID = @matchId
update Host_Request set status = 'accepted' where Host_Request.ID = @hostRequestId
update Match set stadium_ID = @stadiumId where match_ID = @matchId
declare @capacity int
select  @capacity = s.capacity from Stadium s, Stadium_Manager sm where s.ID = sm.stadium_ID 
DECLARE @Counter INT 
SET @Counter=1
WHILE ( @Counter <= @capacity)
BEGIN
    exec addTicket @hosting_club_name, @guest_club_name, @match_start_time 
    SET @Counter  = @Counter + 1
END
go

create proc rejectRequest
@username varchar(30),
@hosting_club_name varchar(30),
@guest_club_name varchar(30),
@match_start_time varchar(30)
as
declare @matchId int, @managerId int
exec @matchId = dbo.getMatchId @hosting_club_name, @guest_club_name, @match_start_time
select @managerId = sm.ID from Stadium_Manager sm where sm.username = @username
update Host_Request set status = 'rejected' where Host_Request.match_ID = @matchId and Host_Request.manager_ID = @managerId
go

create proc addFan 
@fan_name varchar(30), @username varchar(30), @password varchar(30),
@national_id varchar(20), @birthdate date, @address varchar(30), @phone varchar(20)
as
insert into System_User2 values(@username, @password)
insert into Fan values(@national_id, @fan_name, @birthdate, @address, @phone, 1, @username) 
go


create function [upcomingMatchesOfClub] 
(@club_name varchar(30))
returns @T table(club_name varchar(30), opponent_name varchar(30), match_start_time datetime, stadium varchar(30))
as 
begin
declare @clubId int
exec @clubId = dbo.getClubId @club_name
insert into @T

select c.name, c2.name, m.start_time, s.name
from Club c, Club c2, Match m, Stadium s
where c.club_ID = @clubId
and
((c.club_ID = m.host_club_ID and
c2.club_ID = m.guest_club_ID) or
(c.club_ID = m.guest_club_ID and
c2.club_ID = m.host_club_ID))
and 
s.ID = m.stadium_ID
and
m.start_time > CURRENT_TIMESTAMP
return
end
go


create function [availableMatchesToAttend]()
returns @T table(HostClub varchar(30), GuestClub varchar(30), StartTime datetime, Stadium varchar(30), Location varchar(30))
as 
begin
insert into @T
select distinct c.name as host, c2.name as guest, m.start_time, s.name, s.location
from Club c, Club c2, Match m, Stadium s, Ticket t
where
s.ID = m.stadium_ID
and
c.club_ID = m.host_club_ID
and
c2.club_ID = m.guest_club_ID
and
m.start_time >= CURRENT_TIMESTAMP
and 
t.match_ID = m.match_ID
and 
t.status = 1
return 
end
go

create proc purchaseTicket
@national_id varchar(20), @hosting_club_name varchar(30), @guest_club_name varchar(30), @match_start_time datetime
as
declare @matchId int, @ticketId int
exec @matchId = dbo.getMatchId @hosting_club_name, @guest_club_name, @match_start_time
select @ticketId = MAX(t.ID) from Ticket t where t.match_ID = @matchId and t.status = 1
insert into Ticket_Buying_Transactions values(@national_id, @ticketId)
update Ticket set Ticket.status = 0 where Ticket.ID = @ticketId 
go

create proc updateMatchHost
@host_name varchar(30), @guest_name varchar(30), @match_start_time datetime
as
declare @matchId int, @host_id int, @guest_id int
exec @matchId = dbo.getMatchId @host_name, @guest_name, @match_start_time
exec @host_id = dbo.getClubId @host_name
exec @guest_id = dbo.getClubId @guest_name
update Match set Match.host_club_ID = @guest_id, guest_club_ID = @host_id
go

create view matchesPerTeam
as
select c.name as club_name, count(c.club_ID) as no_of_matches_played
from Club c
inner join Match m
on c.club_ID = m.host_club_ID or c.club_ID = m.guest_club_ID
group by c.name 
go

create view clubsNeverMatched
AS
select c1.name as club1_name, c2.name as club2_name from Club c1 cross join Club c2 where c1.name <> c2.name 
except
(
select c1.name, c2.name from Club c1
inner join Match m
on c1.club_ID = m.host_club_ID
inner join club c2
on c2.club_ID = m.guest_club_ID
where m.stadium_ID is not NULL
union
select c1.name, c2.name from Club c1
inner join Match m
on c1.club_ID = m.guest_club_ID
inner join club c2
on c2.club_ID = m.host_club_ID
where m.stadium_ID is not NULL)
go

create function [clubsNeverPlayed]
(@club_name varchar(30))
returns @T table(club_name varchar(30))
as
begin
insert into @T 
select cnm.club2_name from clubsNeverMatched cnm where club1_name = @club_name
return
end
go


create function [getTicketsSoldPerMatch]
()
returns @T table(match_id int, tickets_sold int)
as
begin
insert into @T
select m.match_ID, count(t.ID) from Ticket t
inner join Match m on m.match_ID = t.match_ID
where t.status = 0
group by m.match_ID
return
end
go


create function [matchWithHighestAttendance]
()
returns @T table(hosting_club_name varchar(30), guest_club_name varchar(30))
as
begin
declare @matches_tickets_sold table(match_id int, tickets_sold int)
declare @max_tickets_sold int
insert into @matches_tickets_sold select * from dbo.getTicketsSoldPerMatch ()
select @max_tickets_sold = max(tickets_sold) from @matches_tickets_sold
insert into @T
select c1.name, c2.name from Club c1
inner join Match m on m.host_club_ID = c1.club_ID
inner join Club c2 on m.guest_club_ID = c2.club_ID
where m.match_ID = (select mts.match_id from @matches_tickets_sold mts where tickets_sold = @max_tickets_sold)
return
end
go

create function[matchesRankedByAttendance]
()
returns @T table(hosting_club_name varchar(30), guest_club_name varchar(30))
as
begin
declare @matches_tickets_sold table(match_id int, tickets_sold int)
insert into @matches_tickets_sold select * from dbo.getTicketsSoldPerMatch ()
insert into @T 
select c1.name, c2.name from Club c1, Club c2, Match m, @matches_tickets_sold mts
where c1.club_ID = m.host_club_ID and c2.club_ID = m.guest_club_ID
and m.match_ID = mts.match_id
order by mts.tickets_sold desc
return
end
go

create function [requestsFromClub]
(@stadium_name varchar(30),
@club_name varchar(30))
returns @T table(hosting_club varchar(30), guest_club varchar(30))
as
begin
declare @stadium_manager_id int, @representative_id int, @club_id int, @manager_username varchar(30)

exec @stadium_manager_id = dbo.getStadiumManagerId @stadium_name
exec @club_id = dbo.getClubId @club_name
select @representative_id = cr.ID from Club_Representative cr where cr.club_ID = @club_id

insert into @T
select c1.name, c2.name From Club c1, Club c2, Match m, Host_Request hr
where c1.club_ID = m.host_club_ID and 
c2.club_ID = m.guest_club_ID and
hr.match_ID = m.match_ID and
hr.manager_ID = @stadium_manager_id and
hr.representative_ID = @representative_id
and hr.status ='unhandled'
return
end;
go

create function [AllUpComingMatchesFunc] ()
returns @T table(HostClub varchar(30),GuestClub varchar(30),StartTime datetime , EndTime datetime)
as 
begin

insert into @T
Select c.name ,c1.name,m.start_time,m.end_time
from Match m ,Club c ,Club c1 
where m.host_club_Id = c.club_id and m.guest_club_id = c1.club_id and m.start_time>Current_TimeStamp
return
end;
go

create view AllUpComingMatches as
select * from dbo.AllUpComingMatchesFunc()
go

create proc updateMatch @OldMatchId int, @HostClub varchar(30), @GuestClub varchar(30), @StartTime DateTime, @EndTime DateTime
as
declare @hostId int, @guestId int
exec @hostId = dbo.getClubId @HostClub
exec @guestId = dbo.getClubId @GuestClub
update Match set host_club_ID = @hostId, guest_club_ID = @guestId,
start_time = @StartTime, end_time = @EndTime 
where match_ID = @OldMatchId;
go

create view AlreadyPlayedMatches
as select host.name as HostClub, guest.name as GuestClub, m.start_time as StartTime, m.end_time as EndTime from Club host, Club guest, Match m
where m.host_club_ID = host.club_ID and
m.guest_club_ID = guest.club_ID and
m.end_time <= CURRENT_TIMESTAMP and
m.stadium_ID is not null
go

create function IsMatchHostable(@HostClub varchar(30), @GuestClub varchar(30), @StartTime DateTime)
returns BIT
as
begin
declare @count int, @res bit 
select @count = count(*) from Host_Request hr where hr.match_ID = dbo.getMatchId(@HostClub, @GuestClub, @StartTime)
and (hr.status='unhandled' or hr.status='accepted');
if @count = 0
    select @res = 1;
else
    select @res = 0;
return @res;
end

go
create function getAllUpComingMatchesOfClub
(@ClubId int)
returns @T table(HostClub varchar(30), GuestClub varchar(30), StartTime datetime, EndTime datetime, IsHostable BIT)
as
begin
insert into @T
select al.HostClub, al.GuestClub, al.StartTime, al.EndTime, dbo.IsMatchHostable(al.HostClub, al.GuestClub, al.StartTime) from AllUpComingMatches al
inner join Club c
on c.name = al.HostClub
where c.club_ID = @ClubId
return
end
go

create function purchasedTicketsPerMatchFor(@nationalId varchar(20))
returns @T table(HostClub varchar(30), GuestClub varchar(30), StartTime DateTime,
Stadium varchar(30), Location varchar(30), Tickets int)
as
begin 
insert into @T
select  c1.name, c2.name, m.start_time, s.name, s.location, count(t.ID)
from Club c1, Club c2, Match m, Stadium s,
Ticket_Buying_Transactions tbt, Ticket t, Fan f
where c1.club_ID = m.host_club_ID and c2.club_ID = m.guest_club_ID
and tbt.ticket_ID = t.ID and t.match_ID = m.match_ID
and s.ID = m.stadium_ID
and f.national_ID = tbt.fan_national_ID
and f.national_ID = @nationalId
group by c1.name, c2.name, m.start_time, s.name, s.location
return
end
go


create proc dropAllProceduresFunctionsViews
as 
drop procedure createAllTables 
drop procedure dropAllTables
drop procedure clearAllTables
drop view allAssocManagers
drop view allClubRepresentatives
drop view allStadiumManagers
drop view allFans
drop view allMatches 
drop view allTickets
drop view allClubs 
drop view allStadiums
drop view allRequests
drop procedure addAssociationManager
drop procedure addNewMatch 
drop function [getClubId]
drop procedure deleteMatch
drop view clubsWithNoMatches
drop proc deleteMatchesOnStadium
drop proc addClub
drop function [getMatchId] 
drop proc addTicket
drop proc deleteClub
drop proc  addStadium
drop proc deleteStadium
drop proc blockFan
drop proc unblockFan
drop proc addRepresentative
drop function[viewAvailableStadiumsOn]
drop proc addHostRequest
drop FUNCTION [getStadiumID]
drop function [getStadiumManagerId]
drop function [getRepresentativeId]
drop function [allUnassignedMatches]
drop function [allPendingRequests]
drop PROCEDURE addStadiumManager
drop proc acceptRequest
drop proc rejectRequest
drop proc addFan
drop function [upcomingMatchesOfClub]
drop function [availableMatchesToAttend]
drop proc purchaseTicket
drop  proc updateMatchHost
drop view matchesPerTeam
drop view clubsNeverMatched
drop function [clubsNeverPlayed]
drop function [getTicketsSoldPerMatch]
drop function [matchWithHighestAttendance]
drop function[matchesRankedByAttendance]
drop function [requestsFromClub];

insert into System_User2 values('Admin', 'admin');
insert into System_User2 values('Abdo', 'password');

insert into System_Admin values('Admin', 'Admin');
insert into System_Admin values('Abdo', 'Abdo');

