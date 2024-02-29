//====================================================================================================
// $itemrootname.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Graphics;
using SFML.System;

namespace nk.Carcassonne {
	public class InputBox : RoundedRectangleShape, IDrawableObject {

		//====================================================================================================| Members

		private TextObject? textElement;
		public  TextObject? TextElement {
			get => textElement;
			set {
				textElement = value;
				needUpdate  = true;
			}
		}

		//====================================================================================================| Constructor

		public InputBox() {
			
		}

		//====================================================================================================| Override

		public new void Draw(RenderTarget _target, RenderStates _state) {
			base.Draw(_target, _state);
		}
	}
}
