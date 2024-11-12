using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PAC1
{
    public partial class MinLengthTextBox : UserControl
    {
        // DependencyProperty for the Text property
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(MinLengthTextBox),
            new PropertyMetadata(string.Empty, OnTextChanged));

        // DependencyProperty for TooltipMessage
        public static readonly DependencyProperty TooltipMessageProperty = DependencyProperty.Register(
            "TooltipMessage",
            typeof(string),
            typeof(MinLengthTextBox),
            new PropertyMetadata(string.Empty)
        );

        // DependencyProperty for IsValidText
        public static readonly DependencyProperty IsValidTextProperty = DependencyProperty.Register(
            "IsValidText",
            typeof(bool),
            typeof(MinLengthTextBox),
            new PropertyMetadata(false)
        );

        // Property for Tooltip message
        public string TooltipMessage
        {
            get => (string)GetValue(TooltipMessageProperty);
            set => SetValue(TooltipMessageProperty, value);
        }

        // Property for IsValidText
        public bool IsValidText
        {
            get => (bool)GetValue(IsValidTextProperty);
            set => SetValue(IsValidTextProperty, value);
        }

        // Backing field for Mask (minimum length)
        private int _longitut = 3;

        public int Mask
        {
            get => _longitut;
            set => _longitut = value;
        }

        // Constructor
        public MinLengthTextBox()
        {
            InitializeComponent();
        }

        // DependencyProperty getter/setter for Text
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // TextChanged event handler to update TooltipMessage and Border color
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (MinLengthTextBox)d;
            control.ValidateForm();
        }

        // Validation function to check if text length meets the requirement
        private void ValidateForm()
        {
            if (NameTextBox.Text.Length >= _longitut)
            {
                TooltipMessage = "El nom es correcte";
                miBorde.BorderBrush = Brushes.Blue;
                IsValidText = true; // Set IsValidText to true if validation passes
            }
            else
            {
                TooltipMessage = $"El nom ha de tenir {_longitut} o mes caracters";
                miBorde.BorderBrush = Brushes.Red;
                IsValidText = false; // Set IsValidText to false if validation fails
            }
        }
    }
}
