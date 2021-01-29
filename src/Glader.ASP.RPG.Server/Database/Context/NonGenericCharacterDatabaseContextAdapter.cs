using System;
using System.Collections.Generic;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Non-generic <see cref="RPGCharacterDatabaseContext"/> <see cref="IDBContextAdapter{TDBContextType}"/> adapter implementation.
	/// </summary>
	/// <typeparam name="TCustomizableSlotType"></typeparam>
	/// <typeparam name="TColorStructureType"></typeparam>
	/// <typeparam name="TProportionSlotType"></typeparam>
	/// <typeparam name="TProportionStructureType"></typeparam>
	internal class NonGenericCharacterDatabaseContextAdapter<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> : IDBContextAdapter<RPGCharacterDatabaseContext>
		where TCustomizableSlotType : Enum 
		where TProportionSlotType : Enum
	{
		/// <inheritdoc />
		public RPGCharacterDatabaseContext Context { get; }

		public NonGenericCharacterDatabaseContextAdapter(RPGCharacterDatabaseContext<TCustomizableSlotType, TColorStructureType, TProportionSlotType, TProportionStructureType> context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
		}
	}
}
