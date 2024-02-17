//====================================================================================================
// $itemrootname.Cs
// Copyright (C) 2024~ Neeko_onTheRoad
//----------------------------------------------------------------------------------------------------
// CC BY-NC
// https://creativecommons.org/licenses/by-nc/4.0/deed.en
//====================================================================================================

using SFML.Graphics;
using SFML.System;

namespace nk.Carcassonne {
	public class RoundedRectangleShape() : Shape, Drawable {

		protected VertexArray vertexs    = new();
		protected bool        needUpdate = true;

		public override Vector2f GetPoint(uint index) {
			return vertexs[index].Position;
		}

		public override uint GetPointCount() {
			return vertexs.VertexCount;
		}

		public bool Contains(float _x, float _y) {
			return(
				(Position.X <= _x) &&
				(Position.Y <= _y) &&
				(Position.X + Size.X >= _x) &&
				(Position.Y + Size.Y >= _y)
			);
		}

		public bool GetHovering() {
			return Contains(MouseListener.Position.X, MouseListener.Position.Y);
		}

		private ushort pointCount = 2;
		public  ushort PointCount {
			get => pointCount;
			set {
				pointCount = value;
				needUpdate = true;
			}
		}

		private Vector2f size = new(30, 30);
		public  Vector2f Size {
			get => size;
			set {
				size = value;
				needUpdate = true;
			}
		}

		private float radius = 5f;
		public  float Radius {
			get => radius;
			set {
				radius = value;
				needUpdate = true;
			}
		}

		private ColorSF color = new(255, 255, 255);
		public  ColorSF Color {
			get => color;
			set {
				color = value;
				needUpdate = true;
			}
		}

		private void Rerender() {
            vertexs = new(PrimitiveType.TriangleFan, 8u + (4u * PointCount));

			vertexs[0] = new(new(
				Position.X + Radius,
				Position.Y
			), Color);
			vertexs[1] = new(new(
				Position.X + Size.X - Radius,
				Position.Y
			), Color);

			for (ushort i = 1; i <= pointCount; i++) {
				vertexs[1u + i] = new(new(
					Position.X + Size.X - Radius + 
						Radius * (float)Math.Sin(Math.PI * i / (2 * PointCount + 2)),
					Position.Y + Radius - 
						Radius * (float)Math.Cos(Math.PI * i / (2 * PointCount + 2))
				), Color);
			}

			vertexs[2u + PointCount] = new(new(
				Position.X + Size.X,
				Position.Y + Radius
			), Color);
			vertexs[3u + PointCount] = new(new(
				Position.X + Size.X,
				Position.Y + Size.Y - Radius
			), Color);
			
			for (ushort i = 1; i <= pointCount; i++) {
				vertexs[3u + PointCount + i] = new(new(
					Position.X + Size.X - Radius + 
						Radius * (float)Math.Sin(Math.PI * (i + PointCount + 1) / (2 * PointCount + 2)),
					Position.Y + Size.Y - Radius -
						Radius * (float)Math.Cos(Math.PI * (i + PointCount + 1) / (2 * PointCount + 2))
				), Color);
			}

			vertexs[4u + (2u * PointCount)] = new(new(
				Position.X + Size.X - Radius,
				Position.Y + Size.Y
			), Color);
			vertexs[5u + (2u * PointCount)] = new(new(
				Position.X + Radius,
				Position.Y + Size.Y
			), Color);

			for (ushort i = 1; i <= pointCount; i++) {
				vertexs[5u + (2u * PointCount) + i] = new(new(
					Position.X + Radius -
						Radius * (float)Math.Sin(Math.PI * i / (2 * PointCount + 2)),
					Position.Y + Size.Y - Radius +
						Radius * (float)Math.Cos(Math.PI * i / (2 * PointCount + 2))
				), Color);
			}

			vertexs[6u + (3u * PointCount)] = new(new(
				Position.X,
				Position.Y + Size.Y - Radius
			), Color);
			vertexs[7u + (3u * PointCount)] = new(new(
				Position.X,
				Position.Y + Radius
			), Color);
			
			for (ushort i = 1; i <= pointCount; i++) {
				vertexs[7u + (3u * PointCount) + i] = new(new(
					Position.X + Radius -
						Radius * (float)Math.Sin(Math.PI * (i + PointCount + 1) / (2 * PointCount + 2)),
					Position.Y + Radius +
						Radius * (float)Math.Cos(Math.PI * (i + PointCount + 1) / (2 * PointCount + 2))
				), Color);
			}
		}

		public new void Draw(RenderTarget _target, RenderStates _states) {
			if (needUpdate) Rerender();

			_target.Draw(vertexs, _states);
		}

		void Drawable.Draw(RenderTarget _target, RenderStates _states) {
			Draw(_target, _states);
		}

	}
}
