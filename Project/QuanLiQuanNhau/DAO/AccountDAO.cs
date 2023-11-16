using QuanLiQuanNhau.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanNhau.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance 
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }
        private AccountDAO() { }
        public bool Login(string username, string password)
        {
            string query = "[dbo].[USP_Login] @userName , @passWord ";
            DataTable result = DataProvide.Instance.ExcuteQuery(query,new object[] { username,password });
            return result.Rows.Count>0;
        }
        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvide.Instance.ExcuteQuery("Select * from account where userName = '" + userName+"'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }
        public bool UpdateAccount(string userName, string displayName, string password, string newPass)
        {
            int result = DataProvide.Instance.ExcuteNonQuery("EXEC USP_UpdateAccount  @userName=@p1,  @displayName=@p2 ,  @password=@3, @newPassword ", new object[] { userName, displayName, password, newPass });

            return result > 0;
        }
        public DataTable GetListAccounts()
        {
            return DataProvide.Instance.ExcuteQuery("SELECT UserName,DisplayName, Type FROM dbo.Account");
        }
        public bool InsertAccount(string name,string displayname,int type)
        {
            string query = string.Format("INSERT dbo.Account(UserName,Displayname,Type) VALUES (N'{0}',N'{1}',{2})", name, displayname,type);
            int result = DataProvide.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateAccounts(string name, string displayname, int type)
        {
            string query = string.Format("UPDATE dbo.Account SET DisplayName=N'{1}',Type={2} WHERE UserName=N'{0}'", name, displayname, type);
            int result = DataProvide.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteAccount(string name)
        {
            
            string query = string.Format("DELETE Account WHERE UserName = N'{0}'", name);
            int result = DataProvide.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool ResetPassWord(string name)
        {
            string query = string.Format("update account set password=N'{0}' where UserName=N'{0}'", name);
            int result=DataProvide.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
    }
}
