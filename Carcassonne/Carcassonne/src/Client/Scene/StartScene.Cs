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

		public override void Initialize() {
			
			StartSceneST st = new(WindowBase.Language);

			//====================================================================================================| Make Shapes

			//====================================================================================| Title

			TextObject title = new() {
				CharacterSize   = (uint)(WindowBase.WindowSize.X * 0.075f),
				Font            = Fonts.Normal,
				DisplayedString = "Not Carcassonne",
				Position        = new(WindowBase.WindowSize.X * 0.05f, WindowBase.WindowSize.X * 0.05f)
			};
			Objects.Add(title);

			//====================================================================================| Comment

			TextObject comment = new() {
				CharacterSize    = (uint)(WindowBase.WindowSize.X * 0.02f),
				Font             = Fonts.Normal,
				Rotation         = -10,
				DisplayedString  = st.Comment,
				FillColor        = Colors.Yellow,
				OutlineThickness = 1.5f,
				Position         = new(WindowBase.WindowSize.X * 0.63f, WindowBase.WindowSize.X * 0.12f)
			};
			comment.Position += new Vector2f(0f, comment.GetGlobalBounds().Height / 2);
			comment.Origin = new(
				comment.GetGlobalBounds().Width / 2,
				comment.GetGlobalBounds().Height / 2
			);
			Objects.Add(comment);
			Updates.Add((_clientTime) => {
				float scale = 1f + 0.075f * (float)Math.Sin(_clientTime.TotalTimeElapsed * 7);
				comment.Scale = new(scale, scale);
			});

			//====================================================================================| Version

			TextObject version = new() {
				CharacterSize   = (uint)(WindowBase.WindowSize.X * 0.015f),
				Font            = Fonts.Normal,
				FillColor       = new(255, 255, 255),
				DisplayedString = WindowBase.ClientVersion
			};
			version.Position = new(
				WindowBase.WindowSize.X - version.GetGlobalBounds().Width  - WindowBase.WindowSize.X * 0.01f,
				WindowBase.WindowSize.Y - version.GetGlobalBounds().Height - WindowBase.WindowSize.X * 0.01f
			);
			Objects.Add(version);

			//====================================================================================| Connect Buttons

			Button connectButton = new() {
				Position     = new(WindowBase.WindowSize.X * 0.05f, WindowBase.WindowSize.Y * 0.6f),
				Size         = new(WindowBase.WindowSize.X * 0.3f,  WindowBase.WindowSize.X * 0.05f),
				Radius       = WindowBase.WindowSize.X * 0.01f,
				TextElement  = new(st.Connect, Fonts.Normal, (uint)(WindowBase.WindowSize.X * 0.025f)),
				OnClickEvent = () => {

                }
			};
			Objects.Add(connectButton);

			//====================================================================================| Host Buttons

			Button hostButton = new() {
				Position      = new(WindowBase.WindowSize.X * 0.05f, WindowBase.WindowSize.Y * 0.71f),
				Size          = new(WindowBase.WindowSize.X * 0.3f,  WindowBase.WindowSize.X * 0.05f),
				Radius        = WindowBase.WindowSize.X * 0.01f,
				TextElement   = new(st.Host, Fonts.Normal, (uint)(WindowBase.WindowSize.X * 0.025f)),
				OnClickEvent  = () => {
                    
                }
			};
			Objects.Add(hostButton);

			//====================================================================================| Option Buttons

			Button optionButton = new() {
				Position     = new(WindowBase.WindowSize.X * 0.05f,  WindowBase.WindowSize.Y * 0.82f),
				Size         = new(WindowBase.WindowSize.X * 0.145f, WindowBase.WindowSize.X * 0.05f),
				Radius       = WindowBase.WindowSize.X * 0.01f,
				TextElement  = new(st.Option, Fonts.Normal, (uint)(WindowBase.WindowSize.X * 0.025f)),
				OnClickEvent = () => {

                }
			};
			Objects.Add(optionButton);

			//====================================================================================| Exit Buttons

			Button exitButton = new() {
				Position      = new(WindowBase.WindowSize.X * 0.205f,  WindowBase.WindowSize.Y * 0.82f),
				Size          = new(WindowBase.WindowSize.X * 0.145f,  WindowBase.WindowSize.X * 0.05f),
				Radius        = WindowBase.WindowSize.X * 0.01f,
				TextElement   = new(st.Exit, Fonts.Normal, (uint)(WindowBase.WindowSize.X * 0.025f)),
				OnClickEvent  = WindowBase.Window.Close
			};
			Objects.Add(exitButton);

			//====================================================================================| Exit Buttons

			WindowTab connectWindow = new() {
				Size     = new(WindowBase.WindowSize.X * 0.35f, WindowBase.WindowSize.X * 0.15f),
				Movement = true,
				Title    = st.Connect
			};
			connectWindow.ToCenter();
			Objects.Add(connectWindow);

        }
	}
}