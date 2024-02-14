//====================================================================================================
// StartScreen.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.System;

namespace nk.Carcassonne {
	public class StartScene() : SceneBase() {

		public override void Initialize(Vector2i _windowSize) {
            using (RoundedRectangle connectButton = new() {
				Position = new(),
			})
			Sprites.Add(connectButton);
        }

		public override void Update(ClientTime _clientTime) {

		}

	}
}