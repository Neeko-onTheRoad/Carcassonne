//====================================================================================================
// RoundedRectangleShape.Cs
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

		private void SetEdgePoint (
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
					_position.X + (_xAxisInverse ? _radius : -_radius) -
					(_xAxisInverse ? _radius : -_radius) * (float)Math.Sin(Math.PI * (_inversDirection ? (i + PointCount + 1) : i) / (2 * PointCount + 2)),

					_position.Y + (_yAxisInverse ? _radius : -_radius) * (_inversDirection ? -1 : 1) -
					(_yAxisInverse ? _radius : -_radius) * (float)Math.Cos(Math.PI * (_inversDirection ? (i + PointCount + 1) : i) / (2 * PointCount + 2))
				), _color);
			}
		}

		private void SetEdgePointDouble (
			VertexArray _target,
			Vector2f    _position,
			float       _radius,
			float       _offset,
			bool        _xAxisInverse,
			bool        _yAxisInverse,
			bool        _inversDirection,
			ColorSF     _color,
			ref uint    _pointPointer
		) {
			if (_inversDirection) _yAxisInverse = !_yAxisInverse;

			float outLine = _radius + _offset;
			float xPos    = _position.X - (_xAxisInverse ? _offset : -_offset);
			float yPos    = _position.Y - (_yAxisInverse ? _offset : -_offset) * (_inversDirection ? -1 : 1);

			for (ushort i = 1; i <= pointCount; i++) {
				_target[(ushort)_pointPointer++] = new(new(
					_position.X + (_xAxisInverse ? _radius : -_radius) -
					(_xAxisInverse ? _radius : -_radius) * (float)Math.Sin(Math.PI * (_inversDirection ? (i + PointCount + 1) : i) / (2 * PointCount + 2)),

					_position.Y + (_yAxisInverse ? _radius : -_radius) * (_inversDirection ? -1 : 1) -
					(_yAxisInverse ? _radius : -_radius) * (float)Math.Cos(Math.PI * (_inversDirection ? (i + PointCount + 1) : i) / (2 * PointCount + 2))
				), _color);

				_target[(ushort)_pointPointer++] = new(new(
					xPos + (_xAxisInverse ? outLine : -outLine) -
					(_xAxisInverse ? outLine : -outLine) * (float)Math.Sin(Math.PI * (_inversDirection ? (i + PointCount + 1) : i) / (2 * PointCount + 2)),

					yPos + (_yAxisInverse ? outLine : -outLine) * (_inversDirection ? -1 : 1) -
					(_yAxisInverse ? outLine : -outLine) * (float)Math.Cos(Math.PI * (_inversDirection ? (i + PointCount + 1) : i) / (2 * PointCount + 2))
				), _color);
			}
		}

		private void Rerender() {
			//PointCount = 10;
			
			//============================================================| Main Element Update

            vertexs = new(PrimitiveType.TriangleFan, 8u + (4u * PointCount));

			uint pointPointer = 0;

			vertexs[pointPointer++] = new(new(
				Position.X + Radius,
				Position.Y
			), FillColor);
			vertexs[pointPointer++] = new(new(
				Position.X + Size.X - Radius,
				Position.Y
			), FillColor);

			SetEdgePoint(
				vertexs, new Vector2f(Position.X + Size.X, Position.Y), Radius,
				false, true, false, FillColor,
				ref pointPointer
			);

			vertexs[pointPointer++] = new(new(
				Position.X + Size.X,
				Position.Y + Radius
			), FillColor);
			vertexs[pointPointer++] = new(new(
				Position.X + Size.X,
				Position.Y + Size.Y - Radius
			), FillColor);
			
			SetEdgePoint(
				vertexs, new Vector2f(Position.X + Size.X, Position.Y + Size.Y), Radius,
				false, false, true, FillColor,
				ref pointPointer
			);

			vertexs[pointPointer++] = new(new(
				Position.X + Size.X - Radius,
				Position.Y + Size.Y
			), FillColor);
			vertexs[pointPointer++] = new(new(
				Position.X + Radius,
				Position.Y + Size.Y
			), FillColor);

			SetEdgePoint(
				vertexs, new Vector2f(Position.X, Position.Y + Size.Y), Radius,
				true, false, false, FillColor,
				ref pointPointer
			);

			vertexs[pointPointer++] = new(new(
				Position.X,
				Position.Y + Size.Y - Radius
			), FillColor);
			vertexs[pointPointer++] = new(new(
				Position.X,
				Position.Y + Radius
			), FillColor);
			
			SetEdgePoint(
				vertexs, new Vector2f(Position.X, Position.Y), Radius,
				true, true, true, FillColor,
				ref pointPointer
			);

			//============================================================| In Outline Update
			PointCount = 3;

			outline = new(PrimitiveType.TriangleStrip, 18u + (8u * PointCount));
			pointPointer = 0;

			outline[pointPointer++] = new(new(
				Position.X + Radius,
				Position.Y
			), OutlineColor);
			outline[pointPointer++] = new(new(
				Position.X + Radius,
				Position.Y - OutlineThickness
			), OutlineColor);

			outline[pointPointer++] = new(new(
				Position.X + Size.X - Radius,
				Position.Y
			), OutlineColor);
			outline[pointPointer++] = new(new(
				Position.X + Size.X - Radius,
				Position.Y - OutlineThickness
			), OutlineColor);

			SetEdgePointDouble(
				outline, new Vector2f(Position.X + Size.X, Position.Y), Radius, OutlineThickness,
				false, true, false, OutlineColor,
				ref pointPointer
			);

			outline[pointPointer++] = new(new(
				Position.X + Size.X,
				Position.Y + Radius
			), OutlineColor);
			outline[pointPointer++] = new(new(
				Position.X + Size.X + OutlineThickness,
				Position.Y + Radius
			), OutlineColor);
			outline[pointPointer++] = new(new(
				Position.X + Size.X,
				Position.Y + Size.Y - Radius
			), OutlineColor);
			outline[pointPointer++] = new(new(
				Position.X + Size.X + OutlineThickness,
				Position.Y + Size.Y - Radius
			), OutlineColor);
			
			SetEdgePointDouble(
				outline, new Vector2f(Position.X + Size.X, Position.Y + Size.Y), Radius, OutlineThickness,
				false, false, true, OutlineColor,
				ref pointPointer
			);

			outline[pointPointer++] = new(new(
				Position.X + Size.X - Radius,
				Position.Y + Size.Y
			), OutlineColor);
			outline[pointPointer++] = new(new(
				Position.X + Size.X - Radius,
				Position.Y + Size.Y + OutlineThickness
			), OutlineColor);
			outline[pointPointer++] = new(new(
				Position.X + Radius,
				Position.Y + Size.Y
			), OutlineColor);
			outline[pointPointer++] = new(new(
				Position.X + Radius,
				Position.Y + Size.Y + OutlineThickness
			), OutlineColor);

			SetEdgePointDouble(
				outline, new Vector2f(Position.X, Position.Y + Size.Y), Radius, OutlineThickness,
				true, false, false, OutlineColor,
				ref pointPointer
			);

			outline[pointPointer++] = new(new(
				Position.X,
				Position.Y + Size.Y - Radius
			), OutlineColor);
			outline[pointPointer++] = new(new(
				Position.X - OutlineThickness,
				Position.Y + Size.Y - Radius
			), OutlineColor);
			outline[pointPointer++] = new(new(
				Position.X,
				Position.Y + Radius
			), OutlineColor);
			outline[pointPointer++] = new(new(
				Position.X - OutlineThickness,
				Position.Y + Radius
			), OutlineColor);
			
			SetEdgePointDouble(
				outline, new Vector2f(Position.X, Position.Y), Radius, OutlineThickness,
				true, true, true, OutlineColor,
				ref pointPointer
			);

			outline[pointPointer++] = new(new(
				Position.X + Radius,
				Position.Y
			), OutlineColor);
			outline[pointPointer++] = new(new(
				Position.X + Radius,
				Position.Y - OutlineThickness
			), OutlineColor);
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
