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
	//====================================================================================================| Flagss

	public enum ButtonElementType {
		TEXT,
		ICON,
		BOTH_TEXT_FIRST,
		BOTH_ICON_FIRST
	}

	public enum ButtonElementAlign {
		LEFT, MIDDLE, RIGHT
	}

	public class Button : RoundedRectangleShape, IDrawableObject {

		//====================================================================================================| State, Flags

		public ButtonElementType  ElementType              { get; set; } = ButtonElementType.TEXT;
		public ButtonElementAlign ElementAlign             { get; set; } = ButtonElementAlign.MIDDLE;
		public ColorSF            NormalColor              { get; set; } = Colors.LightPurple;
		public ColorSF            HoveringColor            { get; set; } = Colors.Purple;
		public ColorSF            TextElementColor         { get; set; } = Colors.White;
		public ColorSF            TextElementHoveringColor { get; set; } = Colors.LightPurple;
		public ColorSF            IconElementColor         { get; set; } = Colors.White;
		public ColorSF            IconElementHoveringColor { get; set; } = Colors.LightPurple;

		public  bool Hovering { get; private set; } = false;
		private byte Hovered = 2;
		private byte Relesed = 0;

		private readonly EventHandler<MouseMoveEventArgs>   MouseMovedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonPressedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonReleasedEvent;

		//====================================================================================================| Constructor

		public Button() {
			Ignore = false;
			CursorChanging = true;
            
            MouseMovedEvent = (_sender, _event) => {
				if (!Show) return;

                if (WindowBase.Hovering == this) { if (Hovered == 2) Hovered = 1; }
				else if (Relesed == 2) { Relesed = 1;}

                if (Hovered == 1) {
                    FillColor = Colors.Purple;
                    WindowBase.Window.SetMouseCursor(new(Cursor.CursorType.Hand));

					if      (ElementType == ButtonElementType.TEXT) { if (TextElement != null) TextElement.FillColor = TextElementHoveringColor; }
					else if (ElementType == ButtonElementType.ICON) { if (IconElement != null) IconElement.Color     = IconElementHoveringColor; }
					else {
						if (TextElement != null) TextElement.FillColor = TextElementHoveringColor;
						if (IconElement != null) IconElement.Color     = IconElementHoveringColor;
					}

					Hovering = true;
					Hovered  = 0;
					Relesed  = 2;
				}
				if (Relesed == 1) {
					FillColor = Colors.LightPurple;

					if (!(WindowBase.Hovering ?? new EmptyObject()).CursorChanging) {
						WindowBase.Window.SetMouseCursor(new(Cursor.CursorType.Arrow));
					}

                    Hovering = false;
					Relesed  = 0;
                    Hovered  = 2;
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

			WindowBase.Window.MouseMoved            += MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  += MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased += MouseButtonReleasedEvent;

			FillColor = Colors.LightPurple;
		}

		//====================================================================================================| Destructor

		~Button() {
            WindowBase.Window.MouseMoved          -= MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  -= MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased -= MouseButtonReleasedEvent;
		}

		//====================================================================================================| Members

		private TextObject? textElement;
		public  TextObject? TextElement {
			get => textElement;
			set {
				textElement = value;
				needUpdate  = true;
			}
		}

		private Sprite? iconElement;
		public  Sprite? IconElement {
			get => iconElement;
			set {
				iconElement = value;
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

		//====================================================================================================| Rerender

		private void Rerender() {

			//============================================================| Text Update

			if (
				ElementType == ButtonElementType.TEXT            ||
				ElementType == ButtonElementType.BOTH_TEXT_FIRST ||
				ElementType == ButtonElementType.BOTH_ICON_FIRST
			) {
				if (textElement != null) {

					Vector2f position = new(0, Position.Y + (Size.Y / 2) - (textElement.CharacterSize * 0.55f));	

					if (ElementAlign == ButtonElementAlign.LEFT) {
						position.X = Position.X + Margin;
					}
					else if (ElementAlign == ButtonElementAlign.MIDDLE) {
						position.X = Position.X + (Size.X - textElement.GetGlobalBounds().Width) * 0.49f;
					}
					else if (ElementAlign == ButtonElementAlign.RIGHT) {
						position.X = Position.X + Size.X - textElement.GetGlobalBounds().Width - Margin;
					}

					textElement.Position  = position;
					textElement.FillColor = TextElementColor;
				}
			}

			//============================================================| Icon Update

			else if (
				ElementType == ButtonElementType.ICON            ||
				ElementType == ButtonElementType.BOTH_TEXT_FIRST ||
				ElementType == ButtonElementType.BOTH_ICON_FIRST
			) {
				if (iconElement != null) {

					Vector2f position = new(0, Position.Y + (Size.Y / 2) - (iconElement.GetGlobalBounds().Height / 2));

					if (ElementAlign == ButtonElementAlign.LEFT) {
						position.X = Position.X + Margin;
					}
					else if (ElementAlign == ButtonElementAlign.MIDDLE) {
						position.X = Position.X + (Size.X - iconElement.GetGlobalBounds().Width) * 0.5f;
					}
					else if (ElementAlign == ButtonElementAlign.RIGHT) {
						position.X = Position.X + Size.X - iconElement.GetGlobalBounds().Width - Margin;
					}

					iconElement.Position = position;
					iconElement.Color    = IconElementColor;
				}
			}
		}

		//====================================================================================================| Overrides

		public new FloatRect GetGlobalBounds() {
			if (Show) return base.GetGlobalBounds();
			else return new(0, 0, 0, 0);
		}

		void Drawable.Draw(RenderTarget _target, RenderStates _states) {
			if (!Show) return;

			Draw(_target, _states);

			if (needUpdate) {
				Rerender();
				needUpdate = false;
			}

			switch (ElementType) {
				case ButtonElementType.TEXT: _target.Draw(TextElement); break;
				case ButtonElementType.ICON: _target.Draw(iconElement); break;
				case ButtonElementType.BOTH_TEXT_FIRST: {
					_target.Draw(TextElement);
					_target.Draw(iconElement);
					break;
				}
				case ButtonElementType.BOTH_ICON_FIRST: {
					_target.Draw(iconElement);
					_target.Draw(TextElement);
					break;
				}
			}
		}
	}
}