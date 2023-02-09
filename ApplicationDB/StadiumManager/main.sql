-- Stadium Manager mainly used stored procedures and functions


create proc addTicket @host_club varchar(30) , @guest_club varchar(30) , @start_time datetime 
as
declare @matchId int 
exec @matchId = dbo.getMatchId @host_club ,@guest_club, @start_time
insert into Ticket values (1, @matchId);
go

---------------------------------------------------------------------------------------------------


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

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------
