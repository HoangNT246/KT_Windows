using QuanLiQuanNhau.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanNhau.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance 
        { get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; } 
        }
        public static int TableWidht = 90;
        public static int TableHeight = 90;
        private TableDAO() { }
        public List<Table> LoadTableList()
        {
            List<Table> tablelist = new List<Table>();
            DataTable data = DataProvide.Instance.ExcuteQuery("USP_GetTableList");
            foreach(DataRow item in data.Rows)
            {
                Table table=new Table(item);
                tablelist.Add(table);
            }
            return tablelist;

        }
    }
}
