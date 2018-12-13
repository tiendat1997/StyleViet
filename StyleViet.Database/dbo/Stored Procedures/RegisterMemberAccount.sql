CREATE PROCEDURE [dbo].[RegisterMemberAccount]
(
	@username NVARCHAR(MAX),  
    @password NVARCHAR(MAX),  
    @email NVARCHAR(MAX),  	
    @firstname NVARCHAR(MAX),  
	@lastname NVARCHAR(MAX),
	@phone NVARCHAR(MAX)
)
AS
BEGIN
	DECLARE @result VARCHAR(10) = 'Failed'
	DECLARE @newAccId INT 
	DECLARE @roleMember INT = 3	

	IF NOT EXISTS(SELECT 1 FROM Account a where a.Username = @username or UPPER(a.Email) = UPPER(@email))
	BEGIN
		INSERT INTO Account(Username, Password, Email, Expired)  
		VALUES(@username,@password,@email,0)

		SET @newAccId = (SELECT a.Id from Account a where a.Username = @username and a.Password = @password)
		IF  (@newAccId IS NOT NULL)
		BEGIN
			INSERT INTO AccountRole(AccountId,RoleId)
			VALUES(@newAccId, @roleMember)

			INSERT INTO [dbo].[User](AccountId,Email,Phone,FirstName,LastName)
			VALUES(@newAccId,@email,@phone,@firstname,@lastname)
			SET @result='Success'
		END 		
	END
	SELECT @result AS Result  
END
GO


