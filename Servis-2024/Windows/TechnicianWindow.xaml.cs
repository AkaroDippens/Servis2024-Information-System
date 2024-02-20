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
    /// Логика взаимодействия для TechnicianWindow.xaml
    /// </summary>
    public partial class TechnicianWindow : Window
    {
        public TechnicianWindow()
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
            if (TechnicianFrame.Content is ApplicationPage page)
            {
                page.addButton.Visibility = Visibility.Collapsed;
                page.deleteButton.Visibility = Visibility.Collapsed;
                page.create_date.Visibility = Visibility.Collapsed;
                page.comboTechnician.Visibility = Visibility.Collapsed;
                page.comboClient.Visibility = Visibility.Collapsed;
                page.execute_date.Visibility = Visibility.Collapsed;
                page.comment.Visibility = Visibility.Collapsed;
                page.dateTextBlock.Visibility = Visibility.Collapsed;
                page.TechTextBlock.Visibility = Visibility.Collapsed;
                page.ClientTextBlock.Visibility = Visibility.Collapsed;
                page.executeDateTextBlock.Visibility = Visibility.Collapsed;
                page.commentTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                ApplicationPage newPage = new ApplicationPage();
                newPage.addButton.Visibility = Visibility.Collapsed;
                newPage.deleteButton.Visibility = Visibility.Collapsed;
                newPage.create_date.Visibility = Visibility.Collapsed;
                newPage.comboTechnician.Visibility = Visibility.Collapsed;
                newPage.comboClient.Visibility = Visibility.Collapsed;
                newPage.execute_date.Visibility = Visibility.Collapsed;
                newPage.comment.Visibility = Visibility.Collapsed;
                newPage.dateTextBlock.Visibility = Visibility.Collapsed;
                newPage.TechTextBlock.Visibility = Visibility.Collapsed;
                newPage.ClientTextBlock.Visibility = Visibility.Collapsed;
                newPage.executeDateTextBlock.Visibility = Visibility.Collapsed;
                newPage.commentTextBlock.Visibility = Visibility.Collapsed;
                TechnicianFrame.Content = newPage;
            }

        }

        /// <summary>
        /// Метод для открытия страницы с запасными частями
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SparPartsButton(object sender, RoutedEventArgs e)
        {
            if (TechnicianFrame.Content is SparPartPage page)
            {
                page.deleteButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                SparPartPage newPage = new SparPartPage();
                newPage.deleteButton.Visibility = Visibility.Collapsed;
                TechnicianFrame.Content = newPage;
            }
        }

        /// <summary>
        /// Метод для открытия страницы с графиком технического обслуживания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaintenanceButton(object sender, RoutedEventArgs e)
        {
            if (TechnicianFrame.Content is MaintenanceSchedulePage page)
            {
                page.addButton.Visibility = Visibility.Collapsed;
                page.editButton.Visibility = Visibility.Collapsed;
                page.deleteButton.Visibility = Visibility.Collapsed;
                page.service_date.Visibility = Visibility.Collapsed;
                page.comboTechnician.Visibility = Visibility.Collapsed;
                page.name.Visibility = Visibility.Collapsed;
                page.status.Visibility = Visibility.Collapsed;
                page.job_description.Visibility = Visibility.Collapsed;
                page.techTextBlock.Visibility = Visibility.Collapsed;
                page.serviceTextBlock.Visibility = Visibility.Collapsed;
                page.nameTextBlock.Visibility = Visibility.Collapsed;
                page.statusTextBlock.Visibility = Visibility.Collapsed;
                page.descTextBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                MaintenanceSchedulePage newPage = new MaintenanceSchedulePage();
                newPage.addButton.Visibility = Visibility.Collapsed;
                newPage.editButton.Visibility = Visibility.Collapsed;
                newPage.deleteButton.Visibility = Visibility.Collapsed;
                newPage.service_date.Visibility = Visibility.Collapsed;
                newPage.comboTechnician.Visibility = Visibility.Collapsed;
                newPage.name.Visibility = Visibility.Collapsed;
                newPage.status.Visibility = Visibility.Collapsed;
                newPage.job_description.Visibility = Visibility.Collapsed;
                newPage.techTextBlock.Visibility = Visibility.Collapsed;
                newPage.serviceTextBlock.Visibility = Visibility.Collapsed;
                newPage.nameTextBlock.Visibility = Visibility.Collapsed;
                newPage.statusTextBlock.Visibility = Visibility.Collapsed;
                newPage.descTextBlock.Visibility = Visibility.Collapsed;
                TechnicianFrame.Content = newPage;
            }
        }
    }
}
