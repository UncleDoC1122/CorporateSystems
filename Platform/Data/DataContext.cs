using Microsoft.EntityFrameworkCore;
using Platform.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options)
			: base(options) { }

		public DbSet<TroopKind> TroopKinds { get; set; }

		public DbSet<TroopType> TroopTypes { get; set; }

		public DbSet<Schedule> Schedules { get; set; }

		public DbSet<MedicalComissionResults> MedicalComissionResults { get; set; }

		public DbSet<Recruit> Recruits { get; set; }

		public DbSet<RecruitmentOffice> RecruitmentOffices { get; set; }

		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<TroopKind>()
				.HasMany(tk => tk.TroopTypes)
				.WithOne(tt => tt.TroopKind)
				.HasForeignKey(tt => tt.TroopKindId);

			modelBuilder.Entity<RecruitmentOffice>()
				.HasMany(ro => ro.Schedule)
				.WithOne(s => s.RecruitmentOffice)
				.HasForeignKey(s => s.RecruitmentOfficeId);

			modelBuilder.Entity<Recruit>()
				.HasMany(r => r.MedicalComissionResults)
				.WithOne(mcr => mcr.Recruit)
				.HasForeignKey(mcr => mcr.RecruitId);

		}
	}
}
