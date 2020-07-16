using System;
using System.Linq;
using AddressLabelUtilityCli.Extensions;
using AddressLabelUtilityCore.Label;
using AddressLabelUtilityCore.Pdf;

namespace AddressLabelUtilityCli.Arguments.Common
{
    internal class HelpArgument : ArgumentBase
    {
        public override string[] Alias => new[] { "-h", "--help" };

        public override string Raw { get; set; }

        public override bool IsRequired => false;

        public override ArgumentKind ArgumentKind => ArgumentKind.Help;

        public override bool ShouldHaveArgument => false;

        public override string Argument { get; set; }

        public override string GetMessage()
        {
            var csvKindList = Enum.GetNames(typeof(CsvKind))
                .Select(x => x.ToLower());

            var labelContext = new LabelContext();
            var pdfContext = new PdfContext();

            return @$"各種サービスから出力された住所 CSV からラベルを作成するユーティリティープログラム
    1. 住所 CSV を元に郵便用ラベルを作成し、 PDF として出力する
    2. 住所 CSV を元に他サービスに対応した CSV へ変換する
--------------------
オプション説明 : ""*"" は必須オプション

    共通オプション :

      * --csv                                            : CSV 変換を実行。 --pdf とは排他。
      * --pdf                                            : PDF 作成を実行。 --csv とは排他。
        --help | -h                                      : ヘルプを表示。このテキスト。
      * --source | -s | --source1 | -s1 <file path>      : ソースとなる CSV ファイルを指定。
        --sourceType | -st | --sourceType1 | -st1 <type> : ソースとなる CSV ファイルの種別を指定。
                                                           {csvKindList.Join(", ")} の{csvKindList.Count()}種類。
                                                           指定がない場合、 CSV ファイルから推定。
        --output | -o <output path>                      : 出力先を指定。
                                                           指定がない場合、ソースファイルと同じディレクトリに作成。
                                                           ファイル名は output.csv / output.pdf

    CSV 変換プログラム用オプション : --pdf が指定された場合無視される

        --outputType | -ot <type>                        : 出力 CSV の種別を指定。
                                                           {csvKindList.Join(", ")} の{csvKindList.Count()}種類。
                                                           指定がない場合、 {CsvKind.Default.ToString().ToLower()} で出力。

    PDF 作成プログラム用オプション : --csv が指定された場合無視される

      * --source2 | -s2 <file path>                      : 差出人用 CSV ファイルを指定。
        --sourceType2 | -st2 <type>                      : 差出人用 CSV ファイルの種別を指定。
                                                           {csvKindList.Join(", ")} の{csvKindList.Count()}種類。
                                                           指定がない場合、 CSV ファイルから推定。
        --dpi | -d <num>                                 : DPI を指定。 デフォルトは ""{pdfContext.Dpi}"" 。
        --parPage | -p <num>                             : 面付を指定。 デフォルトは ""{labelContext.ParPage}"" 。
        --margin | -m <num>                              : ラベルの余白を指定。 デフォルトは ""{labelContext.MarginRatio}"" 。
        --width | -w <num>                               : ラベル枠線の幅を指定。 デフォルトは ""{labelContext.OutlineWidth}"" 。
        --visible | -v                                   : ラベルの分割線を表示する。
";
        }

        public override bool Validate(out string message)
        {
            message = string.Empty;
            return true;
        }
    }
}
