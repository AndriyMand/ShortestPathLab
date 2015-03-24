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

            Checking lab = new Checking(args[0]);

            //Checking lab = new Checking("data.txt");



            lab.PrintLabirynt();
            lab.FindEnter();
            lab.PrintShortestPath();

            Console.ReadKey();
        }
    }
}
