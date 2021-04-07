using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace _VetCliniсBusinessLogic_.ViewModels
{
    public class PurchaseViewModel
    {
        public int Id { get; set; }
        public int AnimalId { get; set; }
        [DisplayName("Имя животного")]
        public string AnimalName { get; set; }
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }
        [DisplayName("Дата оплаты")]
        public DateTime DatePayment { get; set; }
        public Dictionary<string, int> MedicinesPurchases { get; set; }
    }
}
