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
        private readonly IVisitStorage _visitStorage;
        private readonly IPurchaseStorage _purchaseStorage;
        public ReportLogic(IMedicationStorage medicationStorage, IMedicineStorage
       medicineStorage, IVisitStorage visitStorage, IPurchaseStorage purchaseStorage)
        {
            _medicationStorage = medicationStorage;
            _medicineStorage = medicineStorage;
            _visitStorage = visitStorage;
            _purchaseStorage = purchaseStorage;
        }
        /// <summary>
        /// Получение списка компонент с указанием, в каких изделиях используются
        /// </summary>
        /// <returns></returns>
        public List<ReportMedicineMedicationViewModel> GetPurchasesMedication(ReportBindingModel model)
        {
            var medications = _medicationStorage.GetFullList();
            var purchases = _purchaseStorage.GetFullList();
            var medicines = _medicineStorage.GetFullList();
            var list = new List<ReportMedicineMedicationViewModel>();
            foreach (var purchase in purchases)
            {
                bool have_medications = true;
                foreach (int medication in model.Medications) 
                {
                    foreach (var medicine in purchase.MedicinesPurchases)
                    {
                        if (!medicines.FirstOrDefault(rec => rec.MedicineName == medicine.Key).Medications.ContainsKey(medication))
                        {
                            have_medications = false;
                        }
                    }
                }
                if (!have_medications)
                    continue;

                var record = new ReportMedicineMedicationViewModel
                {
                    PurchaseId = purchase.Id,
                    Date = purchase.DatePayment,
                    Sum = purchase.Sum,
                    Medications = new List<string>()
                };
                list.Add(record);
            }
            return list;
        }
        public List<ReportServiceMedicineViewModel> GetServiceMedicine(ReportBindingModel model)
        {
            var purchaces = _purchaseStorage.GetFullList().Where(rec => rec.DatePayment > model.DateFrom && rec.DatePayment < model.DateTo);
            var visits = _visitStorage.GetFullList().Where(rec => rec.DateVisit > model.DateFrom && rec.DateVisit < model.DateTo);
            var list = new List<ReportServiceMedicineViewModel>();
            foreach (var purchace in purchaces)
            {
                foreach (String medicine in purchace.MedicinesPurchases.Keys)
                {
                    var selectedvisits = visits.Where(rec => rec.DateVisit.Date == purchace.DatePayment.Date).ToList();
                    foreach (var visit in selectedvisits)
                    {
                        var report = visit.ServicesVisits.Values.Select(s => new ReportServiceMedicineViewModel
                        {
                            Date = visit.DateVisit.Date,
                            ServiceName = s,
                            MedicineName = medicine
                        }).ToList();
                        report.ForEach(rep => list.Add(rep));
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SavePurchasesToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordExelInfo
            {
                FileName = model.FileName,
                Title = "Список покупок на основе выбранных медикаментов",
                MedicineMedications = GetPurchasesMedication(model),
                NeededMedications = _medicationStorage.GetFullList().Where(rec => model.Medications.Contains(rec.Id)).Select(rec => rec.MedicationName).ToList()
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SavePurchasesToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new WordExelInfo
            {
                FileName = model.FileName,
                Title = "Список покупок на основе выбранных медикаментов",
                MedicineMedications = GetPurchasesMedication(model),
                NeededMedications = _medicationStorage.GetFullList().Where(rec => model.Medications.Contains(rec.Id)).Select(rec => rec.MedicationName).ToList()
            });
        }
    }
}
