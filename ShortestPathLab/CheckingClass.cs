using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LabiryntSpace
{
    delegate void CheckSideFunction(int i, int j);
    class Checking
    {
        char[,] shortestPath; //масив з найкоротший шляхом
        bool[,] wasHere;
        int minLength; //мінімальна довжина шляху

        Labirunt rectangularLabirynt; //Створюємо лабіринт

        public Labirunt RectangularLabirynt
        { get { return rectangularLabirynt; } }

        public char[,] ShortestPath
        {
            get { return shortestPath; }
            set { shortestPath = value; }
        }

        public Checking(string fileName)
        {
            rectangularLabirynt = new Labirunt(fileName); //fileName - шлях до файлу з даними
            wasHere = new bool[rectangularLabirynt.RowsCount, rectangularLabirynt.CellsCount]; //масив для перевірки правильного шляху
            shortestPath = new char[rectangularLabirynt.RowsCount, rectangularLabirynt.CellsCount]; //найкоротший шлях
            minLength = 0;

            for (int i = 0; i < rectangularLabirynt.RowsCount; i++)
                for (int j = 0; j < rectangularLabirynt.CellsCount; j++)
                    wasHere[i, j] = false; //на пачатку у нас немає ніякого правильного шляху
        }

        public void FindShortestPath()
        {
            //рахуємо довжину шляху і записуємо її в countOfSteps
            //в wasHere зберігається наш шлях
            int countOfSteps = 0;
            for (int i = 0; i < rectangularLabirynt.RowsCount; i++)
                for (int j = 0; j < rectangularLabirynt.CellsCount; j++)
                    if (wasHere[i, j] == true)  countOfSteps++;


            //Кожен наступний шлях порівнюється з попереднім і коротший зберігається в shortestPath:

            if (countOfSteps < minLength || minLength == 0) //Шукає найкоротшу довжину 
            {
                minLength = countOfSteps; // і зберігає її в "minLength" для подальшого порівняння

                for (int i = 0; i < rectangularLabirynt.RowsCount; i++)
                    for (int j = 0; j < rectangularLabirynt.CellsCount; j++)
                        if (wasHere[i, j] == true)
                            ShortestPath[i, j] = '+';  //плюсами заповнюємо оптимальний шлях
                        else
                            ShortestPath[i, j] = Convert.ToChar(rectangularLabirynt.LabiryntElements[i, j]); //все інше заповнюємо так як в labirynt

            }
        }

        public void FindEnterAndStartChecking() // ШУКАЄ ВХІД
        {
            for (int i = 0; i < rectangularLabirynt.RowsCount; i++)
                for (int j = 0; j < rectangularLabirynt.CellsCount; j++)
                    if (rectangularLabirynt.LabiryntElements[i, j] == rectangularLabirynt.Enter) //Якщо знайшли вихід
                    {
                        wasHere[i, j] = true; //то позначаємо, що даний крок правильний
                        if (i == 0) CheckDown(i, j);              //Якщо вхід в лабіринт розташований зверху, то першу перевірку робимо вниз
                        if (i == rectangularLabirynt.RowsCount - 1) CheckUp(i, j);    //Якщо вхід в лабіринт розташований знизу,  то першу перевірку робимо вверх
                        if (j == 0) CheckRight(i, j);             //Якщо вхід в лабіринт розташований зліва,  то першу перевірку робимо праворуч
                        if (j == rectangularLabirynt.CellsCount - 1) CheckLeft(i, j); //Якщо вхід в лабіринт розташований зправа, то першу перевірку робимо ліворуч
                    }
        }

        public bool CheckExit(int i, int j) //ПЕРЕВІРЯЄ, ЧИ Є ВИХІД в даному напрямку
        {
            if (rectangularLabirynt.LabiryntElements[i, j] == rectangularLabirynt.Exit) //Якщо на даному кроці є вихід
            {
                wasHere[i, j] = true; //то позначаємо, що даний крок правильний
                return true;
            }
            return false;
        }

        public bool CheckSide(int i, int j)//ПЕРЕВІРЯЄ, ЧИ Є СТІНА в даному напрямку
        {
            // якщо в даному напрямку немає стіни і ми ще там не були
            if (rectangularLabirynt.LabiryntElements[i, j] == rectangularLabirynt.Freeway && wasHere[i, j] == false)
            {
                wasHere[i, j] = true; //Позначаємо, що даний крок пройдений
                return true;
            }
            return false;
        }

        void GeneralCheck(int i, int j) 
        {
            if (CheckSide(i, j)) //Якщо в даному напрямку немає стіни і якщо ми там ще не були, то:
            {
                //на кожному кроці перевіряємо всі сторони
                CheckLeft(i, j);
                CheckDown(i, j);
                CheckRight(i, j);
                CheckUp(i, j);

                wasHere[i, j] = false;
            }
            else
                if (CheckExit(i, j))
                {
                    wasHere[i, j] = true; //Позначаємо, що даний крок пройдений
                    FindShortestPath(); //Записуємо знайдений шлях
                }
        }
        public void CheckLeft(int i, int j) //На даному кроці зміщаємося вліво
        {
            //функції GeneralCheck передадуться координати напрямку
            GeneralCheck(i, j - 1);
        }
        public void CheckDown(int i, int j)
        {
            GeneralCheck(i + 1, j);
        }
        public void CheckRight(int i, int j)
        {
            GeneralCheck(i, j + 1);
        }
        public void CheckUp(int i, int j)
        {
            GeneralCheck(i - 1, j);
        }
    }
}