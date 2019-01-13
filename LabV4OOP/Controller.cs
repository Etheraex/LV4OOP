using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker;

namespace LabV4OOP
{
    public abstract class Controller
    {
        protected IView _view;
        protected Model _model;

        public Controller(IView v, Model m)
        {
            _view = v;
            _model = m;
            _model.Attach((IModelObserver)_view);
            _view.SetController(this);
        }

        public void Bet(int bet)
        {
            _model.Bet(bet);
        }

        public void SetPanels(List<Panel> p)
        {
            _model.SetPanels(p);
        }

        public abstract void StartRound(bool beginning);

        public abstract void SubmitHand();

        public abstract void card_Click(object sender, EventArgs e);

        public abstract void ReturnHand();

        public void Swap()
        {
            if (_model.ToSwap.Count != 0)
            {
                foreach (Card c in _model.ToSwap)
                    _model.Hand.Remove(c);
                foreach (Card c in _model.Hand)
                    c.RemoveAction();
                _model.Hand.AddRange(Deck.DeckInstance.Swap(_model.ToSwap));
                _model.Swap();
            }
        }
    }

    /// <summary>
    /// Controller for the standard gameplay mode
    /// </summary>

    public class StandardController : Controller
    {
        public StandardController(IView v, Model m) : base(v, m) { }

        public override void card_Click(object sender, EventArgs e)
        {
            Card tmp = sender as Card;
            int i = 0;
            for (; i < _model.Hand.Count; i++)
                if (_model.Hand[i].IsEqual(tmp))
                    break;

            if (_model.Panels[i].BackColor == Color.Red)
            {
                _model.ToSwap.Remove(_model.Hand[i]);
                _model.Panels[i].BackColor = Color.OliveDrab;
            }
            else if (_model.ToSwap.Count < 3)
            {
                _model.Panels[i].BackColor = Color.Red;
                _model.ToSwap.Add(_model.Hand[i]);
            }
        }

        public override void ReturnHand()
        {
            if (_model.Hand != null)
                for (int i = _model.Hand.Count - 1; i >= 0; i--)
                {
                    Deck.DeckInstance.ReturnCard(_model.Hand[i]);
                    _model.Hand.RemoveAt(i);
                }
        }

        public override void StartRound(bool beginning)
        {
            ReturnHand();
            _model.Initialise(beginning);
            foreach (Card c in _model.Hand)
                c.AddAction(card_Click);
            _model.Display();
        }

        public override void SubmitHand()
        {
            int multiplier = Deck.DeckInstance.WinningHand(_model.Hand, 1);
            _model.CurrentPoints = _model.BetAmount * multiplier;
            StartRound(false);
            _model.SubmitHand(multiplier);
        }
    }

    /// <summary>
    /// Controller for the Texas Holdem gameplay mode
    /// </summary>

    public class TexasHoldemController : Controller
    {
        public TexasHoldemController(IView v, Model m) : base(v, m) { }

        public override void card_Click(object sender, EventArgs e)
        {
            Card tmp = sender as Card;
            if (_model.Clicked == 2 && !_model.PenaltyBet())
                MessageBox.Show("Nemate dovoljno poena da otvorite trecu kartu");
            else
            {
                _model.Clicked++;
                tmp.DisplayCard();
            }
        }

        public override void ReturnHand()
        {
            if (_model.Hand != null)
                for (int i = _model.Hand.Count - 1; i >= 0; i--)
                {
                    _model.Hand[i].DisplayCard();
                    Deck.DeckInstance.ReturnCard(_model.Hand[i]);
                    _model.Hand.RemoveAt(i);
                }
        }

        public override void StartRound(bool beginning)
        {
            ReturnHand();
            _model.Initialise(beginning);
            for (int i = 2; i < _model.Hand.Count; i++)
            {
                _model.Hand[i].HideCard();
                _model.Hand[i].AddAction(card_Click);
            }
            _model.Display();
        }

        public override void SubmitHand()
        {
            int multiplier = Deck.DeckInstance.WinningHand(_model.Hand, 0);
            int multiplier1 = 1;

            if (_model.Clicked == 0)
                multiplier1 = 5;
            else if (_model.Clicked == 1)
                multiplier1 = 2;

            _model.CurrentPoints = _model.BetAmount * multiplier * multiplier1;
            StartRound(false);
            if (multiplier1 == 12 || multiplier1 == 40 || multiplier1 == 60)
                multiplier1++;
            _model.SubmitHand(multiplier);
        }
    }
}
