using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker;

namespace LabV4OOP
{
    public interface IController
    {
        void SubmitHand();
        void StartRound();
        void Swap(List<Card> toSwap);
    }

    public class StandardController : IController
    {
        IView _view;
        IModel _model;

        public StandardController(IView v, IModel m)
        {
            _view = v;
            _model = m;
            _model.Attach((IModelObserver)_view);
            _view.SetController(this);
        }

        public void StartRound()
        {
            _model.StartRound();
        }

        public void SubmitHand()
        {
            _model.SubmitHand();
        }

        public void Swap(List<Card> toSwap)
        {
            _model.Swap(toSwap);
        }
    }

    /// <summary>
    /// Texas holdem Controler 
    /// TODO Later
    /// </summary>
    public class TexasHoldemController : IController
    {
        IView _view;
        IModel _model;

        public TexasHoldemController(IView v, IModel m)
        {
            _view = v;
            _model = m;
            _model.Attach((IModelObserver)_view);
            _view.SetController(this);
        }

        public void StartRound()
        {
            _model.StartRound();
        }

        public void SubmitHand()
        {

        }

        public void Swap(List<Card> toSwap)
        {
            throw new NotImplementedException();
        }
    }
}
