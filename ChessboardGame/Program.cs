using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessboardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int t = Convert.ToInt32(Console.ReadLine());
            for (int j = 0; j < t; j++)
            {
                int[] pos = Array.ConvertAll(Console.ReadLine().Split(' '), Int32.Parse);
                Chessboard board = new Chessboard(15, 15, pos[0], pos[1]);
                Console.WriteLine(board.Winner());
            }
        }
    }

    class Chessboard
    {
        int width { get; set; }
        int height { get; set; }
        int coin_x { get; set; } = 1;
        int coin_y { get; set; } = 1;
        List<int[]> moves = new List<int[]>();
        List<int[]> winningPos = new List<int[]>();
        Dictionary<int[], int> pos = new Dictionary<int[], int>();
        bool?[,] table;

        public Chessboard(int width, int height, int coinX, int coinY)
        {
            Width = width;
            Height = height;
            AddMoves();
            table = new bool?[Width, Height];
            for (int i = 1; i <= Width; i++)
            {
                CoinX = i;
                for (int j = 1; j <= Height; j++)
                {
                    CoinY = j;
                    if (moves.All(m => !IsValidMoveAt(m, CoinX - 1, CoinY - 1))) table[i, j] = true;
                }
            }
            CoinX = coinX;
            CoinY = coinY;
            CalculateTable();
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int CoinX
        {
            get { return coin_x; }
            set { coin_x = value; }
        }

        public int CoinY
        {
            get { return coin_y; }
            set { coin_y = value; }
        }

        void AddMoves()
        {
            int[] m1 = { -2, 1 };
            int[] m2 = { -2, -1 };
            int[] m3 = { 1, -2 };
            int[] m4 = { -1, -2 };
            moves.Add(m1);
            moves.Add(m2);
            moves.Add(m3);
            moves.Add(m4);
        }

        bool IsValidMoveAt(int[] dpos, int posx, int posy) => posx + dpos[0] >= 0 && posx + dpos[0] < Width && posy + dpos[1] >= 0 && posy + dpos[1] < Height;

        public void CalculateTable()
        {
            for (int i = 0; i < Width; i++)
            {
                for (int posx = 0; posx < Width; posx++)
                {
                    for (int posy = 0; posy < Width; posy++)
                    {
                        List<int[]> m = moves.Where(x => IsValidMoveAt(x, posx, posy)).ToList();
                        if (m.Count == 0) table[posx, posy] = false;
                        else table[posx, posy] = m.All(x => table[posx + x[0], posy + x[1]] == true) ? false : true;
                    }
                }
            }
        }

        public string Winner() => table[CoinX - 1, CoinY - 1] == true ? "First" : "Second";
    }
}