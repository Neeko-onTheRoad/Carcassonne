//====================================================================================================
// $itemrootname.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace nk.Carcassonne {
	public class WindowTab : IMultyDrawableObject {

		//====================================================================================================| Members

		private bool needUpdate = true;

		public bool Show     { get; set; } = true;
		public bool Movement { get; set; } = true;

		private string   title           = "";
		private ColorSF  borderColor     = Colors.LightPurple;
		private ColorSF  backgroundColor = Colors.TransparentBlack;
		private Vector2f size            = new(100f, 100f);
		private Vector2f position        = new(0f, 0f);

		public List<IDrawableObject> Objects      { get; set; } = [];
		public List<IDrawableObject> AddedObjects { get; set; } = [];

		//====================================================================================================| Normal Shapes

		private RectangleShapeObject Background = new();
		private RectangleShapeObject TitleBar   = new();

		//====================================================================================================| Getter, Setter

		public string Title {
			get => title;
			set {
				title      = value;
				needUpdate = true;
			}
		}
		public ColorSF BorderColor {
			get => borderColor;
			set {
				borderColor = value;
				needUpdate  = true;
			}
		}
		public ColorSF BackgroundColor {
			get => backgroundColor;
			set {
				backgroundColor = value;
				needUpdate      = true;
			}
		}
		public Vector2f Size {
			get => size;
			set {
				size       = value;
				needUpdate = true;
			}
		}
		public Vector2f Position {
			get => position;
			set {
				position   = value;
				needUpdate = true;
			}
		}

		//====================================================================================================| Rerender

		private void Rerender() {
			AddedObjects.Clear();

			Background = new() {
				Position         = Position + new Vector2f(0, WindowBase.WindowSize.X * 0.02f),
				Size             = Size     - new Vector2f(0, WindowBase.WindowSize.X * 0.02f),
				FillColor        = BackgroundColor,
				OutlineColor     = BorderColor,
				OutlineThickness = WindowBase.WindowSize.X * 0.001f
			};
			AddedObjects.Add(Background);

			TitleBar = new() {
				Position         = Position,
				Size             = new(Size.X, WindowBase.WindowSize.X * 0.02f),
				FillColor        = BorderColor,
				OutlineColor     = BorderColor,
				OutlineThickness = WindowBase.WindowSize.X * 0.001f
			};
			AddedObjects.Add(TitleBar);

			Objects.ForEach(AddedObjects.Add);
		}

		//====================================================================================================| Constructor

		private bool BorderHovered = false;
		private bool BorderRelesed = false;
		private readonly EventHandler<MouseMoveEventArgs>   MouseMovedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonPressedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonReleasedEvent;

		public WindowTab() {
            MouseMovedEvent = (_sender, _event) => {
				if (!Show) return;

				if (WindowBase.Hovering == TitleBar) BorderHovered = true;
				else BorderRelesed = true;

				if (BorderHovered) {
					if (Movement) BorderColor = Colors.Purple;

					BorderHovered = false;
				}
				if (BorderRelesed) {
                    if (Movement) BorderColor = Colors.LightPurple;

					BorderRelesed = false;
				}
            };

			MouseButtonPressedEvent = (_sender, _event) => {
				if (!Show) return;
				if (_event.Button == Mouse.Button.Left) {
					if (WindowBase.Hovering == this) {
						
					}
				}
			};

			MouseButtonReleasedEvent = (_sender, _event) => {
				if (!Show) return;
			};

			WindowBase.Window.MouseMoved          += MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  += MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased += MouseButtonReleasedEvent;
		}

		//====================================================================================================| Destructor

		~WindowTab() {
			WindowBase.Window.MouseMoved          -= MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  -= MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased -= MouseButtonReleasedEvent;
		}

		//====================================================================================================| Override

		public void AddObject(IDrawableObject _object) => AddedObjects.Add(_object);

		public FloatRect GetGlobalBounds() {
			if (Show) return new(
				Position.X, Position.Y,
				Size.X, Size.Y
			);
			else return new(0, 0, 0, 0);
		}
		
		void Drawable.Draw(RenderTarget _target, RenderStates _states) {
            if (!Show) return;
			
			if (needUpdate) {
				Rerender();
				needUpdate = false;
			}

			AddedObjects.ForEach(i => {
                _target.Draw(i, _states);
			});
		}

	}
}
