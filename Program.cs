// See https://aka.ms/new-console-template for more information
using System;
using System.Globalization;
using Plans.Entities;

bool menu = true;
Data data = new();

while (menu)
{
    Console.WriteLine("Bem Vindo!");
    Console.WriteLine("Escolha as Opções abaixo:");
    Console.Write("1 - Abrir Arquivo com Boletos;\n 2 - Adicionar Novo Boleto;\n 3 - Exibir Boletos;\n 4 - Remover Boleto; \n 5 - Salvar em Arquivo; \n 6 - Mudar Para Pago;\n 7 - Encerrar; ");
    switch (Console.ReadLine())
    {
        case "1":
            data.Abrir();
            break;
        case "2":
            data.AdicionarBoleto();
            break;
        case "3":
            data.BuscarBoleto();
            break;
        case "4":
            data.RemoverBoleto();
            break;
        case "5":
            data.Salvar();
                break;
        case "6":
            data.BoletoPago();
            break;
        case "7":
            menu = false;
            break;
        default:
            Console.WriteLine("Opção Invalida!");
            break;
    }
    Console.Write("Aperte um botão para continuar:");
    Console.ReadKey();
    Console.Clear();
}
Console.WriteLine("Programa Encerrado!");