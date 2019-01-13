using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public void Swap()
        {
            _model.Swap();
        }
    }

    /// <summary>
    /// Controller for the standard gameplay mode
    /// </summary>

    public class StandardController : Controller
    {
        public StandardController(IView v, Model m) : base(v, m) { }

        public override void StartRound(bool beginning)
        {
            _model.SwapInit();
            _model.EmptyStandardHand();
            _model.Initialise(beginning);
            _model.SetStandardAction();
            _model.Display();
        }

        public override void SubmitHand()
        {
            _model.SubmitStandardHand();
        }
    }


    /// <summary>
    /// Controller for the Texas Holdem gameplay mode
    /// </summary>

    public class TexasHoldemController : Controller
    {
        public TexasHoldemController(IView v, Model m) : base(v, m) { }

        public override void StartRound(bool beginning)
        {
            _model.EmptyTexasHand();
            _model.Initialise(beginning);
            _model.SetTexasAction();
            _model.Display();
        }

        public override void SubmitHand()
        {
            _model.SubmitTexasHand();
        }
    }
}
