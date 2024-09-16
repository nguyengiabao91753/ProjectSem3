using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectSem3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeGroups",
                columns: table => new
                {
                    AgeGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Discount = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AgeGroup__5B9B0B756531FE14", x => x.AgeGroupID);
                });

            migrationBuilder.CreateTable(
                name: "BusTypes",
                columns: table => new
                {
                    BusTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BusTypes__84A10CC8B044D471", x => x.BusTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    LevelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Levels__09F03C0632759436", x => x.LevelID);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Location__E7FEA47732F53FCB", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    PolicyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Policies__2E133944A05EB817", x => x.PolicyID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCACA57E2758", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    BusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusTypeID = table.Column<int>(type: "int", nullable: true),
                    AirConditioned = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0),
                    LicensePlate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SeatCount = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Buses__6A0F60955A389DB8", x => x.BusID);
                    table.ForeignKey(
                        name: "FK__Buses__BusTypeID__403A8C7D",
                        column: x => x.BusTypeID,
                        principalTable: "BusTypes",
                        principalColumn: "BusTypeID");
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureLocationID = table.Column<int>(type: "int", nullable: true),
                    ArrivalLocationID = table.Column<int>(type: "int", nullable: true),
                    DateStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Trips__51DC711EFB36F7D2", x => x.TripID);
                    table.ForeignKey(
                        name: "FK__Trips__ArrivalLo__4BAC3F29",
                        column: x => x.ArrivalLocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID");
                    table.ForeignKey(
                        name: "FK__Trips__Departure__4AB81AF0",
                        column: x => x.DepartureLocationID,
                        principalTable: "Locations",
                        principalColumn: "LocationID");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1),
                    LevelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Accounts__349DA586459CDD34", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK__Accounts__Accoun__5629CD9C",
                        column: x => x.AccountID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__Accounts__LevelI__5812160E",
                        column: x => x.LevelID,
                        principalTable: "Levels",
                        principalColumn: "LevelID");
                });

            migrationBuilder.CreateTable(
                name: "Buses_Seats",
                columns: table => new
                {
                    SeatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Buses_Se__311713D3E2343E89", x => x.SeatID);
                    table.ForeignKey(
                        name: "FK__Buses_Sea__BusID__44FF419A",
                        column: x => x.BusID,
                        principalTable: "Buses",
                        principalColumn: "BusID");
                });

            migrationBuilder.CreateTable(
                name: "BusesTrips",
                columns: table => new
                {
                    BusTripID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusID = table.Column<int>(type: "int", nullable: true),
                    TripID = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BusesTri__14ADEEEDBEDFC35F", x => x.BusTripID);
                    table.ForeignKey(
                        name: "FK__BusesTrip__BusID__4F7CD00D",
                        column: x => x.BusID,
                        principalTable: "Buses",
                        principalColumn: "BusID");
                    table.ForeignKey(
                        name: "FK__BusesTrip__TripI__5070F446",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "TripID");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true, defaultValueSql: "(NULL)"),
                    BusTripID = table.Column<int>(type: "int", nullable: true),
                    SeatID = table.Column<int>(type: "int", nullable: true),
                    AgeGroupID = table.Column<int>(type: "int", nullable: true),
                    Distance = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PriceAfterDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TicketCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TicketStatus = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)1),
                    PaymentStatus = table.Column<byte>(type: "tinyint", nullable: true, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bookings__73951ACDE13B6702", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK__Bookings__AgeGro__60A75C0F",
                        column: x => x.AgeGroupID,
                        principalTable: "AgeGroups",
                        principalColumn: "AgeGroupID");
                    table.ForeignKey(
                        name: "FK__Bookings__BusTri__5FB337D6",
                        column: x => x.BusTripID,
                        principalTable: "BusesTrips",
                        principalColumn: "BusTripID");
                });

            migrationBuilder.CreateTable(
                name: "CustomerFeedback",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    BusTripID = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    FeedbackDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__6A4BEDF632949F12", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK__CustomerF__BusTr__693CA210",
                        column: x => x.BusTripID,
                        principalTable: "BusesTrips",
                        principalColumn: "BusTripID");
                    table.ForeignKey(
                        name: "FK__CustomerF__UserI__68487DD7",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingID = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payments__9B556A5852CF210A", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK__Payments__Bookin__656C112C",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "BookingID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LevelID",
                table: "Accounts",
                column: "LevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AgeGroupID",
                table: "Bookings",
                column: "AgeGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BusTripID",
                table: "Bookings",
                column: "BusTripID");

            migrationBuilder.CreateIndex(
                name: "UQ__Bookings__598CF7A383E52A74",
                table: "Bookings",
                column: "TicketCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buses_BusTypeID",
                table: "Buses",
                column: "BusTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_Seats_BusID",
                table: "Buses_Seats",
                column: "BusID");

            migrationBuilder.CreateIndex(
                name: "IX_BusesTrips_BusID",
                table: "BusesTrips",
                column: "BusID");

            migrationBuilder.CreateIndex(
                name: "IX_BusesTrips_TripID",
                table: "BusesTrips",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedback_BusTripID",
                table: "CustomerFeedback",
                column: "BusTripID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedback_UserID",
                table: "CustomerFeedback",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingID",
                table: "Payments",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ArrivalLocationID",
                table: "Trips",
                column: "ArrivalLocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DepartureLocationID",
                table: "Trips",
                column: "DepartureLocationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Buses_Seats");

            migrationBuilder.DropTable(
                name: "CustomerFeedback");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "AgeGroups");

            migrationBuilder.DropTable(
                name: "BusesTrips");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "BusTypes");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
