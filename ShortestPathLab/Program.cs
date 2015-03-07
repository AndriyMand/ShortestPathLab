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
            file = File.ReadAllLines("E:\\filee6.txt");
            rows_count = file.Length;
            cell_count = file[0].Length;
            labirynt = new char[rows_count, cell_count];
            wasHere = new bool[rows_count, cell_count];
            correctPath = new bool[rows_count, cell_count];
            correctPath1 = new char[rows_count, cell_count];
            n = 0;
            count = 0;
            countOfSteps = new int[100];
            for (int i = 0; i < file.Length; i++)
            {
                for (int j = 0; j < file[0].Length; j++)
                {
                    labirynt[i, j] = Convert.ToChar(file[i][j]);
                    wasHere[i, j] = false;
                    correctPath[i, j] = false;
                }
            }

            for (int i = 0; i < file.Length; i++)
            {
                for (int j = 0; j < file[0].Length; j++)
                {
                    correctPath1[i, j] = ' ';
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

        public void PrintPath()
        {

            count = 0;
            Console.WriteLine();
            Console.WriteLine("Path number " + (n + 1) + ":");
            Console.WriteLine();
            for (int i = 0; i < rows_count; i++)
            {
                for (int j = 0; j < cell_count; j++)
                {
                    Console.Write(correctPath1[i, j]);
                    if (correctPath1[i, j] == '+')
                        count++;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            countOfSteps[n] = count;
            Console.WriteLine("This path has " + count + " steps!");
            n++;
        }

        public void Lengthh()
        {
            int min = countOfSteps[0];
            int indexx = 0;

            for (int i = 0; i < n; i++)
            {
                if (countOfSteps[i] < min)
                {
                    min = countOfSteps[i];
                    indexx = i;
                }
            }
            Console.WriteLine();
            Console.WriteLine("The path number " + (indexx + 1) + " is shortest: length = " + min);

        }

        public void Check() // Шукає вхід
        {
            for (int j = 0; j < cell_count; j++)
            {
                if (labirynt[0, j] == enter) // Коли ми знайшли вхід, ми перевіряємо, куди можна піти
                {
                    Console.WriteLine("Enter found");
                    wasHere[0, j] = true;
                    correctPath[0, j] = true;
                    IfCheckDown(0, j);
                }
            }
        }


        public bool CheckLeft(int i, int j)
        {
            j = j - 1;
            if (labirynt[i, j] == freeway)
                return true;
            return false;
        }

        public bool CheckRight(int i, int j)
        {
            j = j + 1;
            if (labirynt[i, j] == freeway)
                return true;
            return false;
        }

        public bool CheckDown(int i, int j)
        {
            i = i + 1;
            if (labirynt[i, j] == freeway)
                return true;
            return false;
        }

        public bool CheckUp(int i, int j)
        {
            i = i - 1;
            if (labirynt[i, j] == freeway)
                return true;
            return false;
        }

        void SetPluss(int i, int j)
        {
            correctPath1[i, j] = '+';
            correctPath1[i, j - 1] = '3';
            PrintPath();
        }

        public bool CheckExit(int i, int j)
        {
            if (labirynt[i, j - 1] == exit)
            {
                SetPluss(i, j);
                return true;
            }
            if (labirynt[i, j + 1] == exit)
            {
                SetPluss(i, j);
                return true;
            }
            if (labirynt[i - 1, j] == exit)
            {
                SetPluss(i, j);
                return true;
            }
            if (labirynt[i + 1, j] == exit)
            {
                SetPluss(i, j);
                return true;
            }
            return false;
        }


        void IfCheckLeft(int i, int j)
        {
            if (CheckLeft(i, j) && wasHere[i, j - 1] == false)
            {

                correctPath1[i, j] = '+';
                wasHere[i, j] = true;

                j = j - 1;

                if (CheckExit(i, j))
                { }
                else
                {
                    IfCheckUp(i, j);
                    IfCheckLeft(i, j);
                    IfCheckDown(i, j);
                }
                //correctPath1[i, j + 1] = ' ';
            }

            correctPath1[i, j] = ' ';
            wasHere[i, j] = false;
        }

        void IfCheckDown(int i, int j)
        {
            if (CheckDown(i, j) && wasHere[i + 1, j] == false)
            {

                correctPath1[i, j] = '+';
                wasHere[i, j] = true;
                i = i + 1;

                if (CheckExit(i, j))
                { }
                else
                {
                    IfCheckLeft(i, j);
                    IfCheckDown(i, j);
                    IfCheckRight(i, j);
                }
                //correctPath1[i - 1, j] = ' ';
            }

            correctPath1[i, j] = ' ';
            wasHere[i, j] = false;
        }

        void IfCheckRight(int i, int j)
        {
            if (CheckRight(i, j) && wasHere[i, j + 1] == false)
            {

                correctPath1[i, j] = '+';
                wasHere[i, j] = true;
                j = j + 1;

                if (CheckExit(i, j))
                { }
                else
                {
                    IfCheckDown(i, j);
                    IfCheckRight(i, j);
                    IfCheckUp(i, j);
                }
                //correctPath1[i, j - 1] = ' ';
            }

            correctPath1[i, j] = ' ';
            wasHere[i, j] = false;
        }

        void IfCheckUp(int i, int j)
        {
            if (CheckUp(i, j) && wasHere[i - 1, j] == false)
            {

                correctPath1[i, j] = '+';
                wasHere[i, j] = true;
                i = i - 1;

                if (CheckExit(i, j))
                { }
                else
                {
                    IfCheckRight(i, j);
                    IfCheckUp(i, j);
                    IfCheckLeft(i, j);
                }
                //correctPath1[i + 1, j] = ' ';
            }

            correctPath1[i, j] = ' ';
            wasHere[i, j] = false;
        }
    }
}