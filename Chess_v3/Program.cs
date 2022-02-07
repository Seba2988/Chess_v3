using System;

namespace Chess_v3
{
    class ChessGameLauncher
    {
        static void Main(string[] args)
        {
            new ChessGame().play();
        }
    }
    class Player
    {
        bool whitePlayerTurn = true;
        bool validInput;
        char[] playerMoveInput = new char[4];
        int[] playerMoveCoordinates = new int[4];
        public bool getPlayerTurn()
        {
            return whitePlayerTurn;
        }
        public void changePlayerTurn()
        {
            whitePlayerTurn = !whitePlayerTurn;
        }
        public void playerInputToIndex(string playerInput)
        {
            while (playerInput.Trim().Length != 4)
            {
                Console.WriteLine("Invalid input, please try again");
                playerInput = Console.ReadLine();
            }
            playerMoveInput = playerInput.ToUpper().ToCharArray(0, 4);
            playerInputCheck(playerMoveInput);
            validInput = true;
            playerMoveFromInputToCoordinates();
        }
        public void playerInputCheck(char[] playerMoveInput)
        {
            validInput = false;
            while (!validInput)
            {
                validInput = true;
                for (int i = 0; i < playerMoveInput.Length && validInput; i++)
                {
                    validInput = false;
                    for (int j = 0; j < 8 && !validInput; j++)
                    {
                        if (i % 2 != 0)
                        {
                            if (ChessGame.getNumbers(j) == playerMoveInput[i])
                            {
                                validInput = true;
                                continue;
                            }
                            else
                                validInput = false;
                        }
                        else
                        if (ChessGame.getLetters(j) == playerMoveInput[i])
                        {
                            validInput = true;
                            continue;
                        }
                        else
                            validInput = false;
                    }
                }
                if (!validInput)
                {
                    Console.WriteLine("Invalid input, please try again");
                    playerInputToIndex(Console.ReadLine());
                }
                else
                    break;
            }
        }
        public void playerMoveFromInputToCoordinates()
        {
            for (int i = 0; i < playerMoveInput.Length; i++)
            {
                if (i % 2 == 0)
                    playerMoveCoordinates[i] = ChessGame.getLettersIndex(playerMoveInput[i]);
                else
                    playerMoveCoordinates[i] = ChessGame.getNumbersIndex(playerMoveInput[i]);
            }
        }
        public int getFromRow()
        {
            return playerMoveCoordinates[1];
        }
        public int getFromColumn()
        {
            return playerMoveCoordinates[0];
        }
        public int getToRow()
        {
            return playerMoveCoordinates[3];
        }
        public int getToColumn()
        {
            return playerMoveCoordinates[2];
        }
    }
    class ChessGame
    {
        Piece[,] grid;
        static char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8' };
        static char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
        bool isDraw;
        public ChessGame()
        {
            grid = new Piece[8, 8]
            {
                {new Rook(0, 0, true), new Knight(0, 1, true), new Bishop(0, 2, true), new Queen(0, 3, true), new King(0, 4, true), new Bishop(0, 5,true), new Knight(0, 6, true), new Rook(0, 7, true)},
                {new Pawn(1, 0, true), new Pawn(1, 1, true), new Pawn(1, 2, true), new Pawn(1, 3, true), new Pawn(1, 4, true), new Pawn(1, 5, true), new Pawn(1, 6, true), new Pawn(1, 7, true)},
                {new Empty(2, 0), new Empty(2, 1), new Empty(2, 2), new Empty(2, 3), new Empty(2, 4), new Empty(2, 5), new Empty(2, 6), new Empty(2, 7)},
                {new Empty(3, 0), new Empty(3, 1), new Empty(3, 2), new Empty(3, 3), new Empty(3, 4), new Empty(3, 5), new Empty(3, 6), new Empty(3, 7)},
                {new Empty(4, 0), new Empty(4, 1), new Empty(4, 2), new Empty(4, 3), new Empty(4, 4), new Empty(4, 5), new Empty(4, 6), new Empty(4, 7)},
                {new Empty(5, 0), new Empty(5, 1), new Empty(5, 2), new Empty(5, 3), new Empty(5, 4), new Empty(5, 5), new Empty(5, 6), new Empty(5, 7)},
                {new Pawn(6, 0, false), new Pawn(6, 1, false), new Pawn(6, 2, false), new Pawn(6, 3, false), new Pawn(6, 4, false), new Pawn(6, 5, false), new Pawn(6, 6, false), new Pawn(6, 7, false)},
                {new Rook(7, 0, false), new Knight(7, 1, false), new Bishop(7, 2, false), new Queen(7, 3, false), new King(7, 4, false), new Bishop(7, 5,false), new Knight(7, 6, false), new Rook(7, 7, false)}
            };

        }
        public Piece getDestinationPiece(int rowTo, int columnTo)
        {
            return grid[rowTo, columnTo];
        }
        public static char getNumbers(int index)
        {
            return numbers[index];
        }
        public static char getLetters(int index)
        {
            return letters[index];
        }
        public static int getNumbersIndex(char number)
        {
            int index = 0;
            for (; index < numbers.Length; index++)
            {
                if (number == numbers[index])
                    break;
            }
            return index;
        }
        public static int getLettersIndex(char letter)
        {
            int index = 0;
            for (; index < letters.Length; index++)
            {
                if (letter == letters[index])
                    break;
            }
            return index;
        }
        public void print()
        {
            Console.WriteLine();
            Console.Write("    ");
            for (int i = 0; i < letters.Length; i++)
            {
                Console.Write(letters[i] + "  ");
            }
            Console.WriteLine();

            for (int i = 0; i < 8; i++)
            {
                Console.Write(numbers[i] + "  ");
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public void play()
        {
            ChessGame chessGame = new ChessGame();
            Console.WriteLine("Welcome to the game!\nPlayer one=W, Player two=B");
            Player players = new Player();
            print();

            while (!isDraw)
            {
                Console.WriteLine();
                Console.WriteLine((players.getPlayerTurn() ? "White" : "Black") + " player turn");
                players.playerInputToIndex(Console.ReadLine());

                //move check
                while (!grid[players.getFromRow(), players.getFromColumn()].isValidMove(getDestinationPiece(players.getToRow(), players.getToColumn()), players.getPlayerTurn(), grid))
                {
                    Console.WriteLine("Invalid move, please try again");
                    players.playerInputToIndex(Console.ReadLine());
                }
                //make move

                //copy board

                //win check

                //check?

                //draw check
                print();
                players.changePlayerTurn();

            }
        }

    }
    class Piece
    {
        bool isWhite;
        int row;
        int column;
        bool validMove;
        public Piece(int row, int column, bool isWhite)
        {
            this.row = row;
            this.column = column;
            this.isWhite = isWhite;
        }
        public virtual bool pieceIsWhite()
        {
            return isWhite;
        }
        public virtual void setPieceIsWhite(bool isWhite)
        {
            this.isWhite = isWhite;
        }
        public virtual bool isValidMove(Piece destinationPiece, bool playerTurn, Piece[,] grid)
        {
            return validMove;
        }
        public Piece copy(int row, int column, bool isWhite)
        {
            Piece result = new Piece(row, column, isWhite);
            return result;
        }
        public virtual int getRow()
        {
            return row;
        }
        public virtual void setRow(int row)
        {
            this.row = row;
        }
        public virtual int getColumn()
        {
            return column;
        }
        public virtual void setColumn(int column)
        {
            this.column = column;
        }
    }
    class Empty : Piece
    {
        public Empty(int row, int column) : base(row, column, false) { }
        public override string ToString()
        {
            return "EE";
        }
        public override bool isValidMove(Piece destinationPiece, bool playerTurn, Piece[,] grid)
        {
            return false;
        }
    }
    class King : Piece
    {
        bool hasMoved;
        public King(int row, int column, bool isWhite) : base(row, column, isWhite) { }
        public override string ToString()
        {
            return string.Format(pieceIsWhite() ? "W" : "B") + "K";
        }
        public override bool isValidMove(Piece destinationPiece, bool playerTurn, Piece[,] grid)
        {
            if (pieceIsWhite() != playerTurn)
                return false;
            if (destinationPiece is Empty || destinationPiece.pieceIsWhite() != pieceIsWhite())
            {
                if (getRow() - destinationPiece.getRow() == 1 && getColumn() - destinationPiece.getColumn() == 1)
                    return true;
                if (getRow() - destinationPiece.getRow() == 1 && getColumn() == destinationPiece.getColumn())
                    return true;
                if (getRow() - destinationPiece.getRow() == 1 && destinationPiece.getColumn() - getColumn() == 1)
                    return true;
                if (getRow() == destinationPiece.getRow() && destinationPiece.getColumn() - getColumn() == 1)
                    return true;
                if (destinationPiece.getRow() - getRow() == 1 && destinationPiece.getColumn() - getColumn() == 1)
                    return true;
                if (destinationPiece.getRow() - getRow() == 1 && destinationPiece.getColumn() == getColumn())
                    return true;
                if (destinationPiece.getRow() - getRow() == 1 && getColumn() - destinationPiece.getColumn() == 1)
                    return true;
                if (getRow() == destinationPiece.getRow() && getColumn() - destinationPiece.getColumn() == 1)
                    return true;
                
                if (!hasMoved)
                {
                    if (destinationPiece.getColumn() - getColumn() > 1 && grid[getRow(), getColumn() + 3] is Rook)
                    {
                        if (((Rook)grid[getRow(), getColumn() + 3]).getHasMoved())
                            return false;
                        else
                        if (grid[getRow(), getColumn() + 1] is Empty)
                            return true;
                    }
                    if (getColumn() - destinationPiece.getColumn() > 1 && grid[getRow(), getColumn() - 4] is Rook)
                    {
                        if (((Rook)grid[getRow(), getColumn() - 4]).getHasMoved())
                            return false;
                        else
                        if (grid[getRow(), getColumn() - 1] is Empty && grid[getRow(), getColumn() - 2] is Empty)
                            return true;
                    }
                }
            }
            return false;
        }
        public void setHasMoved()
        {
            hasMoved = true;
        }
        public bool getHasMoved()
        {
            return hasMoved;
        }
    }

    class Queen : Piece
    {
        Bishop queenAsBishop = new Bishop(0, 0, true);
        Rook queenAsRook = new Rook(0, 0, true);
        public Queen(int row, int column, bool isWhite) : base(row, column, isWhite) { }
        public override string ToString()
        {
            return string.Format(pieceIsWhite() ? "W" : "B") + "Q";
        }
        public override bool isValidMove(Piece destinationPiece, bool playerTurn, Piece[,] grid)
        {
            queenAsBishop.setRow(getRow());
            queenAsBishop.setColumn(getColumn());
            queenAsBishop.setPieceIsWhite(pieceIsWhite());
            if (queenAsBishop.isValidMove(destinationPiece, playerTurn, grid))
            {
                return true;
            }
            else
            {
                queenAsRook.setRow(getRow());
                queenAsRook.setColumn(getColumn());
                queenAsRook.setPieceIsWhite(pieceIsWhite());
            }
            return queenAsRook.isValidMove(destinationPiece, playerTurn, grid);
        }
    }
    class Knight : Piece
    {
        public Knight(int row, int column, bool isWhite) : base(row, column, isWhite) { }
        public override string ToString()
        {
            return string.Format(pieceIsWhite() ? "W" : "B") + "N";
        }
        public override bool isValidMove(Piece destinationPiece, bool playerTurn, Piece[,] grid)
        {
            if (pieceIsWhite() != playerTurn)
                return false;
            if (destinationPiece is Empty || destinationPiece.pieceIsWhite() != pieceIsWhite())
            {
                if (getRow() - destinationPiece.getRow() == 2 && destinationPiece.getColumn() - getColumn() == 1)
                    return true;
                if (getRow() - destinationPiece.getRow() == 2 && getColumn() - destinationPiece.getColumn() == 1)
                    return true;
                if (getRow() - destinationPiece.getRow() == 1 && destinationPiece.getColumn() - getColumn() == 2)
                    return true;
                if (getRow() - destinationPiece.getRow() == 1 && getColumn() - destinationPiece.getColumn() == 2)
                    return true;
                if (destinationPiece.getRow() - getRow() == 2 && destinationPiece.getColumn() - getColumn() == 1)
                    return true;
                if (destinationPiece.getRow() - getRow() == 2 && getColumn() - destinationPiece.getColumn() == 1)
                    return true;
                if (destinationPiece.getRow() - getRow() == 1 && destinationPiece.getColumn() - getColumn() == 2)
                    return true;
                if (destinationPiece.getRow() - getRow() == 1 && getColumn() - destinationPiece.getColumn() == 2)
                    return true;

                return false;
            }
            return false;
        }
    }
    class Bishop : Piece
    {
        public Bishop(int row, int column, bool isWhite) : base(row, column, isWhite) { }
        public override string ToString()
        {
            return string.Format(pieceIsWhite() ? "W" : "B") + "B";
        }
        public override bool isValidMove(Piece destinationPiece, bool playerTurn, Piece[,] grid)
        {
            if (pieceIsWhite() != playerTurn)
                return false;
            if (destinationPiece is Empty || destinationPiece.pieceIsWhite() != pieceIsWhite())
            {
                //right up move
                if (getRow() > destinationPiece.getRow() && getColumn() < destinationPiece.getColumn())
                {
                    for (int i = getRow(); i > destinationPiece.getRow(); i--)
                    {
                        for (int j = getColumn(); j < destinationPiece.getColumn(); j++)
                        {
                            if (!(grid[i - 1, j + 1] is Empty))
                            {
                                return false;
                            }
                            i--;
                        }
                    }
                    return true;
                }
                //rigth down move
                if (getRow() < destinationPiece.getRow() && getColumn() < destinationPiece.getColumn())
                {
                    for (int i = getRow(); i < destinationPiece.getRow(); i++)
                    {
                        for (int j = getColumn(); j < destinationPiece.getColumn(); j++)
                        {
                            if (!(grid[i + 1, j + 1] is Empty))
                            {
                                return false;
                            }
                            i++;
                        }
                    }
                    return true;
                }
                //left up move
                if (getRow() > destinationPiece.getRow() && getColumn() > destinationPiece.getColumn())
                {
                    for (int i = getRow(); i > destinationPiece.getRow(); i--)
                    {
                        for (int j = getColumn(); j > destinationPiece.getColumn(); j--)
                        {
                            if (!(grid[i - 1, j - 1] is Empty))
                            {
                                return false;
                            }
                            i--;
                        }
                    }
                    return true;
                }
                //left down move
                if (getRow() < destinationPiece.getRow() && getColumn() > destinationPiece.getColumn())
                {
                    for (int i = getRow(); i < destinationPiece.getRow(); i++)
                    {
                        for (int j = getColumn(); j > destinationPiece.getColumn(); j--)
                        {
                            if (!(grid[i + 1, j - 1] is Empty))
                            {
                                return false;
                            }
                            i++;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

    }
    class Rook : Piece
    {
        bool hasMoved;
        public Rook(int row, int column, bool isWhite) : base(row, column, isWhite) { }
        public override string ToString()
        {
            return string.Format(pieceIsWhite() ? "W" : "B") + "R";
        }

        public override bool isValidMove(Piece destinationPiece, bool playerTurn, Piece[,] grid)
        {
            if (pieceIsWhite() != playerTurn)
                return false;
            if (destinationPiece is Empty || destinationPiece.pieceIsWhite() != pieceIsWhite())
            {
                //right move
                if (getRow() == destinationPiece.getRow() && getColumn() < destinationPiece.getColumn())
                {
                    for (int i = getColumn(); i < destinationPiece.getColumn(); i++)
                    {
                        if (!(grid[getRow(), i + 1] is Empty))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                //left move
                if (getRow() == destinationPiece.getRow() && getColumn() > destinationPiece.getColumn())
                {
                    for (int i = getColumn(); i > destinationPiece.getColumn(); i--)
                    {
                        if (!(grid[getRow(), i - 1] is Empty))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                //down move
                if (getRow() < destinationPiece.getRow() && getColumn() == destinationPiece.getColumn())
                {
                    for (int i = getRow(); i < destinationPiece.getRow(); i++)
                    {
                        if (!(grid[i + 1, getColumn()] is Empty))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                //up move
                if (getRow() > destinationPiece.getRow() && getColumn() == destinationPiece.getColumn())
                {
                    for (int i = getRow(); i > destinationPiece.getRow(); i--)
                    {
                        if (!(grid[i - 1, getColumn()] is Empty))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
        public void setHasMoved()
        {
            hasMoved = true;
        }
        public bool getHasMoved()
        {
            return hasMoved;
        }
    }
    class Pawn : Piece
    {
        bool isEnPassant;
        bool hasMoved;
        public Pawn(int row, int column, bool isWhite) : base(row, column, isWhite) { }
        public override string ToString()
        {
            return string.Format(pieceIsWhite() ? "W" : "B") + "P";
        }
        public override bool isValidMove(Piece destinationPiece, bool playerTurn, Piece[,] grid)
        {
            if (!hasMoved)
            {
                if (playerTurn && destinationPiece is Empty)
                {
                    if (destinationPiece.getRow() - getRow() > 1 && destinationPiece.getColumn() == getColumn() && grid[getRow() + 1, getColumn()] is Empty)
                    {
                        return true;
                    }
                }
                else
                if (!playerTurn && destinationPiece is Empty)
                {
                    if (getRow() - destinationPiece.getRow() > 1 && destinationPiece.getColumn() == getColumn() && grid[getRow() - 1, getColumn()] is Empty)
                    {
                        return true;
                    }
                }
            }
            if (destinationPiece is Empty)
            {
                if (playerTurn)
                {
                    if (destinationPiece.getRow() - getRow() == 1 && destinationPiece.getColumn() == getColumn())
                        return true;

                    if (((destinationPiece.getRow() - getRow() == 1 && destinationPiece.getColumn() - getColumn() == 1) || (destinationPiece.getRow() - getRow() == 1 && getColumn() - destinationPiece.getColumn() == 1)) && grid[destinationPiece.getRow() - 1, destinationPiece.getColumn()] is Pawn)
                    {
                        if (((Pawn)grid[destinationPiece.getRow() - 1, destinationPiece.getColumn()]).getIsEnPassant())
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (getRow() - destinationPiece.getRow() == 1 && destinationPiece.getColumn() == getColumn())
                        return true;
                    if (((getRow() - destinationPiece.getRow() == 1 && destinationPiece.getColumn() - getColumn() == 1) || (getRow() - destinationPiece.getRow() == 1 && getColumn() - destinationPiece.getColumn() == 1)) && grid[destinationPiece.getRow() + 1, destinationPiece.getColumn()] is Pawn)
                    {
                        if (((Pawn)grid[destinationPiece.getRow() + 1, destinationPiece.getColumn()]).getIsEnPassant())
                        {
                            return true;
                        }
                    }
                }
            }
            if (destinationPiece.pieceIsWhite() != playerTurn && !(destinationPiece is Empty))
            {
                if (playerTurn)
                {
                    if ((destinationPiece.getRow() - getRow() == 1 && destinationPiece.getColumn() - getColumn() == 1) || (destinationPiece.getRow() - getRow() == 1 && getColumn() - destinationPiece.getColumn() == 1))
                    {
                        return true;
                    }
                }
                else
                {
                    if ((getRow() - destinationPiece.getRow() == 1 && destinationPiece.getColumn() - getColumn() == 1) || (getRow() - destinationPiece.getRow() == 1 && getColumn() - destinationPiece.getColumn() == 1))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void setHasMoved()
        {
            hasMoved = true;
        }
        public bool getHasMoved()
        {
            return hasMoved;
        }
        public void setIsEnPassant()
        {
            isEnPassant = !isEnPassant;
        }
        public bool getIsEnPassant()
        {
            return isEnPassant;
        }
        public Piece promote(bool playerTurn)
        {
            bool validInput = false;
            char[] validChoices = { 'Q', 'R', 'N', 'B' };

            Console.WriteLine("The pawn is promoted, please select the new piece and press ENTER");
            string input = Console.ReadLine();
            while (!validInput)
            {
                while (input.Trim().Length != 1)
                {
                    Console.WriteLine("Invalid input, please try again");
                    input = Console.ReadLine();
                }
                input = input.Trim().ToUpper();
                for (int i = 0; i < validChoices.Length && !validInput; i++)
                {
                    if (validChoices[i].ToString() == input)
                    {
                        validInput = true;
                    }
                }
                if (!validInput)
                {
                    Console.WriteLine("Invalid input, please try again");
                    input = Console.ReadLine();
                }
            }
            Piece pawnPromoted = new Piece(0, 0, true);
            switch (input)
            {
                case "Q":
                    pawnPromoted = new Queen(getRow(), getColumn(), playerTurn);
                    break;
                case "R":
                    pawnPromoted = new Rook(getRow(), getColumn(), playerTurn);
                    break;
                case "N":
                    pawnPromoted = new Knight(getRow(), getColumn(), playerTurn);
                    break;
                case "B":
                    pawnPromoted = new Bishop(getRow(), getColumn(), playerTurn);
                    break;
            }
            return pawnPromoted;
        }
    }
}

