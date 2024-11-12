using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;

namespace PAC1
{
    public partial class EmailTextBox : UserControl, INotifyPropertyChanged
    {
        private const string EmailPattern = @"^[\w\.-]+@([\w-]+\.)+[\w-]{2,4}$";

        // Propiedad de dependencia para Email
        public static readonly DependencyProperty EmailProperty =
            DependencyProperty.Register("Email", typeof(string), typeof(EmailTextBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnEmailChanged));

        // Propiedad de dependencia para TooltipMessage
        public static readonly DependencyProperty TooltipMessageProperty = DependencyProperty.Register(
            "TooltipMessage",
            typeof(string),
            typeof(EmailTextBox),
            new PropertyMetadata(string.Empty));

        // Propiedad de dependencia para IsEmailValid
        public static readonly DependencyProperty IsEmailValidProperty =
            DependencyProperty.Register("IsEmailValid", typeof(bool), typeof(EmailTextBox), new PropertyMetadata(false));

        // Propiedad TooltipMessage
        public string TooltipMessage
        {
            get => (string)GetValue(TooltipMessageProperty);
            set
            {
                SetValue(TooltipMessageProperty, value);
                RaisePropertyChanged(nameof(TooltipMessage));
            }
        }

        // Propiedad IsEmailValid
        public bool IsEmailValid
        {
            get => (bool)GetValue(IsEmailValidProperty);
            set
            {
                SetValue(IsEmailValidProperty, value);
                RaisePropertyChanged(nameof(IsEmailValid));  // Notificar cambio
            }
        }

        // Propiedad Email
        public string Email
        {
            get => (string)GetValue(EmailProperty);
            set
            {
                SetValue(EmailProperty, value);
                RaisePropertyChanged(nameof(Email));  // Notificar cambio de Email
                ValidateEmail(value); // Validar el correo al cambiar
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public EmailTextBox()
        {
            InitializeComponent();
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Cuando el texto cambie, se actualiza la propiedad Email
            Email = EmailInputTextBox.Text;
        }

        private void ValidateEmail(string email)
        {
            // Verificar si el correo es válido usando la expresión regular
            bool isValid = Regex.IsMatch(email, EmailPattern);
            IsEmailValid = isValid; // Actualizar el estado de la validación
            TooltipMessage = isValid ? "Format correcte" : "Format incorrecte";
            miBorde.BorderBrush = isValid ? Brushes.Blue : Brushes.Red; // Cambiar el borde según la validez
        }

        private static void OnEmailChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is EmailTextBox control)
            {
                control.ValidateEmail((string)e.NewValue); // Validar correo al cambiar
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
