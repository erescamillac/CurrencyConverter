using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CurrencyConverter
{
    /*
     * @author Erick Escamilla Charco
     */
    /*
    * Programa que muestre un Menú
    * Opt 1: "Conversión de Pesos (MXN) a Dólares (USD)"
    * Opt 2: "Conversión de Pesos (MXN) a Euros (EUR)"
    * Opt 3: Salir
    * Solicitar una cantidad en MXN ()
    */
    public class InputReader {
        public InputReader() {
        }

        public byte ReadOption(string inputMsg, string errorMsg) {
            byte option = 0;
            char optAsChar;
            bool successParsing, error = false;

            do {
                if (error) {
                    Console.Error.WriteLine("\n" + errorMsg);
                }
                Console.WriteLine(inputMsg);
                optAsChar = Console.ReadKey().KeyChar;
                successParsing = Byte.TryParse(optAsChar.ToString(), out option);
                if (!successParsing)
                {
                    error = true;
                }
                else {
                    error = false;
                }
            } while (error);

            return option;
        } //--fin: ReadOption

        public double ReadCurrencyAmount(string inputMsg) {
            double currencyAmount = 0.0;
            string cAmountAsStr;
            bool successParsing, error = false;
            do {
                Console.WriteLine(inputMsg);
                cAmountAsStr = Console.ReadLine();
                successParsing = Double.TryParse(cAmountAsStr, out currencyAmount);
                if (!successParsing)
                {
                    error = true;
                    Console.WriteLine($"ENTRADA INVÁLIDA, debe ingresar un VALOR NUMÉRICO ENTRE {Double.MinValue} y {Double.MaxValue}");
                }
                else {
                    error = false;
                }
            } while (error);
            return currencyAmount;
        }

    } //-- fin: InputReader

    public class CurrencyFactors {
        public static double MXNToUSDFactor = 0.049;
        public static double MXNToEURFactor = 0.042;
    } //--fin: CurrencyFactors

    class Program
    {
        static InputReader iReader = new InputReader();
        static double MXNToUSDConversion(double mxnAmount) {
            double usdAmount = 0.0;
            usdAmount = mxnAmount * CurrencyFactors.MXNToUSDFactor;
            return usdAmount;
        }

        static double MXNToEURConversion(double mxnAmount) {
            double eurAmount = 0.0;
            eurAmount = mxnAmount * CurrencyFactors.MXNToEURFactor;
            return eurAmount;
        }
        static void MXNToUSDCase()
        {
            char continueP = 'n';
            double mxnAmount, usdAmount;
            do {
                Console.Clear();
                Console.WriteLine("Conversión de MXN a USD.");
                mxnAmount = iReader.ReadCurrencyAmount("Ingrese el monto en Pesos Mexicanos (MXN) $: ");
                usdAmount = MXNToUSDConversion(mxnAmount);
                Console.WriteLine($"{mxnAmount} MXN equivalen a {usdAmount} USD");
                Console.WriteLine("\t¿Desea realizar otra conversión MXN a USD? [y/n]: ");
                continueP = Console.ReadKey().KeyChar;
            } while (Char.ToLower(continueP).Equals('y'));
        } //-- MXNToUSDCase()

        static void MXNToEURCase() {
            char continueP = 'n';
            double mxnAmount, eurAmount;
            do {
                Console.Clear();
                Console.WriteLine("Conversión de MXN a EUR.");
                mxnAmount = iReader.ReadCurrencyAmount("Ingrese el monto en Pesos Mexicanos (MXN) $: ");
                eurAmount = MXNToEURConversion(mxnAmount);
                Console.WriteLine($"{mxnAmount} MXN equivalen a {eurAmount} EUR");
                Console.WriteLine("\t¿Desea realizar otra conversión MXN a EUR? [y/n]: ");
                continueP = Console.ReadKey().KeyChar;
            } while (Char.ToLower(continueP).Equals('y'));
        } //-- MXNToEURCase
        static void Main(string[] args)
        {
            InputReader iReader = new InputReader();
            byte option = 0;
            bool delay = false;
            do {
                Console.Clear();
                Console.WriteLine("##--Menú--##");
                Console.WriteLine("1. Conversión de Pesos (MXN) a Dólares (USD)");
                Console.WriteLine("2. Conversión de Pesos (MXN) a Euros (EUR)");
                Console.WriteLine("3. Salir");

                option = iReader.ReadOption("\tSeleccione una Opción (1, 2, 3): ", "ENTRADA INVÁLIDA, debe ingresar un ENTERO entre 0 y 255");

                switch (option) {
                    case 1:
                        MXNToUSDCase();
                        delay = false;
                        break;
                    case 2:
                        MXNToEURCase();
                        delay = false;
                        break;
                    case 3:
                        Console.WriteLine("\n\tHasta la próxima, gracias por utilizar SuperCurrencyConverter v1.0");
                        delay = true;
                        break;
                    default:
                        Console.WriteLine("\tOpción INVÁLIDA, las opciones válidas son: 1, 2 o 3");
                        delay = true;
                        break;
                }

                if (delay) {
                    Thread.Sleep(1450);
                }
                
            } while (option != 3);
        }
    }
}
