using Servis_2024.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        private Frame mainFrame;
        public AuthorizationPage(Frame frame)
        {
            InitializeComponent();
            mainFrame = frame;
        }
        
        /// <summary>
        /// Метод реализации авторизации путём поиска введенного логина и пароля в БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthClick(object sender, RoutedEventArgs e)
        {
            DataBase dataBase = new DataBase();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            HashPassword hashPassword = new HashPassword();

            if (string.IsNullOrEmpty(login.Text) ||
                string.IsNullOrEmpty(password.Password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            string passwordHash = hashPassword.GenerateSha256Hash(password.Password, 16);

            string query = $"SELECT account_id, login, password, role from Account WHERE login = '{login.Text}' and password = '{passwordHash}'";

            SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

            adapter.SelectCommand = sqlCommand;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 1)
            {
                string role = dataTable.Rows[0]["role"].ToString();

                if (role == "Администратор")
                {
                    AdminWindow admin = new AdminWindow(); 
                    admin.Show();
                }
                else if (role == "Техник")
                {
                    TechnicianWindow technician = new TechnicianWindow();
                    technician.Show();
                }
                else if (role == "Оператор")
                {
                    OperatorWindow oper = new OperatorWindow();
                    oper.Show();
                }
            }
            else
            {
                MessageBox.Show("Произошла ошибка");
            }
        }

        /// <summary>
        /// Метод перехода на страницу с регистрацией
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegClick(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new RegistrationPage(mainFrame);
        }
    }
}
