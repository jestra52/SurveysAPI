CREATE PROCEDURE dbo.SP_UpdateQuestion
	@Id INT,
    @Text VARCHAR(200)
AS BEGIN
	UPDATE dbo.Question
	SET [Text] = ISNULL(@Text, [Text])
	WHERE Id = @Id;
END