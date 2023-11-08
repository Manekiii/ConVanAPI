using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CVAPI.Models;

public partial class ConvandbContext : DbContext
{
    public ConvandbContext()
    {
    }

    public ConvandbContext(DbContextOptions<ConvandbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<ContainerVan> ContainerVans { get; set; }

    public virtual DbSet<CvChecklist> CvChecklists { get; set; }

    public virtual DbSet<CvColorCoding> CvColorCodings { get; set; }

    public virtual DbSet<CvStatus> CvStatuses { get; set; }

    public virtual DbSet<Dispatch> Dispatches { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<ShippingLine> ShippingLines { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<Truck> Trucks { get; set; }

    public virtual DbSet<Trucker> Truckers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBranch> UserBranches { get; set; }

    public virtual DbSet<UserMenu> UserMenus { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<VanInspectionChecklist> VanInspectionChecklists { get; set; }

    public virtual DbSet<VanInspectionReport> VanInspectionReports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=horreum.ccf9t0eeel7h.ap-southeast-1.rds.amazonaws.com;Database=CONVANDB;Username=convanuser;Password=convanpassword");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Branch_pkey");

            entity.ToTable("Branch");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.Alias).HasMaxLength(50);
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Status).HasDefaultValueSql("1");
        });

        modelBuilder.Entity<ContainerVan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("containervan_pkey");

            entity.ToTable("ContainerVan");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('containervan_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Agefrompulloutdate).HasColumnName("agefrompulloutdate");
            entity.Property(e => e.Createdbyuserid).HasColumnName("createdbyuserid");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Cvstatusid).HasColumnName("cvstatusid");
            entity.Property(e => e.Datearrive)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datearrive");
            entity.Property(e => e.Detentioncharge)
                .HasPrecision(10, 2)
                .HasColumnName("detentioncharge");
            entity.Property(e => e.Detentionfee)
                .HasPrecision(10, 2)
                .HasColumnName("detentionfee");
            entity.Property(e => e.Detentiontrigger)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("detentiontrigger");
            entity.Property(e => e.Dispatcheddateofvaninspector)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dispatcheddateofvaninspector");
            entity.Property(e => e.Dispatchedemptycvan)
                .HasMaxLength(100)
                .HasColumnName("dispatchedemptycvan");
            entity.Property(e => e.Dispatchedweeknumber).HasColumnName("dispatchedweeknumber");
            entity.Property(e => e.Eirnumber)
                .HasMaxLength(20)
                .HasColumnName("eirnumber");
            entity.Property(e => e.Freedays).HasColumnName("freedays");
            entity.Property(e => e.Hasdetention)
                .HasDefaultValueSql("0")
                .HasColumnName("hasdetention");
            entity.Property(e => e.Isdispatch)
                .HasDefaultValueSql("0")
                .HasColumnName("isdispatch");
            entity.Property(e => e.Modifiedbyuserid).HasColumnName("modifiedbyuserid");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Neardetention)
                .HasDefaultValueSql("0")
                .HasColumnName("neardetention");
            entity.Property(e => e.Noofdayswithdetention).HasColumnName("noofdayswithdetention");
            entity.Property(e => e.Procuredemptycvan)
                .HasMaxLength(100)
                .HasColumnName("procuredemptycvan");
            entity.Property(e => e.Pulloutdatefrompier)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("pulloutdatefrompier");
            entity.Property(e => e.Pulloutweeknumber).HasColumnName("pulloutweeknumber");
            entity.Property(e => e.Remarks)
                .HasMaxLength(100)
                .HasColumnName("remarks");
            entity.Property(e => e.Returndatetopier)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("returndatetopier");
            entity.Property(e => e.Shippinglineid).HasColumnName("shippinglineid");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Vannumber)
                .HasMaxLength(50)
                .HasColumnName("vannumber");

            entity.HasOne(d => d.Cvstatus).WithMany(p => p.ContainerVans)
                .HasForeignKey(d => d.Cvstatusid)
                .HasConstraintName("ContainerVan_cvstatusid_fkey");

            entity.HasOne(d => d.Shippingline).WithMany(p => p.ContainerVans)
                .HasForeignKey(d => d.Shippinglineid)
                .HasConstraintName("ContainerVan_shippinglineid_fkey1");
        });

        modelBuilder.Entity<CvChecklist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cv_Checklist_pkey");

            entity.ToTable("cv_Checklist");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");
            entity.Property(e => e.Description).HasMaxLength(50);
        });

        modelBuilder.Entity<CvColorCoding>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ContainerVanColorCoding_pkey");

            entity.ToTable("cv_ColorCoding");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.ColorName).HasMaxLength(10);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<CvStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("containervanstatus_pkey");

            entity.ToTable("cv_Status");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('containervanstatus_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Createdbyuserid).HasColumnName("createdbyuserid");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.Isforempty).HasColumnName("isforempty");
            entity.Property(e => e.Modifiedbyuserid).HasColumnName("modifiedbyuserid");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Statusname)
                .HasMaxLength(50)
                .HasColumnName("statusname");
        });

        modelBuilder.Entity<Dispatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Dispatch_pkey");

            entity.ToTable("Dispatch");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Destination).HasMaxLength(150);
            entity.Property(e => e.Driver).HasMaxLength(100);
            entity.Property(e => e.EmptyLoad).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Plate).HasMaxLength(20);
            entity.Property(e => e.Remarks).HasMaxLength(100);
            entity.Property(e => e.TruckersName).HasMaxLength(150);

            entity.HasOne(d => d.ContainerVan).WithMany(p => p.Dispatches)
                .HasForeignKey(d => d.ContainerVanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Dispatch_ContainerVanId_fkey");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("driver_pkey");

            entity.ToTable("Driver");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contactno)
                .HasMaxLength(13)
                .HasColumnName("contactno");
            entity.Property(e => e.Createdbyuserid).HasColumnName("createdbyuserid");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Emailadd)
                .HasMaxLength(100)
                .HasColumnName("emailadd");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("firstname");
            entity.Property(e => e.Imgfilename)
                .HasMaxLength(30)
                .HasColumnName("imgfilename");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("lastname");
            entity.Property(e => e.Licenseno)
                .HasMaxLength(20)
                .HasColumnName("licenseno");
            entity.Property(e => e.Middlename)
                .HasMaxLength(50)
                .HasColumnName("middlename");
            entity.Property(e => e.Modifiedbyuserid).HasColumnName("modifiedbyuserid");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .HasColumnName("nickname");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("1")
                .HasColumnName("status");
            entity.Property(e => e.Truckercode)
                .HasMaxLength(20)
                .HasColumnName("truckercode");

            entity.HasOne(d => d.TruckercodeNavigation).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.Truckercode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("driver_truckercode_fkey");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Menu_pkey");

            entity.ToTable("Menu");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.BrowserDefault).HasDefaultValueSql("1");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");
            entity.Property(e => e.Icon).HasMaxLength(50);
            entity.Property(e => e.IsBrowser)
                .HasDefaultValueSql("1")
                .HasColumnName("isBrowser");
            entity.Property(e => e.IsMobile).HasDefaultValueSql("1");
            entity.Property(e => e.IsTransaction).HasDefaultValueSql("1");
            entity.Property(e => e.MenuName).HasMaxLength(50);
            entity.Property(e => e.MobileDefault).HasDefaultValueSql("1");
            entity.Property(e => e.Status).HasDefaultValueSql("1");
        });

        modelBuilder.Entity<ShippingLine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shippingline_pkey");

            entity.ToTable("ShippingLine");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"shippingline_Id_seq\"'::regclass)");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("Token_pkey");

            entity.ToTable("Token");

            entity.Property(e => e.TokenId).UseIdentityAlwaysColumn();
            entity.Property(e => e.AuthToken).HasMaxLength(250);
            entity.Property(e => e.ExpiresOn).HasColumnType("timestamp without time zone");
            entity.Property(e => e.IssuedOn).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<Truck>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("truck_pkey");

            entity.ToTable("Truck");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.PlateNo).HasMaxLength(10);
            entity.Property(e => e.Status).HasDefaultValueSql("1");
            entity.Property(e => e.TruckerId).HasMaxLength(50);
            entity.Property(e => e.VehicleType).HasMaxLength(20);

            entity.HasOne(d => d.Trucker).WithMany(p => p.Trucks)
                .HasForeignKey(d => d.TruckerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Truck_truckerid_fkey");
        });

        modelBuilder.Entity<Trucker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trucker_pkey");

            entity.ToTable("Trucker");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Branch)
                .HasMaxLength(50)
                .HasColumnName("branch");
            entity.Property(e => e.Celphoneno)
                .HasMaxLength(13)
                .HasColumnName("celphoneno");
            entity.Property(e => e.Contactperson)
                .HasMaxLength(50)
                .HasColumnName("contactperson");
            entity.Property(e => e.Createdbyuserid).HasColumnName("createdbyuserid");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Emailadd)
                .HasMaxLength(100)
                .HasColumnName("emailadd");
            entity.Property(e => e.Imgfilename)
                .HasMaxLength(255)
                .HasColumnName("imgfilename");
            entity.Property(e => e.Modifiedbyuserid).HasColumnName("modifiedbyuserid");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Ownername)
                .HasMaxLength(100)
                .HasColumnName("ownername");
            entity.Property(e => e.Ownertype)
                .HasMaxLength(10)
                .HasColumnName("ownertype");
            entity.Property(e => e.Remarks).HasColumnName("remarks");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("1")
                .HasColumnName("status");
            entity.Property(e => e.Telephoneno)
                .HasMaxLength(13)
                .HasColumnName("telephoneno");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("User");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('users_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Contactnumber)
                .HasMaxLength(13)
                .HasColumnName("contactnumber");
            entity.Property(e => e.Createdbyuserid).HasColumnName("createdbyuserid");
            entity.Property(e => e.Createddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Isdeactivated)
                .HasDefaultValueSql("0")
                .HasColumnName("isdeactivated");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Middlename)
                .HasMaxLength(100)
                .HasColumnName("middlename");
            entity.Property(e => e.Modifiedbyuserid).HasColumnName("modifiedbyuserid");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .HasColumnName("nickname");
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .HasColumnName("password");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("1")
                .HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
            entity.Property(e => e.Userroleid).HasColumnName("userroleid");

            entity.HasOne(d => d.Userrole).WithMany(p => p.Users)
                .HasForeignKey(d => d.Userroleid)
                .HasConstraintName("users_userroleid_fkey");
        });

        modelBuilder.Entity<UserBranch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserBranch_pkey");

            entity.ToTable("UserBranch");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");
            entity.Property(e => e.Status).HasDefaultValueSql("1");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Branch).WithMany(p => p.UserBranches)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserBranch_BranchId_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserBranches)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserBranch_UserID_fkey");
        });

        modelBuilder.Entity<UserMenu>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.MenuId });
            entity.ToTable("UserMenu");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("now()");

            entity.HasOne(d => d.Menu).WithMany()
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserMenu_MenuId_fkey");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserMenu_UserId_fkey");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserRole_pkey");

            entity.ToTable("UserRole");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.ModifiedDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<VanInspectionChecklist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("VanInspectionChecklist_pkey");

            entity.ToTable("VanInspectionChecklist");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Remarks).HasMaxLength(100);
            entity.Property(e => e.Virid).HasColumnName("VIRId");

            entity.HasOne(d => d.Vir).WithMany(p => p.VanInspectionChecklists)
                .HasForeignKey(d => d.Virid)
                .HasConstraintName("VanInspectionChecklist_VIRId_fkey");
        });

        modelBuilder.Entity<VanInspectionReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vaninspectionreport_pkey");

            entity.ToTable("VanInspectionReport");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('vaninspectionreport_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Colorcodingid).HasColumnName("colorcodingid");
            entity.Property(e => e.Confirmedbyuserid).HasColumnName("confirmedbyuserid");
            entity.Property(e => e.Confirmeddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("confirmeddate");
            entity.Property(e => e.Containervanid).HasColumnName("containervanid");
            entity.Property(e => e.Createdbyuserid).HasColumnName("createdbyuserid");
            entity.Property(e => e.Createddate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createddate");
            entity.Property(e => e.Customer)
                .HasMaxLength(100)
                .HasColumnName("customer");
            entity.Property(e => e.Driver)
                .HasMaxLength(100)
                .HasColumnName("driver");
            entity.Property(e => e.Finaldate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("finaldate");
            entity.Property(e => e.Finaldriver)
                .HasMaxLength(100)
                .HasColumnName("finaldriver");
            entity.Property(e => e.Finalplatenumber)
                .HasMaxLength(15)
                .HasColumnName("finalplatenumber");
            entity.Property(e => e.Finalremarks)
                .HasMaxLength(100)
                .HasColumnName("finalremarks");
            entity.Property(e => e.Finaltruckersname)
                .HasMaxLength(150)
                .HasColumnName("finaltruckersname");
            entity.Property(e => e.Hasfinal)
                .HasDefaultValueSql("0")
                .HasColumnName("hasfinal");
            entity.Property(e => e.Hasinitial)
                .HasDefaultValueSql("0")
                .HasColumnName("hasinitial");
            entity.Property(e => e.Initialdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("initialdate");
            entity.Property(e => e.Initialinspectiondate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("initialinspectiondate");
            entity.Property(e => e.Initialinspectoruserid).HasColumnName("initialinspectoruserid");
            entity.Property(e => e.Initialremarks)
                .HasMaxLength(100)
                .HasColumnName("initialremarks");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Modifiedbyuserid).HasColumnName("modifiedbyuserid");
            entity.Property(e => e.Modifieddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifieddate");
            entity.Property(e => e.Platenumber)
                .HasMaxLength(15)
                .HasColumnName("platenumber");
            entity.Property(e => e.Shipper)
                .HasMaxLength(150)
                .HasColumnName("shipper");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Statusid).HasColumnName("statusid");
            entity.Property(e => e.Truckersname)
                .HasMaxLength(150)
                .HasColumnName("truckersname");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
            entity.Property(e => e.Virno)
                .HasMaxLength(20)
                .HasColumnName("virno");

            entity.HasOne(d => d.Colorcoding).WithMany(p => p.VanInspectionReports)
                .HasForeignKey(d => d.Colorcodingid)
                .HasConstraintName("VanInspectionReport_colorcodingid_fkey");

            entity.HasOne(d => d.Containervan).WithMany(p => p.VanInspectionReports)
                .HasForeignKey(d => d.Containervanid)
                .HasConstraintName("VanInspectionReport_containervanid_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.VanInspectionReports)
                .HasForeignKey(d => d.Statusid)
                .HasConstraintName("VanInspectionReport_statusid_fkey");
        });
        modelBuilder.HasSequence("trk_sequence");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
