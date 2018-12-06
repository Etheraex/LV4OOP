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

    public delegate void ModelHandler<IModel>(IModel sender, ModelEventArgs e);

    public class ModelEventArgs : EventArgs
    {
        public int points;
        public List<Card> hand;
        public ModelEventArgs(List<Card> l, int p)
        {
            hand = l;
            points = p;
        }
    }

    public interface IModelObserver
    {
        void StartRound(bool beginning);
        void Display(IModel m, ModelEventArgs e);
        void UpdatePoints(IModel m, ModelEventArgs e);
        void InitialBet();
    }

    public interface IModel
    {
        void Attach(IModelObserver imo);
        void StartRound(bool beginning);
        void SubmitHand();
        void Bet(int bet);
        void SetPanels(List<Panel> panels);
        void Swap();
    }

    /// <summary>
    /// //////////////////////////////////////////////////
    /// </summary>
    

    public class StandardModel : IModel
    {
        IModelObserver _imo;
        private event ModelHandler<StandardModel> _display;
        private event ModelHandler<StandardModel> _updatePoints;
        List<Card> _hand;
        List<Card> _toSwap;
        List<Panel> _panels;
        int _currentPoints;
        int _bet;
        
        public StandardModel(bool type)
        {
            Deck.DeckInstance.InitDeck(type);
        }

        public void Attach(IModelObserver imo)
        {
            _imo = imo;
            _display += new ModelHandler<StandardModel>(imo.Display);
            _updatePoints += new ModelHandler<StandardModel>(imo.UpdatePoints);
        }

        public void StartRound(bool beginning)
        {
            _toSwap = new List<Card>(3);
            if (beginning)
                _currentPoints = 500;
            _bet = 0;
            if(_hand != null)
                for (int i = _hand.Count - 1; i >= 0; i--)
                {
                    Deck.DeckInstance.ReturnCard(_hand[i]);
                    _hand.RemoveAt(i);
                }
            _hand = Deck.DeckInstance.GetHand();
            SetAction();
            _display.Invoke(this, new ModelEventArgs(_hand, _currentPoints));
            _updatePoints.Invoke(this, new ModelEventArgs(_hand, _currentPoints));
            _imo.InitialBet();
        }

        public void SubmitHand()
        {
            int multiplier = Deck.DeckInstance.WinningHand(_hand);
            _currentPoints += _bet * multiplier;
            _imo.StartRound(false);
        }

        public void Bet(int bet)
        {
            _bet += bet;
            _currentPoints -= bet;
            _updatePoints.Invoke(this, new ModelEventArgs(_hand, _currentPoints));
        }

        public void SetPanels(List<Panel> panels)
        {
            _panels = panels;
        }

        private void SetAction()
        {
            foreach (Card c in _hand)
                c.AddAction(card_Click);
        }

        private void card_Click(object sender, EventArgs e)
        {
            Card tmp = sender as Card;
            int i = 0;
            for (; i < _hand.Count; i++)
                if (_hand[i].IsEqual(tmp))
                    break;

            if (_panels[i].BackColor == Color.Red)
            {
                _toSwap.Remove(_hand[i]);
                _panels[i].BackColor = Color.OliveDrab;
            }
            else if (_toSwap.Count < 3)
            {
                _panels[i].BackColor = Color.Red;
                _toSwap.Add(_hand[i]);
            }
        }

        public void Swap()
        {
            if (_toSwap.Count != 0)
            {
                foreach (Card c in _toSwap)
                {
                    Deck.DeckInstance.ReturnCard(c);
                    _hand.Remove(c);
                }
                foreach (Card c in _hand)
                    c.RemoveAction();
                _hand.AddRange(Deck.DeckInstance.Swap(_toSwap));
                _display.Invoke(this, new ModelEventArgs(_hand, _currentPoints));
                _toSwap = new List<Card>(3);
            }
        }
    }

    /// <summary>
    /// Texas holdem model
    /// TODO later
    /// </summary>
    
    public class TexasHoldemModel : IModel
    {
        private event ModelHandler<TexasHoldemModel> _changed;
        List<Card> _hand;

        public TexasHoldemModel(bool type)
        {
            Deck.DeckInstance.InitDeck(type);
            _hand = Deck.DeckInstance.GetHand();
        }

        public void Attach(IModelObserver imo)
        {
            _changed += new ModelHandler<TexasHoldemModel>(imo.Display);
        }

        public void StartRound(bool beginning)
        {
            _changed.Invoke(this, new ModelEventArgs(_hand, 500));
        }

        public void SubmitHand()
        {
            throw new NotImplementedException();
        }

        public void Bet(int bet)
        {
            throw new NotImplementedException();
        }

        public void SetPanels(List<Panel> panels)
        {
            throw new NotImplementedException();
        }

        public void Swap()
        {
            throw new NotImplementedException();
        }
    }
}
