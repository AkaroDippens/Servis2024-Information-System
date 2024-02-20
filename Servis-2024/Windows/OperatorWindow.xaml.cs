using Servis_2024.Pages;
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
using System.Windows.Shapes;

namespace Servis_2024.Windows
{
    /// <summary>
    /// Логика взаимодействия для OperatorWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Window
    {
        public OperatorWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод для открытия страницы с заявками
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationButton(object sender, RoutedEventArgs e)
        {
            if (OperatorFrame.Content is ApplicationPage page)
            {
                page.deleteButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                ApplicationPage newPage = new ApplicationPage();
                newPage.deleteButton.Visibility = Visibility.Collapsed;
                OperatorFrame.Content = newPage;
            }
        }

        /// <summary>
        /// Метод для открытия страницы с графиками технического обслуживания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaintenanceButton(object sender, RoutedEventArgs e)
        {
            if (OperatorFrame.Content is MaintenanceSchedulePage page)
            {
                page.editButton.Visibility = Visibility.Collapsed;
                page.deleteButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                MaintenanceSchedulePage newPage = new MaintenanceSchedulePage();
                newPage.editButton.Visibility = Visibility.Collapsed;
                newPage.deleteButton.Visibility = Visibility.Collapsed;
                OperatorFrame.Content = newPage;
            }
        }

        /// <summary>
        /// Метод для открытия страницы с клиентами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientButton(object sender, RoutedEventArgs e)
        {
            OperatorFrame.Content = new ClientPage();
        }

        /// <summary>
        /// Метод для открытия страницы с оборудованиями
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EquipmentButton(object sender, RoutedEventArgs e)
        {
            OperatorFrame.Content = new EquipmentPage();
        }
    }
}
