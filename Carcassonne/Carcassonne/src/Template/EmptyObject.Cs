//====================================================================================================
// EmptyObject.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.System;
using SFML.Graphics;

namespace nk.Carcassonne {
	public class EmptyObject : IDrawableObject {
		public bool     Show           { get; set; } = true;
		public bool     Ignore         { get; set; } = true;
		public bool     CursorChanging { get; set; } = false;
		public Vector2f Position       { get; set; } = new(0, 0);

		FloatRect IDrawableObject.GetGlobalBounds() => new(0, 0, 0, 0);
		void Drawable.Draw(RenderTarget _target, RenderStates _states) {}
	}
}