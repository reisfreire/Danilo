using Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonGravar_Click(object sender, EventArgs e)
        {
            var usuario = new Usuario();
            usuario.Nome = textBoxNome.Text;
            usuario.Telefone = maskedTextBoxTelefone.Text;
            usuario.CPF = maskedTextBoxCPF.Text;
            usuario.Gravar();
            MessageBox.Show("Usuário gravado com sucesso");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
