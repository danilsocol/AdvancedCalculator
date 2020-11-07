using System;
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

        static int FindSpaceMaxRange(string formula)   // для разных столбцов разное кол-во пробелов
        {
            string maxRandeFormula = $" {formula} ";
            return maxRandeFormula.Length;
        }

        static void Read(int spaceNeed, int minRange, int maxRange,string formula)
        {
            string[] tabl = new string[(maxRange - minRange)*4];
            int LinesInTxt = 0;

            ReadTable('|', '-', spaceNeed, LinesInTxt, tabl);
            LinesInTxt++; // сделать с этим что то
            ReadTable('|', "x", $"{formula}", spaceNeed, LinesInTxt, tabl);
            LinesInTxt++;

            for (int i = minRange; i <= maxRange; i++)
            {
                ReadTable('|', '-', spaceNeed, LinesInTxt, tabl);
                LinesInTxt++;
                ReadTable('|', $"{i}", $"{ParseExpression(formula,i)}", spaceNeed, LinesInTxt, tabl);
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

        static void ReadTable(char oneChar, string oneNum, string twoNum, int spaceNeed, int LinesInTxt, string[] tabl) // убрать console write
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

        static string ParseExpression(string formula,int num)
        {
            string example = formula.Replace("x",$"{num}");

            example = DeleteSpace(example);

            string[] str = Reworking(example);

            int quantityOfActions = CountingAction(str);

            int divisionMultiplication = checkDivisionMultiplication(str, quantityOfActions);
            int additionSubtraction = checkAdditionSubtraction(str, quantityOfActions);

            string[] operationsLight = new string[additionSubtraction];
            int numArrOperationsLight = 0;

            string[] operationsHard = new string[divisionMultiplication];
            int numArrOperationsHard = 0;

            string[] nums =new string[str.Length -(divisionMultiplication + additionSubtraction)];
            int numArrNums = 0;

            string[] arrRPN = new string[str.Length];
            int numArrRPN = 0;


            // разбиение на цифры и операции
            for (int i = 0; i<str.Length; i++)
            {
                if (OperationsHard(str, i))
                {
                    operationsHard[numArrOperationsHard] = str[i];
                    numArrOperationsHard++;
                }
                else if (OperationsLight(str, i))
                {
                    operationsLight[numArrOperationsLight] = str[i];
                    numArrOperationsLight++;
                }
                else
                {
                    nums[numArrNums] = str[i];
                    numArrNums++;
                }
            }

            numArrOperationsLight = 0;
            numArrOperationsHard = 0;
            numArrNums = 0;

            arrRPN[numArrRPN] = nums[numArrNums];
            arrRPN[numArrRPN + 1] = nums[numArrNums + 1];
            numArrNums += 2;
            numArrRPN += 2;

            for (int i = 2; i < arrRPN.Length; i++) // больше проверки (не всегда работает!)
            {

                if(OperationsHard(str, i))
                {
                    arrRPN[numArrRPN] = operationsHard[numArrOperationsHard];
                    numArrOperationsHard++;
                    numArrRPN ++;
                }
                else if (OperationsLight(str, i))
                {
                    //RPN[numArrRPN] = nums[numArrNums];
                    //numArrRPN++;
                    //numArrNums++;

                    arrRPN[numArrRPN] = operationsLight[numArrOperationsLight];
                    numArrOperationsLight++;
                    numArrRPN++;
                }
                else if(operationsHard.Length == numArrOperationsHard)
                {
                    arrRPN[numArrRPN] = operationsLight[numArrOperationsLight];
                    numArrOperationsLight++;
                    numArrRPN++;
                }
                else
                {
                    arrRPN[numArrRPN] = nums[numArrNums];
                    numArrRPN++;
                    numArrNums++;

                    //RPN[numArrRPN] = operationsLight[numArrOperationsLight];
                    //numArrOperationsLight++;
                    //numArrRPN++;
                }

            }
            string RPN = "";
            for(int i =0;i<arrRPN.Length-1;i++)
            {
                RPN += $"{arrRPN[i]},";
            }
            RPN += $"{arrRPN[arrRPN.Length-1]}";

            return RPN;

        }

        static bool OperationsHard(string[] str, int i)
        {
            return str[i] == "*" || str[i] == "/";
        }


        static bool OperationsLight(string[] str, int i)
        {
            return str[i] == "+" || str[i] == "-";
        }
        

        //static bool NoOperations(string[] str, int i)
        //{
        //    return str[i] != "+" || str[i] != "-" || str[i] != "*" || str[i] != "/";
        //}

        static int checkDivisionMultiplication(string[] str, int quantityOfActions)
        {
            int correctActions = 0;

            for (int i = 1; i < quantityOfActions * 2; i += 2)
            {
                if (str[i] == "/" || str[i] == "*")
                    correctActions++;
            }
            return correctActions;
        }
        static int checkAdditionSubtraction(string[] str, int quantityOfActions)
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

        static int CountingAction(string[] str)
        {
            int quantityOfNumbers = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (i % 2 == 0)
                    quantityOfNumbers++;
            }
            return str.Length - quantityOfNumbers;
        }

    }
}
