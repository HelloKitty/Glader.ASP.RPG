using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Glader.Essentials;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Glader.ASP.RPG
{
	public static class IMvcBuilderExtensions
	{
		/// <summary>
		/// Registers the Glader.ASP.RPG controllers with the MVC system.
		/// See controller documentation for what it does and how it works.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="rpgOptionsBuilder"></param>
		/// <returns></returns>
		public static IMvcBuilder RegisterCharacterDataController(this IMvcBuilder builder, Func<RPGOptionsBuilder, RPGOptionsBuilder> rpgOptionsBuilder)
		{
			if(builder == null) throw new ArgumentNullException(nameof(builder));

			var rpgOptions = rpgOptionsBuilder(RPGOptionsBuilder.CreateDefault());
			return RegisterCharacterDataController(builder, rpgOptions);
		}

		/// <summary>
		/// Registers the Glader.ASP.RPG controllers with the MVC system.
		/// See controller documentation for what it does and how it works.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="rpgOptions"></param>
		/// <returns></returns>
		public static IMvcBuilder RegisterCharacterDataController(this IMvcBuilder builder, RPGOptionsBuilder rpgOptions)
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));
			if (rpgOptions == null) throw new ArgumentNullException(nameof(rpgOptions));

			RegisterControllerNonGeneric(builder, rpgOptions, typeof(CharacterDataController<,>).MakeGenericType(rpgOptions.RaceType, rpgOptions.ClassType));

			//CharacterAppearanceController<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType>
			RegisterControllerNonGeneric(builder, rpgOptions, typeof(CharacterAppearanceController<,,,,,>).MakeGenericType(rpgOptions.CustomizationTypes[0], rpgOptions.CustomizationTypes[1], rpgOptions.ProportionTypes[0], rpgOptions.ProportionTypes[1], rpgOptions.RaceType, rpgOptions.ClassType));


			return builder.RegisterController<GroupController>();
		}

		internal static void RegisterControllerNonGeneric(IMvcBuilder builder, RPGOptionsBuilder rpgOptions, Type controllerType)
		{
			typeof(IMvcBuilderExtensions)
				.GetMethod(nameof(RegisterController), BindingFlags.NonPublic | BindingFlags.Static)
				.MakeGenericMethod(controllerType)
				.Invoke(null, new[] {builder});
		}

		internal static IMvcBuilder RegisterController<TControllerType>(IMvcBuilder builder)
			where TControllerType : Controller
		{
			return builder.RegisterController<TControllerType>();
		}
	}
}
