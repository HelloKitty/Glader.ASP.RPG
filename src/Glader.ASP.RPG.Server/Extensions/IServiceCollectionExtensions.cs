using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Text;
using Glader.Essentials;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Glader.ASP.RPG
{
	//<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType>
	public record RPGDatabaseContextOptionsBuilder(Type[] CustomizationTypes, Type[] ProportionTypes, Type RaceType, Type ClassType, Type SkillType, Type StatType)
	{
		public enum UninitializedRPGEnum : int
		{

		}

		public bool RegisterAsNonGenericDBContext { get; init; } = false;

		//<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType>
		internal Type[] BuildTypeParameters()
		{
			return new Type[] {CustomizationTypes[0], CustomizationTypes[1], ProportionTypes[0], ProportionTypes[1], RaceType, ClassType, SkillType, StatType};
		}

		public RPGDatabaseContextOptionsBuilder WithCustomizationType<TCustomizableSlotType, TColorStructureType>()
			where TCustomizableSlotType : Enum
		{
			return this with {CustomizationTypes = new[] {typeof(TCustomizableSlotType), typeof(TColorStructureType)}};
		}

		public RPGDatabaseContextOptionsBuilder WithProportionType<TProportionSlotType, TProportionStructureType>()
			where TProportionSlotType : Enum
		{
			return this with { ProportionTypes = new[] { typeof(TProportionSlotType), typeof(TProportionStructureType) } };
		}

		public RPGDatabaseContextOptionsBuilder WithRaceType<TRaceType>()
			where TRaceType : Enum
		{
			return this with { RaceType = typeof(TRaceType) };
		}

		public RPGDatabaseContextOptionsBuilder WithClassType<TClassType>()
			where TClassType : Enum
		{
			return this with { ClassType = typeof(TClassType) };
		}

		public RPGDatabaseContextOptionsBuilder WithSkillType<TSkillType>()
			where TSkillType : Enum
		{
			return this with { SkillType = typeof(TSkillType) };
		}

		public RPGDatabaseContextOptionsBuilder WithStatType<TStatType>()
			where TStatType : Enum
		{
			return this with { StatType = typeof(TStatType) };
		}

	}

	public static class IServiceCollectionExtensions
	{
		/// <summary>
		/// Registers a <see cref="RPGCharacterDatabaseContext{TCustomizableSlotType,TColorStructureType,TProportionSlotType,TProportionStructureType,TRaceType,TClassType,TSkillType,TStatType}"/> and <see cref="IRPGCharacterRepository{TRaceType,TClassType}"/>
		/// in the provided <see cref="services"/>.
		/// </summary>
		/// <param name="services">Service container.</param>
		/// <param name="optionsAction">The DB context options action.</param>
		/// <param name="rpgDatabaseOptionsBuilder"></param>
		/// <returns></returns>
		public static IServiceCollection RegisterCharacterDatabase(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction, Func<RPGDatabaseContextOptionsBuilder, RPGDatabaseContextOptionsBuilder> rpgDatabaseOptionsBuilder)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (optionsAction == null) throw new ArgumentNullException(nameof(optionsAction));
			if (rpgDatabaseOptionsBuilder == null) throw new ArgumentNullException(nameof(rpgDatabaseOptionsBuilder));

			//We build the default parameters
			var rpgOptions = new RPGDatabaseContextOptionsBuilder(
				ProportionTypes: new Type[2] {typeof(RPGDatabaseContextOptionsBuilder.UninitializedRPGEnum), typeof(Vector2<float>)},
				CustomizationTypes: new Type[2] {typeof(RPGDatabaseContextOptionsBuilder.UninitializedRPGEnum), typeof(Vector3<byte>)},
				RaceType: typeof(RPGDatabaseContextOptionsBuilder.UninitializedRPGEnum),
				ClassType: typeof(RPGDatabaseContextOptionsBuilder.UninitializedRPGEnum),
				SkillType: typeof(RPGDatabaseContextOptionsBuilder.UninitializedRPGEnum),
				StatType: typeof(RPGDatabaseContextOptionsBuilder.UninitializedRPGEnum));

			rpgOptions = rpgDatabaseOptionsBuilder(rpgOptions);
			var dbContextType = typeof(RPGCharacterDatabaseContext<,,,,,,,>).MakeGenericType(rpgOptions.BuildTypeParameters());

			//services.AddDbContext<RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType>>(optionsAction);
			typeof(IServiceCollectionExtensions)
				.GetMethod(nameof(AddDbContext), BindingFlags.Static | BindingFlags.NonPublic)
				.MakeGenericMethod(dbContextType)
				.Invoke(null, new object[] {services, optionsAction});

			if(rpgOptions.RegisterAsNonGenericDBContext)
				services.AddTransient<DbContext>(provider => (DbContext)provider.GetService(dbContextType));

			//Registered for consumers of non-generic context
			services.AddTransient(typeof(IRPGDBContext), typeof(DefaultCharacterDatabaseContextAdapter<,,,,,,,>).MakeGenericType(rpgOptions.BuildTypeParameters()));

			//DefaultServiceEndpointRepository : IServiceEndpointRepository
			services.AddTransient(typeof(IRPGCharacterRepository<,>).MakeGenericType(rpgOptions.RaceType, rpgOptions.ClassType), typeof(DefaultRPGCharacterRepository<,>).MakeGenericType(rpgOptions.RaceType, rpgOptions.ClassType));

			//DefaultRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> : IRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>
			//IRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>
			//DefaultRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType>
			services.AddTransient(typeof(IRPGCharacterAppearanceRepository<,,,>).MakeGenericType(rpgOptions.CustomizationTypes[0], rpgOptions.CustomizationTypes[1], rpgOptions.ProportionTypes[0], rpgOptions.ProportionTypes[1]), typeof(DefaultRPGCharacterAppearanceRepository<,,,,,,,>).MakeGenericType(rpgOptions.BuildTypeParameters()));

			//DefaultRPGGroupRepository
			services.AddTransient<IRPGGroupRepository, DefaultRPGGroupRepository>();

			//Example:
			//services.AddDbContext<ServiceDiscoveryDatabaseContext>(builder => { builder.UseMySql("server=127.0.0.1;port=3306;Database=guardians.global;Uid=root;Pwd=test;"); });
			return services;
		}

		internal static void AddDbContext<TContextType>(IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
			where TContextType : DbContext
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddDbContext<TContextType>(optionsAction);
		}
	}
}
