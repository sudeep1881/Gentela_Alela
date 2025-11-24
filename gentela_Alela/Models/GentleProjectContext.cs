using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace gentela_Alela.Models;

public partial class GentleProjectContext : DbContext
{
    public GentleProjectContext()
    {
    }

    public GentleProjectContext(DbContextOptions<GentleProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddtionalDetail> AddtionalDetails { get; set; }

    public virtual DbSet<CommunicationDetail> CommunicationDetails { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<DomicileVerification> DomicileVerifications { get; set; }

    public virtual DbSet<EducationDetail> EducationDetails { get; set; }

    public virtual DbSet<LoginDetail> LoginDetails { get; set; }

    public virtual DbSet<PersonalDetail> PersonalDetails { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Taluk> Taluks { get; set; }

    public virtual DbSet<YuvanidhiApplicantDetail> YuvanidhiApplicantDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-DH1O5F3\\SQLEXPRESS;Initial Catalog=GentleProject;User ID=sa;Password=Sudeep@123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddtionalDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Addtiona__3213E83FC634CFA0");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Caste)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Disability)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("disability");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Upidno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UPIDNO");
        });

        modelBuilder.Entity<CommunicationDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Communic__3213E83F674864CD");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(12)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC0705AB240B");

            entity.ToTable("Country");

            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__District__3214EC07FDC0818E");

            entity.ToTable("District");

            entity.Property(e => e.DistrictName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.State).WithMany(p => p.Districts)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__District__StateI__07C12930");
        });

        modelBuilder.Entity<DomicileVerification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Domicile__3213E83FDC00CB46");

            entity.ToTable("DomicileVerification");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DomicileOption)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.NameAsPerRtionOrcet)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NameAsPerRtionORCet");
            entity.Property(e => e.RationOrcetNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("RationORCetNumber");
        });

        modelBuilder.Entity<EducationDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Educatio__3214EC07B03D7112");

            entity.Property(e => e.CandidateName).HasMaxLength(150);
            entity.Property(e => e.CollegeName).HasMaxLength(150);
            entity.Property(e => e.CourseType).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Isdelted).HasColumnName("isdelted");
            entity.Property(e => e.Level).HasMaxLength(50);
            entity.Property(e => e.RegistrationNumber).HasMaxLength(100);
            entity.Property(e => e.UniversityName).HasMaxLength(150);
        });

        modelBuilder.Entity<LoginDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoginDet__3213E83FC24F14A3");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        modelBuilder.Entity<PersonalDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personal__3214EC07E19520D1");

            entity.ToTable("Personal_Details");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.FullName).IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.Password)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProfileImage).IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.StateId).HasColumnName("StateID");

            entity.HasOne(d => d.Country).WithMany(p => p.PersonalDetails)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__Personal___Count__2BFE89A6");

            entity.HasOne(d => d.District).WithMany(p => p.PersonalDetails)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK__Personal___Distr__2DE6D218");

            entity.HasOne(d => d.Role).WithMany(p => p.PersonalDetails)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Personal___RoleI__2B0A656D");

            entity.HasOne(d => d.State).WithMany(p => p.PersonalDetails)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__Personal___State__2CF2ADDF");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Registra__3213E83FA14A79DA");

            entity.ToTable("Registration");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.ProfileImage)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83F41DF49C4");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__State__3214EC0754447B01");

            entity.ToTable("State");

            entity.Property(e => e.StateName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__State__CountryId__04E4BC85");
        });

        modelBuilder.Entity<Taluk>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Taluk__3213E83FFF2239E7");

            entity.ToTable("Taluk");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DistictId).HasColumnName("distictId");
            entity.Property(e => e.TalukName)
                .IsUnicode(false)
                .HasColumnName("Taluk_Name");

            entity.HasOne(d => d.Distict).WithMany(p => p.Taluks)
                .HasForeignKey(d => d.DistictId)
                .HasConstraintName("FK__Taluk__distictId__3C34F16F");
        });

        modelBuilder.Entity<YuvanidhiApplicantDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Yuvanidh__3214EC07763CC972");

            entity.ToTable("Yuvanidhi_Applicant_Details");

            entity.Property(e => e.AdditionalId).HasColumnName("additionalId");
            entity.Property(e => e.Address1).IsUnicode(false);
            entity.Property(e => e.Address2)
                .IsUnicode(false)
                .HasColumnName("address2");
            entity.Property(e => e.AdharName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CommunicationId).HasColumnName("communicationId");
            entity.Property(e => e.DistinctId).HasColumnName("distinctID");
            entity.Property(e => e.DomicineId).HasColumnName("domicineId");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.PernmentAdress)
                .IsUnicode(false)
                .HasColumnName("pernmentAdress");
            entity.Property(e => e.Photo).IsUnicode(false);
            entity.Property(e => e.Pincode)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Additional).WithMany(p => p.YuvanidhiApplicantDetails)
                .HasForeignKey(d => d.AdditionalId)
                .HasConstraintName("FK__Yuvanidhi__addit__09746778");

            entity.HasOne(d => d.Communication).WithMany(p => p.YuvanidhiApplicantDetails)
                .HasForeignKey(d => d.CommunicationId)
                .HasConstraintName("FK__Yuvanidhi__commu__0880433F");

            entity.HasOne(d => d.Distinct).WithMany(p => p.YuvanidhiApplicantDetails)
                .HasForeignKey(d => d.DistinctId)
                .HasConstraintName("FK__Yuvanidhi__disti__43D61337");

            entity.HasOne(d => d.Domicine).WithMany(p => p.YuvanidhiApplicantDetails)
                .HasForeignKey(d => d.DomicineId)
                .HasConstraintName("FK__Yuvanidhi__domic__078C1F06");

            entity.HasOne(d => d.Education).WithMany(p => p.YuvanidhiApplicantDetails)
                .HasForeignKey(d => d.EducationId)
                .HasConstraintName("FK__Yuvanidhi__Educa__0697FACD");

            entity.HasOne(d => d.Taluk).WithMany(p => p.YuvanidhiApplicantDetails)
                .HasForeignKey(d => d.TalukId)
                .HasConstraintName("FK__Yuvanidhi__Taluk__44CA3770");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
