//====================================================================================================
// SceneBase.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Graphics;

namespace nk.Carcassonne {
	public abstract class SceneBase : Drawable {

		public List<IDrawableObject>    Objects { get; protected set; } = [];
		public List<Action<ClientTime>> Updates { get; protected set; } = [];
		
		public abstract void Initialize();

		public void Update(ClientTime _clientTime) {
			Updates.ForEach(i => i(_clientTime));
		}

		void Drawable.Draw(RenderTarget _target, RenderStates _state) {
			Objects.ForEach(i => {
                _target.Draw(i, _state);
			});
		}

	}
}
