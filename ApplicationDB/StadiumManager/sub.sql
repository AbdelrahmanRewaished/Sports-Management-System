-- Stadium Manager Sub (Additional) stored procedures and functions


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

---------------------------------------------------------------------------------------------------
