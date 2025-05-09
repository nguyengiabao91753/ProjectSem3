USE [master]
GO
/****** Object:  Database [Bus_Ticket]    Script Date: 4/24/2025 5:49:05 PM ******/
CREATE DATABASE [Bus_Ticket]
USE [Bus_Ticket]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountID] [int] NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[Status] [tinyint] NULL,
	[LevelID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AgeGroups]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AgeGroups](
	[AgeGroupID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Discount] [varchar](50) NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[AgeGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BookingDetails]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookingDetails](
	[BookingDetailID] [int] IDENTITY(1,1) NOT NULL,
	[BookingID] [int] NULL,
	[SeatID] [int] NULL,
	[SeatName] [nvarchar](20) NULL,
	[AgeGroupID] [int] NULL,
	[PriceAfterDiscount] [decimal](18, 2) NULL,
	[TicketCode] [nvarchar](255) NOT NULL,
	[TicketStatus] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bookings]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookings](
	[BookingID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](15) NULL,
	[UserID] [int] NULL,
	[BusTripID] [int] NULL,
	[BookingDate] [datetime] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[PaymentStatus] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Buses]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buses](
	[BusID] [int] IDENTITY(1,1) NOT NULL,
	[BusTypeID] [int] NULL,
	[AirConditioned] [tinyint] NULL,
	[LicensePlate] [nvarchar](20) NOT NULL,
	[SeatCount] [int] NOT NULL,
	[BasePrice] [decimal](18, 2) NOT NULL,
	[Status] [tinyint] NULL,
	[LocationID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Buses_Seats]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buses_Seats](
	[SeatID] [int] IDENTITY(1,1) NOT NULL,
	[BusID] [int] NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[SeatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusesTrips]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusesTrips](
	[BusTripID] [int] IDENTITY(1,1) NOT NULL,
	[BusID] [int] NULL,
	[TripID] [int] NULL,
	[Price] [decimal](10, 2) NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[BusTripID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusTypes]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusTypes](
	[BusTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[BusTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerFeedback]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerFeedback](
	[FeedbackID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[BusTripID] [int] NULL,
	[Content] [text] NULL,
	[FeedbackDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Levels]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Levels](
	[LevelID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[LevelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[LocationID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[BookingID] [int] NULL,
	[PaymentDate] [datetime] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PaymentMethod] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Policies]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Policies](
	[PolicyID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Content] [text] NOT NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[PolicyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trips]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trips](
	[TripID] [int] IDENTITY(1,1) NOT NULL,
	[DepartureLocationID] [int] NULL,
	[ArrivalLocationID] [int] NULL,
	[DateStart] [datetime] NULL,
	[DateEnd] [datetime] NULL,
	[Status] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[TripID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 4/24/2025 5:49:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[Email] [nvarchar](100) NULL,
	[PhoneNumber] [nvarchar](15) NULL,
	[Address] [nvarchar](200) NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Accounts] ([AccountID], [Username], [Password], [Status], [LevelID]) VALUES (6, N'baobao', N'$2b$10$rctxd9GAQS94ETm9gH7VTew4fvu9uEnUUwS4kIwPGIjG4OO4K5Ov.', 1, 3)
INSERT [dbo].[Accounts] ([AccountID], [Username], [Password], [Status], [LevelID]) VALUES (7, N'vana', N'$2b$10$XY1abxEETcF6b5cDIJqoKOQ/aoQcZdZtr3bYA2u4Vt/03PJr.EN2i', 1, 3)
INSERT [dbo].[Accounts] ([AccountID], [Username], [Password], [Status], [LevelID]) VALUES (8, N'admin', N'$2b$10$jeIxv9shQZStm7W1POKWveD/ruewp9A.dTEBKylWs.7OP5Rgqk26W', 1, 1)
INSERT [dbo].[Accounts] ([AccountID], [Username], [Password], [Status], [LevelID]) VALUES (9, N'staff', N'$2b$10$M5jLOYrIin9ka/H5Z7erhe3eRejL4H0xsIPiRDpNp872F5JK3bYGW', 0, 2)
INSERT [dbo].[Accounts] ([AccountID], [Username], [Password], [Status], [LevelID]) VALUES (10, N'test', N'$2b$10$FGPv./TXzSdZgAW8Znf2AOYYaAVyGa81sYl9xIyhE4thyouTcxjw2', 1, 2)
INSERT [dbo].[Accounts] ([AccountID], [Username], [Password], [Status], [LevelID]) VALUES (11, N'Duy', N'$2b$10$BMJXaRCTkhLl3pXlz7i82eEpwCYEUdKNAYFusyIB/T3fdBXKKY38K', 1, 2)
INSERT [dbo].[Accounts] ([AccountID], [Username], [Password], [Status], [LevelID]) VALUES (12, N'huyhuy', N'$2b$10$eUv6BZrJM3rOGkpYUYMEKeSfQ5dSdIV4Rt2wkgxaOGWFmPin1Asha', 1, 3)
INSERT [dbo].[Accounts] ([AccountID], [Username], [Password], [Status], [LevelID]) VALUES (13, N'baonew', N'$2b$10$Vh3gJek2CVEFf1Uvq9UJpu0m7UbN2mCQd5v3OJNVxyxPNzAHicfwu', 1, 3)
GO
SET IDENTITY_INSERT [dbo].[AgeGroups] ON 

INSERT [dbo].[AgeGroups] ([AgeGroupID], [Name], [Discount], [Status]) VALUES (1, N'Age 5-12', N'50', 1)
INSERT [dbo].[AgeGroups] ([AgeGroupID], [Name], [Discount], [Status]) VALUES (2, N'Age 12-50', N'0', 1)
INSERT [dbo].[AgeGroups] ([AgeGroupID], [Name], [Discount], [Status]) VALUES (3, N'Age >50', N'30', 1)
SET IDENTITY_INSERT [dbo].[AgeGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[BookingDetails] ON 

INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (1, 1, 17, N'1', 1, CAST(9.00 AS Decimal(18, 2)), N'82112728575', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (2, 1, 18, N'2', 2, CAST(18.00 AS Decimal(18, 2)), N'796989120123', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (5, 3, 21, N'5', 3, CAST(12.60 AS Decimal(18, 2)), N'1107991119117', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (6, 3, 22, N'6', 2, CAST(18.00 AS Decimal(18, 2)), N'106116117108106', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (7, 4, 19, N'3', 2, CAST(18.00 AS Decimal(18, 2)), N'104129106128108', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (8, 5, 1, N'1', 2, CAST(160.00 AS Decimal(18, 2)), N'112117968893', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (9, 6, 2, N'2', 1, CAST(80.00 AS Decimal(18, 2)), N'VNPCr9', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (10, 7, 26, N'10', 1, CAST(9.00 AS Decimal(18, 2)), N'QQmxs10', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (11, 8, 25, N'9', 1, CAST(9.00 AS Decimal(18, 2)), N'oYdYz11', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (12, 8, 29, N'13', 2, CAST(18.00 AS Decimal(18, 2)), N'rwRPs12', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (13, 9, 20, N'4', 2, CAST(18.00 AS Decimal(18, 2)), N'NAIRg13', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (14, 10, 23, N'7', 2, CAST(17.00 AS Decimal(18, 2)), N'kkNzi14', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (15, 11, 24, N'8', 2, CAST(18.00 AS Decimal(18, 2)), N'wmXpv15', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (16, 12, 5, N'5', 2, CAST(160.00 AS Decimal(18, 2)), N'tAfTK16', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (17, 12, 6, N'6', 1, CAST(80.00 AS Decimal(18, 2)), N'PsXVG17', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (18, 13, 30, N'14', 2, CAST(18.00 AS Decimal(18, 2)), N'ESMLu18', 0)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (19, 13, 34, N'18', 1, CAST(9.00 AS Decimal(18, 2)), N'KRbrE19', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (20, 14, 35, N'1', 1, CAST(55.50 AS Decimal(18, 2)), N'LLSFx20', 0)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (21, 14, 36, N'2', 2, CAST(111.00 AS Decimal(18, 2)), N'ybgqp21', 0)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (22, 15, 33, N'17', 2, CAST(12.00 AS Decimal(18, 2)), N'cguvr22', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (23, 16, 17, N'1', 2, CAST(12.00 AS Decimal(18, 2)), N'plxKk23', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (24, 17, 18, N'2', 2, CAST(12.00 AS Decimal(18, 2)), N'mDGji24', 0)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (25, 18, 22, N'6', 2, CAST(12.00 AS Decimal(18, 2)), N'WzuTE25', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (26, 19, 21, N'5', 2, CAST(12.00 AS Decimal(18, 2)), N'ahmQl26', 0)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (27, 20, 115, N'1', 1, CAST(10.00 AS Decimal(18, 2)), N'qlSgw27', 0)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (28, 21, 37, N'3', 2, CAST(13.00 AS Decimal(18, 2)), N'Rwvac28', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (29, 22, 95, N'1', 3, CAST(10.50 AS Decimal(18, 2)), N'BIUDX29', 0)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (30, 23, 80, N'14', 2, CAST(10.00 AS Decimal(18, 2)), N'vWvIA30', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (31, 23, 79, N'13', 2, CAST(10.00 AS Decimal(18, 2)), N'OEIGK31', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (32, 24, 71, N'5', 1, CAST(5.00 AS Decimal(18, 2)), N'kkHiV32', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (33, 24, 72, N'6', 3, CAST(7.00 AS Decimal(18, 2)), N'VdWYO33', 0)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (34, 25, 95, N'1', 2, CAST(10.00 AS Decimal(18, 2)), N'elGZF34', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (35, 25, 96, N'2', 2, CAST(10.00 AS Decimal(18, 2)), N'CIMOJ35', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (36, 25, 99, N'5', 1, CAST(5.00 AS Decimal(18, 2)), N'wWyNL36', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (37, 25, 100, N'6', 3, CAST(7.00 AS Decimal(18, 2)), N'hCKET37', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (38, 25, 103, N'9', 2, CAST(10.00 AS Decimal(18, 2)), N'ruGae38', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (39, 25, 104, N'10', 3, CAST(7.00 AS Decimal(18, 2)), N'YYtdU39', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (40, 25, 107, N'13', 1, CAST(5.00 AS Decimal(18, 2)), N'oRupY40', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (41, 25, 108, N'14', 1, CAST(5.00 AS Decimal(18, 2)), N'rCQrb41', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (42, 25, 111, N'17', 1, CAST(5.00 AS Decimal(18, 2)), N'pKMVC42', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (43, 25, 112, N'18', 1, CAST(5.00 AS Decimal(18, 2)), N'JSGvR43', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (44, 25, 113, N'19', 1, CAST(5.00 AS Decimal(18, 2)), N'KzBuX44', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (45, 25, 114, N'20', 1, CAST(5.00 AS Decimal(18, 2)), N'CVXuv45', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (46, 25, 110, N'16', 1, CAST(5.00 AS Decimal(18, 2)), N'wzMyT46', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (47, 25, 109, N'15', 1, CAST(5.00 AS Decimal(18, 2)), N'fWrGN47', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (48, 25, 105, N'11', 1, CAST(5.00 AS Decimal(18, 2)), N'EdtQW48', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (49, 25, 106, N'12', 1, CAST(5.00 AS Decimal(18, 2)), N'FvXbW49', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (50, 25, 102, N'8', 1, CAST(5.00 AS Decimal(18, 2)), N'KiABM50', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (51, 25, 101, N'7', 1, CAST(5.00 AS Decimal(18, 2)), N'NnYXt51', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (52, 25, 97, N'3', 1, CAST(5.00 AS Decimal(18, 2)), N'akWJa52', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (53, 25, 98, N'4', 2, CAST(10.00 AS Decimal(18, 2)), N'fipBV53', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (54, 26, 143, N'1', 2, CAST(10.00 AS Decimal(18, 2)), N'IzDHF54', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (55, 27, 144, N'2', 2, CAST(10.00 AS Decimal(18, 2)), N'aqcgd55', 1)
INSERT [dbo].[BookingDetails] ([BookingDetailID], [BookingID], [SeatID], [SeatName], [AgeGroupID], [PriceAfterDiscount], [TicketCode], [TicketStatus]) VALUES (56, 28, 115, N'1', 2, CAST(13.00 AS Decimal(18, 2)), N'TFWbD56', 1)
SET IDENTITY_INSERT [dbo].[BookingDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Bookings] ON 

INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (1, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 3, CAST(N'2024-09-29T16:30:00.497' AS DateTime), CAST(27.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (3, N'Nguyen Gia Bao', N'giabao2082004@gmail.com', N'0768920314', 0, 3, CAST(N'2024-09-29T16:34:29.877' AS DateTime), CAST(30.60 AS Decimal(18, 2)), 0)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (4, N'Nguyen Gia Huy', N'huy@gmail.com', N'343453645345', 0, 3, CAST(N'2024-09-29T16:54:04.120' AS DateTime), CAST(18.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (5, N'Nguyen Lan', N'lan@gmail.com', N'0768920314', 0, 2, CAST(N'2024-09-29T23:05:56.087' AS DateTime), CAST(160.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (6, N'Nguyen Hue', N'hue@gmail.com', N'0768920314', 0, 2, CAST(N'2024-09-29T23:08:16.500' AS DateTime), CAST(80.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (7, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 3, CAST(N'2024-10-01T14:59:32.700' AS DateTime), CAST(9.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (8, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 3, CAST(N'2024-10-01T23:15:44.460' AS DateTime), CAST(27.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (9, N'Nguyen Gia Huy', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 3, CAST(N'2024-10-01T23:20:04.473' AS DateTime), CAST(18.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (10, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 1, CAST(N'2024-10-01T23:30:09.857' AS DateTime), CAST(17.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (11, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 3, CAST(N'2024-10-02T18:19:04.210' AS DateTime), CAST(18.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (12, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 2, CAST(N'2024-10-02T18:28:46.253' AS DateTime), CAST(240.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (13, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 3, CAST(N'2024-10-02T18:32:55.680' AS DateTime), CAST(27.00 AS Decimal(18, 2)), 0)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (14, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 4, CAST(N'2024-10-02T18:43:08.550' AS DateTime), CAST(166.50 AS Decimal(18, 2)), 0)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (15, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 5, CAST(N'2024-10-12T11:26:59.957' AS DateTime), CAST(12.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (16, N'Nguyen Van A', N'giabao2082004@gmail.com', N'123456789', 0, 5, CAST(N'2024-10-13T15:02:44.377' AS DateTime), CAST(12.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (17, N'Nguyen Van A', N'giabao2082004@gmail.com', N'123456789', 0, 5, CAST(N'2024-10-13T15:04:58.947' AS DateTime), CAST(12.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (18, N'Nguyen Gia Huy', N'giabao2082004@gmail.com', N'0768920314', 0, 5, CAST(N'2024-10-14T16:37:04.920' AS DateTime), CAST(12.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (19, N'Nguyen Lan', N'giabao2082004@gmail.com', N'0768920314', 0, 5, CAST(N'2024-10-14T17:01:00.167' AS DateTime), CAST(12.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (20, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 6, CAST(N'2024-10-14T20:11:13.803' AS DateTime), CAST(10.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (21, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 8, CAST(N'2025-02-13T09:46:31.843' AS DateTime), CAST(13.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (22, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 9, CAST(N'2025-02-13T10:02:11.153' AS DateTime), CAST(10.50 AS Decimal(18, 2)), 1)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (23, N'Nguyen Thế Huy', N'huy@gmail.com', N'0768920314', 0, 12, CAST(N'2025-02-19T07:32:39.350' AS DateTime), CAST(20.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (24, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 12, CAST(N'2025-02-20T07:17:12.427' AS DateTime), CAST(12.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (25, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 11, CAST(N'2025-02-20T10:53:59.870' AS DateTime), CAST(124.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (26, N'Nguyen Gia Bao', N'giabao2082005@gmail.com', N'0768920314', 0, 15, CAST(N'2025-04-12T09:07:42.497' AS DateTime), CAST(10.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (27, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0758964123', 0, 15, CAST(N'2025-04-12T09:10:14.330' AS DateTime), CAST(10.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[Bookings] ([BookingID], [FullName], [Email], [PhoneNumber], [UserID], [BusTripID], [BookingDate], [Total], [PaymentStatus]) VALUES (28, N'Nguyen Gia Bao', N'nguyengiabao91753@gmail.com', N'0768920314', 0, 16, CAST(N'2025-04-15T15:53:30.460' AS DateTime), CAST(13.00 AS Decimal(18, 2)), 1)
SET IDENTITY_INSERT [dbo].[Bookings] OFF
GO
SET IDENTITY_INSERT [dbo].[Buses] ON 

INSERT [dbo].[Buses] ([BusID], [BusTypeID], [AirConditioned], [LicensePlate], [SeatCount], [BasePrice], [Status], [LocationID]) VALUES (1, 1, 0, N'DHA-01', 16, CAST(10.00 AS Decimal(18, 2)), 1, NULL)
INSERT [dbo].[Buses] ([BusID], [BusTypeID], [AirConditioned], [LicensePlate], [SeatCount], [BasePrice], [Status], [LocationID]) VALUES (2, 2, 1, N'DHV-01', 18, CAST(15.00 AS Decimal(18, 2)), 1, NULL)
INSERT [dbo].[Buses] ([BusID], [BusTypeID], [AirConditioned], [LicensePlate], [SeatCount], [BasePrice], [Status], [LocationID]) VALUES (3, 2, 1, N'51H.012.34', 32, CAST(900.00 AS Decimal(18, 2)), 1, NULL)
INSERT [dbo].[Buses] ([BusID], [BusTypeID], [AirConditioned], [LicensePlate], [SeatCount], [BasePrice], [Status], [LocationID]) VALUES (4, 3, 0, N'DHV-02', 28, CAST(12.00 AS Decimal(18, 2)), 1, NULL)
INSERT [dbo].[Buses] ([BusID], [BusTypeID], [AirConditioned], [LicensePlate], [SeatCount], [BasePrice], [Status], [LocationID]) VALUES (5, 1, 0, N'Bus-test1', 20, CAST(12.00 AS Decimal(18, 2)), 1, 8)
INSERT [dbo].[Buses] ([BusID], [BusTypeID], [AirConditioned], [LicensePlate], [SeatCount], [BasePrice], [Status], [LocationID]) VALUES (6, 2, 1, N'ABCD1234', 28, CAST(15.00 AS Decimal(18, 2)), 2, 2)
INSERT [dbo].[Buses] ([BusID], [BusTypeID], [AirConditioned], [LicensePlate], [SeatCount], [BasePrice], [Status], [LocationID]) VALUES (7, 2, 1, N'51H-300', 28, CAST(15.00 AS Decimal(18, 2)), 1, 1)
SET IDENTITY_INSERT [dbo].[Buses] OFF
GO
SET IDENTITY_INSERT [dbo].[Buses_Seats] ON 

INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (1, 1, N'1', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (2, 1, N'2', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (3, 1, N'3', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (4, 1, N'4', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (5, 1, N'5', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (6, 1, N'6', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (7, 1, N'7', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (8, 1, N'8', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (9, 1, N'9', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (10, 1, N'10', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (11, 1, N'11', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (12, 1, N'12', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (13, 1, N'13', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (14, 1, N'14', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (15, 1, N'15', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (16, 1, N'16', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (17, 2, N'1', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (18, 2, N'2', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (19, 2, N'3', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (20, 2, N'4', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (21, 2, N'5', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (22, 2, N'6', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (23, 2, N'7', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (24, 2, N'8', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (25, 2, N'9', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (26, 2, N'10', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (27, 2, N'11', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (28, 2, N'12', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (29, 2, N'13', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (30, 2, N'14', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (31, 2, N'15', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (32, 2, N'16', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (33, 2, N'17', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (34, 2, N'18', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (35, 3, N'1', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (36, 3, N'2', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (37, 3, N'3', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (38, 3, N'4', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (39, 3, N'5', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (40, 3, N'6', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (41, 3, N'7', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (42, 3, N'8', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (43, 3, N'9', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (44, 3, N'10', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (45, 3, N'11', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (46, 3, N'12', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (47, 3, N'13', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (48, 3, N'14', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (49, 3, N'15', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (50, 3, N'16', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (51, 3, N'17', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (52, 3, N'18', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (53, 3, N'19', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (54, 3, N'20', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (55, 3, N'21', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (56, 3, N'22', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (57, 3, N'23', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (58, 3, N'24', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (59, 3, N'25', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (60, 3, N'26', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (61, 3, N'27', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (62, 3, N'28', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (63, 3, N'29', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (64, 3, N'30', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (65, 3, N'31', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (66, 3, N'32', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (67, 4, N'1', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (68, 4, N'2', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (69, 4, N'3', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (70, 4, N'4', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (71, 4, N'5', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (72, 4, N'6', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (73, 4, N'7', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (74, 4, N'8', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (75, 4, N'9', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (76, 4, N'10', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (77, 4, N'11', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (78, 4, N'12', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (79, 4, N'13', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (80, 4, N'14', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (81, 4, N'15', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (82, 4, N'16', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (83, 4, N'17', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (84, 4, N'18', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (85, 4, N'19', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (86, 4, N'20', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (87, 4, N'21', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (88, 4, N'22', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (89, 4, N'23', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (90, 4, N'24', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (91, 4, N'25', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (92, 4, N'26', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (93, 4, N'27', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (94, 4, N'28', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (95, 5, N'1', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (96, 5, N'2', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (97, 5, N'3', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (98, 5, N'4', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (99, 5, N'5', 1)
GO
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (100, 5, N'6', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (101, 5, N'7', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (102, 5, N'8', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (103, 5, N'9', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (104, 5, N'10', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (105, 5, N'11', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (106, 5, N'12', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (107, 5, N'13', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (108, 5, N'14', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (109, 5, N'15', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (110, 5, N'16', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (111, 5, N'17', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (112, 5, N'18', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (113, 5, N'19', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (114, 5, N'20', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (115, 6, N'1', 0)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (116, 6, N'2', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (117, 6, N'3', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (118, 6, N'4', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (119, 6, N'5', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (120, 6, N'6', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (121, 6, N'7', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (122, 6, N'8', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (123, 6, N'9', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (124, 6, N'10', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (125, 6, N'11', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (126, 6, N'12', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (127, 6, N'13', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (128, 6, N'14', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (129, 6, N'15', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (130, 6, N'16', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (131, 6, N'17', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (132, 6, N'18', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (133, 6, N'19', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (134, 6, N'20', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (135, 6, N'21', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (136, 6, N'22', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (137, 6, N'23', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (138, 6, N'24', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (139, 6, N'25', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (140, 6, N'26', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (141, 6, N'27', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (142, 6, N'28', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (143, 7, N'1', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (144, 7, N'2', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (145, 7, N'3', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (146, 7, N'4', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (147, 7, N'5', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (148, 7, N'6', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (149, 7, N'7', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (150, 7, N'8', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (151, 7, N'9', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (152, 7, N'10', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (153, 7, N'11', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (154, 7, N'12', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (155, 7, N'13', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (156, 7, N'14', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (157, 7, N'15', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (158, 7, N'16', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (159, 7, N'17', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (160, 7, N'18', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (161, 7, N'19', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (162, 7, N'20', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (163, 7, N'21', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (164, 7, N'22', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (165, 7, N'23', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (166, 7, N'24', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (167, 7, N'25', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (168, 7, N'26', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (169, 7, N'27', 1)
INSERT [dbo].[Buses_Seats] ([SeatID], [BusID], [Name], [Status]) VALUES (170, 7, N'28', 1)
SET IDENTITY_INSERT [dbo].[Buses_Seats] OFF
GO
SET IDENTITY_INSERT [dbo].[BusesTrips] ON 

INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (1, 2, 1, CAST(17.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (2, 1, 1, CAST(160.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (3, 2, 1, CAST(18.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (4, 3, 3, CAST(111.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (5, 2, 4, CAST(12.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (6, 6, 6, CAST(20.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (7, 5, 4, CAST(12.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (8, 3, 7, CAST(13.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (9, 5, 8, CAST(15.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (10, 6, 11, CAST(12.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (11, 5, 10, CAST(10.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (12, 4, 9, CAST(10.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (13, 3, 12, CAST(10.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (14, 3, 13, CAST(10.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (15, 7, 16, CAST(10.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (16, 6, 17, CAST(13.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[BusesTrips] ([BusTripID], [BusID], [TripID], [Price], [Status]) VALUES (17, 5, 18, CAST(12.00 AS Decimal(10, 2)), 2)
SET IDENTITY_INSERT [dbo].[BusesTrips] OFF
GO
SET IDENTITY_INSERT [dbo].[BusTypes] ON 

INSERT [dbo].[BusTypes] ([BusTypeID], [Name], [Status]) VALUES (1, N'Express', 1)
INSERT [dbo].[BusTypes] ([BusTypeID], [Name], [Status]) VALUES (2, N'Volvo', 1)
INSERT [dbo].[BusTypes] ([BusTypeID], [Name], [Status]) VALUES (3, N'Luxury', 1)
SET IDENTITY_INSERT [dbo].[BusTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Levels] ON 

INSERT [dbo].[Levels] ([LevelID], [Name], [Status]) VALUES (1, N'Admin', 1)
INSERT [dbo].[Levels] ([LevelID], [Name], [Status]) VALUES (2, N'Staff', 1)
INSERT [dbo].[Levels] ([LevelID], [Name], [Status]) VALUES (3, N'Passenger', 1)
SET IDENTITY_INSERT [dbo].[Levels] OFF
GO
SET IDENTITY_INSERT [dbo].[Locations] ON 

INSERT [dbo].[Locations] ([LocationID], [Name], [Status]) VALUES (1, N'Ho Chi Minh', 1)
INSERT [dbo].[Locations] ([LocationID], [Name], [Status]) VALUES (2, N'Hue', 1)
INSERT [dbo].[Locations] ([LocationID], [Name], [Status]) VALUES (3, N'Nha Trang', 1)
INSERT [dbo].[Locations] ([LocationID], [Name], [Status]) VALUES (4, N'Hà Nội', 1)
INSERT [dbo].[Locations] ([LocationID], [Name], [Status]) VALUES (5, N'Đà Nẵng', 1)
INSERT [dbo].[Locations] ([LocationID], [Name], [Status]) VALUES (6, N'Cà Mau', 1)
INSERT [dbo].[Locations] ([LocationID], [Name], [Status]) VALUES (7, N'Vĩnh Long', 1)
INSERT [dbo].[Locations] ([LocationID], [Name], [Status]) VALUES (8, N'Hội An', 1)
INSERT [dbo].[Locations] ([LocationID], [Name], [Status]) VALUES (9, N'Nghệ An', 1)
INSERT [dbo].[Locations] ([LocationID], [Name], [Status]) VALUES (10, N'Thanh Hóa', 1)
INSERT [dbo].[Locations] ([LocationID], [Name], [Status]) VALUES (11, N'Hà Tĩnh', 1)
INSERT [dbo].[Locations] ([LocationID], [Name], [Status]) VALUES (12, N'Quảng Ngãi', 1)
SET IDENTITY_INSERT [dbo].[Locations] OFF
GO
SET IDENTITY_INSERT [dbo].[Payments] ON 

INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (1, 7, CAST(N'2024-10-01T14:59:32.880' AS DateTime), CAST(9.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (2, 8, CAST(N'2024-10-01T23:15:44.703' AS DateTime), CAST(27.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (3, 9, CAST(N'2024-10-01T23:20:04.647' AS DateTime), CAST(18.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (4, 10, CAST(N'2024-10-01T23:30:10.133' AS DateTime), CAST(17.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (5, 11, CAST(N'2024-10-02T18:19:04.623' AS DateTime), CAST(18.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (6, 12, CAST(N'2024-10-02T18:28:48.717' AS DateTime), CAST(240.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (7, 13, CAST(N'2024-10-02T18:32:58.020' AS DateTime), CAST(27.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (8, 14, CAST(N'2024-10-02T18:43:08.647' AS DateTime), CAST(166.50 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (9, 15, CAST(N'2024-10-12T11:27:00.410' AS DateTime), CAST(12.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (10, 16, CAST(N'2024-10-13T15:02:44.523' AS DateTime), CAST(12.00 AS Decimal(18, 2)), N'VNPay')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (11, 17, CAST(N'2024-10-13T15:04:58.980' AS DateTime), CAST(12.00 AS Decimal(18, 2)), N'VNPay')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (12, 18, CAST(N'2024-10-14T16:37:05.230' AS DateTime), CAST(12.00 AS Decimal(18, 2)), N'VNPay')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (13, 19, CAST(N'2024-10-14T17:01:00.677' AS DateTime), CAST(12.00 AS Decimal(18, 2)), N'VNPay')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (14, 20, CAST(N'2024-10-14T20:11:13.850' AS DateTime), CAST(10.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (15, 21, CAST(N'2025-02-13T09:46:31.960' AS DateTime), CAST(13.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (16, 22, CAST(N'2025-02-13T10:02:11.230' AS DateTime), CAST(10.50 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (17, 23, CAST(N'2025-02-19T07:32:39.563' AS DateTime), CAST(20.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (18, 24, CAST(N'2025-02-20T07:17:12.740' AS DateTime), CAST(12.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (19, 25, CAST(N'2025-02-20T10:54:00.353' AS DateTime), CAST(124.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (20, 26, CAST(N'2025-04-12T09:07:42.703' AS DateTime), CAST(10.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (21, 27, CAST(N'2025-04-12T09:10:14.423' AS DateTime), CAST(10.00 AS Decimal(18, 2)), N'Paypal')
INSERT [dbo].[Payments] ([PaymentID], [BookingID], [PaymentDate], [Amount], [PaymentMethod]) VALUES (22, 28, CAST(N'2025-04-15T15:53:30.847' AS DateTime), CAST(13.00 AS Decimal(18, 2)), N'Paypal')
SET IDENTITY_INSERT [dbo].[Payments] OFF
GO
SET IDENTITY_INSERT [dbo].[Policies] ON 

INSERT [dbo].[Policies] ([PolicyID], [Title], [Content], [Status]) VALUES (1, N'bao', N'111', 1)
INSERT [dbo].[Policies] ([PolicyID], [Title], [Content], [Status]) VALUES (2, N'Hong River', N'11111', 1)
INSERT [dbo].[Policies] ([PolicyID], [Title], [Content], [Status]) VALUES (3, N'Hong River1', N'222222', 1)
SET IDENTITY_INSERT [dbo].[Policies] OFF
GO
SET IDENTITY_INSERT [dbo].[Trips] ON 

INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (1, 1, 2, CAST(N'2024-09-28T05:00:00.000' AS DateTime), CAST(N'2024-09-29T17:00:00.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (2, 1, 1, CAST(N'2024-09-29T10:00:00.000' AS DateTime), CAST(N'2024-10-01T13:30:00.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (3, 3, 3, CAST(N'2024-10-11T06:00:00.000' AS DateTime), CAST(N'2024-10-16T06:30:00.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (4, 1, 2, CAST(N'2024-10-15T19:30:56.000' AS DateTime), CAST(N'2024-10-17T19:30:04.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (5, 4, 5, CAST(N'2024-10-18T17:00:00.000' AS DateTime), CAST(N'2024-10-19T18:00:00.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (6, 8, 4, CAST(N'2024-10-17T20:05:35.000' AS DateTime), CAST(N'2024-10-21T20:05:48.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (7, 2, 3, CAST(N'2025-02-16T09:21:49.000' AS DateTime), CAST(N'2025-02-17T09:22:00.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (8, 4, 3, CAST(N'2025-02-18T09:59:31.000' AS DateTime), CAST(N'2025-02-19T09:59:35.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (9, 11, 12, CAST(N'2025-02-20T12:14:10.000' AS DateTime), CAST(N'2025-02-21T12:14:13.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (10, 3, 2, CAST(N'2025-02-20T12:14:23.000' AS DateTime), CAST(N'2025-02-22T12:14:26.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (11, 12, 9, CAST(N'2025-02-20T12:14:36.000' AS DateTime), CAST(N'2025-02-22T12:14:38.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (12, 3, 4, CAST(N'2025-02-28T21:35:09.000' AS DateTime), CAST(N'2025-03-01T21:35:14.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (13, 4, 3, CAST(N'2025-03-02T21:41:39.000' AS DateTime), CAST(N'2025-03-04T21:41:44.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (14, 4, 3, CAST(N'2025-02-26T21:42:10.000' AS DateTime), CAST(N'2025-02-28T21:42:12.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (15, 4, 5, CAST(N'2025-03-06T18:32:10.000' AS DateTime), CAST(N'2025-03-07T18:32:14.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (16, 3, 1, CAST(N'2025-04-13T09:03:27.000' AS DateTime), CAST(N'2025-04-14T09:03:29.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (17, 4, 2, CAST(N'2025-04-20T09:55:14.000' AS DateTime), CAST(N'2025-04-21T09:55:17.000' AS DateTime), 1)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (18, 9, 8, CAST(N'2025-04-15T09:57:07.000' AS DateTime), CAST(N'2025-04-17T09:57:09.000' AS DateTime), 2)
INSERT [dbo].[Trips] ([TripID], [DepartureLocationID], [ArrivalLocationID], [DateStart], [DateEnd], [Status]) VALUES (19, 8, 3, CAST(N'2025-04-16T09:58:22.000' AS DateTime), CAST(N'2025-04-18T09:58:25.000' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[Trips] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [FullName], [BirthDate], [Email], [PhoneNumber], [Address], [CreatedAt]) VALUES (6, N'Nguyen Gia Bao', CAST(N'2004-08-20' AS Date), N'nguyengiabao91753@gmail.com', N'012356468', N'12 An Duong Vuong 45', CAST(N'2024-10-09T21:16:11.073' AS DateTime))
INSERT [dbo].[Users] ([UserID], [FullName], [BirthDate], [Email], [PhoneNumber], [Address], [CreatedAt]) VALUES (7, N'Nguyen Van A', CAST(N'2004-08-20' AS Date), N'giabao2082004@gmail.com', N'123456789', N'Hoc Mon', CAST(N'2024-10-13T14:58:45.197' AS DateTime))
INSERT [dbo].[Users] ([UserID], [FullName], [BirthDate], [Email], [PhoneNumber], [Address], [CreatedAt]) VALUES (8, N'Admin', CAST(N'2004-08-20' AS Date), N'admin@gmail.com', N'1234567890', N'Hoc Mon', CAST(N'2024-10-14T08:35:23.783' AS DateTime))
INSERT [dbo].[Users] ([UserID], [FullName], [BirthDate], [Email], [PhoneNumber], [Address], [CreatedAt]) VALUES (9, N'Nguyễn Văn Staff', CAST(N'1990-01-01' AS Date), N'giabao2082004@gmail.com', N'0768920314', N'12 An Duong Vuong 45', CAST(N'2024-10-14T16:08:58.830' AS DateTime))
INSERT [dbo].[Users] ([UserID], [FullName], [BirthDate], [Email], [PhoneNumber], [Address], [CreatedAt]) VALUES (10, N'test', CAST(N'2024-01-10' AS Date), N'huynhvany@gmail.com', N'0768920314', N'12 An Duong Vuong 45', CAST(N'2024-10-14T19:18:16.633' AS DateTime))
INSERT [dbo].[Users] ([UserID], [FullName], [BirthDate], [Email], [PhoneNumber], [Address], [CreatedAt]) VALUES (11, N'Staff', CAST(N'2024-10-15' AS Date), N'huynhvany@gmail.com', N'0768920314', N'123 Main St, City, Country', CAST(N'2024-10-14T20:17:35.473' AS DateTime))
INSERT [dbo].[Users] ([UserID], [FullName], [BirthDate], [Email], [PhoneNumber], [Address], [CreatedAt]) VALUES (12, N'Nguyen Thế Huy', CAST(N'2002-12-05' AS Date), N'huy@gmail.com', N'0768920314', N'23/4', CAST(N'2025-02-18T11:52:18.617' AS DateTime))
INSERT [dbo].[Users] ([UserID], [FullName], [BirthDate], [Email], [PhoneNumber], [Address], [CreatedAt]) VALUES (13, N'Nguyen Gia Bao', CAST(N'2025-04-18' AS Date), N'giabao2082005@gmail.com', N'0768920314', N'35/2a Nguyen Trai', CAST(N'2025-04-12T09:01:57.780' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__BookingD__598CF7A3FE4B9A97]    Script Date: 4/24/2025 5:49:06 PM ******/
ALTER TABLE [dbo].[BookingDetails] ADD UNIQUE NONCLUSTERED 
(
	[TicketCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[AgeGroups] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[BookingDetails] ADD  DEFAULT ((1)) FOR [TicketStatus]
GO
ALTER TABLE [dbo].[Bookings] ADD  DEFAULT (NULL) FOR [UserID]
GO
ALTER TABLE [dbo].[Bookings] ADD  DEFAULT ((0)) FOR [PaymentStatus]
GO
ALTER TABLE [dbo].[Buses] ADD  DEFAULT ((0)) FOR [AirConditioned]
GO
ALTER TABLE [dbo].[Buses] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Buses_Seats] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[BusesTrips] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[BusTypes] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Levels] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Locations] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Policies] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Trips] ADD  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD FOREIGN KEY([AccountID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD FOREIGN KEY([LevelID])
REFERENCES [dbo].[Levels] ([LevelID])
GO
ALTER TABLE [dbo].[BookingDetails]  WITH CHECK ADD FOREIGN KEY([AgeGroupID])
REFERENCES [dbo].[AgeGroups] ([AgeGroupID])
GO
ALTER TABLE [dbo].[BookingDetails]  WITH CHECK ADD FOREIGN KEY([BookingID])
REFERENCES [dbo].[Bookings] ([BookingID])
GO
ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD FOREIGN KEY([BusTripID])
REFERENCES [dbo].[BusesTrips] ([BusTripID])
GO
ALTER TABLE [dbo].[Buses]  WITH CHECK ADD FOREIGN KEY([BusTypeID])
REFERENCES [dbo].[BusTypes] ([BusTypeID])
GO
ALTER TABLE [dbo].[Buses]  WITH CHECK ADD  CONSTRAINT [FK_Buses_Locations] FOREIGN KEY([LocationID])
REFERENCES [dbo].[Locations] ([LocationID])
GO
ALTER TABLE [dbo].[Buses] CHECK CONSTRAINT [FK_Buses_Locations]
GO
ALTER TABLE [dbo].[Buses_Seats]  WITH CHECK ADD FOREIGN KEY([BusID])
REFERENCES [dbo].[Buses] ([BusID])
GO
ALTER TABLE [dbo].[BusesTrips]  WITH CHECK ADD FOREIGN KEY([BusID])
REFERENCES [dbo].[Buses] ([BusID])
GO
ALTER TABLE [dbo].[BusesTrips]  WITH CHECK ADD FOREIGN KEY([TripID])
REFERENCES [dbo].[Trips] ([TripID])
GO
ALTER TABLE [dbo].[CustomerFeedback]  WITH CHECK ADD FOREIGN KEY([BusTripID])
REFERENCES [dbo].[BusesTrips] ([BusTripID])
GO
ALTER TABLE [dbo].[CustomerFeedback]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD FOREIGN KEY([BookingID])
REFERENCES [dbo].[Bookings] ([BookingID])
GO
ALTER TABLE [dbo].[Trips]  WITH CHECK ADD FOREIGN KEY([ArrivalLocationID])
REFERENCES [dbo].[Locations] ([LocationID])
GO
ALTER TABLE [dbo].[Trips]  WITH CHECK ADD FOREIGN KEY([DepartureLocationID])
REFERENCES [dbo].[Locations] ([LocationID])
GO
USE [master]
GO
ALTER DATABASE [Bus_Ticket] SET  READ_WRITE 
GO
