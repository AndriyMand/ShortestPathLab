using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabiryntSpace
{
    class PrintShortestPathClass
    {
        public PrintShortestPathClass(Checking labirynt)
        {
            Console.WriteLine();
            for (int i = 0; i < labirynt.RectangularLabirynt.RowsCount; i++)
            {
                for (int j = 0; j < labirynt.RectangularLabirynt.CellsCount; j++)
                {
                    Console.Write(labirynt.ShortestPath[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
