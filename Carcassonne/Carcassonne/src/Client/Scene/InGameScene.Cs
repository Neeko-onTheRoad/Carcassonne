//====================================================================================================
// InGame.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.System;

namespace nk.Carcassonne {
	public class InGameScene() : SceneBase() {

		public List<Player>                   Players { get; protected set; } = [];
		public Dictionary<Vector2i, TileBase> Cards   { get; protected set; } = [];

		public override void Initialize(WindowBase _window) {
			
		}

	}
}
