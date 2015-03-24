using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LabiryntSpace
{
    delegate void Function(int i, int j);
    public partial interface FunctionsInLabirynt
    {
        void PrintLabirynt();
    }

    public partial interface FunctionsInChecking
    {
        void PrintShortestPath();
        void FindShortestPath();
        void FindEnter(); // Шукає вхід
        void CheckLeft(int i, int j);
        void CheckDown(int i, int j);
        void CheckRight(int i, int j);
        void CheckUp(int i, int j);
        bool CheckExit(int i, int j);
    }
    partial class Labirunt : FunctionsInLabirynt
    {
        protected int[,] labirynt;
        protected int rowsCount;
        protected int cellsCount;
        protected const int freeway = 0;
        protected const int enter = 2;
        protected const int exit = 3;  
    }

    partial class Checking : Labirunt, FunctionsInChecking
    {
        char[,] shortestPath;
        bool[,] wasHere;
        int minLength;
    }

}