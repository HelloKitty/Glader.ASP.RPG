using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Table for RPG Item Class.
	/// </summary>
	/// <typeparam name="TItemClassType">The itemclass type.</typeparam>
	[DataContract]
	[Table("item_class")]
	public class DBRPGItemClass<TItemClassType> : IModelDescriptable
		where TItemClassType : Enum
	{
		/// <summary>
		/// The Item Class identifier.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[DataMember(Order = 1)]
		public TItemClassType Id { get; private set; }

		/// <summary>
		/// The visual human-readable name for the item class.
		/// </summary>
		[DataMember(Order = 2)]
		public string VisualName { get; private set; }

		/// <summary>
		/// The description of the item class.
		/// </summary>
		[DataMember(Order = 3)]
		public string Description { get; private set; }

		[InverseProperty(nameof(DBRPGSubItemClass<TItemClassType>.ItemClass))]
		public virtual ICollection<DBRPGSubItemClass<TItemClassType>> SubClasses { get; private set; }

		public DBRPGItemClass(TItemClassType itemClass, string visualName, string description)
		{
			Id = itemClass ?? throw new ArgumentNullException(nameof(itemClass));
			VisualName = visualName;
			Description = description;
		}

		public DBRPGItemClass(TItemClassType itemClass)
		{
			Id = itemClass ?? throw new ArgumentNullException(nameof(itemClass));
			VisualName = String.Empty;
			Description = String.Empty;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGItemClass()
		{

		}
	}
}
