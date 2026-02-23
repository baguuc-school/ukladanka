namespace Ukladanka
{
    internal class BoardGenerator
    {
        /// <summary>
        /// Generuje losową planszę gry w postaci dwuwymiarowej tablicy int.
        /// Liczba "0" w tablicy oznacza puste pole.
        /// </summary>
        /// <returns></returns>
        public static int[][] Generate()
        {
            int[] numberPool = new int[]
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15
            };
            int[][] board = [
                [ 0, 0, 0, 0 ],
                [ 0, 0, 0, 0 ],
                [ 0, 0, 0, 0 ],
                [ 0, 0, 0, 0 ]
            ];
            Utils.ShuffleArray(numberPool);

            for (int i = 0; i < numberPool.Length; i++)
            {
                int column = i % 4;
                int row = i / 4;

                board[row][column] = numberPool[i];
            }
            board[3][3] = 0;

            if (CanBeSolved(board))
            {
                return board;
            }
            else
            {
                return Generate();
            }
        }

        /// <summary>
        /// Sprawdza czy plansza może być ułożona na podstawie liczby inwersji i numeru wiersza pustego pola.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public static bool CanBeSolved(int[][] board)
        {
            int inverseCount = CountInverses(board);
            int emptyCellRow = GetEmptyCellRow(board);
            int sum = inverseCount + emptyCellRow;
            bool canBeFinished = sum % 2 == 0;

            return canBeFinished;
        }

        /// <summary>
        /// Liczy liczbę inwersji podanej planszy, zwracając ją w postaci int.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public static int CountInverses(int[][] board)
        {
            int inverseCount = 0;

            for (int i = 0; i < Config.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Config.BOARD_SIZE; j++)
                {
                    int curr = board[i][j];
                    int next = (j < Config.BOARD_SIZE - 1)
                        ? board[i][j + 1]
                        : (i < Config.BOARD_SIZE - 1)
                            ? board[i + 1][0]
                            : -1;

                    if (next == -1) break;
                    if (next < curr) inverseCount++;
                }
            }

            return inverseCount;
        }

        /// <summary>
        /// Zwraca wiersz w którym znajduje się puste pole (liczba "0") w planszy, liczony od dołu.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public static int GetEmptyCellRow(int[][] board)
        {
            for (int i = 0; i < Config.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Config.BOARD_SIZE; j++)
                {
                    if (board[i][j] == 0)
                    {
                        return (Config.BOARD_SIZE - i);
                    }
                }
            }

            return -1;
        }
    }
}
