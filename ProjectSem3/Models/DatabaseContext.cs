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

    public virtual DbSet<BookingDetail> BookingDetails { get; set; }

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
        => optionsBuilder.UseSqlServer("Server=Eragon\\SQLEXPRESS;Database=Bus_Ticket;user id=hai;password=123;trusted_connection=true;encrypt=false");

#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Accounts__349DA586AE10859D");
            entity.HasKey(e => e.AccountId).HasName("PK__Accounts__349DA5861664C857");
            entity.HasKey(e => e.AccountId).HasName("PK__Accounts__349DA5868EDAA38E");


            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.LevelId).HasColumnName("LevelID");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.AccountNavigation).WithOne(p => p.Account)
                .HasForeignKey<Account>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Accounts__Accoun__571DF1D5");

            entity.HasOne(d => d.Level).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.LevelId)
                .HasConstraintName("FK__Accounts__LevelI__59063A47");
        });

        modelBuilder.Entity<AgeGroup>(entity =>
        {
            entity.HasKey(e => e.AgeGroupId).HasName("PK__AgeGroup__5B9B0B75487F19D6");
            entity.HasKey(e => e.AgeGroupId).HasName("PK__AgeGroup__5B9B0B75335DED11");

            entity.HasKey(e => e.AgeGroupId).HasName("PK__AgeGroup__5B9B0B751A0D376A");

            entity.Property(e => e.AgeGroupId).HasColumnName("AgeGroupID");
            entity.Property(e => e.Discount)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<Booking>(entity =>
        {

            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951ACD8AF7FF5A");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.BusTripId).HasColumnName("BusTripID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PaymentStatus).HasDefaultValue((byte)0);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("UserID");

            entity.HasOne(d => d.BusTrip).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.BusTripId)
                .HasConstraintName("FK__Bookings__BusTri__5FB337D6");
        });

        modelBuilder.Entity<BookingDetail>(entity =>
        {
            entity.HasKey(e => e.BookingDetailId).HasName("PK__BookingD__8136D47AD950A7D5");

            entity.HasIndex(e => e.TicketCode, "UQ__BookingD__598CF7A39CA8CD8D").IsUnique();

            entity.Property(e => e.BookingDetailId).HasColumnName("BookingDetailID");
            entity.Property(e => e.AgeGroupId).HasColumnName("AgeGroupID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.PriceAfterDiscount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SeatId).HasColumnName("SeatID");
            entity.Property(e => e.SeatName).HasMaxLength(20);
            entity.Property(e => e.TicketCode).HasMaxLength(255);
            entity.Property(e => e.TicketStatus).HasDefaultValue((byte)1);

            entity.HasOne(d => d.AgeGroup).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.AgeGroupId)
                .HasConstraintName("FK__BookingDe__AgeGr__656C112C");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__BookingDe__Booki__6477ECF3");
        });

        modelBuilder.Entity<Bus>(entity =>
        {
            entity.HasKey(e => e.BusId).HasName("PK__Buses__6A0F6095D39F8C9E");
            entity.HasKey(e => e.BusId).HasName("PK__Buses__6A0F6095EA63B6EC");

            entity.HasKey(e => e.BusId).HasName("PK__Buses__6A0F6095E9634EE3");

            entity.Property(e => e.BusId).HasColumnName("BusID");
            entity.Property(e => e.AirConditioned).HasDefaultValue((byte)0);
            entity.Property(e => e.BasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BusTypeId).HasColumnName("BusTypeID");
            entity.Property(e => e.LicensePlate).HasMaxLength(20);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.BusType).WithMany(p => p.Buses)
                .HasForeignKey(d => d.BusTypeId)
                .HasConstraintName("FK__Buses__BusTypeID__403A8C7D");
        });

        modelBuilder.Entity<BusType>(entity =>
        {
            entity.HasKey(e => e.BusTypeId).HasName("PK__BusTypes__84A10CC812177BC9");
            entity.HasKey(e => e.BusTypeId).HasName("PK__BusTypes__84A10CC881B7A277");

            entity.HasKey(e => e.BusTypeId).HasName("PK__BusTypes__84A10CC89659EC58");

            entity.Property(e => e.BusTypeId).HasColumnName("BusTypeID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<BusesSeat>(entity =>
        {
            entity.HasKey(e => e.SeatId).HasName("PK__Buses_Se__311713D3F28446BF");
            entity.HasKey(e => e.SeatId).HasName("PK__Buses_Se__311713D3D3F85F6F");
            entity.HasKey(e => e.SeatId).HasName("PK__Buses_Se__311713D3C9DFC1E7");

            entity.ToTable("Buses_Seats");

            entity.Property(e => e.SeatId).HasColumnName("SeatID");
            entity.Property(e => e.BusId).HasColumnName("BusID");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.Bus).WithMany(p => p.BusesSeats)
                .HasForeignKey(d => d.BusId)
                .HasConstraintName("FK__Buses_Sea__BusID__44FF419A");
        });

        modelBuilder.Entity<BusesTrip>(entity =>
        {
            entity.HasKey(e => e.BusTripId).HasName("PK__BusesTri__14ADEEED91B91E5F");
            entity.HasKey(e => e.BusTripId).HasName("PK__BusesTri__14ADEEEDC258F61D");
            entity.HasKey(e => e.BusTripId).HasName("PK__BusesTri__14ADEEEDA77A18C0");

            entity.Property(e => e.BusTripId).HasColumnName("BusTripID");
            entity.Property(e => e.BusId).HasColumnName("BusID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.TripId).HasColumnName("TripID");

            entity.HasOne(d => d.Bus).WithMany(p => p.BusesTrips)
                .HasForeignKey(d => d.BusId)
                .HasConstraintName("FK__BusesTrip__BusID__5070F446");

            entity.HasOne(d => d.Trip).WithMany(p => p.BusesTrips)
                .HasForeignKey(d => d.TripId)
                .HasConstraintName("FK__BusesTrip__TripI__5165187F");
        });

        modelBuilder.Entity<CustomerFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Customer__6A4BEDF6D015C815");
            entity.HasKey(e => e.FeedbackId).HasName("PK__Customer__6A4BEDF6145C34BD");

            entity.HasKey(e => e.FeedbackId).HasName("PK__Customer__6A4BEDF6E3F0D4C6");

            entity.ToTable("CustomerFeedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.BusTripId).HasColumnName("BusTripID");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.FeedbackDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.BusTrip).WithMany(p => p.CustomerFeedbacks)
                .HasForeignKey(d => d.BusTripId)
                .HasConstraintName("FK__CustomerF__BusTr__6D0D32F4");

            entity.HasOne(d => d.User).WithMany(p => p.CustomerFeedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__CustomerF__UserI__6C190EBB");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.LevelId).HasName("PK__Levels__09F03C06D66A2E17");

            entity.HasKey(e => e.LevelId).HasName("PK__Levels__09F03C0653D98E81");

            entity.HasKey(e => e.LevelId).HasName("PK__Levels__09F03C06C9CC97D0");

            entity.Property(e => e.LevelId).HasColumnName("LevelID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA477A980D6F8");
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA477ED535BCD");

            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA477CC92E96C");

            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A588EF2614D");
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A5891004621");
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A5865DAB4E7");


            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__Payments__Bookin__693CA210");
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(e => e.PolicyId).HasName("PK__Policies__2E1339449303642F");
            entity.HasKey(e => e.PolicyId).HasName("PK__Policies__2E133944CCB3983A");

            entity.HasKey(e => e.PolicyId).HasName("PK__Policies__2E133944D44A48AF");

            entity.Property(e => e.PolicyId).HasColumnName("PolicyID");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.TripId).HasName("PK__Trips__51DC711E471D9D0A");
            entity.HasKey(e => e.TripId).HasName("PK__Trips__51DC711E4B66A50B");
            entity.HasKey(e => e.TripId).HasName("PK__Trips__51DC711EEF833D8D");

            entity.Property(e => e.TripId).HasColumnName("TripID");
            entity.Property(e => e.ArrivalLocationId).HasColumnName("ArrivalLocationID");
            entity.Property(e => e.DateEnd).HasColumnType("datetime");
            entity.Property(e => e.DateStart).HasColumnType("datetime");
            entity.Property(e => e.DepartureLocationId).HasColumnName("DepartureLocationID");
            entity.Property(e => e.Status).HasDefaultValue((byte)1);

            entity.HasOne(d => d.ArrivalLocation).WithMany(p => p.TripArrivalLocations)
                .HasForeignKey(d => d.ArrivalLocationId)
                .HasConstraintName("FK__Trips__ArrivalLo__4CA06362");

            entity.HasOne(d => d.DepartureLocation).WithMany(p => p.TripDepartureLocations)
                .HasForeignKey(d => d.DepartureLocationId)
                .HasConstraintName("FK__Trips__Departure__4BAC3F29");
        });

        modelBuilder.Entity<User>(entity =>
        {

            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACB2BD3965");
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACFEE889C1");

            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC70223897");


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
