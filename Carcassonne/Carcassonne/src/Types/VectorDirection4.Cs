//====================================================================================================
// VectorDirection4.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using System.Collections;

namespace nk.Carcassonne {
	public struct VectorDirection4<T>() : IEnumerable<T> {
		public T Top;
		public T Left;
		public T Bottom;
		public T Right;

		public override readonly string ToString() {
			return $"nk.Carcassonne.VectorDirection4<{typeof(T)}>: [{Top}, {Left}, {Bottom}, {Right}]";
		}

		readonly IEnumerator<T> IEnumerable<T>.GetEnumerator() {
			return new VectorDirection4Enum<T>(in this);
		}

		readonly IEnumerator IEnumerable.GetEnumerator() {
			return new VectorDirection4Enum<T>(in this);
		}
	}

	public class VectorDirection4Enum<T> : IEnumerator<T> {
		public T[] Elements = [];

		public VectorDirection4Enum(in VectorDirection4<T> _super) {
			List<T> values = [];

			values.Add(_super.Top);
			values.Add(_super.Left);
			values.Add(_super.Bottom);
			values.Add(_super.Right);

			Elements = [.. values];
		}

		private int position = -1;

		public bool MoveNext() {
			position++;
			return (position < Elements.Length);
		}

		public void Reset() {
			position = 0;
		}

		object IEnumerator.Current {
			get {
				if (Current == null) return new ArgumentNullException();
				return Current;
			}
		}

		void IDisposable.Dispose() {
			GC.SuppressFinalize(this);
		}

		public T Current {
			get {
				try { return Elements[position]; }
				catch (IndexOutOfRangeException) {
					throw new InvalidOperationException();
				}
			}
		}
	}
}
