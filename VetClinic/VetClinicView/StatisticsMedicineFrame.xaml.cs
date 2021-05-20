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
    /// Логика взаимодействия для StatisticsMedicineFrame.xaml
    /// </summary>
    public partial class StatisticsMedicineFrame : Window
    {
        private readonly StatisticsBusinessLogic statistics_logic;
        public StatisticsMedicineFrame(StatisticsBusinessLogic statistics_logic)
        {
            InitializeComponent();
            this.statistics_logic = statistics_logic;
        }
        private void LoadData()
        {
            try
            {
                var list = statistics_logic.GetStatisticsByMedicine(new StatisticsBindingModel
                {
                    DateFrom = DatePickerFrom.SelectedDate,
                    DateTo = DatePickerTo.SelectedDate,
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
            var list = statistics_logic.GetStatisticsByMedicine(new StatisticsBindingModel
            {
                DateFrom = DatePickerFrom.SelectedDate,
                DateTo = DatePickerTo.SelectedDate,
            });
            SeriesCollection series = new SeriesCollection();

            ChartValues<int> numberOfVisits = new ChartValues<int>();

            List<string> medicinesName = new List<string>();

            foreach (var item in list)

            {
                numberOfVisits.Add(item.count);
                medicinesName.Add(item.medicineName);
            }
            cartesianChart.AxisX.Clear();
            cartesianChart.AxisX.Add(new Axis()
            {
                Title = "\nЛекартва",
                Labels = medicinesName
            });
            LineSeries animalLine = new LineSeries
            {
                Title = "Статистика по лекарствам: ",
                Values = numberOfVisits
            };

            series.Add(animalLine);
            cartesianChart.Series = series;
        }

        private void GraphButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            ChartCreation();
        }
    }
}
