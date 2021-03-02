using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;

namespace Glader.ASP.RPG
{
	//TODO: Move this somewhere else.
	public class FullCharacterData<TRaceType, TClassType>
		where TRaceType : Enum
		where TClassType : Enum
	{
		public DBRPGCharacter Character { get; }

		public DBRPGCharacterDefinition<TRaceType, TClassType> Definition { get; }

		public FullCharacterData(DBRPGCharacter character, DBRPGCharacterDefinition<TRaceType, TClassType> definition)
		{
			Character = character ?? throw new ArgumentNullException(nameof(character));
			Definition = definition ?? throw new ArgumentNullException(nameof(definition));
		}
	}

	/// <summary>
	/// <see cref="IGenericRepositoryCrudable{TKey,TModel}"/> for <see cref="DBRPGCharacter"/>.
	/// </summary>
	public interface IRPGCharacterRepository<TRaceType, TClassType> : IGenericRepositoryCrudable<int, DBRPGCharacter> 
		where TRaceType : Enum 
		where TClassType : Enum
	{
		/// <summary>
		/// Retrieves owned characters associated with the <see cref="ownershipId"/>.
		/// </summary>
		/// <param name="ownershipId">Ownership id.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns>Array of all characters linked to the ownership id.</returns>
		Task<FullCharacterData<TRaceType, TClassType>[]> RetrieveOwnedCharactersAsync(int ownershipId, CancellationToken token = default);

		//TODO: Should we input the transit data model?
		/// <summary>
		/// Create a new character and associate it with an <see cref="DBRPGCharacterOwnership"/>.
		/// </summary>
		/// <param name="ownershipId">The ownership id.</param>
		/// <param name="name">The character name.</param>
		/// <param name="classType"></param>
		/// <param name="token">Cancel token.</param>
		/// <param name="race"></param>
		/// <returns>The DB character instance created.</returns>
		Task<DBRPGCharacter> CreateCharacterAsync(int ownershipId, string name, TRaceType race, TClassType classType, CancellationToken token = default);

		/// <summary>
		/// Indicates if a character is owned by a specified ownership id.
		/// </summary>
		/// <param name="ownershipId">The ownership id.</param>
		/// <param name="characterId">The character id.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns>True if the account owns the character.</returns>
		Task<bool> AccountOwnsCharacterAsync(int ownershipId, int characterId, CancellationToken token = default);

		//TODO: Finish doc.
		/// <summary>
		/// Similar to <see cref="RetrieveAsync"/> this will retrieve the <see cref="FullCharacterData{TRaceType,TClassType}"/> version
		/// of <see cref="DBRPGCharacter"/>
		/// </summary>
		/// <param name="id"></param>
		/// <param name="token"></param>
		/// <returns></returns>
		Task<FullCharacterData<TRaceType, TClassType>> RetrieveFullCharacterDataAsync(int id, CancellationToken token = default);
	}
}
