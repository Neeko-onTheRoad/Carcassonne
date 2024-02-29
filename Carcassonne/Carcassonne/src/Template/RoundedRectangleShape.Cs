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
	public class RoundedRectangleShape() : Shape, IDrawableObject {

		//====================================================================================================| Status, Flags

		public bool Show           { get; set; } = true;
		public bool Ignore         { get; set; } = true;
		public bool CursorChanging { get; set; } = false;

		protected VertexArray vertexs    = new();
		protected VertexArray outline    = new();
		protected bool        needUpdate = true;

		private ushort   pointCount = 2;
		private float    radius     = 5f;
		private Vector2f size       = new(30, 30);

		//====================================================================================================| Member

		public ushort PointCount {
			get => pointCount;
			set {
				pointCount = value;
				needUpdate = true;
			}
		}

		public Vector2f Size {
			get => size;
			set {
				size = value;
				needUpdate = true;
			}
		}

		public float Radius {
			get => radius;
			set {
				radius = value;
				needUpdate = true;
			}
		}

		public new ColorSF FillColor {
			get => base.FillColor;
			set {
				base.FillColor = value;
				needUpdate = true;
			}
		}

		//====================================================================================================| Rerender

		private void SetEdgePoint(
			VertexArray _target,
			Vector2f    _position,
			float       _radius,
			bool        _xAxisInverse,
			bool        _yAxisInverse,
			bool        _inversDirection,
			ColorSF     _color,
			ref uint    _pointPointer
		) {
			if (_inversDirection) _yAxisInverse = !_yAxisInverse;

			for (ushort i = 1; i <= pointCount; i++) {
				_target[(ushort)_pointPointer++] = new(new(
					_position.X + ,
					_position.Y + 
				), _color);
			}
		}

		private void Rerender() {
			
			//============================================================| Main Element Update
		
            vertexs = new(PrimitiveType.TriangleFan, 8u + (4u * PointCount));

			uint pointPointer = 0;

			vertexs[0] = new(new(
				Position.X + Radius,
				Position.Y
			), FillColor);
			vertexs[1] = new(new(
				Position.X + Size.X - Radius,
				Position.Y
			), FillColor);

			for (ushort i = 1; i <= pointCount; i++) {
				vertexs[1u + i] = new(new(
					Position.X + Size.X - Radius + 
						Radius * (float)Math.Sin(Math.PI * i / (2 * PointCount + 2)),
					Position.Y + Radius - 
						Radius * (float)Math.Cos(Math.PI * i / (2 * PointCount + 2))
				), FillColor);
			}

			vertexs[2u + PointCount] = new(new(
				Position.X + Size.X,
				Position.Y + Radius
			), FillColor);
			vertexs[3u + PointCount] = new(new(
				Position.X + Size.X,
				Position.Y + Size.Y - Radius
			), FillColor);
			
			for (ushort i = 1; i <= pointCount; i++) {
				vertexs[3u + PointCount + i] = new(new(
					Position.X + Size.X - Radius + 
						Radius * (float)Math.Sin(Math.PI * (i + PointCount + 1) / (2 * PointCount + 2)),
					Position.Y + Size.Y - Radius -
						Radius * (float)Math.Cos(Math.PI * (i + PointCount + 1) / (2 * PointCount + 2))
				), FillColor);
			}

			vertexs[4u + (2u * PointCount)] = new(new(
				Position.X + Size.X - Radius,
				Position.Y + Size.Y
			), FillColor);
			vertexs[5u + (2u * PointCount)] = new(new(
				Position.X + Radius,
				Position.Y + Size.Y
			), FillColor);

			for (ushort i = 1; i <= pointCount; i++) {
				vertexs[5u + (2u * PointCount) + i] = new(new(
					Position.X + Radius -
						Radius * (float)Math.Sin(Math.PI * i / (2 * PointCount + 2)),
					Position.Y + Size.Y - Radius +
						Radius * (float)Math.Cos(Math.PI * i / (2 * PointCount + 2))
				), FillColor);
			}

			vertexs[6u + (3u * PointCount)] = new(new(
				Position.X,
				Position.Y + Size.Y - Radius
			), FillColor);
			vertexs[7u + (3u * PointCount)] = new(new(
				Position.X,
				Position.Y + Radius
			), FillColor);
			
			for (ushort i = 1; i <= pointCount; i++) {
				vertexs[7u + (3u * PointCount) + i] = new(new(
					Position.X + Radius -
						Radius * (float)Math.Sin(Math.PI * (i + PointCount + 1) / (2 * PointCount + 2)),
					Position.Y + Radius +
						Radius * (float)Math.Cos(Math.PI * (i + PointCount + 1) / (2 * PointCount + 2))
				), FillColor);
			}

			//============================================================| In Outline Update

			outline = new(PrimitiveType.Points, 16u + (8u * PointCount));

			outline[0] = new(new(
				Position.X + Radius,
				Position.Y
			), OutlineColor);
			outline[1] = new(new(
				Position.X + Size.X - Radius,
				Position.Y
			), OutlineColor);

			for (ushort i = 1; i <= pointCount; i++) {
				outline[1u + i] = new(new(
					Position.X + Size.X - Radius + 
					Radius * (float)Math.Sin(Math.PI * i / (2 * PointCount + 2)),

					Position.Y + Radius - 
					Radius * (float)Math.Cos(Math.PI * i / (2 * PointCount + 2))
				), OutlineColor);
			}

			outline[2u + PointCount] = new(new(
				Position.X + Size.X,
				Position.Y + Radius
			), OutlineColor);
			outline[3u + PointCount] = new(new(
				Position.X + Size.X,
				Position.Y + Size.Y - Radius
			), OutlineColor);
			
			for (ushort i = 1; i <= pointCount; i++) {
				outline[3u + PointCount + i] = new(new(
					Position.X + Size.X - Radius + 
					Radius * (float)Math.Sin(Math.PI * (i + PointCount + 1) / (2 * PointCount + 2)),

					Position.Y + Size.Y - Radius -
					Radius * (float)Math.Cos(Math.PI * (i + PointCount + 1) / (2 * PointCount + 2))
				), OutlineColor);
			}

			outline[4u + (2u * PointCount)] = new(new(
				Position.X + Size.X - Radius,
				Position.Y + Size.Y
			), OutlineColor);
			outline[5u + (2u * PointCount)] = new(new(
				Position.X + Radius,
				Position.Y + Size.Y
			), OutlineColor);

			for (ushort i = 1; i <= pointCount; i++) {
				outline[5u + (2u * PointCount) + i] = new(new(
					Position.X + Radius -
					Radius * (float)Math.Sin(Math.PI * i / (2 * PointCount + 2)),

					Position.Y + Size.Y - Radius +
					Radius * (float)Math.Cos(Math.PI * i / (2 * PointCount + 2))
				), OutlineColor);
			}

			outline[6u + (3u * PointCount)] = new(new(
				Position.X,
				Position.Y + Size.Y - Radius
			), OutlineColor);
			outline[7u + (3u * PointCount)] = new(new(
				Position.X,
				Position.Y + Radius
			), OutlineColor);
			
			for (ushort i = 1; i <= pointCount; i++) {
				outline[7u + (3u * PointCount) + i] = new(new(
					Position.X + Radius -
					Radius * (float)Math.Sin(Math.PI * (i + PointCount + 1) / (2 * PointCount + 2)),

					Position.Y + Radius +
					Radius * (float)Math.Cos(Math.PI * (i + PointCount + 1) / (2 * PointCount + 2))
				), OutlineColor);
			}

			//============================================================| Out Outline Update

			outline[8u + (4u * PointCount)] = new(new(
				Position.X + Radius,
				Position.Y - OutlineThickness
			), OutlineColor);

			for (ushort i = 1; i <= pointCount; i++) {
				outline[8u + (4u * PointCount) + i] = new(new(
					Position.X + Radius - 
					(Radius + OutlineThickness) * (float)Math.Sin(Math.PI * i / (2 * PointCount + 2)),

					Position.Y + Radius - 
					(Radius + OutlineThickness) * (float)Math.Cos(Math.PI * i / (2 * PointCount + 2))
				), OutlineColor);
			}

		}

		//====================================================================================================| Override

		public new void Draw(RenderTarget _target, RenderStates _states) {
			if (!Show)      return;
			if (needUpdate) Rerender();

			_target.Draw(vertexs, _states);

			if (OutlineThickness == 0f) return;
			_target.Draw(outline, _states);
		}

		void Drawable.Draw(RenderTarget _target, RenderStates _states) {
			Draw(_target, _states);
		}

		public override Vector2f GetPoint(uint index) {
			return vertexs[index].Position;
		}

		public override uint GetPointCount() {
			return vertexs.VertexCount;
		}

		public new FloatRect GetGlobalBounds() {
			return new (
				Position.X, Position.Y, Size.X, Size.Y
			);
		}

	}
}
