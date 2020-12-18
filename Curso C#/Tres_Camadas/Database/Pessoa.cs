using Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Database
{
    public class Pessoa : IPessoa
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }

        public virtual void Gravar()
        {
            var dados = this.Ler();
            dados.Add(this);
            // if (File.Exists(diretorioComArquivo()))
            //{
            // Em verde era a primeira solução usando o \n no fim da string
            //    string conteudo = "nome;telefone;cpf;\n";
            string conteudo = "nome;telefone;cpf;";
            // Em destaque a nova solução usando O streamWriter que grava sempre numa nova linha;
            // o streamWriter se não existir o arquivo, ele o cria
            StreamWriter r = new StreamWriter(diretorioComArquivo());
            r.WriteLine(conteudo);

            foreach (Pessoa b in dados)
            {
                // conteudo += cliente.Nome + ";" + cliente.Telefone + ";" + cliente.CPF + ";\n";
                var linha = b.Nome + ";" + b.Telefone + ";" + b.CPF + ";";
                r.WriteLine(linha);
            }
            r.Close();
            // File.WriteAllText(caminhoBaseClientes(), conteudo);
            //}
        }
        public virtual List<Pessoa> Ler()  /// Ele colocou o tipo como IPessoa
        {
            var dados = new List<Pessoa>();
            if (File.Exists(diretorioComArquivo()))
            {
                {

                    using (StreamReader arquivo = File.OpenText(diretorioComArquivo()))
                    {
                        string linha;
                        int i = 0;
                        while ((linha = arquivo.ReadLine()) != null)
                        {
                            i++;
                            if (i == 1) continue;
                            var baseArquivo = linha.Split(';');
                            /*var cliente = new Cliente();
                            cliente.Nome = clienteArquivo[0];
                            cliente.Telefone = clienteArquivo[1];
                            cliente.CPF= clienteArquivo[2];
                            */
                            //  Cria uma instäncia em runtime de qualquer tipo
                            var dado = (Pessoa)Activator.CreateInstance(this.GetType()); //IPessoa em vez de Pessoa
                            dado.Nome=(baseArquivo[0]);
                            dado.Telefone=(baseArquivo[1]);
                            dado.CPF=(baseArquivo[2]);
                            dados.Add(dado);
                        }
                    }
                }
            }
            return dados;
        }
        internal string diretorioComArquivo()
        {
            return ConfigurationManager.AppSettings["CaminhoArquivos"] + this.GetType().Name + ".txt";
        }
    }
}
