-- Sub (Additional) General Views


create view allAssocManagers
as
select s.username, su.password, s.name 
from Sports_association_manager s , System_User2  su
where s.username = su.username
go 

---------------------------------------------------------------------------------------------------

create view allClubRepresentatives
AS
select cr.username,su.password , cr.name , c.name as club_name
from Club_Representative cr ,  System_User2  su ,Club c
where cr.username = su.username and cr.ID = c.club_ID 
go 

---------------------------------------------------------------------------------------------------

create view allStadiumManagers
AS
select  sm.username, su.password, sm.name, s.name as stadium_name
from Stadium_Manager sm , System_User2  su , Stadium s 
where sm.username = su.username and s.ID = sm.Stadium_ID
go 

---------------------------------------------------------------------------------------------------

create view allFans
AS
select f.username , su.password , f.name , f.national_ID , f.birth_date, f.status
from Fan f , System_User2  su 
where f.username= su.username 
go 

---------------------------------------------------------------------------------------------------

create view allMatches 
AS
select c.name as host_club , c2.name as guest_club, m.start_time, m.end_time
from Match m , Club c ,Club c2 
where m.host_club_ID = c.club_ID and 
m.guest_club_ID = c2.club_ID 
go 

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------

create view allClubs 
AS
select name, location
from Club
go 

---------------------------------------------------------------------------------------------------

create view allStadiums
as 
select name, location, capacity, status
from Stadium
go 

---------------------------------------------------------------------------------------------------

create view allRequests
as
select cr.username as club_representative ,sm.username as stadium_manager, h.status as request_status
from Host_Request h , Club_Representative cr , Stadium_Manager sm
where h.representative_ID = cr.ID and h.manager_ID = sm.ID
go 

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------

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

---------------------------------------------------------------------------------------------------

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

