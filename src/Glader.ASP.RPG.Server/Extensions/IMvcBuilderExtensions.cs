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
		public static IMvcBuilder RegisterRPGControllers(this IMvcBuilder builder, Func<RPGOptionsBuilder, RPGOptionsBuilder> rpgOptionsBuilder)
		{
			if(builder == null) throw new ArgumentNullException(nameof(builder));
			var rpgOptions = rpgOptionsBuilder(RPGOptionsBuilder.CreateDefault());
			return RegisterRPGControllers(builder, rpgOptions);
		}

		/// <summary>
		/// Registers the Glader.ASP.RPG controllers with the MVC system.
		/// See controller documentation for what it does and how it works.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="rpgOptions"></param>
		/// <returns></returns>
		public static IMvcBuilder RegisterRPGControllers(this IMvcBuilder builder, RPGOptionsBuilder rpgOptions)
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));
			if (rpgOptions == null) throw new ArgumentNullException(nameof(rpgOptions));

			typeof(IMvcBuilderExtensions)
				.GetMethod(nameof(RegisterRPGControllersGeneric), BindingFlags.Static | BindingFlags.NonPublic)
				.MakeGenericMethod(rpgOptions.BuildTypeParameters())
				.Invoke(null, new object[] { builder, rpgOptions });

			return builder.RegisterController<GroupController>();
		}

		internal static void RegisterRPGControllersGeneric<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType, TItemClassType, TQualityType, TQualityColorStructureType>(IMvcBuilder builder, RPGOptionsBuilder rpgOptions)
			where TCustomizableSlotType : Enum
			where TProportionSlotType : Enum
			where TRaceType : Enum
			where TClassType : Enum
			where TSkillType : Enum
			where TStatType : Enum
			where TItemClassType : Enum
			where TQualityType : Enum
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));
			if (rpgOptions == null) throw new ArgumentNullException(nameof(rpgOptions));

			builder.RegisterController<CharacterDataController<TRaceType, TClassType>>()
				.RegisterController<CharacterAppearanceController<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType>>();
		}
	}
}
