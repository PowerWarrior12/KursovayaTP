using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.Interfaces;
using System;
using System.Collections.Generic;

namespace _VetCliniсBusinessLogic_.BusinessLogic
{
    public class UserBusinessLogic
    {
        private readonly IUserStorage _userStorage;
        public UserBusinessLogic(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }
        public void Login(UserBindingModel model)
        {
            var element = _userStorage.GetElement(new UserBindingModel
            {
                Login = model.Login
            });
            if (element == null)
            {
                throw new Exception("Пользователь не найден");
            }
            if (element.Password != model.Password)
            {
                throw new Exception("Неверный пароль");
            }
        }
        public void CreateOrUpdate(UserBindingModel model)
        {
            var element = _userStorage.GetElement(new UserBindingModel
            {
                Login = model.Login
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть пользователь с таким логином");
            }
            if (model.Id.HasValue)
            {
                _userStorage.Update(model);
            }
            else
            {
                _userStorage.Insert(model);
            }
        }
        public void Delete(UserBindingModel model)
        {
            var element = _userStorage.GetElement(new UserBindingModel
            {
                Id =  model.Id
            });
            if (element == null)
            {
                throw new Exception("Пользователь не найден");
            }
            _userStorage.Delete(model);
        }
    }
}
