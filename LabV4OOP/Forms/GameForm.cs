using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker;

namespace LabV4OOP
{
    public partial class GameForm : Form
    {
        List<Card> test = new List<Card>();
        public GameForm()
        {
            InitializeComponent();
            Deck.DeckInstance.InitDeck(false);
            test = Deck.DeckInstance.GetCards();
            asd();
        }

        private async Task asd()
        {
            foreach (Card c in test)
            {
                pictureBox1.Controls.Clear();
                pictureBox1.Controls.Add(c);
                textBox1.Text = c.Value.ToString();
                textBox2.Text = c.Suit;
                await Task.Delay(1500);
            }
        }
    }
}
