using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.IO;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private int _width = 20;
        private Label _food;
        private Random _rand;
        private Label _snake;
        private Label _snakePiece;
        private Label snakeHead;
        List<Label> snakePieces = new List<Label>();
        List<int> scores = new List<int>();
        List<string> highScores = new List<string>();
        //List<string> lines;
        private int _score;
        private Direction direction;
        //private string _filePath = "c:/Users/Togrul/Desktop/score.txt";

        enum Direction
        {
            up,
            down,
            right,
            left
        }

        public Form1()
        {
            InitializeComponent();
            _rand = new Random();
            //File = new StreamWriter("c:/Users/Togrul/Desktop/score.txt");
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Reset();
            PlaceSnake();
            timer1.Interval = 50;
            switch (direction)
            {
                case Direction.up:
                    direction = Direction.up;
                    break;
                case Direction.down:
                    direction = Direction.down;
                    break;
                case Direction.right:
                    direction = Direction.right;
                    break;
                case Direction.left:
                    direction = Direction.left;
                    break;
                default:
                    break;
            }
            snakePieces.Add(_snake);
        }

        private void Reset()
        {
            CreateFood();
            PlaceFood();
        }
        
        private void GameOver()
        {
            scores.Add(_score);

            //lines = new List<string>();
            //lines = File.ReadAllLines(_filePath).ToList();


            //for (int i = 0; i < scores.Count; i++)
            //{
            //    lines.Add(scores[i].ToString());
            //}

            //for (int i = 0; i < lines.Count; i++)
            //{
            //    highScores.Add(lines[i]);
            //}

            //File.WriteAllLines(_filePath, lines);
            //Console.ReadLine();

            panel1.Controls.Clear();
            snakePieces.Clear();
            _score = 0;
            scoreLbl.Text = _score.ToString();
            Reset();
            PlaceSnake();
            snakePieces.Add(_snake);
        }

        private void  panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        private Label SnakePieceCreate()
        {
            Label lbl = new Label()
            {
                Name = "labelnew",
                Width = _width,
                Height = _width,
                BackColor = Color.Red,
                BorderStyle = BorderStyle.FixedSingle
            };
            panel1.Controls.Add(lbl);
            _snakePiece = lbl;
            return lbl;
        }

        private void PlaceSnake()
        {
            snakeHead = SnakePieceCreate();
            snakeHead.BackColor = Color.Green;
            int pnlLocationX = (panel1.Width / 2);
            int pnlLocationY = (panel1.Height / 2);
            snakeHead.Location = new Point(pnlLocationX, pnlLocationY);
            _snake = snakeHead;
        }
        private void Find()
        {
            Burn();
            if ((_snake.Location.X == _food.Location.X) && (_snake.Location.Y == _food.Location.Y))
            {
                SnakePieceCreate();
                switch (direction)
                {
                    case Direction.up:
                        _snakePiece.Location = new Point(_snake.Location.X, _snake.Location.Y + _width);
                        break;
                    case Direction.down:
                        _snakePiece.Location = new Point(_snake.Location.X, _snake.Location.Y - _width);
                        break;
                    case Direction.right:
                        _snakePiece.Location = new Point(_snake.Location.X - _width, _snake.Location.Y);
                        break;
                    case Direction.left:
                        _snakePiece.Location = new Point(_snake.Location.X + _width, _snake.Location.Y);
                        break;
                    default:
                        break;
                }
                snakePieces.Add(_snakePiece);
                panel1.Controls.Remove(_food);
                _score += 20;
                scoreLbl.Text = Convert.ToString(_score);
                Reset();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var locationX = snakeHead.Location.X;
            var locationY = snakeHead.Location.Y;
            switch (direction)
            {
                case Direction.up:
                    snakeHead.Location = new Point(locationX, locationY - _width);
                    if (_snake.Location.Y < 0)
                    {
                        _snake.Location = new Point(_snake.Location.X, panel1.Height - _width);
                    }

                    for (int i = snakePieces.Count - 1; i > 0; i--)
                    {
                        if (i == 0)
                        {
                            return;
                        }
                        var nextLocation = snakePieces[i];
                        var prevLocation = snakePieces[i - 1];
                        nextLocation.Location = prevLocation.Location;
                    }

                    break;
                case Direction.down:
                    snakeHead.Location = new Point(locationX, locationY + _width);
                    if (_snake.Location.Y > panel1.Height)
                    {
                        _snake.Location = new Point(_snake.Location.X, 0);
                    }

                    for (int i = snakePieces.Count - 1; i > 0; i--)
                    {
                        var nextLocation = snakePieces[i];
                        var prevLocation = snakePieces[i - 1];
                        nextLocation.Location = prevLocation.Location;
                    }
                    break;
                case Direction.right:
                    snakeHead.Location = new Point(locationX + _width, locationY);
                    if (_snake.Location.X == panel1.Width)
                    {
                        _snake.Location = new Point(0, _snake.Location.Y);
                    }

                    for (int i = snakePieces.Count - 1; i > 0; i--)
                    {
                        var nextLocation = snakePieces[i];
                        var prevLocation = snakePieces[i - 1];
                        nextLocation.Location = prevLocation.Location;
                    }
                    break;
                case Direction.left:
                    snakeHead.Location = new Point(locationX - _width, locationY);
                    if (_snake.Location.X < 0)
                    {
                        _snake.Location = new Point(panel1.Width - _width, _snake.Location.Y);
                    }

                    for (int i = snakePieces.Count - 1; i > 0; i--)
                    {
                        var nextLocation = snakePieces[i];
                        var prevLocation = snakePieces[i - 1];
                        nextLocation.Location = prevLocation.Location;
                    }
                    break;
                default:
                    break;
            }
            Find();
        }

        private void CreateFood()
        {
            Label lbl = new Label
            {
                Name = "food",
                Width = _width,
                Height = _width,
                BackColor = Color.Orange
            };
            panel1.Controls.Add(lbl);
            _food = lbl;
        }

        private void PlaceFood()
        {
            int locationX;
            int locationY;


        Here:
            locationX = _rand.Next(0, panel1.Width / 20 - 1) * 20;
            locationY = _rand.Next(0, panel1.Height / 20 - 1) * 20;

            for (int i = 0; i < snakePieces.Count; i++)
            {
                if (locationX == snakePieces[i].Location.X && locationY == snakePieces[i].Location.Y)
                {
                    goto Here;
                }
            }
            _food.Location = new Point(locationX, locationY);
        }

        private void Burn()
        {
            for (int i = 2; i < snakePieces.Count; i++)
            {
                if (_snake.Location.X == snakePieces[i].Location.X && _snake.Location.Y == snakePieces[i].Location.Y)
                {
                    timer1.Stop();
                    MessageBox.Show("Yandiniz");
                    GameOver();
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;

            if (e.KeyCode == Keys.Up && direction != Direction.down)
            {
                direction = Direction.up;
            }
            else if (e.KeyCode == Keys.Down && direction != Direction.up)
            {
                direction = Direction.down;
            }
            else if (e.KeyCode == Keys.Right && direction != Direction.left)
            {
                direction = Direction.right;
            }
            else if (e.KeyCode == Keys.Left && direction != Direction.right)
            {
                direction = Direction.left;
            }
        }
    }
}
