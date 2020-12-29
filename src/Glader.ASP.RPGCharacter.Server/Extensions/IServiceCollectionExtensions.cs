using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Glader.ASP.RPGCharacter
{
	public static class IServiceCollectionExtensions
	{
		/// <summary>
		/// Registers a <see cref="RPGCharacterDatabaseContext"/> and <see cref="IRPGCharacterRepository"/>
		/// in the provided <see cref="services"/>.
		/// </summary>
		/// <param name="services">Service container.</param>
		/// <param name="optionsAction">The DB context options action.</param>
		/// <returns></returns>
		public static IServiceCollection RegisterCharacterDatabase(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			if (optionsAction == null) throw new ArgumentNullException(nameof(optionsAction));

			//DefaultServiceEndpointRepository : IServiceEndpointRepository
			services.AddTransient<IRPGCharacterRepository, DefaultRPGCharacterRepository>();
			services.AddDbContext<RPGCharacterDatabaseContext>(optionsAction);

			//Example:
			//services.AddDbContext<ServiceDiscoveryDatabaseContext>(builder => { builder.UseMySql("server=127.0.0.1;port=3306;Database=guardians.global;Uid=root;Pwd=test;"); });
			return services;
		}
	}
}
