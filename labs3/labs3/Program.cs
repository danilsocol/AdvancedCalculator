using System;
using System.Runtime.CompilerServices;

namespace labs7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите диапазон значений x, начиная с наименьшего ");
            int minRange = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Теперь наибольшое");
            int maxRange = Convert.ToInt32(Console.ReadLine());

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
            ReadTable('|', '-', spaceNeed);
            ReadTable('|', "x", "y", spaceNeed);

            for (int i = minRange; i <= maxRange; i++)
            {
                ReadTable('|', '-', spaceNeed);
                ReadTable('|', $"{i}", $"{i * i}", spaceNeed);
            }

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

        static void ReadTable(char oneChar, char twoChar, int spaceNeed)
        {
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

        static void ReadTable(char oneChar, string oneNum, string twoNum, int spaceNeed)
        {
            Console.Write(oneChar);

            Console.Write(FindSpace(oneNum, spaceNeed));

            Console.Write(oneChar);

            Console.Write(FindSpace(twoNum, spaceNeed));

            Console.WriteLine(oneChar);
        }
    }
}
