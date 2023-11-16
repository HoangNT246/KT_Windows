using QuanLiQuanNhau.DAO;
using QuanLiQuanNhau.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace QuanLiQuanNhau
{
    public partial class frmAdmin : Form
    {
        BindingSource foodList=new BindingSource();
        BindingSource accountList=new BindingSource();
        BindingSource tableFoodList=new BindingSource();
        public frmAdmin()
        {
            InitializeComponent();
            Loading();
        }
        void Loading()
        {
            dtgvFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadAccount();
            AddFoodBinding();
            LoasdCategoryIntoCombox(cbFoodCategory);
            AddAccountBinding();
            LoadTableFood();
            AddTableFoodBinding();
        }
        void LoadTableFood()
        {
            dtgvTable.DataSource = TableFoodDAO.Instance.GetListTableFood();
        }
        void AddTableFoodBinding()
        {
            txbTableId.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "ID"));
            txbTableName.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Name"));
            txbStatus.DataBindings.Add(new Binding("Text", dtgvTable.DataSource, "Status"));
        }
        void AddAccountBinding()
        {
            txbAccountName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName"));
            txbAccountDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName"));
            nmAccount.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type"));
        }
        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccounts();
        }
        void LoadDateTimePickerBill()
        {
            DateTime today= DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tbFood_Click(object sender, EventArgs e)
        {

        }

        private void tpCategory_Click(object sender, EventArgs e)
        {

        }
        #region methods
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource= BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }
        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name",true,DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        void LoasdCategoryIntoCombox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        void AddTableFood(int id, string name, string status)
        {
            if (TableFoodDAO.Instance.InsertTableFood(id, name, status))
            {
                MessageBox.Show("Thêm bàn ăn thành công");
            }
            else
            {
                MessageBox.Show("Thêm bàn ăn thất bại");
            }
            LoadTableFood();
        }
        void DeleteTableFood(string name)
        {
            if (TableFoodDAO.Instance.DeleteTableFood(name))
            {
                MessageBox.Show("Xóa bàn ăn thành công");
            }
            else
            {
                MessageBox.Show("Xóa bàn ăn thất bại");
            }
            LoadTableFood();
        }
        void EditTableFood(int id, string name, string status)
        {
            if (TableFoodDAO.Instance.UpdateTableFood(id, name, status))
            {
                MessageBox.Show("Cập nhật bàn ăn thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật bàn ăn thất bại");
            }
            LoadTableFood();
        }
        void Addcount(string userName, string displayName,int type)
        {
            if(AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thất bại");
            }
            LoadAccount();
        }
        void Editcount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccounts(userName, displayName, type))
            {
                MessageBox.Show("Cập nhật tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Cập nhật tài khoản thất bại");
            }
            LoadAccount();
        }
        void Deletecount(string userName)
        {
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa tài khoản thất bại");
            }
            LoadAccount();
        }
        void ResetPass(string userName)
        {
            if (AccountDAO.Instance.ResetPassWord(userName))
            {
                MessageBox.Show("Đặt lại mật khẩu  thành công");
            }
            else
            {
                MessageBox.Show("Đặt lại mật khẩu thất bại");
            }
            
        }
        #endregion
        #region envent
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }
    
       
        

        #endregion

        private void btnViewFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanLyQuanNhau99DataSet.Food' table. You can move, or remove it, as needed.
            this.foodTableAdapter.Fill(this.quanLyQuanNhau99DataSet.Food);

        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count > 0)
            {
                int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;


                Category category = CategoryDAO.Instance.GetCategoryByID(id);
                cbFoodCategory.SelectedItem = category;
                int index = -1;
                int i = 0;
                foreach(Category item in cbFoodCategory.Items)
                {
                    if(item.ID==category.ID)
                    {
                        index = i; break;
                    }    
                    i++;
                }
                cbFoodCategory.SelectedIndex = index;
            }
           
          
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name=txbFoodName.Text;
            int categoryID=(cbFoodCategory.SelectedItem as Category).ID;
            float price=(float)nmFoodPrice.Value;
            if(FoodDAO.Instance.InsertFood(name,categoryID,price))
            {
                MessageBox.Show("Thêm món thành công");
                LoadListFood();

            }    
            else
            {
                MessageBox.Show("Có lỗi khi thêm thức ăn");
            }    
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id=Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.UpdateFood(name,categoryID,price,id))
            {
                MessageBox.Show("Sửa món thành công");
                LoadListFood();

            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa thức ăn");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món thành công");
                LoadListFood();

            }
            else
            {
                MessageBox.Show("Xóa lỗi khi sửa thức ăn");
            }
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName=txbAccountName.Text;
            string displayName=txbAccountDisplayName.Text;
            int type = (int)nmAccount.Value;
            Addcount(userName,displayName,type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbAccountName.Text;
            
            Deletecount(userName);
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string userName = txbAccountName.Text;
            string displayName = txbAccountDisplayName.Text;
            int type = (int)nmAccount.Value;
            Editcount(userName, displayName, type);
        }

        private void btnResetPass_Click(object sender, EventArgs e)
        {
            string userName = txbAccountName.Text;

            ResetPass(userName);
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            LoadTableFood();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableId.Text);
            string name = txbTableName.Text;
            string status=txbStatus.Text;
            AddTableFood(id, name, status);

        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbTableId.Text);
            string name = txbTableName.Text;
            string status = txbStatus.Text;
            EditTableFood(id, name, status);
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;

            DeleteTableFood(name);
        }
    }
}
