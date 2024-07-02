using System;
using System.IO;

class Program
{
    
    static string[] produtos = new string[4];
    static int[] estoque = new int[4];
    static int[,] vendas = new int[4, 30];

    static void Main(string[] args)
    {
        Menu();
    }

    static void ImportarProdutos(string filename)
    {
        try
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                for (int i = 0; i < 4; i++)
                {
                    string[] linha = sr.ReadLine().Split(',');
                    produtos[i] = linha[0];
                    estoque[i] = int.Parse(linha[1]);
                }
            }
            Console.WriteLine("Produtos importados com sucesso.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao importar os produtos: " + e.Message);
        }
    }

    static void RegistrarVenda()
    {
        Console.Write("Número do produto (0-3): ");
        int produtoNum = int.Parse(Console.ReadLine());
        Console.Write("Dia do mês (1-30): ");
        int dia = int.Parse(Console.ReadLine()) - 1;
        Console.Write("Quantidade vendida: ");
        int quantidade = int.Parse(Console.ReadLine());

        if (quantidade <= estoque[produtoNum])
        {
            vendas[produtoNum, dia] += quantidade;
            estoque[produtoNum] -= quantidade;
            Console.WriteLine("Venda registrada com sucesso.");
        }
        else
        {
            Console.WriteLine("Quantidade em estoque insuficiente.");
        }
    }

    static void RelatorioEstoque()
    {
        Console.WriteLine("Estoque Atualizado:");
        for (int i = 0; i < produtos.Length; i++)
        {
            Console.WriteLine($"{produtos[i]}: {estoque[i]}");
        }
    }

    static void RelatorioVendas()
    {
        Console.WriteLine("Relatório de Vendas do Mês:");
        Console.WriteLine("Dia\t" + string.Join("\t", produtos));
        for (int dia = 0; dia < 30; dia++)
        {
            Console.Write($"{dia + 1}\t");
            for (int produto = 0; produto < 4; produto++)
            {
                Console.Write($"{vendas[produto, dia]}\t");
            }
            Console.WriteLine();
        }
    }

    static void GerarArquivoVendas(string filename)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(filename))
            {
                for (int i = 0; i < produtos.Length; i++)
                {
                    int totalVendas = 0;
                    for (int dia = 0; dia < 30; dia++)
                    {
                        totalVendas += vendas[i, dia];
                    }
                    sw.WriteLine($"{produtos[i]},{totalVendas}");
                }
            }
            Console.WriteLine("Arquivo de vendas gerado com sucesso.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao gerar o arquivo de vendas: " + e.Message);
        }
    }

    static void Menu()
    {
        while (true)
        {
            Console.WriteLine("Menu Principal:");
            Console.WriteLine("1 – Importar arquivo de produtos");
            Console.WriteLine("2 – Registrar venda");
            Console.WriteLine("3 – Relatório de vendas");
            Console.WriteLine("4 – Relatório de estoque");
            Console.WriteLine("5 – Criar arquivo de vendas");
            Console.WriteLine("6 - Sair");

            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.Write("Nome do arquivo de produtos: ");
                    string filename = Console.ReadLine();
                    ImportarProdutos(filename);
                    break;
                case 2:
                    RegistrarVenda();
                    break;
                case 3:
                    RelatorioVendas();
                    break;
                case 4:
                    RelatorioEstoque();
                    break;
                case 5:
                    Console.Write("Nome do arquivo para salvar vendas: ");
                    string arquivoVendas = Console.ReadLine();
                    GerarArquivoVendas(arquivoVendas);
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }
}