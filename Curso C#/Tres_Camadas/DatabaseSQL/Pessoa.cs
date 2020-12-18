﻿using Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DatabaseSQL
{
    public class Pessoa : IPessoa
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        

        public void Gravar()
        {

            string connectionString = ConfigurationManager.AppSettings["sqlConnection"];
            //string connectionString = @"Data Source=DESKTOP-BM0U12B\SQLEXPRESS;Initial Catalog=TresCamadas;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(
               connectionString))
            {

                // string queryString = "INSERT INTO "+this.GetType().BaseType.Name+"s (nome,telefone,cpf) VALUES(@nome,@telefone,@cpf);";
                string queryString = "INSERT INTO " + this.GetType().Name + "s (nome,telefone,cpf) VALUES(@nome,@telefone,@cpf);";
                SqlCommand command = new SqlCommand(queryString, connection);

                var parametro = new SqlParameter();
                parametro.ParameterName = "@nome";
                parametro.Value = this.Nome;
                command.Parameters.Add(parametro);

                parametro = new SqlParameter();
                parametro.ParameterName = "@telefone";
                parametro.Value = this.Telefone;
                command.Parameters.Add(parametro);

                parametro = new SqlParameter();
                parametro.ParameterName = "@cpf";
                parametro.Value = this.CPF;
                command.Parameters.Add(parametro);

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
