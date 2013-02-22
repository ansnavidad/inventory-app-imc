using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions;

namespace InventoryApp.ViewModel.Login
{
    public class PasswordValidator : FrameworkElement
    {
        static IDictionary<PasswordBox, Brush> _passwordBoxes = new Dictionary<PasswordBox, Brush>();
        static IDictionary<TextBox, Brush> _textBox = new Dictionary<TextBox, Brush>();

        public static readonly DependencyProperty Box1Property =
            DependencyProperty.Register("Box1", typeof(PasswordBox), typeof(PasswordValidator), new PropertyMetadata(Box1Changed));
        public static readonly DependencyProperty Box2Property = 
            DependencyProperty.Register("Box2", typeof(PasswordBox), typeof(PasswordValidator), new PropertyMetadata(Box2Changed));
        public static readonly DependencyProperty TextProperty = 
            DependencyProperty.Register("TextMail", typeof(TextBox), typeof(PasswordValidator), new PropertyMetadata(TextChanged));

        
        public PasswordBox Box1
        {
            get { return (PasswordBox)GetValue(Box1Property); }
            set { SetValue(Box1Property, value); }
        }
        public PasswordBox Box2
        {
            get { return (PasswordBox)GetValue(Box2Property); }
            set { SetValue(Box2Property, value); }
        }
        public TextBox TextMail
        {
            get { return (TextBox)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private static void Box1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }
        private static void Box2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pv = (PasswordValidator)d;
            _passwordBoxes[pv.Box2] = pv.Box2.BorderBrush;
            _passwordBoxes[pv.Box1] = pv.Box1.BorderBrush;
            pv.Box2.LostFocus += (obj, evt) =>
            {
                if (pv.Box1.Password != pv.Box2.Password)
                {
                    pv.Box2.BorderBrush = new SolidColorBrush(Colors.Red);
                    pv.Box2.ToolTip = "Contraseñas incorrectas ...";
                    
                    return;
                }
                if (pv.Box1.Password.Length < 8 || pv.Box2.Password.Length < 8)
                {
                    pv.Box1.BorderBrush = new SolidColorBrush(Colors.Red);
                    pv.Box2.BorderBrush = new SolidColorBrush(Colors.Red);
                    pv.Box1.ToolTip = "La longitud mínima de la contraseña es de 8 caracteres..";
                    pv.Box2.ToolTip = "La longitud mínima de la contraseña es de 8 caracteres..";
                    return;
                }
                else
                {
                    pv.Box1.BorderBrush = _passwordBoxes[pv.Box1];
                    pv.Box2.BorderBrush = _passwordBoxes[pv.Box2];
                    pv.Box1.ToolTip = null;
                    pv.Box2.ToolTip = null;
                    
                }
            };
        }
        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pv = (PasswordValidator)d;
            _textBox[pv.TextMail] = pv.TextMail.BorderBrush;
            pv.TextMail.LostFocus += (obj, evt) =>
            {
                if (!Regex.IsMatch(pv.TextMail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    pv.TextMail.BorderBrush = new SolidColorBrush(Colors.Red);
                    pv.TextMail.ToolTip = "Email  incorrecto..";
                    
                }
                else
                {
                    
                    pv.TextMail.BorderBrush = _textBox[pv.TextMail];
                    pv.TextMail.ToolTip = null;
                }
            };
        }
        
        
    }

 }
