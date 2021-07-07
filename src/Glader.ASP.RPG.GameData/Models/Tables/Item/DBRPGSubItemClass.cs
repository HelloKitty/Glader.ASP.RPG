using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using GGDBF;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Table for RPG Item SubClass.
	/// </summary>
	/// <typeparam name="TItemClassType">The subclass type.</typeparam>
	[DataContract]
	[CompositeKeyHint(nameof(ItemClassId), nameof(SubClassId))]
	[Table("item_sub_class")]
	public class DBRPGSubItemClass<TItemClassType> : IModelDescriptable
		where TItemClassType : Enum
	{
		/// <summary>
		/// Unique subclass id within the <see cref="ItemClassId"/> subset.
		/// </summary>
		[DataMember(Order = 1)]
		public int SubClassId { get; private set; }

		/// <summary>
		/// The Item Class identifier.
		/// </summary>
		[DataMember(Order = 2)]
		public TItemClassType ItemClassId { get; private set; }

		[IgnoreDataMember]
		[ForeignKey(nameof(ItemClassId))]
		public virtual DBRPGItemClass<TItemClassType> ItemClass { get; private set; }

		/// <summary>
		/// The visual human-readable name for the item class.
		/// </summary>
		[DataMember(Order = 3)]
		public string VisualName { get; private set; }

		/// <summary>
		/// The description of the item class.
		/// </summary>
		[DataMember(Order = 4)]
		public string Description { get; private set; }

		public DBRPGSubItemClass(int subClassId, TItemClassType itemClass, string visualName, string description)
		{
			if (subClassId <= 0) throw new ArgumentOutOfRangeException(nameof(subClassId));

			SubClassId = subClassId;
			ItemClassId = itemClass ?? throw new ArgumentNullException(nameof(itemClass));
			VisualName = visualName;
			Description = description;
		}

		public DBRPGSubItemClass(int subClassId, TItemClassType itemClass)
		{
			if (subClassId <= 0) throw new ArgumentOutOfRangeException(nameof(subClassId));

			SubClassId = subClassId;
			ItemClassId = itemClass ?? throw new ArgumentNullException(nameof(itemClass));
			VisualName = String.Empty;
			Description = String.Empty;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGSubItemClass()
		{

		}
	}
}
