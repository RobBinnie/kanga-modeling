﻿using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using KangaModeling.Visuals.SequenceDiagrams.Styles;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class TextVisual : SDVisualBase
    {
        public const float MinWidth = 40;
        private readonly Column m_Column;
        private readonly string m_Name;
        private readonly Row m_Row;

        public TextVisual(IStyle style, string name, Column column, Row row)
            : base(style)
        {
            m_Name = name;
            m_Column = column;
            m_Row = row;
            Padding = new Padding(2);
        }

        protected Padding Padding { get; private set; }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);
            Size = graphicContext.MeasureText(m_Name, Style.Common.Font, Style.Lifeline.NameFontSize);
            if (Size.Width < MinWidth)
            {
                Size = new Size(MinWidth, Height);
            }
            m_Column.Allocate(Size.Width);
            m_Row.Body.Allocate(Size.Height);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            Location = Parent.Location;
            graphicContext.DrawText(Location, Size, m_Name, Style.Common.Font, Style.Lifeline.NameFontSize, Style.Lifeline.NameTextColor, HorizontalAlignment.Center, VerticalAlignment.Middle);
        }
    }
}