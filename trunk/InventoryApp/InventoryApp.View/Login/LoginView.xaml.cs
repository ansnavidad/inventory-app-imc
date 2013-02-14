﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InventoryApp.ViewModel;
using InventoryApp.ViewModel.Login;

namespace InventoryApp.View.Login
{
    /// <summary>
    /// Lógica de interacción para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            
            FocusManager.SetFocusedElement(this, this.txtUsuario);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void recuperar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RecoverPassword recuperar = new RecoverPassword();
            recuperar.Owner = this;
            recuperar.DataContext = this.DataContext;
            this.Hide();
            recuperar.ShowDialog();
        }

        private void registro_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RegisterUser registro = new RegisterUser();
            registro.Owner = this;
            registro.DataContext = this.DataContext;
            this.Hide();
            registro.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        
        private void checkBoxOpenMain_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {            
            if (checkBoxOpenMain.IsEnabled)
            {
                
                MessageBoxResult result = MessageBox.Show("Bienvenido al sistema de inventarios Elara", "Bienvenido", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                MainWindow mw = new MainWindow();                
                mw.DataContext = new MainWindowViewModel();
                this.Close();
                mw.ShowDialog();
            }
        }

        private void pass_PasswordChanged(object sender, RoutedEventArgs e)
        {
                        
        }
    }
    
    public class BindablePasswordBox : Decorator
    {
        /// <summary>
        /// The password dependency property.
        /// </summary>
        public static readonly DependencyProperty PasswordProperty;

        private bool isPreventCallback;
        private RoutedEventHandler savedCallback;

        /// <summary>
        /// Static constructor to initialize the dependency properties.
        /// </summary>
        static BindablePasswordBox()
        {
            PasswordProperty = DependencyProperty.Register(
                "Password",
                typeof(string),
                typeof(BindablePasswordBox),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnPasswordPropertyChanged))
            );
        }

        /// <summary>
        /// Saves the password changed callback and sets the child element to the password box.
        /// </summary>
        public BindablePasswordBox()
        {
            savedCallback = HandlePasswordChanged;

            PasswordBox passwordBox = new PasswordBox();
            passwordBox.PasswordChanged += savedCallback;
            Child = passwordBox;
        }

        /// <summary>
        /// The password dependency property.
        /// </summary>
        public string Password
        {
            get { return GetValue(PasswordProperty) as string; }
            set { SetValue(PasswordProperty, value); }
        }

        /// <summary>
        /// Handles changes to the password dependency property.
        /// </summary>
        /// <param name="d">the dependency object</param>
        /// <param name="eventArgs">the event args</param>
        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs eventArgs)
        {
            BindablePasswordBox bindablePasswordBox = (BindablePasswordBox)d;
            PasswordBox passwordBox = (PasswordBox)bindablePasswordBox.Child;

            if (bindablePasswordBox.isPreventCallback)
            {
                return;
            }

            passwordBox.PasswordChanged -= bindablePasswordBox.savedCallback;
            passwordBox.Password = (eventArgs.NewValue != null) ? eventArgs.NewValue.ToString() : "";
            passwordBox.PasswordChanged += bindablePasswordBox.savedCallback;
        }

        /// <summary>
        /// Handles the password changed event.
        /// </summary>
        /// <param name="sender">the sender</param>
        /// <param name="eventArgs">the event args</param>
        private void HandlePasswordChanged(object sender, RoutedEventArgs eventArgs)
        {
            PasswordBox passwordBox = (PasswordBox)sender;

            isPreventCallback = true;
            Password = passwordBox.Password;
            isPreventCallback = false;
        }
    }    
}
