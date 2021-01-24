CREATE TABLE [dbo].[Survey] (
	[Id] INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	[Name] VARCHAR(50) NOT NULL,
	[Description] VARCHAR(1000),
	[Updated] TIMESTAMP NOT NULL
);

CREATE TABLE [dbo].[Question] (
	[Id] INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	[Text] VARCHAR(200),
	[Updated] TIMESTAMP NOT NULL
);

CREATE TABLE [dbo].[Respondent] (
	[Id] INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	[Name] VARCHAR(50) NOT NULL,
	[HashedPassword] VARCHAR(1000) NOT NULL,
	[Email] VARCHAR(254) NOT NULL,
	[Created] TIMESTAMP NOT NULL
);

CREATE TABLE [dbo].[QuestionOrder] (
	[OrderNbr] INT NOT NULL,
	[QuestionId] INT FOREIGN KEY REFERENCES dbo.Question(Id) ON UPDATE CASCADE ON DELETE CASCADE NOT NULL,
	[SurveyId] INT FOREIGN KEY REFERENCES dbo.Survey(Id) ON UPDATE CASCADE ON DELETE CASCADE NOT NULL,
	PRIMARY KEY ([QuestionId], [SurveyId])
);

CREATE TABLE [dbo].[SurveyResponse] (
	[Id] INT PRIMARY KEY NOT NULL IDENTITY(1,1),
	[SurveyId] INT FOREIGN KEY REFERENCES dbo.Survey(Id) ON UPDATE CASCADE ON DELETE CASCADE NOT NULL,
	[RespondentId] INT FOREIGN KEY REFERENCES dbo.Respondent(Id) ON UPDATE CASCADE ON DELETE CASCADE NOT NULL,
	[Updated] TIMESTAMP NOT NULL
);

CREATE TABLE [dbo].[Response] (
	[SurveyResponseId] INT FOREIGN KEY REFERENCES dbo.SurveyResponse(Id) ON UPDATE CASCADE ON DELETE CASCADE NOT NULL,
	[QuestionId] INT FOREIGN KEY REFERENCES dbo.Question(Id) ON UPDATE CASCADE ON DELETE CASCADE NOT NULL,
	[RespondentId] INT FOREIGN KEY REFERENCES dbo.Respondent(Id) NOT NULL,
	[Answer] VARCHAR(100) NOT NULL,
	PRIMARY KEY ([SurveyResponseId], [QuestionId], [RespondentId])
);