﻿using VetClinikEntitiesImplements.Modules;

namespace VetClinikEntitiesImplements.Modules
{
    public class MedicinePurchase
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public int PurchaseId { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}