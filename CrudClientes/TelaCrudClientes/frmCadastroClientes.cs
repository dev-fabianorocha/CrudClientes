using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelaCrudClientes
{
    public partial class frmCadastroClientes : Form
    {
        public frmCadastroClientes()
        {
            InitializeComponent();
        }

        public string operacao;

        public void btnConfirmar_Click(object sender, EventArgs e)
        {
            mskTelefone.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mskCEP.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mskCPF.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            mskCNPJ.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            string cgc = "";
            string nome = txtNome.Text;
            string nascimento = mskNascimento.Text;
            string telefone = mskTelefone.Text;
            string cep = mskCEP.Text;
            string endereco = txtEndereco.Text;
            string numero = txtNumero.Text;
            string complemento = txtComplemento.Text;
            string bairro = txtBairro.Text;
            string estado = txtEstado.Text;
            string cidade = txtCidade.Text;
            AtualizarBanco acao = new AtualizarBanco();

            if (operacao == "incluir")
            {
                if (rdbFisica.Checked == true & rdbJuridica.Checked == false)
                {
                    cgc = mskCPF.Text;
                    acao.incluirClienteCPF(cgc, nome, nascimento, telefone, cep, endereco, numero, complemento, bairro, estado, cidade);
                    if (acao.concluiuTransacao == true) { this.Close(); }
                }
                else if (rdbJuridica.Checked == true & rdbFisica.Checked == false)
                {
                    cgc = mskCNPJ.Text;
                    acao.incluirClienteCNPJ(cgc, nome, nascimento, telefone, cep, endereco, numero, complemento, bairro, estado, cidade);
                    if (acao.concluiuTransacao == true) { this.Close(); }
                }
                else
                {
                    MessageBox.Show("ERRO!", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (operacao == "alterar")
            {
                if (rdbFisica.Checked == true & rdbJuridica.Checked == false)
                {
                    cgc = mskCPF.Text;
                    acao.alterarClienteCPF(cgc, nome, nascimento, telefone, cep, endereco, numero, complemento, bairro, estado, cidade);
                    if (acao.concluiuTransacao == true) { this.Close(); }
                }
                else if (rdbJuridica.Checked == true & rdbFisica.Checked == false)
                {
                    cgc = mskCNPJ.Text;
                    acao.alterarClienteCNPJ(cgc, nome, nascimento, telefone, cep, endereco, numero, complemento, bairro, estado, cidade);
                    if (acao.concluiuTransacao == true) { this.Close(); }
                }
                else
                {
                    MessageBox.Show("ERRO!", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (operacao == "excluir")
            {
                cgc = mskCPF.Text;
                acao.excluirCliente(cgc);
                if (acao.concluiuTransacao == true) { this.Close(); }
            }
            
            zerarFormulario();

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            zerarFormulario();
            this.Close();
        }
        private void mskNascimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }

        }
        private void mskNascimento_MouseClick(object sender, MouseEventArgs e)
        {
            mskNascimento.SelectionStart = 0;
        }
        private void mskNascimento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
        }
        private void mskCPF_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void mskCPF_MouseClick(object sender, MouseEventArgs e)
        {
            mskCPF.SelectionStart = 0;
        }
        private void mskCPF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
        }
        private void mskTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void mskTelefone_MouseClick(object sender, MouseEventArgs e)
        {
            mskTelefone.SelectionStart = 0;
        }
        private void mskTelefone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
        }
        private void mskCEP_KeyPress(object sender, KeyPressEventArgs e)
        {
            mskCEP.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string cep = mskCEP.Text;
            try
            {
                if (e.KeyChar == 13)
                {
                    using (var ws = new WSCorreios.AtendeClienteClient())
                    {
                        var resultado = ws.consultaCEP(cep);
                        txtEndereco.Text = resultado.end;
                        txtComplemento.Text = resultado.complemento2;
                        txtCidade.Text = resultado.cidade;
                        txtBairro.Text = resultado.bairro;
                        txtEstado.Text = resultado.uf;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO!", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void mskCEP_MouseClick(object sender, MouseEventArgs e)
        {
            mskCEP.SelectionStart = 0;

        }
        private void mskCEP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
        }
        private void rdbFisica_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbFisica.Checked == true)
            {
                mskCPF.Visible = true;
                mskCNPJ.Visible = false;
                lblNome.Visible = true;
                lblRazaoSocial.Visible = false;
                lblDataNascimento.Visible = true;
                mskNascimento.Visible = true;
            }
        }
        private void rdbJuridica_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbJuridica.Checked == true)
            {
                mskCPF.Visible = false;
                mskCNPJ.Visible = true;
                lblNome.Visible = false;
                lblRazaoSocial.Visible = true;
                lblDataNascimento.Visible = false;
                mskNascimento.Visible = false;
            }
        }
        private void frmIncluirCliente_Load(object sender, EventArgs e)
        {
            rdbFisica.Checked = true;
        }
        private void mskCNPJ_MouseClick(object sender, MouseEventArgs e)
        {
            mskCNPJ.SelectionStart = 0;
        }
        private void mskCNPJ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
        }
        void zerarFormulario()
        {
            mskCNPJ.Text = "";
            mskCPF.Text = "";
            txtNome.Text = "";
            mskNascimento.Text = "";
            mskTelefone.Text = "";
            mskCEP.Text = "";
            txtEndereco.Text = "";
            txtNumero.Text = "";
            txtComplemento.Text = "";
            txtBairro.Text = "";
            txtEstado.Text = "";
            txtCidade.Text = "";
        }
    }
}

