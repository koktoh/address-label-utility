using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace AddressLabelUtility.Views
{
    public class NumericTextBox : TextBox
    {
        public NumericTextBox()
        {
            this.PreviewTextInput += this.NumericTextBox_PreviewTextInput;
        }

        private void NumericTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (this.Text.Contains(".") && e.Text == ".")
            {
                e.Handled = true;
                return;
            }

            e.Handled = !Regex.IsMatch(e.Text, @"[0-9\.]");
        }
    }
}
