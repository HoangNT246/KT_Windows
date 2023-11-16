using QuanLiQuanNhau.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanNhau.DAO
{
    public class TableFoodDAO
    {
        private static TableFoodDAO instance;

        public static TableFoodDAO Instance
        {
            get { if (instance == null) instance = new TableFoodDAO(); return TableFoodDAO.instance; }
            private set { TableFoodDAO.instance = value; }
        }
        private TableFoodDAO() { }
        public List<TableFood> GetListTableFood()
        {
            List<TableFood> list = new List<TableFood>();

            string query = "select * from TableFood";

            DataTable data = DataProvide.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                TableFood tableFood = new TableFood(item);
                list.Add(tableFood);
            }

            return list;
        }
        public bool InsertTableFood(int id,string name,string status)
        {
            string query = string.Format("SET IDENTITY_INSERT TableFood ON INSERT dbo.TableFood(id, name, status) VALUES (N'{0}',N'{1}',N'{2}')", id,name,status);
            int result = DataProvide.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateTableFood(int id,string name, string status)
        {
            string query = string.Format("UPDATE dbo.TableFood SET Name=N'{1}',status=N'{2}' WHERE id=N'{0}'",id, name, status);
            int result = DataProvide.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteTableFood(string name)
        {

            string query = string.Format("DELETE TableFood WHERE Name = N'{0}'", name);
            int result = DataProvide.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
    }
}
