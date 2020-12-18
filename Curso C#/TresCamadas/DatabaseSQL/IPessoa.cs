using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSQL
{
    public interface IPessoa
    {
        string Nome { get; set; }
        string Telefone { get; set; }
        string CPF { get; set; }
        void Gravar();
    }
}
