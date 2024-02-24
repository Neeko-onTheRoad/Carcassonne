//====================================================================================================
// IMultySprited.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.System;

namespace nk.Carcassonne {
	public interface IMultyDrawableObject : IDrawableObject {

		public List<Tuple<IDrawableObject, Vector2f>> Objects      { get; protected set; }
		public List<IDrawableObject>        AddedObjects { get; protected set; }

		public void AddObject(IDrawableObject _object);
	}
}
