//====================================================================================================
// IGetGlobalBounds.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Graphics;

namespace nk.Carcassonne {
	public interface IDrawableObject : Drawable, ICursorChangeable, IPosition {
		public bool Show           { get; set; }
		public bool Ignore         { get; set; }

		public FloatRect GetGlobalBounds();
	}
}
