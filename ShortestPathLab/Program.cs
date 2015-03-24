using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LabiryntSpace
{
    partial class Labirunt : FunctionsInLabirynt
    {
        public Labirunt(string list)
        {
            //string list = "data.txt";
            string[] file = File.ReadAllLines(list);

            //Розмір лабіринта
            rowsCount = file.Length; //кількість стовпців
            cellsCount = file[0].Length; //кількість рядків

            labirynt = new int[rowsCount, cellsCount]; //лабіринт

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < cellsCount; j++)
                {
                    labirynt[i, j] = Convert.ToInt32(Convert.ToString(file[i][j])); //заповнюємо лабіринт даними
                }
            }
        }
        public void PrintLabirynt()  //Малює лабіринт
        {
            Console.WriteLine();
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < cellsCount; j++)
                {
                    Console.Write(labirynt[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    partial class Checking : Labirunt, FunctionsInChecking
    {
        public Checking(string list) : base(list)
        {
            wasHere = new bool[rowsCount, cellsCount]; //масив для перевірки правильного шляху
            shortestPath = new char[rowsCount, cellsCount]; //найкоротший шлях
            minLength = 0;

            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < cellsCount; j++)
                {
                    wasHere[i, j] = false; //на пачатку у нас немає ніякого правильного шляху
                }
            }
        }

        public void PrintShortestPath() //Малює найкоротший шлях, який ми знайшли в функції FindShortestPath()
        {
            Console.WriteLine();
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < cellsCount; j++)
                {
                    Console.Write(shortestPath[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void FindShortestPath()
        {
            //рахуємо довжину шляху і записуємо її в countOfSteps
            //в wasHere зберігається наш шлях
            int countOfSteps = 0;
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < cellsCount; j++)
                {
                    if (wasHere[i, j] == true)
                        countOfSteps++;
                }
            }

            //Кожен наступний шлях порівнюється з попереднім і коротший зберігається в shortestPath:

            if (countOfSteps < minLength || minLength == 0) //Шукає найкоротшу довжину 
            {
                minLength = countOfSteps; // і зберігає її в "minLength" для подальшого порівняння

                for (int i = 0; i < rowsCount; i++)
                {
                    for (int j = 0; j < cellsCount; j++)
                    {

                        if (wasHere[i, j] == true)
                            shortestPath[i, j] = '+';  //плюсами заповнюємо оптимальний шлях
                        else
                            shortestPath[i, j] = Convert.ToChar(labirynt[i, j]); //все інше заповнюємо так як в labirynt
                    }
                }
            }
        }

        public void FindEnter() // ШУКАЄ ВХІД
        {
            for (int i = 0; i < rowsCount; i++)
            {
                for (int j = 0; j < cellsCount; j++)
                {
                    if (labirynt[i, j] == enter) //Якщо знайшли вихід
                    {
                        wasHere[i, j] = true; //то позначаємо, що даний крок правильний
                        if (i == 0) CheckDown(i, j);              //Якщо вхід в лабіринт розташований зверху, то першу перевірку робимо вниз
                        if (i == rowsCount - 1) CheckUp(i, j);    //Якщо вхід в лабіринт розташований знизу,  то першу перевірку робимо вверх
                        if (j == 0) CheckRight(i, j);             //Якщо вхід в лабіринт розташований зліва,  то першу перевірку робимо праворуч
                        if (j == cellsCount - 1) CheckLeft(i, j); //Якщо вхід в лабіринт розташований зправа, то першу перевірку робимо ліворуч
                    }
                }
            }
        }

        public bool CheckExit(int i, int j) //ПЕРЕВІРЯЄ, ЧИ Є ВИХІД
        {
            if (labirynt[i, j] == exit) //Якщо на даному кроці є вихід
            {
                wasHere[i, j] = true; //то позначаємо, що даний крок правильний
                return true;
            }
            return false;
        }


        //Цю функцію викликає фукнція CheckLeft, або CheckDown, або CheckRight, або CheckUp
        //Від того, яка функція викликає залежить, яку сторону перевіряємо
        void GeneralCheck(int i, int j, int i1, int j1, Function Func1, Function Func2, Function Func3) //ПЕРЕВІРЯЄ, ЧИ Є СТІНА
        {
            if (labirynt[i1, j1] == freeway && wasHere[i1, j1] == false) //Якщо в даному напрямку немає стіни і якщо ми там ще не були, то:
            {
                wasHere[i, j] = true; //Позначаємо, що попередній крок пройдений


                // На даному кроці перевіряємо, чи є вихід праворуч, ліворуч, зверху чи знизу від нього
                //          /ЗНИЗУ\                   /ЗВЕРХУ\                 /ПРАВОРУЧ\               /ЛІВОРУЧ\
                if (CheckExit(i1 + 1, j1) || CheckExit(i1 - 1, j1) || CheckExit(i1, j1 + 1) || CheckExit(i1, j1 - 1))
                {
                    wasHere[i1, j1] = true; //Позначаємо, що даний крок пройдений
                    FindShortestPath(); //Записуємо знайдений вихід
                    wasHere[i1, j1] = false; //Після того, як записали, даний крок позначимо як ще непройдений
                }
                else //Якщо виходу не знайшли, то перевіряємо наступні три напрямки
                {
                    Func1(i1, j1);
                    Func2(i1, j1);
                    Func3(i1, j1);
                }
            }
            //Якщо в даному напрямку є стіна, то ми вертаємося назад і позначаємо, що крок ще непройденим
            wasHere[i, j] = false;
        }

        public void CheckLeft(int i, int j) //На даному кроці зміщаємося вліво
        {
            //функції GeneralCheck передадуться (CheckUp, CheckLeft, CheckDown) в (Func1, Func2, Func3) відповідно
            GeneralCheck(i, j, i, j - 1, CheckUp, CheckLeft, CheckDown);
        }

        public void CheckDown(int i, int j)
        {
            GeneralCheck(i, j, i + 1, j, CheckLeft, CheckDown, CheckRight);
        }

        public void CheckRight(int i, int j)
        {
            GeneralCheck(i, j, i, j + 1, CheckDown, CheckRight, CheckUp);
        }

        public void CheckUp(int i, int j)
        {
            GeneralCheck(i, j, i - 1, j, CheckRight, CheckUp, CheckLeft);
        }
    }

}