//====================================================================================================
// TileList.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

///
/// This Enum contain base info of Tiles.
/// 
/// First Block show what kind of type that each side has.
/// The order is TOP, LEFT, BOTTOM, RIGHT
///     - P : Plane
///     - R : Road
///     - C : Castle
///     - L : River
///			* and small alphabet behind each latter show how same element connected each other.
///     
/// Same with First, but show type of plane splited.
///		- N : There are no plane at that side
///		- P : There are fool of plane at that side.
///			> small latter behind this show what group that plane included
///		- D : There are two side of plane in that side.
///			> first small latter behind this show what group that left side of plane included.
///			> second small latter behind this show what group that right side of plane included.
///				* Left and Right is based on when the relevant side is facing upward.
///				* If there are only one group of plane, small lastter that show group deosn't exist
///		
/// If teard block exist, that mean there are some spacial Status exist in that tile.
///		- M : Monastery
///		- G : Garden
///		- S : Shield
///		- P : Start or End point of river
///		- I : Inne
///		- C : Catherals
///

namespace nk.Carcassonne {
	public enum TileList {

		// Base

		PPRaP_PPPP_M, // 2
		PPPP_PPPP_M, // 4
		CaCaCaCa_NNNN_S, // 1
		CaRaPCa_NDabPbDba_H, // 4

		CaPPP_NPPP, // 4
		CaPPP_NPPP_G, // 1
		PCaPCa_PaNPbN_S, // 2
		PCaPCa_PaNPbN, // 1
		CaPCbP_PaNPbN, // 2
		CaPCbP_NPNP_G, // 1

		CaPPCb_NPPN, // 1
		CaPPCb_NPPN_G, // 1
		CaRaRaP_NDabDbaPa, // 3
		CaPRaRa_NPaDabDba, // 3
		CaRaRbRc_NDabDbcDca, // 3

		CaCaPP_NNPP_S, // 1
		CaCaPP_NNPP_SG, // 1
		CaCaPP_NNPP, // 2
		CaCaPP_NNPP_G, // 1
		CaRaRaCa_NDabDbaN_S, // 2
		CaRaRaCa_NDabDbaN, // 3

		CaCaPCa_NNPN_S, // 1
		CaCaPCa_NNPN, // 2
		CaCaPCa_NNPN_G, // 1
		CaCaRaCa_NNDabN_S, // 2
		CaCaRaCa_NNDabN, // 1

		RaPRaP_DabPbDbaPa, // 7
		RaPRaP_DabPbDbaPa_G, // 1
		PPRaRa_PaPaDabDba, // 8
		PPRaRa_PaPaDabDba_G, // 1
		PRaRbRc_PaDabDbcDca, // 4
		RaRbRcRd_DabDbcDcdDda, // 1

		// River I

		RaRaLP_DabDbcDca_P, // 1
		CaLRaL_NDabDbcDcdDda, // 2
		CaLCbL_NDabNDba, // 1
		LPLP_DabPbDbaPa, // 1

		CaCaLL_NNDabDba, // 1
		LPPL_DabPbPbDba, // 3
		PLRaL_PaDabDbcDca_M, // 1
		 
		RaLLRa_DabDbcDcbDba, // 1
		PRaRaP_PaDabDbaPa_G, // 1
		LRaLRa_DabDbcDcdDda, // 1
		LPPP_PPPP_M, // 1

		// River II 

		PPLL_PaPaDabDba_G, // 1
		CaLLCa_NDabDbaN_S, // 1
		LRaLRa_DabDbcDcdDda_I, // 1

		LCaLCa_DabNDbaN, // 1
		RaRaLL_DabDbaDbcDca, // 1

		LLLP_DabDbcDca, // 1
		PLPL_PaDabPbDba, // 1
		CaPLP_NPaDabPb, // 1

		// Inns and Cathedrals

		PPRaRa_PaPaDabDba_I,
		PRaPRa_PaDabPbDba_GI,
		PRaRaRa_PaDabDbcDca_I,
		PRaPRb_PaDabPbDba_M,

		RaRbRbRa_DabDbcDcbDba,
		CaRaPRc_NDabPbN,
		PPPCa_PaPbPbN,
		CaCbCcCd_NNNN_G,

		CaRaCbRb_NDabNDcd,
		CaPRaP_NPaDabPb,
		CaCaCaCa_NNNN_C,
		
		CaRaRaCa_NDabDbaN_SI,
		CaPRaRa_NPaDabDba_I,
		CaPRaCa_NPaDabN_I,
		CaCbPCc_NNPN,

		CaPCbCa_NPNN_S,
		RaCaRbCa_DabNDcdN_S

	}
}

