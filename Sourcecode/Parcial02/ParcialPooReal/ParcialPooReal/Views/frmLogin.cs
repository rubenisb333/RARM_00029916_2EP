using ParcialPooReal.Controller;
using ParcialPooReal.Model;
using ParcialPooReal.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParcialPooReal
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
       
        public void fillCombo()
        {
            // Actualizar ComboBox
            cmbUsers.DataSource = null;
            cmbUsers.ValueMember = "password";
            cmbUsers.DisplayMember = "userName";
            cmbUsers.DataSource = UserDAO.GetUsers();
        }

        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtPass.Text == cmbUsers.SelectedValue.ToString())
            {
                User u = (User)cmbUsers.SelectedItem;
                MessageBox.Show("¡Bienvenido!",
                "Parcial", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmMain window = new frmMain(u, this);
                window.Show();
                this.Hide();

            }
            else
                MessageBox.Show("¡Contraseña incorrecta!", "Clase GUI 05",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            fillCombo();
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLogin_Click(sender, e);

        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            var frmChange = new frmChange(this);
            frmChange.Show();
            Hide();
        }
    }
}
