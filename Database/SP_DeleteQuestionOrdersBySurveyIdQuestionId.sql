CREATE OR ALTER PROCEDURE dbo.SP_DeleteQuestionOrdersBySurveyIdQuestionId
	@QuestionId INT,
	@SurveyId INT
AS BEGIN
	IF @QuestionId IS NOT NULL BEGIN
		DELETE FROM dbo.QuestionOrder
		WHERE QuestionId = @QuestionId;
	END
	ELSE IF @SurveyId IS NOT NULL BEGIN
		DELETE FROM dbo.QuestionOrder
		WHERE SurveyId = @SurveyId;
	END
END