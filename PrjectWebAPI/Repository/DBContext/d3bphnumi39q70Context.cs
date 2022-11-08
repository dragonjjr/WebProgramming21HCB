using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Repository.DBContext
{
    public partial class d3bphnumi39q70Context : DbContext
    {
        public d3bphnumi39q70Context()
        {
        }

        public d3bphnumi39q70Context(DbContextOptions<d3bphnumi39q70Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<BankReference> BankReferences { get; set; }
        public virtual DbSet<DebtReminder> DebtReminders { get; set; }
        public virtual DbSet<PaymentFeeType> PaymentFeeTypes { get; set; }
        public virtual DbSet<PgStatStatement> PgStatStatements { get; set; }
        public virtual DbSet<Recipient> Recipients { get; set; }
        public virtual DbSet<TransactionBanking> TransactionBankings { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
        public virtual DbSet<UserManage> UserManages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=ec2-3-233-7-12.compute-1.amazonaws.com;Database=d3bphnumi39q70;Username=qfibtevwlsrhor;Password=fc1975c65abac0e78c29e4bd087f083c5dcde625ddfd7e6101875bfe0a5dbca5;Port=5432;SSL Mode=Require;Trust Server Certificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pg_stat_statements")
                .HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedOtpdate).HasColumnName("CreatedOTPDate");

                entity.Property(e => e.ExpiredOtpdate).HasColumnName("ExpiredOTPDate");

                entity.Property(e => e.Otp)
                    .HasMaxLength(255)
                    .HasColumnName("OTP");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

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

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            modelBuilder.Entity<DebtReminder>(entity =>
            {
                entity.ToTable("Debt_reminder");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.NoiDung)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Stk)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("STK");

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

                entity.HasOne(d => d.StkNavigation)
                    .WithMany(p => p.DebtReminders)
                    .HasPrincipalKey(p => p.Stk)
                    .HasForeignKey(d => d.Stk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Debt_reminder_fk0");
            });

            modelBuilder.Entity<PaymentFeeType>(entity =>
            {
                entity.ToTable("PaymentFeeType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<PgStatStatement>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pg_stat_statements", "heroku_ext");

                entity.Property(e => e.BlkReadTime).HasColumnName("blk_read_time");

                entity.Property(e => e.BlkWriteTime).HasColumnName("blk_write_time");

                entity.Property(e => e.Calls).HasColumnName("calls");

                entity.Property(e => e.Dbid)
                    .HasColumnType("oid")
                    .HasColumnName("dbid");

                entity.Property(e => e.LocalBlksDirtied).HasColumnName("local_blks_dirtied");

                entity.Property(e => e.LocalBlksHit).HasColumnName("local_blks_hit");

                entity.Property(e => e.LocalBlksRead).HasColumnName("local_blks_read");

                entity.Property(e => e.LocalBlksWritten).HasColumnName("local_blks_written");

                entity.Property(e => e.MaxExecTime).HasColumnName("max_exec_time");

                entity.Property(e => e.MaxPlanTime).HasColumnName("max_plan_time");

                entity.Property(e => e.MeanExecTime).HasColumnName("mean_exec_time");

                entity.Property(e => e.MeanPlanTime).HasColumnName("mean_plan_time");

                entity.Property(e => e.MinExecTime).HasColumnName("min_exec_time");

                entity.Property(e => e.MinPlanTime).HasColumnName("min_plan_time");

                entity.Property(e => e.Plans).HasColumnName("plans");

                entity.Property(e => e.Query).HasColumnName("query");

                entity.Property(e => e.Queryid).HasColumnName("queryid");

                entity.Property(e => e.Rows).HasColumnName("rows");

                entity.Property(e => e.SharedBlksDirtied).HasColumnName("shared_blks_dirtied");

                entity.Property(e => e.SharedBlksHit).HasColumnName("shared_blks_hit");

                entity.Property(e => e.SharedBlksRead).HasColumnName("shared_blks_read");

                entity.Property(e => e.SharedBlksWritten).HasColumnName("shared_blks_written");

                entity.Property(e => e.StddevExecTime).HasColumnName("stddev_exec_time");

                entity.Property(e => e.StddevPlanTime).HasColumnName("stddev_plan_time");

                entity.Property(e => e.TempBlksRead).HasColumnName("temp_blks_read");

                entity.Property(e => e.TempBlksWritten).HasColumnName("temp_blks_written");

                entity.Property(e => e.TotalExecTime).HasColumnName("total_exec_time");

                entity.Property(e => e.TotalPlanTime).HasColumnName("total_plan_time");

                entity.Property(e => e.Userid)
                    .HasColumnType("oid")
                    .HasColumnName("userid");

                entity.Property(e => e.WalBytes).HasColumnName("wal_bytes");

                entity.Property(e => e.WalFpi).HasColumnName("wal_fpi");

                entity.Property(e => e.WalRecords).HasColumnName("wal_records");
            });

            modelBuilder.Entity<Recipient>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.Name).ValueGeneratedOnAdd();

                entity.Property(e => e.Stk)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("STK");

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Recipients)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Recipients_fk0");
            });

            modelBuilder.Entity<TransactionBanking>(entity =>
            {
                entity.ToTable("Transaction_banking");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BankReferenceId).HasColumnName("BankReferenceID");

                entity.Property(e => e.CeatedOtpdate).HasColumnName("CeatedOTPDate");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("'2022-11-08 02:48:03.042626'::timestamp without time zone");

                entity.Property(e => e.ExpiredOtpdate).HasColumnName("ExpiredOTPDate");

                entity.Property(e => e.Otp)
                    .HasMaxLength(255)
                    .HasColumnName("OTP");

                entity.Property(e => e.PaymentFeeTypeId).HasColumnName("PaymentFeeTypeID");

                entity.Property(e => e.Stkreceive).HasColumnName("STKReceive");

                entity.Property(e => e.Stksend).HasColumnName("STKSend");

                entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionTypeID");

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);

                entity.HasOne(d => d.BankReference)
                    .WithMany(p => p.TransactionBankings)
                    .HasForeignKey(d => d.BankReferenceId)
                    .HasConstraintName("Transaction_banking_fk2");

                entity.HasOne(d => d.PaymentFeeType)
                    .WithMany(p => p.TransactionBankings)
                    .HasForeignKey(d => d.PaymentFeeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Transaction_banking_fk1");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.TransactionBankings)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Transaction_banking_fk0");
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.ToTable("TransactionType");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            modelBuilder.Entity<UserManage>(entity =>
            {
                entity.ToTable("User_manage");

                entity.HasIndex(e => e.Stk, "User_manage_STK_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.BankKind)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Cmnd)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

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

                entity.Property(e => e.Stk)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("STK");

                entity.Property(e => e.UpdatedBy).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
