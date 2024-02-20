using Servis_2024.SERVER2024DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace Servis_2024.Pages
{
    /// <summary>
    /// Логика взаимодействия для MaintenanceSchedulePage.xaml
    /// </summary>
    public partial class MaintenanceSchedulePage : Page
    {
        Maintenance_scheduleTableAdapter schedule = new Maintenance_scheduleTableAdapter();
        TechnicianTableAdapter technician = new TechnicianTableAdapter();
        public MaintenanceSchedulePage()
        {
            InitializeComponent();
            DataGrid.ItemsSource = schedule.GetData();

            comboTechnician.ItemsSource = technician.GetData();
            comboTechnician.DisplayMemberPath = "Фамилия";
            comboTechnician.SelectedValuePath = "Id";
        }

        /// <summary>
        /// Метод для поиска элемента по названию и серийному номеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchChanged(object sender, TextChangedEventArgs e)
        {
            Search(DataGrid);
        }

        /// <summary>
        /// Метод реализации поиска
        /// </summary>
        /// <param name="dataGrid"></param>
        private void Search(DataGrid dataGrid)
        {
            DataBase dataBase = new DataBase();
            dataGrid.ItemsSource = null;

            string querySearch = $"SELECT maintenance_shedule_id AS 'Id', service_date AS 'Дата обслуживания', equipment_name AS 'Название оборудования', job_decription AS 'Описание работ', technician_id AS 'Техник', status AS 'Статус' FROM Maintenance_schedule WHERE status LIKE '%{searchText.Text}%'";

            SqlCommand command = new SqlCommand(querySearch, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            dataGrid.ItemsSource = dataTable.DefaultView;
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
                string query = (service_date.Text != null && service_date.Text != "") ? $"INSERT INTO Maintenance_schedule (service_date, equipment_name, job_decription, technician_id, status) " +
                    $"VALUES('{service_date.Text}', '{name.Text}', '{job_description.Text}', {Convert.ToInt32((comboTechnician.SelectedItem as DataRowView).Row[0])}, '{status.Text}'); "
                    : $"INSERT INTO Maintenance_schedule (service_date, equipment_name, job_decription, technician_id, status) " +
                    $"VALUES(GETDATE(), '{name.Text}', '{job_description.Text}', {Convert.ToInt32((comboTechnician.SelectedItem as DataRowView).Row[0])}, '{status.Text}'); ";

                SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());
                dataBase.openConnection();

                sqlCommand.ExecuteNonQuery();
                DataGrid.ItemsSource = schedule.GetData();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка");
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
                    string query = $"update Maintenance_schedule set equipment_name = '{name.Text}', job_description = {job_description.Text}, technician_id = {Convert.ToInt32((comboTechnician.SelectedItem as DataRowView).Row[0])}, " +
                        $"status = {status.Text} WHERE maintenance_shedule_id = {id}";


                    DataBase dataBase = new DataBase();
                    SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

                    dataBase.openConnection();

                    sqlCommand.ExecuteNonQuery();
                    DataGrid.ItemsSource = schedule.GetData();
                }
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
                string query = $"delete from Maintenance_schedule where maintenance_shedule_id = {id}";

                DataBase dataBase = new DataBase();
                SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

                dataBase.openConnection();

                sqlCommand.ExecuteNonQuery();
                DataGrid.ItemsSource = schedule.GetData();
            }
            catch
            {
                MessageBox.Show("Удаление не удалось");
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
            csv.ExportDataGridToCSV(DataGrid, "C:\\Users\\mirzo\\source\\repos\\Servis-2024\\Servis-2024\\CSV\\MaintenanceSchedule.csv");
            MessageBox.Show("Ваши данные успешно экспортированы в CSV!");
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
                service_date.Text = (DataGrid.SelectedItem as DataRowView).Row[1].ToString();
                name.Text = (DataGrid.SelectedItem as DataRowView).Row[2].ToString();
                job_description.Text = (DataGrid.SelectedItem as DataRowView).Row[3].ToString();
                comboTechnician.SelectedValue = (DataGrid.SelectedItem as DataRowView).Row[4].ToString();
                status.Text = (DataGrid.SelectedItem as DataRowView).Row[5].ToString();
            }
        }


        /// <summary>
        /// Метод, который проверяет на пустую строку в текстовых полях
        /// </summary>
        private void isEmpty()
        {
            if (string.IsNullOrEmpty(name.Text) ||
                (comboTechnician.SelectedItem == null) ||
                string.IsNullOrEmpty(job_description.Text) ||
                string.IsNullOrEmpty(status.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }
        }
    }
}
