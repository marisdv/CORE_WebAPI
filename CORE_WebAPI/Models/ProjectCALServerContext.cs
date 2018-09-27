using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CORE_WebAPI.Models
{
    public partial class ProjectCALServerContext : DbContext
    {
        public ProjectCALServerContext()
        {
        }

        public ProjectCALServerContext(DbContextOptions<ProjectCALServerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessArea> AccessArea { get; set; }
        public virtual DbSet<AccessRole> AccessRole { get; set; }
        public virtual DbSet<AccessRoleArea> AccessRoleArea { get; set; }
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<ApplicationStatus> ApplicationStatus { get; set; }
        public virtual DbSet<AuditLog> AuditLog { get; set; }
        public virtual DbSet<AuditType> AuditType { get; set; }
        public virtual DbSet<BasketLine> BasketLine { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<DownloadLocation> DownloadLocation { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<FixedPrice> FixedPrice { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<PackageContent> PackageContent { get; set; }
        public virtual DbSet<PackageType> PackageType { get; set; }
        public virtual DbSet<PaymentReference> PaymentReference { get; set; }
        public virtual DbSet<Penalty> Penalty { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Receiver> Receiver { get; set; }
        public virtual DbSet<Sender> Sender { get; set; }
        public virtual DbSet<Shipment> Shipment { get; set; }
        public virtual DbSet<ShipmentAgent> ShipmentAgent { get; set; }
        public virtual DbSet<ShipmentAgentLocation> ShipmentAgentLocation { get; set; }
        public virtual DbSet<ShipmentAgentNotification> ShipmentAgentNotification { get; set; }
        public virtual DbSet<ShipmentStatus> ShipmentStatus { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<VehicleMake> VehicleMake { get; set; }
        public virtual DbSet<VehiclePacakageLine> VehiclePacakageLine { get; set; }
        public virtual DbSet<VehicleStatus> VehicleStatus { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=ProjectCALServer;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessArea>(entity =>
            {
                entity.ToTable("ACCESS_AREA");

                entity.Property(e => e.AccessAreaId).HasColumnName("Access_Area_ID");

                entity.Property(e => e.AccessAreaDescr)
                    .IsRequired()
                    .HasColumnName("Access_Area_Descr")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccessRole>(entity =>
            {
                entity.ToTable("ACCESS_ROLE");

                entity.Property(e => e.AccessRoleId).HasColumnName("Access_Role_ID");

                entity.Property(e => e.AccessRoleDescr)
                    .IsRequired()
                    .HasColumnName("Access_Role_Descr")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AccessRoleName)
                    .IsRequired()
                    .HasColumnName("Access_Role_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccessRoleArea>(entity =>
            {
                entity.HasKey(e => new { e.AccessRoleId, e.AccessAreaId });

                entity.ToTable("ACCESS_ROLE_AREA");

                entity.Property(e => e.AccessRoleId).HasColumnName("Access_Role_ID");

                entity.Property(e => e.AccessAreaId).HasColumnName("Access_Area_ID");

                entity.HasOne(d => d.AccessArea)
                    .WithMany(p => p.AccessRoleArea)
                    .HasForeignKey(d => d.AccessAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ACCESS_ROLE_AREA_ACCESS_AREA");

                entity.HasOne(d => d.AccessRole)
                    .WithMany(p => p.AccessRoleArea)
                    .HasForeignKey(d => d.AccessRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ACCESS_ROLE_AREA_ACCESS_ROLE");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("APPLICATION");

                entity.Property(e => e.ApplicationId).HasColumnName("Application_ID");

                entity.Property(e => e.AgentId).HasColumnName("Agent_ID");

                entity.Property(e => e.ApplicationDate)
                    .HasColumnName("Application_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ApplicationStatusId).HasColumnName("Application_Status_ID");

                entity.Property(e => e.DateAccepted)
                    .HasColumnName("Date_Accepted")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APPLICATION_SHIPMENT_AGENT");

                entity.HasOne(d => d.ApplicationStatus)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.ApplicationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_APPLICATION_APPLICATION_STATUS");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Application)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_APPLICATION_EMPLOYEE");
            });

            modelBuilder.Entity<ApplicationStatus>(entity =>
            {
                entity.ToTable("APPLICATION_STATUS");

                entity.Property(e => e.ApplicationStatusId).HasColumnName("Application_Status_ID");

                entity.Property(e => e.ApplicationStatusDescr)
                    .IsRequired()
                    .HasColumnName("Application_Status_Descr")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(e => e.AuditId);

                entity.ToTable("AUDIT_LOG");

                entity.Property(e => e.AuditId).HasColumnName("Audit_ID");

                entity.Property(e => e.AuditDateTime)
                    .HasColumnName("Audit_DateTime")
                    .HasColumnType("datetime");

                entity.Property(e => e.AuditTypeId).HasColumnName("Audit_Type_ID");

                entity.Property(e => e.AuditUserName)
                    .HasColumnName("Audit_User_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ItemAffected)
                    .IsRequired()
                    .HasColumnName("Item_Affected")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TxAmount)
                    .HasColumnName("Tx_Amount")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.UserTypeId).HasColumnName("User_Type_ID");

                entity.HasOne(d => d.AuditType)
                    .WithMany(p => p.AuditLog)
                    .HasForeignKey(d => d.AuditTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AUDIT_LOG_AUDIT_TYPE");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.AuditLog)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AUDIT_LOG_USER_TYPE");
            });

            modelBuilder.Entity<AuditType>(entity =>
            {
                entity.ToTable("AUDIT_TYPE");

                entity.Property(e => e.AuditTypeId).HasColumnName("Audit_Type_ID");

                entity.Property(e => e.AuditTypeName)
                    .IsRequired()
                    .HasColumnName("Audit_Type_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BasketLine>(entity =>
            {
                entity.HasKey(e => new { e.SenderId, e.PackageId });

                entity.ToTable("BASKET_LINE");

                entity.Property(e => e.SenderId).HasColumnName("Sender_ID");

                entity.Property(e => e.PackageId).HasColumnName("Package_ID");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.BasketLine)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BASKET_LINE_PACKAGE");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.BasketLine)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BASKET_SENDER");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("CITY");

                entity.Property(e => e.CityId).HasColumnName("City_ID");

                entity.Property(e => e.CityAvailability).HasColumnName("City_Availability");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasColumnName("City_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceId).HasColumnName("Province_ID");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CITY_PROVINCE");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("COMPANY");

                entity.Property(e => e.CompanyId).HasColumnName("Company_ID");

                entity.Property(e => e.CompanyCity)
                    .IsRequired()
                    .HasColumnName("Company_City")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyEmail)
                    .IsRequired()
                    .HasColumnName("Company_Email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyLogo)
                    .IsRequired()
                    .HasColumnName("Company_Logo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasColumnName("Company_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyPhone)
                    .IsRequired()
                    .HasColumnName("Company_Phone")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyPostalCode)
                    .IsRequired()
                    .HasColumnName("Company_Postal_Code")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyStreetAddress)
                    .IsRequired()
                    .HasColumnName("Company_Street_Address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompanySuburb)
                    .IsRequired()
                    .HasColumnName("Company_Suburb")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PayGateId)
                    .IsRequired()
                    .HasColumnName("PayGate_ID")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.PayGatePassword)
                    .IsRequired()
                    .HasColumnName("PayGate_Password")
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationNo)
                    .IsRequired()
                    .HasColumnName("Registration_No")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.VatNo)
                    .IsRequired()
                    .HasColumnName("VAT_No")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DownloadLocation>(entity =>
            {
                entity.HasKey(e => e.DownloadId);

                entity.ToTable("DOWNLOAD_LOCATION");

                entity.Property(e => e.DownloadId).HasColumnName("Download_ID");

                entity.Property(e => e.CityId).HasColumnName("City_ID");

                entity.Property(e => e.DownloadDateTime)
                    .HasColumnName("Download_DateTime")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.DownloadLocation)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DOWNLOAD_LOCATION_CITY");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("EMPLOYEE");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_ID");

                entity.Property(e => e.AccessRoleId).HasColumnName("Access_Role_ID");

                entity.Property(e => e.DateEmployed)
                    .HasColumnName("Date_Employed")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmployeeActive).HasColumnName("Employee_Active");

                entity.Property(e => e.EmployeeEmail)
                    .IsRequired()
                    .HasColumnName("Employee_Email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeImage)
                    .IsRequired()
                    .HasColumnName("Employee_Image")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasColumnName("Employee_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeNationalId)
                    .HasColumnName("Employee_National_ID")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeePassportNo)
                    .HasColumnName("Employee_Passport_No")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeePassword)
                    .IsRequired()
                    .HasColumnName("Employee_Password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeePhone)
                    .IsRequired()
                    .HasColumnName("Employee_Phone")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeSurname)
                    .IsRequired()
                    .HasColumnName("Employee_Surname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AccessRole)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.AccessRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EMPLOYEE_ACCESS_ROLE");
            });

            modelBuilder.Entity<FixedPrice>(entity =>
            {
                entity.ToTable("FIXED_PRICE");

                entity.Property(e => e.FixedPriceId).HasColumnName("Fixed_Price_ID");

                entity.Property(e => e.DateFrom).HasColumnType("datetime");

                entity.Property(e => e.DateTo).HasColumnType("datetime");

                entity.Property(e => e.FixedPrice1)
                    .HasColumnName("Fixed_Price")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.FixedPriceDescr)
                    .IsRequired()
                    .HasColumnName("Fixed_Price_Descr")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("LOGIN");

                entity.Property(e => e.LoginId).HasColumnName("Login_ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserTypeId).HasColumnName("UserType_ID");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Login)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LOGIN_USER_TYPE");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("NOTIFICATION");

                entity.Property(e => e.NotificationId).HasColumnName("Notification_ID");

                entity.Property(e => e.NotificationDateTime)
                    .HasColumnName("Notification_DateTime")
                    .HasColumnType("date");

                entity.Property(e => e.NotificationMessage)
                    .IsRequired()
                    .HasColumnName("Notification_Message")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.ToTable("PACKAGE");

                entity.Property(e => e.PackageId).HasColumnName("Package_ID");

                entity.Property(e => e.PackageContentId).HasColumnName("Package_Content_ID");

                entity.Property(e => e.PackageTypeId).HasColumnName("Package_Type_ID");

                entity.Property(e => e.PackageTypeQty).HasColumnName("Package_Type_Qty");

                entity.Property(e => e.ShipmentId).HasColumnName("Shipment_ID");

                entity.HasOne(d => d.PackageContent)
                    .WithMany(p => p.Package)
                    .HasForeignKey(d => d.PackageContentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PACKAGE_PACKAGE_CONTENT");

                entity.HasOne(d => d.PackageType)
                    .WithMany(p => p.Package)
                    .HasForeignKey(d => d.PackageTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PACKAGE_PACKAGE_TYPE");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.Package)
                    .HasForeignKey(d => d.ShipmentId)
                    .HasConstraintName("FK_PACKAGE_SHIPMENT");
            });

            modelBuilder.Entity<PackageContent>(entity =>
            {
                entity.ToTable("PACKAGE_CONTENT");

                entity.Property(e => e.PackageContentId).HasColumnName("Package_Content_ID");

                entity.Property(e => e.PackageContent1)
                    .IsRequired()
                    .HasColumnName("Package_Content")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PackageQrcode)
                    .HasColumnName("Package_QRCode")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PackageType>(entity =>
            {
                entity.ToTable("PACKAGE_TYPE");

                entity.Property(e => e.PackageTypeId).HasColumnName("Package_Type_ID");

                entity.Property(e => e.PackageTypeDescr)
                    .IsRequired()
                    .HasColumnName("Package_Type_Descr")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PackageTypeImage)
                    .IsRequired()
                    .HasColumnName("Package_Type_Image")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PackageTypePrice)
                    .HasColumnName("Package_Type_Price")
                    .HasColumnType("decimal(5, 2)");
            });

            modelBuilder.Entity<PaymentReference>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.ToTable("PAYMENT_REFERENCE");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("Transaction_ID")
                    .HasColumnType("numeric(11, 0)");

                entity.Property(e => e.Amount).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.PaymentTypeDetail)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentTypeMethod)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ResultCode)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.ResultDescr)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.RiskIndicator)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ShipmentId).HasColumnName("Shipment_ID");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TxStatusCode).HasColumnType("numeric(1, 0)");

                entity.Property(e => e.TxStatusDescr)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.PaymentReference)
                    .HasForeignKey(d => d.ShipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PAYMENT_REFERENCE_SHIPMENT");
            });

            modelBuilder.Entity<Penalty>(entity =>
            {
                entity.HasKey(e => e.PentaltyId);

                entity.ToTable("PENALTY");

                entity.Property(e => e.PentaltyId).HasColumnName("Pentalty_ID");

                entity.Property(e => e.DateCharged)
                    .HasColumnName("Date_Charged")
                    .HasColumnType("datetime");

                entity.Property(e => e.DatePaid)
                    .HasColumnName("Date_Paid")
                    .HasColumnType("datetime");

                entity.Property(e => e.PenaltyAmount)
                    .HasColumnName("Penalty_Amount")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.ShipmentId).HasColumnName("Shipment_ID");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.Penalty)
                    .HasForeignKey(d => d.ShipmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PENALTY_SHIPMENT");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("PROVINCE");

                entity.Property(e => e.ProvinceId).HasColumnName("Province_ID");

                entity.Property(e => e.ProvinceName)
                    .IsRequired()
                    .HasColumnName("Province_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Receiver>(entity =>
            {
                entity.ToTable("RECEIVER");

                entity.Property(e => e.ReceiverId).HasColumnName("Receiver_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("Phone_Number")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sender>(entity =>
            {
                entity.ToTable("SENDER");

                entity.Property(e => e.SenderId).HasColumnName("Sender_ID");

                entity.Property(e => e.LoginId).HasColumnName("Login_ID");

                entity.Property(e => e.SenderActive).HasColumnName("Sender_Active");

                entity.Property(e => e.SenderEmail)
                    .IsRequired()
                    .HasColumnName("Sender_Email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SenderName)
                    .IsRequired()
                    .HasColumnName("Sender_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SenderNationalId)
                    .HasColumnName("Sender_NationalID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SenderPassportNo)
                    .HasColumnName("Sender_PassportNo")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SenderSurname)
                    .IsRequired()
                    .HasColumnName("Sender_Surname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.Sender)
                    .HasForeignKey(d => d.LoginId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SENDER_LOGIN");
            });

            modelBuilder.Entity<Shipment>(entity =>
            {
                entity.ToTable("SHIPMENT");

                entity.Property(e => e.ShipmentId).HasColumnName("Shipment_ID");

                entity.Property(e => e.AgentId).HasColumnName("Agent_ID");

                entity.Property(e => e.CollectionTime)
                    .HasColumnName("Collection_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeliveryTime)
                    .HasColumnName("Delivery_Time")
                    .HasColumnType("datetime");

                entity.Property(e => e.EndLatitude)
                    .IsRequired()
                    .HasColumnName("End_Latitude")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.EndLongitude)
                    .IsRequired()
                    .HasColumnName("End_Longitude")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.ReceiverId).HasColumnName("Receiver_ID");

                entity.Property(e => e.ReceiverSig)
                    .HasColumnName("Receiver_Sig")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SenderId).HasColumnName("Sender_ID");

                entity.Property(e => e.SenderSig)
                    .HasColumnName("Sender_Sig")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ShipmentDate)
                    .HasColumnName("Shipment_Date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ShipmentDistance)
                    .IsRequired()
                    .HasColumnName("Shipment_Distance")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ShipmentStatusId).HasColumnName("Shipment_Status_ID");

                entity.Property(e => e.SpecialInstruction)
                    .IsRequired()
                    .HasColumnName("Special_Instruction")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StartLatitude)
                    .IsRequired()
                    .HasColumnName("Start_Latitude")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.StartLongitude)
                    .IsRequired()
                    .HasColumnName("Start_Longitude")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.TotalCost).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Shipment)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_SHIPMENT_AGENT");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.Shipment)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_RECEIVER");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Shipment)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_SENDER");

                entity.HasOne(d => d.ShipmentStatus)
                    .WithMany(p => p.Shipment)
                    .HasForeignKey(d => d.ShipmentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_SHIPMENT_STATUS");
            });

            modelBuilder.Entity<ShipmentAgent>(entity =>
            {
                entity.HasKey(e => e.AgentId);

                entity.ToTable("SHIPMENT_AGENT");

                entity.Property(e => e.AgentId).HasColumnName("Agent_ID");

                entity.Property(e => e.AgentActive).HasColumnName("Agent_Active");

                entity.Property(e => e.AgentAvailability).HasColumnName("Agent_Availability");

                entity.Property(e => e.AgentCompany)
                    .IsRequired()
                    .HasColumnName("Agent_Company")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AgentEmail)
                    .IsRequired()
                    .HasColumnName("Agent_Email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AgentImage)
                    .IsRequired()
                    .HasColumnName("Agent_Image")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AgentName)
                    .IsRequired()
                    .HasColumnName("Agent_Name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AgentNationalId)
                    .HasColumnName("Agent_National_ID")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.AgentPassportNo)
                    .HasColumnName("Agent_Passport_No")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.AgentSurname)
                    .IsRequired()
                    .HasColumnName("Agent_Surname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationAccepted).HasColumnName("Application_Accepted");

                entity.Property(e => e.BankAccNo)
                    .IsRequired()
                    .HasColumnName("Bank_Acc_No")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.BankAccType)
                    .IsRequired()
                    .HasColumnName("Bank_Acc_Type")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BankBranchCode)
                    .IsRequired()
                    .HasColumnName("Bank_Branch_Code")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasColumnName("Bank_Name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CityId).HasColumnName("City_ID");

                entity.Property(e => e.DateEmployed)
                    .HasColumnName("Date_Employed")
                    .HasColumnType("datetime");

                entity.Property(e => e.LicenceImage)
                    .IsRequired()
                    .HasColumnName("Licence_Image")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LoginId).HasColumnName("Login_ID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.ShipmentAgent)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_AGENT_CITY");

                entity.HasOne(d => d.Login)
                    .WithMany(p => p.ShipmentAgent)
                    .HasForeignKey(d => d.LoginId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_AGENT_LOGIN");
            });

            modelBuilder.Entity<ShipmentAgentLocation>(entity =>
            {
                entity.HasKey(e => e.CurrentLocId);

                entity.ToTable("SHIPMENT_AGENT_LOCATION");

                entity.Property(e => e.CurrentLocId).HasColumnName("CurrentLoc_ID");

                entity.Property(e => e.AgentId).HasColumnName("Agent_ID");

                entity.Property(e => e.CurrentLocLatitude)
                    .HasColumnName("CurrentLoc_Latitude")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentLocLongitude)
                    .HasColumnName("CurrentLoc_Longitude")
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.ShipmentAgentLocation)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_AGENT_LOCATION_SHIPMENT_AGENT");
            });

            modelBuilder.Entity<ShipmentAgentNotification>(entity =>
            {
                entity.HasKey(e => new { e.NotificationId, e.AgentId });

                entity.ToTable("SHIPMENT_AGENT_NOTIFICATION");

                entity.Property(e => e.NotificationId).HasColumnName("Notification_ID");

                entity.Property(e => e.AgentId).HasColumnName("Agent_ID");

                entity.Property(e => e.DateRead)
                    .HasColumnName("Date_Read")
                    .HasColumnType("date");

                entity.Property(e => e.NotificationRead).HasColumnName("Notification_Read");

                entity.Property(e => e.RequestAccepted).HasColumnName("Request_Accepted");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.ShipmentAgentNotification)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_AGENT_NOTIFICATION_SHIPMENT_AGENT");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.ShipmentAgentNotification)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SHIPMENT_AGENT_NOTIFICATION_NOTIFICATION");
            });

            modelBuilder.Entity<ShipmentStatus>(entity =>
            {
                entity.ToTable("SHIPMENT_STATUS");

                entity.Property(e => e.ShipmentStatusId).HasColumnName("Shipment_Status_ID");

                entity.Property(e => e.ShipmentStatusDescr)
                    .IsRequired()
                    .HasColumnName("Shipment_Status_Descr")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("USER_TYPE");

                entity.Property(e => e.UserTypeId).HasColumnName("User_Type_ID");

                entity.Property(e => e.UserTypeDescr)
                    .IsRequired()
                    .HasColumnName("User_Type_Descr")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("VEHICLE");

                entity.Property(e => e.VehicleId).HasColumnName("Vehicle_ID");

                entity.Property(e => e.AgentId).HasColumnName("Agent_ID");

                entity.Property(e => e.Colour)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DateDeactivated)
                    .HasColumnName("Date_Deactivated")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateVerified)
                    .HasColumnName("Date_Verified")
                    .HasColumnType("datetime");

                entity.Property(e => e.LicencePlateNo)
                    .IsRequired()
                    .HasColumnName("Licence_Plate_No")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VehicleActive).HasColumnName("Vehicle_Active");

                entity.Property(e => e.VehicleMakeId).HasColumnName("Vehicle_Make_ID");

                entity.Property(e => e.VehicleProofImage)
                    .IsRequired()
                    .HasColumnName("Vehicle_Proof_Image")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.VehicleStatusId).HasColumnName("Vehicle_Status_ID");

                entity.Property(e => e.VehicleTypeId).HasColumnName("Vehicle_Type_ID");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VEHICLE_SHIPMENT_AGENT");

                entity.HasOne(d => d.VehicleMake)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.VehicleMakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VEHICLE_VEHICLE_MAKE");

                entity.HasOne(d => d.VehicleStatus)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.VehicleStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VEHICLE_VEHICLE_STATUS");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.Vehicle)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VEHICLE_VEHICLE_TYPE");
            });

            modelBuilder.Entity<VehicleMake>(entity =>
            {
                entity.ToTable("VEHICLE_MAKE");

                entity.Property(e => e.VehicleMakeId).HasColumnName("Vehicle_Make_ID");

                entity.Property(e => e.VehicleMakeDescr)
                    .IsRequired()
                    .HasColumnName("Vehicle_Make_Descr")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VehiclePacakageLine>(entity =>
            {
                entity.HasKey(e => new { e.VehicleTypeId, e.PackageTypeId });

                entity.ToTable("VEHICLE_PACAKAGE_LINE");

                entity.Property(e => e.VehicleTypeId).HasColumnName("Vehicle_Type_ID");

                entity.Property(e => e.PackageTypeId).HasColumnName("Package_Type_ID");

                entity.HasOne(d => d.PackageType)
                    .WithMany(p => p.VehiclePacakageLine)
                    .HasForeignKey(d => d.PackageTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VEHICLE_PACAKAGE_LINE_PACKAGE_TYPE");

                entity.HasOne(d => d.VehicleType)
                    .WithMany(p => p.VehiclePacakageLine)
                    .HasForeignKey(d => d.VehicleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VEHICLE_PACAKAGE_LINE_VEHICLE_TYPE");
            });

            modelBuilder.Entity<VehicleStatus>(entity =>
            {
                entity.ToTable("VEHICLE_STATUS");

                entity.Property(e => e.VehicleStatusId).HasColumnName("Vehicle_Status_ID");

                entity.Property(e => e.VehicleStatusDescr)
                    .IsRequired()
                    .HasColumnName("Vehicle_Status_Descr")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("VEHICLE_TYPE");

                entity.Property(e => e.VehicleTypeId).HasColumnName("Vehicle_Type_ID");

                entity.Property(e => e.VehicleTypeDescr)
                    .IsRequired()
                    .HasColumnName("Vehicle_Type_Descr")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
