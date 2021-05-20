using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.BusinessLogic;
using _VetCliniсBusinessLogic_.ViewModels;
using System.Windows;
using Unity;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using LiveCharts;
using LiveCharts.Wpf;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для StatisticsFrame.xaml
    /// </summary>
    public partial class StatisticsFrame : Window
    {
        private readonly ServiceBusinessLogic servic_logic;
        private readonly StatisticsBusinessLogic statistics_logic;
        public StatisticsFrame(ServiceBusinessLogic servic_logic, StatisticsBusinessLogic statistics_logic)
        {
            InitializeComponent();
            this.servic_logic = servic_logic;
            this.statistics_logic = statistics_logic;
            ServiceComboBox.ItemsSource = servic_logic.Read(new ServiceBindingModel { DoctorId = App.DoctorId});
        }
        private void LoadData()
        {
            try
            {
                var list = statistics_logic.GetStatisticsByServices(new StatisticsBindingModel { 
                    DateFrom = DatePickerFrom.SelectedDate, 
                    DateTo = DatePickerTo.SelectedDate,
                    ElementId = ((ServiceViewModel)ServiceComboBox.SelectedItem).Id
                });
                if (list != null)
                {
                    dataGridView.ItemsSource = list;
                    dataGridView.ColumnWidth = DataGridLength.Auto;
                    var columns = dataGridView.Columns;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }
        private void ChartCreation()

        {
            var list = statistics_logic.GetStatisticsByServices(new StatisticsBindingModel
            {
                DateFrom = DatePickerFrom.SelectedDate,
                DateTo = DatePickerTo.SelectedDate,
                ElementId = ((ServiceViewModel)ServiceComboBox.SelectedItem).Id
            });
            SeriesCollection series = new SeriesCollection();

            ChartValues<int> numberOfVisits = new ChartValues<int>();

            List<string> animalsName = new List<string>();

            foreach (var item in list)

            {
                numberOfVisits.Add(item.count);
                animalsName.Add(item.date.ToShortDateString());
            }
            cartesianChart.AxisX.Clear();
            cartesianChart.AxisX.Add(new Axis()
            {
                Title = "\n" + ((ServiceViewModel)ServiceComboBox.SelectedItem).ServiceName,
                Labels = animalsName
            });
            LineSeries animalLine = new LineSeries
            {
                Title = "Статистика по услуге: ",
                Values = numberOfVisits
            };

            series.Add(animalLine);
            cartesianChart.Series = series;
        }

        private void ServiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception)
            {
                
            }
        }

        private void GraphButton_Click(object sender, RoutedEventArgs e)
        {
            ChartCreation();
        }
    }
}
