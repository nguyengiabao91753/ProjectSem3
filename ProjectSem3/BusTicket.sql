-- Use the master database
USE master;
GO

-- Drop the database if it already exists
DROP DATABASE IF EXISTS Bus_Ticket;
GO

-- Create a new database
CREATE DATABASE Bus_Ticket;
GO

USE Bus_Ticket;
-- Create Levels table
CREATE TABLE Policies (
    PolicyID INT PRIMARY KEY IDENTITY(1,1),
	Title NVARCHAR(50),
    Content Text NOT NULL,
    Status TINYINT DEFAULT 1
);

CREATE TABLE Levels (
    LevelID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Status TINYINT DEFAULT 1
);

-- Create Modules table
--CREATE TABLE Modules (
--    ModuleID INT IDENTITY(1,1) PRIMARY KEY,
--    ModuleName NVARCHAR(50) NOT NULL
--);

-- Create Permissions table
--CREATE TABLE Permissions (
--    PermissionID INT IDENTITY(1,1) PRIMARY KEY,
--    ModuleID INT,
--    Name NVARCHAR(50),
--    Title NVARCHAR(24),
--    FOREIGN KEY (ModuleID) REFERENCES Modules(ModuleID)
--);

-- Create Level_Permissions table
--CREATE TABLE Level_Permissions (
--    ID INT IDENTITY(1,1) PRIMARY KEY,
--    LevelID INT,
--    PermissionID INT,
--    FOREIGN KEY (LevelID) REFERENCES Levels(LevelID),
--    FOREIGN KEY (PermissionID) REFERENCES Permissions(PermissionID)
--);

-- Create Bus Types table
CREATE TABLE BusTypes (
    BusTypeID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
	 Status TINYINT DEFAULT 1
);

-- Create Buss table
CREATE TABLE Buses (
    BusID INT PRIMARY KEY IDENTITY(1,1),
    BusTypeID INT FOREIGN KEY REFERENCES BusTypes(BusTypeID),
    AirConditioned TINYINT DEFAULT 0, -- Default no air conditioning
    LicensePlate NVARCHAR(20) NOT NULL,
    SeatCount INT NOT NULL,
    BasePrice DECIMAL(18,2) NOT NULL,
	Status TINYINT DEFAULT 1
);


CREATE TABLE Buses_Seats (
    SeatID INT PRIMARY KEY IDENTITY(1,1),
	BusID INT FOREIGN KEY REFERENCES Buses(BusID),
    Name NVARCHAR(20) NOT NULL,
    Status TINYINT DEFAULT 1 -- Available
);


-- Create Locations table
CREATE TABLE Locations (
    LocationID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
	Status TINYINT DEFAULT 1
);

-- Create TimeSlots table
--CREATE TABLE TimeSlots (
--    TimeSlotID INT PRIMARY KEY IDENTITY(1,1),
--    StartTime TIME NOT NULL,
--    EndTime TIME NOT NULL
--);

-- Create BusTrips table
CREATE TABLE Trips (
    TripID INT PRIMARY KEY IDENTITY(1,1),
    DepartureLocationID INT FOREIGN KEY REFERENCES Locations(LocationID),
    ArrivalLocationID INT FOREIGN KEY REFERENCES Locations(LocationID),
    DateStart DateTIME,
	DateEnd DATETIME,
    Status TINYINT DEFAULT 1 -- Active
);
CREATE TABLE BusesTrips (
    BusTripID INT PRIMARY KEY IDENTITY(1,1),
    BusID INT FOREIGN KEY REFERENCES Buses(BusID),
	TripID INT FOREIGN KEY REFERENCES Trips(TripID),
	Price DECIMAL(10,2),
    Status TINYINT DEFAULT 1 -- Active
);

-- Create Users table
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    BirthDate DATE NOT NULL,
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(15),
    Address NVARCHAR(200),
    CreatedAt DATETIME,
);

-- Create Accounts table
CREATE TABLE Accounts (
    AccountID INT PRIMARY KEY FOREIGN KEY REFERENCES Users(UserID),
	Username VARCHAR(50) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Status TINYINT DEFAULT 1,
    LevelID INT FOREIGN KEY REFERENCES Levels(LevelID)
);

-- Create AgeGroups table
CREATE TABLE AgeGroups (
    AgeGroupID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    Discount VARCHAR(50),
	Status TINYINT DEFAULT 1
);

-- Create TicketPrices table
--CREATE TABLE Enquiries  (
--    EnquiriesID INT PRIMARY KEY IDENTITY(1,1),
--    BusTripID INT FOREIGN KEY REFERENCES BusTrips(BusTripID),
--    AgeGroupID INT FOREIGN KEY REFERENCES AgeGroups(AgeGroupID),
--    Distance DECIMAL(10,2) NOT NULL,
--    PriceAfterDiscount DECIMAL(18,2) -- Price after discount or promotion
--);

-- Create Bookings table
CREATE TABLE Bookings (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
	FullName NVARCHAR(100) NULL,
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(15),
	UserID INT DEFAULT NULL,
	BusTripID INT FOREIGN KEY REFERENCES BusesTrips(BusTripID),
    BookingDate DATETIME NOT NULL,
    Total DECIMAL(18,2) NOT NULL,
    PaymentStatus TINYINT DEFAULT 0 -- Not Paid
);

CREATE TABLE BookingDetails(
    BookingDetailID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT FOREIGN KEY REFERENCES Bookings(BookingID),
    SeatID INT,
    SeatName NVARCHAR(20),
    AgeGroupID INT FOREIGN KEY REFERENCES AgeGroups(AgeGroupID),
    PriceAfterDiscount DECIMAL(18,2), -- Price after discount or promotion 
    TicketCode NVARCHAR(255) UNIQUE NOT NULL, -- Unique ticket code
    TicketStatus TINYINT DEFAULT 1,
);
GO



-- Create Payments table
CREATE TABLE Payments(
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT FOREIGN KEY REFERENCES Bookings(BookingID),
    PaymentDate DATETIME NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    PaymentMethod NVARCHAR(50)
);

-- Create CustomerFeedback table
CREATE TABLE CustomerFeedback (
    FeedbackID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
	BusTripID INT FOREIGN KEY REFERENCES BusesTrips(BusTripID),
    Content TEXT,
    FeedbackDate DATETIME NOT NULL
);
