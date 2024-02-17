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
using System.Reflection;

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
		public static object? Clicking { get; set; } = null;
		public static object? Hovering { get; set; } = null;

		//====================================================================================================| Constructors

		public static void Init(string _title, string _version) {
			WindowTitle   = _title;
			ClientVersion = _version;
			
			Cursors[Cursor.CursorType.Arrow] = new(Cursor.CursorType.Arrow);
			Cursors[Cursor.CursorType.Hand]  = new(Cursor.CursorType.Hand);
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
					foreach (var i in NowScene.Objects) {
                        if (i.GetGlobalBounds().Contains(MouseListener.Position.X, MouseListener.Position.Y)) {
                            if (i.GetType().GetInterfaces()[0] == typeof(IMultyDrawableObject)) {
								foreach(var j in (i as IMultyDrawableObject ?? new WindowTab()).AddedObjects) {
									if (j.GetGlobalBounds().Contains(MouseListener.Position.X, MouseListener.Position.Y)) {
                                        Hovering = j;
										break;
									}
								}
							}

                            Hovering = i;
							break;
						}
						Hovering = null;
					}

                    Window.DispatchEvents();

					ClientTime.Update(totalTimeBeforUpdate, clock.ElapsedTime.AsSeconds());
					totalTimeBeforUpdate = 0f;

					NowScene.Update(ClientTime);

					Window.Clear(new(25, 25, 25));
					Window.Draw(NowScene);
					Window.Display();
				}
			}
		}
	}

	public static class MouseListener {

		public static Vector2i Position { get; private set; } = new(0, 0);
		public static void Initialize() {
			WindowBase.Window.MouseMoved += (object? _sender, MouseMoveEventArgs _event) => {
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