//====================================================================================================
// VectorDirection8.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using System.Collections;

namespace nk.Carcassonne {
	public struct VectorDirection8<T>() : IEnumerable<T> {
		public T Top;
		public T TopLeft;
		public T Left;
		public T BottomLeft;
		public T Bottom;
		public T BottomRight;
		public T Right;
		public T TopRight;

		public override readonly string ToString() {
			return $"nk.Carcassonne.VectorDirection8<{typeof(T)}>: [{Top}, {TopLeft}, {Left}, {BottomLeft}, {Bottom}, {BottomRight}, {Right}, {TopRight}]";
		}

		readonly IEnumerator<T> IEnumerable<T>.GetEnumerator() {
			return new VectorDirection8Enum<T>(in this);
		}
		
		readonly IEnumerator IEnumerable.GetEnumerator() {
			return new VectorDirection8Enum<T>(in this);
		}
	}

	public class VectorDirection8Enum<T> : IEnumerator<T> {
		public T[] Elements = [];

		public VectorDirection8Enum(in VectorDirection8<T> _super) {
			List<T> values = [];

			values.Add(_super.Top);
			values.Add(_super.TopLeft);
			values.Add(_super.Left);
			values.Add(_super.BottomLeft);
			values.Add(_super.Bottom);
			values.Add(_super.BottomRight);
			values.Add(_super.Right);
			values.Add(_super.TopRight);

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
				if (Current == null) throw new ArgumentNullException();
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
