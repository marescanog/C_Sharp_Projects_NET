using SudokuSolver.Strategies;
using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SudokuMapper sudokuMapper = new SudokuMapper();
                SudokuBoardStateManager sudokuBoardStateManager = new SudokuBoardStateManager();
                SudokuSolverEngine sudokuSolverEngine = new SudokuSolverEngine(sudokuBoardStateManager, sudokuMapper);
                SudokuFileReader sudokuFileReader = new SudokuFileReader();
                SudokuBoardDisplayer sudokuBoardDisplayer = new SudokuBoardDisplayer();

                Console.WriteLine("Please enter the filename containing the Sudoku Puzzle");
                var filename = Console.ReadLine();

                var sudokuBoard = sudokuFileReader.ReadFile(filename);

                sudokuBoardDisplayer.Display("Initial State", sudokuBoard);

                bool isSudokuSolved = sudokuSolverEngine.Solve(sudokuBoard);

                sudokuBoardDisplayer.Display("Final State", sudokuBoard);

                Console.WriteLine(isSudokuSolved 
                    ? "You have sucessfully solved this sudoku puzzle"
                    : "Unofortunately current algorithm(s) were not enough to solve the sudoku Puzzle!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("{0}:{1},", "Sudoku Puzzle cannot be solved because there was an error", ex.Message);
            }
        }
    }
}
