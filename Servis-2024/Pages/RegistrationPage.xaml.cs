using Servis_2024.Windows;
using System;
using System.Collections.Generic;
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

namespace Servis_2024.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public Frame frame;
     
        public RegistrationPage(Frame frameRegAuth)
        {
            InitializeComponent();
            frame = frameRegAuth;
        }


        /// <summary>
        /// Метод для регистрации с валидацией
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegClick(object sender, RoutedEventArgs e)
        {
            DataBase dataBase = new DataBase();

            HashPassword hashPassword = new HashPassword();

            string passwordHash = hashPassword.GenerateSha256Hash(password.Password, 16);
            if (string.IsNullOrEmpty(surname.Text) ||
                        string.IsNullOrEmpty(name.Text) ||
                        string.IsNullOrEmpty(patronymic.Text) ||
                        string.IsNullOrEmpty(login.Text) ||
                        string.IsNullOrEmpty(password.Password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля");
                return;
            }
            if (role.Text == "Оператор" || role.Text == "Техник")
            {
                string query = $"INSERT INTO Account (surname, name, patronymic, login, password, role) VALUES (" +
                $"'{surname.Text}', '{name.Text}', '{patronymic.Text}', '{login.Text}', '{passwordHash}', '{role.Text}')";

                SqlCommand sqlCommand = new SqlCommand(query, dataBase.getConnection());

                dataBase.openConnection();

                if (sqlCommand.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт успешно создан");
                }
                else
                {
                    MessageBox.Show("Аккаунт не создан!");
                }

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
            }     
        }

        /// <summary>
        /// Метод перехода на страницу авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthClick(object sender, RoutedEventArgs e)
        {
            frame.Content = new AuthorizationPage(frame);
        }
    }
}
