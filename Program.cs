using System;
using System.Collections.Generic;

namespace Bank
{
    class Program
    {
        static List<Conta> listContas = new List<Conta>();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarContas();
                        break;
                    case "2":
                        InserirConta();
                        break;
                    case "3":
                        Transferir();
                        break;
                    case "4":
                        Sacar();
                        break;
                    case "5":
                        Depositar();
                        break;
                    case "C":
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }

				opcaoUsuario = ObterOpcaoUsuario();
			}
			
			Console.WriteLine("Obrigado por utilizar nossos serviços.");
		}

        private static void Depositar()
        {
            Console.WriteLine("*** Depositar ***");
            Console.Write("Digite o número da conta: ");
			int indiceConta = int.Parse(Console.ReadLine());

			Console.Write("Digite o valor a ser depositado: ");
			double valorDeposito = double.Parse(Console.ReadLine());

            listContas[indiceConta].Depositar(valorDeposito);
        }

        private static void Sacar()
        {
            Console.WriteLine("*** Sacar ***");
            Console.Write("Digite o número da conta: ");
			int indiceConta = int.Parse(Console.ReadLine());

			Console.Write("Digite o valor a ser sacado: ");
			double valorSaque = double.Parse(Console.ReadLine());

            listContas[indiceConta].Sacar(valorSaque);
        }

        private static void Transferir()
        {
            Console.WriteLine("*** Transferir ***");
            Console.Write("Digite o número da conta de origem: ");
			int indiceContaOrigem = int.Parse(Console.ReadLine());

            Console.Write("Digite o número da conta de destino: ");
			int indiceContaDestino = int.Parse(Console.ReadLine());

			Console.Write("Digite o valor a ser transferido: ");
			double valorTransferencia = double.Parse(Console.ReadLine());

            listContas[indiceContaOrigem].Transferir(valorTransferencia, listContas[indiceContaDestino]);
        }

        private static void ListarContas()
        {
            Console.WriteLine("*** Listar ***");

			if (listContas.Count == 0)
			{
				Console.WriteLine("Nenhuma conta cadastrada.");
				return;
			}

			for (int i = 0; i < listContas.Count; i++)
			{
				Conta conta = listContas[i];
				Console.Write("#{0} - ", i);
				Console.WriteLine(conta);
			}
        }

        private static void InserirConta()
        {
            int entradaTipoConta;
            double entradaSaldo, entradaCredito;
            bool check = true;

            Console.WriteLine("** Inserir ***");

			WriteTipoConta();
            do {
                if (!int.TryParse(Console.ReadLine(), out entradaTipoConta)) {
                    Console.WriteLine("Opção inválida");
                    continue;
                }
                
				check = (entradaTipoConta < 1) | (entradaTipoConta > Enum.GetValues(typeof(TipoConta)).Length);
				if (check) {
					Console.WriteLine("Opção inválida. Por favor, entre com o valor dentro da faixa.");
				}
			} while (check);

			Console.Write("Digite o Nome do Cliente: ");
			string entradaNome = Console.ReadLine();

			Console.Write("Digite o saldo inicial: ");
            
            if (!Double.TryParse(Console.ReadLine(), out entradaSaldo)) {
                Console.WriteLine("Valor inválido");
                return;
            }

			Console.Write("Digite o crédito: ");
			
            if (!Double.TryParse(Console.ReadLine(), out entradaCredito)) {
                Console.WriteLine("Valor inválido");
                return;
            }

			Conta novaConta = new Conta(tipoConta: (TipoConta)entradaTipoConta,
										saldo: entradaSaldo,
										credito: entradaCredito,
										nome: entradaNome);

			listContas.Add(novaConta);
        }

        private static void WriteTipoConta()
        {
            foreach (int i in Enum.GetValues(typeof(TipoConta)))
            {
                Console.Write("{0}:{1}  ", i, Enum.GetName(typeof(TipoConta), i));
            }
            Console.WriteLine();
            Console.WriteLine("Digite o tipo de conta: ");
        }

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("*** Bank ***");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar");
			Console.WriteLine("2- Inserir");
			Console.WriteLine("3- Transferir");
			Console.WriteLine("4- Sacar");
			Console.WriteLine("5- Depositar");
            Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}
