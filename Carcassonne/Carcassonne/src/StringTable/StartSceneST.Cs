//====================================================================================================
// $itemrootname.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

namespace nk.Carcassonne {
	public class StartSceneST {
		public readonly string Connect = "Connect";
		public readonly string Host    = "Host";
		public readonly string Option  = "Option";
		public readonly string Exit    = "Exit";

		public  readonly string Comment;
		private readonly string[] Comments = [
			"It's free!", "How to make a nuclear bomb", "I need AK-47", "Hello, World!",
			"Click here to Get 1M robux", "Don't play Valorant", "Josh Hutcherson - Whistle"
		];

		public readonly string Addres = "Addres";
		public readonly string Port   = "Port";

		public StartSceneST(string _lang){

			{ Random rand = new();
				Comment = Comments[rand.Next(0, Comments.Length)];
			}

			if (_lang == "ko") {
				Connect = "접속";
				Host    = "호스트";
				Option  = "설정";
				Exit    = "종료";

				Addres = "주소";
				Port   = "포트";
			}
		}
	}
}