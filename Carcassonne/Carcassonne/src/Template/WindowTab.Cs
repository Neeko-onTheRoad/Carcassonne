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

		private bool needUpdate = true;

		public bool Show     { get; set; } = true;
		public bool Movement { get; set; } = false;

		private string   title           = "";
		private ColorSF  borderColor     = Colors.Purple;
		private ColorSF  backgroundColor = Colors.TransparentBlack;
		private Vector2f size            = new(100f, 100f);
		private Vector2f position        = new(0f, 0f);

		public List<IDrawableObject> Objects      { get; set; } = [];
		public List<IDrawableObject> AddedObjects { get; set; } = [];

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

		private bool BorderHovered = false;
		private bool BorderRelesed = false;

		private void Rerender() {
			AddedObjects.Clear();
			Objects.ForEach(AddedObjects.Add);
		}

		private readonly EventHandler<MouseMoveEventArgs>   MouseMovedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonPressedEvent;
		private readonly EventHandler<MouseButtonEventArgs> MouseButtonReleasedEvent;

		public WindowTab() {
            MouseMovedEvent = (_sender, _event) => {
				if (!Show) return;
			};
			MouseButtonPressedEvent = (_sender, _event) => {
				if (!Show) return;
			};
			MouseButtonReleasedEvent = (_sender, _event) => {
				if (!Show) return;
			};

			WindowBase.Window.MouseMoved          += MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  += MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased += MouseButtonReleasedEvent;
		}

		~WindowTab() {
			WindowBase.Window.MouseMoved          -= MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  -= MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased -= MouseButtonReleasedEvent;
		}

		public void AddObject(IDrawableObject _object) => AddedObjects.Add(_object);

		public FloatRect GetGlobalBounds() => new(
			Position.X, Position.Y,
			Size.X, Size.Y
		);
		
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
