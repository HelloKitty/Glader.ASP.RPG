using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Glader.ASP.RPGCharacter
{
	public sealed class RPGCharacterDatabaseContext<TCustomizableSlotType> : DbContext
		where TCustomizableSlotType : Enum
	{
		/// <summary>
		/// The character table.
		/// </summary>
		public DbSet<DBRPGCharacter> Characters { get; set; }

		/// <summary>
		/// The ownership relationship of characters.
		/// </summary>
		public DbSet<DBRPGCharacterOwnership> CharacterOwnership { get; set; }

		/// <summary>
		/// The customized slots for the character.
		/// </summary>
		public DbSet<DBRPGCharacterCustomizableSlot<TCustomizableSlotType>> CustomizableSlots { get; set; }

		/// <summary>
		/// The customizable slot types supported.
		/// </summary>
		public DbSet<DBRPGCharacterCustomizableSlotType<TCustomizableSlotType>> CustomizableSlotTypes { get; set; }

		public RPGCharacterDatabaseContext(DbContextOptions<RPGCharacterDatabaseContext<TCustomizableSlotType>> options)
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

			modelBuilder.Entity<DBRPGCharacterProgress>(builder =>
			{
				builder.Property(c => c.PlayTime)
					.HasDefaultValue(TimeSpan.Zero);
			});

			modelBuilder.Entity<DBRPGCharacterOwnership>(builder =>
			{
				//index by owner since we will query character lists and such.
				builder.HasIndex(c => c.OwnershipId);

				//Builds a composite key between the owner and characterid.
				//EF Requires keys, keyless entities are second class citizens.
				builder.HasKey(c => new {c.OwnershipId, c.CharacterId});
			});

			modelBuilder.Entity<DBRPGCharacterCustomizableSlot<TCustomizableSlotType>>(builder =>
			{
				builder.HasIndex(c => c.CharacterId);
				builder.HasKey(c => new {c.CharacterId, c.SlotType});
			});

			//Seed the DB with the available enum entries.
			modelBuilder.Entity<DBRPGCharacterCustomizableSlotType<TCustomizableSlotType>>().HasData(
				((TCustomizableSlotType[]) Enum.GetValues(typeof(TCustomizableSlotType)))
				.Select(v => new DBRPGCharacterCustomizableSlotType<TCustomizableSlotType>(v, v.ToString(), String.Empty))
				.ToArray()
			);
		}
	}
}
