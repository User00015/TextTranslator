using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ConsoleApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var source = new TextSource();

            string example = "123456 N Prov Paid N N 12/30/1980 hospital name $12 $34 $56 12/31/1981 A1234 Treatment";

            OriginalText.DataBindings.Add("Text", source, "Text", false, DataSourceUpdateMode.OnPropertyChanged);
            TranslatedText.DataBindings.Add("Text", source, "FormattedText", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(TranslatedText.Text);
        }
    }

    public class TextSource : INotifyPropertyChanged
    {
        private string _text; 
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("Text"));
            }
        }

        public string FormattedText
        {
            get => FormatText(_text);
        }

        private string FormatText(string text)
        {

            try
            {
                var claimNumberRegex = new Regex(@"^\w*");
                var datesRegex = new Regex(@"[0-9]*/[0-9]*/[0-9]*");
                var chargesRegex = new Regex(@"\$\d+");

                var slice1 = text.IndexOf("N ");
                var slice2 = text.LastIndexOf(" N") + 3 - slice1;

                var input = text.Remove(slice1, slice2);

                var claimNumber = claimNumberRegex.Match(input);
                var dates = datesRegex.Matches(input);
                var charges = chargesRegex.Matches(input);

                var provider = input.Substring(dates[0].Index + dates[0].Length,
                    charges[0].Index - (dates[0].Index + dates[0].Length));
                var diagnosis = input.Substring(dates[1].Index + dates[1].Length);

                return $@"Claim Number: {claimNumber}
Paid date: {dates[0]}
Provider: {provider}
Date of Service: {dates[1]}
Billed: {charges[0]}
Paid: {charges[2]}
Diagnosis: {diagnosis}";

            }
            catch (Exception e)
            {

            }

            return string.Empty;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, e);
        }

    }
}
