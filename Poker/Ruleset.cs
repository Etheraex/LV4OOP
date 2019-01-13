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

        public int GetResult(List<Card> hand, int type)
        {
            // Standard and Texas
            if (StraightFlush(hand))
            {
                if (type == 1)
                    return 100; //Standard
                else
                    return 150; //Texas
            }

            // Standard and Texas
            if (FourOfaKind(hand))
            {
                if (type == 1)
                    return 60;  //Standard
                else
                    return 90;  //Texas
            }

            // Only Texas
            if (type == 0 && BigDog(hand))
                return 60;

            // Only standard
            if (type==1 && BigBobtail(hand))
                return 40;

            // Only standard
            if (type == 1 && FullHouse(hand))
                return 24;

            // Standard and Texas
            if (Flush(hand))
            {
                if (type == 1)
                    return 16;  //Standard
                else
                    return 40;  //Texas
            }

            // Standard and Texas
            if (Straight(hand))
            {
                if (type == 1)
                    return 12;  //Standard
                else
                    return 26;  //Texas
            }

            //Only Texas
            if (type == 0 && BobtailStraight(hand))
                return 18;

            //Only Texas
            if (type == 0 && LittleDog(hand))
                return 12;

            // Only standard
            if (type == 1 && Blaze(hand))
                return 9;

            // Standard and Texas
            if (TreeOfaKind(hand))
            {
                if (type == 1)
                    return 6;  //Standard
                else
                    return 8;  //Texas
            }

            // Standard and Texas
            if (TwoPairs(hand))
            {
                if (type == 1)
                    return 4;  //Standard
                else
                    return 5;  //Texas
            }

            // Standard and Texas
            if (OnePair(hand))
            {
                if (type == 1)
                    return 2;  //Standard
                else
                    return 1;  //Texas
            }

            return 0;
        }

        private List<Card> Sort(List<Card> hand)
        {
            Card tmp;
            for (int i = 0; i < hand.Count - 1; i++)
                for (int j = 0; j < hand.Count - i - 1; j++)
                    if (hand[j].Value > hand[j + 1].Value)
                    {
                        tmp = hand[j];
                        hand[j] = hand[j + 1];
                        hand[j + 1] = tmp;
                    }
            return hand;
        }

        private bool BobtailStraight(List<Card> hand)
        {
            if (hand[0].Value < hand[1].Value && hand[1].Value < hand[2].Value && hand[2].Value < hand[3].Value)
                return false;
            if (hand[1].Value < hand[2].Value && hand[2].Value < hand[3].Value && hand[3].Value < hand[4].Value)
                return false;
            return false;
        }

        private bool BigDog(List<Card> hand)
        {
            bool high = false;
            bool low = false;
            foreach (Card c in hand)
            {
                if (c.Value == 9)
                    low = true;
                else if (c.Value == 11)
                    high = true;
            }
            if (low && high && hand[0].Value < hand[1].Value && hand[1].Value < hand[2].Value && hand[2].Value < hand[3].Value && hand[3].Value < hand[4].Value)
                return true;
            return false;
        }

        private bool LittleDog(List<Card> hand)
        {
            bool high = false;
            bool low = false;
            foreach (Card c in hand)
            {
                if (c.Value == 2)
                    low = true;
                else if (c.Value == 7)
                    high = true;
            }
            if (low && high && hand[0].Value < hand[1].Value && hand[1].Value < hand[2].Value && hand[2].Value < hand[3].Value && hand[3].Value < hand[4].Value)
                return false;
            return false;
        }

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
            hand = Sort(hand);
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
