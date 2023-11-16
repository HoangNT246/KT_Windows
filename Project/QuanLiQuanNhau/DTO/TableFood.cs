using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanNhau.DTO
{
    public class TableFood
    {
        public TableFood(int id, string name, string status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }

        public TableFood(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();

            this.Status = row["status"].ToString();
        }
        private int iD;
        private string name;
        private string status;


        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public string Status { get => status; set => status = value; }
    }
}
