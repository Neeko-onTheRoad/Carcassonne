//====================================================================================================
// InputBox.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace nk.Carcassonne {
	
	public class InputBox : RoundedRectangleShape, IDrawableObject {

		//====================================================================================================| State, Flags

		public bool GetInput { get; set; } = true;
		
		public string InputString { get; private set; } = "";

		private byte Hovered  = 2;
		private byte Released = 0;

		private readonly EventHandler<MouseMoveEventArgs>   MouseMovedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonPressedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonReleasedEvent;
		private readonly EventHandler<TextEventArgs>        TextEnteredEvent;

		private ElementAlign textElementAlign        = ElementAlign.LEFT;
		private float        margin                  = WindowBase.WindowSize.X * 0.005f;
		private ColorSF      backgroundIdleColor     = Colors.Transparent(Colors.Black, 100);
		private ColorSF      backgroundClickingColor = Colors.Transparent(Colors.Black, 50);
		private ColorSF      textIdleColor           = Colors.GrayIn(75);
		private ColorSF      textClickingColor       = Colors.GrayIn(50);
		private ColorSF      inputTextIdleColor      = Colors.GrayIn(25);
		private ColorSF      inputTextClickingColor  = Colors.White;
		private ColorSF      outlineIdleColor        = Colors.LightPurple;
		private ColorSF      outlineClickingColor    = Colors.Purple;
		private TextObject   textElement             = new();
		private TextObject   inputTextStyle          = new();
		private TextObject   textDisplayBuffer       = new();

		//====================================================================================================| Propertys

		public ElementAlign TextElementAlign {
			get => textElementAlign;
			set {
				textElementAlign = value;
				needUpdate       = true;
			}
		}

		public float Margin {
			get => margin;
			set {
				margin     = value;
				needUpdate = true;
			}
		}

		public ColorSF BackgroundIdleColor {
			get => backgroundIdleColor;
			set {
				backgroundIdleColor = value;
				needUpdate          = true;
			}
		}

		public ColorSF BackgroundClickingColor {
			get => backgroundClickingColor;
			set {
				backgroundClickingColor = value;
				needUpdate              = true;
			}
		}

		public ColorSF TextIdleColor {
			get => textIdleColor;
			set {
				textIdleColor = value;
				needUpdate    = true;
			}
		}

		public ColorSF TextClickingColor {
			get => textClickingColor;
			set {
				textClickingColor = value;
				needUpdate        = true;
			}
		}

		public ColorSF OutlineIdleColor {
			get => outlineIdleColor;
			set {
				outlineIdleColor = value;
				needUpdate       = true;
			}
		}

		public ColorSF OutlineClickingColor {
			get => outlineClickingColor;
			set {
				outlineClickingColor = value;
				needUpdate           = true;
			}
		}

		public ColorSF InputTextIdleColor {
			get => inputTextIdleColor;
			set {
				inputTextIdleColor = value;
				needUpdate         = true;
			}
		}

		public ColorSF InputTextClickingColor {
			get => inputTextClickingColor;
			set {
				inputTextClickingColor = value;
				needUpdate             = true;
			}
		}

		public TextObject TextElement {
			get => textElement;
			set {
				textElement = value;
				needUpdate  = true;
			}
		}

		public TextObject InputTextStyle {
			get => inputTextStyle;
			set {
				inputTextStyle = value;
				needUpdate     = true;
			}
		}

		public string DisplayString {
			get => TextElement.DisplayedString;
			set {
				TextElement.DisplayedString = value;
			}
		}

		//====================================================================================================| Constructor

		public InputBox() {
			Ignore           = false;
			CursorChanging   = true;
			FillColor        = BackgroundIdleColor;
			OutlineColor     = OutlineIdleColor;
			OutlineThickness = WindowBase.WindowSize.X * 0.001f;

			textElement.CharacterSize = (uint)(WindowBase.WindowSize.X * 0.01f);
			textElement.FillColor     = TextIdleColor;
			textElement.Font          = Res.Fonts.Normal;

			inputTextStyle.CharacterSize = (uint)(WindowBase.WindowSize.X * 0.01f);

			MouseMovedEvent = (_sender, _event) => {
				if (!Show) return;

				if (WindowBase.Hovering == this) { if (Hovered == 2) Hovered = 1; }
				else if (Released == 2) { Released = 1; }

				if (Hovered == 1) {
					WindowBase.Window.SetMouseCursor(new(Cursor.CursorType.Text));

					Hovered  = 0;
					Released = 2;
				}

				if (Released == 1) {
					if (!(WindowBase.Hovering ?? new EmptyObject()).CursorChanging) {
						WindowBase.Window.SetMouseCursor(new(Cursor.CursorType.Arrow));
					}

					Released = 0;
					Hovered  = 2;
				}
			};

			MouseButtonPressedEvent = (_sender, _event) => {
				if (!Show) return;
				if (_event.Button == Mouse.Button.Left) {
					if (WindowBase.Hovering == this) {
						WindowBase.Clicking = this;
					}
				}
			};

			MouseButtonReleasedEvent = (_sender, _event) => {
				if (_event.Button == Mouse.Button.Left) {
					if (WindowBase.Clicking == this && WindowBase.Hovering == this) {
						WindowBase.Clicking = null;
					}
				}
			};

			TextEnteredEvent = (_sender, _event) => {
				if (!Show || Ignore) return;

				if (WindowBase.LastClicked == this) {
					if (_event.Unicode == "\b") {
						if (InputString.Length > 0) {
							InputString = InputString[..^1];
						}
						return;
					}

					InputString += _event.Unicode;
                }
			};

			WindowBase.Window.MouseMoved          += MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  += MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased += MouseButtonReleasedEvent;
			WindowBase.Window.TextEntered         += TextEnteredEvent;
		}

		//====================================================================================================| Desstructor

		~InputBox() {
			WindowBase.Window.MouseMoved          -= MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  -= MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased -= MouseButtonReleasedEvent;
			WindowBase.Window.TextEntered         -= TextEnteredEvent;
		}

		//====================================================================================================| Rerender

		private void Rerender() {

			TextObject? textElementTemp = null;

			if (InputString == "") {
				textElementTemp = TextElement;
			}
			else {
                textElementTemp = inputTextStyle;
				if (textElementTemp != null) textElementTemp.DisplayedString = InputString;
			}

			if (textElementTemp != null) {
				Vector2f position = new(0, Position.Y + (Size.Y / 2) - (textElementTemp.CharacterSize * 0.55f));

				if (TextElementAlign == ElementAlign.LEFT) {
					position.X = Position.X + Margin;
				}
				else if (TextElementAlign == ElementAlign.MIDDLE) {
					position.X = Position.X + (Size.X - textElementTemp.GetGlobalBounds().Width * 0.49f);
				}
				else if (TextElementAlign == ElementAlign.RIGHT) {
					position.X = Position.X + Size.X - textElementTemp.GetGlobalBounds().Width - Margin;
				}

				textElementTemp.Position = position;
				textDisplayBuffer = textElementTemp;
			}
		}

		//====================================================================================================| Override

		public new void Draw(RenderTarget _target, RenderStates _state) {
			if (!Show) return;
		
			if (needUpdate) {
				Rerender();
			}

			base.Draw(_target, _state);

			_target.Draw(textDisplayBuffer);
			
		}
	}
}
