using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Default <see cref="IRPGDBContext"/> implementation.
	/// </summary>
	/// <typeparam name="TCustomizableSlotType"></typeparam>
	/// <typeparam name="TColorStructureType"></typeparam>
	/// <typeparam name="TProportionSlotType"></typeparam>
	/// <typeparam name="TProportionStructureType"></typeparam>
	/// <typeparam name="TRaceType"></typeparam>
	/// <typeparam name="TClassType"></typeparam>
	/// <typeparam name="TSkillType"></typeparam>
	/// <typeparam name="TStatType"></typeparam>
	internal class DefaultCharacterDatabaseContextAdapter<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType> 
		: IRPGDBContext
		where TCustomizableSlotType : Enum 
		where TProportionSlotType : Enum
		where TRaceType : Enum
		where TClassType : Enum
		where TSkillType : Enum
		where TStatType : Enum
	{
		/// <inheritdoc />
		public DbContext Context { get; }

		public DefaultCharacterDatabaseContextAdapter(RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType, TStatType> context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
		}
	}
}
