//====================================================================================================
// IDragable.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.System;

namespace nk.Carcassonne {
	public interface IDragable {
		public bool     Draging      { get; protected set; }
		public Vector2f DragStartPos { get; protected set; }
	}
}
