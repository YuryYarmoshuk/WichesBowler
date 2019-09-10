using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WichesBowler
{
    public class BD
    {
        const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Мои документы\ОАиП\Visual\WichesBowler\WichesBowler\DB.mdf";
        
        /// <summary>
        /// Проверка логина и пороля
        /// </summary>
        /// <param name="log"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool AuthCheck(string log, string pass)
        {
            string sqlCommand = String.Format("SELECT * FROM Accounts WHERE Login = '{0}'", log);

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {

                sqlConnection.Open();

                SqlDataReader sqlReader = null;

                SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

                sqlReader = command.ExecuteReader();

                while (sqlReader.Read())
                {
                    if (Convert.ToString(sqlReader["Password"]).Equals(pass))
                    {
                        return true;
                    }
                }

                sqlReader.Close();

                sqlConnection.Close();
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
        /// <summary>
        /// Проверка доступа
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public string AccessCheck(string log)
        {
            string access = "null";

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            
            string sqlCommand = String.Format("SELECT * FROM Accounts WHERE Login = '{0}'", log);

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                access = Convert.ToString(sqlReader["Access"]);
            }
                
            sqlReader.Close();
              
           
            sqlConnection.Close();

            return access;
        }
        /// <summary>
        /// Проверка наличия названия в БД
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool ExistTitleCheck(string title)
        {
            string sqlCommand = String.Format("SELECT Title, COUNT(*) as Quant FROM Products WHERE Title = '{0}' GROUP BY Title", title);
            
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                if (Convert.ToString(sqlReader["Quant"]).Equals("1"))
                {
                    return true;
                }
            }

            sqlReader.Close();


            sqlConnection.Close();

            return false;
        }
        /// <summary>
        /// Проверка наличия id корзины в БД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ExistIdCheck(int id)
        {
            string sqlCommand = String.Format("SELECT idBucket, COUNT(*) as ID FROM Accounts WHERE idBucket = {0} GROUP BY idBucket", id);

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                if (Convert.ToString(sqlReader["ID"]).Equals("1"))
                {
                    return true;
                }
            }

            sqlReader.Close();


            sqlConnection.Close();

            return false;
        }
        /// <summary>
        /// Вывод всей БД. По доступу
        /// </summary>
        /// <param name="list"></param>
        /// <param name="quantity"></param>
        public void ViewAll(ListBox list, bool quantity)
        {
            list.Items.Clear();

            string sqlCommand = "SELECT * FROM Products";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string ans = "";

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                if (quantity) {
                    ans = "Id: " + Convert.ToString(sqlReader["Id"]) + "\t" + 
                        "Title: " + Convert.ToString(sqlReader["Title"]) + "\t" +
                        "Category: " + Convert.ToString(sqlReader["Category"]) + "\t" + 
                        "Cost: " + Convert.ToString(sqlReader["Cost"]) + "\t" +
                        "Discription: " + Convert.ToString(sqlReader["Discription"]) + "\t" + 
                        "Quantity: " + Convert.ToString(sqlReader["Quantity"]);
                    list.Items.Add(ans);
                }
                else
                {
                    if (Int32.Parse(Convert.ToString(sqlReader["Quantity"])) > 0)
                    {
                        ans = "Id: " + Convert.ToString(sqlReader["Id"]) + "\t" +
                        "Title: " + Convert.ToString(sqlReader["Title"]) + "\t" +
                        "Category: " + Convert.ToString(sqlReader["Category"]) + "\t" +
                        "Cost: " + Convert.ToString(sqlReader["Cost"]) + "\t" +
                        "Discription: " + Convert.ToString(sqlReader["Discription"]) + "\t";
                        list.Items.Add(ans);
                    }
                }
            }

            sqlReader.Close();


            sqlConnection.Close();
        }
        /// <summary>
        /// Выполняет заданный запрос
        /// </summary>
        /// <param name="req"></param>
        public void WorkWithBD(string req)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(req, sqlConnection);
            
            sqlConnection.Open();

            command.ExecuteNonQuery();

            sqlConnection.Close();
        }
        /// <summary>
        /// Возврожает массив из БД по конкретному Id
        /// </summary>
        /// <param name="ind"></param>
        /// <returns></returns>
        public string[] WorkWithBD(int ind)
        {
            string[] ans = new string[6];

            string sqlCommand = String.Format("SELECT * FROM Products WHERE Id = {0}", ind);

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                ans[0] = sqlReader["Title"].ToString();
                ans[1] = sqlReader["Category"].ToString();
                ans[2] = sqlReader["Cost"].ToString();
                ans[3] = sqlReader["Discription"].ToString();
                ans[4] = sqlReader["Quantity"].ToString();
                ans[5] = sqlReader["Id"].ToString();
            }

            sqlReader.Close();

            sqlConnection.Close();

            return ans;
        }
        /// <summary>
        /// Выводит информацию по аккаунтам
        /// </summary>
        /// <param name="list"></param>
        public void ViewAllAcc(ListBox list)
        {
            list.Items.Clear();

            string sqlCommand = "SELECT * FROM Accounts WHERE Access != 'Admin'";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string ans = "";

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                ans = "Id: " + Convert.ToString(sqlReader["Id"]) + "\t" +
                    "Login: " + Convert.ToString(sqlReader["Login"]) + "\t" +
                    "Password: " + Convert.ToString(sqlReader["Password"]);
                list.Items.Add(ans);
            }

            sqlReader.Close();


            sqlConnection.Close();
        }
        /// <summary>
        /// Проверка наличия аккаунта в БД
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public bool ExistAccCheck(string login)
        {
            string sqlCommand = String.Format("SELECT Login, COUNT(*) as Quant FROM Accounts WHERE Login = '{0}' GROUP BY Login", login);

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                if (Convert.ToString(sqlReader["Quant"]).Equals("1"))
                {
                    return true;
                }
            }

            sqlReader.Close();


            sqlConnection.Close();

            return false;
        }
        /// <summary>
        /// Получает Id корзины из БД
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public int IdCheck(string login)
        {
            int id = -1;

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string sqlCommand = String.Format("SELECT * FROM Accounts WHERE Login = '{0}'", login);

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                id = Int32.Parse(Convert.ToString(sqlReader["idBucket"]));
            }

            sqlReader.Close();


            sqlConnection.Close();

            return id;
        }
        /// <summary>
        /// Проверка наличия корзины в БД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool EmptyBucketCheck(int id)
        {
            bool ans = false;

            string sqlCommand = String.Format("SELECT idBucket,COUNT(*) AS myCount FROM Buckets WHERE idBucket = {0} GROUP BY idBucket", id);

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                if (Int32.Parse(Convert.ToString(sqlReader["myCount"])) > 0)
                {
                    ans = true;
                }
            }

            sqlReader.Close();


            sqlConnection.Close();
            return ans;
        }
        /// <summary>
        /// возвращает массив всех товаров из корзины
        /// </summary>
        public string[][] ViewAllBucket(int id)
        {
            string[] bucketProdId = AllBucket(id);
            string[][] prod = new string[bucketProdId.Length][];
            int index = 0;

            foreach (string prodId in bucketProdId)
            {
                prod[index] = WorkWithBD(Int32.Parse(prodId));
                index++;
            }

            return prod;
        }
        /// <summary>
        /// Получает из корзины все id продуктов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string[] AllBucket(int id)
        {
            string sqlCommandtoBucket = String.Format("SELECT * FROM Buckets WHERE idBucket = {0}", id);
            string sqlCommandtoCount = String.Format("SELECT COUNT(*) AS MyCount FROM Buckets WHERE idBucket = {0}", id);

            string[] idProd = new string[CountBD(sqlCommandtoCount)];
            int index = 0;

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommandtoBucket, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                idProd[index] = Convert.ToString(sqlReader["idProducts"]);
                index++;
            }

            sqlReader.Close();


            sqlConnection.Close();

            return idProd;
        }
        /// <summary>
        /// Считает количество записей в таблице, которая указана в запросе
        /// </summary>
        /// <param name="sqlCom"></param>
        /// <returns></returns>
        public int CountBD(string sqlCom)
        {
            int count = 0;

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            SqlDataReader dataReader = null;

            SqlCommand sqlCommand = new SqlCommand(sqlCom, sqlConnection);

            dataReader = sqlCommand.ExecuteReader();

            while (dataReader.Read())
            {
                count = Int32.Parse(dataReader["MyCount"].ToString());
            }

            return count;
        }
        /// <summary>
        /// Проверка наличия заказа
        /// </summary>
        /// <param name="idBucket"></param>
        /// <returns></returns>
        public bool OrderExist(int idBucket)
        {
            string sqlCommand = String.Format("SELECT COUNT(*) as Quant FROM Orders WHERE idBucket = {0}", idBucket);

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                if (Convert.ToString(sqlReader["Quant"]).Equals("1"))
                {
                    return true;
                }
            }

            sqlReader.Close();


            sqlConnection.Close();

            return false;
        }
        /// <summary>
        /// Возвращает Id заказа
        /// </summary>
        /// <param name="idBucket"></param>
        /// <returns></returns>
        public int OrderId(int idBucket)
        {
            string sqlCommand = String.Format("SELECT * FROM Orders WHERE idBucket = {0}", idBucket);

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            int id = -1;

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                id = Int32.Parse(Convert.ToString(sqlReader["Id"]));
            }

            sqlReader.Close();


            sqlConnection.Close();

            return id;
        }
        /// <summary>
        /// Возвращает id корзины пользователся
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public int GetBucketId(string login)
        {
            int id = -1;

            string sqlCommand = String.Format("SELECT * FROM Accounts WHERE login = '{0}'", login);

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                id = Int32.Parse(Convert.ToString(sqlReader["idBucket"]));
            }

            sqlReader.Close();


            sqlConnection.Close();

            return id;
        }
        /// <summary>
        /// Выводит все записи корзин
        /// </summary>
        /// <param name="list"></param>
        public void ViewBucket(ListBox list)
        {
            list.Items.Clear();

            string sqlCommand = "SELECT * FROM Buckets";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string ans = "";

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                ans = "ID: " + Convert.ToString(sqlReader["Id"]) + "\t" +
                    "Bucket ID: " + Convert.ToString(sqlReader["idBucket"]) + "\t" +
                    "Product ID: " + Convert.ToString(sqlReader["idProducts"]);
                list.Items.Add(ans);
            }

            sqlReader.Close();

            sqlConnection.Close();
        }
        /// <summary>
        /// Выводит все записи определенной корзины
        /// </summary>
        /// <param name="list"></param>
        public void ViewBucket(ListBox list, int id)
        {
            list.Items.Clear();

            string sqlCommand = String.Format("SELECT * FROM Buckets WHERE idBucket = {0}", id);

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string ans = "";

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                ans = "ID: " + Convert.ToString(sqlReader["Id"]) + "\t" +
                    "Bucket ID: " + Convert.ToString(sqlReader["idBucket"]) + "\t" +
                    "Product ID: " + Convert.ToString(sqlReader["idProducts"]);
                list.Items.Add(ans);
            }

            sqlReader.Close();


            sqlConnection.Close();
        }
        /// <summary>
        /// Возвращает id продукта 1 записи в корзине
        /// </summary>
        /// <param name="idBucket"></param>
        /// <returns></returns>
        public int ProdIdFromBucket(int idBucket)
        {
            int id = -1;
            
            string sqlCommand = String.Format("SELECT TOP (1) * FROM Buckets WHERE idBucket = {0}", idBucket);

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            
            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                id = Int32.Parse(sqlReader["idProducts"].ToString());
            }

            sqlReader.Close();


            sqlConnection.Close();

            return id;
        }
        /// <summary>
        /// Возвращает количествол продукции в БД
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int QuantityProduct(int id)
        {
            int quant = 0;

            string sqlCommand = String.Format("SELECT * FROM Products WHERE Id = {0}", id);

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                quant = Int32.Parse(sqlReader["Quantity"].ToString());
            }

            sqlReader.Close();


            sqlConnection.Close();
            
            return quant;
        }
        /// <summary>
        /// Записывает краткую информацию о заказе
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id"></param>
        public void OrderView(ListBox list)
        {
            list.Items.Clear();

            string sqlCommand = "SELECT * FROM Orders";

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string ans = "";

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                ans = "Id: " + Convert.ToString(sqlReader["Id"]) + " " +
                    "Full name: " + Convert.ToString(sqlReader["sName"]) + " " +
                    Convert.ToString(sqlReader["fName"]) + " " +
                    Convert.ToString(sqlReader["mName"]) + " " +
                    "Number: " + Convert.ToString(sqlReader["number"]) + " " +
                    "Status: " + Convert.ToString(sqlReader["status"]);
                list.Items.Add(ans);
            }

            sqlReader.Close();

            sqlConnection.Close();
        }
        /// <summary>
        /// Возвращает массив всех данных о заказе
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string[] OrderView(int id)
        {
            string[] mas = new string[10];

            string sqlCommand = String.Format("SELECT * FROM Orders WHERE Id = {0}", id);

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            
            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                mas[0] = Convert.ToString(sqlReader["Id"]);
                mas[1] = Convert.ToString(sqlReader["fName"]);
                mas[2] = Convert.ToString(sqlReader["mName"]);
                mas[3] = Convert.ToString(sqlReader["sName"]);
                mas[4] = Convert.ToString(sqlReader["address"]);
                mas[5] = Convert.ToString(sqlReader["email"]);
                mas[6] = Convert.ToString(sqlReader["date"]);
                mas[7] = Convert.ToString(sqlReader["number"]);
                mas[8] = Convert.ToString(sqlReader["status"]);
                mas[9] = Convert.ToString(sqlReader["idBucket"]);

            }

            sqlReader.Close();

            sqlConnection.Close();

            return mas;
        }
    }
}
