### Run the Project (Visual Studio 2026)
- Visual Studio 2026
- .NET 10 SDK
- SQL Server

### Steps
1. Clone the repository:
 bash:  git clone -b unitTest https://github.com/gmerkouris/SrorageApp.git

2. Open the solution with Visual Studio 2026: SrorageApp.slnx file
   
4. Install NuGet packages <br>
   -Microsoft.EntityFrameworkCore <br>
   -Microsoft.EntityFrameworkCore.SqlServer <br>
   -Microsoft.EntityFrameworkCore.Tools <br>
    -Microsoft.AspNetCore.Identity.EntityFrameworkCore <br>
    -Microsoft.EntityFrameworkCore.InMemory <br>
    -xUnit <br>
    -xunit.runner.visualstudio <br>

5. Import the database file db.bacpac  from ..\SrorageApp\database\ into SQL SERVER
6. Configure database connection: Update the DefaultConnection value in:  ..\SrorageApp\appsettings.json
7. Build the solution.
8. Start Without Debugging (Ctrl + F5)
9. Test credentials: <br>
   Admin <br>
    -Username: storage@admin.gr <br>
    -Password: Test1234!!! <br>
  User <br>
    -Username: pcbox@pcbox.gr <br>
     -Password: Qwerty123!!!! <br>

10. Run unit tests: Ctrl + R, A
