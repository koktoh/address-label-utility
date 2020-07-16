namespace AddressLabelUtilityCli.Arguments
{
    internal enum ArgumentKind
    {
        Default,

        // Common
        Help,
        ExecCsv,
        ExecPdf,
        SrcPath1,
        SrcType1,
        DestPath,

        // CSV
        DestType,

        // PDf
        SrcPath2,
        SrcType2,
        Dpi,
        LineWidth,
        Margin,
        ParPage,
        VisibleLine,
    }
}
