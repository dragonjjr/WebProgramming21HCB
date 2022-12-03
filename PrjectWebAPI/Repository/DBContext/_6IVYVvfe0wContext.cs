using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Repository.DBContext
{
    public partial class _6IVYVvfe0wContext : DbContext
    {
        public _6IVYVvfe0wContext()
        {
        }

        public _6IVYVvfe0wContext(DbContextOptions<_6IVYVvfe0wContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<BankReference> BankReferences { get; set; }
        public virtual DbSet<DebtReminder> DebtReminders { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<OtpTable> OtpTables { get; set; }
        public virtual DbSet<PaymentFeeType> PaymentFeeTypes { get; set; }
        public virtual DbSet<Recipient> Recipients { get; set; }
        public virtual DbSet<TransactionBanking> TransactionBankings { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
        public virtual DbSet<UserManage> UserManages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=remotemysql.com;userid=6IVYVvfe0w;password=rSLnSeLDh7;database=6IVYVvfe0w;Port=3306");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.Username, "Username")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Account)
                    .HasForeignKey<Account>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Account_fk0");
            });

            modelBuilder.Entity<BankReference>(entity =>
            {
                entity.ToTable("BankReference");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<DebtReminder>(entity =>
            {
                entity.ToTable("Debt_reminder");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.NoiDung)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SoTien).HasColumnType("decimal(10,0)");

                entity.Property(e => e.Status).HasColumnType("int(11)");

                entity.Property(e => e.Stkreceive)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("STKReceive");

                entity.Property(e => e.Stksend)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("STKSend");

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Notification");

                entity.HasIndex(e => e.Id, "id")
                    .IsUnique();

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Stkreceive)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("STKReceive");

                entity.Property(e => e.Stksend)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("STKSend");

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<OtpTable>(entity =>
            {
                entity.ToTable("OTP_Table");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Otp)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("OTP");

                entity.Property(e => e.Status).HasDefaultValueSql("'0'");

                entity.Property(e => e.TransactionId)
                    .HasColumnType("int(11)")
                    .HasColumnName("TransactionID");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("UserID");
            });

            modelBuilder.Entity<PaymentFeeType>(entity =>
            {
                entity.ToTable("PaymentFeeType");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<Recipient>(entity =>
            {
                entity.HasIndex(e => e.BankId, "BankID");

                entity.HasIndex(e => e.UserId, "Recipients_fk0");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.BankId)
                    .HasColumnType("int(11)")
                    .HasColumnName("BankID");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Stk)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("STK");

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UserId)
                    .HasColumnType("int(255)")
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.Recipients)
                    .HasForeignKey(d => d.BankId)
                    .HasConstraintName("Recipients_ibfk_1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Recipients)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Recipients_fk0");
            });

            modelBuilder.Entity<TransactionBanking>(entity =>
            {
                entity.ToTable("Transaction_banking");

                entity.HasIndex(e => e.TransactionTypeId, "Transaction_banking_fk0");

                entity.HasIndex(e => e.PaymentFeeTypeId, "Transaction_banking_fk1");

                entity.HasIndex(e => e.BankReferenceId, "Transaction_banking_fk2");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.BankReferenceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("BankReferenceID");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Money).HasColumnType("decimal(10,0)");

                entity.Property(e => e.PaymentFeeTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("PaymentFeeTypeID");

                entity.Property(e => e.Stkreceive)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("STKReceive");

                entity.Property(e => e.Stksend)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("STKSend");

                entity.Property(e => e.TransactionTypeId)
                    .HasColumnType("int(255)")
                    .HasColumnName("TransactionTypeID");

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasOne(d => d.BankReference)
                    .WithMany(p => p.TransactionBankings)
                    .HasForeignKey(d => d.BankReferenceId)
                    .HasConstraintName("Transaction_banking_fk2");

                entity.HasOne(d => d.PaymentFeeType)
                    .WithMany(p => p.TransactionBankings)
                    .HasForeignKey(d => d.PaymentFeeTypeId)
                    .HasConstraintName("Transaction_banking_fk1");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.TransactionBankings)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .HasConstraintName("Transaction_banking_fk0");
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.ToTable("TransactionType");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            modelBuilder.Entity<UserManage>(entity =>
            {
                entity.ToTable("User_manage");

                entity.HasIndex(e => e.Email, "Email")
                    .IsUnique();

                entity.HasIndex(e => e.Stk, "STK");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.BankKind).HasMaxLength(255);

                entity.Property(e => e.Cmnd)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.IsStaff).HasColumnName("isStaff");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.SoDu).HasColumnType("decimal(10,0)");

                entity.Property(e => e.Stk)
                    .HasMaxLength(255)
                    .HasColumnName("STK");

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
