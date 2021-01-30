using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Non-generic <see cref="RPGCharacterDatabaseContext"/> <see cref="IDBContextAdapter{TDBContextType}"/> adapter implementation.
	/// </summary>
	/// <typeparam name="TCustomizableSlotType"></typeparam>
	/// <typeparam name="TColorStructureType"></typeparam>
	/// <typeparam name="TProportionSlotType"></typeparam>
	/// <typeparam name="TProportionStructureType"></typeparam>
	/// <typeparam name="TRaceType"></typeparam>
	/// <typeparam name="TClassType"></typeparam>
	/// <typeparam name="TSkillType"></typeparam>
	internal class NonGenericCharacterDatabaseContextAdapter<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType> 
		: IDBContextAdapter<RPGCharacterDatabaseContext>
		where TCustomizableSlotType : Enum 
		where TProportionSlotType : Enum
		where TRaceType : Enum
		where TClassType : Enum
		where TSkillType : Enum
	{
		private RPGCharacterDatabaseContext<TRaceType, TClassType> _Context { get; }

		/// <inheritdoc />
		public RPGCharacterDatabaseContext Context => _Context;

		public NonGenericCharacterDatabaseContextAdapter(RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType, TRaceType, TClassType, TSkillType> context)
		{
			_Context = context ?? throw new ArgumentNullException(nameof(context));
		}
	}
}
