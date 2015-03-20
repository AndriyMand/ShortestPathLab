using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LabiryntSpace
{
    delegate void Function(int i, int j);
    //public partial interface ISeries
    //{
    //    void PrintLabirynt();
    //    void PrintShortestPath();
    //    void FindShortestPath();
    //    void FindEnter(); // Шукає вхід
    //    void CheckLeft(int i, int j);
    //    void CheckDown(int i, int j);
    //    void CheckRight(int i, int j);
    //    void CheckUp(int i, int j);
    //}
    partial class Labirunt 
    {
        protected char[,] labirynt;
        protected int rowsCount;
        protected int cellsCount;
        protected const char freeway = '0';
        protected const char enter = '2';
        protected const char exit = '3';  
    }

    partial class Checking : Labirunt
    {
        char[,] shortestPath;
        bool[,] wasHere;
        int minLength;
    }

}