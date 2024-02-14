//====================================================================================================
// Meeple.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

namespace nk.Carcassonne {
	public struct Meeples() {

		public byte Small   { get; set; } = 8;
		public bool Large   { get; set; } = true;
		public bool Monk    { get; set; } = true;
		public bool Builder { get; set; } = true;
		public bool Pig     { get; set; } = true;

	}
}
