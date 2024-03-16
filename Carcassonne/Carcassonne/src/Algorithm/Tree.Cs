//====================================================================================================
// Tree.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

namespace nk.Carcassonne.Alg {
	public static class Tree {
		public static void DFT(List<IDrawableObject> _objects, Action<IDrawableObject> _action) {
			IMultyDrawableObject objectsTemp = new EmptyMultyObject();
			_objects.ForEach(objectsTemp.AddObject);

			DFT(objectsTemp, _action);
		}

		public static void DFT(IDrawableObject _objects, Action<IDrawableObject> _action) {
			_action(_objects);
			if (_objects is IMultyDrawableObject i_) {
				
				foreach(var i in i_.Objects) {
					DFT(i.Item1, _action);
				}

			}
			else {
				return;
			}
		}
	}
}