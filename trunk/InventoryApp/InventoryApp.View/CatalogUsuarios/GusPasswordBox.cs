using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace InventoryApp.View.CatalogUsuarios
{
    public class GusPasswordBox : Decorator
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
        static GusPasswordBox()
        {
            PasswordProperty = DependencyProperty.Register(
                "Password",
                typeof(string),
                typeof(GusPasswordBox),
                new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnPasswordPropertyChanged))
            );
        }
        
        /// <summary>
        /// Guarda el callback contraseña cambiada y establece el elemento secundario a la caja de contraseña.
        /// </summary>
        public GusPasswordBox()
        {
            savedCallback = HandlePasswordChanged;

            PasswordBox passwordBox = new PasswordBox();
            passwordBox.PasswordChanged += savedCallback;
            Child = passwordBox;
            passwordBox.MaxLength = 12;
        }

        /// <summary>
        /// La propiedad de dependencia contraseña.
        /// </summary>
        public string Password
        {
            get { return GetValue(PasswordProperty) as string; }
            set { SetValue(PasswordProperty, value); }
        }

        /// <summary>
        /// Maneja los cambios en la propiedad de dependencia contraseña.
        /// </summary>
        /// <param name="d">el objeto de dependencia</param>
        /// <param name="eventArgs">los argumentos del evento</param>
        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs eventArgs)
        {
            GusPasswordBox bindablePasswordBox = (GusPasswordBox)d;
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
        /// Controla el evento contraseña cambiada.
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
