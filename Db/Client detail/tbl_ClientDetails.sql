CREATE TABLE tbl_ClientDetails
(
ClientId INT IDENTITY(1,1) NOT NULL, 
ClientName NVARCHAR(255) NOT NULL,
PhoneNo NVARCHAR(255) NOT NULL
)

INSERT INTO dbo.tbl_ClientDetails
(
    ClientName,
    PhoneNo
)
VALUES
(   'Navraj', -- ClientName - nvarchar(255)
    '9845486590'  -- PhoneNo - nvarchar(255)
    )

	SELECT* FROM dbo.tbl_ClientDetails
