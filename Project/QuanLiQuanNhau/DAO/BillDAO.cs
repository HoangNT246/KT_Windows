using QuanLiQuanNhau.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanNhau.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get{if (instance==null)instance=new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; } 
        }
        private BillDAO() { }
        public int GetUncheckBillIDByTableID(int id)
        {
             DataTable data= DataProvide.Instance.ExcuteQuery("SELECT *FROM dbo.Bill WHERE idTable = "+id+"AND status=0");
            if(data.Rows.Count > 0 )
            {
                Bill bill=new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }
        public void CheckOut(int id,int discount,float totalPrice)
        {
            string query = "UPDATE dbo.Bill SET dateCheckOut=GETDATE(),  status =1 ,"+"discount="+discount+",totalPrice="+totalPrice + " WHERE id="+id;
            DataProvide.Instance.ExcuteNonQuery(query);
        }

        public void InsertBill(int id)
        {
            DataProvide.Instance.ExcuteQuery("USP_InsertBill @idTable", new object[] { id });
        }
        public DataTable GetBillListByDate(DateTime checkIn, DateTime checkOut) 
        {
            return DataProvide.Instance.ExcuteQuery("EXEC USP_GetListBillByDate @checkIn = '2023-01-01', @checkOut = '2023-12-31';", new object[] { checkIn, checkOut });


        }
        public int GetMaxIDBill()
        {
            try
            {
                return (int)DataProvide.Instance.ExcuteScalar("SELECT MAX(id) FROM dbo.Bill");
            }
            catch
            { return 1; }
        }
    }
}
