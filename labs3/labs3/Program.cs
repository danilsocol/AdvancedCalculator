using System;

namespace labs3
{
     class Program
     {


        private static void Main()
        
        {
            Console.WriteLine("Привет,это программа выводит табличку с х и значением функции y=x^2. \nВведите диапазон значений x, начиная с наименьшего ");
            int From = Convert.ToInt32(Console.ReadLine());
            int Before = Convert.ToInt32(Console.ReadLine());
            if (From> Before)
            {
                return;
            }
            Console.WriteLine($"X\tY");
            while (From <= Before ) 
            {
                Console.WriteLine($"{From}\t{From * From}");
                From++;
                
            }
            
                
            
        }
     }
}