using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Glader.ASP.RPGCharacter
{
	public sealed class RPGCharacterDatabaseContext : DbContext
	{
		/// <summary>
		/// The character table.
		/// </summary>
		public DbSet<DBRPGCharacter> Characters { get; set; }

		public RPGCharacterDatabaseContext(DbContextOptions<RPGCharacterDatabaseContext> options)
			: base(options)
		{

		}

		public RPGCharacterDatabaseContext()
		{

		}

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
