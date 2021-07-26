using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FrogsWindowsFormsApp
{
    public partial class MainForm : Form
    {
        private int emptyPictureBoxBeginLocationX;
        private int stepCount = 0;
        private int bestResult;
        private List<int> result = new List<int>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Init();
            emptyPictureBoxBeginLocationX = emptyPictureBox.Location.X;
        }

        private void FrogPictureBox_Click(object sender, EventArgs e)
        {
            var clickedPictureBox = (PictureBox)sender;
            var emptyLocationX = emptyPictureBox.Location.X;
            var distance = Math.Abs(clickedPictureBox.Location.X - emptyLocationX);
            var normalizedDistance = distance / clickedPictureBox.Size.Width;//находим сколько картинок есть между ними (1, 2, 3 и т.д.)
            if (normalizedDistance > 2)
            {
                MessageBox.Show("Так прыгать нельзя!");
                return;
            }
            var emptyLocation = emptyPictureBox.Location;
            emptyPictureBox.Location = clickedPictureBox.Location;
            clickedPictureBox.Location = emptyLocation;
            stepCount++;
            ShowStepCount();
            if (IsWin())
            {
                SaveResult();
                ShowWin();
            }
            Init();
        }

        private void ShowStepCount()
        {
            stepCountLabel.Text = "Количество ходов - " + stepCount;
        }

        private void ShowBestResult()
        {
            bestResultLabel.Text = "Рекорд - " + bestResult;
        }

        private bool IsWin()
        {
            return emptyPictureBox.Location.X == emptyPictureBoxBeginLocationX &&
                leftFrogPictureBox1.Location.X > emptyPictureBoxBeginLocationX &&
                leftFrogPictureBox2.Location.X > emptyPictureBoxBeginLocationX &&
                leftFrogPictureBox3.Location.X > emptyPictureBoxBeginLocationX &&
                leftFrogPictureBox4.Location.X > emptyPictureBoxBeginLocationX;
        }

        private void ShowWin()
        {
            var winForm = new WinForm(stepCount);
            winForm.Show();
            if (stepCount < bestResult)
            {
                ShowBestResult();
            }
        }

        private void Init()
        {
            if (!File.Exists("result.json"))
            {
                bestResult = stepCount;
            }
            else
            {
                if (File.Exists("result.json"))
                {
                    result = ResultStorage.GetResultFromFile();
                    bestResult = Convert.ToInt32(result[0]);
                    for (int i = 1; i < result.Count; i++)
                    {
                        if (bestResult > Convert.ToInt32(result[i]))
                        {
                            bestResult = Convert.ToInt32(result[i]);
                        }
                    }
                }
            }
            ShowBestResult();
        }

        private void SaveResult()
        {
            if (!File.Exists("result.json"))
            {
                result.Add(stepCount);
                ResultStorage.SaveResult(result);
                return;
            }
            result = ResultStorage.GetResultFromFile();
            result.Add(stepCount);
            ResultStorage.SaveResult(result);
        }

        private void NewGame()
        {

            rightFrogPictureBox4.Location = new System.Drawing.Point(832, 27);
            rightFrogPictureBox3.Location = new System.Drawing.Point(728, 27);
            rightFrogPictureBox2.Location = new System.Drawing.Point(624, 27);
            rightFrogPictureBox1.Location = new System.Drawing.Point(520, 27);
            emptyPictureBox.Location = new System.Drawing.Point(416, 27);
            leftFrogPictureBox4.Location = new System.Drawing.Point(312, 27);
            leftFrogPictureBox3.Location = new System.Drawing.Point(208, 27);
            leftFrogPictureBox2.Location = new System.Drawing.Point(104, 27);
            leftFrogPictureBox1.Location = new System.Drawing.Point(0, 27);
            stepCount = 0;
            ShowStepCount();
        }
        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Необходимо поменять лягушек местами, смотрящих вправо – направо, смотрящих влево – налево." +
                " Лягушки прыгают вперед и назад – на следующую кочку или через одну кочку в зависимости от наличия свободных.",
                "Правила игры");
        }
    }
}
