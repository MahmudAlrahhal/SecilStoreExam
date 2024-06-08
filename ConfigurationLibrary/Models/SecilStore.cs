using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SecilStoreExam.Models;

public partial class SecilStore : DbContext
{
    public SecilStore()
    {
    }

    public SecilStore(DbContextOptions<SecilStore> options)
        : base(options)
    {
    }

    public virtual DbSet<ConfigurationRecord> ConfigurationRecords { get; set; }

    public int ExecuteConfigurationsUpdateSP(int Config_id, String Name,
    Object Type, object value, bool isActive, string ApplicationName)
    {
        var parameter1Param = new SqlParameter("@Parameter1", Config_id);
        var parameter2Param = new SqlParameter("@Parameter2", Name);
        var parameter3Param = new SqlParameter("@Parameter3", Type);
        var parameter4Param = new SqlParameter("@Parameter4", value);
        var parameter5Param = new SqlParameter("@Parameter5", isActive);
        var parameter6Param = new SqlParameter("@Parameter6", ApplicationName);

        // Execute stored procedure using ExecuteSqlRaw
        return Database.ExecuteSqlRaw("EXEC [dbo].[UpdateAppointment]" +
            " @Parameter1, @Parameter2, @Parameter3, @Parameter4, @Parameter5, @Parameter6," +
            " @Parameter7, @Parameter8, @Parameter9, @Parameter10",
            parameter1Param, parameter2Param, parameter3Param, parameter4Param,
            parameter5Param, parameter6Param);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-N9EN98T;Database=SecilStore;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConfigurationRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Configur__3214EC07867CFD56");

            entity.ToTable("Configuration");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ApplicationName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.Value).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
