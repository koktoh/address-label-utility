using AddressLabelUtilityCore.Pdf;

namespace AddressLabelUtility.Models.Pdf
{
    internal class PdfBuilder
    {
        private readonly PdfBuildingContext _context;

        public PdfBuilder(PdfBuildingContext context)
        {
            this._context = context;
        }

        public void Build()
        {
            var drawer = new PdfDrawer(this._context.PdfContext, this._context.LabelContext);
            drawer.Draw(this._context.LabelContents);
        }
    }
}
