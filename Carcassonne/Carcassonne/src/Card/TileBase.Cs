//====================================================================================================
// CardBase.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

namespace nk.Carcassonne {

	public enum SideType {
		NON, LAKE, ROAD, CASTLE
	}

	public class TileBase() {

		public bool       StartCard    { get; protected set; } = false;
		public ChargeInfo Charge       { get; protected set; } = new();
		public bool       HasInne      { get; protected set; } = false;
		public bool       HasShield    { get; protected set; } = false;
		public bool       HasCathedral { get; protected set; } = false;

		public VectorDirection4<Tuple<SideType, VectorDirection4<bool>>>
			Side { get; protected set; } = new();

		
	}
}
