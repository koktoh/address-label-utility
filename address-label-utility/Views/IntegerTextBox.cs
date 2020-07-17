using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace AddressLabelUtility.Views
{
    public class IntegerTextBox : TextBox
    {
        public IntegerTextBox()
        {
            this.PreviewTextInput += this.IntegerTextBox_PreviewTextInput;
        }

        private void IntegerTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "[0-9]");
        }
    }
}
