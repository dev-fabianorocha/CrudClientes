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

namespace GymSystem
{
    public partial class frmPesquisaAlunos : Form
    {
        frmAlunos frm = new frmAlunos();
        public frmPesquisaAlunos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm.incluirAluno = true;
            frm.ShowDialog();
            
          
        }

        private void FrmPesquisaAlunos_Load(object sender, EventArgs e)
        {
            ConexaoBanco cn = new ConexaoBanco();
            string query = "SELECT ALU_CPF,ALU_NOME,ALU_GENERO,ALU_CELULAR FROM ALUNOS";
            SqlCommand cmd = new SqlCommand(query, cn.conectar());
            cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);

            dgvAlunos.DataSource = ds;
            dgvAlunos.DataMember = ds.Tables[0].TableName;

            this.dgvAlunos.DefaultCellStyle.Font = new Font("Arial", 8);
            this.dgvAlunos.DefaultCellStyle.ForeColor = Color.Black;
            this.dgvAlunos.DefaultCellStyle.BackColor = Color.White;
            this.dgvAlunos.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.dgvAlunos.DefaultCellStyle.SelectionBackColor = Color.White;

            dgvAlunos.ReadOnly = true;

            foreach (DataGridViewColumn coluna in dgvAlunos.Columns)
            {
                switch (coluna.Name)
                {
                    case "ALU_CPF":
                        coluna.Width = 100;
                        coluna.HeaderText = "CPF";
                        break;
                    case "ALU_NOME":
                        coluna.Width = 210;
                        coluna.HeaderText = "Nome";
                        break;
                    case "consultar":
                        coluna.Width = 25;
                        coluna.DisplayIndex = 6;
                        break;
                    case "alterar":
                        coluna.Width = 25;
                        coluna.DisplayIndex = 6;
                        break;
                    case "excluir":
                        coluna.DisplayIndex = 6;
                        coluna.Width = 25;
                        break;
                    default:
                        coluna.Visible = false;
                        break;

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvAlunos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvAlunos.Columns[e.ColumnIndex] == dgvAlunos.Columns["consultar"])
                {
                    frm.consultarAluno = true;
                    frm.ShowDialog();


                }
                if (dgvAlunos.Columns[e.ColumnIndex] == dgvAlunos.Columns["alterar"])
                {
                    frm.alterarAluno = true;
                    frm.ShowDialog();


                }
                if (dgvAlunos.Columns[e.ColumnIndex] == dgvAlunos.Columns["excluir"])
                {
                    frm.excluirAluno = true;
                    frm.ShowDialog();


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inserir Cliente\n" + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        
    }
}
