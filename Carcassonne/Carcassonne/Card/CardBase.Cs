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

	public class CardBase() {

		public bool StartCard { get; protected set; } = false;
		
		public ChargeInfo Charge { get; protected set; } = new();

		public VectorDirection4<SideType> 

	}
}
