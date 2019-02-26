SET XACT_ABORT ON

BEGIN TRANSACTION


-- Create tables
CREATE TABLE dbo.Wreck (
	Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name] VARCHAR(255) NOT NULL,
	[Description] VARCHAR(2000) NULL,
	ImageName VARCHAR(500) NOT NULL,
	Price DECIMAL NOT NULL,
	AddedOn DATETIME2 NOT NULL
)

INSERT INTO dbo.Wreck ([Name], [Description], ImageName, Price, AddedOn) 
	VALUES ('Needs vacuum', 'well worn but ready for the right owner', 'bad-interior.jpg', 500.00, GETUTCDATE())

INSERT INTO dbo.Wreck ([Name], [Description], ImageName, Price, AddedOn) 
	VALUES ('Classic', 'Scuba special', 'car-under-water.jpg', 1000.00, GETUTCDATE())

INSERT INTO dbo.Wreck ([Name], [Description], ImageName, Price, AddedOn) 
	VALUES ('It''ll buff out', 'Appearances aren''t everything', 'it-will-buff-out.jpeg', 400.00, GETUTCDATE())

INSERT INTO dbo.Wreck ([Name], [Description], ImageName, Price, AddedOn) 
	VALUES ('Limitless possibilites', 'Tires sold separately', 'on-blocks.JPG', 1500.00, GETUTCDATE())

INSERT INTO dbo.Wreck ([Name], [Description], ImageName, Price, AddedOn) 
	VALUES ('Clear for takeoff', 'Sometime last century', 'plane.jpg', 5000.00, GETUTCDATE())

INSERT INTO dbo.Wreck ([Name], [Description], ImageName, Price, AddedOn) 
	VALUES ('Just located', 'Perfect for the right owner - with a crane', 'shipOnSide.jpg', 8000.00, GETUTCDATE())


COMMIT TRANSACTION
