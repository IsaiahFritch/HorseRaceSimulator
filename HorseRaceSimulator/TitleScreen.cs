using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Media;

namespace HorseRaceSimulator
{
    public partial class TitleScreen : UserControl
    {
        public TitleScreen()
        {
            InitializeComponent();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            // play sound
            System.Media.SoundPlayer startSound = new System.Media.SoundPlayer(Application.StartupPath + "/Resources/HRSstartSound.wav");
            startSound.Play();

            // Open XML file
            XmlReader reader = XmlReader.Create("Resources/savedGame.xml");

            // try to run and if it fails create a new save file
            try
            {
                // read XML file
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Text)
                    {
                        // copy Player information
                        string mA = reader.ReadString();
                        Form1.moneyAmount = Convert.ToInt32(mA);

                        // copy Horse information
                        reader.ReadToFollowing("horseOneActive");
                        string h1A = reader.ReadString();
                        reader.ReadToNextSibling("horseTwoActive");
                        string h2A = reader.ReadString();
                        reader.ReadToNextSibling("horseThreeActive");
                        string h3A = reader.ReadString();
                        reader.ReadToNextSibling("horseOneInjured");
                        string h1I = reader.ReadString();
                        reader.ReadToNextSibling("horseTwoInjured");
                        string h2I = reader.ReadString();
                        reader.ReadToNextSibling("horseThreeInjured");
                        string h3I = reader.ReadString();

                        // convert to bool
                        if (h1A == "true") { Form1.horseOneActive = true; }
                        else { Form1.horseOneActive = false; }
                        if (h2A == "true") { Form1.horseTwoActive = true; }
                        else { Form1.horseTwoActive = false; }
                        if (h3A == "true") { Form1.horseThreeActive = true; }
                        else { Form1.horseThreeActive = false; }

                        if (h1I == "true") { Form1.horseOneInjured = true; }
                        else { Form1.horseOneInjured = false; }
                        if (h2I == "true") { Form1.horseTwoInjured = true; }
                        else { Form1.horseTwoInjured = false; }
                        if (h3I == "true") { Form1.horseThreeInjured = true; }
                        else { Form1.horseThreeInjured = false; }
                    }
                }

                // stop reading
                reader.Close();

                // Launch Menu Screen
                Form1.ChangeScreen(this, new MenuScreen());
            }
            catch
            {
                // stop reading
                reader.Close();

                startNewGame();
            }
        }

        private void newSaveButton_Click(object sender, EventArgs e)
        {
            // play sound
            System.Media.SoundPlayer startSound = new System.Media.SoundPlayer(Application.StartupPath + "/Resources/HRSstartSound.wav");
            startSound.Play();

            startNewGame();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // play sound
            System.Media.SoundPlayer startSound = new System.Media.SoundPlayer(Application.StartupPath + "/Resources/HRSexitSound.wav");
            startSound.Play();

            // End program
            Application.Exit();
        }

        public void startNewGame()
        {
            // Create XML file
            XmlWriter writer = XmlWriter.Create("Resources/savedGame.xml", null);

            // create root element
            writer.WriteStartElement("Save");

            // write Player conditions
            writer.WriteStartElement("player");

            #region player info
            // add info to Player
            writer.WriteElementString("moneyAmount", "500");
            #endregion

            // finish writing Player
            writer.WriteEndElement();

            // write Horse conditions
            writer.WriteStartElement("horse");

            #region horse info
            // add info to Horse
            writer.WriteElementString("horseOneActive", "true");
            writer.WriteElementString("horseTwoActive", "true");
            writer.WriteElementString("horseThreeActive", "true");
            writer.WriteElementString("horseOneInjured", "false");
            writer.WriteElementString("horseTwoInjured", "false");
            writer.WriteElementString("horseThreeInjured", "false");
            #endregion

            // finish writing Horse
            writer.WriteEndElement();

            // finish writing Save
            writer.WriteEndElement();

            // close file
            writer.Close();

            // Set the page number
            Form1.pageNumber = 1;

            // Launch Cut Scene Screen
            Form1.ChangeScreen(this, new CutSceneScreen());
        }
    }
}
