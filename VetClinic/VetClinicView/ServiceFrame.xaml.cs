using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.BusinessLogic;
using _VetCliniсBusinessLogic_.ViewModels;
using System.Windows;
using Unity;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для ServiceFrame.xaml
    /// </summary>
    public partial class ServiceFrame : Window
    {
        public int Id { set { id = value; } }
        private readonly ServiceBusinessLogic servic_logic;
        private readonly MedicationBusinessLogic medication_logic;
        private int? id;
        private Dictionary<int, string> serviceMedications;
        public ServiceFrame(ServiceBusinessLogic servic_logic, MedicationBusinessLogic medication_logic)
        {
            InitializeComponent();
            this.servic_logic = servic_logic;
            this.medication_logic = medication_logic;
        }
        private void LoadData()
        {
            try
            {
                if (serviceMedications != null)
                {
                    SelectedMedicationsListBox.Items.Clear();
                    //dataGridView.Columns[0].Visible = false;
                    foreach (var mm in serviceMedications)
                    {
                        SelectedMedicationsListBox.Items.Add(mm.Value);
                    }
                }
                CanSelectedMedicationsListBox.Items.Clear();
                foreach (var m in medication_logic.Read(null))
                {
                    if (serviceMedications.Values.Where(rec => rec == m.MedicationName).ToList().Count == 0)
                        CanSelectedMedicationsListBox.Items.Add(m);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(FIOTextBox.Text))
            {
                MessageBox.Show("Укажите врача", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            if (serviceMedications == null || serviceMedications.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            try
            {
                servic_logic.CreateOrUpdate(new ServiceBindingModel
                {
                    Id = id,
                    ServiceName = NameTextBox.Text,
                    FIO = FIOTextBox.Text,
                    Medications = serviceMedications
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK,
               MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void RefundButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedMedicationsListBox.SelectedItems.Count == 1)
            {
                serviceMedications.Remove(serviceMedications.Where(rec => rec.Value == (string)SelectedMedicationsListBox.SelectedItem).ToList()[0].Key);
                LoadData();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CanSelectedMedicationsListBox.SelectedItems.Count == 1)
            {
                serviceMedications.Add(((MedicationViewModel)CanSelectedMedicationsListBox.SelectedItem).Id
                    , ((MedicationViewModel)CanSelectedMedicationsListBox.SelectedItem).MedicationName);
                LoadData();
            }
        }

        private void ServiceFrame_Load(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ServiceViewModel view = servic_logic.Read(new ServiceBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    serviceMedications = view.Medications;
                    NameTextBox.Text = view.ServiceName.ToString();
                    FIOTextBox.Text = view.FIO.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                }
            }
            else
            {
                serviceMedications = new Dictionary<int, string>();
            }
            LoadData();
        }
    }
}
