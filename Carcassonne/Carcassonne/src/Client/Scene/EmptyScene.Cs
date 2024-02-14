//====================================================================================================
// EmptyScene.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.System;

namespace nk.Carcassonne {
	public class EmptyScene() : SceneBase() {

		public override void Initialize(Vector2i _windowSize) {}
		public override void Update(ClientTime _clientTime) {}

	}
}
