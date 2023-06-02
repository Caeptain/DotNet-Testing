# DotNetTesting

## Migration:
> $env:WeAssist = "Server=(localdb)\mssqllocaldb;Database=WeAssistDb;Trusted_Connection=True"
### Add
> add-migration -Name <NameDerNeuenMigration> -Context WeAssistDbContext -Project Database.Lib -StartupProject Database.Lib
### Update
> Update-Database -Context WeAssistDbContext -Project Database.Lib -StartupProject Database.Lib
