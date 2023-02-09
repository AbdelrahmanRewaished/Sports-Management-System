-- Fan mainly used stored procedures and functions


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

---------------------------------------------------------------------------------------------------

create proc purchaseTicket
@national_id varchar(20), @hosting_club_name varchar(30), @guest_club_name varchar(30), @match_start_time datetime
as
declare @matchId int, @ticketId int
exec @matchId = dbo.getMatchId @hosting_club_name, @guest_club_name, @match_start_time
select @ticketId = MAX(t.ID) from Ticket t where t.match_ID = @matchId and t.status = 1
insert into Ticket_Buying_Transactions values(@national_id, @ticketId)
update Ticket set Ticket.status = 0 where Ticket.ID = @ticketId 
go

---------------------------------------------------------------------------------------------------


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

---------------------------------------------------------------------------------------------------
