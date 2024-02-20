//====================================================================================================
// $itemrootname.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Graphics;
using SFML.System;

namespace nk.Carcassonne {
	public class SubWindow : IDrawableObject {

		public bool Show           { get; set; } = true;
		public bool Ignore         { get; set; } = true;
		public bool CursorChanging { get; set; } = false;

		private RenderWindow Window = new(new(0, 0), "");

		public SceneBase    Scene  { get; set; } = new EmptyScene();

		public SubWindow() {
			
		}

		public FloatRect GetGlobalBounds() {
			Vector2i positionInWindowBase = Window.Position - WindowBase.Window.Position;

			return new(
				positionInWindowBase.X, positionInWindowBase.Y,
				Window.Size.X,          Window.Size.Y
			);
		}

		public void Draw(RenderTarget _target, RenderStates _state) {
			Scene.Update(WindowBase.ClientTime);

			Window.Clear(Colors.Gray);
			Window.Draw(Scene);
			Window.Display();
		}

	}
}
