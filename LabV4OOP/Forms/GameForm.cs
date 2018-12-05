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

        public GameForm()
        {
            InitializeComponent();
        }

        public void SetController(IController c)
        {
            _formController = c;
            StartRound();
        }

        private void StartRound()
        {
            _formController.StartRound();
        }

        public void Display(IModel m, ModelEventArgs e)
        {
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
    }
}
