using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestSpace
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Labirunt lab = new Labirunt();
            lab.PrintLabirynt();
            lab.Check();
            lab.Lengthh();

            Console.ReadKey();
        }
    }
}
