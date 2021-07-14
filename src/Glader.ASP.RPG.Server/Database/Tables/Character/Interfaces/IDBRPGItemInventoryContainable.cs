using System;
using System.Collections.Generic;
using System.Text;

namespace Glader.ASP.RPG
{
	public interface IDBRPGItemInventoryContainable<TItemClassType, TQualityType, TQualityColorStructureType> 
		where TItemClassType : Enum 
		where TQualityType : Enum
	{
		/// <summary>
		/// The ownership identifier for <see cref="ItemOwnership"/> (may be identical to the item instance)
		/// </summary>
		public int OwnershipId { get; }

		/// <summary>
		/// The ownership type of the container.
		/// </summary>
		public ItemInstanceOwnershipType OwnershipType { get; }

		public DBRPGItemInstanceOwnership<TItemClassType, TQualityType, TQualityColorStructureType> ItemOwnership { get; }
	}
}
