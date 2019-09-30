using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRacingGame
{
    public partial class Form1 : Form
    {
        Cars[] jeviCarArray = new Cars[4]; // creates one array of 4 cars objects 
        Punter[] jeviPuntersArray = new Punter[3]; // creates one array of 3 guy objects
        Random MyRandomNumbers = new Random();
        public Form1()
        {
            InitializeComponent();

            settingTheRaceTrack();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Cars take starting position
            jeviCarArray[0].CarsStartingPosition();
            jeviCarArray[1].CarsStartingPosition();
            jeviCarArray[2].CarsStartingPosition();
            jeviCarArray[3].CarsStartingPosition();

            //disable race button till the end of the race
            bettingParlor.Enabled = false;

            //start timer
            timer1.Start();
        }

        private void Bets_Click(object sender, EventArgs e)
        {
            if (joeRadioButton.Checked)
            {
                if (jeviPuntersArray[0].PlaceBet((int)numericUpDown1.Value, (int)numericUpDown2.Value))
                {
                    joeBetLabel.Text = jeviPuntersArray[0].MyBetForCars.GetTheDescription();
                }
            }
            else if (bobRadioButton.Checked)
            {
                if (jeviPuntersArray[1].PlaceBet((int)numericUpDown1.Value, (int)numericUpDown2.Value))
                {
                    bobBetLabel.Text = jeviPuntersArray[1].MyBetForCars.GetTheDescription();
                }
            }
            else if (alRadioButton.Checked)
            {
                if (jeviPuntersArray[2].PlaceBet((int)numericUpDown1.Value, (int)numericUpDown2.Value))
                {
                    alBetLabel.Text = jeviPuntersArray[2].MyBetForCars.GetTheDescription();
                }
            }
        }


        private void settingTheRaceTrack()//this funtion is for setting the race track
        {
            joeRadioButton.Checked = true;
            // initialize minimum bet label
            minimumBetLabel.Text = "Minimum Bet : " + numericUpDown1.Minimum.ToString() + " dollars";

            // initialize all 4 elements of the CarArray
            jeviCarArray[0] = new Cars()
            {
                MyPictureBox = Car1,
                CarStartingPosition = Car1.Left,
                TrackLength = pictureBox1.Width - Car1.Width,
                Randomizer = MyRandomNumbers
            };

            jeviCarArray[1] = new Cars()
            {
                MyPictureBox = Car2,
                CarStartingPosition = Car2.Left,
                TrackLength = pictureBox1.Width - Car2.Width,
                Randomizer = MyRandomNumbers
            };

            jeviCarArray[2] = new Cars()
            {
                MyPictureBox = Car3,
                CarStartingPosition = Car3.Left,
                TrackLength = pictureBox1.Width - Car3.Width,
                Randomizer = MyRandomNumbers
            };

            jeviCarArray[3] = new Cars()
            {
                MyPictureBox = Car4,
                CarStartingPosition = Car4.Left,
                TrackLength = pictureBox1.Width - Car4.Width,
                Randomizer = MyRandomNumbers
            };

            //initialize all 3 elements of the GuysArray
            jeviPuntersArray[0] = new Punter()
            {
                PunterName = "Sukhman",
                MyBetForCars = null,
                Cashes = 50,
                MyRadioButton = joeRadioButton,
                MyLabel = joeBetLabel
            };

            jeviPuntersArray[1] = new Punter()
            {
                PunterName = "Sid",
                MyBetForCars = null,
                Cashes = 75,
                MyRadioButton = bobRadioButton,
                MyLabel = bobBetLabel
            };

            jeviPuntersArray[2] = new Punter()
            {
                PunterName = "Ajay",
                MyBetForCars = null,
                Cashes = 45,
                MyRadioButton = alRadioButton,
                MyLabel = alBetLabel
            };

            for (int i = 0; i <= 2; i++)
            {
                jeviPuntersArray[i].UpdatingLabels();
                jeviPuntersArray[i].MyBetForCars = new Bet();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i <= 3; i++)
            {
                if (jeviCarArray[i].CarRunning())
                {
                    timer1.Stop();
                    bettingParlor.Enabled = true;
                    i++;
                    MessageBox.Show("Car " + i + " won the race");
                    for (int j = 0; j <= 2; j++)
                    {
                        jeviPuntersArray[j].Collect(i);
                        jeviPuntersArray[j].ClearTheBet();
                    }

                    foreach (Cars car in jeviCarArray)
                    {
                        car.CarsStartingPosition();
                    }
                    break;
                }
            }
        }

        private void joeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            nameLabel.Text = "Sukhman";
        }

        private void bobRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            nameLabel.Text = "Sid";
        }

        private void alRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            nameLabel.Text = "Ajay";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
        }
    }
}
