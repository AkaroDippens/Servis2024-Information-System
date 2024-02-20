using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Principal;
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
using Servis_2024.SERVER2024DataSetTableAdapters;
using System.Xml.Linq;

namespace Servis_2024.Pages
{
    public partial class ApplicationPage : Page
    {
        ApplicationTableAdapter application = new ApplicationTableAdapter();
        TechnicianTableAdapter technician = new TechnicianTableAdapter();
        public ApplicationPage()
        {
            InitializeComponent();
            DataGrid.ItemsSource = application.GetData();

            comboTechnician.ItemsSource = technician.GetData();
            comboTechnician.DisplayMemberPath = "Фамилия";
            comboTechnician.SelectedValuePath = "Id";
    
            comboClient.ItemsSource = ClientPage.client.GetData();
            comboClient.DisplayMemberPath = "Почта";
            comboClient.SelectedValuePath = "Id";
        }

        /// <summary>
        /// Метод добавления данных с валидацией
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add(object sender, RoutedEventArgs e)
        {
            try
            {
                DataBase dataBase = new DataBase();

                isEmpty();
                string query = $"INSERT INTO Application (create_date_and_time, status, technician_id, client_id, execution_date_and_time, information)" +
                    $"VALUES(GETDATE(), '{status.Text}', {Convert.ToInt32((comboTechnician.SelectedItem as DataRowView).Row[0])}, " +
                    $"{Convert.ToInt32((comboClient.SelectedItem as DataRowView).Row[0])}, GETDATE(), '{comment.Text}')";

                SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());
                dataBase.openConnection();

                sqlCommand.ExecuteNonQuery();
                DataGrid.ItemsSource = application.GetData();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка");
            }
        }

        /// <summary>
        /// Метод удаления данных с DataGrid и БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (DataGrid.SelectedItem as DataRowView).Row[0];
                string query = $"delete from Application where application_id = {id}";

                DataBase dataBase = new DataBase();
                SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

                dataBase.openConnection();

                sqlCommand.ExecuteNonQuery();
                DataGrid.ItemsSource = application.GetData();
            }
            catch
            {
                MessageBox.Show("Удаление не удалось");
            }
        }

        /// <summary>
        /// Метод вывода в TextBox данные при нажатии на определенную ячейку в DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                create_date.Text = (DataGrid.SelectedItem as DataRowView).Row[1].ToString();
                status.Text = (DataGrid.SelectedItem as DataRowView).Row[2].ToString();
                comboTechnician.SelectedValue = (DataGrid.SelectedItem as DataRowView).Row[3];
                comboClient.SelectedValue = (DataGrid.SelectedItem as DataRowView).Row[4].ToString();
                execute_date.Text = (DataGrid.SelectedItem as DataRowView).Row[5].ToString().ToString();
                comment.Text = (DataGrid.SelectedItem as DataRowView).Row[6].ToString();
            }
        }

        /// <summary>
        /// Метод изменения данных в DataGrid и БД, ведя измененные данные в TextBox и ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update(object sender, RoutedEventArgs e)
        {
            try
            {
                object id = (DataGrid.SelectedItem as DataRowView).Row[0];

                if (id != null)
                {
                    isEmpty();

                    string query = $"update Application set status = '{status.Text}', technician_id = {Convert.ToInt32((comboTechnician.SelectedItem as DataRowView).Row[0])}, " +
                        $"client_id = {Convert.ToInt32((comboClient.SelectedItem as DataRowView).Row[0])}, execution_date_and_time = GETDATE()," +
                        $" information = '{comment.Text}' WHERE application_id = {id}";


                    DataBase dataBase = new DataBase();
                    SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

                    dataBase.openConnection();

                    sqlCommand.ExecuteNonQuery();
                    DataGrid.ItemsSource = application.GetData();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка");
            }
        }

        /// <summary>
        /// Метод экспорта данных в CSV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportToCSV(object sender, RoutedEventArgs e)
        {
            ExportToCSV csv = new ExportToCSV();
            csv.ExportDataGridToCSV(DataGrid, "C:\\Users\\mirzo\\source\\repos\\Servis-2024\\Servis-2024\\CSV\\Applications.csv");
            MessageBox.Show("Ваши данные успешно экспортированы в CSV!");
        }

        /// <summary>
        /// Метод, который проверяет на пустую строку в текстовых полях
        /// </summary>
        private void isEmpty()
        {
            if (
                string.IsNullOrEmpty(status.Text) ||
                (comboTechnician.SelectedItem == null) ||
                (comboClient.SelectedItem == null) ||
                string.IsNullOrEmpty(comment.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }
        }
    }
}
