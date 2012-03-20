using System;

namespace KangaModeling.Compiler.SequenceDiagrams.Model
{
	/// <summary>
	/// A Participant participates in a sequence diagram.
	/// It defines its own lifeline, can call other participants, and can receive calls.
	/// </summary>
	public class Participant
	{
		/// <summary>
		/// Initialize a new Participant instance and sets its fields.
		/// </summary>
		/// <param name="name">The name of the participant. Must be non-empty.</param>
		public Participant (String name)
		{
		}
		
		/// <summary>
		/// The name of this participant.
		/// </summary>
		public String Name { get; private set; }
		
	}
	
}
