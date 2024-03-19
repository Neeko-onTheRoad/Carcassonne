//====================================================================================================
// SceneBase.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Graphics;
using SFML.Window;

namespace nk.Carcassonne {
	public abstract class SceneBase : Drawable {

		public List<IDrawableObject>    Objects { get; protected set; } = [];
		public List<Action<ClientTime>> Updates { get; protected set; } = [];
		
		public void Initialize() {
            InitializeBase();

			Alg.Tree.DFT(Objects, i => {
				if (i is INeedUpdate i_) {
					Updates.Add(i_.UpdateElement);
				}
			});

		}

		public abstract void InitializeBase();

		public void Update(ClientTime _clientTime) {
			Updates.ForEach(i => i(_clientTime));
		}

		void Drawable.Draw(RenderTarget _target, RenderStates _state) {
			Objects.ForEach(i => {
				_target.Draw(i, _state);
			});
		}

		protected EventHandler?                       CloseButtonEvent;
		protected EventHandler<MouseMoveEventArgs>?   MouseMovedEvent;
		protected EventHandler<MouseButtonEventArgs>? MouseButtonPressedEvent;
		protected EventHandler<MouseButtonEventArgs>? MouseButtonReleasedEvent;

		public SceneBase() {
			WindowBase.Window.Closed              += CloseButtonEvent;
			WindowBase.Window.MouseMoved          += MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  += MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased += MouseButtonReleasedEvent;
		}

		~SceneBase() {
			Updates.Clear();

            WindowBase.Window.Closed              -= CloseButtonEvent;
			WindowBase.Window.MouseMoved          -= MouseMovedEvent;
			WindowBase.Window.MouseButtonPressed  -= MouseButtonPressedEvent;
			WindowBase.Window.MouseButtonReleased -= MouseButtonReleasedEvent;
		}

	}
}
