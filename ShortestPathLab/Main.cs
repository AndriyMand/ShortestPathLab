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

            FileStream fin;
            try
            {
                fin = new FileStream(args[0], FileMode.Open);
            }
            catch (IOException exc)
            {
                Console.WriteLine("Помилка! Не вдається вiдкрити файл!");
                Console.WriteLine(exc.Message);
                return;
            }

            Checking lab = new Checking(args[0]);

            //Checking lab = new Checking("data.txt");



            lab.PrintLabirynt();
            lab.FindEnter();
            lab.PrintShortestPath();

            Console.ReadKey();
        }
    }
}
