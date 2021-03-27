using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.HelperModels;
using _VetCliniсBusinessLogic_.Interfaces;
using _VetCliniсBusinessLogic_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _VetCliniсBusinessLogic_.BusinessLogic
{
    public class ReportLogic
    {
        private readonly IMedicationStorage _medicationStorage;
        private readonly IMedicineStorage _medicineStorage;
        public ReportLogic(IMedicationStorage medicationStorage, IMedicineStorage
       medicineStorage)
        {
            _medicationStorage = medicationStorage;
            _medicineStorage = medicineStorage;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportMedicineMedicationViewModel> GetMedicineMedication(ReportBindingModel model)
        {
            var medications = _medicationStorage.GetFullList();
            var medicines = _medicineStorage.GetFullList();
            var list = new List<ReportMedicineMedicationViewModel>();
            foreach (var medicine in medicines)
            {
                bool have_medications = true;
                foreach (String medication in model.Medications) {
                    if (medicine.Medications.Values.FirstOrDefault(rec => rec == medication) == null) 
                    {
                        have_medications = false;
                    }
                }
                if (!have_medications)
                    continue;

                var record = new ReportMedicineMedicationViewModel
                {
                    MedicineName = medicine.MedicineName,
                    Medications = new List<string>()
                };
                foreach (var medication in medications)
                {
                    if (medicine.Medications.ContainsKey(medication.Id))
                    {
                        record.Medications.Add(medication.MedicationName);
                    }
                }
                list.Add(record);
            }
            return list;
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordExelInfo
            {
                FileName = model.FileName,
                Title = "Список лекарств",
                MedicineMedications = GetMedicineMedication(model),
                NeededMedications = model.Medications
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveDishComponentToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new WordExelInfo
            {
                FileName = model.FileName,
                Title = "Список лекарств",
                MedicineMedications = GetMedicineMedication(model),
                NeededMedications = model.Medications
            });
        }
    }
}
