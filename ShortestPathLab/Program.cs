using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestSpace
{

    partial class Labirunt
    {
        delegate bool CheckIf(int i, int j);
        delegate void Ck(int i, int j);

        public Labirunt()
        {
            file = File.ReadAllLines("filee5.txt");
            rows_count = file.Length;
            cell_count = file[0].Length;
            labirynt = new char[rows_count, cell_count];
            wasHere = new bool[rows_count, cell_count];
            shortestPath = new char[rows_count, cell_count];
            correctPath1 = new char[rows_count, cell_count];
            n = 0;
            countOfSteps = new int[100];
            for (int i = 0; i < file.Length; i++)
            {
                for (int j = 0; j < file[0].Length; j++)
                {
                    labirynt[i, j] = Convert.ToChar(file[i][j]);
                    wasHere[i, j] = false;
                }
            }
            for (int i = 0; i < file.Length; i++)
            {
                for (int j = 0; j < file[0].Length; j++)
                {
                    correctPath1[i, j] = '.';
                    shortestPath[i, j] = '.';
                }
            }
        }

        public void PrintLabirynt()
        {
            Console.WriteLine();
            for (int i = 0; i < rows_count; i++)
            {
                for (int j = 0; j < cell_count; j++)
                {
                    Console.Write(labirynt[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PrintShortestPath()
        {
            int pathLangth = 0;
            Console.WriteLine("\n\n\n");
            for (int i = 0; i < rows_count; i++)
            {
                for (int j = 0; j < cell_count; j++)
                {
                    Console.Write(shortestPath[i, j]);
                    if (shortestPath[i, j] == '+')
                        pathLangth++;
                }
                Console.WriteLine();
            }
            Console.WriteLine("This path has " + pathLangth + " steps and it is shortest");
        }

        public void PrintPathes()
        {
            int count = 0;
            for (int i = 0; i < rows_count; i++)
            {
                for (int j = 0; j < cell_count; j++)
                {
                    //Console.Write(correctPath1[i, j]);
                    if (correctPath1[i, j] == '+')
                        count++;
                }
                //Console.WriteLine();
            }
            //Console.WriteLine();
            countOfSteps[n] = count;

            int min = countOfSteps[0];
            int indexx = 0;
            if (n != 0)
            {
                if (countOfSteps[n] < min)
                {
                    min = countOfSteps[n];
                    indexx = n;
                    for (int i = 0; i < rows_count; i++)
                    {
                        for (int j = 0; j < cell_count; j++)
                        {
                            shortestPath[i, j] = correctPath1[i, j];
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < rows_count; i++)
                {
                    for (int j = 0; j < cell_count; j++)
                    {
                        shortestPath[i, j] = correctPath1[i, j];
                    }
                }
            }
            Console.WriteLine("Path number " + (n+1) + " has " + count + " steps!");
            n++;
        }

        public void Check() // Шукає вхід
        {
            for (int i = 0; i < rows_count; i++)
            {
                for (int j = 0; j < cell_count; j++)
                {
                    if (labirynt[i, j] == enter) // Коли ми знайшли вхід, ми перевіряємо, куди можна піти
                    {
                        wasHere[i, j] = true;
                        CheckDown(i, j);
                    }
                }
            }

        }

         public bool CheckExit(int i, int j)
        {
            if (labirynt[i, j - 1] == exit)
            {
                correctPath1[i, j] = '+';
                correctPath1[i, j - 1] = '+';
                PrintPathes();
                return true;
            }
            if (labirynt[i, j + 1] == exit)
            {
                correctPath1[i, j] = '+';
                correctPath1[i, j + 1] = '+';
                PrintPathes();
                return true;
            }
            if (labirynt[i - 1, j] == exit)
            {
                correctPath1[i, j] = '+';
                correctPath1[i - 1, j] = '+';
                PrintPathes();
                return true;
            }
            if (labirynt[i + 1, j] == exit)
            {
                correctPath1[i, j] = '+';
                correctPath1[i + 1, j] = '+';
                PrintPathes();
                return true;
            }
            return false;
        }

        void GeneralCheck(int i, int j, int i1, int j1, Ck Func1, Ck Func2, Ck Func3)
        {
            if (labirynt[i1, j1] == freeway && wasHere[i1, j1] == false)
            {
                correctPath1[i, j] = '+';
                wasHere[i, j] = true;

                if (CheckExit(i1, j1) != true)
                {
                    Func1(i1, j1);
                    Func2(i1, j1);
                    Func3(i1, j1);
                }
                correctPath1[i1, j1] = ' ';
                wasHere[i1, j1] = false;
            }
            correctPath1[i, j] = ' ';
            wasHere[i, j] = false;
        }

        void CheckLeft(int i, int j)
        {
            int j1 = j - 1;
            GeneralCheck(i, j, i, j1, CheckUp, CheckLeft, CheckDown);
        }

        void CheckDown(int i, int j)
        {
            int i1 = i + 1;
            GeneralCheck(i, j, i1, j, CheckLeft, CheckDown, CheckRight);
        }

        void CheckRight(int i, int j)
        {
            int j1 = j + 1;
            GeneralCheck(i, j, i, j1, CheckDown, CheckRight, CheckUp);
        }

        void CheckUp(int i, int j)
        {
            int i1 = i - 1;
            GeneralCheck(i, j, i1, j, CheckRight, CheckUp, CheckLeft);
        }
    }
}