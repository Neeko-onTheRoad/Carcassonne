//====================================================================================================
// ChargeInfo.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.System;

namespace nk.Carcassonne {
	public class ChargeInfo() {

		public bool     Exist    { get; private set; } = false;
		public Vector2f Position { get; private set; } = new(0f, 0f);
		
		public Player?                Owner      { get; set; } = new();
		public VectorDirection8<bool> NearbyCard { get; set; } = new();

		public byte Withdrawal() {
			byte Point = 0;
			foreach (var i in NearbyCard) Point += (i ? (byte)1 : (byte)0);

			Owner = null;
			return Point;
		}
	}
}
