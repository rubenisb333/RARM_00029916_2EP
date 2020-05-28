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
    public partial class frmChange : Form
    {
        private frmLogin originForm;
        public frmChange(frmLogin originForm)
        {
            this.originForm = originForm;
            InitializeComponent();
            fillCombo();

        }
        private void fillCombo()
        {
            // Actualizar ComboBox
            cmbUsers.DataSource = null;
            cmbUsers.ValueMember = "password";
            cmbUsers.DisplayMember = "userName";
            cmbUsers.DataSource = UserDAO.GetUsers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
            originForm.Show();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            User user = (User)cmbUsers.SelectedItem;

            if (txtPass.Text.Length!=0 && txtConfirmPass.Text.Length != 0 && txtOldPass.Text.Length !=0)
            {
                if (UserDAO.getIfPasswordMatch(user.userName,txtOldPass.Text))
                {
                    if (txtPass.Text == txtConfirmPass.Text)
                    {
                        UserDAO.changeUserPassword(user.userName, txtConfirmPass.Text);
                        MessageBox.Show("Operacion realizada exitosamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        originForm.fillCombo();

                    }
                    else MessageBox.Show("Las claves no coinciden", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                    else MessageBox.Show("La clave vieja es incorrecta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            else MessageBox.Show("Llenar todos los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void frmChange_Load(object sender, EventArgs e)
        {

        }
    }
}
