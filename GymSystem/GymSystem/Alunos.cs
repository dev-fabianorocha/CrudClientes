using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymSystem
{
    class Alunos
    {
        frmAlunos frm = new frmAlunos();
        public void criar(string cpf, string nome, string nascimento, string genero, string telefone, string celular, string cep, string endereco, string numero, string complemento, string bairro, string estado, string cidade)
        {
            if (cpf == "")
                MessageBox.Show("Informe um CPF/CNPJ!", "CPF/CNPJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (ValidarCpf.validarCPF(cpf))
                {
                    try
                    {
                        ConexaoBanco cn = new ConexaoBanco();
                        string querySelect = "SELECT COUNT(ALU_CPF) FROM ALUNOS WHERE ALU_CPF = '" + cpf + "'";
                        SqlCommand cmdSelect = new SqlCommand(querySelect, cn.conectar());
                        int row = Convert.ToInt32(cmdSelect.ExecuteScalar());
                        cn.desconectar();

                        if (row >= 1)
                        {
                            MessageBox.Show("Cliente já cadastrado!", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {

                            string queryInsertCliente = "INSERT INTO ALUNOS (ALU_CPF, ALU_NOME, ALU_NASCIMENTO, ALU_GENERO, ALU_TELEFONE, ALU_CELULAR) VALUES('" + cpf + "', '" + nome + "','" + nascimento + "','" + genero + "','" + telefone + "','" + celular + "')";
                            SqlCommand cmdInsertCliente = new SqlCommand(queryInsertCliente, cn.conectar());
                            cmdInsertCliente.ExecuteNonQuery();

                            string queryInsertEndereco = "INSERT INTO ENDERECO (END_CEP, END_ENDERECO, END_NUMERO, END_COMPLEMENTO, END_BAIRRO, END_ESTADO, END_CIDADE, END_CPF) VALUES('" + cep + "', '" + endereco + "','" + numero + "','" + complemento + "','" + bairro + "','" + estado + "','" + cidade + "','" + cpf + "')";
                            SqlCommand cmdInsertEndereco = new SqlCommand(queryInsertEndereco, cn.conectar());
                            cmdInsertEndereco.ExecuteNonQuery();

                            cn.desconectar();

                            MessageBox.Show("Cliente cadastrado com sucesso!", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frm.Close();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao inserir Cliente\n" + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Informe um CPF válido!", "CPF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
