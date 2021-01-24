INSERT INTO [dbo].[Survey] ([Name], [Description]) VALUES
('Survey 1', 'Description of survey 1'),
('Survey 2', 'Description of survey 2'),
('Survey 3', 'Description of survey 3');

INSERT INTO [dbo].[Question]([Text]) VALUES
('Is this question 1?'),
('Is this question 2?'),
('Is this question 3?'),
('Is this question 4?'),
('Is this question 5?'),
('Is this question 6?'),
('Is this question 7?'),
('Is this question 8?'),
('Is this question 9?');

INSERT INTO [dbo].[Respondent] ([Name], [HashedPassword], [Email]) VALUES
('John Doe', '$2y$12$GirbThIx1sP21AEKxohOSu9kcxy8BAFH3gZ7JLgnNuUrOonsUiBrO', 'john.doe@email.com'),
('Johnny Doe', '$2y$12$GirbThIx1sP21AEKxohOSu9kcxy8BAFH3gZ7JLgnNuUrOonsUiBrO', 'johnnny.doe@email.com'),
('Pepe', '$2y$12$GirbThIx1sP21AEKxohOSu9kcxy8BAFH3gZ7JLgnNuUrOonsUiBrO', 'pepe@email.com'),
('Pepito', '$2y$12$GirbThIx1sP21AEKxohOSu9kcxy8BAFH3gZ7JLgnNuUrOonsUiBrO', 'pepito@email.com'),
('Fulano', '$2y$12$GirbThIx1sP21AEKxohOSu9kcxy8BAFH3gZ7JLgnNuUrOonsUiBrO', 'fulano@email.com'),
('Fulanito', '$2y$12$GirbThIx1sP21AEKxohOSu9kcxy8BAFH3gZ7JLgnNuUrOonsUiBrO', 'fulanito@email.com'),
('Fulana', '$2y$12$GirbThIx1sP21AEKxohOSu9kcxy8BAFH3gZ7JLgnNuUrOonsUiBrO', 'fulana@email.com'),
('Fulanita', '$2y$12$GirbThIx1sP21AEKxohOSu9kcxy8BAFH3gZ7JLgnNuUrOonsUiBrO', 'fulanita@email.com');

INSERT INTO [dbo].[QuestionOrder] ([OrderNbr], [QuestionId], [SurveyId]) VALUES
(1, 9, 1),
(2, 8, 1),
(3, 7, 1),
(4, 6, 1),
(5, 5, 1),
(6, 4, 1),
(7, 3, 1),
(8, 2, 1),
(9, 1, 1),
(1, 6, 2),
(2, 8, 2),
(3, 7, 2),
(4, 9, 2),
(5, 2, 2),
(6, 4, 2),
(7, 3, 2),
(8, 5, 2),
(9, 1, 2);

INSERT INTO [dbo].[SurveyResponse] ([SurveyId], [RespondentId]) VALUES
(1, 1),
(1, 2),
(1, 3),
(2, 1);

INSERT INTO [dbo].[Response] ([SurveyResponseId], [QuestionId], [RespondentId], [Answer]) VALUES
(1, 1, 1, 'YES'),
(2, 1, 2, 'NO'),
(3, 1, 3, 'YES'),
(4, 1, 4, 'DO NOT KNOW'),
(2, 2, 1, 'NO');
