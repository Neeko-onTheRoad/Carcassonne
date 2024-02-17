//====================================================================================================
// $itemrootname.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace nk.Carcassonne {
	public enum ButtonElementType {
		TEXT, ICON
	}

	public enum ButtonElementAlign {
		LEFT, MIDDLE, RIGHT
	}

	public class Button : RoundedRectangleShape, Drawable {

		public ButtonElementType  ElementType  { get; set; } = ButtonElementType.TEXT;
		public ButtonElementAlign ElementAlign { get; set; } = ButtonElementAlign.MIDDLE;

		public bool Show { get; set; } = true;

		public  bool Hovering { get; protected set ; } = false;
		private bool Hovered  = false;
		private bool Relesed  = false;

		public Button() {
			WindowBase.Window.MouseMoved += (object? _sender, MouseMoveEventArgs _event) => {
				if (!Show) return;
				if (GetHovering()) {
					Hovering = true;

					WindowBase.Window.SetMouseCursor(WindowBase.Cursors[Cursor.CursorType.Hand]);
					WindowBase.Hovering = this;
					Color = Colors.Purple;
				}
				else {
					Hovering = false;

					WindowBase.Window.SetMouseCursor(WindowBase.Cursors[Cursor.CursorType.Arrow]);
					WindowBase.Hovering = null;
					Color = Colors.LightPurple;
				}
			};

			WindowBase.Window.MouseButtonPressed += (object? _sender, MouseButtonEventArgs _event) => {
				if (!Show) return;
				if (_event.Button == Mouse.Button.Left) {
					if (WindowBase.Hovering == this) WindowBase.Clicking = this;
				}
			};

			WindowBase.Window.MouseButtonReleased += (object? _sender, MouseButtonEventArgs _event) => {
				if (!Show) return;
				if (_event.Button == Mouse.Button.Left) {
					if (WindowBase.Clicking == this && WindowBase.Hovering == this) {
                        OnClickEvent();
                    }
				}
			};

			Color = Colors.LightPurple;
		}

		~Button() {
			
		}

		private Text textElement = new();
		public  Text TextElement {
			get => textElement;
			set {
				textElement = value;
				needUpdate  = true;
			}
		}

		private float margin = 7f;
		public  float Margin {
			get => margin;
			set {
				margin     = value;
				needUpdate = true;
			}
		}

		public Action OnClickEvent { get; set; } = () => {};

		private void Rerender() {
			if (ElementType == ButtonElementType.TEXT) {
				Vector2f position = new(0, Position.Y + (Size.Y / 2) - (TextElement.CharacterSize * 0.55f));	

				if (ElementAlign == ButtonElementAlign.LEFT) {
					position.X = Position.X + Margin;
				}
				else if (ElementAlign == ButtonElementAlign.MIDDLE) {
					position.X = Position.X + ((Size.X - TextElement.GetGlobalBounds().Width) * 0.49f);
				}
				else if (ElementAlign == ButtonElementAlign.RIGHT) {
					position.X = Position.X + Size.X - TextElement.GetGlobalBounds().Width - Margin;
				}

				TextElement.Position = position;
			}
		}

		void Drawable.Draw(RenderTarget _target, RenderStates _states) {
			if (!Show) return;

			Draw(_target, _states);

			if (needUpdate) {
				Rerender();
				needUpdate = false;
			}

			if (ElementType == ButtonElementType.TEXT) {
				_target.Draw(TextElement);
			}
			else if (ElementType == ButtonElementType.ICON) {
				
			}
		}
	}
}