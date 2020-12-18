using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
namespace DatabaseTXT
{
    public  class Base : IPessoa
    {
        public string Nome;
        public string Telefone;
        public string CPF;
        private string sobrenome;

        public Base(string nome, string telefone, string cpf)
        {
            this.Nome = nome;
            this.Telefone = telefone;
            this.CPF = cpf;
        }
        public Base()
        {
    
        }
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

                foreach (Base b in dados)
                {
                    // conteudo += cliente.Nome + ";" + cliente.Telefone + ";" + cliente.CPF + ";\n";
                    var linha = b.Nome + ";" + b.Telefone + ";" + b.CPF + ";";
                    r.WriteLine(linha);
                }
                r.Close();
                // File.WriteAllText(caminhoBaseClientes(), conteudo);
            //}
        }
        internal string diretorioComArquivo()
        {
            return ConfigurationManager.AppSettings["CaminhoArquivos"] + this.GetType().Name + ".txt";
        }
        public virtual List<Base> Ler()  /// Ele colocou o tipo como IPessoa
        {
            var dados = new List<Base>();
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
                            var dado = (Base)Activator.CreateInstance(this.GetType()); //IPessoa em vez de base
                            dado.SetNome(baseArquivo[0]);
                            dado.SetTelefone(baseArquivo[1]);
                            dado.SetCPF(baseArquivo [2]);
                            dados.Add(dado);
                        }
                    }
                }
            }
            return dados;
        }

       /*   public virtual void Olhar()
        {
            Console.WriteLine("O cliente " + this.Nome + " " + this.sobrenome + " está olhando para mim");
        }*/
      public virtual void Olhar()
        {
             Base c = new Base();
             c.CalcularUmMaisDois();
           // int resulatado = this.CalcularUmMaisDois();
            Console.WriteLine("O usuário " + this.Nome + " está olhando para mim");
            Console.WriteLine("=================================================");
            Console.WriteLine("O método original");
           // base.Olhar();
        }
        protected int CalcularUmMaisDois()
        {
            return 1 + 2;
        }

        public void SetNome(string nome)
        {
            this.Nome=nome;
        }

        public void SetTelefone(string telefone)
        {
            this.Telefone=telefone;
        }

        public void SetCPF(string cpf)
        {
            this.CPF=cpf;
        }
    }
}
