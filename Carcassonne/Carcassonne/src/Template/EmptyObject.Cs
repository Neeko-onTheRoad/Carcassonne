//====================================================================================================
// $itemrootname.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.System;
using SFML.Graphics;

namespace nk.Carcassonne {
	public class EmptyObject : IDrawableObject {
		public bool     Show           { get; set; }
		public bool     Ignore         { get; set; }
		public bool     CursorChanging { get; set; }
		public Vector2f Position       { get; set; }

		FloatRect IDrawableObject.GetGlobalBounds() => new(0, 0, 0, 0);
		void Drawable.Draw(RenderTarget _target, RenderStates _states) {}
	}
}
