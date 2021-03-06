﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Lab6LS3
{
    class Program
    {

        public class OutToFile : IDisposable
        {
            private StreamWriter fileOutput;
            private TextWriter oldOutput;
            /// <summary>
            /// Create a new object to redirect the output
            /// </summary>
            /// <param name="outFileName">
            /// The name of the file to capture console output
            /// </param>
            public OutToFile(string outFileName)
            {
                oldOutput = Console.Out;
                fileOutput = new StreamWriter(
                new FileStream(outFileName, FileMode.Create)
                );
                fileOutput.AutoFlush = true;
                Console.SetOut(fileOutput);
            }
            // Dispose() is called automatically when the object
            // goes out of scope
            public void Dispose()
            {
                Console.SetOut(oldOutput); // Restore the console output
                fileOutput.Close(); // Done with the file
            }
        }

        public delegate double MojDelegat(int n);

        static double Liniowa(int n) {

            return 2 * 0.03 * n;
        }

        static double Kwadratowa(int n) {

            return n * n;
        }

        static void Suma(int n, MojDelegat F) {

            int[] tab = new int[n];

            Stopwatch stoper = new Stopwatch();
            stoper.Start();

            int suma = 0;
            for (int i = 0; i < n; i++)
            {
                tab[i] = i;
                suma = suma + tab[i];
                // Console.WriteLine(i);
            }

            stoper.Stop();

            Console.WriteLine("n:{0,10}, {1,10}, {2,10}, {3,10}", n, 
                stoper.ElapsedTicks, F(n), stoper.ElapsedTicks/F(n));

        
        }
        static void Main(string[] args)
        {

            // MojDelegat F1 = new MojDelegat(Liniowa);
            MojDelegat F2 = new MojDelegat(Kwadratowa);

            

            //zapis do pliku
            using (new OutToFile("output.txt")) // redirect the output to a text file
            {
                // Tutaj nasz cały kod zawierający informacje, które chcemy przesłać do pliku
                for (int i = 1; i <= 20; i++)
                {
                    Suma(1000 * i, F2);
                }

            } // redirection ends upon exiting the using block

            Console.ReadKey();
        }
    }
}
