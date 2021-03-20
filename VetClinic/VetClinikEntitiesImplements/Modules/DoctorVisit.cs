using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinikModels.Modules
{
    /// <summary>
    /// Сущность посещения врача
    /// </summary>
    public class DoctorVisit
    {
        public int Id { get; set; }
        [Required]
        public string ClientFIO { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [ForeignKey("DoctorVisitId")]
        public virtual List<DoctorVisitService> DoctorVisitsServices { get; set; }
    }
}
