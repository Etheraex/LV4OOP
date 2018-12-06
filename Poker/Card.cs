using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker
{
    public enum Suits
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades,
        Default
    }

    public class Card : PictureBox
    {
        private Suits _suit;
        private int _value;
        private String _backGround = "../../../Data/Backgrounds/red.png";
        private String _image;
        private EventHandler _e;

        #region Properties
        public String Suit { get { return _suit.ToString(); } }
        public int Value { get { return _value; } }
        #endregion

        public bool IsEqual(Card c)
        {
            return ((_value == c.Value) && (Suit == c.Suit)) ? true : false;
        }

        public Card(String image)
        {
            _image = image;
            base.ImageLocation = _image;
            base.Width = 71;
            base.Height = 94;
            base.SizeMode = PictureBoxSizeMode.StretchImage;
            _suit = CardSuit();
            _value = CardValue();
        }

        #region RegexStuff
        public Suits CardSuit()
        {
            Regex regex = new Regex("_(.*)__");
            Match pattern = regex.Match(_image);
            if (pattern.Success)
            {
                String s = pattern.Groups[1].ToString();
                switch (s)
                {
                    case "D":
                        return Suits.Diamonds;
                    case "H":
                        return Suits.Hearts;
                    case "S":
                        return Suits.Spades;
                    case "C":
                        return Suits.Clubs;
                }
            }
            return Suits.Default;
        }

        public int CardValue()
        {
            int number;
            Regex regex = new Regex("__(.*).png");
            Match pattern = regex.Match(_image);
            if (pattern.Success)
            {
                String s = pattern.Groups[1].ToString();
                if (int.TryParse(s, out number))
                    return number;
                else
                    switch(s)
                    {
                        case "A":
                            return 11;
                        case "J":
                            return 12;
                        case "Q":
                            return 13;
                        case "K":
                            return 14;
                    }   
            }
            return 0;
        }
        #endregion
        public void AddAction(EventHandler e)
        {
            _e = e;
            base.Click += _e;
        }

        public void RemoveAction()
        {
            base.Click -= _e;
        }
    }
}
