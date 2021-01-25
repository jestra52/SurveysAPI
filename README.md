# Surveys API
An API created for TEAM's international .NET challenge. I hope you like my implementation. I enjoyed doing this :smile: 

## General requirements
- [Git](https://git-scm.com/) (2.30.0)
- [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) (3.1.11)
- [SQL Server 2019](https://www.microsoft.com/es-es/sql-server/sql-server-2019)
---
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

![homepage](Documentation/homePage.png)

To run the Tests just follow these steps:

```shell
$ cd SurveysAPI/Surveys.Tests
$ dotnet test
```
---
## Achitecture/File structure

```
SurveysAPI
│
└─── Database -> Database scripts such as schema, data, stored procedures and views
└─── Surveys.Tests -> Test project for unit testing and integration testing
└─── Surveys.Application.Dto -> DTOs and Mapper configuration
└─── Surveys.Application.Services -> Service layer that performs as an intermediary
                                     between presentation and data access
└─── Surveys.Common.Enum -> Enum files
└─── Surveys.Data.Domain -> Data access layer that contains entities, repositories and DbContext
└─── Surveys.Presentation.Api -> Presentation layer and main project that contains
                                 Controllers/Endpoints and configuration items such as
                                 Dependency injection, Error handling, Logging, Documentation, etc.
```

### Design and architecture 
- The project is based on the Domain-Driven Design principle, in order to decople the software artifacts, make a more scalable architecture and gain better knowledge of the domain. Also, to track business logic and implementation, avoiding as possible any chaotic grow.
- In order to perform manipulation of data without compromising consistency, I decided to implement the Repository pattern alongside the Unit of Work pattern. In that way, I am able to perform data mutations, with each Repository, before pushing changes to the database and, when all changes are ready, submit those changes to the database, with the Unit of Work acting as that intermediary between the repositories and the physical database.
- I also implemented unit tests for Utilities and basic items. Also, implemented an integration test for some of the Survey endpoints.

---

## Missing points and enhancement opportunities
- I only implemented data related mutations for the Survey, Question and QuestionOrder tables. So, if a Respondent gets deleted their related data in Response and SurveyResponse tables does not get deleted. It can be improved by creating a Stored Procedure that could handle this operations between these three last tables.
- Integration tests could be implemented for the others endpoints as well, not only the Survey endpoint.
- My implementation of the Repository pattern and the Unit of Work pattern could improved by abstracting another specific operations that some repositories have in common.
