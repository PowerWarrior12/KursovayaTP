using _VetCliniсBusinessLogic_.BindingModels;

namespace _VetCliniсBusinessLogic_.Interfaces
{
    public interface IUserStorage
    {
        UserBindingModel GetElement(UserBindingModel model);
        void Insert(UserBindingModel model);
        void Update(UserBindingModel model);
        void Delete(UserBindingModel model);
    }
}
