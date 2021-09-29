# TAU

### Create new solution

- dotnet new sln -o tau

- cd tau
- dotnet new console -o app
- dotnet new classlib -o lib
- dotnet new xunit -o test

- dotnet sln add app/app.csproj
- dotnet sln add lib/lib.csproj
- dotnet sln add test/test.

- dotnet new tool-manifest
- dotnet tool install dotnet-ef

- cd app
- dotnet add reference ../lib/lib.cscd ..

- cd lib
- dotnet add package Microsoft.Extensions.Logging.Console
- dotnet add package Microsoft.EntityFrameworkCore.Sqlite
- dotnet add package Microsoft.EntityFrameworkCore.Design
- dotnet add package Microsoft.EntityFrameworkCore

- cd test
- dotnet add package FluentAssertions
