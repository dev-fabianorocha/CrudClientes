using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace TelaCrudClientes
{
    public class ConexaoBanco
    {
        SqlConnection con = new SqlConnection();

        public ConexaoBanco ()
        {

            List<string> config = new List<string>();
            string arquivoComCaminho = @"C:\system\config.txt";

            if (File.Exists(arquivoComCaminho))
            {
                using (StreamReader arquivo = File.OpenText(arquivoComCaminho))
                {
                    string linha;
                    while ((linha = arquivo.ReadLine()) != null)
                    {
                        config.Add(linha);

                    }
                }
            }

            string servidor = config[0];
            string banco = config[1];
            string usuario = config[2];
            string senha = config[3];

            con.ConnectionString = @"Data Source=" + servidor + ";Initial Catalog=" + banco + " ;User ID=" + usuario + ";Password=" + senha;
        }
            
        public SqlConnection conectar()
        {
            if(con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        public void desconectar()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

    }
}