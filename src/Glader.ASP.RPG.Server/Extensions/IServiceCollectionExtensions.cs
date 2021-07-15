using System;
using System.Collections.Generic;
using System.Linq;
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
	public record RPGOptionsBuilder(Type[] CustomizationTypes, Type[] ProportionTypes, Type RaceType, Type ClassType, Type SkillType, Type StatType, Type ItemClassType, Type[] QualityTypes)
	{
		public enum UninitializedRPGEnum : int
		{

		}

		public bool RegisterAsNonGenericDBContext { get; init; } = false;

		//<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType>
		internal Type[] BuildTypeParameters()
		{
			return new Type[] {CustomizationTypes[0], CustomizationTypes[1], ProportionTypes[0], ProportionTypes[1], RaceType, ClassType, SkillType, StatType, ItemClassType, QualityTypes[0], QualityTypes[1]};
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

		public RPGOptionsBuilder WithItemClassType<TItemClassType>()
			where TItemClassType : Enum
		{
			return this with { ItemClassType = typeof(TItemClassType) };
		}

		public RPGOptionsBuilder WithQualityType<TQualityType, TQualityColorStructureType>()
			where TQualityType : Enum
		{
			return this with { QualityTypes = new[] { typeof(TQualityType), typeof(TQualityColorStructureType) } };
		}


		internal static RPGOptionsBuilder CreateDefault()
		{
			return new RPGOptionsBuilder(
				ProportionTypes: new Type[2] { typeof(RPGOptionsBuilder.UninitializedRPGEnum), typeof(Vector2<float>) },
				CustomizationTypes: new Type[2] { typeof(RPGOptionsBuilder.UninitializedRPGEnum), typeof(Vector3<byte>) },
				RaceType: typeof(RPGOptionsBuilder.UninitializedRPGEnum),
				ClassType: typeof(RPGOptionsBuilder.UninitializedRPGEnum),
				SkillType: typeof(RPGOptionsBuilder.UninitializedRPGEnum),
				StatType: typeof(RPGOptionsBuilder.UninitializedRPGEnum),
				ItemClassType: typeof(RPGOptionsBuilder.UninitializedRPGEnum),
				QualityTypes: new Type[2] { typeof(RPGOptionsBuilder.UninitializedRPGEnum), typeof(Vector3<byte>) });
		}
	}

	public static class IServiceCollectionExtensions
	{
		/// <summary>
		/// Registers a <see cref="RPGCharacterDatabaseContext{TCustomizableSlotType,TColorStructureType,TProportionSlotType,TProportionStructureType,TRaceType,TClassType,TSkillType,TStatType,TItemClassType,TQualityType,TQualityColorStructureType}"/>
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

			typeof(IServiceCollectionExtensions)
				.GetMethod(nameof(RegisterGladerRPGSystemGeneric), BindingFlags.Static | BindingFlags.NonPublic)
				.MakeGenericMethod(rpgOptions.BuildTypeParameters())
				.Invoke(null, new object[] { services, optionsAction, rpgOptions, mvcBuilder });

			//Example:
			//services.AddDbContext<ServiceDiscoveryDatabaseContext>(builder => { builder.UseMySql("server=127.0.0.1;port=3306;Database=guardians.global;Uid=root;Pwd=test;"); });
			return services;
		}

		//Copy the type parameters from: RPGCharacterDatabaseContext
		internal static void RegisterGladerRPGSystemGeneric<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>(IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction, RPGOptionsBuilder rpgOptions, IMvcBuilder mvcBuilder)
			where TCustomizableSlotType : Enum
			where TProportionSlotType : Enum
			where TRaceType : Enum
			where TClassType : Enum
			where TSkillType : Enum
			where TStatType : Enum
			where TItemClassType : Enum
			where TQualityType : Enum
		{
			AddDbContext<RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>>(services, optionsAction);

			if(rpgOptions.RegisterAsNonGenericDBContext)
				services.AddTransient<DbContext>(provider => (DbContext)provider.GetService< RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>>());

			//Registered for consumers of non-generic context
			services.AddTransient<IRPGDBContext, DefaultCharacterDatabaseContextAdapter<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>>();

			//DefaultServiceEndpointRepository : IServiceEndpointRepository
			services.AddTransient<IRPGCharacterRepository<TRaceType, TClassType>, DefaultRPGCharacterRepository<TRaceType, TClassType>>();

			//DefaultRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> : IRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>
			//IRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>
			//DefaultRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType>
			services.AddTransient<IRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>, DefaultRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>>();

			//DefaultRPGGroupRepository
			services.AddTransient<IRPGGroupRepository, DefaultRPGGroupRepository>();
			services.AddTransient<IRPGItemInstanceRepository<TItemClassType, TQualityType, TQualityColorStructureType>, DefaultRPGItemInstanceRepository<TItemClassType, TQualityType, TQualityColorStructureType>>();
			services.AddTransient<IRPGCharacterItemInventoryRepository<TItemClassType, TQualityType, TQualityColorStructureType>, DefaultRPGCharacterItemInventoryRepository<TItemClassType, TQualityType, TQualityColorStructureType>>();

			IMvcBuilderExtensions.RegisterRPGControllersGeneric<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>(mvcBuilder, rpgOptions);
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

			typeof(IServiceCollectionExtensions)
				.GetMethod(nameof(RegisterGladerRPGGGDBFGeneric), BindingFlags.Static | BindingFlags.NonPublic)
				.MakeGenericMethod(new Type[]{ typeof(TGGDBFDataSourceType), typeof(TGGDBFConverterType) }.Concat(rpgOptions.BuildTypeParameters()).ToArray())
				.Invoke(null, new object[] { services, rpgOptions, mvcBuilder });

			mvcBuilder.RegisterGGDBFController()
				.AddNewtonsoftJson(options =>
				{
					options.RegisterGGDBFSerializers();
				});

			return services;
		}

		//Copy the type parameters from: RPGCharacterDatabaseContext (keep the first 2 parameters)
		internal static void RegisterGladerRPGGGDBFGeneric<TGGDBFDataSourceType, TGGDBFConverterType,
			TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>(IServiceCollection services, RPGOptionsBuilder rpgOptions, IMvcBuilder mvcBuilder)
			where TGGDBFDataSourceType : class, IGGDBFDataSource
			where TGGDBFConverterType : class, IGGDBFDataConverter
			where TSkillType : Enum
			where TRaceType : Enum
			where TClassType : Enum
			where TCustomizableSlotType : Enum
			where TProportionSlotType : Enum
			where TStatType : Enum
			where TItemClassType : Enum
			where TQualityType : Enum
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (rpgOptions == null) throw new ArgumentNullException(nameof(rpgOptions));
			if (mvcBuilder == null) throw new ArgumentNullException(nameof(mvcBuilder));

			RegisterGGDBFContentServices<TGGDBFDataSourceType, TGGDBFConverterType, RPGStaticDataContext<TSkillType, TRaceType, TClassType, TProportionSlotType, TCustomizableSlotType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>>(services);
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
