# Sports Management System
[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](https://choosealicense.com/licenses/mit/)


An implementation of a full-stack web application.
The application function is to manage a sports platform serving 5 types of users: Admins, Sports Association Managers, Club Representatives, Stadium Managers and Fans.

## Features ✨
<b> The System Serves 5 types of Users (Admins, Sports Association Managers, Stadium Managers, Club Representatives, Fans) </b>

### An Admin can

- Add/Edit/Delete Clubs.

- Add/Edit/Delete Stadiums.

- Block/Unblock Fans.

### A Sports Association Manager can

- Register to the System.

- Add/Edit/Delete Matches and define who is the Host-Club and the Guest-Club of the match.

- View Upcoming and Already Played Matches.

- View Clubs who never faced each other.

### A Club Representative can

- Register to the System.

- Send a Hosting Request on the Match (whose host-club is represented by him) to requests the manager of a stadium to allow this match to be hosted in the stadium.

- View all sent requests status by him.

### A Stadium Manager can

- Register to the System.

- Accept/Reject the upcoming requests to his/her Stadium.

### A Fan can

- Register to the System.

- Purchase/Book a Ticket to an available match.

- View all purchased tickets by him.



## Tech and Frameworks Used 🧰
### Backend
<a href="https://cdnlogo.com/logo/dot-net-core_832.html"><img src="https://cdn.cdnlogo.com/logos/d/6/dot-net-core.svg" alt=".NET Core" width="70" height="70"/></a>     <a href="https://www.microsoft.com/en-us/sql-server" target="_blank" rel="noreferrer"> <img src="https://www.svgrepo.com/show/303229/microsoft-sql-server-logo.svg" alt="mssql" width="70" height="70"/> </a>
- 

### Frontend
<a href="https://www.w3.org/html/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/html5/html5-original-wordmark.svg" alt="html5" width="70" height="70"/> </a><a href="https://www.w3schools.com/css/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/css3/css3-original-wordmark.svg" alt="css3" width="70" height="70"/> </a><a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/javascript/javascript-original.svg" alt="javascript" width="60" height="60"/> </a>

### Design

- <a href="https://datatables.net/reference/api/">DataTables.Net</a>  (To Add Searching, Sorting and Pagination Functionality to The tables.)

- <a href="https://sweetalert2.github.io/">Sweet Alert</a>

- <a href="https://github.com/CodeSeven/toastr">Toastr</a>

## Implementation 🔨
- The Application was mainly built using ASP.NET Core Razor Pages and .NET Core Web API [version 7.0]
- The implementation was based on "Database First Approach" startegy.
The database components including Entities, Views, Stored Procedures and Functions was the first step of the implementation, built using Microsoft SQL Server and included the Different elements of the system. <br>
Secondly, reverse engineering was applied to transform all the components into C# Classes by the Microsoft Entity Framework Core, which consequently formed the DatabaseContext which controlls all the classes representing the Entities.

- System/Database Main Entities:

1) <b>System Users</b> <br>
  a- <b>System Admins</b> <br>
  b- <b>Sports Association Managers</b> <br>
  c- <b>Stadium Managers</b> <br>
  d- <b>Club Representatives</b> <br>
  e- <b>Fans</b> <br>
2) <b>Clubs</b> <br>
3) <b>Stadiums</b> <br>
4) <b>Matches</b> <br>
5) <b>Hosting Requests</b> <br>
6) <b>Tickets</b> <br>
<br>

- Security
Security is Added to the System to handle the following :<br>
1- The Application Authentication and Authorization based on Users Roles which implemented by <b>Security Claims</b> in .NET Core <br>
2- Password Hashing to protect Users passwords by <b>BCript Library</b>.






