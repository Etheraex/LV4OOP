using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Poker;

namespace LabV4OOP
{
    public partial class GameForm : Form, IView, IModelObserver
    {
        IController _formController;
        List<Card> _hand;
        List<Card> toSwap = new List<Card>(3);
        int _bet;

        public GameForm()
        {
            InitializeComponent();
        }

        public void SetController(IController c)
        {
            _formController = c;
            _formController.StartRound(true);
            _bet = 100;
            UpdateBet();
        }

        public void StartRound(bool beginning)
        {
            _formController.StartRound(beginning);
            _bet = 100;
            UpdateBet();
        }

        public void Display(IModel m, ModelEventArgs e)
        {
            _hand = e.hand;
            p1.BackColor = SystemColors.Control;
            p2.BackColor = SystemColors.Control;
            p3.BackColor = SystemColors.Control;
            p4.BackColor = SystemColors.Control;
            p5.BackColor = SystemColors.Control;
            cardOne.Controls.Clear();
            e.hand[0].AddAction(cardOne_Click);
            cardOne.Controls.Add(e.hand[0]);
            cardTwo.Controls.Clear();
            e.hand[1].AddAction(cardTwo_Click);
            cardTwo.Controls.Add(e.hand[1]);
            cardThree.Controls.Clear();
            e.hand[2].AddAction(cardThree_Click);
            cardThree.Controls.Add(e.hand[2]);
            cardFour.Controls.Clear();
            e.hand[3].AddAction(cardFour_Click);
            cardFour.Controls.Add(e.hand[3]);
            cardFive.Controls.Clear();
            e.hand[4].AddAction(cardFive_Click);
            cardFive.Controls.Add(e.hand[4]);
        }

        #region CardSelect
        private void cardOne_Click(object sender, EventArgs e)
        {
            if (p1.BackColor == Color.Red)
            {
                toSwap.Remove(_hand[0]);
                p1.BackColor = SystemColors.Control;
            }
            else if(toSwap.Count < 3)
            { 
                p1.BackColor = Color.Red;
                toSwap.Add(_hand[0]);
            }
        }

        private void cardTwo_Click(object sender, EventArgs e)
        {
            if (p2.BackColor == Color.Red)
            {
                toSwap.Remove(_hand[1]);
                p2.BackColor = SystemColors.Control;
            }
            else if (toSwap.Count < 3)
            {
                p2.BackColor = Color.Red;
                toSwap.Add(_hand[1]);
            }
        }

        private void cardThree_Click(object sender, EventArgs e)
        {
            if (p3.BackColor == Color.Red)
            {
                toSwap.Remove(_hand[2]);
                p3.BackColor = SystemColors.Control;
            }
            else if(toSwap.Count < 3)
            {
                p3.BackColor = Color.Red;
                toSwap.Add(_hand[2]);
            }
        }

        private void cardFour_Click(object sender, EventArgs e)
        {
            if (p4.BackColor == Color.Red)
            {
                toSwap.Remove(_hand[3]);
                p4.BackColor = SystemColors.Control;
            }
            else if (toSwap.Count < 3)
            {
                p4.BackColor = Color.Red;
                toSwap.Add(_hand[3]);
            }
        }

        private void cardFive_Click(object sender, EventArgs e)
        {
            if (p5.BackColor == Color.Red)
            {
                toSwap.Remove(_hand[4]);
                p5.BackColor = SystemColors.Control;
            }
            else if (toSwap.Count < 3)
            {
                p5.BackColor = Color.Red;
                toSwap.Add(_hand[4]);
            }
        }
        #endregion

        private void btnSwap_Click(object sender, EventArgs e)
        {
            if (toSwap.Count != 0)
            {
                _formController.Swap(toSwap);
                toSwap = new List<Card>(3);
            }
            btnSwap.Enabled = false;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            _formController.SubmitHand();
            btnSwap.Enabled = true;
        }

        private void bet_Click(object sender, EventArgs e)
        {
            _bet += int.Parse(txtBoxBet.Text);
            _formController.Bet(int.Parse(txtBoxBet.Text));
            UpdateBet();
        }

        public void UpdatePoints(IModel m, ModelEventArgs e)
        {
            txtBoxPoints.Text = e.points.ToString();
        }

        public void InitialBet()
        {
            int tmp = int.Parse(txtBoxPoints.Text);
            MessageBox.Show("Current balance: "+ tmp.ToString()+" initial bet: 100");
            if (tmp > 100)
            {
                _formController.Bet(100);
            }
            else
            {
                GameOverForm gof = new GameOverForm();
                DialogResult dr = gof.ShowDialog();
                if (dr == DialogResult.Retry)
                    StartRound(true);
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            _bet += int.Parse(txtBoxPoints.Text);
            _formController.Bet(int.Parse(txtBoxPoints.Text));
            UpdateBet();
        }

        private void UpdateBet()
        {
            lblBetMade.Text = _bet.ToString();
        }
    }
}
