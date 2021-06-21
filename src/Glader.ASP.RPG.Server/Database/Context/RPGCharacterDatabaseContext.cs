using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Glader.ASP.RPG
{
	public abstract class RPGCharacterDatabaseContext : DbContext
	{
		/// <summary>
		/// The character table.
		/// </summary>
		public DbSet<DBRPGCharacter> Characters { get; set; }

		/// <summary>
		/// The ownership relationship of characters.
		/// </summary>
		public DbSet<DBRPGCharacterOwnership> CharacterOwnership { get; set; }

		public DbSet<DBRPGCharacterProgress> CharacterProgresses { get; set; }

		public DbSet<DBRPGGroup> Groups { get; set; }

		public DbSet<DBRPGGroupMember> GroupMembers { get; set; }

		protected RPGCharacterDatabaseContext(DbContextOptions options)
			: base(options)
		{

		}

		protected RPGCharacterDatabaseContext()
		{

		}

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
				builder.HasKey(c => new { c.OwnershipId, c.CharacterId });
			});
		}
	}

	public abstract class RPGCharacterDatabaseContext<TRaceType, TClassType> : RPGCharacterDatabaseContext
		where TRaceType : Enum 
		where TClassType : Enum
	{
		public DbSet<DBRPGClass<TClassType>> Classes { get; set; }

		public DbSet<DBRPGRace<TRaceType>> Races { get; set; }

		public DbSet<DBRPGCharacterDefinition<TRaceType, TClassType>> CharacterDefinitions { get; set; }

		protected RPGCharacterDatabaseContext(DbContextOptions<RPGCharacterDatabaseContext<TRaceType, TClassType>> options)
			: base(options)
		{

		}

		protected RPGCharacterDatabaseContext(DbContextOptions options)
			: base(options)
		{

		}

		protected RPGCharacterDatabaseContext()
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//Seed the DB with the available enum entries.
			modelBuilder.Entity<DBRPGRace<TRaceType>>().HasData(
				((TRaceType[])Enum.GetValues(typeof(TRaceType)))
				.Select(v => new DBRPGRace<TRaceType>(v, v.ToString(), String.Empty))
				.ToArray()
			);

			//Seed the DB with the available enum entries.
			modelBuilder.Entity<DBRPGClass<TClassType>>().HasData(
				((TClassType[])Enum.GetValues(typeof(TClassType)))
				.Select(v => new DBRPGClass<TClassType>(v, v.ToString(), String.Empty))
				.ToArray()
			);
		}
	}

	public sealed class RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType> 
		: RPGCharacterDatabaseContext<TRaceType, TClassType>
		where TCustomizableSlotType : Enum
		where TProportionSlotType : Enum
		where TRaceType : Enum
		where TClassType : Enum
		where TSkillType : Enum
		where TStatType : Enum
	{
		/// <summary>
		/// The customized slots for the character.
		/// </summary>
		public DbSet<DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>> CustomizableSlots { get; set; }

		/// <summary>
		/// The customizable slot types supported.
		/// </summary>
		public DbSet<DBRPGCharacterCustomizableSlotType<TCustomizableSlotType>> CustomizableSlotTypes { get; set; }

		/// <summary>
		/// The customized slots for the character.
		/// </summary>
		public DbSet<DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType>> ProportionSlots { get; set; }

		/// <summary>
		/// The customizable slot types supported.
		/// </summary>
		public DbSet<DBRPGCharacterProportionSlotType<TProportionSlotType>> ProportionSlotTypes { get; set; }

		public DbSet<DBRPGCharacterSkillKnown<TSkillType>> CharacterKnownSkills { get; set; }

		public DbSet<DBRPGCharacterSkillLevel<TSkillType>> CharacterSkillLevels { get; set; }

		public DbSet<DBRPGSkill<TSkillType>> Skills { get; set; }

		public DbSet<DBRPGStat<TStatType>> Stats { get; set; }

		public DbSet<DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>> StatDefaults { get; set; }

		public RPGCharacterDatabaseContext(DbContextOptions<RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType>> options)
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

			modelBuilder.Entity<DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>>(builder =>
			{
				builder.HasOne<DBRPGCharacter>()
					.WithMany()
					.HasForeignKey(c => c.CharacterId);

				builder.HasIndex(c => c.CharacterId);
				builder.HasKey(c => new {c.CharacterId, c.SlotType});

				if (!typeof(TColorStructureType).IsPrimitive)
				{
					//TODO: A total hack, but it must be a reference type if we make it Owned.
					Expression<Func<DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>, TColorStructureType>> expression = c => c.SlotColor;
					((dynamic)builder).OwnsOne(expression);
				}
			});

			//Seed the DB with the available enum entries.
			modelBuilder.Entity<DBRPGCharacterCustomizableSlotType<TCustomizableSlotType>>().HasData(
				((TCustomizableSlotType[]) Enum.GetValues(typeof(TCustomizableSlotType)))
				.Select(v => new DBRPGCharacterCustomizableSlotType<TCustomizableSlotType>(v, v.ToString(), String.Empty))
				.ToArray()
			);

			modelBuilder.Entity<DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType>>(builder =>
			{
				builder.HasOne<DBRPGCharacter>()
					.WithMany()
					.HasForeignKey(c => c.CharacterId);

				builder.HasIndex(c => c.CharacterId);
				builder.HasKey(c => new { c.CharacterId, c.SlotType });

				if(!typeof(TProportionStructureType).IsPrimitive)
				{
					//TODO: A total hack, but it must be a reference type if we make it Owned.
					Expression<Func<DBRPGCharacterProportionSlot<TProportionSlotType, TProportionStructureType>, TProportionStructureType>> expression = c => c.Proportion;
					((dynamic)builder).OwnsOne(expression);
				}
			});

			//Seed the DB with the available enum entries.
			modelBuilder.Entity<DBRPGCharacterProportionSlotType<TProportionSlotType>>().HasData(
				((TProportionSlotType[])Enum.GetValues(typeof(TProportionSlotType)))
				.Select(v => new DBRPGCharacterProportionSlotType<TProportionSlotType>(v, v.ToString(), String.Empty))
				.ToArray()
			);

			//Seed the DB with the available enum entries.
			modelBuilder.Entity<DBRPGSkill<TSkillType>>().HasData(
				((TSkillType[])Enum.GetValues(typeof(TSkillType)))
				.Select(v => new DBRPGSkill<TSkillType>(v, v.ToString(), String.Empty))
				.ToArray()
			);

			modelBuilder.Entity<DBRPGCharacterSkillKnown<TSkillType>>(builder =>
			{
				builder.HasKey(m => new {m.CharacterId, m.SkillId});
				builder.HasIndex(m => m.CharacterId);
				builder.HasIndex(m => m.SkillId);

				//Manual foreign key (without nav prop to char for simplified Type)
				builder.HasOne<DBRPGCharacter>()
					.WithMany()
					.HasForeignKey(c => c.CharacterId);
			});

			modelBuilder.Entity<DBRPGCharacterSkillLevel<TSkillType>>(builder =>
			{
				builder.HasKey(m => new { m.CharacterId, m.SkillId });
				builder.HasIndex(m => m.CharacterId);
				builder.HasIndex(m => m.SkillId);

				//Manual foreign key (without nav prop to char for simplified Type)
				builder.HasOne<DBRPGCharacter>()
					.WithMany()
					.HasForeignKey(c => c.CharacterId);
			});

			modelBuilder.Entity<DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>>(builder =>
			{
				builder.OwnsMany(m => m.Stats, m =>
				{
					m.WithOwner()
						.HasForeignKey(nameof(DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>.Class), nameof(DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>.Race), nameof(DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>.Level));

					m.HasIndex(nameof(DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>.Class),
							nameof(DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>.Race),
							nameof(DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>.Level),
							nameof(RPGStatDefinition<TStatType>.Id))
						.IsUnique();
				});

				builder.HasKey(m => new {m.Level, m.RaceId, m.ClassId});
			});

			modelBuilder.Entity<RPGStatDefinition<TStatType>>(builder =>
			{
				//Adds FK to RPGStatDef to DBRPGStat
				builder.HasOne<DBRPGStat<TStatType>>()
					.WithOne()
					.HasForeignKey<RPGStatDefinition<TStatType>>(s => s.Id)
					.IsRequired();
			});

			//Seed the DB with the available enum entries.
			modelBuilder.Entity<DBRPGStat<TStatType>>().HasData(
				((TStatType[])Enum.GetValues(typeof(TStatType)))
				.Select(v => new DBRPGStat<TStatType>(v, v.ToString(), String.Empty))
				.ToArray());
		}
	}
}
