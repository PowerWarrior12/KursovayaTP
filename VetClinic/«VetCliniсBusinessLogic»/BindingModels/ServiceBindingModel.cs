using System;
using System.Collections.Generic;

namespace _VetCliniсBusinessLogic_.BindingModels
{
    /// <summary>
    /// Услуга, оказываемая ветеринарной клиникой
    /// </summary>
    public class ServiceBindingModel
    {
        public int? Id { get; set; }
        public string ServiceName { get; set; }
        public string FIO { get; set; }
        public Dictionary<int, (string, int)> Medications { get; set; }
    }
}
