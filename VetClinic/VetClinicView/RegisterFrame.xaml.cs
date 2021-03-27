﻿using _VetCliniсBusinessLogic_.BindingModels;
using _VetCliniсBusinessLogic_.BusinessLogic;
using System.Windows;
using Unity;
using System;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для RegisterFrame.xaml
    /// </summary>
    public partial class RegisterFrame : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly UserBusinessLogic logic;
        public RegisterFrame(UserBusinessLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка");
                return;
            }
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка");
                return;
            }
            try
            {
                logic.CreateOrUpdate(new UserBindingModel
                {
                    Id = null,
                    Login = LoginTextBox.Text,
                    Password = PasswordTextBox.Text,
                });
                MessageBox.Show("Регистрация прошла успешно", "Сообщение");
                var form = Container.Resolve<MainFrame>();
                Close();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<RegisterLoginFrame>();
            Close();
            form.ShowDialog();
        }
    }
}