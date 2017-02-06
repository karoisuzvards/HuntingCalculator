using System.Collections.Generic;
using System.Globalization;
using System.Windows.Documents;

namespace HuntingCalculator
{
    /// <summary>
    /// Interaction logic for InputFieldsControl.xaml
    /// </summary>
    public partial class InputFieldsControl
    {
        public InputFieldsControl(int lastId)
        {
            InitializeComponent();
            Id.Content = lastId.ToString(CultureInfo.InvariantCulture);
            KvartalaNrTextBox.Text = string.Empty;
            NogabalaNrTextBox.Text = string.Empty;
            PlatibaTextBox.Text = string.Empty;
            VecumsTextBox.Text = string.Empty;
            MaatComboBox.ItemsSource = PopulateMaatItemSource();
            KokaSugaComboBox.ItemsSource = PopulateKokaSugaItemsSource();
        }

        public InputFieldsControl()
        {
            InitializeComponent();
            Id.Content = "1";
            KvartalaNrTextBox.Text = string.Empty;
            NogabalaNrTextBox.Text = string.Empty;
            PlatibaTextBox.Text = string.Empty;
            VecumsTextBox.Text = string.Empty;
            MaatComboBox.ItemsSource = PopulateMaatItemSource();
            KokaSugaComboBox.ItemsSource = PopulateKokaSugaItemsSource();
        }

        private static IEnumerable<string> PopulateMaatItemSource()
        {
            return new List<string>
            {
                "Am",
                "Ap",
                "As",
                "Av",
                "Db",
                "Dm",
                "Dms",
                "DzivnBarLauces",
                "Gr",
                "Grs",
                "Gs",
                "KlajieSunuPurvi",
                "Km",
                "Kp",
                "Krumaji",
                "Ks",
                "Kv",
                "Lk",
                "Ln",
                "MezaNeapkl",
                "Mr",
                "Mrs",
                "Nd",
                "NeiezLauksaimn",
                "NesaslEKult",
                "NesaslPKult",
                "None",
                "Pv",
                "Sl",
                "Trases",
                "Virsaji",
                "Vr",
                "Vrs",
                "ZaluParejas"
            };
        }

        private static IEnumerable<string> PopulateKokaSugaItemsSource()
        {
            return new List<string>
            {
                "A",
                "B",
                "Ba",
                "Be",
                "Bl",
                "Cp",
                "Ds",
                "E",
                "EC",
                "G",
                "K",
                "Ki",
                "L",
                "Le",
                "M",
                "None",
                "Os",
                "Oz",
                "P",
                "Pa",
                "PC",
                "Sk",
                "V",
                "Vi"
            };
        }
    }
}
