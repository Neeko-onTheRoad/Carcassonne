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

		public override void InitializeBase() {
			
			StartSceneST st = new(WindowBase.Language);

			//====================================================================================================| Make Shapes

			//====================================================================================| Title

			TextObject title = new() {
				CharacterSize   = (uint)(WindowBase.WindowSize.X * .075f),
				DisplayedString = "Not Carcassonne",
				Position        = new(WindowBase.WindowSize.X * .05f, WindowBase.WindowSize.X * .05f)
			};
			Objects.Add(title);

			//====================================================================================| Comment

			TextObject comment = new() {
				CharacterSize    = (uint)(WindowBase.WindowSize.X * .02f),
				Rotation         = -10,
				DisplayedString  = st.Comment,
				FillColor        = Colors.Yellow,
				OutlineThickness = 1.5f,
				Position         = new(WindowBase.WindowSize.X * .63f, WindowBase.WindowSize.X * .12f)
			};
			comment.Position += new Vector2f(0f, comment.GetGlobalBounds().Height / 2);
			comment.Origin = new(
				comment.GetGlobalBounds().Width  / 2,
				comment.GetGlobalBounds().Height / 2
			);
			Objects.Add(comment);
			Updates.Add((_clientTime) => {
				float scale = 1f + .075f * (float)Math.Sin(_clientTime.TotalTimeElapsed * 7);
				comment.Scale = new(scale, scale);
			});

			//====================================================================================| Version

			TextObject version = new() {
				CharacterSize   = (uint)(WindowBase.WindowSize.X * .015f),
				FillColor       = new(255, 255, 255),
				DisplayedString = WindowBase.ClientVersion
			};
			version.Position = new(
				WindowBase.WindowSize.X - version.GetGlobalBounds().Width  - WindowBase.WindowSize.X * .01f,
				WindowBase.WindowSize.Y - version.GetGlobalBounds().Height - WindowBase.WindowSize.X * .01f
			);
			Objects.Add(version);

			//====================================================================================| Connect Window

			WindowTab connectWindow = new() {
				Size     = new(WindowBase.WindowSize.X * .35f, WindowBase.WindowSize.X * .15f),
				Movement = true,
				Title    = st.Connect,
				Show     = false
			};
			InputBox addres = new() {
				Position = connectWindow.Position + new Vector2f(WindowBase.WindowSize.X * .02f, WindowBase.WindowSize.X * .025f),
				Size     = new(WindowBase.WindowSize.X * .1f, WindowBase.WindowSize.X * .0225f)
			};
			InputBox port = new InputBox() {
				Position = connectWindow.Position + new Vector2f(WindowBase.WindowSize.X * .02f, WindowBase.WindowSize.X * .055f),
				Size     = new(WindowBase.WindowSize.X * .1f, WindowBase.WindowSize.X * .0225f),
				Previous = addres
			};
			addres.Next          = port;
			addres.DisplayString = st.Addres;
			port.  DisplayString = st.Port;
			connectWindow.AddObject(addres);
			connectWindow.AddObject(port);

			//====================================================================================| Connect Button

			Button connectButton = new() {
				Position     = new(WindowBase.WindowSize.X * .05f, WindowBase.WindowSize.Y * .6f),
				Size         = new(WindowBase.WindowSize.X * .3f,  WindowBase.WindowSize.X * .05f),
				Radius       = WindowBase.WindowSize.X * .01f,
				TextElement  = new(st.Connect, (uint)(WindowBase.WindowSize.X * .025f)),
				OnClickEvent = () => {
					connectWindow.ToCenter();
					connectWindow.Show = true;
				}

			};
			Objects.Add(connectButton);

			//====================================================================================| Host Buttons

			Button hostButton = new() {
				Position      = new(WindowBase.WindowSize.X * .05f, WindowBase.WindowSize.Y * .71f),
				Size          = new(WindowBase.WindowSize.X * .3f,  WindowBase.WindowSize.X * .05f),
				Radius        = WindowBase.WindowSize.X * .01f,
				TextElement   = new(st.Host, (uint)(WindowBase.WindowSize.X * .025f)),
				OnClickEvent  = () => {
                    
				}
			};
			Objects.Add(hostButton);

			//====================================================================================| Option Buttons

			Button optionButton = new() {
				Position     = new(WindowBase.WindowSize.X * .05f,  WindowBase.WindowSize.Y * .82f),
				Size         = new(WindowBase.WindowSize.X * .145f, WindowBase.WindowSize.X * .05f),
				Radius       = WindowBase.WindowSize.X * .01f,
				TextElement  = new(st.Option, (uint)(WindowBase.WindowSize.X * .025f)),
				OnClickEvent = () => {
					
				}
			};
			Objects.Add(optionButton);

			//====================================================================================| Exit Buttons

			Button exitButton = new() {
				Position      = new(WindowBase.WindowSize.X * .205f,  WindowBase.WindowSize.Y * .82f),
				Size          = new(WindowBase.WindowSize.X * .145f,  WindowBase.WindowSize.X * .05f),
				Radius        = WindowBase.WindowSize.X * .01f,
				TextElement   = new(st.Exit, (uint)(WindowBase.WindowSize.X * .025f)),
				OnClickEvent  = WindowBase.Window.Close
			};
			Objects.Add(exitButton);

			Objects.Add(connectWindow);
			
			//====================================================================================================| Initialize Event

		}
	}
}