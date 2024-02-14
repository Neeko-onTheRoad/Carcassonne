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
		
		public Player? Owner { get; set; } = new();

		public void Withdrawal() {
			if (Owner == null) {
				return;
			}

			Meeples meepleTemp = Owner.Meeples;
			meepleTemp.Monk = true;
			Owner.Meeples = meepleTemp;
		}
	}
}
