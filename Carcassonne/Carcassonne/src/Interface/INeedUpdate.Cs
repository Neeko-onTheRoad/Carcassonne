//====================================================================================================
// INeedUpdate.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

namespace nk.Carcassonne {
	public interface INeedUpdate {
		public bool Updating { get; set; }

		public void UpdateElement(ClientTime _ct);
	}
}