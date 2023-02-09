-- Club Representative mainly used stored procedures and functions


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

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------
