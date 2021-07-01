using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection;
using System.Text;
using GGDBF;
using Glader.Essentials;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Glader.ASP.RPG
{
	//<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType>
	public record RPGOptionsBuilder(Type[] CustomizationTypes, Type[] ProportionTypes, Type RaceType, Type ClassType, Type SkillType, Type StatType)
	{
		public enum UninitializedRPGEnum : int
		{

		}

		public bool RegisterAsNonGenericDBContext { get; init; } = false;

		public bool RegisterGGDBFSystem { get; init; } = true;

		//<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType>
		internal Type[] BuildTypeParameters()
		{
			return new Type[] {CustomizationTypes[0], CustomizationTypes[1], ProportionTypes[0], ProportionTypes[1], RaceType, ClassType, SkillType, StatType};
		}

		public RPGOptionsBuilder WithCustomizationType<TCustomizableSlotType, TColorStructureType>()
			where TCustomizableSlotType : Enum
		{
			return this with {CustomizationTypes = new[] {typeof(TCustomizableSlotType), typeof(TColorStructureType)}};
		}

		public RPGOptionsBuilder WithProportionType<TProportionSlotType, TProportionStructureType>()
			where TProportionSlotType : Enum
		{
			return this with { ProportionTypes = new[] { typeof(TProportionSlotType), typeof(TProportionStructureType) } };
		}

		public RPGOptionsBuilder WithRaceType<TRaceType>()
			where TRaceType : Enum
		{
			return this with { RaceType = typeof(TRaceType) };
		}

		public RPGOptionsBuilder WithClassType<TClassType>()
			where TClassType : Enum
		{
			return this with { ClassType = typeof(TClassType) };
		}

		public RPGOptionsBuilder WithSkillType<TSkillType>()
			where TSkillType : Enum
		{
			return this with { SkillType = typeof(TSkillType) };
		}

		public RPGOptionsBuilder WithStatType<TStatType>()
			where TStatType : Enum
		{
			return this with { StatType = typeof(TStatType) };
		}


		internal static RPGOptionsBuilder CreateDefault()
		{
			return new RPGOptionsBuilder(
				ProportionTypes: new Type[2] { typeof(RPGOptionsBuilder.UninitializedRPGEnum), typeof(Vector2<float>) },
				CustomizationTypes: new Type[2] { typeof(RPGOptionsBuilder.UninitializedRPGEnum), typeof(Vector3<byte>) },
				RaceType: typeof(RPGOptionsBuilder.UninitializedRPGEnum),
				ClassType: typeof(RPGOptionsBuilder.UninitializedRPGEnum),
				SkillType: typeof(RPGOptionsBuilder.UninitializedRPGEnum),
				StatType: typeof(RPGOptionsBuilder.UninitializedRPGEnum));
		}
	}

	public static class IServiceCollectionExtensions
	{
		/// <summary>
		/// Registers a <see cref="RPGCharacterDatabaseContext{TCustomizableSlotType,TColorStructureType,TProportionSlotType,TProportionStructureType,TRaceType,TClassType,TSkillType,TStatType}"/>
		/// and <see cref="IRPGCharacterRepository{TRaceType,TClassType}"/>
		/// and the Glader.ASP.Controllers through <see cref="IMvcBuilderExtensions"/>'s controller methods within
		/// the provided <see cref="services"/>.
		/// </summary>
		/// <param name="services">Service container.</param>
		/// <param name="optionsAction">The DB context options action.</param>
		/// <param name="rpgDatabaseOptionsBuilder"></param>
		/// <param name="mvcBuilder"></param>
		/// <returns></returns>
		public static IServiceCollection RegisterGladerRPGSystem(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction, Func<RPGOptionsBuilder, RPGOptionsBuilder> rpgDatabaseOptionsBuilder, IMvcBuilder mvcBuilder)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (optionsAction == null) throw new ArgumentNullException(nameof(optionsAction));
			if (rpgDatabaseOptionsBuilder == null) throw new ArgumentNullException(nameof(rpgDatabaseOptionsBuilder));
			if (mvcBuilder == null) throw new ArgumentNullException(nameof(mvcBuilder));

			//We build the default parameters
			var rpgOptions = rpgDatabaseOptionsBuilder(RPGOptionsBuilder.CreateDefault());
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

			mvcBuilder.RegisterCharacterDataController(rpgOptions);

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

		public static IServiceCollection RegisterGladerRPGGGDBF<TGGDBFDataSourceType, TGGDBFConverterType>(this IServiceCollection services, Func<RPGOptionsBuilder, RPGOptionsBuilder> rpgDatabaseOptionsBuilder, IMvcBuilder mvcBuilder)
			where TGGDBFDataSourceType : class, IGGDBFDataSource
			where TGGDBFConverterType : class, IGGDBFDataConverter
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (mvcBuilder == null) throw new ArgumentNullException(nameof(mvcBuilder));

			//RPGStaticDataContext<TestSkillType, TestRaceType, TestClassType, TestProportionSlotType, TestCustomizationSlotType, TestStatType>
			var rpgOptions = rpgDatabaseOptionsBuilder(RPGOptionsBuilder.CreateDefault());

			//services.RegisterGGDBFContentServices<EntityFrameworkGGDBFDataSource, AutoMapperGGDBFDataConverter, RPGStaticDataContext<TestSkillType, TestRaceType, TestClassType, TestProportionSlotType, TestCustomizationSlotType, TestStatType>>();
			Type contextType = typeof(RPGStaticDataContext<,,,,,>).MakeGenericType(rpgOptions.SkillType, rpgOptions.RaceType, rpgOptions.ClassType, rpgOptions.ProportionTypes[0], rpgOptions.CustomizationTypes[0], rpgOptions.StatType);
			typeof(IServiceCollectionExtensions)
				.GetMethod(nameof(RegisterGGDBFContentServices), BindingFlags.Static | BindingFlags.NonPublic)
				.MakeGenericMethod(new Type[]{ typeof(TGGDBFDataSourceType), typeof(TGGDBFConverterType), contextType })
				.Invoke(null, new object[] { services });

			mvcBuilder.RegisterGGDBFController()
				.AddNewtonsoftJson(options =>
				{
					options.RegisterGGDBFSerializers();
				});

			return services;
		}

		internal static void RegisterGGDBFContentServices<TGGDBFDataSourceType, TGGDBFConverterType, TContextType>(IServiceCollection services)
			where TGGDBFDataSourceType : class, IGGDBFDataSource
			where TGGDBFConverterType : class, IGGDBFDataConverter
			where TContextType : class, IGGDBFContext
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.RegisterGGDBFContentServices<TGGDBFDataSourceType, TGGDBFConverterType, TContextType>();
		}
	}
}
