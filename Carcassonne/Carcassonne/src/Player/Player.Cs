//====================================================================================================
// Player.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

namespace nk.Carcassonne {
	public class Player {

		public uint    Score   { get; set; } = 0u;
		public Meeples Meeples { get; set; } = new();

		public byte    Code  { get; protected set; } = 0;
		public string  Name  { get; protected set; } = "Unnamed";
		public ColorSF Color { get; protected set; } = new();

	}
}
