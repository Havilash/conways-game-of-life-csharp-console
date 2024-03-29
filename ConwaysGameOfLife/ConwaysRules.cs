﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConwaysGameOfLife
{
    class ConwaysRules
    {
        public bool[,] gridBool = new bool[100, 37];
        private bool[,] tempGridBool;
        private int[] drawerCellPos = { 0, 0 };


        public void StartGame()
        {
            Console.SetWindowSize(Console.LargestWindowWidth - 10, Console.LargestWindowHeight - 5);



            while (true)
            {
                Console.Clear();
                Instructions();
                tempGridBool = (bool[,])gridBool.Clone();


                DrawField();

                for (int i = 0; i < gridBool.GetLength(1); i++)
                {
                    for (int j = 0; j < gridBool.GetLength(0); j++)
                    {
                        OverUnderPopulation(j, i);
                        Alive3Neighbours(j, i);
                    }
                }

                //Next Generation
                DrawCell();
            }
        }
        private void Instructions()
        {
            Console.WriteLine("--- Conways Game Of Life ---");
            Console.WriteLine("With the red Cursor you can draw the Cells. Navigate to a position and press F. To delete a Cell press D.");
            Console.WriteLine("To start the next Generation press ENTER.");
            Console.WriteLine("------");

        }
        private void DrawCell()
        {
            while (true)
            {
                var key = Console.ReadKey(false).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        drawerCellPos[1]--;
                        break;
                    case ConsoleKey.DownArrow:
                        drawerCellPos[1]++;
                        break;
                    case ConsoleKey.LeftArrow:
                        drawerCellPos[0]--;
                        break;
                    case ConsoleKey.RightArrow:
                        drawerCellPos[0]++;
                        break;
                    case ConsoleKey.Enter:
                        return;
                        break;
                    case ConsoleKey.F:
                        gridBool[drawerCellPos[0], drawerCellPos[1]] = true;
                        tempGridBool[drawerCellPos[0], drawerCellPos[1]] = true;
                        break;
                    case ConsoleKey.D:
                        gridBool[drawerCellPos[0], drawerCellPos[1]] = false;
                        tempGridBool[drawerCellPos[0], drawerCellPos[1]] = false;
                        break;
                }

                Console.WriteLine(drawerCellPos[0] + " " + drawerCellPos[1]);
                Console.Clear();
                DrawField();
            }
        }
        private void DrawField()
        {
            for (int i = 0; i < gridBool.GetLength(1); i++)
            {
                for (int j = 0; j < gridBool.GetLength(0); j++)
                {
                    if (tempGridBool[j, i] == true)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    else if ((j, i) == (drawerCellPos[0], drawerCellPos[1]))
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine();
            }
        }
        private int CountNeighbours(int xPos, int yPos)
        {
            int neighbours = 0;

            //Check Around Field
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (xPos + j <= gridBool.GetLength(0) - 1 && yPos + i <= gridBool.GetLength(1) - 1 && xPos + j >= 0 && yPos + i >= 0)
                    {
                        if (tempGridBool[xPos + j, yPos + i] == true)
                        {
                            neighbours++;
                        }
                    }
                }
            }

            return neighbours;
        }
        private void OverUnderPopulation(int xPos, int yPos)
        {
            if ((CountNeighbours(xPos, yPos) > 4 || CountNeighbours(xPos, yPos) < 3) && tempGridBool[xPos, yPos] == true)
            {
                gridBool[xPos, yPos] = false;

            }
        }
        private void Alive3Neighbours(int xPos, int yPos)
        {
            if (CountNeighbours(xPos, yPos) == 3 && tempGridBool[xPos, yPos] == false)
            {
                gridBool[xPos, yPos] = true;
            }
        }
    }
}
