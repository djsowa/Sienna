FROM microsoft/aspnetcore-build:2.0

RUN apt-get update -y
RUN apt-get install -y poppler-utils

WORKDIR /packages

RUN dotnet new console

RUN dotnet add package log4net -v 2.0.8
RUN dotnet add package Newtonsoft.Json -v 10.0.3
RUN dotnet add package RabbitMQ.Client -v 5.0.1
RUN dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL -v 2.0.0
RUN dotnet add package Microsoft.EntityFrameworkCore.Design -v 2.0.0
RUN dotnet add package System.Configuration -v 2.0.5
RUN dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Contracts -v 2.0.0
RUN dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Tools -v 2.0.0
RUN dotnet add package Microsoft.AspNetCore.All -v 2.0.0
RUN dotnet add package Microsoft.EntityFrameworkCore.Tools -v 2.0.0
RUN dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design -v 2.0.0
RUN dotnet add package Microsoft.EntityFrameworkCore.Tools.DotNet -v 2.0.0
RUN dotnet add package Microsoft.AspNetCore.Razor -v 2.0.1
RUN dotnet add package Microsoft.Extensions.Configuration.Json -v 2.0.0
RUN dotnet add package X.PagedList.Mvc.Core -v 7.2.0
RUN dotnet add package Swashbuckle.AspNetCore -v 1.1.0


WORKDIR /app
COPY ./src /app/src

RUN dotnet restore /app/src
RUN dotnet build /app/src
RUN dotnet publish /app/src --output /app

#RUN rm -R /app/src

EXPOSE 5000/tcp
ENV ASPNETCORE_URLS="http://0.0.0.0:5000"

ENTRYPOINT [ "dotnet", "ef", "database", "update" ]