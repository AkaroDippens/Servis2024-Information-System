using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Xml.Linq;
using Microsoft.Win32;
using Servis_2024.SERVER2024DataSetTableAdapters;

namespace Servis_2024.Pages
{
    public partial class AccountPage : Page
    {
        /// <summary>
        /// 
        /// </summary>
        public static AccountTableAdapter account = new AccountTableAdapter();
        public AccountPage()
        {
            InitializeComponent();
            DataGrid.ItemsSource = account.GetData();
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
                HashPassword hashPassword = new HashPassword();

                isEmpty();
                string passwordHash = hashPassword.GenerateSha256Hash(password.Text, 16);

                string query = $"INSERT INTO Account (surname, name, patronymic, login, password, role) VALUES (" +
                    $"'{surname.Text}', '{name.Text}', '{patronymic.Text}', '{login.Text}', '{passwordHash}', '{role.Text}')";

                SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

                dataBase.openConnection();
                sqlCommand.ExecuteNonQuery();
                
                
                string getAccountIdQuery = $"SELECT account_id FROM Account WHERE login = '{login.Text}'";
                SqlCommand getAccountIdCommand = new SqlCommand(getAccountIdQuery, dataBase.getConnection());
                int accountId = Convert.ToInt32(getAccountIdCommand.ExecuteScalar());
                if (role.Text.Equals("Техник", StringComparison.OrdinalIgnoreCase))
                {
                    string addTechnicianQuery = $"INSERT INTO Technician (account_id, surname, name, patronymic) VALUES ({accountId}, '{surname.Text}', " +
                        $"'{name.Text}', '{patronymic.Text}')";
                    SqlCommand addTechnicianCommand = new SqlCommand(addTechnicianQuery, dataBase.getConnection());
                    addTechnicianCommand.ExecuteNonQuery();
                }

                dataBase.closeConnection();
                DataGrid.ItemsSource = account.GetData();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка");
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
                surname.Text = (DataGrid.SelectedItem as DataRowView).Row[1].ToString();
                name.Text = (DataGrid.SelectedItem as DataRowView).Row[2].ToString();
                patronymic.Text = (DataGrid.SelectedItem as DataRowView).Row[3].ToString();
                login.Text = (DataGrid.SelectedItem as DataRowView).Row[4].ToString();
                password.Text = (DataGrid.SelectedItem as DataRowView).Row[5].ToString();
                role.Text = (DataGrid.SelectedItem as DataRowView).Row[6].ToString();
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
                string query = $"delete from Account where account_id = {id}";

                DataBase dataBase = new DataBase();
                SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

                dataBase.openConnection();

                sqlCommand.ExecuteNonQuery();
                DataGrid.ItemsSource = account.GetData();
            }
            catch
            {
                MessageBox.Show("Удаление не удалось");
            }
        }

        /// <summary>
        /// Метод изменения данных в DataGrid и БД, ведя измененные данные в TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update(object sender, RoutedEventArgs e)
        {
            try
            {

                object id = (DataGrid.SelectedItem as DataRowView).Row[0];

                HashPassword hashPassword = new HashPassword();
                string passwordHash = hashPassword.GenerateSha256Hash(password.Text, 16);

                if (id != null)
                {
                    isEmpty();
                    if (password.Text != (DataGrid.SelectedItem as DataRowView).Row[5].ToString())
                    {
                        string query = $"update Account set surname = '{surname.Text}', name = '{name.Text}', " +
                            $"patronymic = '{patronymic.Text}', login = '{login.Text}', password = '{passwordHash}', role = '{role.Text}' " +
                            $"WHERE account_id = {id}";


                        DataBase dataBase = new DataBase();
                        SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

                        dataBase.openConnection();

                        sqlCommand.ExecuteNonQuery();
                        DataGrid.ItemsSource = account.GetData();
                    }
                    else
                    {
                        string query = $"update Account set surname = '{surname.Text}', name = '{name.Text}', " +
                           $"patronymic = '{patronymic.Text}', login = '{login.Text}', role = '{role.Text}' " +
                           $"WHERE account_id = {id}";

                        DataBase dataBase = new DataBase();
                        SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

                        dataBase.openConnection();

                        sqlCommand.ExecuteNonQuery();
                        DataGrid.ItemsSource = account.GetData();
                    }
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
            csv.ExportDataGridToCSV(DataGrid, "C:\\Users\\mirzo\\source\\repos\\Servis-2024\\Servis-2024\\CSV\\Accounts.csv");
            MessageBox.Show("Ваши данные успешно экспортированы в CSV!");
        }

        /// <summary>
        /// Метод, который проверяет на пустую строку в текстовых полях
        /// </summary>
        private void isEmpty()
        {
            if (string.IsNullOrEmpty(surname.Text) ||
                        string.IsNullOrEmpty(name.Text) ||
                        string.IsNullOrEmpty(patronymic.Text) ||
                        string.IsNullOrEmpty(login.Text) ||
                        string.IsNullOrEmpty(password.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }
        }
    }
}
