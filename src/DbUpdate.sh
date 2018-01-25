#!/bin/bash

cd /app/src/SIENN.WebApi

dotnet ef database update -s SIENN.WebApi.csproj -p ../SIENN.DbAccess/SIENN.DbAccess.csproj