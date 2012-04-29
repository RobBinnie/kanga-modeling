﻿using System.Collections.Generic;
using KangaModeling.Graphics;
using KangaModeling.Graphics.Primitives;
using System.Diagnostics;

namespace KangaModeling.Visuals.SequenceDiagrams
{
    internal class GridLayout : Visual
    {
        private readonly Padding m_Padding = new Padding(10);

        public GridLayout(int lifelineCount, int rowCount)
        {
            Rows = new List<Row>();
            Columns = new List<Column>();

            for (int i = 0; i < lifelineCount; i++)
            {
                Columns.Add(new Column());
            }


            HeaderRow = new Row();
            for (int i = 0; i < rowCount; i++)
            {
                Rows.Add(new Row());
            }
            FooterRow = new Row();
        }

        public IList<Row> Rows { get; private set; }
        public IList<Column> Columns { get; private set; }
        public Row HeaderRow { get; private set; }
        public Row FooterRow { get; private set; }

        protected override void LayoutCore(IGraphicContext graphicContext)
        {
            base.LayoutCore(graphicContext);

            if (Columns.Count == 0)
            {
                return;
            }

            AdjustLocations();

            Column lastColumn = Columns[Columns.Count - 1];
            Row lastRow = FooterRow;

            Size = new Size(lastColumn.Right + m_Padding.Right, lastRow.Bottom + m_Padding.Bottom);
        }

        protected override void DrawCore(IGraphicContext graphicContext)
        {
            DrawCellAreas(graphicContext);
        }

        private void AdjustLocations()
        {
            AdjustRowLocations();
            AdjustColumnLocations();
        }

        private void AdjustColumnLocations()
        {
            Column prevColumn = null;
            foreach (Column currentColumn in Columns)
            {
                float x = (prevColumn != null) ? prevColumn.Right : m_Padding.Left;
                currentColumn.AdjustLocation(x);
                prevColumn = currentColumn;
            }
        }

        private void AdjustRowLocations()
        {
            HeaderRow.AdjustLocation(m_Padding.Top);
            Row prevRow = HeaderRow;
            foreach (Row currentRow in Rows)
            {
                currentRow.AdjustLocation(prevRow.Bottom);
                prevRow = currentRow;
            }
            FooterRow.AdjustLocation(prevRow.Bottom);
        }

        [Conditional("DEBUG")]
        private void DrawCellAreas(IGraphicContext graphicContext)
        {            
            foreach (var row in Rows)
            {
                var rowTop = row.Top;
                var rowHeight = row.Bottom - rowTop;
                var rowBodyTop = row.Body.Top;
                var rowBodyHeight = row.Body.Bottom - row.Body.Top;

                foreach (var column in Columns)
                {
                    var columnLeft = column.Left;
                    var columnWidth = column.Right - columnLeft;
                    var columnBodyLeft = column.Body.Left;
                    var columnBodyWidth = column.Body.Right - columnBodyLeft;

                    graphicContext.FillRectangle(
                        new Point(columnLeft, rowTop),
                        new Size(columnWidth, rowHeight),
                        new Color(50, Color.Green));

                    graphicContext.DrawRectangle(
                        new Point(columnLeft, rowTop),
                        new Size(columnWidth, rowHeight),
                        new Color(100, Color.Green));

                    graphicContext.FillRectangle(
                        new Point(columnBodyLeft, rowBodyTop),
                        new Size(columnBodyWidth, rowBodyHeight),
                        new Color(50, Color.Red));

                    graphicContext.DrawRectangle(
                        new Point(columnBodyLeft, rowBodyTop),
                        new Size(columnBodyWidth, rowBodyHeight),
                        new Color(100, Color.Red));
                }
            }
        }
    }
}