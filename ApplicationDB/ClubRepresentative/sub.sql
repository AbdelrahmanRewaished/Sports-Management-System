-- Club Reprsentative Sub (Additional) stored procedures and functions

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

---------------------------------------------------------------------------------------------------

