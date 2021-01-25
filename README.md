# Surveys API
An API created for an job test.

## General requirements
- [Git](https://git-scm.com/) (2.30.0)
- [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) (3.1.11)
- [SQL Server 2019](https://www.microsoft.com/es-es/sql-server/sql-server-2019)

## Setup for web app
**Note:** There is already an Azure DB instance connected to the appsettings.json. If you wish
to run the API with a local DB instance make sure to run before these SQL scripts located in the Database folder:

- Schema.sql
- SP_DeleteQuestionOrdersBySurveyIdQuestionId.sql
- VSurveyResponses.sql
- Data.sql

Then, change ConnectionStrings.SurveysDB from appsettings.json for your
instance's connection string.

### Run API
You can just open the solution and run it from Visual Studio 2019. If you
wish to run it with dotnet cli here are the steps:

```shell
$ git clone https://github.com/jestra52/SurveysAPI.git
$ cd SurveysAPI/Surveys.Presentation.Api
$ dotnet run
```

Then go to your web explorer and open http://localhost:5000. It will launch the documentation page
with all the services available.

To run the Tests just follow these steps:

```shell
$ cd SurveysAPI/Surveys.Tests
$ dotnet test
```

## Achitecture

