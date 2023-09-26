using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backendAPIs.Models;

public partial class LoansContext : DbContext
{
    public LoansContext()
    {
    }

    public LoansContext(DbContextOptions<LoansContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmployeeLoanCardDetail> EmployeeLoanCardDetails { get; set; }

    public virtual DbSet<EmployeeMaster> EmployeeMasters { get; set; }

    public virtual DbSet<EmployeeRequestDetail> EmployeeRequestDetails { get; set; }

    public virtual DbSet<ItemMaster> ItemMasters { get; set; }

    public virtual DbSet<LoanCardMaster> LoanCardMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WINDOWS-BVQNF6J;Database=loans;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeLoanCardDetail>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__Employee__45A7902B4A72FCDB");

            entity.ToTable("Employee_loan_card_details");

            entity.Property(e => e.CardId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Card_id");
            entity.Property(e => e.CardIssueDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("Card_issue_date");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Employee_id");
            entity.Property(e => e.LoanId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Loan_id");
            entity.Property(e => e.RequestId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Request_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeLoanCardDetails)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Employee___Emplo__02FC7413");

            entity.HasOne(d => d.Loan).WithMany(p => p.EmployeeLoanCardDetails)
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("FK__Employee___Loan___03F0984C");

            entity.HasOne(d => d.Request).WithMany(p => p.EmployeeLoanCardDetails)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__Employee___Reque__02084FDA");
        });

        modelBuilder.Entity<EmployeeMaster>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__781228D983EB6E81");

            entity.ToTable("Employee_master");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Employee_id");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_of_birth");
            entity.Property(e => e.DateOfJoining)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("Date_of_joining");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Designation)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Employee_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Password_hash");
            entity.Property(e => e.Salt)
                .HasMaxLength(32)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeeRequestDetail>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Employee__E9C0AF0B7804DEDB");

            entity.ToTable("Employee_request_details");

            entity.Property(e => e.RequestId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Request_id");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Employee_id");
            entity.Property(e => e.ItemId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Item_id");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("Request_date");
            entity.Property(e => e.RequestStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('Pending Approval')")
                .HasColumnName("Request_status");
            entity.Property(e => e.ReturnDate)
                .HasColumnType("date")
                .HasColumnName("Return_date");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeRequestDetails)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Employee___Emplo__68487DD7");

            entity.HasOne(d => d.Item).WithMany(p => p.EmployeeRequestDetails)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Employee___Item___693CA210");
        });

        modelBuilder.Entity<ItemMaster>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item_mas__3FB403ACCFDA2565");

            entity.ToTable("Item_master");

            entity.Property(e => e.ItemId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Item_id");
            entity.Property(e => e.IssueStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Issue_status");
            entity.Property(e => e.ItemCategory)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Item_category");
            entity.Property(e => e.ItemDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Item_description");
            entity.Property(e => e.ItemMake)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Item_make");
            entity.Property(e => e.ItemValuation).HasColumnName("Item_valuation");

            entity.HasOne(d => d.ItemCategoryNavigation).WithMany(p => p.ItemMasters)
                .HasPrincipalKey(p => p.LoanType)
                .HasForeignKey(d => d.ItemCategory)
                .HasConstraintName("FK__Item_mast__Item___6477ECF3");
        });

        modelBuilder.Entity<LoanCardMaster>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__Loan_car__937D5B6B942A5531");

            entity.ToTable("Loan_card_master");

            entity.HasIndex(e => e.LoanType, "UQ__Loan_car__9F0F31C04D50CBE9").IsUnique();

            entity.Property(e => e.LoanId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Loan_id");
            entity.Property(e => e.DurationInYears).HasColumnName("Duration_in_years");
            entity.Property(e => e.LoanType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Loan_type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
