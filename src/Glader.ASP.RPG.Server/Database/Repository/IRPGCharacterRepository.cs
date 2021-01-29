using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Glader.Essentials;

namespace Glader.ASP.RPG
{
	/// <summary>
	/// <see cref="IGenericRepositoryCrudable{TKey,TModel}"/> for <see cref="DBRPGCharacter"/>.
	/// </summary>
	public interface IRPGCharacterRepository : IGenericRepositoryCrudable<int, DBRPGCharacter>
	{
		/// <summary>
		/// Retrieves owned characters associated with the <see cref="ownershipId"/>.
		/// </summary>
		/// <param name="ownershipId">Ownership id.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns>Array of all characters linked to the ownership id.</returns>
		Task<DBRPGCharacter[]> RetrieveOwnedCharactersAsync(int ownershipId, CancellationToken token = default);

		//TODO: Should we input the transit data model?
		/// <summary>
		/// Create a new character and associate it with an <see cref="DBRPGCharacterOwnership"/>.
		/// </summary>
		/// <param name="ownershipId">The ownership id.</param>
		/// <param name="name">The character name.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns>The DB character instance created.</returns>
		Task<DBRPGCharacter> CreateCharacterAsync(int ownershipId, string name, CancellationToken token = default);

		/// <summary>
		/// Indicates if a character is owned by a specified ownership id.
		/// </summary>
		/// <param name="ownershipId">The ownership id.</param>
		/// <param name="characterId">The character id.</param>
		/// <param name="token">Cancel token.</param>
		/// <returns>True if the account owns the character.</returns>
		Task<bool> AccountOwnsCharacterAsync(int ownershipId, int characterId, CancellationToken token = default);
	}
}
