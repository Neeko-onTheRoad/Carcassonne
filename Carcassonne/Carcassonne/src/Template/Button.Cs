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

	public class Button : RoundedRectangleShape, IDrawableObject {

		public ButtonElementType  ElementType   { get; set; } = ButtonElementType.TEXT;
		public ButtonElementAlign ElementAlign  { get; set; } = ButtonElementAlign.MIDDLE;
		public bool               Show          { get; set; } = true;
		public bool               Ignore        { get; set; } = false;
		public ColorSF            NormalColor   { get; set; } = Colors.LightPurple;
		public ColorSF            HoveringColor { get; set; } = Colors.Purple;

		public  bool Hovering { get; private set; } = false;
		private bool Hovered = false;
		private byte Relesed = 0;

		private readonly EventHandler<MouseMoveEventArgs>   MouseMovedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonPressedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonReleasedEvent;

		public Button() {
			MouseMovedEvent = (_sender, _event) => {
				if (!Show) return;

                if (WindowBase.Hovering == this) Hovered = true;
				else if (Relesed == 2) Relesed = 1;

				if (Hovered) {
                    Color = Colors.Purple;
					WindowBase.Window.SetMouseCursor(new(Cursor.CursorType.Hand));

					Hovering = true;
					Hovered  = false;
					Relesed  = 2;
				}
				if (Relesed == 1) {
					Color = Colors.LightPurple;
					WindowBase.Window.SetMouseCursor(new(Cursor.CursorType.Arrow));

                    Hovering = false;
					Relesed  = 0;
				}
			};

			MouseButtonPressedEvent = (_sender, _event) => {
				if (!Show) return;
				if (_event.Button == Mouse.Button.Left) {
                    if (WindowBase.Hovering == this) WindowBase.Clicking = this;
				}
			};

			MouseButtonReleasedEvent = (_sender, _event) => {
				if (!Show) return;
				if (_event.Button == Mouse.Button.Left) {
                    if (WindowBase.Clicking == this && WindowBase.Hovering == this) {
                        OnClickEvent();
                    }
				}
			};

			WindowBase.Window.MouseMoved          += MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  += MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased += MouseButtonReleasedEvent;

			Color = Colors.LightPurple;
		}

		~Button() {
            WindowBase.Window.MouseMoved          -= MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  -= MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased -= MouseButtonReleasedEvent;
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

		public new FloatRect GetGlobalBounds() {
			if (Show) {
				return base.GetGlobalBounds();
			}
			else return new(0, 0, 0, 0);
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