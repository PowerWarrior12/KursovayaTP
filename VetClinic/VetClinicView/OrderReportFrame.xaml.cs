using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.BusinessLogic;
using _VetCliniсBusinessLogic_.ViewModels;
using System.Windows;
using Unity;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для OrderReportFrame.xaml
    /// </summary>
    public partial class OrderReportFrame : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        public OrderReportFrame(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var order = logic.GetServiceMedicine(new ReportBindingModel { 
                    DateFrom = DatePickerFrom.SelectedDate,
                    DateTo = DatePickerTo.SelectedDate
                });
                if (order != null)
                {
                    DataGridView.Items.Clear();
                    foreach (var elem in order)
                    {
                        DataGridView.Items.Add(elem);
                    }
                }
                System.Windows.MessageBox.Show("Получилось", "Информация", MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
        }

        private void ButtonMail_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
