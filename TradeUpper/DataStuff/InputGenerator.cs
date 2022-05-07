using System;
using System.Collections.Generic;
using System.Linq;
using TradeUpper.Modes;

namespace TradeUpper.DataStuff
{
    public class InputGenerator
    {
        private IEnumerable<Skin> InputData;
        private List<Skin> OutputData;
        public List<string> Collections => InputData.Select(x => x.collectionName).Distinct().ToList();


        public InputGenerator(List<Skin> input)
        {
            InputData = input.OrderByDescending(x => x.Exterior);
        }

        public void SetOutComes(List<Skin> skins)
        {
            OutputData = skins;
        }


        private static IEnumerable<int[]> dist(int collectionCount)
        {
            int n = 10;
            var values1 = Enumerable.Range(1, n);

            var a = values1.GetPermutationsWithRept(collectionCount);
            foreach (var item in a)
            {
                if (item.Sum() == 10)
                {
                    yield return item.ToArray();
                }
            }
        }

        public IEnumerable<List<Skin>> Generate()
        {
            var FloatTargets = OutputData
                .Where(X => X.FloatTarget < 1)
                .Select(x => x.FloatTarget)
                .Distinct()
                .OrderByDescending(x => x);
            //Näiden perusteella pitäs sitte koittaa keksiä minkä floattisiä skinejä pitäs käyttää

            var cnt = Collections.Count();

            var d = dist(cnt);
            foreach (var item in d)
            {
                foreach (var targetFloat in FloatTargets)
                {
                    List<Skin> ret = new();
                    for (int i = 0; i < item.Length; i++)
                    {
                        var input = fitFloat(targetFloat, item[i], Collections[i]);
                        if (input == null)
                        {
                            yield break;
                        }
                        ret.AddRange(input);
                    }

                    yield return ret;

                }
            }
        }


        private IEnumerable<Skin> fitFloat(float TargetFloat, int collectionDist, string collection)
        {
            List<Skin> skins = new List<Skin>();

            for (int i = 0; i < collectionDist; i++)
            {
                float currentFloat = skins.Count() == 0 ? 0 : skins.Average(x => x.SkinFloat);

                if (currentFloat > TargetFloat)
                {
                    var s = InputData.Where(x => x.SkinFloat < TargetFloat && x.collectionName == collection).FirstOrDefault();
                    if (s == null)
                    {
                        s = InputData.Where(x => x.SkinFloat < TargetFloat).FirstOrDefault();
                    }
                    if (s == null)
                    {
                        return null;
                    }
                    skins.Add(s);

                }
                else
                {
                    var s = InputData.Where(x => x.SkinFloat >= currentFloat && x.collectionName == collection).First();
                    skins.Add(s);
                }
            }

            //OPTIMOINTI TÄHÄN

            return skins;
        }


        private IEnumerable<Skin> GetBestOutComes(IEnumerable<Skin> outPutSkins, float targetFloat)
        {
            var AvailableOutComes = outPutSkins.Where(x => targetFloat < x.FloatTarget);
            var Outputs = AvailableOutComes.GroupBy(x => x.name);
            foreach (var skinGroup in Outputs)
            {
                var bestPossibleExtreriorForAvgFloat = skinGroup.Min(x => x.Exterior);
                var bestOutcome = skinGroup.First(x => x.Exterior == bestPossibleExtreriorForAvgFloat);
                yield return bestOutcome;
            }

        }

        public static IEnumerable<IEnumerable<string>> GenerateCollectionCombinations(int CollectionsToPutInTradeup, IEnumerable<string> Collections)
        {
            int n = Collections.Count();
            int r = CollectionsToPutInTradeup;
            int i = 1;

            foreach (var permutation in Collections.GetKCombs(r))
            {
                //   Console.WriteLine($"{i} / {comb} {string.Join(", ", permutation)}");
                i++;
                yield return permutation;
            }
        }

        public static void printInputSkin(IEnumerable<Skin> skins)
        {
            Console.WriteLine("INPUT SKINS");
            foreach (var item in skins)
            {
                Console.WriteLine($"[{ item.collectionName}] [{item.Exterior}] [{item.SkinFloat}] [{item.Price}] {item.name} ");
            }
            var selectedCollections = skins.Select(x => x.collectionName).Distinct();
            Console.WriteLine($"Input Collections {string.Join(',', selectedCollections)}");
            Console.WriteLine();
        }

    }

}
