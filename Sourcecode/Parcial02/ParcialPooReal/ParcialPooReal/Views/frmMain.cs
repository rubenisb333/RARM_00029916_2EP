using ParcialPooReal.Controller;
using ParcialPooReal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParcialPooReal.Views
{
    public partial class frmMain : Form
    {
        private User loggedUser;
        private bool isDirectionSelected = false;
        private bool isBusiSelected = false;

        private bool hasCheckedUserType = false;
        private string idAddressSelected = "";
        private string idBusiSelected = "";
        private Form originForm;

        public frmMain(User loggedUser, Form originForm)
        {
            this.loggedUser = loggedUser;
            this.originForm = originForm;
            InitializeComponent();
        }

        private void frmAddUsers_Load(object sender, EventArgs e)
        {
            fillControls();
            
        }

        private void fillControls()
        {
            List<UserDirections> directions = UserDirectionsDAO.getDirections(loggedUser.idUser.ToString());

            if (loggedUser.userType)
            {
                List<User> lista = UserDAO.GetUsers();
                List<Business> businesses = BusinessDAO.getBusiness();
                List<Product> products = ProductDAO.getAll();

                cmbProBusi.DataSource = null;
                cmbProBusi.ValueMember = "idbusiness";
                cmbProBusi.DisplayMember = "name";
                cmbProBusi.DataSource = businesses;

                // Tabla (data grid view)
                dtvUsers.DataSource = null;
                dtvUsers.DataSource = lista;
                //Escondiendo la columna de la clave
                dtvUsers.Columns[3].Visible = false;
                dtvUsers.Columns[0].Visible = false;


                dgvBusi.DataSource = null;
                dgvBusi.DataSource = businesses;

                dgvPro.DataSource = null;
                dgvPro.DataSource = products;

            }
            else
            {
                if (!hasCheckedUserType)
                {
                    tabControl1.TabPages[0].Parent = null;
                    tabControl1.TabPages[1].Parent = null;
                    hasCheckedUserType = true;
                }
            }
            dgvDirections.DataSource = null;
            dgvDirections.DataSource = directions;

        }



        private bool allFieldsAreNotEmpty()
        {
            return (txtName.Text.Length != 0 
                && txtUsername.Text.Length != 0);
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            if (allFieldsAreNotEmpty())
            {
                try
                {
                    UserDAO.createNewUser(txtName.Text, txtUsername.Text, txtUsername.Text, chAdmin.Checked);
                    MessageBox.Show("Usuario agregado exitosamente", "Clase GUI 05",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fillControls();
                } catch (Exception ex)
                {
                    if (ex.GetType().ToString() == "Npgsql.PostgresException")
                    {
                        MessageBox.Show("Verificar si el usuario a ingresar ya existe en la base","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Error desconocido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            else
            {
                MessageBox.Show("¡Complete todos los campos!", "Clase GUI 05",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            fillControls();
        }

        private void dtvUsers_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dtvUsers.Rows[e.RowIndex];
            string selectedUser = row.Cells[2].Value.ToString();

            if(selectedUser == loggedUser.userName)
                MessageBox.Show("No se puede eliminar al usuario actual","Alerta", 
                    MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            

            else
            {
                if (MessageBox.Show("¿Seguro que desea eliminar al usuario " + selectedUser + "?",
            "Clase GUI 06", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UserDAO.dropUser(selectedUser);

                    MessageBox.Show("¡Usuario eliminado exitosamente!",
                        "Clase GUI 06", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    fillControls();
                }
            }

        }


        private void btnDirectionAction_Click(object sender, EventArgs e)
        {
            if (txtDirection.Text.Length != 0)
            {
                if (!isDirectionSelected)
                {
                    UserDirectionsDAO.createDirection(loggedUser, txtDirection.Text);
                    MessageBox.Show("Direccion agregada exitosamente", "Clase GUI 05",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fillControls();
                }
                else
                {
                    UserDirectionsDAO.modify(idAddressSelected, txtDirection.Text);
                    MessageBox.Show("Direccion modificada exitosamente", "Clase GUI 05",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fillControls();
                    isDirectionSelected = false;
                    changeStateOfDirectionControlls();
                    txtDirection.Text = "";
                }
            }
            else MessageBox.Show("Favor llenar campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgvDirections_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dgvDirections.Rows[e.RowIndex];
            string selectedDirection = row.Cells[0].Value.ToString();
            UserDirectionsDAO.dropDirection(selectedDirection);
            MessageBox.Show("Direccion eliminada exitosamente", "Clase GUI 05",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
            isDirectionSelected = false;
            changeStateOfDirectionControlls();
            fillControls();
            txtDirection.Text = "";
        }

        private void dgvDirections_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            isDirectionSelected = true;
            changeStateOfDirectionControlls();
            DataGridViewRow row = dgvDirections.Rows[e.RowIndex];
            string selectedDirection = row.Cells[1].Value.ToString();
            idAddressSelected = row.Cells[0].Value.ToString();
            txtDirection.Text = selectedDirection;
        }

        private void dgvBusi_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            isBusiSelected = true;
            changeBusiControlState();
            DataGridViewRow row = dgvBusi.Rows[e.RowIndex];
            idBusiSelected = row.Cells[0].Value.ToString();
            string selectedBusi = row.Cells[1].Value.ToString();
            txtBusiName.Text = selectedBusi;
            txtBusiDesc.Text = row.Cells[2].Value.ToString();
        }
        private void dgvBusi_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dgvBusi.Rows[e.RowIndex];
            string selectedBusi = row.Cells[0].Value.ToString();
            BusinessDAO.drop(selectedBusi);
            mostrarMensajeExito();
            isBusiSelected = false;
            changeBusiControlState();
            fillControls();
            refreshBusinessFields();
        }


        private void changeStateOfDirectionControlls()
        {
            if (isDirectionSelected)
            {
                btnDirectionAction.Text = "Modificar";
                btnCancelar.Visible = true;
            }
            else
            {
                idAddressSelected = "";
                btnDirectionAction.Text = "Agregar";
                btnCancelar.Visible = false;
            }
        }
        private void changeBusiControlState()
        {
            if (isBusiSelected)
            {
                btnBusiAction.Text = "Modificar";
                btnBusiCancel.Visible = true;
            }
            else
            {
                isBusiSelected = false;
                btnBusiAction.Text = "Agregar";
                btnBusiCancel.Visible = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            isDirectionSelected = false;
            txtDirection.Text = "";
            changeStateOfDirectionControlls();
        }

        private void btnDirectionRefresh_Click(object sender, EventArgs e)
        {
            fillControls();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            this.Dispose();
            hasCheckedUserType = false;
            originForm.Show();

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            originForm.Show();
        }

        private void btnBusiAction_Click(object sender, EventArgs e)
        {
            if (txtBusiName.Text.Length != 0 && txtBusiDesc.Text.Length != 0)
            {
                if (!isBusiSelected)
                {
                    var business = new Business();
                    business.name = txtBusiName.Text;
                    business.description = txtBusiDesc.Text;
                    BusinessDAO.createBusiness(business);
                    refreshBusinessFields();
                    fillControls();
                    mostrarMensajeExito();
                }
                else
                {
                    var business = new Business();
                    business.idBusiness = Convert.ToInt32(idBusiSelected);
                    business.name = txtBusiName.Text;
                    business.description = txtBusiDesc.Text;
                    BusinessDAO.modify(business);
                    refreshBusinessFields();
                    fillControls();
                    mostrarMensajeExito();
                    isBusiSelected = false;
                    changeBusiControlState();
                }
            }
            else mostrarFuncionLlenadoCampos();
        }

        private void btnProAdd_Click(object sender, EventArgs e)
        {
            if (txtProName.Text.Length != 0)
            {
                var product = new Product();
                product.productName = txtProName.Text;
                product.businessId = Convert.ToInt32(cmbProBusi.SelectedValue.ToString());
                ProductDAO.create(product);
                cleanProductFields();
                fillControls();
                mostrarMensajeExito();
            }
            else mostrarFuncionLlenadoCampos();

        }

        private void cleanProductFields()
        {
            txtProName.Text = "";
        }



        private void refreshBusinessFields()
        {
            txtBusiName.Text = "";
            txtBusiDesc.Text = "";
        }

        private void mostrarFuncionLlenadoCampos()
        {
            MessageBox.Show("Favor llenar todos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void mostrarMensajeExito()
        {
            MessageBox.Show("Accion realizada correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnBusiCancel_Click(object sender, EventArgs e)
        {
            isBusiSelected = false;
            refreshBusinessFields();
            changeBusiControlState();
            
        }

        private void btnBusiRefresh_Click(object sender, EventArgs e)
        {
            isBusiSelected = false;
            refreshBusinessFields();
            fillControls();
            changeBusiControlState();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
