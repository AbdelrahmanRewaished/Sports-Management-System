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

### APIs

- <a href="https://datatables.net/reference/api/">DataTables.Net</a>  (To Add Searching, Sorting and Pagination Functionality to The tables.)

- <a href="https://sweetalert2.github.io/">Sweet Alert</a>

- <a href="https://github.com/CodeSeven/toastr">Toastr</a>

## Screenshots

<details>
  <summary>Login Page</summary>
    ![image](https://user-images.githubusercontent.com/116602823/218286132-989a95a5-e5ad-4311-8d4f-6c8d6be83688.png)
</details>

## Installation 

1- <b>Install the .NET Core SDK 7.0</b>: Before you can run the application, you need to have the .NET Core SDK 7.0 installed on your machine. 
To Download it in:

- Windows:
```bash
Invoke-WebRequest -Uri https://dotnet.microsoft.com/download/dotnet-core/7.0.1/dotnet-sdk-7.0.1-win-x64.exe -OutFile dotnet-sdk-7.0.1-win-x64.exe
.\dotnet-sdk-7.0.1-win-x64.exe
```

- MacOS:
```bash
curl -L https://dotnet.microsoft.com/download/dotnet-core/7.0.1/dotnet-sdk-7.0.1-osx-x64.pkg -o dotnet-sdk-7.0.1-osx-x64.pkg
sudo installer -pkg dotnet-sdk-7.0.1-osx-x64.pkg -target /
```

- Linux:
```bash
wget https://dotnet.microsoft.com/download/dotnet-core/7.0.1/dotnet-sdk-7.0.1-linux-x64.tar.gz
tar -xvf dotnet-sdk-7.0.1-linux-x64.tar.gz
sudo mv dotnet /usr/share/
export PATH=$PATH:/usr/share/dotnet
```
2- <b>Install Microsoft SQL Server:</b>

- Windows:

Installation guide line from <a href="https://www.guru99.com/download-install-sql-server.html">here</a>


- MacOS:

Installation guideline from <a href="https://builtin.com/software-engineering-perspectives/sql-server-management-studio-mac">here</a>

- Linux: 

Installation guideline from <a href="https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-setup?view=sql-server-ver16">here</a>

3- <b>Get the Database connection String:</b>
To get the connection string, right-click on the database in the Object Explorer and choose Properties.
Select the Connections page and copy the string under the Connection string: field.
The connection string will contain the server name, database name, and other connection parameters necessary to connect to the database from a client application. Here's an example of a typical connection string for Microsoft SQL Server 2019:
```bash
Data Source=myServerName;Initial Catalog=myDataBase;Integrated Security=True;
```
<b>Enter the following commands in the powershell/bash</b>:

4- <b>Clone the Repository</b> 
```bash
git clone https://github.com/AbdelrahmanRewaished/Sports-Management-System
cd Sports-Management-System
```

5- <b>Install All required Dependencies</b>
```bash
dotnet restore
```
6- <b>Add the Connection string as a User Secret</b>
```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Your-Connection-String" 
```
To check if it has been stored successfully 
```bash
dotnet user-secrets list
```
7- <b>Build and Run the application</b>
```bash
dotnet build
dotnet run
```
The port number will be available to you, open the browser and paste the url and start the application


- Restore the packages: Open a command prompt or terminal window, navigate to the root folder of the application, and run the following command to restore the packages:

dotnet add package BCrypt.NET-Core --version 1.6.0
dotnet add package Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.1
dotnet add package Microsoft.Extensions.Configuration.UserSecrets --version 7.0.0
dotnet add package System.Data.SqlClient --version 4.8.5



## Implementation 🔨
- The Application was mainly built using ASP.NET Core Razor Pages and .NET Core Web API [version 7.0]
- The implementation was based on "Database First Approach" startegy.
The database components including Entities, Views, Stored Procedures and Functions was the first step of the implementation, built using Microsoft SQL Server and included the Different elements of the system. <br>
Secondly, reverse engineering was applied to transform all the components into C# Classes by the Microsoft Entity Framework Core, that consequently formed the DatabaseContext which controlls all the classes representing the Entities.

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
2- Password Hashing to by <b>BCript Library</b>.






