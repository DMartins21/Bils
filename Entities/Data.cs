using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using Microsoft.VisualBasic;
using System.Security.Cryptography.X509Certificates;
using Plans.Entities;
using Plans.Entities.Enuns;

namespace Plans.Entities
{
    internal class Data
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public Status Status { get; set; }
        public decimal Total { get; set; }

        List<Data> dataList = new();

        public CultureInfo culture = CultureInfo.CreateSpecificCulture("pt-BR");

        public Data(string nome, decimal valor, DateTime vencimento)
        {
            Nome = nome;
            Valor = valor;
            Vencimento = vencimento;
        }

        public Data(string nome, decimal valor, DateTime vencimento, Status status)
        {
            Nome = nome;
            Valor = valor;
            Vencimento = vencimento;
            Status = status;
        }

        public Data() { }

        public void AdicionarBoleto()
        {
            Console.Write("Quantos boletos serão adicionados?");
            int n = int.Parse(Console.ReadLine());

            for(int i = 1; i <= n; i++)
            {
                Console.Write("Nome:");
                string? nome = Console.ReadLine();
                Console.Write("Valor:");
                decimal valor = decimal.Parse(Console.ReadLine(),culture);
                Console.Write("Vencimento:");
                DateTime vencimento = DateTime.Parse(Console.ReadLine(),culture);
                Status status = Status.A_pagar;
                Console.WriteLine();
                dataList.Add(new(nome, valor, vencimento, status));
            }
        }

        public void BoletoPago()
        {
            Console.WriteLine("Quantos foram pagos?");
            int n = int.Parse(Console.ReadLine());

            for(int i = 1; i <= n; i++)
            {
                Console.Write("Digite o nome do boleto pago:");
                var nome = Console.ReadLine();

                var result = from x in dataList where x.Nome.Equals(nome) select x;

                foreach(var itens in result)
                {
                    itens.Status = Status.Pago;
                }
            }
        }

        public void RemoverBoleto()
        {
            Console.WriteLine("Quantos boletos serão removidos:");
            int n = int.Parse(Console.ReadLine());

            for(int i = 1; i <= n; i++)
            {
                Console.Write("Nome:");
                string? nome = Console.ReadLine();

                var result = from x in dataList.ToList() where x.Nome.Equals(nome) select x;
                foreach (var item in result)
                {
                    dataList.Remove(item);
                }
            }
        }

        public void BuscarBoleto()
        {
            Console.WriteLine("Todos os Boletos para serem pagos:");

            var itens = from x in dataList orderby x.Valor select  (x.Nome, 
                        x.Valor.ToString("C2",culture),
                        x.Vencimento, x.Status);
            
            foreach(var item in itens)
            {
                Console.WriteLine(item);
            }
            Total = dataList.Sum(x => x.Valor);
            Console.WriteLine($"Valor Total: {Total.ToString("C2",culture)}");
        }

        public void Salvar()
        {
            Console.Write("Digite o caminho do arquivo:");
            string? caminho = Console.ReadLine();


            StreamWriter sw = new(caminho);
            foreach(var itens in dataList)
            {
                var item = $"{itens.Nome} R${itens.Valor} {itens.Vencimento.ToString("dd/MM/yyyy")} {itens.Status}";
                sw.WriteLine(item);
            }
            sw.Close();
        }

        public void Abrir()
        {
            Console.Write("Digite o caminho do arquivo salvo:");
            string? caminho = Console.ReadLine();
            using (StreamReader sr = File.OpenText(caminho))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(' ');
                    string nome = line[0];
                    decimal valor = decimal.Parse(line[1].Trim('R','$'),culture);
                    DateTime vencimento = DateTime.Parse(line[2].ToString(culture));
                    Status status = Enum.Parse<Status>(line[3]);
                    dataList.Add(new(nome, valor,vencimento,status));
                    Total = dataList.Sum(x => x.Valor);
                }
            }
        }
    }
}
