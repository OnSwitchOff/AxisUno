// <copyright file="PaymentButton.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AxisUno.Controls
{
    using CommunityToolkit.Mvvm.Input;
    using Microinvest.CommonLibrary.Enums;
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;

    /// <summary>
    /// Custom button with image and text.
    /// </summary>
    public sealed partial class PaymentButton : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentButton"/> class.
        /// </summary>
        public PaymentButton()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets a command to invoke when button was pressed.
        /// </summary>
        /// <date>20.04.2022.</date>
        public IRelayCommand ButtonClickCommand
        {
            get => (IRelayCommand)this.GetValue(ButtonClickCommandProperty);
            set => this.SetValue(ButtonClickCommandProperty, value);
        }

        /// <summary>
        /// Register property "ButtonClickCommand".
        /// </summary>
        /// <date>20.04.2022.</date>
        public static readonly DependencyProperty ButtonClickCommandProperty =
            DependencyProperty.Register("ButtonClickCommand", typeof(IRelayCommand), typeof(PaymentButton), null);

        /// <summary>
        /// Gets or sets type of payment.
        /// </summary>
        /// <date>20.04.2022.</date>
        public EPaymentTypes PaymentType
        {
            get => (EPaymentTypes)this.GetValue(PaymentTypeProperty);
            set => this.SetValue(PaymentTypeProperty, value);
        }

        /// <summary>
        /// Register property "PaymentType".
        /// </summary>
        /// <date>20.04.2022.</date>
        public static readonly DependencyProperty PaymentTypeProperty =
            DependencyProperty.Register("PaymentType", typeof(EPaymentTypes), typeof(PaymentButton), null);

        /// <summary>
        /// Gets or sets path to image of button.
        /// </summary>
        /// <date>20.04.2022.</date>
        public string ImagePath
        {
            get => (string)this.GetValue(ImagePathProperty);
            set => this.SetValue(ImagePathProperty, value);
        }

        /// <summary>
        /// Register property "ImagePath".
        /// </summary>
        /// <date>20.04.2022.</date>
        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register("ImagePath", typeof(string), typeof(PaymentButton), null);

        /// <summary>
        /// Gets or sets text of the button.
        /// </summary>
        /// <date>20.04.2022.</date>
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        /// <summary>
        /// Register property "Text".
        /// </summary>
        /// <date>20.04.2022.</date>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(PaymentButton), null);
    }
}
