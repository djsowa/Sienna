FROM microsoft/aspnetcore-build:2.0

RUN apt-get update -y
RUN apt-get install -y poppler-utils

WORKDIR /packages

RUN dotnet new console


RUN dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL -v 2.0.0
RUN dotnet add package Microsoft.EntityFrameworkCore.Design -v 2.0.0
RUN dotnet add package System.Configuration -v 2.0.5
RUN dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Contracts -v 2.0.0
RUN dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Tools -v 2.0.0
RUN dotnet add package Microsoft.AspNetCore.All -v 2.0.0
RUN dotnet add package Microsoft.EntityFrameworkCore.Tools -v 2.0.0
RUN dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design -v 2.0.0
RUN dotnet add package Microsoft.EntityFrameworkCore.Tools.DotNet -v 2.0.0
RUN dotnet add package Microsoft.Extensions.Configuration.Json -v 2.0.0
RUN dotnet add package Swashbuckle.AspNetCore -v 1.1.0
RUN dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection -v 3.2.0
RUN dotnet add package AutoMapper -v 6.2.2
RUN dotnet add package FluentAssertions -v 4.19.4
RUN dotnet add package Microsoft.NET.Test.Sdk -v 15.3.0-preview-20170628-02
RUN dotnet add package xunit -v 2.2.0
RUN dotnet add package xunit.runner.visualstudio -v 2.2.0


WORKDIR /app
WORKDIR /app
COPY ./src /app/src

RUN chmod +x /app/src/DbUpdate.sh

RUN dotnet restore /app/src
RUN dotnet test /app/src/SIENN.Tests/SIENN.Tests.csproj
RUN dotnet publish /app/src --output /app

#RUN rm -R /app/src

EXPOSE 5000/tcp

ENV ASPNETCORE_URLS="http://0.0.0.0:5000"