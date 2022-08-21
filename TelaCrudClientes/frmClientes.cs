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
    public partial class frmClientes : Form
    {
        frmCadastroClientes form = new frmCadastroClientes();
        public frmClientes()
        {
            InitializeComponent();
        }
        private void btnIncluir_Click(object sender, EventArgs e)
        {
            form.operacao = "incluir";
            var formIncluir = new frmCadastroClientes();
            form.mskCNPJ.Enabled = true;
            form.mskCPF.Enabled = true;
            form.txtNome.Enabled = true;
            form.mskNascimento.Enabled = true;
            form.mskTelefone.Enabled = true;
            form.mskCEP.Enabled = true;
            form.txtEndereco.Enabled = true;
            form.txtNumero.Enabled = true;
            form.txtComplemento.Enabled = true;
            form.txtBairro.Enabled = true;
            form.txtEstado.Enabled = true;
            form.txtCidade.Enabled = true;
            form.rdbFisica.Enabled = true;
            form.rdbJuridica.Enabled = true;
            form.ShowDialog();
        }
        public void btnLLocalizar_Click(object sender, EventArgs e)
        {
            string condicaoPesquisa = txtPesquisa.Text;
            try
            {
                if (rdbCPF.Checked == true)
                {
                    string query = "SELECT CLI_CGC, CLI_NOME, CLI_NASCIMENTO, CLI_TELEFONE, END_CEP, END_ENDERECO, END_NUMERO, END_COMPLEMENTO, END_BAIRRO, END_ESTADO, END_CIDADE FROM CLIENTES INNER JOIN ENDERECO ON END_CGC = CLI_CGC WHERE CLI_CGC ='" + condicaoPesquisa + "'";
                    alimentarDataGrid(query);
                }
                else if (rdbNome.Checked == true)
                {
                    string query = "SELECT CLI_CGC, CLI_NOME, CLI_NASCIMENTO, CLI_TELEFONE, END_CEP, END_ENDERECO, END_NUMERO, END_COMPLEMENTO, END_BAIRRO, END_ESTADO, END_CIDADE FROM CLIENTES INNER JOIN ENDERECO ON END_CGC = CLI_CGC WHERE CLI_NOME LIKE '" + condicaoPesquisa + "%'";
                    alimentarDataGrid(query);
                }
                else
                {
                    string query = "SELECT CLI_CGC, CLI_NOME, CLI_NASCIMENTO, CLI_TELEFONE, END_CEP, END_ENDERECO, END_NUMERO, END_COMPLEMENTO, END_BAIRRO, END_ESTADO, END_CIDADE FROM CLIENTES INNER JOIN ENDERECO ON END_CGC = CLI_CGC";
                    alimentarDataGrid(query);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao localizar dados do cliente\n" + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            rdbCPF.Checked = false;
            rdbNome.Checked = false;
            txtPesquisa.Text = "";

        }
        private void frmClientes_Load(object sender, EventArgs e)
        { 
            string query = "SELECT CLI_CGC, CLI_NOME, CLI_NASCIMENTO, CLI_TELEFONE, END_CEP, END_ENDERECO, END_NUMERO, END_COMPLEMENTO, END_BAIRRO, END_ESTADO, END_CIDADE FROM CLIENTES INNER JOIN ENDERECO ON END_CGC = CLI_CGC";
            alimentarDataGrid(query);

            dgvClientes.Columns["consultar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvClientes.Columns["alterar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvClientes.Columns["excluir"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            this.dgvClientes.DefaultCellStyle.Font = new Font("Tahoma", 10);
            this.dgvClientes.DefaultCellStyle.ForeColor = Color.Black;
            this.dgvClientes.DefaultCellStyle.BackColor = Color.White;
            this.dgvClientes.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.dgvClientes.DefaultCellStyle.SelectionBackColor = Color.White;
       
            dgvClientes.ReadOnly = true;

            foreach (DataGridViewColumn coluna in dgvClientes.Columns)
            {
                switch (coluna.Name)
                {
                    case "CLI_CGC":
                        coluna.Width = 150;
                        coluna.HeaderText = "CPF/CNPJ";
                        break;
                    case "CLI_NOME":
                        coluna.Width = 180;
                        coluna.HeaderText = "Nome";
                        break;
                    case "CLI_TELEFONE":
                        coluna.Width = 120;
                        coluna.HeaderText = "Celular";
                        break;
                    case "END_ENDERECO":
                        coluna.Width = 300;
                        coluna.HeaderText = "Endereço";
                        break;
                    case "END_NUMERO":
                        coluna.Width = 50;
                        coluna.HeaderText = "Numero";
                        break;
                    case "END_BAIRRO":
                        coluna.Width = 100;
                        coluna.HeaderText = "Bairro";
                        break;
                    case "END_ESTADO":
                        coluna.Width = 30;
                        coluna.HeaderText = "UF";
                        break;
                    case "END_CIDADE":
                        coluna.Width = 150;
                        coluna.HeaderText = "Cidade";
                        break;
                    case "consultar":
                        coluna.Width = 50;
                        coluna.DisplayIndex = 13;
                        break;
                    case "alterar":
                        coluna.Width = 50;
                        coluna.DisplayIndex = 13;
                        break;
                    case "excluir":
                        coluna.DisplayIndex = 13;
                        coluna.Width = 50;
                        break;
                    default:
                        coluna.Visible = false;
                        break;
                }
            }
        }
        private void dgvClientes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgvClientes.Rows[e.RowIndex].Cells["consultar"].ToolTipText = "Clique aqui para consultar.";
            dgvClientes.Rows[e.RowIndex].Cells["alterar"].ToolTipText = "Clique aqui para editar.";
            dgvClientes.Rows[e.RowIndex].Cells["excluir"].ToolTipText = "Clique aqui para excluir.";
        }
        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string cgc = dgvClientes.Rows[e.RowIndex].Cells["CLI_CGC"].Value.ToString();
            string nome = dgvClientes.Rows[e.RowIndex].Cells["CLI_NOME"].Value.ToString();
            string nascimento = dgvClientes.Rows[e.RowIndex].Cells["CLI_NASCIMENTO"].Value.ToString();
            string telefone = dgvClientes.Rows[e.RowIndex].Cells["CLI_TELEFONE"].Value.ToString();
            string cep = dgvClientes.Rows[e.RowIndex].Cells["END_CEP"].Value.ToString();
            string endereco = dgvClientes.Rows[e.RowIndex].Cells["END_ENDERECO"].Value.ToString();
            string numero = dgvClientes.Rows[e.RowIndex].Cells["END_NUMERO"].Value.ToString();
            string complemento = dgvClientes.Rows[e.RowIndex].Cells["END_COMPLEMENTO"].Value.ToString();
            string bairro = dgvClientes.Rows[e.RowIndex].Cells["END_BAIRRO"].Value.ToString();
            string estado = dgvClientes.Rows[e.RowIndex].Cells["END_ESTADO"].Value.ToString();
            string cidade = dgvClientes.Rows[e.RowIndex].Cells["END_CIDADE"].Value.ToString();
            double cgcQuantidade = Convert.ToDouble(cgc);
            try
            {
                if (dgvClientes.Columns[e.ColumnIndex] == dgvClientes.Columns["alterar"])
                {
                    form.operacao = "alterar";
                    form.mskCNPJ.Enabled = false;
                    form.mskCPF.Enabled = false;
                    form.txtNome.Enabled = true;
                    form.mskNascimento.Enabled = true;
                    form.mskTelefone.Enabled = true;
                    form.mskCEP.Enabled = true;
                    form.txtEndereco.Enabled = true;
                    form.txtNumero.Enabled = true;
                    form.txtComplemento.Enabled = true;
                    form.txtBairro.Enabled = true;
                    form.txtEstado.Enabled = true;
                    form.txtCidade.Enabled = true;
                    form.rdbFisica.Enabled = false;
                    form.rdbJuridica.Enabled = false;
                    alimentarFormulario(cgcQuantidade, cgc, nome, nascimento, telefone, cep, endereco, numero, complemento, bairro, estado, cidade);
                }
                if (dgvClientes.Columns[e.ColumnIndex] == dgvClientes.Columns["excluir"])
                {
                    form.operacao = "excluir";
                    form.mskCNPJ.Enabled = false;
                    form.mskCPF.Enabled = false;
                    form.txtNome.Enabled = false;
                    form.mskNascimento.Enabled = false;
                    form.mskTelefone.Enabled = false;
                    form.mskCEP.Enabled = false;
                    form.txtEndereco.Enabled = false;
                    form.txtNumero.Enabled = false;
                    form.txtComplemento.Enabled = false;
                    form.txtBairro.Enabled = false;
                    form.txtEstado.Enabled = false;
                    form.txtCidade.Enabled = false;
                    form.rdbFisica.Enabled = false;
                    form.rdbJuridica.Enabled = false;
                    alimentarFormulario(cgcQuantidade, cgc, nome, nascimento, telefone, cep, endereco, numero, complemento, bairro, estado, cidade);
                }
                if (dgvClientes.Columns[e.ColumnIndex] == dgvClientes.Columns["consultar"])
                {
                    form.operacao = "consultar";
                    form.mskCNPJ.Enabled = false;
                    form.mskCPF.Enabled = false;
                    form.txtNome.Enabled = false;
                    form.mskNascimento.Enabled = false;
                    form.mskTelefone.Enabled = false;
                    form.mskCEP.Enabled = false;
                    form.txtEndereco.Enabled = false;
                    form.txtNumero.Enabled = false;
                    form.txtComplemento.Enabled = false;
                    form.txtBairro.Enabled = false;
                    form.txtEstado.Enabled = false;
                    form.txtCidade.Enabled = false;
                    form.rdbFisica.Enabled = false;
                    form.rdbJuridica.Enabled = false;
                    alimentarFormulario(cgcQuantidade, cgc, nome, nascimento, telefone, cep, endereco, numero, complemento, bairro, estado, cidade);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir Cliente\n" + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void alimentarFormulario(double cgcQuantidade, string cgc, string nome, string nascimento, string telefone, string cep, string endereco, string numero, string complemento, string bairro, string estado, string cidade)
        {
            if (cgcQuantidade <= 99999999999)
            {
                form.rdbFisica.Checked = true;
                form.rdbJuridica.Checked = false;
                form.lblRazaoSocial.Visible = false;
                form.lblNome.Visible = true;
                form.mskCPF.Visible = true;
                form.mskCNPJ.Visible = false;
                form.mskCPF.Text = cgc;
                form.txtNome.Text = nome;
                form.mskNascimento.Text = nascimento;
                form.mskTelefone.Text = telefone;
                form.mskCEP.Text = cep;
                form.txtEndereco.Text = endereco;
                form.txtNumero.Text = numero;
                form.txtComplemento.Text = complemento;
                form.txtBairro.Text = bairro;
                form.txtEstado.Text = estado;
                form.txtCidade.Text = cidade;
                form.ShowDialog();
            }
            else
            {
                form.rdbFisica.Checked = false;
                form.rdbJuridica.Checked = true;
                form.lblRazaoSocial.Visible = true;
                form.lblNome.Visible = false;
                form.mskCPF.Visible = false;
                form.mskCNPJ.Visible = true;
                form.mskCNPJ.Text = cgc;
                form.txtNome.Text = nome;
                form.mskNascimento.Text = nascimento;
                form.mskTelefone.Text = telefone;
                form.mskCEP.Text = cep;
                form.txtEndereco.Text = endereco;
                form.txtNumero.Text = numero;
                form.txtComplemento.Text = complemento;
                form.txtBairro.Text = bairro;
                form.txtEstado.Text = estado;
                form.txtCidade.Text = cidade;
                form.ShowDialog();
            }

        }

        public void alimentarDataGrid(string query)
        {
            ConexaoBanco cn = new ConexaoBanco();
            SqlCommand cmd = new SqlCommand(query, cn.conectar());
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);
            dgvClientes.DataSource = ds;
            dgvClientes.DataMember = ds.Tables[0].TableName;
            cn.desconectar();
        }
    }
}
