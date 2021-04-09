using System;
using System.Collections.Generic;

namespace Bank
{
    class Program
    {
        static List<Conta> listContas = new List<Conta>();
        static void Main(string[] args)
        {
            Console.Clear();
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
                        Console.Clear();
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
            
			int indiceConta = LerConta("Digite o número da conta: ");

			double valorDeposito = LerDouble("Digite o valor a ser depositado: ");

            listContas[indiceConta].Depositar(valorDeposito);
        }

        private static void Sacar()
        {
            Console.WriteLine("*** Sacar ***");
            
			int indiceConta = LerConta("Digite o número da conta: ");

			double valorSaque = LerDouble("Digite o valor a ser sacado: ");

            listContas[indiceConta].Sacar(valorSaque);
        }

        private static void Transferir()
        {
            Console.WriteLine("*** Transferir ***");
            
			int indiceContaOrigem = LerConta("Digite o número da conta de origem: ");

			int indiceContaDestino = LerConta("Digite o número da conta de destino: ");

            if (indiceContaOrigem == indiceContaDestino){
                Console.WriteLine("Transferência cancelada, pois a conta origem e destino é a mesma.");
                return;
            }

			double valorTransferencia = LerDouble("Digite o valor a ser transferido: ");

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

            Console.WriteLine("** Inserir ***");

			entradaTipoConta = WriteTipoConta();

			Console.Write("Digite o Nome do Cliente: ");
			string entradaNome = Console.ReadLine();
            
            entradaSaldo = LerDouble("Digite o saldo inicial: ");
            entradaCredito = LerDouble("Digite o crédito: ");
			            
			Conta novaConta = new Conta(tipoConta: (TipoConta)entradaTipoConta,
										saldo: entradaSaldo,
										credito: entradaCredito,
										nome: entradaNome);

			listContas.Add(novaConta);
        }

        private static int LerConta(string s)
        {
            bool check = true;
            int saidaInt;

            do {
                Console.Write(s);
                if (Int32.TryParse(Console.ReadLine(), out saidaInt) 
                    & ((saidaInt >-1) & (saidaInt < listContas.Count))) {
                    check = false;
                } else {
                    Console.WriteLine("Valor inválido");
                }
            } while(check);
            return saidaInt;
        }

        private static double LerDouble(string s)
        {
            bool check = false;
            double saidaDouble;

            do {
                Console.Write(s);
                if (Double.TryParse(Console.ReadLine(), out saidaDouble)) {
                    saidaDouble = Math.Truncate(saidaDouble * 100) / 100;
                } else {
                    Console.WriteLine("Valor inválido");
                    check = true;
                }
            } while(check);
            return saidaDouble;
        }

        private static int WriteTipoConta()
        {
            bool check = true;
            int entradaTipoConta;

            foreach (int i in Enum.GetValues(typeof(TipoConta)))
            {
                Console.Write("{0}:{1}  ", i, Enum.GetName(typeof(TipoConta), i));
            }
            Console.WriteLine();
            
            do {
                Console.Write("Digite o tipo de conta: ");
                if (!int.TryParse(Console.ReadLine(), out entradaTipoConta)) {
                    Console.WriteLine("Opção inválida");
                    continue;
                }
                
				check = (entradaTipoConta < 1) | (entradaTipoConta > Enum.GetValues(typeof(TipoConta)).Length);
				if (check) {
					Console.WriteLine("Opção inválida. Por favor, entre com o valor dentro da faixa.");
				}
			} while (check);
            return entradaTipoConta;
        }

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("*** Bank ***");

			Console.WriteLine("1- Listar");
			Console.WriteLine("2- Inserir");
			Console.WriteLine("3- Transferir");
			Console.WriteLine("4- Sacar");
			Console.WriteLine("5- Depositar");
            Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
            Console.Write("Informe a opção desejada: ");

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}
