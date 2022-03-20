using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymSystem
{
    public partial class frmAlunos : Form
    {

        public Boolean incluirAluno;
        public Boolean consultarAluno;
        public Boolean alterarAluno;
        public Boolean excluirAluno;
        public frmAlunos()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            mskCPF.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mskNascimento.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mskTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mskCelular.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mskCEP.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            string cpf = mskCPF.Text;
            string nome = txtNome.Text;
            string nascimento = mskNascimento.Text;
            string genero = cbxGenero.Text;
            string telefone = mskTelefone.Text;
            string celular = mskCelular.Text;
            string cep = mskCEP.Text;
            string endereco = txtEndereco.Text;
            string numero = txtNumero.Text;
            string complemento = txtComplemento.Text;
            string bairro = txtBairro.Text;
            string estado = txtUF.Text;
            string cidade = txtCidade.Text;


            if (incluirAluno == true) {         
                Alunos criar = new Alunos();
                criar.criar(cpf, nome, nascimento, genero, telefone, celular, cep, endereco, numero, complemento, bairro, estado, cidade);

            }
            if (consultarAluno == true)
            {

            }
            if (alterarAluno == true)
            {

            }
            if (excluirAluno == true)
            {

            }




        }

        private void frmAlunos_Load(object sender, EventArgs e)
        {

        }

    }
}
