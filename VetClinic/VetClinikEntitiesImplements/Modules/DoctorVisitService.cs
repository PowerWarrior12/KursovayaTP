using System.ComponentModel.DataAnnotations;

namespace VetClinikModels.Modules
{
    /// <summary>
    /// Сущность связи посещения врача и услуги
    /// </summary>
    public class DoctorVisitService
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int DoctorVisitId { get; set; }
        [Required]
        public virtual DoctorVisit DoctorVisit { get; set; }
        public virtual Service Service { get; set; }
    }
}
