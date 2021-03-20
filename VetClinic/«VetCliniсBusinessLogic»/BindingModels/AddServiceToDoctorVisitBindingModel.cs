namespace _VetCliniсBusinessLogic_.BindingModels
{
    /// <summary>
    /// Добавление услуги к посещению врача
    /// </summary>
    public class AddServiceToDoctorVisitBindingModel
    {
        public int DoctorVisitId { get; set; }
        public int ServiceId { get; set; }
    }
}
