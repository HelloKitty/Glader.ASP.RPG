using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Glader.ASP.RPG
{
	[JsonObject]
	public sealed class RPGCharacterProgress : IRPGCharacterProgress
	{
		/// <inheritdoc />
		[JsonProperty]
		public int Experience { get; private set; }

		/// <inheritdoc />
		[JsonProperty]
		public int Level { get; private set; }

		/// <inheritdoc />
		[JsonProperty]
		public TimeSpan PlayTime { get; }

		public RPGCharacterProgress(int experience, int level, TimeSpan playTime)
		{
			Experience = experience;
			Level = level;
			PlayTime = playTime;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		public RPGCharacterProgress()
		{
			
		}
	}
}
