-- Association Manager mainly used stored procedures and functions

create procedure addNewMatch 
@host_club_name varchar(30) , @guest_club_name varchar(30),@start_time datetime ,@end_time datetime 
as
declare @idh int
select @idh = club_ID from Club where name =@host_club_name
declare @idg int 
select @idg = club_ID from Club where name =@guest_club_name
insert into match values (@start_time, @end_time, @idh, @idg, null)
go

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------

create view AllUpComingMatches as
select * from dbo.AllUpComingMatchesFunc()
go

---------------------------------------------------------------------------------------------------

create proc updateMatch @OldMatchId int, @HostClub varchar(30), @GuestClub varchar(30), @StartTime DateTime, @EndTime DateTime
as
declare @hostId int, @guestId int
exec @hostId = dbo.getClubId @HostClub
exec @guestId = dbo.getClubId @GuestClub
update Match set host_club_ID = @hostId, guest_club_ID = @guestId,
start_time = @StartTime, end_time = @EndTime 
where match_ID = @OldMatchId;
go

---------------------------------------------------------------------------------------------------

create view AlreadyPlayedMatches
as select host.name as HostClub, guest.name as GuestClub, m.start_time as StartTime, m.end_time as EndTime from Club host, Club guest, Match m
where m.host_club_ID = host.club_ID and
m.guest_club_ID = guest.club_ID and
m.end_time <= CURRENT_TIMESTAMP and
m.stadium_ID is not null
go

---------------------------------------------------------------------------------------------------
