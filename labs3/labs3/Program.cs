using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace labs7
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            // Console.WriteLine("Введите диапазон значений x, начиная с наименьшего ");
            // int minRange = Convert.ToInt32(Console.ReadLine());

            int minRange = Convert.ToInt32(input[0]);
            Console.WriteLine($"Наименьшое число диапазона :{minRange}");

            // Console.WriteLine("Теперь наибольшое");
            // int maxRange = Convert.ToInt32(Console.ReadLine());

            int maxRange = Convert.ToInt32(input[1]);
            Console.WriteLine($"Наибольшое число диапазона :{maxRange}");
            
            //
            //

            string formula = input[2];
            Console.WriteLine($"Формула: {formula}");

            if (minRange > maxRange)
            {
                Console.WriteLine("неправильно введен диапазон ");
                return;
            }
            int spaceNeed = FindSpaceMaxRange(maxRange);

            Read(spaceNeed, minRange, maxRange);
        }

        static int FindSpaceMaxRange(double maxRange)
        {
            string maxRandeFormula = $" {Convert.ToString(Math.Round(maxRange * maxRange, 0))} ";
            return maxRandeFormula.Length;
        }

        static void Read(int spaceNeed, int minRange, int maxRange)
        {
            string[] tabl = new string[(maxRange - minRange)*4];
            int LinesInTxt = 0;

            ReadTable('|', '-', spaceNeed, LinesInTxt, tabl);
            LinesInTxt++; // сделать с этим что то
            ReadTable('|', "x", "y", spaceNeed, LinesInTxt, tabl);
            LinesInTxt++;

            for (int i = minRange; i <= maxRange; i++)
            {
                ReadTable('|', '-', spaceNeed, LinesInTxt, tabl);
                LinesInTxt++;
                ReadTable('|', $"{i}", $"{i * i}", spaceNeed, LinesInTxt, tabl);
                LinesInTxt++;
            }

            string tabls = "";
            for (int i = 0; i < (maxRange - minRange) * 4; i++)
            {
                tabls += $"{tabl[i]}\n";
            }
            File.WriteAllText("output.txt", tabls);

        }

        static string FindSpace(string num, int spaceNeed)
        {
            string spaceAndNum = $" {num}";
            for (int i = spaceAndNum.Length; i < spaceNeed; i++)
            {
                spaceAndNum += " ";
            }
            return spaceAndNum;
        }

        static void ReadTable(char oneChar, char twoChar, int spaceNeed, int LinesInTxt, string[] tabl)
        {
            string line = "";

            for (int i = 0; i < spaceNeed; i++)
            {
                line += twoChar;
            }


            tabl[LinesInTxt] = $"{oneChar}{line}{oneChar}{line}{oneChar}";
            LinesInTxt++;
            //{oneChar}{line}{oneChar}{line}{oneChar}


            Console.Write(oneChar);

            for (int i = 0; i < spaceNeed; i++)
            {
                Console.Write(twoChar);
            }

            Console.Write(oneChar);

            for (int i = 0; i < spaceNeed; i++)
            {
                Console.Write(twoChar);
            }

            Console.WriteLine(oneChar);


            
        }

        static void ReadTable(char oneChar, string oneNum, string twoNum, int spaceNeed, int LinesInTxt, string[] tabl) // убрать console write
        {
            Console.Write(oneChar);

            Console.Write(FindSpace(oneNum, spaceNeed));

            Console.Write(oneChar);

            Console.Write(FindSpace(twoNum, spaceNeed));

            Console.WriteLine(oneChar);

            tabl[LinesInTxt] = $"{oneChar}{FindSpace(oneNum, spaceNeed)}{oneChar}{FindSpace(twoNum, spaceNeed)}{oneChar}";
            LinesInTxt++;

        }//opend all text
    }
}
