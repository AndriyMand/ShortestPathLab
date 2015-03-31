using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LabiryntSpace
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            try
            {
                Checking lab = new Checking(args[0]); //Створюємо об'єкт типу Checking в якому створюємо об'єкт типу Labirynt
                PrintLabiryntClass printLab = new PrintLabiryntClass(lab); //конструктор цього класу малює лабіринт

                lab.FindEnterAndStartChecking(); //здійснюється пошук шляху

                PrintShortestPathClass shortestPath = new PrintShortestPathClass(lab); //конструктор цього класу малює найкоротший шлях
            }
            catch (IOException exc)
            {
                Console.WriteLine(exc.Message);
                Console.WriteLine("Файл не знайдено або у файлі містяться неправильні дані! \nВзято лабіринт за замовчуванням.");
            }
            

            

            Console.ReadKey();
        }
    }
}
