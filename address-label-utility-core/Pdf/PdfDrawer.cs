using System.Collections.Generic;
using System.IO;
using System.Linq;
using AddressLabelUtilityCore.Extensions;
using AddressLabelUtilityCore.Label;
using AddressLabelUtilityCore.Layout;
using SkiaSharp;

namespace AddressLabelUtilityCore.Pdf
{
    public class PdfDrawer
    {
        private const string TO_HEADER_TEXT = "宛て先";
        private const string FROM_HEADER_TEXT = "差出人";

        private readonly PdfContext _pdfContext;
        private readonly LabelContext _labelContext;
        private readonly LayoutProperty _layoutProperty;
        private readonly LabelDrawer _labelDrawer;

        public PdfDrawer(PdfContext pdfContext, LabelContext labelContext)
        {
            this._pdfContext = pdfContext;
            this._labelContext = labelContext;

            var designer = new LayoutDesigner(pdfContext, labelContext);
            var layoutProperty = designer.Design();
            this._layoutProperty = layoutProperty;

            this._labelDrawer = new LabelDrawer(labelContext, layoutProperty);
        }

        public void Draw(LabelContent labelContent)
        {
            this.Draw(new[] { labelContent });
        }

        public void Draw(IEnumerable<LabelContent> labelContents)
        {
            using var doc = SKDocument.CreatePdf(Path.Combine(this._pdfContext.OutputPath, this._pdfContext.FileName));

            foreach (var contents in labelContents.GroupByCount(this._labelContext.ParPage))
            {
                this.DrawPage(contents, doc);
            }

            doc.Close();
        }

        private void DrawPage(IEnumerable<LabelContent> labelContents, SKDocument doc)
        {
            using var canvas = doc.BeginPage(this._layoutProperty.PageWidth, this._layoutProperty.PageHeight);

            var count = 0;

            for (int row = 0; row < this._layoutProperty.LabelRowCount; row++)
            {
                for (int column = 0; column < this._layoutProperty.LabelColumnCount; column++)
                {
                    if (count >= labelContents.Count())
                    {
                        break;
                    }

                    var content = labelContents.ElementAt(count);

                    this.DrawLabel(
                        content,
                        canvas,
                        this._layoutProperty.Margin + column * this._layoutProperty.UnitSize.Width,
                        this._layoutProperty.Margin + row * this._layoutProperty.UnitSize.Height);

                    count++;
                }
            }

            doc.EndPage();
        }

        public void DrawLabel(LabelContent labelContent, SKCanvas canvas, float x, float y)
        {
            if (this._layoutProperty.LabelOrientation == LabelOrientation.Hirizontal)
            {
                this.DrawLabelHorizontal(labelContent, canvas, x, y);
            }
            else
            {
                this.DrawLabelVartical(labelContent, canvas, x, y);
            }

        }

        public void DrawLabelHorizontal(LabelContent labelContent, SKCanvas canvas, float x, float y)
        {
            using var toLabel = this._labelDrawer.Draw(TO_HEADER_TEXT, labelContent.ToAddress);
            using var fromLabel = this._labelDrawer.Draw(FROM_HEADER_TEXT, labelContent.FromAddress);
            using var rotatedToLabel = this.RotateBitmap(toLabel);
            using var rotatedFromlabel = this.RotateBitmap(fromLabel);

            canvas.DrawBitmap(rotatedToLabel, x, y);
            canvas.DrawBitmap(rotatedFromlabel, x + this._layoutProperty.LabelHeight, y);
            canvas.Save();
        }

        public SKBitmap RotateBitmap(SKBitmap bitmap)
        {
            var rotatedBmp = new SKBitmap(bitmap.Height, bitmap.Width);
            using var canvas = new SKCanvas(rotatedBmp);

            canvas.Translate(0, bitmap.Width);
            canvas.RotateDegrees(-90);
            canvas.DrawBitmap(bitmap, new SKPoint());
            canvas.Save();

            return rotatedBmp;
        }

        public void DrawLabelVartical(LabelContent labelContent, SKCanvas canvas, float x, float y)
        {
            using var toLabel = this._labelDrawer.Draw(TO_HEADER_TEXT, labelContent.ToAddress);
            using var fromLabel = this._labelDrawer.Draw(FROM_HEADER_TEXT, labelContent.FromAddress);

            canvas.DrawBitmap(toLabel, x, y);
            canvas.DrawBitmap(fromLabel, x, y + this._layoutProperty.LabelHeight);
            canvas.Save();
        }
    }
}
