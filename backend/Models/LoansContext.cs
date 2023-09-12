using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public partial class LoansContext : DbContext
{
    public LoansContext()
    {
    }

    public LoansContext(DbContextOptions<LoansContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmployeeCardDetail> EmployeeCardDetails { get; set; }

    public virtual DbSet<EmployeeIssueDetail> EmployeeIssueDetails { get; set; }

    public virtual DbSet<EmployeeMaster> EmployeeMasters { get; set; }

    public virtual DbSet<ItemMaster> ItemMasters { get; set; }

    public virtual DbSet<LoanCardMaster> LoanCardMasters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WINDOWS-BVQNF6J;Database=loans;Trusted_Connection=True;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeCardDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("employee_card_details");

            entity.Property(e => e.CardIssueDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("card_issue_date");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("employee_id");
            entity.Property(e => e.LoanId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("loan_id");

            entity.HasOne(d => d.Employee).WithMany()
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__employee___emplo__32E0915F");

            entity.HasOne(d => d.Loan).WithMany()
                .HasForeignKey(d => d.LoanId)
                .HasConstraintName("FK__employee___loan___33D4B598");
        });

        modelBuilder.Entity<EmployeeIssueDetail>(entity =>
        {
            entity.HasKey(e => e.IssueId).HasName("PK__employee__D6185C392BA84582");

            entity.ToTable("employee_issue_details");

            entity.Property(e => e.IssueId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("issue_id");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("employee_id");
            entity.Property(e => e.IssueDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("issue_date");
            entity.Property(e => e.ItemId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("item_id");
            entity.Property(e => e.ReturnDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("return_date");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeIssueDetails)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__employee___emplo__2B3F6F97");

            entity.HasOne(d => d.Item).WithMany(p => p.EmployeeIssueDetails)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__employee___item___2C3393D0");
        });

        modelBuilder.Entity<EmployeeMaster>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__employee__C52E0BA8BE9D273B");

            entity.ToTable("employee_master");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("employee_id");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Password_hash");
            entity.Property(e => e.Salt)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("Salt");
            entity.Property(e => e.DateOfBirth)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.DateOfJoining)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("date_of_joining");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("department");
            entity.Property(e => e.Designation)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("designation");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("employee_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
        });

        modelBuilder.Entity<ItemMaster>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__item_mas__52020FDD641DC15A");

            entity.ToTable("item_master");

            entity.Property(e => e.ItemId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("item_id");
            entity.Property(e => e.IssueStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("issue_status");
            entity.Property(e => e.ItemCategory)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("item_category");
            entity.Property(e => e.ItemDescription)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("item_description");
            entity.Property(e => e.ItemMake)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("item_make");
            entity.Property(e => e.ItemValuation).HasColumnName("item_valuation");
        });

        modelBuilder.Entity<LoanCardMaster>(entity =>
        {
            entity.HasKey(e => e.LoanId).HasName("PK__loan_car__A1F7955434B68FFB");

            entity.ToTable("loan_card_master");

            entity.Property(e => e.LoanId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("loan_id");
            entity.Property(e => e.DurationInYears).HasColumnName("duration_in_years");
            entity.Property(e => e.LoanType)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("loan_type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
