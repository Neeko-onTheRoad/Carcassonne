//====================================================================================================
// $itemrootname.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

namespace nk {
	public class StartSceneST {
		public readonly string Connect = "Connect";
		public readonly string Host    = "Host";
		public readonly string Option  = "Option";
		public readonly string Exit    = "Exit";


		public StartSceneST(string _lang) {
			if (_lang == "ko") {
				Connect = "접속";
				Host    = "호스트";
				Option  = "설정";
				Exit    = "종료";
			}
		}
	}
}
