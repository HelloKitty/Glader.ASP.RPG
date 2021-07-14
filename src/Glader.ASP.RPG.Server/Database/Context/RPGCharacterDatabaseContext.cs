using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Glader.Essentials;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Glader.ASP.RPG
{
	public sealed class RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType> 
		: DbContext
		where TCustomizableSlotType : Enum
		where TProportionSlotType : Enum
		where TRaceType : Enum
		where TClassType : Enum
		where TSkillType : Enum
		where TStatType : Enum
		where TItemClassType : Enum
		where TQualityType : Enum
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

		public DbSet<DBRPGClass<TClassType>> Classes { get; set; }

		public DbSet<DBRPGRace<TRaceType>> Races { get; set; }

		public DbSet<DBRPGCharacterDefinition<TRaceType, TClassType>> CharacterDefinitions { get; set; }

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

		public DbSet<DBRPGItemClass<TItemClassType>> ItemClasses { get; set; }

		public DbSet<DBRPGSItemSubClass<TItemClassType>> ItemSubclasses { get; set; }

		public DbSet<DBRPGQuality<TQualityType, TQualityColorStructureType>> Qualities { get; private set; }

		public DbSet<DBRPGItemTemplate<TItemClassType, TQualityType, TQualityColorStructureType>> ItemTemplates { get; private set; }

		public DbSet<DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType>> ItemInstances { get; private set; }

		public DbSet<DBRPGItemInstanceOwnership<TItemClassType, TQualityType, TQualityColorStructureType>> ItemInstanceOwnerships { get; private set; }

		public DbSet<DBRPGCharacterItemInventory<TItemClassType, TQualityType, TQualityColorStructureType>> CharacterItemInventories { get; private set; }

		public RPGCharacterDatabaseContext(DbContextOptions<RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>> options)
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
				builder.HasKey(c => new { c.OwnershipId, c.CharacterId });
			});

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

			modelBuilder.Entity<DBRPGCharacterCustomizableSlot<TCustomizableSlotType, TColorStructureType>>(builder =>
			{
				builder.HasOne<DBRPGCharacter>()
					.WithMany()
					.HasForeignKey(c => c.CharacterId);

				builder.HasIndex(c => c.CharacterId);
				builder.HasKey(c => new {c.CharacterId, c.SlotType});

				builder.OwnsOneIfNeeded(c => c.SlotColor);
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

				builder.OwnsOneIfNeeded(c => c.Proportion);
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
					//WARNING: Make sure same order as the HasKey below: builder.HasKey(m => new {m.Level, m.RaceId, m.ClassId});
					m.WithOwner()
						.HasForeignKey(nameof(DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>.Level),
							nameof(DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>.Race),
							nameof(DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>.ClassId));

					//WARNING: Make sure same order as the HasKey below: builder.HasKey(m => new {m.Level, m.RaceId, m.ClassId});
					m.HasKey(nameof(DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>.Level),
						nameof(DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>.Race),
						nameof(DBRPGCharacterStatDefault<TStatType, TRaceType, TClassType>.ClassId),
						nameof(RPGStatValue<TStatType>.StatType));

					//Adds FK to RPGStatDef to DBRPGStat
					//Even though we have the prop we still NEED this because EF Core
					//migration guilder chokes on this.
					/*m.HasOne<DBRPGStat<TStatType>>(m => m.Stat)
						.WithMany()
						.HasForeignKey(definition => definition.StatType)
						.IsRequired();*/
				});

				builder.HasKey(m => new {m.Level, m.RaceId, m.ClassId});
			});

			modelBuilder.Owned<RPGStatValue<TStatType>>();

			//Seed the DB with the available enum entries.
			modelBuilder.Entity<DBRPGStat<TStatType>>().HasData(
				((TStatType[])Enum.GetValues(typeof(TStatType)))
				.Select(v => new DBRPGStat<TStatType>(v, v.ToString(), String.Empty))
				.ToArray());

			modelBuilder.Entity<DBRPGItemClass<TItemClassType>>(builder =>
			{
				builder.SeedWithEnum<DBRPGItemClass<TItemClassType>, TItemClassType>(m => new DBRPGItemClass<TItemClassType>(m, m.ToString(), string.Empty));
			});

			modelBuilder.Entity<DBRPGSItemSubClass<TItemClassType>>(builder =>
			{
				builder.HasKey(m => new {m.ItemClassId, m.SubClassId});
			});

			modelBuilder.Entity<DBRPGQuality<TQualityType, TQualityColorStructureType>>(builder =>
			{
				builder.SeedWithEnum<DBRPGQuality<TQualityType, TQualityColorStructureType>, TQualityType>(m => new DBRPGQuality<TQualityType, TQualityColorStructureType>(m, m.GetEnumDisplay()?.Name ?? m.ToString(), m.GetEnumDescription()?.Description ?? string.Empty));

				builder.OwnsOneIfNeeded(m => m.Color);
			});

			modelBuilder.Entity<DBRPGItemTemplate<TItemClassType, TQualityType, TQualityColorStructureType>>(builder =>
			{
				builder.HasOne(m => m.ItemSubClass)
					.WithMany()
					.HasForeignKey(m => new {m.ClassId, m.SubClassId});
			});

			modelBuilder.Entity<DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType>>(builder =>
			{
				builder.HasIndex(m => m.TemplateId);
			});

			modelBuilder.Entity<DBRPGCharacterItemInventory<TItemClassType, TQualityType, TQualityColorStructureType>>(builder =>
			{

			});
		}
	}
}
