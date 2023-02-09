-- Association Manager Sub (Additional) stored procedures and functions

create procedure deleteMatch @hostclub varchar(30) , @guestclub varchar(30) 
as 
delete from match where match.host_club_id = dbo.getClubId(@hostclub) and match.guest_club_ID = dbo.getClubId(@guestclub)
go

---------------------------------------------------------------------------------------------------

create view clubsWithNoMatches
as
select c.name 
from Club c
where c.club_ID not in ((select host_club_ID from match ) 
union 
(select guest_club_ID from match))
go

---------------------------------------------------------------------------------------------------

create proc deleteMatchesOnStadium @sname varchar(30) 
as
declare @id int 
select @id = id from Stadium where @sname = name 
delete from Match where Match.stadium_Id = @id and Match.start_time > CURRENT_TIMESTAMP;
go

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------

create proc updateMatchHost
@host_name varchar(30), @guest_name varchar(30), @match_start_time datetime
as
declare @matchId int, @host_id int, @guest_id int
exec @matchId = dbo.getMatchId @host_name, @guest_name, @match_start_time
exec @host_id = dbo.getClubId @host_name
exec @guest_id = dbo.getClubId @guest_name
update Match set Match.host_club_ID = @guest_id, guest_club_ID = @host_id
go

---------------------------------------------------------------------------------------------------

create view matchesPerTeam
as
select c.name as club_name, count(c.club_ID) as no_of_matches_played
from Club c
inner join Match m
on c.club_ID = m.host_club_ID or c.club_ID = m.guest_club_ID
group by c.name 
go

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------
