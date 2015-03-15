using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolySimulation
{
    class MonopolySimulation
    {
        //Chance = 7, 22, 36
        // 1/16 go to GO(0)
        // 1/16 go to C1 (11)
        // 1/16 go to E3 (24)
        // 1/16 go to H2 (39)
        // 1/16 go to R1 (5)
        // 2/16 go to next R (5,15,25,35)
        // 1/16 go to next U (12, 28)
        // 1/16 go back 3 squares
        private enum ChanceCards
        {
            Go = 0,
            C1 = 11,
            E3 = 24,
            H2 = 39,
            R1 = 5,
            NextRR,
            NextU,
            GoBack3,
            Nop
        };

        private void ResetChanceCards()
        {
            List<ChanceCards> cards =
                ((ChanceCards[]) Enum.GetValues(typeof (ChanceCards))).Where(c => c != ChanceCards.Nop).ToList();
            //one extra next railroad
            cards.Add(ChanceCards.NextRR);
            while(cards.Count != 16)
                cards.Add(ChanceCards.Nop);

            chanceQueue = new Queue<ChanceCards>(cards.OrderBy(c => random.Next()));

        }

        //Community Chest = 2,17,33
        //  1/16 CC = go to GO(0), 
        // 1/16 CC = Go to Jail(10)
        enum CommunityChestCards
        {
            Go=0,
            Jail=10,
            Nop
        }

        private void ResetCommunityChestCards()
        {
            List<CommunityChestCards> cards = new List<CommunityChestCards>
                {
                    CommunityChestCards.Go,
                    CommunityChestCards.Jail
                };
            while(cards.Count != 16)
                cards.Add(CommunityChestCards.Nop);
            communityChestQueue = new Queue<CommunityChestCards>(cards.OrderBy(c => random.Next()));

        }

        
        private Random random;
        private int dieSides;

        private Queue<ChanceCards> chanceQueue;
        private Queue<CommunityChestCards> communityChestQueue;

        public Dictionary<int, int> SquareHistogram { get; private set; }

        private int square;
        private int doublesCount;

        private static readonly int GO_TO_JAIL = 30;
        private static readonly int JAIL = 10;
        private static readonly int[] CC = {2, 17, 33};
        private static readonly int[] CHANCE = {7, 22, 36};
        //not including railroad and utilities
        private static readonly int[] RAILROADS = {5, 15, 25, 35};
        private static readonly int[] UTILITIES = {12, 28};


        public void Reset()
        {
            random = new Random();
            doublesCount = 0;
            SquareHistogram = new Dictionary<int, int>();
            ResetChanceCards();
            ResetCommunityChestCards();
            SquareHistogram = Enumerable.Range(0, 40).ToDictionary(i => i, i => 0);
            square = 0;
        }

        private Tuple<int, int> GetRoll()
        {
            return Tuple.Create(random.Next(dieSides) + 1, random.Next(dieSides) + 1);
        }

        
        private void DoChance()
        {
            var nextChance = chanceQueue.Dequeue();
            switch (nextChance)
            {
                case ChanceCards.Nop:
                    chanceQueue.Enqueue(nextChance);
                    return; 
                case ChanceCards.GoBack3:
                    square -= 3;
                    square = square < 0 ? square + 40 : square;
                    break;
                case ChanceCards.NextRR:
                    while (!RAILROADS.Contains(square))
                    {
                        square = (square + 1)%40;
                    }
                    break;
                case ChanceCards.NextU:
                    while (!UTILITIES.Contains(square))
                    {
                        square = (square + 1)%40;
                    }
                    break;
                default:
                    square = (int) nextChance;
                    break;
            }

            SquareHistogram[square]++;

            chanceQueue.Enqueue(nextChance);

            ActOnSquare();

        }

        private void DoCommunityChest()
        {
            var nextCC = communityChestQueue.Dequeue();
            switch (nextCC)
            {
                case CommunityChestCards.Nop:
                    communityChestQueue.Enqueue(nextCC);
                    return;
                default:
                    square = (int) nextCC;
                    break;
            }

            SquareHistogram[square]++;
            communityChestQueue.Enqueue(nextCC);
            ActOnSquare();
        }

        public void Step()
        {
            var roll = GetRoll();

            doublesCount = roll.Item1 == roll.Item2 ? doublesCount + 1 : 0;

            if (doublesCount == 3)
            {
                doublesCount = 0;
                GoToJail();
                return;
            }

            square = (square + roll.Item1 + roll.Item2)%40;
            SquareHistogram[square]++;

            ActOnSquare();
        }

        private void ActOnSquare(){

            if (square == GO_TO_JAIL)
            {
                square = JAIL;
                SquareHistogram[square]++;
            }
            else if (CC.Contains(square))
            {
                DoCommunityChest();
            }
            else if (CHANCE.Contains(square))
            {
                DoChance();
            }

        }

        public void GoToJail()
        {
            square = 10;
            SquareHistogram[square]++;
        }

        public MonopolySimulation(int dieSides=6)
        {
            this.dieSides = dieSides;
            Reset();
        }
    }
    class Program
    {
        

        static void Main(string[] args)
        {
            var sim = new MonopolySimulation(4);
            Dictionary<int, int> globalHistogram = Enumerable.Range(0, 40).ToDictionary(i => i, i => 0);

            //play 100 games with 10,000 turns each
            for (int i = 0; i < 100; i++)
            {
                sim.Reset();
                for (int j = 0; j < 10000; j++)
                {
                    sim.Step();
                }
                foreach (var kvp in sim.SquareHistogram)
                {
                    globalHistogram[kvp.Key] += kvp.Value;
                }
            }

            var total = globalHistogram.Values.Sum();
            var percentages = globalHistogram.ToDictionary(kvp => kvp.Key, kvp => (double)kvp.Value/(double)total);

            var top3 = globalHistogram.OrderBy(kvp => -kvp.Value).Select(kvp => kvp.Key).Take(3).ToList();

            var answer = String.Join("", top3.Select(i => i.ToString("D2")));
            Console.WriteLine(answer);
            Console.ReadLine();
        }
    }
}
