using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Workers
{
    class SudokuFileReader
    {
        public int[,] ReadFile(string filename)
        {
            int[,] sudokuBoard = new int[9, 9];
            try
            {
                var sudokuBoardLines = File.ReadAllLines(filename);
                int row = 0;
                foreach (var sudokuBoardLine in sudokuBoardLines)
                {
                    string[] sudokuBoaardLineElements = sudokuBoardLine.Split('|').Skip(1).Take(9).ToArray();

                    int col = 0;
                    foreach (var sudokuBoaardLineElement in sudokuBoaardLineElements)
                    {
                        sudokuBoard[row, col] = sudokuBoaardLineElement.Equals(" ") ? 0 : Convert.ToInt16(sudokuBoaardLineElement);
                        col++;
                    }

                    row++;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Something went wrong while reading the file: " + ex.Message);
            }
            return sudokuBoard;
        }
    }
}
