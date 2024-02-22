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

	public class WindowTab : IMultyDrawableObject, IDragable {

		//====================================================================================================| Members

		private bool needUpdate = true;

		public bool Show           { get; set; } = true;
		public bool Ignore         { get; set; } = false;
		public bool Movement       { get; set; } = true;
		public bool Popupable      { get; set; } = true;
		public bool Closeable      { get; set; } = true;
		public bool CursorChanging { get; set; } = false;

		private string   title           = "";
		private ColorSF  borderColor     = Colors.LightPurple;
		private ColorSF  backgroundColor = Colors.TransparentBlack;
		private Vector2f size            = new(100f, 100f);
		private Vector2f position        = new(0f, 0f);

		public bool	 Draging	  { get; set; } = false;
		public Vector2f DragStartPos { get; set; } = new(0f, 0f);

		public List<IDrawableObject> Objects	  { get; set; } = [];
		public List<IDrawableObject> AddedObjects { get; set; } = [];

		//====================================================================================================| Normal Shapes

		private RectangleShapeObject? Background;
		private RectangleShapeObject? TitleBar;
		private TextObject?		      TitleStr;
		private Button?			      CloseButton;

		//====================================================================================================| Getter, Setter

		public string Title {
			get => title;
			set {
				title	  = value;
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
				needUpdate	  = true;
			}
		}
		public Vector2f Size {
			get => size;
			set {
				size	   = value;
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

			//============================================================| Background

			Background ??= new();

			Background.Position = Position + new Vector2f(
				WindowBase.WindowSize.X * 0.001f,
				WindowBase.WindowSize.X * 0.021f
			);
			Background.Size = Size - new Vector2f (
				WindowBase.WindowSize.X * 0.002f,
				WindowBase.WindowSize.X * 0.022f
			);
			Background.FillColor        = BackgroundColor;
			Background.OutlineColor     = BorderColor;
			Background.OutlineThickness = WindowBase.WindowSize.X * 0.001f;
			AddedObjects.Add(Background);

			//============================================================| Titlebar

			TitleBar ??= new();

			TitleBar.Position	    = Position;
			TitleBar.Size		    = new(Size.X, WindowBase.WindowSize.X * 0.02f);
			TitleBar.FillColor      = BorderColor;
			TitleBar.CursorChanging = true;
			AddedObjects.Add(TitleBar);

			//============================================================| TitleStr

			TitleStr ??= new();

			TitleStr.DisplayedString = Title;
			TitleStr.Font            = Res.Fonts.Normal;
			TitleStr.CharacterSize   = (uint)(WindowBase.WindowSize.X * 0.0125f);
			TitleStr.Ignore		     = true;
			TitleStr.Position        = Position + new Vector2f(
				WindowBase.WindowSize.X * 0.005f,
				(WindowBase.WindowSize.X * 0.01f) - (TitleStr.GetGlobalBounds().Height * 0.65f)
			);
			AddedObjects.Add(TitleStr);

			//============================================================| Close Button

			if (Closeable) {
				CloseButton ??= new();

				CloseButton.Size	     = new(WindowBase.WindowSize.X * 0.017f, WindowBase.WindowSize.X * 0.017f);
				CloseButton.Position     = Position + new Vector2f(Size.X - WindowBase.WindowSize.X * 0.02f, WindowBase.WindowSize.X * 0.002f);
				CloseButton.Radius	     = WindowBase.WindowSize.X * 0.002f;
				CloseButton.PointCount   = 1;
				CloseButton.ElementType  = ButtonElementType.ICON;
				CloseButton.ElementAlign = ButtonElementAlign.MIDDLE;
				CloseButton.OnClickEvent = () => { Show = false; };
				CloseButton.IconElement  = new(Res.Icons.X);
				CloseButton.IconElement.Scale = new(
					WindowBase.WindowSize.X * 0.01f / CloseButton.IconElement.GetGlobalBounds().Width,
					WindowBase.WindowSize.X * 0.01f / CloseButton.IconElement.GetGlobalBounds().Height
				);
				AddedObjects.Add(CloseButton);
			}

			Objects.ForEach(AddedObjects.Add);
		}

		//====================================================================================================| Constructor

		private byte BorderHovered  = 2;
		private byte BorderRelesed  = 0;

		private readonly EventHandler<MouseMoveEventArgs>   MouseMovedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonPressedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonReleasedEvent;

		public WindowTab() {
			
			MouseMovedEvent = (_sender, _event) => {
				if (!Show) return;

				if (WindowBase.Hovering == TitleBar) { if (BorderHovered == 2) BorderHovered = 1; }
				else if (BorderRelesed == 2) BorderRelesed = 1;

				if (BorderHovered == 1) {
					WindowBase.Window.SetMouseCursor(new(Cursor.CursorType.Hand));

					BorderHovered = 0;
					BorderRelesed = 2;
				}
				if (BorderRelesed == 1) {
					WindowBase.Window.SetMouseCursor(new(Cursor.CursorType.Arrow));

					BorderRelesed = 0;
					BorderHovered = 2;
				}

				if (Draging && Movement) {
					Vector2f updatedPosition = DragStartPos + (Vector2f)MouseListener.DragDelta;
					Vector2f positionLimit   = (Vector2f)WindowBase.WindowSize - Size;

					if	    (updatedPosition.Y < 0f)	          updatedPosition = new(updatedPosition.X, 0f             );
					else if (updatedPosition.Y > positionLimit.Y) updatedPosition = new(updatedPosition.X, positionLimit.Y);
					
					if	    (updatedPosition.X < 0f)			  updatedPosition = new(0f             , updatedPosition.Y);
					else if (updatedPosition.X > positionLimit.X) updatedPosition = new(positionLimit.X, updatedPosition.Y);

					Position	= updatedPosition;
				}
			};

			MouseButtonPressedEvent = (_sender, _event) => {
				if (!Show) return;
				if (_event.Button == Mouse.Button.Left) {
					if (WindowBase.Hovering == TitleBar) {
						DragStartPos = Position;
						Draging = true;
						WindowBase.Clicking = TitleBar;
					}
				}
			};

			MouseButtonReleasedEvent = (_sender, _event) => {
				if (!Show) return;
				if (_event.Button == Mouse.Button.Left) {
					Draging = false;
					if (WindowBase.Clicking == TitleBar) {
						WindowBase.Clicking = null;
					}
				}
			};

			WindowBase.Window.MouseMoved          += MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  += MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased += MouseButtonReleasedEvent;
		}

		//====================================================================================================| Destructor

		~WindowTab() {
			WindowBase.Window.MouseMoved		  -= MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  -= MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased -= MouseButtonReleasedEvent;
		}

		//====================================================================================================| Functions

		public void ToCenter() {
			Position = new (
				WindowBase.WindowSize.X / 2f - Size.X / 2f,
				WindowBase.WindowSize.Y / 2f - Size.Y / 2f
			);
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
