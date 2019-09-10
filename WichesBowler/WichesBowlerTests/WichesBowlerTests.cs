using System;
using System.Windows.Forms;
using NUnit.Framework;
using WichesBowler;
using System.Data.SqlClient;

namespace WichesBowlerTests
{
    [TestFixture]
    public class WichesBowlerTests
    {
        const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Мои документы\ОАиП\Visual\WichesBowler\WichesBowler\DB.mdf";

        public ErrorList GetErrorList()
        {
            return new ErrorList();
        }
        public BD GetBD()
        {
            return new BD();
        }
        public SeccessList GetSeccessList()
        {
            return new SeccessList();
        }
        public SubString GetSubString()
        {
            return new SubString();
        }
        public CorrectInputCheck GetInputCheck()
        {
            return new CorrectInputCheck();
        }
        public ListBox GetListBox()
        {
            return new ListBox();
        }
        public int RecordCounted(string sqlReq)
        {
            int count = 0;

            string sqlCommand = sqlReq;

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);

            sqlReader = command.ExecuteReader();

            while (sqlReader.Read())
            {
                count++;
            }

            sqlReader.Close();


            sqlConnection.Close();

            return count;
        }
        public int ListItemCount(ListBox list)
        {
            return list.Items.Count;
        }
        public int ProdIdRecord(string title)
        {
            int id = 0;

            string sqlCommand = String.Format("SELECT * FROM Products WHERE Title = '{0}'", title);

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            sqlConnection.Open();

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

        [TestCase(1, "Login and Password fields must be filled!")]
        [TestCase(2, "Login must contain from 3 to 20 characters!")]
        [TestCase(3, "Password must contain from 3 to 20 characters!")]
        [TestCase(4, "Username can only contain letters, numbers and underscores")]
        [TestCase(5, "Login or password entered incorrectly! Try again!")]
        [TestCase(6, "All fields must be filled!")]
        [TestCase(7, "The cost must be integer!")]
        [TestCase(8, "The cost must be positive!")]
        [TestCase(9, "A product with that name already exists!")]
        [TestCase(10, "Select any product!")]
        [TestCase(11, "Account with that name already exists!")]
        [TestCase(12, "Bucket is empty!")]
        [TestCase(13, "Login or Password is wrong!")]
        [TestCase(14, "All fields must be more than three characters!")]
        [TestCase(15, "For email you need to choose a domain!")]
        [TestCase(16, "The number can only contain numbers (0 - 9)!")]
        [TestCase(17, "Uncorrect email name!")]
        [TestCase(-1, "Unknown error!")]
        public void ErrorStr_errorId_GoodResult(int id, string result)
        {
            string expected = result;

            ErrorList errorList = GetErrorList();
            string actual = errorList.ErrorStr(id);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(1, "Product successfully added")]
        [TestCase(2, "Account successfully added")]
        [TestCase(3, "Product is added to bucket")]
        [TestCase(4, "Order successfully added")]
        [TestCase(5, "Order successfully edit")]
        [TestCase(6, "Order successfully deleted")]
        [TestCase(-1, "Unknown error!")]
        public void SeccessStr_seccessId_GoodResult(int id, string result)
        {
            string expected = result;

            SeccessList seccessList = GetSeccessList();
            string actual = seccessList.SeccessStr(id);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IndReturn_CorrectStr_GoodID()
        {
            string str = "Id: 6 Title: RPG";
            string secStr = "Title";
            int expected = 6;

            SubString subString = GetSubString();
            int actual = subString.IndReturn(str, secStr);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TitleReturn_CorrectStr_CorrectTitle()
        {
            string str = "Id: 6 Title: RPG Category: Card RPG";
            string firstStr = "Title";
            string secStr = "Category";
            string expected = "RPG";

            SubString subString = GetSubString();
            string actual = subString.TitleReturn(str, firstStr, secStr);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IdList_CorrectTitle_GoodID()
        {
            string title = "RPG";
            int expected = 0;

            ListBox list = GetListBox();
            list.Items.Add("Id: 3 Title: RPG");
            SubString subString = GetSubString();
            int actual = subString.IdList(list, title);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CheckLogPas_ValidLogAndPass_TrueReturned()
        {
            string login = "admin";
            string password = "admin";

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.CheckLogPas(login, password);

            Assert.IsTrue(actual);
        }

        [TestCase ("", "admin")]
        [TestCase("admin", "")]
        [TestCase("ad", "admin")]
        [TestCase("admin", "ad")]
        [TestCase("adminadminadminadminadminadminadminadminadminadminadmin", "admin")]
        [TestCase("admin", "adminadminadminadminadminadminadminadminadminadminadmin")]
        public void CheckLogPas_NotValidLogAndPass_FalseReturned(string log, string pass)
        {
            string login = log;
            string password = pass;

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.CheckLogPas(login, password);

            Assert.IsFalse(actual);
        }

        [Test]
        public void CheckAddEdit_ValidData_TrueReturned()
        {
            string title = "RPG";
            int category = 2;
            string cost = "150";
            string discription = "Just Test";
            string quantity = "10";

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.CheckAddEdit(title, category, cost, discription, quantity);

            Assert.IsTrue(actual);
        }

        [TestCase ("", 2, "10", "Just Test", "1")]
        [TestCase("RPG", -1, "10", "Just Test", "1")]
        [TestCase("RPG", 2, "", "Just Test", "1")]
        [TestCase("RPG", 2, "10", "", "1")]
        [TestCase("RPG", 2, "10", "Just Test", "")]
        [TestCase("RPG", 2, "10rt", "Just Test", "1")]
        [TestCase("RPG", 2, "-10", "Just Test", "1")]
        [TestCase("LightRPG", 2, "10", "Just Test", "1")]
        public void CheckAddEdit_NotValidData_FalseReturned(string t, int cat, string c, string d, string q)
        {
            string title = t;
            int category = cat;
            string cost = c;
            string discription = d;
            string quantity = q;

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.CheckAddEdit(title, category, cost, discription, quantity);

            Assert.IsFalse(actual);
        }

        [Test]
        public void CorrectInputLogin_ValidLogin_TrueReturned()
        {
            string login = "admin";

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.CorrectInputLogin(login);

            Assert.IsTrue(actual);
        }

        [TestCase ("")]
        [TestCase ("cool admin")]
        public void CorrectInputLogin_EmptyLogin_TrueReturned(string log)
        {
            string login = log;

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.CorrectInputLogin(login);

            Assert.IsFalse(actual);
        }

        [Test]
        public void BucketIsEmpty_ExeptedBucketId_TrueReturned()
        {
            int id = 951;

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.BucketIsEmpty(id);

            Assert.IsTrue(actual);
        }

        [Test]
        public void BucketIsEmpty_NotExeptedBucketId_FalseReturned()
        {
            int id = -999;

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.BucketIsEmpty(id);

            Assert.IsFalse(actual);
        }

        [Test]
        public void LoginExist_ExeptedLogin_FalseReturned()
        {
            string log = "admin";

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.LoginExist(log);

            Assert.IsFalse(actual);
        }

        [Test]
        public void LoginExist_NotExeptedLogin_TrueReturned()
        {
            string log = "admin123_123";

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.LoginExist(log);

            Assert.IsTrue(actual);
        }

        [Test]
        public void SelectedItemCheck_PositiveId_TrueReturned()
        {
            int id = 2;

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.SelectedItemCheck(id);

            Assert.IsTrue(actual);
        }

        [Test]
        public void SelectedItemCheck_NegativeId_FalseReturned()
        {
            int id = -1;

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.SelectedItemCheck(id);

            Assert.IsFalse(actual);
        }

        [Test]
        public void AccessMessage_Yes_TrueReturned()
        {
            string msg = "YES";

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.AccessMessage(msg);

            Assert.IsTrue(actual);
        }

        [Test]
        public void AccessMessage_No_FalseReturned()
        {
            string msg = "NO";

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.AccessMessage(msg);

            Assert.IsFalse(actual);
        }

        [Test]
        public void OrderCheck_CorrectInputs_TrueReturned()
        {
            string name = "Yura";
            string mname = "Michailovich";
            string surname = "Yarmoshuk";
            string address = "Doroshevica, 3, 230";
            string email = "xpblll";
            string fullEmail = "xpbllll@mail.ru";
            string number = "1862949";

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.OrderCheck(name, mname, surname, address, email, fullEmail, number);

            Assert.IsTrue(actual);
        }

        [TestCase("Y", "Mihailovich", "Yarmoshuk", "Doroshivicha, 3, 230", "xpbllll", "xpbllll@mail.ru", "1862949")]
        [TestCase("Yura", "M", "Yarmoshuk", "Doroshivicha, 3, 230", "xpbllll", "xpbllll@mail.ru", "1862949")]
        [TestCase("Yura", "Mihailovich", "Y", "Doroshivicha, 3, 230", "xpbllll", "xpbllll@mail.ru", "1862949")]
        [TestCase("Yura", "Mihailovich", "Yarmoshuk", "D", "xpbllll", "xpbllll@mail.ru", "1862949")]
        [TestCase("Yura", "Mihailovich", "Yarmoshuk", "Doroshivicha, 3, 230", "x", "xpbllll@mail.ru", "1862949")]
        [TestCase("Yura", "Mihailovich", "Yarmoshuk", "Doroshivicha, 3, 230", "xpbllll", "xpbllll@mail.ru", "1")]
        [TestCase("Yura", "Mihailovich", "Yarmoshuk", "Doroshivicha, 3, 230", "xpbllll@mail.ru", "xpbllll@mail.ru", "1862949")]
        [TestCase("Yura", "Mihailovich", "Yarmoshuk", "Doroshivicha, 3, 230", "xpbllll", "xpbllllmail.ru", "1862949")]
        [TestCase("Yura", "Mihailovich", "Yarmoshuk", "Doroshivicha, 3, 230", "xpbllll", "xpbllll@yura.com", "1862949")]
        [TestCase("Yura", "Mihailovich", "Yarmoshuk", "Doroshivicha, 3, 230", "xpbllll", "xpbllll@mail.ru", "186294a")]
        public void OrderCheck_NotCorrectInputs_FalseReturned(string n, string mn, string s, string a, string e, string fE, string numb)
        {
            string name = n;
            string mname = mn;
            string surname = s;
            string address = a;
            string email = e;
            string fullEmail = fE;
            string number = numb;

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.OrderCheck(name, mname, surname, address, email, fullEmail, number);

            Assert.IsFalse(actual);
        }

        [Test]
        public void ChangeCheck_NotEqual_TrueReturned()
        {
            string[] oldMas = new string[3] { "1", "2", "3" };
            string[] newMas = new string[3] { "3", "2", "3" };

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.ChangeCheck(oldMas, newMas);

            Assert.IsTrue(actual);
        }

        [Test]
        public void ChangeCheck_Equal_FalseReturned()
        {
            string[] oldMas = new string[3] { "1", "2", "3" };
            string[] newMas = new string[3] { "1", "2", "3" };

            CorrectInputCheck inputCheck = GetInputCheck();
            bool actual = inputCheck.ChangeCheck(oldMas, newMas);

            Assert.IsFalse(actual);
        }

        [Test]
        public void AuthCheck_ExistLogAndPass_TrueReturned()
        {
            string login = "admin";
            string password = "admin";

            BD bd = GetBD();
            bool actual = bd.AuthCheck(login, password);

            Assert.IsTrue(actual);
        }

        [TestCase("", "admin")]
        [TestCase("admin", "")]
        [TestCase("123", "123")]
        public void AuthCheck_ExistLogAndPass_FalseReturned(string log, string pass)
        {
            string login = log;
            string password = pass;

            BD bd = GetBD();
            bool actual = bd.AuthCheck(login, password);

            Assert.IsFalse(actual);
        }

        [TestCase("admin", "Admin")]
        [TestCase("consult", "Consultant")]
        [TestCase("guest", "Guest")]
        public void AccessCheck_CorrectLogin_GoodAccessReturned(string log, string acc)
        {
            string login = log;
            string expected = acc;

            BD bd = GetBD();
            string actual = bd.AccessCheck(login);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ExistTitleCheck_TitleExist_TrueReturned()
        {
            string title = "LightRPG";

            BD bd = GetBD();
            bool actual = bd.ExistTitleCheck(title);

            Assert.IsTrue(actual);
        }

        [Test]
        public void ExistTitleCheck_TitleNotExist_FalseReturned()
        {
            string title = "-250";

            BD bd = GetBD();
            bool actual = bd.ExistTitleCheck(title);

            Assert.IsFalse(actual);
        }

        [Test]
        public void ExistIdCheck_IdExist_TrueReturned()
        {
            int id = 951;

            BD bd = GetBD();
            bool actual = bd.ExistIdCheck(id);

            Assert.IsTrue(actual);
        }

        [Test]
        public void ExistIdCheck_IdNotExist_FalseReturned()
        {
            int id = -200;

            BD bd = GetBD();
            bool actual = bd.ExistIdCheck(id);

            Assert.IsFalse(actual);
        }

        [TestCase(true, "SELECT * FROM Products")]
        [TestCase(false, "SELECT * FROM Products WHERE Quantity > 0")]
        public void ViewAll_QuantityTrue_AllRecords(bool quant, string sqlReq)
        {
            bool quantity = quant;
            int expected = RecordCounted(sqlReq);

            ListBox list = GetListBox();
            BD bd = GetBD();
            bd.ViewAll(list, quantity);
            int actual = ListItemCount(list);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WorkWithBD_InsertData_SeccessInsert()
        {
            string title = "TestMethod";
            string category = "TestCategory";
            int cost = -200;
            string discription = "Test method";
            int quantity = -1;
            int expecte = RecordCounted("SELECT * FROM Products") + 1;

            BD bd = GetBD();
            bd.WorkWithBD(String.Format("INSERT INTO Products VALUES ('{0}','{1}',{2},'{3}',{4})", title, category, cost, discription, quantity));
            int actual = RecordCounted("SELECT * FROM Products");

            Assert.AreEqual(expecte, actual);
        }

        [Test]
        public void WorkWithBD_IdProduct_GoodArray()
        {
            int id = ProdIdRecord("TestMethod");
            string[] expected = new string[6] { "TestMethod", "TestCategory" , "-200", "Test method" , "-1", id.ToString()};

            BD bd = GetBD();
            string[] actual = bd.WorkWithBD(id);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("TestMethod", "TestMethodUpdate")]
        [TestCase("TestMethodUpdate", "TestMethod")]
        public void WorkWithBD_UpdateRecord_RecordUpdated(string oldT, string newT)
        {
            string oldTitle = oldT;
            string newTitle = newT;
            int id = ProdIdRecord(oldTitle);
            string[] expected = new string[6] { newTitle, "TestCategory", "-200", "Test method", "-1", id.ToString() };

            BD bd = GetBD();
            bd.WorkWithBD(String.Format("UPDATE Products SET Title = '{0}' WHERE Title = '{1}'", newTitle, oldTitle));
            string[] actual = bd.WorkWithBD(id);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WorkWithBD_DeleteRecord_RecordDeleted()
        {
            string title = "TestMethod";
            int expected = RecordCounted("SELECT * FROM Products") - 1;

            BD bd = GetBD();
            bd.WorkWithBD(String.Format("DELETE Products WHERE Title = '{0}'", title));
            int actual = RecordCounted("SELECT * FROM Products");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ViewAllAcc_ListBoxValue_ViewAllRecords()
        {
            int expected = RecordCounted("SELECT * FROM Accounts") - 1;

            BD bd = GetBD();
            ListBox list = GetListBox();
            bd.ViewAllAcc(list);
            int actual = ListItemCount(list);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ExistAccCheck_ExistAccount_TrueReturned()
        {
            string login = "admin";

            BD bd = GetBD();
            bool actual = bd.ExistAccCheck(login);

            Assert.IsTrue(actual);
        }

        [Test]
        public void ExistAccCheck_NotExistAccount_FalseReturned()
        {
            string login = "12332";

            BD bd = GetBD();
            bool actual = bd.ExistAccCheck(login);

            Assert.IsFalse(actual);
        }

        [Test]
        public void IdCheck_ExistLogin_GoodId()
        {
            string login = "guest";
            int expected = 951;

            BD bd = GetBD();
            int actual = bd.IdCheck(login);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IdCheck_NotExistLogin_InvalidId()
        {
            string login = "guest 123";
            int expected = -1;

            BD bd = GetBD();
            int actual = bd.IdCheck(login);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void EmptyBucketCheck_CorrectId_TrueReturnned()
        {
            int id = 951;

            BD bd = GetBD();
            bool actual = bd.EmptyBucketCheck(id);

            Assert.IsTrue(actual);
        }

        [Test]
        public void EmptyBucketCheck_UncorrectId_FalseReturnned()
        {
            int id = -1;

            BD bd = GetBD();
            bool actual = bd.EmptyBucketCheck(id);

            Assert.IsFalse(actual);
        }

        [Test]
        public void OrderExist_CorrectID_TrueReturned()
        {
            int id = 951;

            BD bd = GetBD();
            bool actual = bd.OrderExist(id);

            Assert.IsTrue(actual);
        }

        [Test]
        public void OrderExist_UncorrectID_FalseReturned()
        {
            int id = -1;

            BD bd = GetBD();
            bool actual = bd.OrderExist(id);

            Assert.IsFalse(actual);
        }

        [Test]
        public void OrderId_CorrectId_GoodId()
        {
            int idBucket = 951;
            int expected = 2;

            BD bd = GetBD();
            int actual = bd.OrderId(idBucket);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderId_UncorrectId_NotGoodId()
        {
            int idBucket = -1;
            int expected = -1;

            BD bd = GetBD();
            int actual = bd.OrderId(idBucket);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBucketId_ExistLogin_GoodID()
        {
            string login = "guest";
            int expected = 951;

            BD bd = GetBD();
            int actual = bd.GetBucketId(login);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBucketId_NotExistLogin_NotgoodID()
        {
            string login = "123 123";
            int expected = -1;

            BD bd = GetBD();
            int actual = bd.GetBucketId(login);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ViewBucket_ListVallue_ViewAllRecord()
        {
            int expected = RecordCounted("SELECT * FROM Buckets");

            ListBox list = GetListBox();
            BD bd = GetBD();
            bd.ViewBucket(list);
            int actual = ListItemCount(list);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ViewBucket_ListVallueAndId_ViewAllRecord()
        {
            int idBucket = 951;
            string sqlRec = String.Format("SELECT * FROM Buckets WHERE idBucket = {0}", idBucket);
            int expected = RecordCounted(sqlRec);

            ListBox list = GetListBox();
            BD bd = GetBD();
            bd.ViewBucket(list, idBucket);
            int actual = ListItemCount(list);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ProdIdFromBucket_CorrectId_GoodId()
        {
            int idBucket = 951;
            int expected = 6;

            BD bd = GetBD();
            int actual = bd.ProdIdFromBucket(idBucket);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ProdIdFromBucket_UncorrectId_BadId()
        {
            int idBucket = -1;
            int expected = -1;

            BD bd = GetBD();
            int actual = bd.ProdIdFromBucket(idBucket);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QuantityProduct_CorrectID_GoodQuantity()
        {
            int id = 6;
            int expected = 2;

            BD bd = GetBD();
            int actual = bd.QuantityProduct(id);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void QuantityProduct_UncorrectID_ZeroQuantity()
        {
            int id = -1;
            int expected = 0;

            BD bd = GetBD();
            int actual = bd.QuantityProduct(id);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderView_ListboxValue_ViewAllRecords()
        {
            int expected = RecordCounted("SELECT * FROM Orders");

            BD bd = GetBD();
            ListBox list = GetListBox();
            bd.OrderView(list);
            int actual = ListItemCount(list);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderView_IdValue_MassOrder()
        {
            int id = 2;
            string[] expected = new string[10] { "2", "Yura", "Mihailovich", "Yarmoshuk", "Minsk, Doroshevicha, 3, 230", "xpbllll@mail.ru", "23.11.2018", "+375 29 1862949", "delivered", "951" };

            BD bd = GetBD();            
            string[] actual = bd.OrderView(id);

            Assert.AreEqual(expected, actual);
        }
    }
}
