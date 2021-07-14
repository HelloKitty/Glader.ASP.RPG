using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPG
{
	public enum ItemInstanceOwnershipType
	{
		CharacterInventory = 1,
	}

	/// <summary>
	/// Table that controls the ownership of <see cref="DBRPGItemInstance{TItemClassType,TQualityType,TQualityColorStructureType}"/>s to prevent errors
	/// of multiple ownerships from several other tables.
	/// </summary>
	[Table("item_instance_ownership")]
	public class DBRPGItemInstanceOwnership<TItemClassType, TQualityType, TQualityColorStructureType> : IRPGDBCreationDetailable
		where TItemClassType : Enum 
		where TQualityType : Enum
	{
		/// <summary>
		/// Primary key for the ownership as well as the foreign key id for the <see cref="Instance"/>.
		/// </summary>
		[Key]
		public int Id { get; private set; }
		
		[Required]
		[ForeignKey(nameof(Id))]
		public virtual DBRPGItemInstance<TItemClassType, TQualityType, TQualityColorStructureType> Instance { get; private set; }

		/// <summary>
		/// Indicates which type or table is mapped to this ownership instance.
		/// </summary>
		[Required]
		public ItemInstanceOwnershipType OwnershipType { get; private set; }

		/// <summary>
		/// The creation data of the character.
		/// </summary>
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreationDate { get; private set; }

		public DBRPGItemInstanceOwnership(int instanceId, ItemInstanceOwnershipType ownershipType)
		{
			if (instanceId <= 0) throw new ArgumentOutOfRangeException(nameof(instanceId));
			if (!Enum.IsDefined(typeof(ItemInstanceOwnershipType), ownershipType)) throw new InvalidEnumArgumentException(nameof(ownershipType), (int) ownershipType, typeof(ItemInstanceOwnershipType));
			Id = instanceId;
			OwnershipType = ownershipType;
		}

		/// <summary>
		/// EF Serializer
		/// </summary>
		public DBRPGItemInstanceOwnership()
		{

		}
	}
}
