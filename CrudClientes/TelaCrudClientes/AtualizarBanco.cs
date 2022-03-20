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
    class AtualizarBanco
    {
        public Boolean concluiuTransacao;
        public void incluirClienteCPF(string cgc, string nome, string nascimento, string telefone, string cep, string endereco, string numero, string complemento, string bairro, string estado, string cidade)
        {
            if (cgc == "")
                MessageBox.Show("Informe um CPF/CNPJ!", "CPF/CNPJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (ValidacaoCgc.validarCPF(cgc))
                {
                    try
                    {
                        ConexaoBanco cn = new ConexaoBanco();
                        string querySelect = "SELECT COUNT(CLI_CGC) FROM CLIENTES WHERE CLI_CGC = '" + cgc + "'";
                        SqlCommand cmdSelect = new SqlCommand(querySelect, cn.conectar());
                        int row = Convert.ToInt32(cmdSelect.ExecuteScalar());
                        cn.desconectar();

                        if (row >= 1)
                        {
                            MessageBox.Show("Cliente já cadastrado!", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {

                            string queryInsertCliente = "INSERT INTO CLIENTES (CLI_CGC, CLI_NOME, CLI_NASCIMENTO, CLI_TELEFONE) VALUES('" + cgc + "', '" + nome + "','" + nascimento + "','" + telefone + "')";
                            SqlCommand cmdInsertCliente = new SqlCommand(queryInsertCliente, cn.conectar());
                            cmdInsertCliente.ExecuteNonQuery();

                            string queryInsertEndereco = "INSERT INTO ENDERECO (END_CEP, END_ENDERECO, END_NUMERO, END_COMPLEMENTO, END_BAIRRO, END_ESTADO, END_CIDADE, END_CGC) VALUES('" + cep + "', '" + endereco + "','" + numero + "','" + complemento + "','" + bairro + "','" + estado + "','" + cidade + "','" + cgc + "')";
                            SqlCommand cmdInsertEndereco = new SqlCommand(queryInsertEndereco, cn.conectar());
                            cmdInsertEndereco.ExecuteNonQuery();

                            cn.desconectar();

                            MessageBox.Show("Cliente cadastrado com sucesso!", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            concluiuTransacao = true;
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

        public void incluirClienteCNPJ(string cgc, string nome, string nascimento, string telefone, string cep, string endereco, string numero, string complemento, string bairro, string estado, string cidade)
        {
            if (cgc == "")
                MessageBox.Show("Informe um CPF/CNPJ!", "CPF/CNPJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (ValidacaoCgc.ValidarCNPJ(cgc))
                {
                    try
                    {
                        ConexaoBanco cn = new ConexaoBanco();
                        string querySelect = "SELECT COUNT(CLI_CGC) FROM CLIENTES WHERE CLI_CGC = '" + cgc + "'";
                        SqlCommand cmdSelect = new SqlCommand(querySelect, cn.conectar());
                        int row = Convert.ToInt32(cmdSelect.ExecuteScalar());
                        cn.desconectar();

                        if (row >= 1)
                        {
                            MessageBox.Show("Cliente já cadastrado!", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {

                            string queryInsertCliente = "INSERT INTO CLIENTES (CLI_CGC, CLI_NOME, CLI_NASCIMENTO, CLI_TELEFONE) VALUES('" + cgc + "', '" + nome + "','" + nascimento + "','" + telefone + "')";
                            SqlCommand cmdInsertCliente = new SqlCommand(queryInsertCliente, cn.conectar());
                            cmdInsertCliente.ExecuteNonQuery();

                            string queryInsertEndereco = "INSERT INTO ENDERECO (END_CEP, END_ENDERECO, END_NUMERO, END_COMPLEMENTO, END_BAIRRO, END_ESTADO, END_CIDADE, END_CGC) VALUES('" + cep + "', '" + endereco + "','" + numero + "','" + complemento + "','" + bairro + "','" + estado + "','" + cidade + "','" + cgc + "')";
                            SqlCommand cmdInsertEndereco = new SqlCommand(queryInsertEndereco, cn.conectar());
                            cmdInsertEndereco.ExecuteNonQuery();

                            cn.desconectar();

                            MessageBox.Show("Cliente cadastrado com sucesso!", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            concluiuTransacao = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao inserir Cliente\n" + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Informe um CNPJ válido!", "CNPJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void alterarClienteCPF(string cgc, string nome, string nascimento, string telefone, string cep, string endereco, string numero, string complemento, string bairro, string estado, string cidade)
        {
            if (cgc == "")
                MessageBox.Show("Informe um CPF!", "CPF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (ValidacaoCgc.validarCPF(cgc))
                {
                    
                    try
                    {
                        ConexaoBanco cn = new ConexaoBanco();

                        string queryUpdateCliente = "update CLIENTES set CLI_CGC = '" + cgc + "', CLI_NOME = '" + nome + "', CLI_NASCIMENTO = '" + nascimento + "', CLI_TELEFONE = '" + telefone + "' where CLI_CGC ='" + cgc + "'";
                        SqlCommand cmdUpdateCliente = new SqlCommand(queryUpdateCliente, cn.conectar());
                        cmdUpdateCliente.ExecuteNonQuery();

                        string queryUpdateEndereco = "update ENDERECO set END_CEP = '" + cep + "', END_ENDERECO = '" + endereco + "', END_NUMERO = '" + numero + "', END_COMPLEMENTO = '" + complemento + "', END_BAIRRO = '" + bairro + "', END_ESTADO = '" + estado + "', END_CIDADE = '" + cidade + "' where END_CGC ='" + cgc + "'";
                        SqlCommand cmdUpdateEndereco = new SqlCommand(queryUpdateEndereco, cn.conectar());
                        cmdUpdateEndereco.ExecuteNonQuery();



                        cn.desconectar();

                        MessageBox.Show("Cliente alterado com sucesso!", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        concluiuTransacao = true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao alterar Cliente\n" + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Informe um CPF válido!", "CPF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void alterarClienteCNPJ(string cgc, string nome, string nascimento, string telefone, string cep, string endereco, string numero, string complemento, string bairro, string estado, string cidade)
        {
            if (cgc == "")
                MessageBox.Show("Informe um CNPJ!", "CNPJ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (ValidacaoCgc.ValidarCNPJ(cgc))
                {

                    try
                    {
                        ConexaoBanco cn = new ConexaoBanco();

                        string queryUpdateCliente = "update CLIENTES set CLI_CGC = '" + cgc + "', CLI_NOME = '" + nome + "', CLI_NASCIMENTO = '" + nascimento + "', CLI_TELEFONE = '" + telefone + "' where CLI_CGC ='" + cgc + "'";
                        SqlCommand cmdUpdateCliente = new SqlCommand(queryUpdateCliente, cn.conectar());
                        cmdUpdateCliente.ExecuteNonQuery();

                        string queryUpdateEndereco = "update ENDERECO set END_CEP = '" + cep + "', END_ENDERECO = '" + endereco + "', END_NUMERO = '" + numero + "', END_COMPLEMENTO = '" + complemento + "', END_BAIRRO = '" + bairro + "', END_ESTADO = '" + estado + "', END_CIDADE = '" + cidade + "' where END_CGC ='" + cgc + "'";
                        SqlCommand cmdUpdateEndereco = new SqlCommand(queryUpdateEndereco, cn.conectar());
                        cmdUpdateEndereco.ExecuteNonQuery();

                        cn.desconectar();

                        MessageBox.Show("Cliente alterado com sucesso!", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        concluiuTransacao = true;



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao alterar Cliente\n" + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Informe um CPF válido!", "CPF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void excluirCliente(string cgc)
        {
            if (cgc == "")
                MessageBox.Show("Informe um CPF!", "CPF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (cgc != "")
                {

                    try
                    {
                        ConexaoBanco cn = new ConexaoBanco();
                        string queryExcluirCliente = "delete from CLIENTES where CLI_CGC = '" + cgc + "'";
                        SqlCommand cmdExcluirCliente = new SqlCommand(queryExcluirCliente, cn.conectar());
                        cmdExcluirCliente.ExecuteNonQuery();

                        string queryExcluirEndereco = "delete from ENDERECO where END_CGC = '" + cgc + "'";
                        SqlCommand cmdExcluirEndereco = new SqlCommand(queryExcluirEndereco, cn.conectar());
                        cmdExcluirEndereco.ExecuteNonQuery();

                        cn.desconectar();

                        MessageBox.Show("Cliente excluído com sucesso!", "Clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        concluiuTransacao = true;


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao excluir Cliente\n" + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Informe um CPF válido!", "CPF", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
