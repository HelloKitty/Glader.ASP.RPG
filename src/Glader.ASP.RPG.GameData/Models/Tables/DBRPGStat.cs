using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Table for RPG Stats.
	/// </summary>
	/// <typeparam name="TStatType">The stat type.</typeparam>
	[DataContract]
	[Table("stat")]
	public class DBRPGStat<TStatType> : IModelDescriptable
		where TStatType : Enum
	{
		/// <summary>
		/// The Stat identifier.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[DataMember(Order = 1)]
		public TStatType Id { get; private set; }

		/// <summary>
		/// The visual human-readable name for the race.
		/// </summary>
		[DataMember(Order = 2)]
		public string VisualName { get; private set; }

		/// <summary>
		/// The description of the race.
		/// </summary>
		[DataMember(Order = 3)]
		public string Description { get; private set; }

		public DBRPGStat(TStatType statType, string visualName, string description)
		{
			Id = statType ?? throw new ArgumentNullException(nameof(statType));
			VisualName = visualName;
			Description = description;
		}

		public DBRPGStat(TStatType classType)
		{
			Id = classType ?? throw new ArgumentNullException(nameof(classType));
			VisualName = String.Empty;
			Description = String.Empty;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		public DBRPGStat()
		{

		}
	}
}
