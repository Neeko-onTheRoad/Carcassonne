//====================================================================================================
// ClientTime.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

namespace nk.Carcassonne {
	public class ClientTime() {
		
		private float deltaTime = 0f;
		public  float TimeScale { get; set; } = 1f;

		public float DeltaTime {
			get { return deltaTime * TimeScale; }
			set { deltaTime = value; }
		}

		public float DeltaTimeUnscaled {
			get { return deltaTime; }
		}

		public float TotalTimeElapsed { get; private set; }

		public void Update(float _deltaTime, float _totalTimeElapsed) {
			deltaTime        = _deltaTime;
			TotalTimeElapsed = _totalTimeElapsed;
		}

	}
}