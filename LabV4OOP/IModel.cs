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
    #region Additional
    public delegate void ModelHandler<IModel>(IModel sender, ModelEventArgs e);
    public delegate void MessageHandler<IModel>(IModel sender, MessageEventArgs e);

    public class MessageEventArgs : EventArgs
    {
        private int _points;
        private static Dictionary<int, String> messages = new Dictionary<int, String>
        {
            {0 , "Nevazne karte" },
            {2 , "One pair" },
            {4 , "Two pairs" },
            {6 , "Three of a kind" },
            {9 , "Blaze" },
            {12 , "Straight" },
            {16 , "Flush" },
            {24 , "Full house" },
            {40 , "Big bobtail" },
            {60 , "Four of a kind" },
            {100 , "Straight Flush" },

            {1, "One pair" },
            {5 , "Two pairs" },
            {8 , "Three of a kind" },
            {13 , "Bobtail straight" },
            {18 , "Little dog" },
            {26 , "Straight" },
            {41 , "Flush" },
            {61 , "Big dog" },
            {90 , "Four of a kind" },
            {150 , "Straight Flush" }
        };

        public MessageEventArgs(int i)
        {
            _points = i;
        }

        public String Message
        {
            get
            {
                return messages[_points];
            }
        }
    }

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
        void UpdateMessage(IModel m, MessageEventArgs e);
        void InitialBet();
        bool PenaltyBet(int x);
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
    #endregion
    /// <summary>
    /// StandardModel
    /// </summary>

    public class StandardModel : IModel
    {
        IModelObserver _imo;
        private event ModelHandler<StandardModel> _display;
        private event ModelHandler<StandardModel> _updatePoints;
        private event MessageHandler<StandardModel> _updateMessage;

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
            _updateMessage += new MessageHandler<StandardModel>(imo.UpdateMessage);
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
            int multiplier = Deck.DeckInstance.WinningHand(_hand, 1);
            _currentPoints += _bet * multiplier;
            _imo.StartRound(false);
            _updateMessage.Invoke(this, new MessageEventArgs(multiplier));
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
                    _hand.Remove(c);
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
        private IModelObserver _imo;
        private event ModelHandler<TexasHoldemModel> _display;
        private event ModelHandler<TexasHoldemModel> _updatePoints;
        private event MessageHandler<TexasHoldemModel> _updateMessage;
        private List<Card> _hand;

        private int _clicked;
        private int _bet;
        private int _currentPoints;

        public TexasHoldemModel(bool type)
        {
            Deck.DeckInstance.InitDeck(type);
        }

        public void Attach(IModelObserver imo)
        {
            _imo = imo;
            _display += new ModelHandler<TexasHoldemModel>(imo.Display);
            _updatePoints += new ModelHandler<TexasHoldemModel>(imo.UpdatePoints);
            _updateMessage += new MessageHandler<TexasHoldemModel>(imo.UpdateMessage);
        }

        public void StartRound(bool beginning)
        {
            if (beginning)
                _currentPoints = 500;
            _clicked = 0;
            _bet = 0;
            if (_hand != null)
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

        private void SetAction()
        {
            for (int i = 2; i < _hand.Count; i++)
            {
                _hand[i].HideCard();
                _hand[i].AddAction(card_Click);
            }
        }

        private void card_Click(object sender, EventArgs e)
        {
            Card tmp = sender as Card;
            if (_clicked == 2 && !PenaltyBet())
                MessageBox.Show("Nemate dovoljno poena da otvorite trecu kartu");
            else
            {
                _clicked++;
                tmp.DisplayCard();
            }
        }

        public void SubmitHand()
        {
            int multiplier = Deck.DeckInstance.WinningHand(_hand, 0);
            int multiplier1 = 1;

            if (_clicked == 0)
                multiplier1 = 5;
            if (_clicked == 1)
                multiplier1 = 2;
            if (_clicked >= 2)
                multiplier1 = 1;

            _currentPoints += _bet * multiplier * multiplier1;
            _imo.StartRound(false);
            if (multiplier1 == 12 || multiplier1 == 40 || multiplier1 == 60)
                multiplier1++;
            _updateMessage.Invoke(this, new MessageEventArgs(multiplier));
        }

        public void Bet(int bet)
        {
            _bet += bet;
            _currentPoints -= bet;
            _updatePoints.Invoke(this, new ModelEventArgs(_hand, _currentPoints));
        }

        public void SetPanels(List<Panel> panels) { }

        public bool PenaltyBet()
        {
            return _imo.PenaltyBet(_bet);
        }

        public void Swap() { }
    }
}
