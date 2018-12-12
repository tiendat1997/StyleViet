USE [StyleVietDB]
GO

/****** Object:  StoredProcedure [dbo].[RegisterAdminAccount]    Script Date: 12/12/2018 9:40:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegisterAdminAccount]
(
	@username NVARCHAR(MAX),  
    @password NVARCHAR(MAX),  
    @email NVARCHAR(MAX),  	
    @fullname NVARCHAR(MAX),  
	@phone NVARCHAR(MAX)
)
AS
BEGIN
	DECLARE @result VARCHAR(10) = 'Failed'
	DECLARE @newAccId INT 
	DECLARE @roleAdmin INT = 1	

	IF NOT EXISTS(SELECT 1 FROM Account a where a.Username = @username or UPPER(a.Email) = UPPER(@email))
	BEGIN
		INSERT INTO Account(Username, Password, Email, Expired)  
		VALUES(@username,@password,@email,0)

		SET @newAccId = (SELECT a.Id from Account a where a.Username = @username and a.Password = @password)
		IF  (@newAccId IS NOT NULL)
		BEGIN
			INSERT INTO AccountRole(AccountId,RoleId)
			VALUES(@newAccId, @roleAdmin)

			INSERT INTO [dbo].[Admin](AccountId,Fullname,Phone)
			VALUES(@newAccId,@fullname,@phone)
			SET @result='Success'
		END 		
	END
	SELECT @result AS Result  
END
GO


