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

        public MessageEventArgs(int i) { _points = i; }

        public String Message { get { return messages[_points]; } }
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
        void ResetBet();
        bool PenaltyBet(int x);
    }

    public abstract class Model
    {
        #region Data
        IModelObserver _imo;
        private event ModelHandler<StandardModel> _display;
        private event ModelHandler<StandardModel> _updatePoints;
        private event MessageHandler<StandardModel> _updateMessage;

        protected List<Card> _hand;
        List<Panel> _panels;
        List<Card> _toSwap;

        protected int _currentPoints;
        protected int _bet;
        private int _clicked;
        #endregion

        #region Properties
        public List<Card> Hand { get { return _hand; } }
        public List<Panel> Panels { get { return _panels; } }
        public List<Card> ToSwap { get { return _toSwap; } }
        public int Clicked { get { return _clicked; } set { _clicked = value; } }
        public int BetAmount { get { return _bet; } set { _bet = value; } }
        public int CurrentPoints { get { return _bet; } set { _currentPoints += value; } }
        #endregion

        public void Initialise(bool beginning)
        {
            _toSwap = new List<Card>(3);
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

        public bool PenaltyBet() { return _imo.PenaltyBet(_bet); }

        public void SubmitHand(int x)
        {
            _updateMessage.Invoke(this, new MessageEventArgs(x));
            _imo.ResetBet();
        }

        public void SetPanels(List<Panel> panels) { _panels = panels; }

        public void Swap()
        {
            _display.Invoke(this, new ModelEventArgs(_hand, _currentPoints));
            _toSwap = new List<Card>(3);
        }
    }

    /// <summary>
    /// Standard Model
    /// </summary>

    public class StandardModel : Model
    {
        public StandardModel() { Deck.DeckInstance.InitDeck(true); }
    }

    /// <summary>
    /// French Model
    /// </summary>

    public class FrenchModel : Model
    {
        public FrenchModel() { Deck.DeckInstance.InitDeck(false); }
    }
}
