//====================================================================================================
// WindowBase.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Window;
using SFML.System;
using SFML.Graphics;

using System.Runtime.InteropServices;


namespace nk.Carcassonne {

	public partial class WindowBase {

		//====================================================================================================| Member Variables
		
		public string WindowTitle     { get; protected set; } = "Carccasson";
		public string ClientVersion   { get; protected set; } = "V Error";
		public int    TargetFPS       { get; protected set; } = 0;
		public float  TimeUntilUpdate { get; protected set; } = 1f;
		public float  DeltaTime       { get; protected set; } = 0f;
		
		private RenderWindow window = new(new(0, 0), "");
		public  RenderWindow Window { get{ return window; } protected set { window = value; } }

		public Vector2u   WindowSize { get; protected set; } = new(0, 0);
		public SceneBase  NowScene   { get; protected set; } = new StartScene();
		public ClientTime ClientTime { get; protected set; } = new();
		
		public object? Clicking { get; set; } = null;
		public object? Hovering { get; set; } = null;

		//====================================================================================================| Constructors

		public WindowBase(string _title, string _version) {
			WindowTitle   = _title;
			ClientVersion = _version;
		}

		//====================================================================================================| Dll Imports

		[LibraryImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static partial bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

		//====================================================================================================| Functions

		//============================================================| Change Scene

		public void ChangeScene(SceneBase _scene) {
			NowScene = _scene;
			NowScene.Initialize(this);
		}

		//============================================================| CreateNewWindow

		protected void CreateNewWindow(Vector2u _size) {
			Window.Close();

			WindowSize = new(1600, 900);
			
			Styles style = Styles.Close;

			ContextSettings setting = new() { AntialiasingLevel = 1 };

			window = new RenderWindow(new VideoMode(WindowSize.X, WindowSize.Y), WindowTitle, style, setting);
			Window.Closed += (object? _sender, EventArgs _event) => Window.Close();

			ChangeScene(NowScene);
		}

		//============================================================| Run

		public void Run() {
			CreateNewWindow(WindowSize);
			MouseListener.Initialize(this);

			float totalTimeBeforUpdate = 0f;
			float previousTimeElapsed  = 0f;
			float deltaTime;
			float totalTimeElapsed;

			Clock clock = new();

			while(Window.IsOpen) {
				
				totalTimeElapsed    = clock.ElapsedTime.AsSeconds();
				deltaTime           = totalTimeElapsed - previousTimeElapsed;
				previousTimeElapsed = totalTimeElapsed;

				totalTimeElapsed += deltaTime;
				DeltaTime = deltaTime;

				if (totalTimeElapsed >= TimeUntilUpdate) {
					Window.DispatchEvents();

					ClientTime.Update(totalTimeBeforUpdate, clock.ElapsedTime.AsSeconds());
					totalTimeBeforUpdate = 0f;

					NowScene.Update(ClientTime);

					Window.Clear(Color.Black);
					Window.Draw(NowScene);
					Window.Display();
				}
			}
		}
	}

	public static class MouseListener {

		public static Vector2i Position { get; private set; } = new(0, 0);
		public static void Initialize(WindowBase _window) {
			_window.Window.MouseMoved += (object? _sender, MouseMoveEventArgs _event) => {
				Position = new(
					_event.X,
					_event.Y
				);
			};
		}

		//============================================================| IsHovering

		public static bool IsHovering(Shape _object) {
            return _object.GetGlobalBounds().Contains(
				Position.X, Position.Y
			);
		}
		public static bool IsHovering(Sprite _object) {
			return _object.GetGlobalBounds().Contains(
				Position.X, Position.Y
			);
		}
	}
}