CREATE PROCEDURE dbo.SP_UpdateSurvey
	@Id INT,
    @Name VARCHAR(50),
    @Description VARCHAR(1000)
AS BEGIN
	UPDATE dbo.Survey
	SET [Name] = ISNULL(@Name, [Name]),
		[Description] = ISNULL(@Description, [Description])
	WHERE Id = @Id;
END