using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.Interfaces;
using VetClinikEntitiesImplements.Modules;
using System;
using System.Linq;
using VetClinikEntitiesImplements;

namespace VetClinikEntitiesImplements.Implements
{
    public class UserStorage : IUserStorage
    {
        public void Delete(UserBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                User element = context.Users.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Users.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public UserBindingModel GetElement(UserBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new VetClinicDataBase())
            {
                var component = context.Users
                .FirstOrDefault(rec => rec.Login == model.Login ||
               rec.Id == model.Id);
                return component != null ?
                new UserBindingModel
                {
                    Id = component.Id,
                    Login = component.Login,
                    Password = component.Password
                } :
               null;
            }
        }

        public void Insert(UserBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                context.Users.Add(CreateModel(model, new User()));
                context.SaveChanges();
            }
        }

        public void Update(UserBindingModel model)
        {
            using (var context = new VetClinicDataBase())
            {
                var element = context.Users.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        private User CreateModel(UserBindingModel model, User user)
        {
            user.Login = model.Login;
            user.Password = model.Password;
            return user;
        }
    }
}
