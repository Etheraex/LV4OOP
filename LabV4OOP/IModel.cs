using System;
using System.Collections.Generic;
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
        void Display(IModel m, ModelEventArgs e);
    }

    public interface IModel
    {
        void Attach(IModelObserver imo);
        void StartRound();
        void Swap(List<Card> toSwap);
        void SubmitHand();
    }

    /// <summary>
    /// //////////////////////////////////////////////////
    /// </summary>
    

    public class StandardModel : IModel
    {
        private event ModelHandler<StandardModel> _changed;
        List<Card> _hand;
        int _currentPoints;

        public StandardModel(bool type)
        {
            Deck.DeckInstance.InitDeck(type);
            _hand = Deck.DeckInstance.GetHand();
            _currentPoints = 500;
        }

        public void Attach(IModelObserver imo)
        {
            _changed += new ModelHandler<StandardModel>(imo.Display);
        }

        public void StartRound()
        {
            _changed.Invoke(this, new ModelEventArgs(_hand, _currentPoints));
        }

        public void Swap(List<Card> toSwap)
        {
            foreach (Card c in toSwap)
                _hand.Remove(c);
            foreach (Card c in _hand)
                c.RemoveAction();
            _hand.AddRange(Deck.DeckInstance.Swap(toSwap));
            _changed.Invoke(this, new ModelEventArgs(_hand, _currentPoints));
        }

        public void SubmitHand()
        {
            int multiplier = Deck.DeckInstance.WinningHand(_hand);
            _currentPoints += 10 * multiplier;
            for(int i = _hand.Count-1; i >= 0; i--)
            {
                Deck.DeckInstance.ReturnCard(_hand[i]);
                _hand.RemoveAt(i);
            }
            _hand = Deck.DeckInstance.GetHand();
            _changed.Invoke(this, new ModelEventArgs(_hand, _currentPoints));
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

        public void StartRound()
        {
            _changed.Invoke(this, new ModelEventArgs(_hand, 500));
        }

        public void Swap(List<Card> toSwap)
        {
            throw new NotImplementedException();
        }

        public void SubmitHand()
        {
            throw new NotImplementedException();
        }
    }
}
