## Setup local development environment:

On Linux/Ubuntu:
 - install .NET Core 2.0 SDK
 - install docker-ce: https://docs.docker.com/install/linux/docker-ce/ubuntu/
 - install docker-compose: https://docs.docker.com/compose/install/
 - `git clone https://github.com/djsowa/Sienna.git`
 - `cd Sienna`
 - `docker-compose -f docker-compose-dev-local.yml up -d`, optionally add `--remove-orphans` to stop unused containers.
 - `cd src/SIENN.WebApi`
 - `dotnet run`
 - Access to PgAdmin web interface to manage PostgreSQL database: http://localhost:82
    - Username: postgresql
    - Password: SiennaPass#0Rd
 - Sql queries located within Tests project.