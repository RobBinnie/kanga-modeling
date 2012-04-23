﻿using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class XCross : Visual
    {
        private readonly Column m_Column;
        private readonly Row m_Row;

        public XCross(Column column, Row row)
        {
            m_Column = column;
            m_Row = row;
            Size = new Size(16, 16);
        }

        protected internal override void LayoutCore(IGraphicContext graphicContext)
        {
            m_Column.Allocate(Width);
            m_Row.Body.Allocate(Height + 20);
            base.LayoutCore(graphicContext);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            float x = m_Column.Body.Middle;
            float y = m_Row.Body.Middle;

            float hWidth = Width/2;
            float hHeight = Height/2;

            var leftTop = new Point(x - hWidth, y - hHeight);
            var rightTop = new Point(x + hWidth, y - hHeight);

            var leftBottom = new Point(x - hWidth, y + hHeight);
            var rightBottom = new Point(x + hWidth, y + hHeight);

            graphicContext.DrawLine(leftTop, rightBottom, 1);
            graphicContext.DrawLine(rightTop, leftBottom, 1);

            base.DrawCore(graphicContext);
        }
    }
}