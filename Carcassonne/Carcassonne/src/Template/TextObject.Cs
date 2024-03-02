//====================================================================================================
// TextObject.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Graphics;

namespace nk.Carcassonne {
	public class TextObject : Text, IDrawableObject {
		public bool Show           { get; set; } = true;
		public bool Ignore         { get; set; } = false;
		public bool CursorChanging { get; set; } = false;

		public TextObject(string? _displayString = null, uint? _charicterSize = null, FontSF? _font = null) : base() {
			Font            = _font          ?? Res.Fonts.Normal;
			DisplayedString = _displayString ?? "";
			CharacterSize   = _charicterSize ?? 10u;
		}

		public new void Draw(RenderTarget _target, RenderStates _state) {
            if (!Show) return;
			base.Draw(_target, _state);
		}
	}
}
