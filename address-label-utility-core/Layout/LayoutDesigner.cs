using System;
using System.Collections.Generic;
using System.Text;
using AddressLabelUtilityCore.Label;
using AddressLabelUtilityCore.Pdf;
using AddressLabelUtilityCore.Utilities;

namespace AddressLabelUtilityCore.Layout
{
    public class LayoutDesigner
    {
        private readonly LabelContext _labelContext;

        // pixel
        private readonly int _pageWidth;
        private readonly int _pageHeight;

        public LayoutDesigner(PdfContext pdfContext, LabelContext labelContext)
        {
            this._labelContext = labelContext;

            this._pageWidth = (int)UnitConverter.ConvertMmToPixel(pdfContext.PdfSize.Width, pdfContext.Dpi);
            this._pageHeight = (int)UnitConverter.ConvertMmToPixel(pdfContext.PdfSize.Height, pdfContext.Dpi);
        }

        public LayoutProperty Design()
        {
            var rowCount = this.CalcRowCount(this._labelContext.ParPage);
            var columnCount = this.CalcColumnCount(this._labelContext.ParPage);

            var unitSize = this.CalcUnitSize(rowCount, columnCount);
            var margin = this._labelContext.MarginRatio / 100 * (unitSize.Width > unitSize.Height ? unitSize.Width : unitSize.Height);
            var orientation = unitSize.Width > unitSize.Height ? LabelOrientation.Hirizontal : LabelOrientation.Vertical;

            var labelWidth = this.CalcLabelWidth(unitSize, margin, orientation);
            var labelHeight = this.CalcLabelHeight(unitSize, margin, orientation);

            return new LayoutProperty
            {
                PageWidth = this._pageWidth,
                PageHeight = this._pageHeight,
                UnitSize = unitSize,
                Margin = margin,
                LabelRowCount = rowCount,
                LabelColumnCount = columnCount,
                LabelWidth = labelWidth,
                LabelHeight = labelHeight,
                LabelHeaderWidth = this.CalcLabelHeaderWidth(labelWidth),
                LabelHeaderHeight = this.CalcLableHeaderHeight(labelHeight),
                LabelOrientation = orientation,
            };
        }

        private int CalcRowCount(int labelCount)
        {
            if (labelCount <= 2)
            {
                return labelCount;
            }

            return labelCount / 2 + labelCount % 2;
        }

        private int CalcColumnCount(int labelCount)
        {
            if (labelCount <= 2)
            {
                return 1;
            }

            return 2;
        }

        private UnitSize CalcUnitSize(int rowCount, int columnCount)
        {
            return new UnitSize(this._pageWidth / columnCount, this._pageHeight / rowCount);
        }

        private float CalcLabelWidth(UnitSize unitSize, float margin, LabelOrientation orientation)
        {
            return (orientation == LabelOrientation.Vertical ? unitSize.Width : unitSize.Height) - 2 * margin;
        }

        private float CalcLabelHeight(UnitSize unitSize, float margin, LabelOrientation orientation)
        {
            return (orientation == LabelOrientation.Vertical ? unitSize.Height : unitSize.Width) / 2 - margin;
        }

        private float CalcLabelHeaderWidth(float labelWidth)
        {
            return labelWidth * 0.4f;
        }

        private float CalcLableHeaderHeight(float labelHeight)
        {
            return labelHeight * 0.15f;
        }
    }
}
