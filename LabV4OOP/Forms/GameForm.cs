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
        Controller _formController;
        List<Panel> _panels = new List<Panel>(5);

        public GameForm()
        {
            InitializeComponent();
            _panels.Add(p1);
            _panels.Add(p2);
            _panels.Add(p3);
            _panels.Add(p4);
            _panels.Add(p5);
        }

        public void SetController(Controller c)
        {
            _formController = c;
            StartRound(true);
            if (c.GetType() == typeof(TexasHoldemController))
                btnSwap.Visible = false;
        }

        public void StartRound(bool beginning)
        {
            _formController.SetPanels(_panels);
            _formController.StartRound(beginning);
            if (beginning)
                lblLastHand.Text = "";
            lblBetMade.Text = "0";
            UpdateBet(100);
        }

        public void Display(Model m, ModelEventArgs e)
        {
            p1.BackColor = Color.OliveDrab;
            p2.BackColor = Color.OliveDrab;
            p3.BackColor = Color.OliveDrab;
            p4.BackColor = Color.OliveDrab;
            p5.BackColor = Color.OliveDrab;

            cardOne.Controls.Clear();
            cardOne.Controls.Add(e.hand[0]);

            cardTwo.Controls.Clear();
            cardTwo.Controls.Add(e.hand[1]);

            cardThree.Controls.Clear();
            cardThree.Controls.Add(e.hand[2]);

            cardFour.Controls.Clear();
            cardFour.Controls.Add(e.hand[3]);

            cardFive.Controls.Clear();
            cardFive.Controls.Add(e.hand[4]);
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            _formController.Swap();
            btnSwap.Enabled = false;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            _formController.SubmitHand();
            btnSwap.Enabled = true;
        }

        private void bet_Click(object sender, EventArgs e)
        {
            UpdateBet(int.Parse(txtBoxBet.Text));
            _formController.Bet(int.Parse(txtBoxBet.Text));
            txtBoxBet.Text = "";
        }

        public void UpdatePoints(Model m, ModelEventArgs e)
        {
            txtBoxPoints.Text = e.points.ToString();
        }

        public void InitialBet()
        {
            int tmp = int.Parse(txtBoxPoints.Text);
            MessageBox.Show("Current balance: " + tmp.ToString() + " initial bet: 100");
            if (tmp >= 100)
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

        public bool PenaltyBet(int x)
        {
            if (x > int.Parse(txtBoxPoints.Text))
                return false;

            _formController.Bet(x);
            UpdateBet(x);
            return true;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            UpdateBet(int.Parse(txtBoxPoints.Text));
            _formController.Bet(int.Parse(txtBoxPoints.Text));
        }

        public void ResetBet()
        {
            lblBetMade.Text = "100";
        }

        private void UpdateBet(int bet)
        {
            lblBetMade.Text = (int.Parse(lblBetMade.Text) + bet).ToString();
        }

        public void UpdateMessage(Model m, MessageEventArgs e)
        {
            lblLastHand.Text = e.Message;
        }
    }
}
