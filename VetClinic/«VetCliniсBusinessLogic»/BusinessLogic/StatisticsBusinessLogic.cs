using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.HelperModels;
using _VetCliniсBusinessLogic_.Interfaces;
using _VetCliniсBusinessLogic_.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _VetCliniсBusinessLogic_.BusinessLogic
{
    public class StatisticsBusinessLogic
    {
        private readonly IVisitStorage _visitStorage;
        private readonly IPurchaseStorage _purchaceStorage;
        public StatisticsBusinessLogic(IVisitStorage visitStorage, IPurchaseStorage purchaceStorage)
        {
            _visitStorage = visitStorage;
            _purchaceStorage = purchaceStorage;
        }
        public List<StatisticsByServicesViewModel> GetStatisticsByServices(StatisticsBindingModel model)
        {
            var visits = _visitStorage.GetFilteredList(new VisitBindingModel { DateFrom = model.DateFrom,DateTo = model.DateTo});
            var list = visits.Where(visit => visit.ServicesVisits.ContainsKey(model.ElementId)).GroupBy(visit => visit.DateVisit).Select(visit => new StatisticsByServicesViewModel { date = visit.Key, count=visit.Count()}).ToList();
            return list;
        }
        public List<StatisticsByMedicinesViewModel> GetStatisticsByMedicine(StatisticsBindingModel model)
        {
            Dictionary<string, int> list = new Dictionary<string, int>();
            var purchaces = _purchaceStorage.GetFilteredList(new PurchaseBindingModel { DateFrom = model.DateFrom, DateTo = model.DateTo });
            foreach (var purchace in purchaces)
            {
                foreach (var medicine_purchace in purchace.MedicinesPurchases)
                {
                    if (list.ContainsKey(medicine_purchace.Value.Item1))
                        list[medicine_purchace.Value.Item1]++;
                    else
                        list.Add(medicine_purchace.Value.Item1, 1);
                }
            }
            return list.Select(l => new StatisticsByMedicinesViewModel { medicineName = l.Key,count=l.Value}).ToList();
        }
    }
}
