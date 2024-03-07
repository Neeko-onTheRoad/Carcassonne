//====================================================================================================
// InputBox.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Graphics;
using SFML.Window;

namespace nk.Carcassonne {
	public class InputBox : RoundedRectangleShape, IDrawableObject {

		//====================================================================================================| Members

		private TextObject? textElement;
		public  TextObject? TextElement {
			get => textElement;
			set {
				textElement = value;
				needUpdate  = true;
			}
		}

		private byte Hovered  = 2;
		private byte Released = 0;

		private readonly EventHandler<MouseMoveEventArgs>   MouseMovedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonPressedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonReleasedEvent;

		//====================================================================================================| Constructor

		public InputBox() {
			Ignore = false;
			CursorChanging = true;

			MouseMovedEvent = (_sender, _event) => {
				if (!Show) return;

				if (WindowBase.Hovering == this) { if (Hovered == 2) Hovered = 1; }
				else if (Released == 2) { Released = 1; }

				if (Hovered == 1) {
					FillColor = Colors.TransparentBlack;
					//WindowBase.Window.SetMouseCursor();
				}
			};

			MouseButtonPressedEvent = (_sender, _event) => {
				
			};

			MouseButtonReleasedEvent = (_sender, _event) => {
				
			};

			WindowBase.Window.MouseMoved          += MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  += MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased += MouseButtonReleasedEvent;
		}

		//====================================================================================================| Desstructor

		~InputBox() {
			WindowBase.Window.MouseMoved          -= MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  -= MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased -= MouseButtonReleasedEvent;
		}

		//====================================================================================================| Rerender

		//====================================================================================================| Override

		public new void Draw(RenderTarget _target, RenderStates _state) {
			base.Draw(_target, _state);
		}
	}
}
