# DotNetTesting

##Migration:
###Add
$env:WeAssist = "Server=(localdb)\mssqllocaldb;Database=WeAssistDb;Trusted_Connection=True"
add-migration -Name <NameDerNeuenMigration> -Context WeAssistDbContext -Project Werma.WeAssist.EntityFrameworkCore.Database.Lib -StartupProject Werma.WeAssist.EntityFrameworkCore.Database.Lib
###Update
