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

namespace Servis_2024.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        /// <summary>
         /// Метод для открытия страницы с учётными записями
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void AccountsButton(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new AccountPage();
        }

        /// <summary>
        /// Метод для открытия страницы с заявками
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplicationButton(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new ApplicationPage();
        }

        /// <summary>
        /// Метод для открытия страницы с запасными частями
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SparPartsButton(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new SparPartPage();
        }

        /// <summary>
        /// Метод для открытия страницы с графиком технического обслуживания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaintenanceButton(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new MaintenanceSchedulePage();
        }

        /// <summary>
        /// Метод для открытия страницы с клиентами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientButton(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new ClientPage();
        }

        /// <summary>
        /// Метод для открытия страницы с оборудованиями
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EquipmentButton(object sender, RoutedEventArgs e)
        {
            AdminFrame.Content = new EquipmentPage();
        }
    }
}
