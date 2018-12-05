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
        public List<Card> hand;
        public ModelEventArgs(List<Card> l)
        {
            hand = l;
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
    }

    /// <summary>
    /// //////////////////////////////////////////////////
    /// </summary>
    
    public class StandardModel : IModel
    {
        public event ModelHandler<StandardModel> changed;
        List<Card> hand;

        public StandardModel(bool type)
        {
            Deck.DeckInstance.InitDeck(type);
            hand = Deck.DeckInstance.GetHand();
        }

        public void Attach(IModelObserver imo)
        {
            changed+= new ModelHandler<StandardModel>(imo.Display);
        }

        public void StartRound()
        {
            changed.Invoke(this, new ModelEventArgs(hand));
        }
    }

    /// <summary>
    /// ////////////////////////////////////////////////
    /// </summary>
    
    public class TexasHoldemModel : IModel
    {
        public event ModelHandler<TexasHoldemModel> changed;
        List<Card> hand;

        public TexasHoldemModel(bool type)
        {
            Deck.DeckInstance.InitDeck(type);
            hand = Deck.DeckInstance.GetHand();
        }

        public void Attach(IModelObserver imo)
        {
            changed += new ModelHandler<TexasHoldemModel>(imo.Display);
        }

        public void StartRound()
        {
            changed.Invoke(this, new ModelEventArgs(hand));
        }
    }
}
