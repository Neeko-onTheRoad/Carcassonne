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
	
	public class InputBox : RoundedRectangleShape, IDrawableObject, INeedUpdate {

		//====================================================================================================| State, Flags

		public bool GetInput { get; set; } = true;
		public bool Updating { get; set; } = true;

		public string InputString { get; private set; } = "";

		private byte Hovered  = 2;
		private byte Released = 0;

		private bool nowCursorShowing = true;
		private bool cursorDisplay    = true;
		public  bool CursorDisplay {
			get => cursorDisplay;
			set {
				if (value) CursorBuffer = new();
				else       CursorBuffer = null;

				cursorDisplay = value;
			}
		}
		private LineObject? CursorBuffer = new();

		private bool Selecting   = false;
		private uint SelectStart = 0;
		private uint SelectEnd   = 0;

		private readonly EventHandler<MouseMoveEventArgs>   MouseMovedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonPressedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonReleasedEvent;
		private readonly EventHandler<TextEventArgs>        TextEnteredEvent;

		private ElementAlign textElementAlign        = ElementAlign.LEFT;
		private float        margin                  = WindowBase.WindowSize.X * .005f;
		private ColorSF      backgroundIdleColor     = Colors.Transparent(Colors.Black, 50);
		private ColorSF      backgroundClickingColor = Colors.Transparent(Colors.Black, 100);
		private ColorSF      textIdleColor           = Colors.GrayIn(25);
		private ColorSF      textClickingColor       = Colors.GrayIn(50);
		private ColorSF      inputTextIdleColor      = Colors.GrayIn(25);
		private ColorSF      inputTextClickingColor  = Colors.White;
		private ColorSF      outlineIdleColor        = Colors.LightPurple;
		private ColorSF      outlineClickingColor    = Colors.Purple;
		private ColorSF      cursorColor             = Colors.LightPurple;
		private float        cursorBlinkingDelay     = .5f;
		private TextObject   textElement             = new();
		private TextObject   inputTextStyle          = new();
		private TextObject   textDisplayBuffer       = new();

		public InputBox? Next     { get; set; } = null;
		public InputBox? Previous { get; set; } = null;

		

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

		public ColorSF CursorColor {
			get => cursorColor;
			set {
				cursorColor = value;
			}
		}

		public float CursorBlinkingDelay {
			get => cursorBlinkingDelay;
			set {
				cursorBlinkingDelay = value;
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

				UpdateCursorLocation();
			}
		}

		public string DisplayString {
			get => TextElement.DisplayedString;
			set {
				TextElement.DisplayedString = value;
			}
		}

		//====================================================================================================| Mecro Functions

		private void UpdateCursorLocation() {
			if (CursorDisplay && CursorBuffer != null) {
				CursorBuffer.Point1 = new(new(
					TextElement.GetGlobalBounds().Left +
					TextElement.GetGlobalBounds().Width,
					TextElement.GetGlobalBounds().Top
				), CursorColor);
				CursorBuffer.Point2 = new(new(
					CursorBuffer.Point1.Position.X,
					TextElement.GetGlobalBounds().Top +
					TextElement.GetGlobalBounds().Height
				), CursorColor);
			}
		}

		//====================================================================================================| Constructor

		public InputBox() {
			Ignore           = false;
			CursorChanging   = true;
			FillColor        = BackgroundIdleColor;
			OutlineColor     = OutlineIdleColor;
			OutlineThickness = WindowBase.WindowSize.X * .001f;

			textElement.CharacterSize = (uint)(WindowBase.WindowSize.X * .01f);
			textElement.FillColor     = TextIdleColor;
			textElement.Font          = Res.Fonts.Normal;

			inputTextStyle.CharacterSize = (uint)(WindowBase.WindowSize.X * .01f);

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
                Console.WriteLine((int)_event.Unicode[0]);

                if (WindowBase.LastClicked == this) {
					char keyCode = _event.Unicode[0];
                    if (keyCode == Ascii.BS) {
						if (InputString.Length > 0) {
							InputString = InputString[..^1];
						}
					}

					else if (keyCode == Ascii.DEL) {
						InputString = "";
					}

					else if (keyCode == Ascii.ESC || _event.Unicode[0] == Ascii.CR) {
						WindowBase.LastClicked = WindowBase.Background;
					}

					else if (keyCode == Ascii.HT) {
                        if (Keyboard.IsKeyPressed(Keyboard.Key.LShift)) {
							if (Previous != null && Previous.Show) {
								WindowBase.Clicking = Previous;
							}
						}
                        else if (Next != null && Next.Show) {
							WindowBase.Clicking = Next;
						}
					}

					else InputString += _event.Unicode;
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

			TextObject? textElementTemp;

			if (InputString == "") {
				textElementTemp = TextElement;
			}
			else {
                textElementTemp = inputTextStyle;
				if (textElementTemp != null) textElementTemp.DisplayedString = InputString;
			}

			if (textElementTemp != null) {
				Vector2f position = new(0, Position.Y + (Size.Y / 2) - (textElementTemp.CharacterSize * .55f));

				if (TextElementAlign == ElementAlign.LEFT) {
					position.X = Position.X + Margin;
				}
				else if (TextElementAlign == ElementAlign.MIDDLE) {
					position.X = Position.X + (Size.X - textElementTemp.GetGlobalBounds().Width * .49f);
				}
				else if (TextElementAlign == ElementAlign.RIGHT) {
					position.X = Position.X + Size.X - textElementTemp.GetGlobalBounds().Width - Margin;
				}

				textElementTemp.Position = position;
				textDisplayBuffer = textElementTemp;
			}
		}

		//====================================================================================================| Override

		private float CursorBlinkingBuffer = 0f;
		public void UpdateElement(ClientTime _ct) {
			if (!Show || WindowBase.LastClicked != this) return;
		
			CursorBlinkingBuffer += _ct.DeltaTime;

            if (CursorBlinkingDelay <= CursorBlinkingBuffer) {
				CursorBlinkingBuffer -= CursorBlinkingDelay;
				nowCursorShowing = !nowCursorShowing;
                Console.WriteLine(nowCursorShowing);
            }
		}

		public new void Draw(RenderTarget _target, RenderStates _state) {
			if (!Show) return;
		
			if (needUpdate) {
				Rerender();
			}

			base.Draw(_target, _state);

			_target.Draw(textDisplayBuffer);
			
			if (WindowBase.Clicking == this && nowCursorShowing) {
                Console.WriteLine("aa");
                _target.Draw(CursorBuffer, _state);
			}
		}
	}
}
