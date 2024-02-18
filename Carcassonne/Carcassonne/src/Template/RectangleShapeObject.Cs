//====================================================================================================
// $itemrootname.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using nk.Carcassonne;
using SFML.Graphics;

namespace nk {
	public class RectangleShapeObject : RectangleShape, IDrawableObject {
		public bool Show   { get; set; } = true;
		public bool Ignore { get; set; } = false;

		public new void Draw(RenderTarget _target, RenderStates _state) {
			if (!Show) return;
			base.Draw(_target, _state);
		}
	}
}
