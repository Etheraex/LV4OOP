using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Ruleset
    {
        private Ruleset() { }

        public bool isStrFlush(List<Card> hand)
        {
            if (hand.Count < 5)
                return false;
            if (this.isFlush(hand) && this.isStr(hand))
                return true;
            return false;
        }

        public bool isFlush(List<Card> hand)
        {
            if (hand.Count < 5)
                return false;
            bool temp = true;
            int i = 0;
            while (temp && i < hand.Count - 1)
            {
                if (hand[i].Suit != hand[++i].Suit)
                    temp = false;
            }
            return temp;
        }

        public bool isStr(List<Card> hand)
        {
            if (hand.Count < 5)
                return false;
            bool temp = false;
            int i = 0;
            while (!temp && i < hand.Count - 1)
            {
                temp = strHelper(hand, hand[i++].Value + 1, 0);
            }
            return temp;
        }
        bool strHelper(List<Card> hand, int next, int depth)
        {
            if (hasNumber(hand, next))
            {
                if (depth == hand.Count - 2)
                    return true;
                return strHelper(hand, next + 1, ++depth);
            }
            return false;
        }
        bool hasNumber(List<Card> hand, int num)
        {
            foreach (Card c in hand)
            {
                if (c.Value == num)
                    return true;
            }
            return false;
        }
        public bool isBigBobtail(List<Card> hand)
        {

            if (hand.Count < 4)
                return false;

            List<Card> tempRuka;
            bool temp = false;
            int i = 0;
            while (!temp && i < hand.Count)
            {
                tempRuka = new List<Card>(hand);
                tempRuka.RemoveAt(i++);
                if (isFlush(tempRuka) && isStr(tempRuka))
                    temp = true;
            }
            return temp;
        }
        public bool isFourOfaKind(List<Card> hand)
        {
            if (hand.Count < 4)
                return false;
            bool temp = false;
            int i = 0;
            while (!temp && i < hand.Count)
            {
                temp = this.fourHelper(hand, i++);
            }
            return temp;
        }

        public bool fourHelper(List<Card> hand, int index) // proverava dali ima 4 iste bez karte sa prosledjenim indexom
        {
            bool temp = true;
            int i = 0;
            List<Card> tempRuka = new List<Card>(hand);
            tempRuka.RemoveAt(index);
            while (temp && i < tempRuka.Count - 1)
            {
                if (tempRuka[i].Value != tempRuka[++i].Value)
                    temp = false;
            }
            return temp;
        }

        public bool isTreeOfaKind(List<Card> hand)
        {
            if (hand.Count < 3)
                return false;
            bool temp = false;
            int i = 0;
            while (!temp && i < hand.Count)
            {
                int count = 0;
                foreach (Card karta in hand)
                {
                    if (karta.Value == hand[i].Value)
                        count++;
                }
                if (count == 3)
                    temp = true;
                i++;
            }
            return temp;
        }

        public bool isOnePair(List<Card> hand)
        {
            bool temp = false;
            int i = 0;
            while (!temp && i < hand.Count)
            {
                int count = 0;
                foreach (Card karta in hand)
                {
                    if (karta.Value == hand[i].Value)
                        count++;
                }
                if (count == 2)
                    temp = true;
                i++;
            }
            return temp;
        }
        public bool isTwoPairs(List<Card> hand)
        {
            if (hand.Count < 4)
                return false;
            int i = 0;
            int bigCount = 0;
            while (i < hand.Count)
            {
                int count = 0;
                foreach (Card karta in hand)
                {
                    if (karta.Value == hand[i].Value)
                        count++;
                }
                if (count == 2)
                    bigCount++;
                i++;
            }
            if (bigCount == 4)
                return true;
            return false;
        }
        public bool isBlaze(List<Card> hand)
        {
            if (hand.Count < 5)
                return false;
            bool temp = true;
            int i = 0;
            while (temp && i < hand.Count)
            {
                if (hand[i++].Value < 12)
                    temp = false;
            }
            return temp;
        }
        public bool isFullHouse(List<Card> hand)
        {
            if (isTreeOfaKind(hand))
            {
                if (hand[0].Value == hand[1].Value && hand[3].Value == hand[4].Value)
                    return true;
            }
            return false;
        }

        private static Ruleset _rulesetInstance = null;
        public static Ruleset RulesetInstance
        {
            get
            {
                if (_rulesetInstance == null)
                    _rulesetInstance = new Ruleset();
                return _rulesetInstance;
            }
        }
    }
}
