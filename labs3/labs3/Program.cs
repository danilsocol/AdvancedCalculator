using System;

namespace labs3
{
     class Program
     {
        private static void Main()

        {
            Console.WriteLine("Привет,это программа выводит табличку со значением функции y=x^2. \nВведите диапазон значений x, начиная с наименьшего ");
            string r = Console.ReadLine();

            Console.WriteLine("Теперь наибольшое");
            double Before = Convert.ToInt32(Console.ReadLine());

            

            string ch = Convert.ToString((r + Before));
            int chs = ch.Length;
            int f = 0;

            double From = Convert.ToInt32(r);
                           
            if (From > Before)
            {
                Console.WriteLine("неправильно введен диапазон ");
                return;
            }

            for (double num = From; num <= Before; num++)
            {
                double squ = num * num;

                int chX = num.ToString().Length;
                int whiteSpaceX = (chs - chX);
                string spX = "".PadRight(whiteSpaceX);

                int chY = squ.ToString().Length;
                int whiteSpaceY = (chs - chY);
                string spY = "".PadRight(whiteSpaceY);


                string line = "|" + num + "|" + squ + "|";
                int str = line.Length + whiteSpaceY + whiteSpaceX;

                if (f == 0)
                {
                    Console.WriteLine("|{0}x{0}|{0}y{0}|", spX);
                    Console.WriteLine("".PadRight(str, '-'));
                    f = 1;
                }
                if (num <= Before) 
                {
                    Console.WriteLine("|{0}{1}|{2}{3}|", num, spX, squ, spY);
                    Console.WriteLine("".PadRight(str, '-'));
                }
          
            }

        }
     }
}