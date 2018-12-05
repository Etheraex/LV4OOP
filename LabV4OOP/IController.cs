﻿using System;
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
        void StartRound(bool beginning);
        void Swap(List<Card> toSwap);
        void Bet(int bet);
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

        public void Bet(int bet)
        {
            _model.Bet(bet);
        }

        public void StartRound(bool beginning)
        {
            _model.StartRound(beginning);
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

        public void Bet(int bet)
        {
            throw new NotImplementedException();
        }

        public void StartRound(bool beginning)
        {
            _model.StartRound(beginning);
        }

        public void SubmitHand()
        {
            throw new NotImplementedException();
        }

        public void Swap(List<Card> toSwap)
        {
            throw new NotImplementedException();
        }
    }
}
