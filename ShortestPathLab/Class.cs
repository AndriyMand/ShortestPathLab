using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TestSpace
{

    partial class Labirunt
    {
        string[] file;
        int[] countOfSteps;
        char[,] labirynt;
        char[,] shortestPath;
        char[,] correctPath1;
        bool[,] wasHere;
        int n;

        int rows_count;
        int cell_count;

        char freeway = '0';

        char enter = '2';
        char exit = '3';

    }
}