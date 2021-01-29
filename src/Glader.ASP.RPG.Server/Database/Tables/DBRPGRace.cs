using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// Table for RPG Race.
	/// </summary>
	/// <typeparam name="TRaceType">The race type.</typeparam>
	[Table("race")]
	public class DBRPGRace<TRaceType> : IModelDescriptable
		where TRaceType : Enum
	{
		/// <summary>
		/// The Race identifier.
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public TRaceType Id { get; private set; }

		/// <summary>
		/// The visual human-readable name for the race.
		/// </summary>
		public string VisualName { get; private set; }

		/// <summary>
		/// The description of the race.
		/// </summary>
		public string Description { get; private set; }

		public DBRPGRace(TRaceType race, string visualName, string description)
		{
			Id = race ?? throw new ArgumentNullException(nameof(race));
			VisualName = visualName;
			Description = description;
		}

		public DBRPGRace(TRaceType race)
		{
			Id = race ?? throw new ArgumentNullException(nameof(race));
			VisualName = String.Empty;
			Description = String.Empty;
		}
	}
}
