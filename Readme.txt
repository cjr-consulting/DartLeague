First steps need to identify the type of db and load the right nuget pacakges

If not using mysql the migrations will need to be reset.

For MYSQL I found the following works:

Pomelo.EntityFrameworkCore.MySql
Pomelo.EntityFrameworkCore.MySql.Design

After changing the configuration re create migrations
dotnet ef migrations add InitialAspnetIdentityMigration -c AuthDbContext -o Data/Migrations/AspnetIdentity/AuthDb
dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb