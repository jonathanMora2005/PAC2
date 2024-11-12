using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PAC1
{
    public partial class DNIcontrol : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly DependencyProperty TooltipMessageProperty = DependencyProperty.Register(
           "TooltipMessage",
           typeof(string),
           typeof(DNIcontrol),
           new PropertyMetadata(string.Empty)
        );

        public string TooltipMessage
        {
            get => (string)GetValue(TooltipMessageProperty);
            set => SetValue(TooltipMessageProperty, value);
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(DNIcontrol),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged)
        );

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DNIcontrol control)
            {
                control.DNITextBox.Text = (string)e.NewValue;
            }
        }

        public static readonly DependencyProperty IsValidDNIProperty = DependencyProperty.Register(
            "IsValidDNI",
            typeof(bool),
            typeof(DNIcontrol),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsValidDNIChanged)
        );

        public bool IsValidDNI
        {
            get => (bool)GetValue(IsValidDNIProperty);
            set
            {
                SetValue(IsValidDNIProperty, value);
                RaisePropertyChanged(nameof(IsValidDNI));
            }
        }

        private static void OnIsValidDNIChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DNIcontrol control)
            {
                control.RaisePropertyChanged(nameof(IsValidDNI));
            }
        }

        public DNIcontrol()
        {
            InitializeComponent();
            DNITextBox.SetBinding(ToolTipProperty, new System.Windows.Data.Binding("TooltipMessage") { Source = this });
        }

        private void DNIChanged(object sender, TextChangedEventArgs e)
        {
            Text = DNITextBox.Text;
            ValidateForm();
        }

        private void ValidateForm()
        {
            string input = DNITextBox.Text;

            string pattern = @"^\d{8}[A-Za-z]$";

            if (Regex.IsMatch(input, pattern))
            {
                TooltipMessage = "El DNI és correcte";
                miBorde.BorderBrush = Brushes.Blue;
                IsValidDNI = true;
            }
            else
            {
                TooltipMessage = "El DNI és incorrecte";
                miBorde.BorderBrush = Brushes.Red;
                IsValidDNI = false;
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
