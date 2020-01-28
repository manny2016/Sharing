CREATE FUNCTION funLpad
(
	@serial_no BIGINT,
	@max_length INT,
	@padtext CHAR(1)
)
RETURNS NVARCHAR(50)
AS
BEGIN
	DECLARE @fillString NVARCHAR(50);
	
	SET @fillString = TRIM(CONVERT(NVARCHAR(50),@serial_no));
	IF LEN(@fillString)>= @max_length 
		RETURN @fillString;

	DECLARE @required_fill_length INT;
	DECLARE @loopIndex INT;
	SET @required_fill_length =@max_length- LEN(@fillString);
	SET @loopIndex = 0;
	WHILE(@loopIndex<@required_fill_length)
	BEGIN
		SET @fillString=@padtext+ISNULL(@fillString,'')
		SET @loopIndex=@loopIndex+1
	END 
	RETURN TRIM(@fillString);
END
GO