//====================================================================================================
// StartScreen.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Graphics;
using SFML.Window;

namespace nk.Carcassonne {
	public class StartScene() : SceneBase() {

		public override void Initialize(WindowBase _window) {
			
			//====================================================================================================| Make Shapes

			Button connectButton = new(_window) {
				Position = new(_window.WindowSize.X * 0.05f, _window.WindowSize.Y * 0.6f),
				Size     = new(_window.WindowSize.X * 0.3f,  _window.WindowSize.X * 0.05f),
				Radius   = _window.WindowSize.X * 0.01f,
				Color    = Colors.Button_Relesed
			};
			Sprites.Add(connectButton);

			Button hostButton = new(_window) {
				Position = new(_window.WindowSize.X * 0.05f, _window.WindowSize.Y * 0.71f),
				Size     = new(_window.WindowSize.X * 0.3f,  _window.WindowSize.X * 0.05f),
				Radius   = _window.WindowSize.X * 0.01f,
				Color    = Colors.Button_Relesed
			};
			Sprites.Add(hostButton);
        }

		public override void Update(ClientTime _clientTime) {
		}

	}
}