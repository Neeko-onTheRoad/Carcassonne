//====================================================================================================
// $itemrootname.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Graphics;

namespace nk.Carcassonne {
	public class WindowTab : Drawable, IGetGlobalBounds {

		public bool Show { get; set; } = true;
		
		void Drawable.Draw(RenderTarget _target, RenderStates _states) {
			if(!Show) return;

			_target.Draw()
		}

	}
}
