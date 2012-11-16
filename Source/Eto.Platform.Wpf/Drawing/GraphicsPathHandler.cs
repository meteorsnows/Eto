﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using swm = System.Windows.Media;
using Eto.Drawing;

namespace Eto.Platform.Wpf.Drawing
{
	public class GraphicsPathHandler : WidgetHandler<swm.PathGeometry, GraphicsPath>, IGraphicsPath
	{
		swm.PathFigure figure;

		public GraphicsPathHandler ()
		{
			Control = new swm.PathGeometry ();
			Control.Figures = new swm.PathFigureCollection ();
		}

		void StartNewFigure (Point startPoint)
		{
			figure = new swm.PathFigure ();
			figure.StartPoint = Generator.Convert (startPoint);
			figure.Segments = new swm.PathSegmentCollection ();
			Control.Figures.Add (figure);
		}

        void StartNewFigure(PointF startPoint)
        {
            figure = new swm.PathFigure();
            figure.StartPoint = Generator.Convert(startPoint);
            figure.Segments = new swm.PathSegmentCollection();
            Control.Figures.Add(figure);
        }

        public void AddLines(PointF[] points)
        {
            var enumerator = points.GetEnumerator();

            if (!enumerator.MoveNext())
                return;

            StartNewFigure(
                (PointF)enumerator.Current);

            while (enumerator.MoveNext())
            {
                figure.Segments.Add(
                    new swm.LineSegment(
                        Generator.Convert(
                            (PointF)enumerator.Current),
                        true));
            }
        }


		public void AddLine (Point point1, Point point2)
		{
			StartNewFigure (point1);
			figure.Segments.Add (new swm.LineSegment (Generator.Convert(point2), true));
		}

		public void LineTo (Point point)
		{
			figure.Segments.Add (new swm.LineSegment (Generator.Convert (point), true));
		}

		public void MoveTo (Point point)
		{
			StartNewFigure (point);
		}

        #region IGraphicsPath Members


        public RectangleF GetBounds()
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty
        {
            get { throw new NotImplementedException(); }
        }

        public void AddCurve(PointF[] points)
        {
            throw new NotImplementedException();
        }

        public void AddLine(PointF point1, PointF point2)
        {
            throw new NotImplementedException();
        }

        public void AddBezier(PointF pt1, PointF pt2, PointF pt3, PointF pt4)
        {
            throw new NotImplementedException();
        }

        public void AddPath(IGraphicsPathBase addingPath, bool connect)
        {
            throw new NotImplementedException();
        }

        public void Translate(PointF point)
        {
            throw new NotImplementedException();
        }

        public IGraphicsPath Clone()
        {
            throw new NotImplementedException();
        }

        public void AddRectangle(RectangleF rectangle)
        {
            throw new NotImplementedException();
        }

        public void CloseFigure()
        {
            throw new NotImplementedException();
        }

        public FillMode FillMode
        {
            set { throw new NotImplementedException(); }
        }

        public void Transform(Matrix matrix)
        {
            throw new NotImplementedException();
        }

        public void AddArc(RectangleF rect, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        public void AddBeziers(Point[] points)
        {
            throw new NotImplementedException();
        }

        public void AddEllipse(RectangleF rect)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void AddEllipse(float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        public GraphicsPath ToGraphicsPath()
        {
            throw new NotImplementedException(); // should never get called
        }
    }
}
