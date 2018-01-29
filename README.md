## Setup local development environment:

On Linux/Ubuntu:
 - Install .NET Core 2.0 SDK
 - Install docker-ce: https://docs.docker.com/install/linux/docker-ce/ubuntu/
 - Install docker-compose: https://docs.docker.com/compose/install/
 - Run `git clone https://github.com/djsowa/Sienna.git`
 - Run `cd Sienna`
 - Run `docker-compose -f docker-compose-dev-local.yml up -d`, optionally add `--remove-orphans` to stop unused containers.
 - Run `cd src/SIENN.WebApi`
 - (Optional) Run: `dotnet ef database update -s SIENN.WebApi.csproj -p ../SIENN.DbAccess/SIENN.DbAccess.csproj`
 - Run `dotnet run`
 - Access to PgAdmin web interface to manage PostgreSQL database: http://localhost:82
    - Username: postgresql
    - Password: SiennaPass#0Rd
 - Sql queries located within Tests project.
 - Project is ready to be used with Visual Studio Code.