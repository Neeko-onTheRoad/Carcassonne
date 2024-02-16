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

			StringTable st = new(_window.Language);
			
			//====================================================================================================| Make Shapes

			//====================================================================================| Title

			Text title = new() {
				CharacterSize   = (uint)(_window.WindowSize.X * 0.075f),
				Font            = Fonts.Normal,
				Position        = new(_window.WindowSize.X * 0.05f, _window.WindowSize.X * 0.05f),
				FillColor       = new(255, 255, 255),
				DisplayedString = "Not Carcassonne"
			};
			Sprites.Add(title);

			Text comment = new() {
				CharacterSize   = (uint)(_window.WindowSize.X * 0.075f),
				Font            = Fonts.Normal,
				Position        = new(_window.WindowSize.X * 0.05f, _window.WindowSize.X * 0.05f),
				FillColor       = new(255, 255, 255),
				DisplayedString = "Not Carcassonne"
			};
			Sprites.Add(title);

			//====================================================================================| Buttons

			Button connectButton = new(_window) {
				Position     = new(_window.WindowSize.X * 0.05f, _window.WindowSize.Y * 0.6f),
				Size         = new(_window.WindowSize.X * 0.3f,  _window.WindowSize.X * 0.05f),
				Radius       = _window.WindowSize.X * 0.01f,
				TextElement  = new("Connect", Fonts.Normal, (uint)(_window.WindowSize.X * 0.025f)),
				OnClickEvent = () => {
					
				}
			};
			Sprites.Add(connectButton);

			Button hostButton = new(_window) {
				Position     = new(_window.WindowSize.X * 0.05f, _window.WindowSize.Y * 0.71f),
				Size         = new(_window.WindowSize.X * 0.3f,  _window.WindowSize.X * 0.05f),
				Radius       = _window.WindowSize.X * 0.01f,
				TextElement  = new("Host", Fonts.Normal, (uint)(_window.WindowSize.X * 0.025f)),
				OnClickEvent = () => {
					
				}
			};
			Sprites.Add(hostButton);

			Button optionButton = new(_window) {
				Position     = new(_window.WindowSize.X * 0.05f,  _window.WindowSize.Y * 0.82f),
				Size         = new(_window.WindowSize.X * 0.145f, _window.WindowSize.X * 0.05f),
				Radius       = _window.WindowSize.X * 0.01f,
				TextElement  = new("Option", Fonts.Normal, (uint)(_window.WindowSize.X * 0.025f)),
				OnClickEvent = () => {
					
				}
			};
			Sprites.Add(optionButton);

			Button exitButton = new(_window) {
				Position     = new(_window.WindowSize.X * 0.205f,  _window.WindowSize.Y * 0.82f),
				Size         = new(_window.WindowSize.X * 0.145f,  _window.WindowSize.X * 0.05f),
				Radius       = _window.WindowSize.X * 0.01f,
				TextElement  = new("Exit", Fonts.Normal, (uint)(_window.WindowSize.X * 0.025f)),
				OnClickEvent =  _window.Window.Close
			};
			Sprites.Add(exitButton);
        }

	}
}