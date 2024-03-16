//====================================================================================================
// LineObject.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Graphics;
using SFML.System;

namespace nk.Carcassonne {
	public class LineObject : IDrawableObject {
		
		public bool Show           { get; set; } = true;
		public bool Ignore         { get; set; } = true;
		public bool CursorChanging { get; set; } = false;
		
		private Vector2f position  = new(0f, 0f);

		private bool needUpdate = true;

		private VertexArray buffer = new VertexArray(PrimitiveType.Lines, 2);
		public LineObject(Vertex _p1 = new(), Vertex _p2 = new()) {
			buffer[0] = _p1;
			buffer[1] = _p2;
		}

		[Obsolete("Don't Use it. use Point1, Point2.")]
		public Vector2f Position {
			get => position;
			set {
				position   = value;
				needUpdate = true;
			}
		}

		public Vertex Point1 {
			get => buffer[0];
			set {
				buffer[0] = value;
			}
		}

		public Vertex Point2 {
			get => buffer[1];
			set {
				buffer[1] = value;
			}
		}

		public Vector2f Color { get; set; }

		//====================================================================================================| Rerender

		private void Rerender() {
			if (!Show) return;

			buffer[0] = new(buffer[0].Position + position, buffer[0].Color);
			buffer[1] = new(buffer[1].Position + position, buffer[1].Color);
		}

		//====================================================================================================| Overrides

		public void Draw(RenderTarget _target, RenderStates _state) {
			if (!Show) return;

			if (needUpdate) {
				Rerender();
				needUpdate = false;
			}

			_target.Draw(buffer, _state);
		}

		public FloatRect GetGlobalBounds() {
			Vector2f p1m = Point1.Position + position;
			Vector2f p2m = Point2.Position + position;

			Vector2f pst;
			Vector2f siz;

			if (p1m.X > p2m.X) {
				pst.X = p2m.X;
				siz.X = p1m.X - p2m.X;
			} else {
				pst.X = p1m.X;
				siz.X = p2m.X - p1m.X;
			}
			
			if (p1m.Y > p2m.Y) {
				pst.Y = p2m.Y;
				siz.Y = p1m.Y - p2m.Y;
			} else {
				pst.Y = p1m.Y;
				siz.Y = p2m.Y - p1m.Y;
			}

			return new(
				pst, siz
			);
		}
	}
}