﻿using System.Collections.Generic;

namespace _VetCliniсBusinessLogic_.BindingModels
{
    /// <summary>
    /// Лекарство, которое основано на медикаментах, предаставляемое ветеринарной клиникой
    /// </summary>
    public class MedicineBindingModel
    {
        public int? Id { get; set; }
        public string MedicineName { get; set; }
        public Dictionary<int, (string, int)> Medications { get; set; }
    }
}
