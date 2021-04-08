using System;
using System.Collections.Generic;


namespace _VetCliniсBusinessLogic_.ViewModels
{
    public class ReportMedicineMedicationViewModel
    {
        public int PurchaseId { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public List<string> Medications { get; set; }
    }
}
