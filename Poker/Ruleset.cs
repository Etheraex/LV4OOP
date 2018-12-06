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

        public bool StraightFlush(List<Card> hand)
        {
            if (hand.Count < 5)
                return false;
            if (Flush(hand) && Straight(hand))
                return true;
            return false;
        }

        public bool Flush(List<Card> hand)
        {
            if (hand.Count < 5)
                return false;
            bool tmp = true;
            int i = 0;
            while (tmp && i < hand.Count - 1)
            {
                if (hand[i].Suit != hand[++i].Suit)
                    tmp = false;
            }
            return tmp;
        }

        public bool Straight(List<Card> hand)
        {
            if (hand.Count < 5)
                return false;
            bool tmp = false;
            int i = 0;
            while (!tmp && i < hand.Count - 1)
            {
                tmp = StraightHelper(hand, hand[i++].Value + 1, 0);
            }
            return tmp;
        }

        bool StraightHelper(List<Card> hand, int next, int depth)
        {
            if (HasNumber(hand, next))
            {
                if (depth == hand.Count - 2)
                    return true;
                return StraightHelper(hand, next + 1, ++depth);
            }
            return false;
        }

        bool HasNumber(List<Card> hand, int number)
        {
            foreach (Card c in hand)
            {
                if (c.Value == number)
                    return true;
            }
            return false;
        }

        public bool BigBobtail(List<Card> hand)
        {
            if (hand.Count < 4)
                return false;

            List<Card> tmpHand;
            bool tmp = false;
            int i = 0;
            while (!tmp && i < hand.Count)
            {
                tmpHand = new List<Card>(hand);
                tmpHand.RemoveAt(i++);
                if (Flush(tmpHand) && Straight(tmpHand))
                    tmp = true;
            }
            return tmp;
        }

        public bool FourOfaKind(List<Card> hand)
        {
            if (hand.Count < 4)
                return false;
            bool tmp = false;
            int i = 0;
            while (!tmp && i < hand.Count)
            {
                tmp = this.FourHelper(hand, i++);
            }
            return tmp;
        }

        public bool FourHelper(List<Card> hand, int index)
        {
            bool tmp = true;
            int i = 0;
            List<Card> tmpHand = new List<Card>(hand);
            tmpHand.RemoveAt(index);
            while (tmp && i < tmpHand.Count - 1)
            {
                if (tmpHand[i].Value != tmpHand[++i].Value)
                    tmp = false;
            }
            return tmp;
        }

        public bool TreeOfaKind(List<Card> hand)
        {
            if (hand.Count < 3)
                return false;
            bool tmp = false;
            int i = 0;
            while (!tmp && i < hand.Count)
            {
                int count = 0;
                foreach (Card card in hand)
                {
                    if (card.Value == hand[i].Value)
                        count++;
                }
                if (count == 3)
                    tmp = true;
                i++;
            }
            return tmp;
        }

        public bool OnePair(List<Card> hand)
        {
            bool tmp = false;
            int i = 0;
            while (!tmp && i < hand.Count)
            {
                int count = 0;
                foreach (Card card in hand)
                {
                    if (card.Value == hand[i].Value)
                        count++;
                }
                if (count == 2)
                    tmp = true;
                i++;
            }
            return tmp;
        }

        public bool TwoPairs(List<Card> hand)
        {
            if (hand.Count < 4)
                return false;
            int i = 0;
            int bigCount = 0;
            while (i < hand.Count)
            {
                int count = 0;
                foreach (Card card in hand)
                {
                    if (card.Value == hand[i].Value)
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

        public bool Blaze(List<Card> hand)
        {
            if (hand.Count < 5)
                return false;
            bool tmp = true;
            int i = 0;
            while (tmp && i < hand.Count)
            {
                if (hand[i++].Value < 12)
                    tmp = false;
            }
            return tmp;
        }

        public bool FullHouse(List<Card> hand)
        {
            if (TreeOfaKind(hand))
            {
                if ((hand[0].Value == hand[1].Value) && (hand[3].Value == hand[4].Value))
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
