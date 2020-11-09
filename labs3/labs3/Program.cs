using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace labs7
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("input.txt");

            int minRange = Convert.ToInt32(input[0]);
            Console.WriteLine($"Наименьшое число диапазона :{minRange}");

            int maxRange = Convert.ToInt32(input[1]);
            Console.WriteLine($"Наибольшое число диапазона :{maxRange}");

            string formula = input[2];
            Console.WriteLine($"Формула: {formula}");

            if (minRange > maxRange)
            {
                Console.WriteLine("неправильно введен диапазон ");
                return;
            }
            int spaceNeed = FindSpaceMaxRange(formula);

            Read(spaceNeed, minRange, maxRange, formula);
        }

        static int FindSpaceMaxRange(string formula) 
        {
            string maxRandeFormula = $" {formula} ";
            return maxRandeFormula.Length;
        }

        static void Read(int spaceNeed, int minRange, int maxRange, string formula)
        {
            string[] tabl = new string[(maxRange - minRange) * 4];
            int LinesInTxt = 0;

            ReadTable('|', '-', spaceNeed, LinesInTxt, tabl);
            LinesInTxt++;
            ReadTable('|', "x", $"{formula}", spaceNeed, LinesInTxt, tabl);
            LinesInTxt++;

            for (int i = minRange; i <= maxRange; i++)
            {
                ReadTable('|', '-', spaceNeed, LinesInTxt, tabl);
                LinesInTxt++;
                ReadTable('|', $"{i}", $"{ParseExpression(formula, i)}", spaceNeed, LinesInTxt, tabl);
                LinesInTxt++;
            }


            StreamWriter output = new StreamWriter("E:\\Проекты\\Лабы\\labs3\\labs3\\File\\output.txt");


            for (int i = 0; i < (maxRange - minRange) * 4; i++)
            {
                output.WriteLine($"{tabl[i]}");
            }
            output.Close();


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


            // для вида
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
            //


        }

        static void ReadTable(char oneChar, string oneNum, string twoNum, int spaceNeed, int LinesInTxt, string[] tabl)
        {
            // для вида
            Console.Write(oneChar);

            Console.Write(FindSpace(oneNum, spaceNeed));

            Console.Write(oneChar);

            Console.Write(FindSpace(twoNum, spaceNeed));

            Console.WriteLine(oneChar);
            //

            tabl[LinesInTxt] = $"{oneChar}{FindSpace(oneNum, spaceNeed)}{oneChar}{FindSpace(twoNum, spaceNeed)}{oneChar}";
            LinesInTxt++;

        }

        static string ParseExpression(string formula, int num)
        {
            string example = formula.Replace("x", $"{num}");

            example = DeleteSpace(example);

            List<string> str = new List<string>(Reworking(example));
            //string[] str = Reworking(example);

            int quantityOfActions = CountingAction(str);

            int divisionMultiplication = checkDivisionMultiplication(str, quantityOfActions);
            int additionSubtraction = checkAdditionSubtraction(str, quantityOfActions);

            List<string> operationsLight = new List<string>();
            int numArrOperationsLight = 0;

            List<string> operationsHard = new List<string>();
            int numArrOperationsHard = 0;

            List<string> nums = new List<string>();
            int numArrNums = 0;

            List<string> arrRPN = new List<string>();
            


            // разбиение на цифры и операции
            for (int i = 0; i < str.Count; i++)
            {
                if (OperationsHard(str, i))
                {
                    operationsHard.Add(str[i]);
                    numArrOperationsHard++;
                }
                else if (OperationsLight(str, i))
                {
                    operationsLight.Add(str[i]);
                    numArrOperationsLight++;
                }
               
                else
                {
                    nums.Add(str[i]);
                    numArrNums++;
                }
            }

            numArrOperationsLight = 0;
            numArrOperationsHard = 0;
            numArrNums = 0;
            //List<string> RPN= new List<string>();

            //RPN.Add(nums[0]);

            tempory3(arrRPN, nums, numArrNums, operationsHard, numArrOperationsHard, operationsLight, numArrOperationsLight, str);


            string RPN = "";
            for (int i = 0; i < arrRPN.Count - 1; i++)
            {
                RPN += Convert.ToString($"{arrRPN[i]},");
            }
            RPN += Convert.ToString($"{arrRPN[arrRPN.Count - 1]}");

            return RPN;
        }

        static void tempory3(List<string> arrRPN, List<string> nums, int numArrNums, List<string> operationsHard, int numArrOperationsHard, List<string> operationsLight,int numArrOperationsLight, List<string> str)
        {
            for (int i = 1; i < nums.Count + operationsLight.Count + operationsHard.Count; i += 2)
            {
                
                if (arrRPN.Count == 0)
                {
                    arrRPN.Add(nums[numArrNums]);
                    numArrNums++;
                }

                if (arrRPN.Count == nums.Count + operationsLight.Count + operationsHard.Count)
                    break;

                if (OperationsHard(str, i))
                {
                    tempory1(arrRPN, nums, numArrNums, operationsHard, numArrOperationsHard);
                    numArrNums++;
                    numArrOperationsHard++;
                }
                else
                {
                    arrRPN.Add(nums[numArrNums]);
                    numArrNums++;

                    if (i < nums.Count + operationsLight.Count + operationsHard.Count - 2)
                        if (OperationsHard(str, i + 2))
                        {
                            i += 2;
                            tempory2(arrRPN, nums, numArrNums, operationsHard, numArrOperationsHard, str, i, operationsLight);
                            numArrNums++;
                            numArrOperationsHard++;
                        }

                    arrRPN.Add(operationsLight[numArrOperationsLight]);
                    numArrOperationsLight++;
                }
            }
        }
        static void tempory1(List<string> arrRPN, List<string> nums, int numArrNums, List<string> operationsHard, int numArrOperationsHard)
        {
            arrRPN.Add(nums[numArrNums]);
            numArrNums++;

            arrRPN.Add(operationsHard[numArrOperationsHard]);
            numArrOperationsHard++;
        }

        static void tempory2(List<string> arrRPN, List<string> nums, int numArrNums, List<string> operationsHard, int numArrOperationsHard, List<string> str, int i, List<string> operationsLight)
        {

            arrRPN.Add(nums[numArrNums]);
            numArrNums++;

            arrRPN.Add(operationsHard[numArrOperationsHard]);
            numArrOperationsHard++;

            if (i + 2 < nums.Count + operationsLight.Count + operationsHard.Count && OperationsHard(str, i + 2))
            {
                i += 2;
                tempory2(arrRPN, nums, numArrNums, operationsHard, numArrOperationsHard, str, i, operationsLight);
                numArrNums++;
                numArrOperationsHard++;
            }

        }

        static bool OperationsHard(List<string> str, int i)
        {
            return str[i] == "*" || str[i] == "/";

        }
        static bool OperationsLight(List<string> str, int i)
        {
            return str[i] == "+" || str[i] == "-";
        }

        static int checkDivisionMultiplication(List<string> str, int quantityOfActions)
        {
            int correctActions = 0;

            for (int i = 1; i < quantityOfActions * 2; i += 2)
            {
                if (str[i] == "/" || str[i] == "*")
                    correctActions++;
            }
            return correctActions;
        }
        static int checkAdditionSubtraction(List<string> str, int quantityOfActions)
        {

            int correctActions = 0;

            for (int i = 1; i < quantityOfActions * 2; i += 2)
            {
                if (str[i] == "+" || str[i] == "-")
                    correctActions++;
            }
            return correctActions;
        }

        static string DeleteSpace(string example)
        {
            return Regex.Replace(example, @"\s+", " ");
        }

        static string[] Reworking(string example)
        {
            return example.Split(new char[] { ' ' });
        }

        static int CountingAction(List<string> str)
        {
            int quantityOfNumbers = 0;

            for (int i = 0; i < str.Count; i++)
            {
                if (i % 2 == 0)
                    quantityOfNumbers++;
            }
            return str.Count - quantityOfNumbers;
        }

    }
}