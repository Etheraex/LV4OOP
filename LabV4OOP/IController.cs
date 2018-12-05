using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabV4OOP
{
    public interface IController
    {
        void SubmitHand();
        void StartRound();
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

        }
    }

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


    }
}
