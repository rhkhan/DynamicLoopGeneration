using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicLoopGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number of rows: ");
            int rowNumber = Convert.ToInt32(Console.ReadLine()); // N=#of task, M=#of programmers
            int[][] jaggedArray = new int[rowNumber][]; // size with number of programmers
            Console.WriteLine("Enter 'start' column index and 'end' column index for each row: ");
            Console.WriteLine("----------------------------------------------------------------");
            for (int i = 0; i < rowNumber; i++)
            {
                Console.WriteLine("start and end column index of row: " + i);
                Console.WriteLine("Start col (space) End Col");
                Console.WriteLine("-------------------------");
                int[] xy = Array.ConvertAll(Console.ReadLine().Split(), Int32.Parse);
                int diff = xy[1] - xy[0]; //3-0=3; column of each particular row

                int[] a = new int[diff + 1]; //size=4;
                int pos = 0;

                for (int j = xy[0]; j <= xy[1]; j++)
                {
                    a[pos] = j; // put 1 to 3 in array as a[0,1,2]=1,2,3
                    pos++;
                }
                jaggedArray[i] = a; // jagged array for different sized columns with values
            }

            int[] arrTrackColIndex = new int[rowNumber];
            for (int i = 0; i < arrTrackColIndex.Length; i++)
                arrTrackColIndex[i] = 0; // Initialize values to track traversed column of each row

            int[] RowcolCel = new int[rowNumber];
            int totalElementCoveredForExit = 1;// count all the elements altogether in the rows

            for (int row = 0; row < RowcolCel.Length; row++)
            {
                RowcolCel[row] = jaggedArray[row].Length; // set diffrent column size of each row
                totalElementCoveredForExit *= RowcolCel[row]; //total number of elements
            }

            Console.WriteLine("looping order by index accross columns of each row: ");
            Console.WriteLine("----------------------------------------------------");
            bool isAllElementcovered = false;
            int ElementCoveredSoFar = 0;

            while (!isAllElementcovered) // if all the elements in nested loops are covered
            {
                ElementCoveredSoFar++;
                //exit condition
                if (ElementCoveredSoFar == totalElementCoveredForExit) // check if total element touches the peek.
                    isAllElementcovered = true;

                for (int k = 0; k < arrTrackColIndex.Length; k++){
                    Console.Write(jaggedArray[k][arrTrackColIndex[k]]+" ");
                }
                Console.WriteLine();

                bool changeRow = true;
                int r = rowNumber - 1;
                while(changeRow && r >= 0)
                {
                    // if current traversed row index is greater than the total number of element of the row
                    // to determine if the number of column index in the row are finished traversing
                    if (++arrTrackColIndex[r] > RowcolCel[r] - 1)
                    {
                        arrTrackColIndex[r] = 0;
                        changeRow = true;
                    }
                    else
                        changeRow = false;
                    r--; // backtrack r to get to the upper row if current row index reaches to the end
                }
            }

            Console.Read();
        }
    }
}
