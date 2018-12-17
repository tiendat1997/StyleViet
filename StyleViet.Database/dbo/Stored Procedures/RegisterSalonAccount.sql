CREATE PROCEDURE [dbo].[RegisterSalonAccount]
(
	@username NVARCHAR(MAX),
	@salt NVARCHAR(MAX),  
    @password NVARCHAR(MAX),  
    @email NVARCHAR(MAX),  	
    @salonname NVARCHAR(MAX),  
	@phone NVARCHAR(MAX),
	@address NVARCHAR(MAX),
	@googleId NVARCHAR(MAX),
	@facebookId NVARCHAR(MAX)
)
AS
BEGIN
	DECLARE @result VARCHAR(10) = 'Failed'
	DECLARE @newAccId INT 
	DECLARE @roleSalon INT = 2	

	IF NOT EXISTS(SELECT 1 FROM Account a where a.Username = @username or UPPER(a.Email) = UPPER(@email))
	BEGIN
		INSERT INTO Account(Username, Password, Email, Expired,Salt,GoogleId,FacebookId)  
		VALUES(@username,@password,@email,0,@salt,@googleId,@facebookId)

		SET @newAccId = (SELECT a.Id from Account a where a.Username = @username and a.Password = @password)
		IF  (@newAccId IS NOT NULL)
		BEGIN
			INSERT INTO AccountRole(AccountId,RoleId)
			VALUES(@newAccId, @roleSalon)

			INSERT INTO [dbo].[Salon](AccountId,Name,Phone,Address,Email)
			VALUES(@newAccId,@salonname,@phone,@address,@email)
			SET @result='Success'
		END 		
	END
	SELECT @result AS Result  
END
