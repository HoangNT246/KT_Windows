using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanNhau.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? dateCheckin,DateTime?dateCheckout,int status, int discount=0)
        {
            this.ID = id;
            this.DateCheckIn=dateCheckin;
            this.DateCheckOut=dateCheckout;
            this.Status = status;
            this.Discount = discount;
        }
        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["dateCheckin"];

            var dateCheckOutTemp = row["dateCheckout"];
            if(dateCheckOutTemp .ToString()!="" )
            {
                this.DateCheckOut = (DateTime?)dateCheckOutTemp;
            }
           
            this.Status = (int)row["status"];
            this.Discount = (int)row["discount"];
        }
        private int discount;
        private int status;
        private DateTime? dateCheckOut;
        private DateTime? dateCheckIn;
        private int iD;

        public DateTime? DateCheckIn 
        { 
            get => dateCheckIn;
            set => dateCheckIn = value; 
        }
        public int ID 
        { 
            get => iD; 
            set => iD = value; 
        }
        public DateTime? DateCheckOut 
        { 
            get => dateCheckOut; 
            set => dateCheckOut = value; 
        }
        public int Status 
        { 
            get => status; 
            set => status = value; 
        }
        public int Discount { get => discount; set => discount = value; }
    }
}
