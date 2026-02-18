using System.Windows;
using System.Windows.Controls;

namespace Ukladanka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        new Dictionary<(char, int), Button> CellMappings = new Dictionary<(char, int), Button>();

        public MainWindow()
        {
            InitializeComponent();
            InitCells();
        }

        private void CellClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null) return;
            
            string buttonName = button.Name.ToLower();
            string cellString = buttonName.Replace("cell", "");
            char column = cellString.ElementAt(0);
            int row = int.Parse(cellString.ElementAt(1).ToString());

            (char, int)? move = CheckMove(column, row);

            // ruch jest nullem - żaden nie jest możliwy
            if (move == null) return;

            string currentValue = GetCellTextValue(column, row)!;
            SetCellTextValue(column, row, null);
            SetCellTextValue(move.Value.Item1, move.Value.Item2, currentValue);
        }

        /// <summary>
        /// Inicjalizuje wszystkie komórki składające się na plansze.s
        /// </summary>
        private void InitCells()
        {
            this.CellMappings = new Dictionary<(char, int), Button>()
            {
                { ('a', 1), this.CellA1 },
                { ('b', 1), this.CellB1 },
                { ('c', 1), this.CellC1 },
                { ('d', 1), this.CellD1 },
                { ('a', 2), this.CellA2 },
                { ('b', 2), this.CellB2 },
                { ('c', 2), this.CellC2 },
                { ('d', 2), this.CellD2 },
                { ('a', 3), this.CellA3 },
                { ('b', 3), this.CellB3 },
                { ('c', 3), this.CellC3 },
                { ('d', 3), this.CellD3 },
                { ('a', 4), this.CellA4 },
                { ('b', 4), this.CellB4 },
                { ('c', 4), this.CellC4 },
                { ('d', 4), this.CellD4 },
            };

            int i = 1;
            foreach(var (key, value) in this.CellMappings)
            {
                // prawy dolny róg - nie ustawiaj znaku i zakończ
                if (key == ('d', 4)) break;

                value.Content = i.ToString();
                i++;
            }
        }

        /// <summary>
        /// Zwraca tuple z znakiem kolumny i numerem wiersza możliwego ruch.
        /// Zwraca null jeśli żaden nie jest możliwy.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private (char, int)? CheckMove(char column, int row)
        {
            // nie przesuwamy pustego oczka
            if(GetCellTextValue(column, row) == null) return null;

            // pierwszy rząd
            if(column == 'a' && row == 1)
            {
                if (IsCellEmpty('b', 1)) return ('b', 1);
                if (IsCellEmpty('a', 2)) return ('a', 2);
            }
            if(column == 'b' && row == 1)
            {
                if (IsCellEmpty('a', 1)) return ('a', 1);
                if (IsCellEmpty('b', 2)) return ('b', 2);
                if (IsCellEmpty('c', 1)) return ('c', 1);
            }
            if (column == 'c' && row == 1)
            {
                if (IsCellEmpty('b', 1)) return ('b', 1);
                if (IsCellEmpty('c', 2)) return ('c', 2);
                if (IsCellEmpty('d', 1)) return ('d', 1);
            }
            if (column == 'd' && row == 1)
            {
                if (IsCellEmpty('c', 1)) return ('c', 1);
                if (IsCellEmpty('d', 2)) return ('d', 2);
            }

            // drugi rząd
            if (column == 'a' && row == 2)
            {
                if (IsCellEmpty('a', 1)) return ('a', 1);
                if (IsCellEmpty('b', 2)) return ('b', 2);
                if (IsCellEmpty('a', 3)) return ('a', 3);
            }
            if (column == 'b' && row == 2)
            {
                if (IsCellEmpty('b', 1)) return ('b', 1);
                if (IsCellEmpty('a', 2)) return ('a', 2);
                if (IsCellEmpty('c', 2)) return ('c', 2);
                if (IsCellEmpty('b', 3)) return ('b', 3);
            }
            if (column == 'c' && row == 2)
            {
                if (IsCellEmpty('c', 1)) return ('c', 1);
                if (IsCellEmpty('b', 2)) return ('b', 2);
                if (IsCellEmpty('d', 2)) return ('d', 2);
                if (IsCellEmpty('c', 3)) return ('c', 3);
            }
            if (column == 'd' && row == 2)
            {
                if (IsCellEmpty('d', 1)) return ('d', 1);
                if (IsCellEmpty('c', 2)) return ('c', 2);
                if (IsCellEmpty('d', 3)) return ('d', 3);
            }

            // trzeci rząd
            if (column == 'a' && row == 3)
            {
                if (IsCellEmpty('a', 2)) return ('a', 2);
                if (IsCellEmpty('b', 3)) return ('b', 3);
                if (IsCellEmpty('a', 4)) return ('a', 4);
            }
            if (column == 'b' && row == 3)
            {
                if (IsCellEmpty('b', 2)) return ('b', 2);
                if (IsCellEmpty('a', 3)) return ('a', 3);
                if (IsCellEmpty('c', 3)) return ('c', 3);
                if (IsCellEmpty('b', 4)) return ('b', 4);
            }
            if (column == 'c' && row == 3)
            {
                if (IsCellEmpty('c', 2)) return ('c', 2);
                if (IsCellEmpty('b', 3)) return ('b', 3);
                if (IsCellEmpty('d', 3)) return ('d', 3);
                if (IsCellEmpty('c', 4)) return ('c', 4);
            }
            if (column == 'd' && row == 3)
            {
                if (IsCellEmpty('d', 2)) return ('d', 2);
                if (IsCellEmpty('c', 3)) return ('c', 3);
                if (IsCellEmpty('d', 4)) return ('d', 4);
            }

            // czwarty rząd
            if (column == 'a' && row == 4)
            {
                if (IsCellEmpty('a', 3)) return ('a', 3);
                if (IsCellEmpty('b', 4)) return ('b', 4);
            }
            if (column == 'b' && row == 4)
            {
                if (IsCellEmpty('b', 3)) return ('b', 3);
                if (IsCellEmpty('a', 4)) return ('a', 4);
                if (IsCellEmpty('c', 4)) return ('c', 4);
            }
            if (column == 'c' && row == 4)
            {
                if (IsCellEmpty('c', 3)) return ('c', 3);
                if (IsCellEmpty('b', 4)) return ('b', 4);
                if (IsCellEmpty('d', 4)) return ('d', 4);
            }
            if (column == 'd' && row == 4)
            {
                if (IsCellEmpty('d', 3)) return ('d', 3);
                if (IsCellEmpty('c', 4)) return ('c', 4);
            }

            return null;
        }

        /// <summary>
        /// Zwraca wartość tekstową z komórki identyfikowanej przez podany znak kolumny i numer wiersza.
        /// Zwraca null jeżeli podana komórka nie istnieje.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private string? GetCellTextValue(char column, int row)
        {
            Button button;
            CellMappings.TryGetValue((column, row), out button);

            if (button == null) return null;
            else return (string?) button.Content;
        }

        /// <summary>
        /// Ustawia wartość komórki identyfikowanej przez podany znak kolumny i numer wiersza.
        /// Jeśli komórka nie istnieje, operacja jest pomijana.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <param name="value"></param>
        private void SetCellTextValue(char column, int row, string? value)
        {
            Button button;
            CellMappings.TryGetValue((column, row), out button);

            if(button == null) return;
            else button.Content = value;
        }

        private bool IsCellEmpty(char column, int row)
        {
            string? value = GetCellTextValue(column, row);

            return string.IsNullOrEmpty(value);
        }
    }
}