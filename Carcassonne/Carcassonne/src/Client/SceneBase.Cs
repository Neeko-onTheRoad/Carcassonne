﻿//====================================================================================================
// SceneBase.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Graphics;

namespace nk.Carcassonne {
	public abstract class SceneBase() : Drawable {

		public SpriteUnion Sprites { get; protected set; } = [];
		
		public abstract void Initialize(WindowBase _window);
		public abstract void Update(ClientTime _clientTime);

		void Drawable.Draw(RenderTarget _target, RenderStates _state) {
			Sprites.ForEach((i) => {
				_target.Draw(i, _state);
			});
		}

	}
}