using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Glader.ASP.RPG
{
	public static class IServiceCollectionExtensions
	{
		/// <summary>
		/// Registers a <see cref="RPGCharacterDatabaseContext"/> and <see cref="IRPGCharacterRepository{TRaceType,TClassType}"/>
		/// in the provided <see cref="services"/>.
		/// </summary>
		/// <param name="services">Service container.</param>
		/// <param name="optionsAction">The DB context options action.</param>
		/// <returns></returns>
		public static IServiceCollection RegisterCharacterDatabase<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
			where TCustomizableSlotType : Enum 
			where TProportionSlotType : Enum
			where TRaceType : Enum
			where TClassType : Enum
			where TSkillType : Enum
			where TStatType : Enum
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (optionsAction == null) throw new ArgumentNullException(nameof(optionsAction));

			services.AddDbContext<RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType>>(optionsAction);

			//Registered for consumers of non-generic context
			services.AddTransient<IDBContextAdapter<RPGCharacterDatabaseContext>, NonGenericCharacterDatabaseContextAdapter<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType>>();
			services.AddTransient<IDBContextAdapter<RPGCharacterDatabaseContext<TRaceType, TClassType>>, GenericCharacterDatabaseContextAdapter<RPGCharacterDatabaseContext<TRaceType, TClassType>, RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType>>>();

			//DefaultServiceEndpointRepository : IServiceEndpointRepository
			services.AddTransient<IRPGCharacterRepository<TRaceType, TClassType>, DefaultRPGCharacterRepository<TRaceType, TClassType>>();
			//DefaultRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> : IRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> 
			services.AddTransient<IRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType>, DefaultRPGCharacterAppearanceRepository<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType>>();

			//DefaultRPGGroupRepository
			services.AddTransient<IRPGGroupRepository, DefaultRPGGroupRepository>();

			//Example:
			//services.AddDbContext<ServiceDiscoveryDatabaseContext>(builder => { builder.UseMySql("server=127.0.0.1;port=3306;Database=guardians.global;Uid=root;Pwd=test;"); });
			return services;
		}
	}
}
