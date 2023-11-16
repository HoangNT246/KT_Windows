using QuanLiQuanNhau.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanNhau.DAO
{
    public class FoodDAO
    {   
            private static FoodDAO instance;

            public static FoodDAO Instance
            {
                get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
                private set { FoodDAO.instance = value; }
            }
            private FoodDAO() { }
        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();
            string query = " SELECT * FROM Food WHERE idCategory =" + id;
            DataTable data = DataProvide.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public List<Food> GetListFood()
        {
            List<Food> list = new List<Food>();

            string query = "select * from Food";

            DataTable data = DataProvide.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }
        public bool InsertFood(string name,int id,float price)
        {
            string query=string.Format("INSERT dbo.Food(name,idcategory,price) VALUES (N'{0}',{1},{2})",name,id,price);
            int result =DataProvide.Instance.ExcuteNonQuery(query);

            return result >0;
        }
        public bool UpdateFood(string name, int id, float price,int idFood)
        {
            string query = string.Format("UPDATE dbo.Food  SET name=N'{0}',idcategory={1},price={2} WHERE id={3}", name, id, price,idFood);
            int result = DataProvide.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteFood(int idFood)
        {
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(idFood);
            string query = string.Format("DELETE Food WHERE id = {0}", idFood);
            int result = DataProvide.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
    }
    
}

