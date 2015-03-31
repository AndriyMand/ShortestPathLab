using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LabiryntSpace
{
    class Labirunt
    {
        int[,] labiryntElements;
        int rowsCount;
        int cellsCount;
        const int freeway = 0;
        const int enter = 2;
        const int exit = 3;

        public int[,] LabiryntElements
        {
            get { return labiryntElements; }
        }

        public int RowsCount
        {
            get { return rowsCount; }
        }

        public int CellsCount
        {
            get { return cellsCount; }
        }

        public int Freeway
        {
            get { return freeway; }
        }

        public int Enter
        {
            get { return enter; }
        }

        public int Exit
        {
            get { return exit; }
        }

        public Labirunt(string fileName)
        {
            string[] file = File.ReadAllLines(fileName);

            //Розмір лабіринта
            rowsCount = file.Length; //кількість стовпців
            cellsCount = file[0].Length; //кількість рядків

            labiryntElements = new int[rowsCount, cellsCount]; //лабіринт

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < cellsCount; j++)
                {
                    labiryntElements[i, j] = Convert.ToInt32(Convert.ToString(file[i][j])); //заповнюємо лабіринт даними
                }
            }
        }
    }
}
