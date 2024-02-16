//====================================================================================================
// $itemrootname.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using System.Windows.Forms;

namespace nk {
	public class StringTable {

		public StringTable(string _lang) {
			string path = $"Carcassonne\\res\\Client\\StringTable_{_lang}.Json";

			try {
				using (FileStream fs = File.OpenRead(path)) {
					
				}
			}
			catch(FileNotFoundException ex) {
				MessageBox.Show($"Language File: {path} isn't exist.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

	}
}
