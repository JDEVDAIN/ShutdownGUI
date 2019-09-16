using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Text.RegularExpressions;

namespace ShutdownFrameworkWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private enum ShutdownMethod {DateTime, Delay, Custom}; // 0= datetime 1=delayed 2=custom
        private ShutdownMethod SelectedShutdownMethod;

        public MainWindow()
        {
            InitializeComponent();
            //ShowTitleBar = false;
            ShowMaxRestoreButton = true;
            ResizeMode = ResizeMode.CanMinimize;
            DateTimePicker.DisplayDateStart= DateTime.Today;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.microsoft.com/en-us/windows-server/administration/windows-commands/shutdown");

        }

       
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void RadioButtonSelect(object sender, RoutedEventArgs e)
        {
            if (RadioButtonDate.IsChecked == true) // datetime
            {
                DateTimePicker.IsEnabled = true;
                //disable delay
                DelayComboBox.IsEnabled = false;
                DelayTimeTextBox.IsEnabled = false;
                //disable custom
                CustomShutdownReason.IsEnabled = false;
                //enable forceButton
                ForceShutdownCheckbox.IsEnabled = true;

                SelectedShutdownMethod = ShutdownMethod.DateTime;

            } else if (RadioButtonDelay.IsChecked == true) //delay
            {
                DelayComboBox.IsEnabled = true;
                DelayTimeTextBox.IsEnabled = true;
                //disable custom
                CustomShutdownReason.IsEnabled = false;
                //disable datetime
                DateTimePicker.IsEnabled = false;
                DateTimeLabel.IsEnabled = false;
                //enable forceButton
                ForceShutdownCheckbox.IsEnabled = true;

                SelectedShutdownMethod = ShutdownMethod.Delay;

            } else if (RadioButtonCustom.IsChecked == true) //custom
            {
                CustomShutdownReason.IsEnabled = true;
                //disable delay
                DelayComboBox.IsEnabled = false;
                DelayTimeTextBox.IsEnabled = false;
                //disable datetime
                DateTimePicker.IsEnabled = false;
                DateTimeLabel.IsEnabled = false;
                //disable forceButton
                ForceShutdownCheckbox.IsEnabled = false;

                SelectedShutdownMethod = ShutdownMethod.Custom;
            }
        }

        private void ShutdownButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (SelectedShutdownMethod == ShutdownMethod.DateTime)
            {
                ShutdownExecutor.ExecuteShutdown("","9999", ForceShutdownCheckbox.IsChecked != null && (bool) ForceShutdownCheckbox.IsChecked);
            } else if (SelectedShutdownMethod == ShutdownMethod.Delay)
            {
                ShutdownExecutor.ExecuteShutdown("", "9999", ForceShutdownCheckbox.IsChecked != null && (bool)ForceShutdownCheckbox.IsChecked);

            }
            else //Shutdown.Custom
            {
                ShutdownExecutor.ExecuteCustomShutdown(CustomShutdownReason.Text);
            }
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            ShutdownExecutor.CancelShutdown();
        }
    }

   
}
