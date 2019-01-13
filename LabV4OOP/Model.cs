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
    public delegate void ModelHandler<IModel>(Model sender, ModelEventArgs e);
    public delegate void MessageHandler<IModel>(Model sender, MessageEventArgs e);

    #region Messages
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
    #endregion

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

    /// <summary>
    /// For the GameForm class
    /// </summary>
    public interface IModelObserver
    {
        void StartRound(bool beginning);
        void Display(Model m, ModelEventArgs e);
        void UpdatePoints(Model m, ModelEventArgs e);
        void UpdateMessage(Model m, MessageEventArgs e);
        void InitialBet();
        bool PenaltyBet(int x);
    }

    public abstract class Model
    {
        IModelObserver _imo;
        private event ModelHandler<StandardModel> _display;
        private event ModelHandler<StandardModel> _updatePoints;
        private event MessageHandler<StandardModel> _updateMessage;

        protected List<Card> _hand;
        List<Panel> _panels;
        List<Card> _toSwap;

        public void SwapInit()
        {
            _toSwap = new List<Card>(3);
        }

        protected int _currentPoints;
        protected int _bet;
        private int _clicked;

        public void Initialise(bool beginning)
        {
            if (beginning)
                _currentPoints = 500;
            _clicked = 0;
            _bet = 0;
            _hand = Deck.DeckInstance.GetHand();
        }

        public void Display()
        {
            _display.Invoke(this, new ModelEventArgs(_hand, _currentPoints));
            _updatePoints.Invoke(this, new ModelEventArgs(_hand, _currentPoints));
            _imo.InitialBet();
        }

        public void Attach(IModelObserver imo)
        {
            _imo = imo;
            _display += new ModelHandler<StandardModel>(imo.Display);
            _updatePoints += new ModelHandler<StandardModel>(imo.UpdatePoints);
            _updateMessage += new MessageHandler<StandardModel>(imo.UpdateMessage);
        }

        public void Bet(int bet)
        {
            _bet += bet;
            _currentPoints -= bet;
            _updatePoints.Invoke(this, new ModelEventArgs(_hand, _currentPoints));
        }

        public void EmptyStandardHand()
        {
            if (_hand != null)
                for (int i = _hand.Count - 1; i >= 0; i--)
                {
                    Deck.DeckInstance.ReturnCard(_hand[i]);
                    _hand.RemoveAt(i);
                }
        }

        public void EmptyTexasHand()
        {
            if (_hand != null)
                for (int i = _hand.Count - 1; i >= 0; i--)
                {
                    _hand[i].DisplayCard();
                    Deck.DeckInstance.ReturnCard(_hand[i]);
                    _hand.RemoveAt(i);
                }
        }

        public void SetTexasAction()
        {
            for (int i = 2; i < _hand.Count; i++)
            {
                _hand[i].HideCard();
                _hand[i].AddAction(card_ClickTexas);
            }
        }

        private void card_ClickStandard(object sender, EventArgs e)
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

        public void SetStandardAction()
        {
            foreach (Card c in _hand)
                c.AddAction(card_ClickStandard);
        }

        private void card_ClickTexas(object sender, EventArgs e)
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

        public bool PenaltyBet()
        {
            return _imo.PenaltyBet(_bet);
        }

        public void SubmitStandardHand()
        {
            int multiplier = Deck.DeckInstance.WinningHand(_hand, 1);
            _currentPoints += _bet * multiplier;
            _imo.StartRound(false);
            _updateMessage.Invoke(this, new MessageEventArgs(multiplier));
        }

        public void SubmitTexasHand()
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

        public void SetPanels(List<Panel> panels)
        {
            _panels = panels;
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
    /// Standard Model
    /// </summary>

    public class StandardModel : Model
    {
        public StandardModel()
        {
            Deck.DeckInstance.InitDeck(true);
        }
    }


    /// <summary>
    /// French Model
    /// </summary>

    public class FrenchModel : Model
    {
        public FrenchModel()
        {
            Deck.DeckInstance.InitDeck(false);
        }
    }
}
