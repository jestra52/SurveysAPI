CREATE OR ALTER VIEW dbo.VSurveyResponses
AS
SELECT
	COUNT(sr.SurveyId) AS TotalResponses,
	s.[Id] AS SurveyId,
	s.[Name] AS SurveyName,
	s.[Description] AS SurveyDescription
FROM dbo.Survey s
INNER JOIN dbo.SurveyResponse sr
ON s.Id = sr.SurveyId
GROUP BY s.[Id], s.[Name], s.[Description]
