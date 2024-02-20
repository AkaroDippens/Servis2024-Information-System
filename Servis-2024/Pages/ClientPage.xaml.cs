using Servis_2024.SERVER2024DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Servis_2024.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public static ClientTableAdapter client = new ClientTableAdapter();

        public ClientPage()
        {
            InitializeComponent();
            DataGrid.ItemsSource = client.GetData();

            comboAccount.ItemsSource = AccountPage.account.GetData();
            comboAccount.DisplayMemberPath = "Логин";
            comboAccount.SelectedValuePath = "Id";
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
                isEmpty();

                DataBase dataBase = new DataBase();

                string query = $"INSERT INTO Client (surname, name, patronymic, phone_number, email, account_id) " +
                    $"VALUES('{surname.Text}', '{name.Text}', '{patronymic.Text}', '{phoneNumber.Text}', '{email.Text}'," +
                    $"{Convert.ToInt32((comboAccount.SelectedItem as DataRowView).Row[0])});";

                SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());
                dataBase.openConnection();

                sqlCommand.ExecuteNonQuery();
                DataGrid.ItemsSource = client.GetData();
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

                    string query = $"update Client set surname = '{surname.Text}', name = '{name.Text}', patronymic = '{patronymic.Text}', phone_number = '{phoneNumber.Text}', email = '{email.Text}', account_id = {Convert.ToInt32((comboAccount.SelectedItem as DataRowView).Row[0])} " +
                        $"WHERE client_id = {id}";


                    DataBase dataBase = new DataBase();
                    SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

                    dataBase.openConnection();

                    sqlCommand.ExecuteNonQuery();
                    DataGrid.ItemsSource = client.GetData();
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
                string query = $"delete from Client where client_id = {id}";

                DataBase dataBase = new DataBase();
                SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

                dataBase.openConnection();

                sqlCommand.ExecuteNonQuery();
                DataGrid.ItemsSource = client.GetData();
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
            csv.ExportDataGridToCSV(DataGrid, "C:\\Users\\mirzo\\source\\repos\\Servis-2024\\Servis-2024\\CSV\\Clients.csv");
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
                surname.Text = (DataGrid.SelectedItem as DataRowView).Row[1].ToString();
                name.Text = (DataGrid.SelectedItem as DataRowView).Row[2].ToString();
                patronymic.Text = (DataGrid.SelectedItem as DataRowView).Row[3].ToString();
                phoneNumber.Text = (DataGrid.SelectedItem as DataRowView).Row[4].ToString();
                email.Text = (DataGrid.SelectedItem as DataRowView).Row[5].ToString();
                comboAccount.SelectedValue = (DataGrid.SelectedItem as DataRowView).Row[6].ToString();
            }
        }

        /// <summary>
        /// Метод, который проверяет на пустую строку в текстовых полях
        /// </summary>
        private void isEmpty()
        {
            if (string.IsNullOrEmpty(surname.Text) ||
                        string.IsNullOrEmpty(name.Text) ||
                        string.IsNullOrEmpty(patronymic.Text) ||
                        string.IsNullOrEmpty(phoneNumber.Text) ||
                        string.IsNullOrEmpty(email.Text) ||
                        comboAccount.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
        }
    }
}
