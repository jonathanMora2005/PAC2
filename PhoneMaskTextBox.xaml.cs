using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PAC1
{
    public partial class PhoneMaskTextBox : UserControl
    {
        // Dependency Property for PhoneNumber
        public static readonly DependencyProperty PhoneNumberProperty = DependencyProperty.Register(
            "PhoneNumber",
            typeof(string),
            typeof(PhoneMaskTextBox),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPhoneNumberChanged)
        );

        public string PhoneNumber
        {
            get => (string)GetValue(PhoneNumberProperty);
            set => SetValue(PhoneNumberProperty, value);
        }

        // Dependency Property for Mask
        public static readonly DependencyProperty MaskProperty = DependencyProperty.Register(
            "Mask",
            typeof(string),
            typeof(PhoneMaskTextBox),
            new PropertyMetadata("###-###-###", OnMaskChanged)
        );

        public string Mask
        {
            get => (string)GetValue(MaskProperty);
            set => SetValue(MaskProperty, value);
        }

        // Dependency Property for Text
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(PhoneMaskTextBox),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged)
        );

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // Dependency Property for IsPhoneValid
        public static readonly DependencyProperty IsPhoneValidProperty = DependencyProperty.Register(
            "IsPhoneValid",
            typeof(bool),
            typeof(PhoneMaskTextBox),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
        );

        public bool IsPhoneValid
        {
            get => (bool)GetValue(IsPhoneValidProperty);
            set => SetValue(IsPhoneValidProperty, value);
        }

        private string _mask = "###-###-###";

        public PhoneMaskTextBox()
        {
            InitializeComponent();
        }

        // Event for TextChanged in InputTextBox
        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string rawText = Regex.Replace(InputTextBox.Text, @"\D", "");

            if (PhoneNumber != rawText)
            {
                PhoneNumber = ApplyMask(rawText);
                InputTextBox.Text = PhoneNumber;
                InputTextBox.CaretIndex = InputTextBox.Text.Length;
            }
            e.Handled = true;
        }

        // Event for PreviewTextInput in InputTextBox
        private void InputTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"\d"))
            {
                e.Handled = true;
            }
            else
            {
                InputTextBox.Background = Brushes.White;
                e.Handled = false;
            }
        }

        // Called when PhoneNumber property changes
        private static void OnPhoneNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (PhoneMaskTextBox)d;
            control.InputTextBox.Text = control.ApplyMask((string)e.NewValue);
            control.Text = control.PhoneNumber;
        }

        // Called when Mask property changes
        private static void OnMaskChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (PhoneMaskTextBox)d;
            control._mask = (string)e.NewValue;
            control.ApplyMask();
        }

        // Called when Text property changes
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (PhoneMaskTextBox)d;
            control.PhoneNumber = (string)e.NewValue;
        }

        // Applies the mask to the current PhoneNumber
        private void ApplyMask()
        {
            PhoneNumber = ApplyMask(PhoneNumber);
        }

        // Helper method to apply the mask to given text
        private string ApplyMask(string text)
        {
            string numericText = Regex.Replace(text ?? "", @"\D", "");
            miBorde.BorderBrush = numericText.Length == 9 ? Brushes.Blue : Brushes.Red;

            // Set IsPhoneValid based on whether the input meets the required length for the mask
            IsPhoneValid = numericText.Length == 9;

            if (numericText.Length > 9) numericText = numericText.Substring(0, 9);

            if (_mask == "###-###-###")
            {
                if (numericText.Length > 3) numericText = numericText.Insert(3, "-");
                if (numericText.Length > 7) numericText = numericText.Insert(7, "-");
            }
            else if (_mask == "+## ###-###-###")
            {
                if (numericText.Length > 2) numericText = numericText.Insert(2, " ");
                if (numericText.Length > 6) numericText = numericText.Insert(6, "-");
                if (numericText.Length > 10) numericText = numericText.Insert(10, "-");
            }
            else if (_mask == "### ## ## ##")
            {
                if (numericText.Length > 3) numericText = numericText.Insert(3, " ");
                if (numericText.Length > 6) numericText = numericText.Insert(6, " ");
                if (numericText.Length > 9) numericText = numericText.Insert(9, " ");
            }

            return numericText;
        }
    }
}