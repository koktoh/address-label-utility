using System.Collections.Generic;

namespace AddressLabelUtilityCore.Pdf
{
    public enum PdfSizeSet
    {
        A4,
    }

    public static class PdfSizeResolver
    {
        // Mili Meter
        private static readonly IReadOnlyDictionary<PdfSizeSet, PdfSize> _sizeDict = new Dictionary<PdfSizeSet, PdfSize>
        {
            { PdfSizeSet.A4, new PdfSize(210, 297) },
        };

        public static PdfSize Resolve(PdfSizeSet pdfSize)
        {
            if (_sizeDict.TryGetValue(pdfSize, out var size))
            {
                return size;
            }

            return default;
        }
    }
}
