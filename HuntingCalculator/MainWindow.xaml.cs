using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HuntingCalculator.Enums;
using HuntingCalculator.Logic;
using Microsoft.Win32;

namespace HuntingCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private int _lastInputId;
        private Dictionary<double, List<ResultTypeNew>> _results;
        private Dictionary<string, List<ResultGrouped>> _groupedResults; 

        public MainWindow()
        {
            InitializeComponent();

            _lastInputId = 1;
            InputIdFieldComboBox.Items.Add(_lastInputId);

            PopulatePrices();

            _groupedResults = new Dictionary<string, List<ResultGrouped>>();

            ErrorButton.Visibility = Visibility.Hidden;
            ErrorLabel.Visibility = Visibility.Hidden;
        }

        private void Button_ClickAddRecord(object sender, RoutedEventArgs e)
        {
            try
            {
                _lastInputId++;
                DataStackPanel.Children.Add(new InputFieldsControl(_lastInputId));
                InputIdFieldComboBox.Items.Add(_lastInputId);
            }
            catch (Exception ex)
            {
                SetError("Technical error 1 - " + ex.Message);
            }
        }

        private void Button_ClickDeleteRecord(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InputIdFieldComboBox.SelectedItem == null) return;

                var selectedItem = (int)InputIdFieldComboBox.SelectedItem - 1;

                if (selectedItem > _lastInputId || selectedItem < 0) return;

                DataStackPanel.Children.RemoveAt(selectedItem);
                _lastInputId--;
                ReorderInputFieldIds();
                RenewComboBoxIds();
            }
            catch (Exception ex)
            {
                SetError("Technical error 2 - " + ex.Message);
            }
        }

        private void ReorderInputFieldIds()
        {

            try
            {
                var startId = 1;
                foreach (var inputFieldsControl in from object child in DataStackPanel.Children select child as InputFieldsControl)
                {
                    if (inputFieldsControl != null) inputFieldsControl.Id.Content = startId;
                    startId++;
                }
            }
            catch (Exception ex)
            {
                SetError("Technical error 3 - " + ex.Message);
            }
        }

        private void RenewComboBoxIds()
        {
            try
            {
                InputIdFieldComboBox.SelectedItem = null;
                InputIdFieldComboBox.Items.Clear();
                for (var i = 1; i <= _lastInputId; i++)
                {
                    InputIdFieldComboBox.Items.Add(i);
                }
            }
            catch (Exception ex)
            {
                SetError("Technical error 4 - " + ex.Message);
            }
        }

        private void PopulatePrices()
        {
            PricesDataGrid.ItemsSource = CenasLogic.PopulatePrices();
        }

        private List<InputRecord> GetInputRecords()
        {
            try
            {
                var i = DataStackPanel.Children.OfType<InputFieldsControl>();
                var result = new List<InputRecord>();
                var output = string.Empty;

                foreach (var inputFieldsControl in i)
                {
                    output += inputFieldsControl.PlatibaTextBox.Text + "-";
                    output += inputFieldsControl.PlatibaTextBox.Text.Replace('.', ',') + "-";
                    output += CustomParse(inputFieldsControl.PlatibaTextBox.Text.Replace('.', ','));

                    result.Add(new InputRecord
                    {

                        KvartalaNr = double.Parse(inputFieldsControl.KvartalaNrTextBox.Text.Replace('.', ',')),
                        NogabalaNr = double.Parse(inputFieldsControl.NogabalaNrTextBox.Text.Replace('.', ',')),
                        Platiba = CustomParse(inputFieldsControl.PlatibaTextBox.Text.Replace('.', ',')),
                        //Platiba = double.Parse(inputFieldsControl.PlatibaTextBox.Text.Replace('.', ',')),
                        MezaApstakluAugsanasTips = StringToMaatEnum(inputFieldsControl.MaatComboBox.Text),
                        KokaSuga = StringKokaVeidsEnum(inputFieldsControl.KokaSugaComboBox.Text),
                        Vecums = int.Parse(inputFieldsControl.VecumsTextBox.Text)
                    });
                }

                //var inputRecords = DataStackPanel.Children.OfType<InputFieldsControl>().Select(inputRecordObj => new InputRecord
                //{

                //    KvartalaNr = double.Parse(inputRecordObj.KvartalaNrTextBox.Text.Replace('.', ',')),
                //    NogabalaNr = double.Parse(inputRecordObj.NogabalaNrTextBox.Text.Replace('.', ',')),
                //    Platiba = double.Parse(inputRecordObj.PlatibaTextBox.Text.Replace('.', ',')),
                //    MezaApstakluAugsanasTips = StringToMaatEnum(inputRecordObj.MaatComboBox.Text),
                //    KokaSuga = StringKokaVeidsEnum(inputRecordObj.KokaSugaComboBox.Text),
                //    Vecums = int.Parse(inputRecordObj.VecumsTextBox.Text)

                //}).ToList();
                //SetError(output);
                return result;
                //return inputRecords;
            }
            catch (Exception ex)
            {
                SetError("Technical error 5 - " + ex.Message);
                return null;
            }
        }

        private void Button_ClickCalculate(object sender, RoutedEventArgs e)
        {
            //Clean existing results
            CleanResults();

            try
            {
                //atrast bonitates katram InputRecord
                var inputRecords = AddBonitates(GetInputRecords());

                //Atrast AreaType 
                //var areaTypes = GetAreaTypeFromInputRecords(inputRecords);

                //Atrast ResultType
                var resultTypes = GetResultTypeNewFromInputRecords(inputRecords);

                var resultTypeNews = resultTypes as IList<ResultTypeNew> ?? resultTypes.ToList();
                CalculatePriceForResultTypeNew(resultTypeNews);

                //Aizpildit rezultata lauku
                PrintResults(resultTypeNews);
                PrintResultsGrouped(resultTypeNews.ToList());

                SetError("Calculation done! Check results tab.");
            }
            catch (Exception ex)
            {
                SetError("Technical error 6 - " + ex.Message);
            }

        }

        private static IEnumerable<ResultTypeNew> GetResultTypeNewFromInputRecords(IEnumerable<InputRecord> inputRecords)
        {
            return inputRecords.Select(inputRecord => new ResultTypeNew
            {
                NogabalaNr = inputRecord.NogabalaNr,
                KvartalaNr = inputRecord.KvartalaNr,
                NogabalaPlatiba = inputRecord.Platiba,
                BonitateAlnis = inputRecord.Bonitates.Alnis,
                BonitateMezaCuka = inputRecord.Bonitates.Mezacuka,
                BonitateStaltbriedis = inputRecord.Bonitates.Staltbriedis,
                BonitateStirna = inputRecord.Bonitates.StirnaOtra
            }).ToList();
        }

        private static IEnumerable<InputRecord> AddBonitates(List<InputRecord> sourceInputRecords)
        {
            foreach (var inputRecord in sourceInputRecords)
            {
                Bonitate bon;

                if (IsExceptional(inputRecord.MezaApstakluAugsanasTips))
                {
                    bon = Bonitates.GetByTips(inputRecord.MezaApstakluAugsanasTips);
                }
                else
                {
                    var mezaTips = MezaTipaGrupaLogic.GetMezaTips(inputRecord.KokaSuga.ToString(),
                    inputRecord.MezaApstakluAugsanasTips.ToString());
                    var vecumaGrupa = VecumaGrupaLogic.GetVecumaGrupa(inputRecord.KokaSuga.ToString(), inputRecord.Vecums);
                    bon = Bonitates.GetByTipsAndVecums(mezaTips, vecumaGrupa);
                }

                inputRecord.Bonitates = new Bonitate();

                if (bon != null)
                {
                    inputRecord.Bonitates.Alnis = bon.Alnis;
                    inputRecord.Bonitates.Mezacuka = bon.Mezacuka;
                    inputRecord.Bonitates.Staltbriedis = bon.Staltbriedis;
                    inputRecord.Bonitates.StirnaPirma = bon.StirnaPirma;
                    inputRecord.Bonitates.StirnaOtra = bon.StirnaOtra;
                    inputRecord.Bonitates.StirnaTresa = bon.StirnaTresa;
                }
                else
                {
                    throw new Exception("Nevareja noteikt bonitates pie NogabalaNr=" + inputRecord.NogabalaNr +
                        " KvartalaNr=" + inputRecord.KvartalaNr +
                        " KokaSugas=" + inputRecord.KokaSuga +
                        " Vecuma=" + inputRecord.Vecums +
                        " MAAT=" + inputRecord.MezaApstakluAugsanasTips);
                }
            }
            return sourceInputRecords;
        }

        private void CalculatePriceForResultTypeNew(IEnumerable<ResultTypeNew> resultRecords)
        {
            const DzivnieksEnum alnis = DzivnieksEnum.Alnis;
            const DzivnieksEnum mezacuka = DzivnieksEnum.Mezacuka;
            const DzivnieksEnum starltbriedis = DzivnieksEnum.Staltbriedis;
            const DzivnieksEnum stirna = DzivnieksEnum.Stirna;

            foreach (var record in resultRecords)
            {
                record.CenaParNogabalu = GetPriceByBonitateAnimal(record.BonitateAlnis, alnis) +
                                         GetPriceByBonitateAnimal(record.BonitateMezaCuka, mezacuka) +
                                         GetPriceByBonitateAnimal(record.BonitateStaltbriedis, starltbriedis) +
                                         GetPriceByBonitateAnimal(record.BonitateStirna, stirna);
                record.CenaParNogabalu = record.CenaParNogabalu * record.NogabalaPlatiba;
            }
        }

        private void Button_ClickDeleteResult(object sender, RoutedEventArgs e)
        {
            CleanResults();
        }

        private void Button_ClickOpenFile(object sender, RoutedEventArgs e)
        {
            try
            {
                var dlg = new OpenFileDialog
                {
                    DefaultExt = ".txt",
                    Filter = "Text documents (.txt)|*.txt"
                };

                var result = dlg.ShowDialog();

                if (result == null || !result.Value) return;

                var data = LoadData(dlg.FileName);

                if (data == null) return;

                DataStackPanel.Children.Clear();
                InputIdFieldComboBox.Items.Clear();
                _lastInputId = 0;

                foreach (var inputRecord in data)
                {
                    _lastInputId++;

                    var control = new InputFieldsControl(_lastInputId)
                    {
                        KvartalaNrTextBox = { Text = inputRecord.KvartalaNr.ToString(CultureInfo.InvariantCulture) },
                        NogabalaNrTextBox = { Text = inputRecord.NogabalaNr.ToString(CultureInfo.InvariantCulture) },
                        PlatibaTextBox = { Text = inputRecord.Platiba.ToString(CultureInfo.InvariantCulture) },
                        VecumsTextBox = { Text = inputRecord.Vecums.ToString(CultureInfo.InvariantCulture) }
                    };

                    var source = control.MaatComboBox.ItemsSource.OfType<string>().ToList();
                    var i = 0;
                    while (source.ElementAt(i) != inputRecord.MezaApstakluAugsanasTips.ToString())
                    {
                        i++;
                    }

                    control.MaatComboBox.SelectedIndex = i;

                    source = control.KokaSugaComboBox.ItemsSource.OfType<string>().ToList();
                    i = 0;
                    while (source.ElementAt(i) != inputRecord.KokaSuga.ToString())
                    {
                        i++;
                    }

                    control.KokaSugaComboBox.SelectedIndex = i;

                    DataStackPanel.Children.Add(control);
                    InputIdFieldComboBox.Items.Add(_lastInputId);
                }
            }
            catch (Exception ex)
            {
                SetError("Technical error 7 - " + ex.Message);
            }
        }

        private void Button_ClickSaveData(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveData();
                SetError("Data saved!");
            }
            catch (Exception ex)
            {
                SetError("Technical error 8 - " + ex.Message);
            }
        }

        private void Button_ClickConfirmError(object sender, RoutedEventArgs e)
        {
            ErrorLabel.Text = string.Empty;
            ErrorButton.Visibility = Visibility.Hidden;
            ErrorLabel.Visibility = Visibility.Hidden;
        }

        private void Button_ClickSaveResult(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_results.Count < 1) return;

                SaveResults(_results);
                SetError("Results saved!");
            }
            catch (Exception ex)
            {
                SetError("Technical error 9 - " + ex.Message);
            }
        }

        private void SetError(string errorText)
        {
            ErrorLabel.Text = DateTime.Now + ": " + errorText;
            ErrorLabel.Visibility = Visibility.Visible;
            ErrorButton.Visibility = Visibility.Visible;
            FirstTabItem.IsSelected = true;
        }

        private static bool IsExceptional(MaatEnum maat)
        {
            return (maat == MaatEnum.NesaslPKult ||
                maat == MaatEnum.NesaslEKult ||
                maat == MaatEnum.MezaNeapkl ||
                maat == MaatEnum.Krumaji ||
                maat == MaatEnum.KlajieSunuPurvi ||
                maat == MaatEnum.ZaluParejas ||
                maat == MaatEnum.NeiezLauksaimn ||
                maat == MaatEnum.Trases ||
                maat == MaatEnum.Virsaji ||
                maat == MaatEnum.DzivnBarLauces);
        }

        private double GetPriceByBonitateAnimal(int bonitate, DzivnieksEnum dzivnieks)
        {
            var prices = (List<PriceType>)PricesDataGrid.ItemsSource;
            PriceType firstOrDefault;
            switch (dzivnieks)
            {
                case DzivnieksEnum.Alnis:
                    firstOrDefault = prices.FirstOrDefault(x => x.Dzivnieks == DzivnieksEnum.Alnis);
                    if (firstOrDefault != null)
                    {
                        return GetPriceByBonitate(bonitate, firstOrDefault);
                    }
                    break;
                case DzivnieksEnum.Mezacuka:
                    firstOrDefault = prices.FirstOrDefault(x => x.Dzivnieks == DzivnieksEnum.Mezacuka);
                    if (firstOrDefault != null)
                    {
                        return GetPriceByBonitate(bonitate, firstOrDefault);
                    }
                    break;
                case DzivnieksEnum.Staltbriedis:
                    firstOrDefault = prices.FirstOrDefault(x => x.Dzivnieks == DzivnieksEnum.Staltbriedis);
                    if (firstOrDefault != null)
                    {
                        return GetPriceByBonitate(bonitate, firstOrDefault);
                    }
                    break;
                case DzivnieksEnum.Stirna:
                    firstOrDefault = prices.FirstOrDefault(x => x.Dzivnieks == DzivnieksEnum.Stirna);
                    if (firstOrDefault != null)
                    {
                        return GetPriceByBonitate(bonitate, firstOrDefault);
                    }
                    break;
                default:
                    return 0;
            }
            return 0;
        }

        private static double GetPriceByBonitate(int bonitate, PriceType priceType)
        {
            if (bonitate == 1) return priceType.CenaParPirmoBonitati;
            if (bonitate == 2) return priceType.CenaParOtroBonitati;
            if (bonitate == 3) return priceType.CenaParTresoBonitati;
            if (bonitate == 4) return priceType.CenaParCeturtoBonitati;
            return bonitate == 5 ? priceType.CenaParPiektoBonitati : 0;
        }

        private void PrintResults(IEnumerable<ResultTypeNew> results)
        {
            var resultDictionary = new Dictionary<double, List<ResultTypeNew>>();
            double kopejaSumma = 0;
            double kopejaPlatiba = 0;

            foreach (var result in results)
            {
                var existsInDictionary = resultDictionary.ContainsKey(result.KvartalaNr);
                if (existsInDictionary)
                {
                    List<ResultTypeNew> list;
                    resultDictionary.TryGetValue(result.KvartalaNr, out list);
                    if (list != null)
                    {
                        list.Add(result);
                    }
                }
                else
                {
                    resultDictionary.Add(result.KvartalaNr, new List<ResultTypeNew> { result });
                }

                kopejaSumma += result.CenaParNogabalu;
                kopejaPlatiba += result.NogabalaPlatiba;
            }

            var possibleKvartals = resultDictionary.Keys.ToList();

            foreach (var kvartals in possibleKvartals)
            {
                ResultDataStackPanel.Children.Add(new Label { Content = kvartals + ". kvartals" });
                List<ResultTypeNew> res;
                resultDictionary.TryGetValue(kvartals, out res);
                if (res != null)
                {
                    ResultDataStackPanel.Children.Add(new DataGrid { ItemsSource = res });
                }
            }

            ResultLabel.Content = kopejaSumma;
            ResultAreaLabel.Content = kopejaPlatiba;

            _results = resultDictionary;
        }

        private void PrintResultsGrouped(List<ResultTypeNew> results)
        {
            var alnisResults = CalculateGroupedResults(results, "Alnis");
            var staltbriedisResults = CalculateGroupedResults(results, "Staltbriedis");
            var stirnaResults = CalculateGroupedResults(results, "Stirna");
            var mezacukaResults = CalculateGroupedResults(results, "Mezacuka");

            if (_groupedResults.Count > 0) _groupedResults.Clear();

            GroupedResultDataStackPanel.Children.Add(new Label { Content = "Alnis" });
            GroupedResultDataStackPanel.Children.Add(new DataGrid { ItemsSource = alnisResults });
            _groupedResults.Add("Alnis", alnisResults);

            GroupedResultDataStackPanel.Children.Add(new Label { Content = "Staltbriedis" });
            GroupedResultDataStackPanel.Children.Add(new DataGrid { ItemsSource = staltbriedisResults });
            _groupedResults.Add("Staltbriedis", staltbriedisResults);

            GroupedResultDataStackPanel.Children.Add(new Label { Content = "Stirna" });
            GroupedResultDataStackPanel.Children.Add(new DataGrid { ItemsSource = stirnaResults });
            _groupedResults.Add("Stirna", stirnaResults);

            GroupedResultDataStackPanel.Children.Add(new Label { Content = "Mezacuka" });
            GroupedResultDataStackPanel.Children.Add(new DataGrid { ItemsSource = mezacukaResults });
            _groupedResults.Add("Mezacuka", mezacukaResults);
        }

        private List<ResultGrouped> CalculateGroupedResults(IEnumerable<ResultTypeNew> results, string dzivnieks)
        {
            double first = 0;
            double second = 0;
            double third = 0;
            double fourth = 0;
            double fifth = 0;
            double sum = 0;

            var prices = CenasLogic.PopulatePrices();

            foreach (var result in results)
            {
                switch (GetBonitateFromResult(result, dzivnieks))
                {
                    case 1:
                        first += result.NogabalaPlatiba;
                        sum += result.NogabalaPlatiba;
                        break;
                    case 2:
                        second += result.NogabalaPlatiba;
                        sum += result.NogabalaPlatiba;
                        break;
                    case 3:
                        third += result.NogabalaPlatiba;
                        sum += result.NogabalaPlatiba;
                        break;
                    case 4:
                        fourth += result.NogabalaPlatiba;
                        sum += result.NogabalaPlatiba;
                        break;
                    case 5:
                        fifth += result.NogabalaPlatiba;
                        sum += result.NogabalaPlatiba;
                        break;
                }
            }

            var resultList = new List<ResultGrouped>
            {
                new ResultGrouped
                {
                    Bonitate = "1",
                    Platiba = first,
                    // ReSharper disable once PossibleNullReferenceException
                    Cena = first * prices.FirstOrDefault(x => x.Dzivnieks == DzivnieksStringToEnum(dzivnieks)).CenaParPirmoBonitati
                },
                new ResultGrouped
                {
                    Bonitate = "2",
                    Platiba = second,
                    // ReSharper disable once PossibleNullReferenceException
                    Cena = second * prices.FirstOrDefault(x => x.Dzivnieks == DzivnieksStringToEnum(dzivnieks)).CenaParOtroBonitati
                },
                new ResultGrouped
                {
                    Bonitate = "3",
                    Platiba = third,
                    // ReSharper disable once PossibleNullReferenceException
                    Cena = third * prices.FirstOrDefault(x => x.Dzivnieks == DzivnieksStringToEnum(dzivnieks)).CenaParTresoBonitati
                },
                new ResultGrouped
                {
                    Bonitate = "4",
                    Platiba = fourth,
                    // ReSharper disable once PossibleNullReferenceException
                    Cena = fourth * prices.FirstOrDefault(x => x.Dzivnieks == DzivnieksStringToEnum(dzivnieks)).CenaParCeturtoBonitati
                },
                new ResultGrouped
                {
                    Bonitate = "5",
                    Platiba = fifth,
                    // ReSharper disable once PossibleNullReferenceException
                    Cena = fifth * prices.FirstOrDefault(x => x.Dzivnieks == DzivnieksStringToEnum(dzivnieks)).CenaParPiektoBonitati
                },
                new ResultGrouped
                {
                    Bonitate = "Kopa",
                    Platiba = sum,
                    // ReSharper disable once PossibleNullReferenceException
                    Cena = first * prices.FirstOrDefault(x => x.Dzivnieks == DzivnieksStringToEnum(dzivnieks)).CenaParPirmoBonitati +
                        second * prices.FirstOrDefault(x => x.Dzivnieks == DzivnieksStringToEnum(dzivnieks)).CenaParOtroBonitati +
                        third * prices.FirstOrDefault(x => x.Dzivnieks == DzivnieksStringToEnum(dzivnieks)).CenaParTresoBonitati +
                        fourth * prices.FirstOrDefault(x => x.Dzivnieks == DzivnieksStringToEnum(dzivnieks)).CenaParCeturtoBonitati +
                        fifth * prices.FirstOrDefault(x => x.Dzivnieks == DzivnieksStringToEnum(dzivnieks)).CenaParPiektoBonitati
                }
            };

            return resultList;
        }

        private static int GetBonitateFromResult(ResultTypeNew result, string dzivnieks)
        {
            switch (dzivnieks)
            {
                case "Alnis":
                    return result.BonitateAlnis;
                case "Staltbriedis":
                    return result.BonitateStaltbriedis;
                case "Stirna":
                    return result.BonitateStirna;
                case "Mezacuka":
                    return result.BonitateMezaCuka;
            }
            return 0;
        }

        private void CleanResults()
        {
            ResultDataStackPanel.Children.RemoveRange(0, ResultDataStackPanel.Children.Count);
            ResultLabel.Content = string.Empty;
            ResultAreaLabel.Content = string.Empty;
        }

        private void SaveData()
        {
            var source = GetInputRecords();

            var resultContent = source.Select(item => item.KvartalaNr + ";" + item.NogabalaNr + ";" + item.Platiba + ";" + item.MezaApstakluAugsanasTips + ";" + item.KokaSuga + ";" + item.Vecums + ";").ToList();
            var time = DateTime.Now;
            var filename = Environment.CurrentDirectory + @"\InputData_" + time.Year + "_" + time.Month + "_" + time.Day + "_" + time.Hour + "_" + time.Minute + "_" + time.Second + ".txt";
            using (var file = new StreamWriter(filename))
            {
                file.WriteLine("KvartalaNumurs;NogabalaNumurs;Platiba;MezaApstakluAugsanasTips;KokaSuga;Vecums;");
                foreach (var line in resultContent)
                {
                    file.WriteLine(line);
                }
            }
        }

        private static void SaveResults(Dictionary<double, List<ResultTypeNew>> dict)
        {
            var keys = dict.Keys.ToList();
            var time = DateTime.Now;
            var filename = Environment.CurrentDirectory + @"\ResultData_" + time.Year + "_" + time.Month + "_" + time.Day + "_" + time.Hour + "_" + time.Minute + "_" + time.Second + ".txt";

            using (var file = new StreamWriter(filename))
            {
                file.WriteLine("KvartalaNumurs;NogabalaNumurs;BonitateStirna;BonitateAlnis;BonitateMezacuka;BonitateStaltBriedis;NogabalaPlatiba;CenaParNogabalu");
                
                foreach (var key in keys)
                {
                    List<ResultTypeNew> results;
                    dict.TryGetValue(key, out results);

                    if (results == null) continue;

                    var resultContent = results.Select(resultTypeNew => resultTypeNew.KvartalaNr + ";" + resultTypeNew.NogabalaNr + ";" + resultTypeNew.BonitateStirna + ";" + resultTypeNew.BonitateAlnis + ";" + resultTypeNew.BonitateMezaCuka + ";" + resultTypeNew.BonitateStaltbriedis + ";" + resultTypeNew.NogabalaPlatiba + ";" + resultTypeNew.CenaParNogabalu + ";").ToList();

                    foreach (var line in resultContent)
                    {
                        file.WriteLine(line);
                    }
                }

            }
        }

        private static IEnumerable<InputRecord> LoadData(string filename)
        {
            if (!File.Exists(filename)) return null;

            var result = new List<InputRecord>();

            using (var reader = new StreamReader(filename))
            {
                string line;
                var i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    i++;
                    if (i == 1) continue;
                    var sep = new[] { ";" };
                    var splitted = line.Split(sep, StringSplitOptions.RemoveEmptyEntries);

                    var kvartals = double.Parse(splitted[0]);
                    var nogabals = double.Parse(splitted[1]);
                    var pl = splitted[2];//.Replace(".", ",");
                    var platiba = double.Parse(pl);
                    var maat = StringToMaatEnum(splitted[3]);
                    var kokaSuga = StringKokaVeidsEnum(splitted[4]);
                    var vecums = int.Parse(splitted[5]);

                    result.Add(new InputRecord
                    {
                        KvartalaNr = kvartals,
                        NogabalaNr = nogabals,
                        Platiba = platiba,
                        MezaApstakluAugsanasTips = maat,
                        KokaSuga = kokaSuga,
                        Vecums = vecums
                        //KvartalaNr = decimal.Parse(splitted[0].Replace(",", ".")),
                        //NogabalaNr = decimal.Parse(splitted[1].Replace(",",".")),
                        //Platiba = decimal.Parse(splitted[2].Replace(",", ".")),
                        //MezaApstakluAugsanasTips = StringToMaatEnum(splitted[3]),
                        //KokaSuga = StringKokaVeidsEnum(splitted[4]),
                        //Vecums = int.Parse(splitted[5])
                    });
                }
            }

            return result;
        }

        private static MaatEnum StringToMaatEnum(string input)
        {
            switch (input)
            {
                case "Gr":
                    return MaatEnum.Gr;
                case "Ln":
                    return MaatEnum.Ln;
                case "Dm":
                    return MaatEnum.Dm;
                case "Ap":
                    return MaatEnum.Ap;
                case "Ks":
                    return MaatEnum.Ks;
                case "Vr":
                    return MaatEnum.Vr;
                case "Sl":
                    return MaatEnum.Sl;
                case "Mr":
                    return MaatEnum.Mr;
                case "Gs":
                    return MaatEnum.Gs;
                case "Mrs":
                    return MaatEnum.Mrs;
                case "Dms":
                    return MaatEnum.Dms;
                case "Grs":
                    return MaatEnum.Grs;
                case "Pv":
                    return MaatEnum.Pv;
                case "Nd":
                    return MaatEnum.Nd;
                case "Db":
                    return MaatEnum.Db;
                case "Lk":
                    return MaatEnum.Lk;
                case "Av":
                    return MaatEnum.Av;
                case "Am":
                    return MaatEnum.Am;
                case "As":
                    return MaatEnum.As;
                case "Kv":
                    return MaatEnum.Kv;
                case "Km":
                    return MaatEnum.Km;
                case "Kp":
                    return MaatEnum.Kp;
                case "NesaslPKult":
                    return MaatEnum.NesaslPKult;
                case "NesaslEKult":
                    return MaatEnum.NesaslEKult;
                case "MezaNeapkl":
                    return MaatEnum.MezaNeapkl;
                case "KlajieSunuPurvi":
                    return MaatEnum.KlajieSunuPurvi;
                case "ZaluParejas":
                    return MaatEnum.ZaluParejas;
                case "NeiezLauksaimn":
                    return MaatEnum.NeiezLauksaimn;
                case "Trases":
                    return MaatEnum.Trases;
                case "Virsaji":
                    return MaatEnum.Virsaji;
                case "DzivnBarLauces":
                    return MaatEnum.DzivnBarLauces;
                case "Krumaji":
                    return MaatEnum.Krumaji;
            }
            return MaatEnum.DzivnBarLauces;
        }

        private static KokaVeidsEnum StringKokaVeidsEnum(string input)
        {
            switch (input)
            {
                case "Ba":
                    return KokaVeidsEnum.Ba;
                case "V":
                    return KokaVeidsEnum.V;
                case "P":
                    return KokaVeidsEnum.P;
                case "B":
                    return KokaVeidsEnum.B;
                case "E":
                    return KokaVeidsEnum.E;
                case "A":
                    return KokaVeidsEnum.A;
                case "Ma":
                    return KokaVeidsEnum.Ma;
                case "Oz":
                    return KokaVeidsEnum.Oz;
                case "Os":
                    return KokaVeidsEnum.Os;
                case "L":
                    return KokaVeidsEnum.L;
                case "K":
                    return KokaVeidsEnum.K;
                case "G":
                    return KokaVeidsEnum.G;
                case "Sk":
                    return KokaVeidsEnum.Sk;
                case "Ds":
                    return KokaVeidsEnum.Ds;
                case "Ki":
                    return KokaVeidsEnum.Ki;
                case "Vi":
                    return KokaVeidsEnum.Vi;
                case "Pl":
                    return KokaVeidsEnum.Pl;
                case "Le":
                    return KokaVeidsEnum.Le;
                case "Bl":
                    return KokaVeidsEnum.Bl;
            }
            return KokaVeidsEnum.None;
        }

        private static DzivnieksEnum DzivnieksStringToEnum(string dzivnieks)
        {
            if (dzivnieks == "Alnis") return DzivnieksEnum.Alnis;
            if (dzivnieks == "Mezacuka") return DzivnieksEnum.Mezacuka;
            if (dzivnieks == "Staltbriedis") return DzivnieksEnum.Staltbriedis;
            if (dzivnieks == "Stirna") return DzivnieksEnum.Stirna;
            return DzivnieksEnum.Alnis;
        }

        private void Button_ClickSaveGroupedResult(object sender, RoutedEventArgs e)
        {
            try
            {
                var time = DateTime.Now;
                var filename = Environment.CurrentDirectory + @"\Result2_" + time.Year + "_" + time.Month + "_" +
                               time.Day + "_" + time.Hour + "_" + time.Minute + "_" + time.Second + ".txt";
                using (var file = new StreamWriter(filename))
                {
                    file.WriteLine("Dzivnieks;Bonitate;Platiba;Cena;DzivniekuSkaits1000Ha;DzivniekuSkaitsNovertetajaPlatiba;");
                    List<ResultGrouped> result;
                    _groupedResults.TryGetValue("Alnis", out result);

                    var output = new List<string>();

                    if (result != null)
                    {
                        output =
                            result.Select(
                                x =>
                                    "Alnis" + ";" + x.Bonitate + ";" + x.Platiba + ";" + x.Cena + ";" + x.DzivniekuSkaits1000Ha + ";" +
                                    x.DzivniekuSkaitsNovertetaPlatiba + ";").ToList();
                    }

                    foreach (var line in output)
                    {
                        file.WriteLine(line);
                    }

                    _groupedResults.TryGetValue("Mezacuka", out result);

                    if (result != null)
                    {
                        output =
                            result.Select(
                                x =>
                                    "Mezacuka" + ";" + x.Bonitate + ";" + x.Platiba + ";" + x.Cena + ";" + x.DzivniekuSkaits1000Ha + ";" +
                                    x.DzivniekuSkaitsNovertetaPlatiba + ";").ToList();
                    }

                    foreach (var line in output)
                    {
                        file.WriteLine(line);
                    }

                    _groupedResults.TryGetValue("Staltbriedis", out result);

                    if (result != null)
                    {
                        output =
                            result.Select(
                                x =>
                                    "Staltbriedis" + ";" + x.Bonitate + ";" + x.Platiba + ";" + x.Cena + ";" + x.DzivniekuSkaits1000Ha + ";" +
                                    x.DzivniekuSkaitsNovertetaPlatiba + ";").ToList();
                    }

                    foreach (var line in output)
                    {
                        file.WriteLine(line);
                    }

                    _groupedResults.TryGetValue("Stirna", out result);

                    if (result != null)
                    {
                        output =
                            result.Select(
                                x =>
                                    "Stirna" + ";" + x.Bonitate + ";" + x.Platiba + ";" + x.Cena + ";" + x.DzivniekuSkaits1000Ha + ";" +
                                    x.DzivniekuSkaitsNovertetaPlatiba + ";").ToList();
                    }

                    foreach (var line in output)
                    {
                        file.WriteLine(line);
                    }
                }

                SetError("Data saved!");
            }
            catch (Exception ex)
            {
                SetError("Technical error 10 - " + ex.Message);
            }
        }

        private static double CustomParse(string input)
        {
            var inputReplaced = input.Replace(".", ",");

            if (inputReplaced.Contains(","))
            {
                var splitted = inputReplaced.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
                var first = splitted[0];
                var second = splitted[1];

                if (second.Length == 1)
                {
                    return double.Parse(first) + (double.Parse(second) / 10);
                }

                return double.Parse(first) + (double.Parse(second) / 100);
                
            }

            return double.Parse(input);

        }
    }
}
 