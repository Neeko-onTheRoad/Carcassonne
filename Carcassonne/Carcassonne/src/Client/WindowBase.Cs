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

	public static partial class WindowBase {

		//====================================================================================================| Member Variables
		
		public static string WindowTitle     { get; private set; } = "Carccasson";
		public static string ClientVersion   { get; private set; } = "V Error";
		public static int    TargetFPS       { get; private set; } = 144;
		public static float  TimeUntilUpdate { get; private set; } = 1f;
		public static float  DeltaTime       { get; private set; } = 0f;
		
		public static RenderWindow Window { get; private set; } = new(new(0, 0), "");

		public static Vector2u   WindowSize { get; private set; } = new(0, 0);
		public static SceneBase  NowScene   { get; private set; } = new StartScene();
		public static ClientTime ClientTime { get; private set; } = new();

		public static Dictionary<Cursor.CursorType, Cursor> Cursors { get; private set; } = [];

		public static string  Language { get; set; } = "ko";
		public static IDrawableObject? Clicking { get; set; } = null;
		public static IDrawableObject? Hovering { get; set; } = null;

		//====================================================================================================| Constructors

		public static void Init(string _title, string _version) {
			WindowTitle   = _title;
			ClientVersion = _version;
		}

		//====================================================================================================| Dll Imports

		[LibraryImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static partial bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

		//====================================================================================================| Functions

		//============================================================| Change Scene

		public static void ChangeScene(SceneBase _scene) {
			NowScene = _scene;
			NowScene.Initialize();
		}

		//============================================================| CreateNewWindow

		private static void CreateNewWindow(Vector2u _size) {
			Window.Close();

			WindowSize = new(1600, 900);
			
			Styles style = Styles.Close;

			ContextSettings setting = new() { AntialiasingLevel = 1 };

			Window = new RenderWindow(new VideoMode(WindowSize.X, WindowSize.Y), WindowTitle, style, setting);

			ChangeScene(NowScene);
		}

		//============================================================| GetHovering

		private static IDrawableObject? GetHovering(List<IDrawableObject> objects) {
			for (int index = objects.Count - 1; index >= 0; index--) {
				IDrawableObject obj = objects[index];

				if (MouseListener.IsHovering(obj)) {
					IDrawableObject hovering = obj;

					while (hovering is IMultyDrawableObject @object) {
						for (int jndex = @object.AddedObjects.Count - 1; jndex >= 0; jndex--) {
							IDrawableObject jovering = @object.AddedObjects[jndex];

							if (MouseListener.IsHovering(jovering)) {
								return jovering;
							}
						}
					}

					return hovering;
				}
			}

			return null;
		}

		//============================================================| Run

		public static void Run() {
			CreateNewWindow(WindowSize);
			MouseListener.Initialize();

			TimeUntilUpdate = 1f / TargetFPS;
			float totalTimeBeforUpdate = 0f;
			float previousTimeElapsed  = 0f;
			float deltaTime;
			float totalTimeElapsed;

			Clock clock = new();

			while(Window.IsOpen) {

				totalTimeElapsed    = clock.ElapsedTime.AsSeconds();
				deltaTime           = totalTimeElapsed - previousTimeElapsed;
				previousTimeElapsed = totalTimeElapsed;

				totalTimeBeforUpdate += deltaTime;
				DeltaTime = deltaTime;

                if (totalTimeBeforUpdate >= TimeUntilUpdate) {

					Hovering = GetHovering(NowScene.Objects);

                    Window.DispatchEvents();

					ClientTime.Update(totalTimeBeforUpdate, clock.ElapsedTime.AsSeconds());
					totalTimeBeforUpdate = 0f;

					NowScene.Update(ClientTime);

					Window.Clear(Colors.Gray);
					Window.Draw(NowScene);
					Window.Display();
				}
			}
		}
	}

	public static class MouseListener {

		private static Vector2i CurruntPosition = new(0, 0);
		
		public static Vector2i DragStart { get; private set; } = new(0, 0);
		public static Vector2i DragDelta { get; private set; } = new(0, 0);
		public static Vector2i Delta     { get; private set; } = new(0, 0);
		public static Vector2i Position  { get; private set; } = new(0, 0);

		public static void Initialize() {
			WindowBase.Window.MouseMoved += (_sender, _event) => {
				CurruntPosition = Position;

				Position = new(
					_event.X,
					_event.Y
				);

				Delta     = Position - CurruntPosition;
				DragDelta = Position - DragStart;
			};
			WindowBase.Window.MouseButtonPressed += (_sender, _event) => {
				DragDelta = new(0, 0);
				DragStart = Position;
			};
		}

		//============================================================| IsHovering

		public static bool IsHovering(IDrawableObject _object) {
			return _object.GetGlobalBounds().Contains(
				Position.X, Position.Y
			);
		}

		public static bool IsHovering(FloatRect _rect) {
			return _rect.Contains(
				Position.X, Position.Y
			);
		}

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