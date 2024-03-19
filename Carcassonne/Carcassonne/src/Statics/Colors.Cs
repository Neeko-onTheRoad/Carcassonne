//====================================================================================================
// Colors.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using System.Data;

namespace nk.Carcassonne {
	public class Colors {
		public static ColorSF Transparent(ColorSF _color, byte _percent) {
			return new(_color.R, _color.G, _color.B,
				(byte)(_color.A * (_percent / 100f))
			);
		}	

		public static ColorSF GrayIn(byte _darkness) {
			byte normal = (byte)(255 * (_darkness / 100f));
			return new(normal, normal, normal);
		}

		public readonly static ColorSF White     = new(255, 255, 255);
		public readonly static ColorSF LightGray = new(127, 127, 127);
		public readonly static ColorSF Gray      = new(25 , 25 , 25 );
		public readonly static ColorSF Black     = new(0  , 0  , 0  );

		public readonly static ColorSF LightPurple  = new(120, 108, 198);
		public readonly static ColorSF Purple       = new(101, 53,  136);
		public readonly static ColorSF Yellow       = new(255, 247, 0  );
		public readonly static ColorSF Red_True     = new(255, 0  , 0  );
		public readonly static ColorSF Green_True   = new(0  , 255, 0  );
		public readonly static ColorSF Blue_True    = new(0  , 0  , 255);
		public readonly static ColorSF Yellow_True  = new(255, 255, 0  );
		public readonly static ColorSF Magenta_True = new(255, 0  , 255);
		public readonly static ColorSF Cyan_True    = new(0  , 255, 255);

		public readonly static ColorSF TransparentBlack = new(0  , 0  , 0  , 150);
	}
}
