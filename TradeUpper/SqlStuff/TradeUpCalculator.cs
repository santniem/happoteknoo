using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUpper.Modes;

namespace TradeUpper.SqlStuff
{
    public class TradeUpCalculator
    {

        public List<Skin> bestOutcomes = new List<Skin>();
        private List<Skin> InputSkins;
        private Dictionary<string, int> CollectionSkinCounts = new Dictionary<string, int>();
        public List<string> selectedCollections => InputSkins.Select(x => x.collectionName).Distinct().ToList();

        public void setInputSkins(IEnumerable<Skin> inputSkins)
        {
            if (inputSkins == null || inputSkins.Count() != 10)
            {
                throw new ArgumentException("FUCKED");
            }
            InputSkins = inputSkins.ToList();

        }

        public void setOutputSkins(IEnumerable<Skin> OutPutSkins)
        {
            bestOutcomes.Clear();

            var AvailableOutComes = OutPutSkins.Where(x => CalculateAverageFloat(InputSkins) < x.FloatTarget);
            var Outputs = AvailableOutComes.GroupBy(x => x.name);
            foreach (var skinGroup in Outputs)
            {
                var bestPossibleExtreriorForAvgFloat = skinGroup.Min(x => x.Exterior);
                var bestOutcome = skinGroup.First(x => x.Exterior == bestPossibleExtreriorForAvgFloat);
                bestOutcomes.Add(bestOutcome);
            }
            var d = bestOutcomes.Select(x => x.collectionName).Distinct();
            foreach (var item in d)
            {
                var cnt = bestOutcomes.Where(x => x.collectionName == item).Count();
                if (!CollectionSkinCounts.ContainsKey(item))
                {
                    CollectionSkinCounts.Add(item, cnt);
                }

            }
        }

        /*
Factory New (0.00 – 0.07)
Minimal Wear (0.07 – 0.15)
Field-Tested (0.15 – 0.38)
Well-Worn (0.38 – 0.45)
Battle-Scarred (0.45 – 1.00)
           */

        public float CalculateAverageFloat(List<Skin> InputSkins)
        {
            return InputSkins.Average(x => x.SkinFloat);
        }

        public double CalculateInputPrice(List<Skin> InputSkins)
        {
            return InputSkins.Sum(x => x.Price);
        }

        public double TotalExpectedValue = 0;

        public bool DoCalc()
        {
            if (bestOutcomes.Count == 0)
            {
                return false;
                //throw new Exception("SET THE SKINS FIRST RETARD");
            }
            //Console.WriteLine("OUTCOMES");

            foreach (var bestOutcome in bestOutcomes)
            {
                var inputCount = InputSkins.Where(x => x.collectionName == bestOutcome.collectionName).Count();
                var groupFactor = inputCount * 0.1;

                var outPutCollectionSkinCount = CollectionSkinCounts[bestOutcome.collectionName];

                var luck = groupFactor / outPutCollectionSkinCount;
                var ev = bestOutcome.Price * luck;
                TotalExpectedValue += ev;
                //  Console.WriteLine($"[{bestOutcome.collectionName}] [{bestOutcome.name}] [{bestOutcome.Price}$] [{luck *100}%] [{ev:0.##}$]");
            }

            return true;
        }
    }
}
