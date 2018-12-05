using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Poker
{
    public class Deck
    {
        List<Card> _deck = new List<Card>();
        // Standard = true
        // French = false
        bool _type;

        private Deck() { }

        private void LoadDeck()
        {
            foreach(String f in Directory.GetFiles("../../../Data/Cards.", "*.png"))
            {
                if (!_type)
                {
                    int number;
                    Regex regex = new Regex("__(.*).png");
                    Match pattern = regex.Match(f);
                    if (pattern.Success)
                    {
                        String s = pattern.Groups[1].ToString();
                        if (!int.TryParse(s, out number))
                            number = 10;
                        if(number >= 7)
                            _deck.Add(new Card(f));
                    }
                }
                else
                    _deck.Add(new Card(f));
            }
            Shuffle();
        }

        public void InitDeck(bool t)
        {
            _type = t;
            LoadDeck();
        }

        private void Shuffle()
        {
            List<Card> randomList = new List<Card>();

            Random r = new Random();
            int randomIndex = 0;
            while (_deck.Count > 0)
            {
                randomIndex = r.Next(0, _deck.Count);
                randomList.Add(_deck[randomIndex]);
                _deck.RemoveAt(randomIndex);
            }
            _deck = randomList;
        }

        public List<Card> GetHand()
        {
            List<Card> hand = new List<Card>(5);
            for (int i = 0; i < 5; i++)
                hand.Add(_deck[i]);
            return hand;
        }

        public int Count
        {
            get
            {
                return _deck.Count;
            }
        }

        private static Deck _deckInstance = null;
        public static Deck DeckInstance
        {
            get
            {
                if (_deckInstance == null)
                    _deckInstance = new Deck();
                return _deckInstance;
            }
        }
    }
}
