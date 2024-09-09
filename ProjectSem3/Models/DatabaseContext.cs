using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectSem3.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AgeGroup> AgeGroups { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Bus> Buses { get; set; }

    public virtual DbSet<BusType> BusTypes { get; set; }

    public virtual DbSet<BusesSeat> BusesSeats { get; set; }

    public virtual DbSet<BusesTrip> BusesTrips { get; set; }

    public virtual DbSet<CustomerFeedback> CustomerFeedbacks { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Policy> Policies { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI;Database=Bus_Ticket;user id=sa;password=1234567890;trusted_connection=true;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Accounts__349DA5861CE1BAA2");

            entity.Property(e => e.AccountId)
                .ValueGeneratedOnAdd()
                .HasColumnName("AccountID");
            entity.Property(e => e.LevelId).HasColumnName("LevelID");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.AccountNavigation).WithOne(p => p.Account)
                .HasForeignKey<Account>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Accounts__Accoun__5441852A");

            entity.HasOne(d => d.Level).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.LevelId)
                .HasConstraintName("FK__Accounts__LevelI__5629CD9C");
        });

        modelBuilder.Entity<AgeGroup>(entity =>
        {
            entity.HasKey(e => e.AgeGroupId).HasName("PK__AgeGroup__5B9B0B7584460283");

            entity.Property(e => e.AgeGroupId).HasColumnName("AgeGroupID");
            entity.Property(e => e.Discount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951ACD56846447");

            entity.HasIndex(e => e.TicketCode, "UQ__Bookings__598CF7A3E4A4CA7B").IsUnique();

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.AgeGroupId).HasColumnName("AgeGroupID");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.BusTripId).HasColumnName("BusTripID");
            entity.Property(e => e.Distance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PaymentStatus).HasDefaultValue((byte)0);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.PriceAfterDiscount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SeatId).HasColumnName("SeatID");
            entity.Property(e => e.TicketCode).HasMaxLength(255);
            entity.Property(e => e.TicketStatus).HasDefaultValue((byte)1);
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("UserID");

            entity.HasOne(d => d.AgeGroup).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.AgeGroupId)
                .HasConstraintName("FK__Bookings__AgeGro__5DCAEF64");

            entity.HasOne(d => d.BusTrip).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BusTripId)
                .HasConstraintName("FK__Bookings__BusTri__5CD6CB2B");
        });

        modelBuilder.Entity<Bus>(entity =>
        {
            entity.HasKey(e => e.BusId).HasName("PK__Buses__6A0F609543E7C32B");

            entity.Property(e => e.BusId).HasColumnName("BusID");
            entity.Property(e => e.AirConditioned).HasDefaultValue((byte)0);
            entity.Property(e => e.BasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BusTypeId).HasColumnName("BusTypeID");
            entity.Property(e => e.LicensePlate).HasMaxLength(20);

            entity.HasOne(d => d.BusType).WithMany(p => p.Buses)
                .HasForeignKey(d => d.BusTypeId)
                .HasConstraintName("FK__Buses__BusTypeID__3F466844");
        });

        modelBuilder.Entity<BusType>(entity =>
        {
            entity.HasKey(e => e.BusTypeId).HasName("PK__BusTypes__84A10CC8A373D606");

            entity.Property(e => e.BusTypeId).HasColumnName("BusTypeID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<BusesSeat>(entity =>
        {
            entity.HasKey(e => e.SeatId).HasName("PK__Buses_Se__311713D32E5F960B");

            entity.ToTable("Buses_Seats");

            entity.Property(e => e.SeatId).HasColumnName("SeatID");
            entity.Property(e => e.BusId).HasColumnName("BusID");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Bus).WithMany(p => p.BusesSeats)
                .HasForeignKey(d => d.BusId)
                .HasConstraintName("FK__Buses_Sea__BusID__4316F928");
        });

        modelBuilder.Entity<BusesTrip>(entity =>
        {
            entity.HasKey(e => e.BusTripId).HasName("PK__BusesTri__14ADEEEDBFBB6A4B");

            entity.Property(e => e.BusTripId).HasColumnName("BusTripID");
            entity.Property(e => e.BusId).HasColumnName("BusID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.TripId).HasColumnName("TripID");

            entity.HasOne(d => d.Bus).WithMany(p => p.BusesTrips)
                .HasForeignKey(d => d.BusId)
                .HasConstraintName("FK__BusesTrip__BusID__4D94879B");

            entity.HasOne(d => d.Trip).WithMany(p => p.BusesTrips)
                .HasForeignKey(d => d.TripId)
                .HasConstraintName("FK__BusesTrip__TripI__4E88ABD4");
        });

        modelBuilder.Entity<CustomerFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Customer__6A4BEDF610EC8DD6");

            entity.ToTable("CustomerFeedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.BusTripId).HasColumnName("BusTripID");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.FeedbackDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.BusTrip).WithMany(p => p.CustomerFeedbacks)
                .HasForeignKey(d => d.BusTripId)
                .HasConstraintName("FK__CustomerF__BusTr__66603565");

            entity.HasOne(d => d.User).WithMany(p => p.CustomerFeedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__CustomerF__UserI__656C112C");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.LevelId).HasName("PK__Levels__09F03C064FBBF99E");

            entity.Property(e => e.LevelId).HasColumnName("LevelID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA47716057638");

            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58771B5AED");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Payments__Bookin__628FA481");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__Policies__2E1339447C954817");

            entity.Property(e => e.PolicyId).HasColumnName("PolicyID");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.TripId).HasName("PK__Trips__51DC711EF4F8D6E4");

            entity.Property(e => e.TripId).HasColumnName("TripID");
            entity.Property(e => e.ArrivalLocationId).HasColumnName("ArrivalLocationID");
            entity.Property(e => e.DateEnd).HasColumnType("datetime");
            entity.Property(e => e.DateStart).HasColumnType("datetime");
            entity.Property(e => e.DepartureLocationId).HasColumnName("DepartureLocationID");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.ArrivalLocation).WithMany(p => p.TripArrivalLocations)
                .HasForeignKey(d => d.ArrivalLocationId)
                .HasConstraintName("FK__Trips__ArrivalLo__49C3F6B7");

            entity.HasOne(d => d.DepartureLocation).WithMany(p => p.TripDepartureLocations)
                .HasForeignKey(d => d.DepartureLocationId)
                .HasConstraintName("FK__Trips__Departure__48CFD27E");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC40964D77");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
