CREATE PROCEDURE dbo.SP_UpdateRespondent
	@Id INT,
    @Name VARCHAR(50),
	@HashedPassword VARCHAR(1000),
	@Email VARCHAR(254)
AS BEGIN
	UPDATE dbo.Respondent
	SET [Name] = ISNULL(@Name, [Name]),
		[HashedPassword] = ISNULL(@HashedPassword, [HashedPassword]),
		[Email] = ISNULL(@Email, [Email])
	WHERE Id = @Id;
END