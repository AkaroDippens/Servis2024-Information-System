using Servis_2024.SERVER2024DataSetTableAdapters;
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
using System.Xml.Linq;

namespace Servis_2024.Pages
{
    public partial class SparPartPage : Page
    {
        SparPartsTableAdapter spars = new SparPartsTableAdapter();
        public SparPartPage()
        {
            InitializeComponent();
            DataGrid.ItemsSource = spars.GetData();
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

                string query = $"INSERT INTO SparParts (spar_parts_name, quantity) VALUES (" +
                    $"'{name.Text}', {quantity.Text})";

                SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());
                dataBase.openConnection();

                sqlCommand.ExecuteNonQuery();
                DataGrid.ItemsSource = spars.GetData();
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
                string query = $"delete from SparParts where spar_parts_id = {id}";

                DataBase dataBase = new DataBase();
                SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

                dataBase.openConnection();

                sqlCommand.ExecuteNonQuery();
                DataGrid.ItemsSource = spars.GetData();
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
            csv.ExportDataGridToCSV(DataGrid, "C:\\Users\\mirzo\\source\\repos\\Servis-2024\\Servis-2024\\CSV\\SparParts.csv");
            MessageBox.Show("Ваши данные успешно экспортированы в CSV!");
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

                if (id != null)
                {
                    isEmpty();
                    string query = $"update SparParts set spar_parts_name = '{name.Text}', quantity = {quantity.Text} WHERE spar_parts_id = {id}";


                    DataBase dataBase = new DataBase();
                    SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

                    dataBase.openConnection();

                    sqlCommand.ExecuteNonQuery();
                    DataGrid.ItemsSource = spars.GetData();
                }
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
                name.Text = (DataGrid.SelectedItem as DataRowView).Row[1].ToString();
                quantity.Text = (DataGrid.SelectedItem as DataRowView).Row[2].ToString();
            }
        }

        /// <summary>
        /// Метод, который проверяет на пустую строку в текстовых полях
        /// </summary>
        private void isEmpty()
        {
            if (string.IsNullOrEmpty(name.Text) ||
                        string.IsNullOrEmpty(quantity.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }
        }
    }
}
