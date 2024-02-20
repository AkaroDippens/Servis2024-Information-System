using System;
using System.Collections.Generic;
using System.Data;
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
using Servis_2024.SERVER2024DataSetTableAdapters;
using System.Data.SqlClient;

namespace Servis_2024.Pages
{
    /// <summary>
    /// Логика взаимодействия для EquipmentPage.xaml
    /// </summary>
    public partial class EquipmentPage : Page
    {
        EquipmentTableAdapter equip = new EquipmentTableAdapter();
        public EquipmentPage()
        {
            InitializeComponent();
            DataGrid.ItemsSource = equip.GetData();

            comboClient.ItemsSource = ClientPage.client.GetData();
            comboClient.DisplayMemberPath = "Почта";
            comboClient.SelectedValuePath = "Id";
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
                    string query = $"update Equipment set name = '{name.Text}', category = '{category.Text}', serial_number = '{serialNumber.Text}', " +
                        $"problem_description = '{problemDecription.Text}', client_id = {Convert.ToInt32((comboClient.SelectedItem as DataRowView).Row[0])} " +
                        $"WHERE equipment_id = {id}";


                    DataBase dataBase = new DataBase();
                    SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

                    dataBase.openConnection();

                    sqlCommand.ExecuteNonQuery();
                    DataGrid.ItemsSource = equip.GetData();
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
            csv.ExportDataGridToCSV(DataGrid, "C:\\Users\\mirzo\\source\\repos\\Servis-2024\\Servis-2024\\CSV\\Equipments.csv");
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
                name.Text = (DataGrid.SelectedItem as DataRowView).Row[1].ToString();
                category.Text = (DataGrid.SelectedItem as DataRowView).Row[2].ToString();
                serialNumber.Text = (DataGrid.SelectedItem as DataRowView).Row[3].ToString();
                problemDecription.Text = (DataGrid.SelectedItem as DataRowView).Row[4].ToString();
                comboClient.SelectedValue = (DataGrid.SelectedItem as DataRowView).Row[5].ToString();
            }
        }

        /// <summary>
        /// Метод для поиска элемента по названию и серийному номеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchNameChanged(object sender, TextChangedEventArgs e)
        {
            Search(DataGrid, "name", "serial_number", searchTextByName);
        }


        /// <summary>
        /// Метод реализации поиска
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="target"></param>
        /// <param name="target2"></param>
        /// <param name="search"></param>
        private void Search(DataGrid dataGrid, string target, string target2, TextBox search)
        {
            DataBase dataBase = new DataBase();
            dataGrid.ItemsSource = null;

            string querySearch = $"SELECT equipment_id AS 'Id', name AS 'Имя', category AS 'Категория', serial_number AS 'Серийный номер', problem_description AS 'Описание проблемы', client_id AS 'Клиент' FROM Equipment WHERE {target} LIKE '%{search.Text}%' AND {target2} LIKE '%{searchTextBySerial.Text}%'";

            SqlCommand command = new SqlCommand(querySearch, dataBase.getConnection());

            dataBase.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        /// <summary>
        /// Метод, который проверяет на пустую строку в текстовых полях
        /// </summary>
        private void isEmpty()
        {
            if (string.IsNullOrEmpty(name.Text) ||
                        string.IsNullOrEmpty(category.Text) ||
                        string.IsNullOrEmpty(serialNumber.Text) ||
                        (comboClient.SelectedItem == null) ||
                        string.IsNullOrEmpty(problemDecription.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }
        }
    }
}
