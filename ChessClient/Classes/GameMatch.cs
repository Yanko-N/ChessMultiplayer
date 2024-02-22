using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessClient.Classes
{
    public class GameMatch
    {
        public char[][] ChessBoard = new char[8][];

        private PictureBox[,] pictureBoxes = new PictureBox[8, 8];

        private bool myTurn;
        private bool _isWhite;

        private Tuple<int, int, char> firstClick;
        private Tuple<int, int,char> finalClick;
        public GameMatch(string fenNotation, Control.ControlCollection control, bool isWhite)
        {
            InitializeChessBoard();
            ParseFEN(fenNotation, isWhite);
            InitializePictureBoxes(control);
        }

        public void InitializeChessBoard()
        {

            for (int i = 0; i < 8; i++)
            {
                ChessBoard[i] = new char[8];

                for (int j = 0; j < 8; j++)
                {
                    ChessBoard[i][j] = ' ';
                }
            }
        }

        private void InitializePictureBoxes(Control.ControlCollection control)
        {
            // Criar um picture box para cada quadrado
            const int squareSize = 50;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PictureBox pictureBox = new PictureBox
                    {
                        Size = new System.Drawing.Size(squareSize, squareSize),
                        Location = new System.Drawing.Point(j * squareSize, i * squareSize),
                        BorderStyle = BorderStyle.FixedSingle,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Tag = new Tuple<int, int, char>(i, j, '?') // Guarda se a coluna e a linha na tag usando tuple -> é tipo uma cena para guardar uma estrutura de dados
                    };


                    pictureBox.Click += PictureBox_Click;

                    pictureBoxes[i, j] = pictureBox;
                    control.Add(pictureBox);
                }
            }

            UpdatePictureBoxes();
        }

        private void PictureBox_Click(object? sender, EventArgs e)
        {
            if (myTurn)
            {
                PictureBox clickedPictureBox = sender as PictureBox;
                if (clickedPictureBox != null)
                {
                    Tuple<int, int,char> position = clickedPictureBox.Tag as Tuple<int, int,char>;
                    if (position != null)
                    {



                        if (firstClick == null && finalClick == null)
                        {
                            if (ChessBoard[position.Item1][position.Item2] != ' ')
                            {
                                firstClick = position;
                                clickedPictureBox.BorderStyle = BorderStyle.Fixed3D;

                            }
                        }
                        else if (firstClick != null && finalClick == null)
                        {
                            finalClick = position;
                            pictureBoxes[firstClick.Item1, firstClick.Item2].BorderStyle = BorderStyle.FixedSingle;
                        }
                        else if (firstClick != null && finalClick != null)
                        {
                            firstClick = position;
                            pictureBoxes[firstClick.Item1, firstClick.Item2].BorderStyle = BorderStyle.Fixed3D;
                            finalClick = null;
                        }

                        if (firstClick != null && finalClick != null)
                        {
                           

                            //-------------------------------\\
                            //AQUI É ONDE SERÁ FEITA A JOGADA\\
                            //-------------------------------\\


                            //depois terá de se remover o myTurn para falso
                            if (isLegalMove(firstClick.Item3, new Tuple<int, int>(firstClick.Item1, firstClick.Item2), new Tuple<int, int>(finalClick.Item1, finalClick.Item2)))
                            {
                                //A JOGADA É LEGAL

                                //limpamos o sitio onde a peça estava
                                ChessBoard[firstClick.Item1][firstClick.Item2] = ' ';

                                //No sitio onde a peça foi jogada limpamos-a 
                                ChessBoard[finalClick.Item1][finalClick.Item2] = firstClick.Item3;

                                //mudamos a tag da peça que clicamos
                                clickedPictureBox.Tag = new Tuple<int, int, char>(firstClick.Item1, firstClick.Item2, '?');

                                //mudamos a tag da picture box para onde a peça vai
                                pictureBoxes[finalClick.Item1,finalClick.Item2].Tag = new Tuple<int, int, char>(finalClick.Item1, finalClick.Item2, firstClick.Item3);

                                myTurn = false;
                                UpdatePictureBoxes();
                                //Enviar para o Servidor!!!
                            }
                           
                        }
                    }
                }
            }

        }

        bool isLegalMove(char piece,Tuple<int,int> position,Tuple<int,int> desiredPosition)
        {
            char[] whitePieces = { 'R', 'N', 'B', 'Q', 'K', 'P' };
            char[] blackPieces = { 'r', 'n', 'b', 'q', 'k', 'p' };
            List<Tuple<int, int>> legalMoves = new List<Tuple<int, int>>();
            switch (piece)
            {
                case 'P'://Peão Branco
                    if (_isWhite)
                    {
                        //o item1 é a coordenada da fila
                        if (position.Item1 == 6)
                        {
                            if (!blackPieces.Contains(ChessBoard[position.Item1 - 2][position.Item2]))
                            {
                                legalMoves.Add(new Tuple<int, int>(position.Item1 - 2, position.Item2));

                            }
                        }

                        if (!blackPieces.Contains(ChessBoard[position.Item1 - 1][position.Item2]))
                        {
                            legalMoves.Add(new Tuple<int, int>(position.Item1 - 1, position.Item2));
                        }

                        //se a sua fila estiver antes ou igual de 1 topo<-
                        if (position.Item1 >= 1)
                        {
                            //se a sua coluna ser maior que 1 lado lateral esquerdo
                            if (position.Item2 >= 1)
                            {
                                //Vemos se na posição que o peao pode comer tem alguma peça preta
                                if (blackPieces.Contains(ChessBoard[position.Item1 - 1][position.Item2 - 1]))
                                {
                                    legalMoves.Add(new Tuple<int, int>(position.Item1 - 1, position.Item2 - 1));



                                }

                            }
                            //se a sua coluna é menor ou igual a 6 lado lateral direito
                            if (position.Item2 <= 6)
                            {
                                //Vemos se na posição que o peao pode comer tem alguma peça preta
                                if (blackPieces.Contains(ChessBoard[position.Item1 - 1][position.Item2 + 1]))
                                {
                                    legalMoves.Add(new Tuple<int, int>(position.Item1 - 1, position.Item2 + 1));



                                }
                            }

                        }


                        //AQUI DEPOIS É PRECISO ADICIONAR O CODIGO NECESSARIO PARA O PEÃO SE TORNAR RAINHA/ETC

                        //Adicionar Também os En Passant

                    }
                    return legalMoves.Contains(desiredPosition);
                case 'N':
                    if (_isWhite)
                    {

                    }
                    break;
                case 'B':
                    if (_isWhite)
                    {

                    }
                    break;
                case 'R':
                    if (_isWhite)
                    {

                    }
                    break;
                case 'Q':
                    if (_isWhite)
                    {

                    }
                    break;
                case 'K':
                    if (_isWhite)
                    {

                    }
                    break;
                case 'p':
                    if (!_isWhite)
                    {
                        //o item1 é a coordenada da fila
                        if (position.Item1 == 6)
                        {
                            if (!whitePieces.Contains(ChessBoard[position.Item1 - 2][position.Item2]))
                            {
                                legalMoves.Add(new Tuple<int, int>(position.Item1 - 2, position.Item2));

                            }
                        }

                        if (!whitePieces.Contains(ChessBoard[position.Item1 - 1][position.Item2]))
                        {
                            legalMoves.Add(new Tuple<int, int>(position.Item1 - 1, position.Item2));
                        }

                        //se a sua fila estiver antes ou igual de 1 topo<-
                        if (position.Item1 >= 1)
                        {
                            //se a sua coluna ser maior que 1 lado lateral esquerdo
                            if (position.Item2 >= 1)
                            {
                                //Vemos se na posição que o peao pode comer tem alguma peça preta
                                if (whitePieces.Contains(ChessBoard[position.Item1 - 1][position.Item2 - 1]))
                                {
                                    legalMoves.Add(new Tuple<int, int>(position.Item1 - 1, position.Item2 - 1));



                                }

                            }
                            //se a sua coluna é menor ou igual a 6 lado lateral direito
                            if (position.Item2 <= 6)
                            {
                                //Vemos se na posição que o peao pode comer tem alguma peça preta
                                if (whitePieces.Contains(ChessBoard[position.Item1 - 1][position.Item2 + 1]))
                                {
                                    legalMoves.Add(new Tuple<int, int>(position.Item1 - 1, position.Item2 + 1));



                                }
                            }

                        }


                        //AQUI DEPOIS É PRECISO ADICIONAR O CODIGO NECESSARIO PARA O PEÃO SE TORNAR RAINHA/ETC

                        //Adicionar Também os En Passant
                    }
                    break;
                case 'n':
                    if (!_isWhite)
                    {

                    }
                    break;
                case 'b':
                    if (!_isWhite)
                    {

                    }
                    break;
                case 'r':
                    if (!_isWhite)
                    {

                    }
                    break;
                case 'q':
                    if (!_isWhite)
                    {

                    }
                    break;
                case 'k':
                    if (_isWhite)
                    {

                    }
                    break;
                default:
                    return false;


            }


            return false;
        }
        void UpdatePictureBoxes()
        {
            // Update PictureBoxes 
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    char piece = ChessBoard[i][j];


                    string imagePath = GetImagePath(piece);
                    pictureBoxes[i, j].ImageLocation = imagePath;
                    pictureBoxes[i,j].Tag = new Tuple<int, int,char>(i, j,piece);

                    if(i%2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            pictureBoxes[i, j].BackColor = Color.Gray;
                        }
                        else
                        {
                            pictureBoxes[i, j].BackColor = Color.White;
                        }
                    }
                    else
                    {
                        if (j % 2 == 0)
                        {
                             pictureBoxes[i, j].BackColor = Color.White;
                        }
                        else
                        {
                            pictureBoxes[i, j].BackColor = Color.Gray;
                        }
                    }
                }
            }
        }
        private string GetImagePath(char piece)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            switch (piece)
            {
                case 'P':
                    return Path.Combine(basePath, "ChessPieces", "pawnBranco.png");
                case 'N':
                    return Path.Combine(basePath, "ChessPieces", "knightBranco.png");
                case 'B':
                    return Path.Combine(basePath, "ChessPieces", "bishopBranco.png");
                case 'R':
                    return Path.Combine(basePath, "ChessPieces", "torreBranco.png");
                case 'Q':
                    return Path.Combine(basePath, "ChessPieces", "rainhaBranco.png");
                case 'K':
                    return Path.Combine(basePath, "ChessPieces", "reiBranco.png");
                case 'p':
                    return Path.Combine(basePath, "ChessPieces", "pawnPreto.png");
                case 'n':
                    return Path.Combine(basePath, "ChessPieces", "knightPreto.png");
                case 'b':
                    return Path.Combine(basePath, "ChessPieces", "bishopPreto.png");
                case 'r':
                    return Path.Combine(basePath, "ChessPieces", "torrePreto.png");
                case 'q':
                    return Path.Combine(basePath, "ChessPieces", "rainhaPreto.png");
                case 'k':
                    return Path.Combine(basePath, "ChessPieces", "reiPreto.png");
                default:
                    return "";
            }
        }

        //"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
        //---------------------------------------------J

        private void ParseFEN(string fenNotation, bool isWhite)
        {
            string[] parts = fenNotation.Split(' ');

            // Verificar se tem 6 partes na notação FEN
            if (parts.Length != 6)
            {
                return;
            }

            string boardLayout = parts[0];


            int rank = (isWhite == true ? 0 : 7); // Inicia no ultimo rank
            int file = 0;

            foreach (char c in boardLayout)
            {
                if (c == '/')
                {
                    if (!isWhite) rank--; // Move para a próxima linha
                    else rank++;

                    file = 0; // Reinicia a coluna
                }
                else if (char.IsDigit(c))
                {
                    file += int.Parse(c.ToString());
                }
                else
                {
                    ChessBoard[rank][file] = c;
                    file++;
                }

            }
            //se sou branco-> true == true -> true || branco->false == true -> false
            myTurn = isWhite == (parts[1] == "w" ? true : false);
            _isWhite = isWhite == (parts[1] == "w" ? true : false);
        }

    }
}
