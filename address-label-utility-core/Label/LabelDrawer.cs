using System.Linq;
using AddressLabelUtilityCore.Address;
using AddressLabelUtilityCore.Extensions;
using AddressLabelUtilityCore.Layout;
using SkiaSharp;

namespace AddressLabelUtilityCore.Label
{
    internal class LabelDrawer
    {
        private readonly LabelContext _context;
        private readonly LayoutProperty _layoutProperty;

        public LabelDrawer(LabelContext labelContext, LayoutProperty layoutProperty)
        {
            this._context = labelContext;
            this._layoutProperty = layoutProperty;
        }

        public SKBitmap Draw(string header, AddressBase address)
        {
            var bitmap = new SKBitmap(this._layoutProperty.LabelWidth.ToInt(), this._layoutProperty.LabelHeight.ToInt());
            using var canvas = new SKCanvas(bitmap);

            this.DrawOutline(canvas);
            this.DrawHeader(canvas, header);
            this.DrawAddress(canvas, address);

            return bitmap;
        }

        private void DrawOutline(SKCanvas canvas)
        {
            using var paint = new SKPaint
            {
                Color = SKColors.Black,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = this._context.OutlineWidth,
            };

            canvas.DrawRect(0, 0, this._layoutProperty.LabelWidth, this._layoutProperty.LabelHeight, paint);
            canvas.Save();
        }

        private void DrawHeader(SKCanvas canvas, string header)
        {
            using var backgroundPaint = new SKPaint
            {
                Color = SKColors.Black,
            };

            canvas.DrawRect(0, 0, this._layoutProperty.LabelHeaderWidth, this._layoutProperty.LabelHeaderHeight, backgroundPaint);

            using var textPaint = new SKPaint
            {
                Color = SKColors.White,
                TextSize = this._layoutProperty.LabelHeaderHeight * 0.75f,
                IsAntialias = true,
            };

            var textWidth = textPaint.MeasureText(header);

            var textX = this._layoutProperty.LabelHeaderWidth / 2 - textWidth / 2;
            var textY = this._layoutProperty.LabelHeaderHeight * 0.8f;

            canvas.DrawText(header, textX, textY, textPaint);
            canvas.Save();
        }

        private void DrawAddress(SKCanvas canvas, AddressBase address)
        {
            var origin = new SKPoint(this._layoutProperty.LabelWidth * 0.05f, this._layoutProperty.LabelHeaderHeight * 1.2f);

            this.DrawMultiLineText(canvas, origin, address.ToString());
        }

        private void DrawMultiLineText(SKCanvas canvas, SKPoint origin, string text)
        {
            using var textPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = this._layoutProperty.LabelHeaderHeight * 0.4f,
                IsAntialias = true,
            };

            var textHeight = textPaint.TextSize * 1.1f;

            var lines = text.SplitNewLine().ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];

                canvas.DrawText(line, origin.X, origin.Y + textHeight * (i + 1), textPaint);
            }

            canvas.Save();
        }
    }
}

