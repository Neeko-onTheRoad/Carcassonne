//====================================================================================================
// Button.Cs
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

	public enum ElementAlign {
		LEFT, MIDDLE, RIGHT
	}

	public class Button : RoundedRectangleShape, IDrawableObject {

		//====================================================================================================| State, Flags

		private ColorSF           backgroundIdleColor      = Colors.LightPurple;
		private ColorSF           backgroundHoveringColor  = Colors.Purple;
		private ColorSF           textElementIdleColor     = Colors.White;
		private ColorSF           textElementHoveringColor = Colors.White;
		private ColorSF           iconElementIdleColor     = Colors.White;
		private ColorSF           iconElementHoveringColor = Colors.White;
		private ButtonElementType elementType              = ButtonElementType.TEXT;
		private ElementAlign      elementAlign             = ElementAlign.MIDDLE;
		private TextObject?       textElement              = null;
		private Sprite?           iconElement              = null;
		private float             margin                   = WindowBase.WindowSize.X * .02f;

		public  bool Hovering { get; private set; } = false;
		private byte Hovered  = 2;
		private byte Released = 0;

		private readonly EventHandler<MouseMoveEventArgs>   MouseMovedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonPressedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonReleasedEvent;


		//====================================================================================================| Propertys

		public ColorSF BackgroundIdleColor {
			get => backgroundIdleColor;
			set {
				backgroundIdleColor = value;
				needUpdate          = true;
			}
		}

		public ColorSF BackgroundHoveringColor {
			get => backgroundHoveringColor;
			set {
				backgroundHoveringColor = value;
				needUpdate              = true;
			}
		}

		public ColorSF TextElementIdleColor {
			get => textElementIdleColor;
			set {
				TextElementIdleColor = value;
				needUpdate           = true;
			}
		}

		public ColorSF TextElementHoveringColor {
			get => textElementHoveringColor;
			set {
				textElementHoveringColor = value;
				needUpdate               = true;
			}
		}
		
		public ColorSF IconElementIdleColor {
			get => iconElementIdleColor;
			set {
				iconElementIdleColor = value;
				needUpdate           = true;
			}
		}

		public ColorSF IconElementHoveringColor {
			get => iconElementHoveringColor;
			set {
				iconElementHoveringColor = value;
				needUpdate               = true;
			}
		}

		public ButtonElementType ElementType { 
			get => elementType;
			set {
				elementType = value;
				needUpdate  = true;
			}
		}
		
		public ElementAlign ElementAlign {
			get => elementAlign;
			set {
				elementAlign = value;
				needUpdate   = true;
			}
		} 

		public  TextObject? TextElement {
			get => textElement;
			set {
				textElement = value;
				needUpdate  = true;
			}
		}

		public  Sprite? IconElement {
			get => iconElement;
			set {
				iconElement = value;
				needUpdate  = true;
			}
		}

		public  float Margin {
			get => margin;
			set {
				margin     = value;
				needUpdate = true;
			}
		}

		public Action OnClickEvent { get; set; } = () => {};

		//====================================================================================================| Constructor

		public Button() {
			Ignore = false;
			CursorChanging = true;
            
            MouseMovedEvent = (_sender, _event) => {
				if (!Show || Ignore) return;

                if (WindowBase.Hovering == this) { if (Hovered == 2) Hovered = 1; }
				else if (Released == 2) { Released = 1;}

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
					Released = 2;
				}

				if (Released == 1) {
					FillColor = Colors.LightPurple;

					if (!(WindowBase.Hovering ?? new EmptyObject()).CursorChanging) {
						WindowBase.Window.SetMouseCursor(new(Cursor.CursorType.Arrow));
					}

					if      (ElementType == ButtonElementType.TEXT) { if (TextElement != null) TextElement.FillColor = TextElementIdleColor; }
					else if (ElementType == ButtonElementType.ICON) { if (IconElement != null) IconElement.Color     = IconElementIdleColor; }
					else {
						if (TextElement != null) TextElement.FillColor = TextElementIdleColor;
						if (IconElement != null) IconElement.Color     = IconElementIdleColor;
					}

                    Hovering = false;
					Released = 0;
                    Hovered  = 2;
				}

			};

			MouseButtonPressedEvent = (_sender, _event) => {
				if (!Show || Ignore) return;
				if (_event.Button == Mouse.Button.Left) {
                    if (WindowBase.Hovering == this) WindowBase.Clicking = this;
				}
			};

			MouseButtonReleasedEvent = (_sender, _event) => {
				if (_event.Button == Mouse.Button.Left) {
                    if (WindowBase.Clicking == this && WindowBase.Hovering == this) {
                        if (Show && !Ignore) OnClickEvent();
						WindowBase.Clicking = null;
                    }
				}
			};

			WindowBase.Window.MouseMoved          += MouseMovedEvent;
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

		//====================================================================================================| Rerender

		private void Rerender() {

			//============================================================| Text Update

			if (
				ElementType == ButtonElementType.TEXT            ||
				ElementType == ButtonElementType.BOTH_TEXT_FIRST ||
				ElementType == ButtonElementType.BOTH_ICON_FIRST
			) {
				if (TextElement != null) {

					Vector2f position = new(0, Position.Y + (Size.Y / 2) - (TextElement.CharacterSize * .55f));	

					if (ElementAlign == ElementAlign.LEFT) {
						position.X = Position.X + Margin;
					}
					else if (ElementAlign == ElementAlign.MIDDLE) {
						position.X = Position.X + (Size.X - TextElement.GetGlobalBounds().Width) * .49f;
					}
					else if (ElementAlign == ElementAlign.RIGHT) {
						position.X = Position.X + Size.X - TextElement.GetGlobalBounds().Width - Margin;
					}

					TextElement.Position  = position;
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

					if (ElementAlign == ElementAlign.LEFT) {
						position.X = Position.X + Margin;
					}
					else if (ElementAlign == ElementAlign.MIDDLE) {
						position.X = Position.X + (Size.X - iconElement.GetGlobalBounds().Width) * .5f;
					}
					else if (ElementAlign == ElementAlign.RIGHT) {
						position.X = Position.X + Size.X - iconElement.GetGlobalBounds().Width - Margin;
					}

					iconElement.Position = position;
					iconElement.Color    = IconElementIdleColor;
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