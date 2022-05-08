using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeUpper.DataStuff;
using TradeUpper.Modes;
using TradeUpper.SqlStuff;

namespace TradeUpper
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var stage = "Data Source=Localhost;Initial Catalog=Stage_steam;Trusted_Connection=True";

            var dataflow = new AnalFlow(stage, true);
            var ConnectionString = "Data Source=Localhost;Initial Catalog=DW;Trusted_Connection=True";

            //SETUP TRADEUP
            var tradeUpInputLevel = Rarity.restricted;
            var Collections = new[] { "Huntsman Weapon Case","Danger Zone Case", "Gamma Case"};
            var statTrak = true;
            //var Collections = DataUpGetter.getcollections(ConnectionString,statTrak).Where(x => x != "Dust Collection");


            var CollectionsToUse = 2;
            var Combinations = InputGenerator.GenerateCollectionCombinations(CollectionsToUse, Collections);
            await DoShit(ConnectionString, (int)Rarity.restricted, Collections, CollectionsToUse, statTrak, Combinations, dataflow);

            //for (int i = 0; i < ((int)Rarity.covert); i++)
            //{

            //    await DoShit(ConnectionString,i,Collections,CollectionsToUse,statTrak,Combinations,dataflow);
            //}
            await dataflow.clean();

        }

        static async Task DoShit(string ConnectionString,
            int rarity,
            IEnumerable<string> Collections,
            int CollectionsToUse,
            bool statTrak,
            IEnumerable<IEnumerable<string>> Combinations,
            AnalFlow dataflow
            )
        {
            var inputData = DataUpGetter.GetInputData(ConnectionString, (Rarity)rarity, Collections, statTrak).ToList();
            var outputData = DataUpGetter.GetOutPutData(ConnectionString, (Rarity)rarity + 1, Collections, statTrak).ToList();

            var iter = 1;
            var combCount = MathExt.getKCombinationCount(CollectionsToUse, Collections.Count());
            Stopwatch sw = Stopwatch.StartNew();
            foreach (var Collection in Combinations)
            {
                if (iter % 10 == 0)
                {
                    var msPerStep = sw.ElapsedMilliseconds / iter;
                    var remainingSteps = combCount - iter;
                    var remainingEstimateSeconds = (msPerStep * remainingSteps) / 1000;
                    Console.WriteLine($"{(Rarity)rarity} {msPerStep}ms {sw.ElapsedMilliseconds / 1000}s   {remainingEstimateSeconds}s {iter}/{combCount}");
                }
                iter++;

                var inputFiltered = inputData.Where(x => Collection.Contains(x.collectionName)).ToList();
                var outputFiltered = outputData.Where(x => Collection.Contains(x.collectionName)).ToList();
                var data = BruteForceCombination(inputFiltered, outputFiltered);
                await dataflow.postData(data);
                //Console.WriteLine($"FILTERING TOOK {testi.ElapsedMilliseconds}ms");

            }
        }

        static IEnumerable<AnalysisData> BruteForceCombination(List<Skin> inputData,List<Skin> outputData)
        {

            var generator = new InputGenerator(inputData);

            if (outputData.Count() == 0) { yield break; }
            generator.SetOutComes(outputData);

            foreach (var inputSkins in generator.Generate())
            {

                if (inputSkins.Count > 0)
                {
                    var inputCollections = inputSkins.Select(x => x.collectionName).Distinct();
                    //START CALCULATING
                    TradeUpCalculator calc = new TradeUpCalculator();
                    calc.setInputSkins(inputSkins);
                    //GET OUTPUT BY INPUT COLLECTIONS
                    calc.setOutputSkins(outputData.Where(x => inputCollections.Contains( x.collectionName)));

                    var success  = calc.DoCalc(); // CALCULATE EV
                    if (success)
                    {
                        // PRING SHIT
                        var inputPrice = calc.CalculateInputPrice(inputSkins);
                        var f = calc.CalculateAverageFloat(inputSkins);
                        var anal = new AnalysisData(inputSkins, calc.bestOutcomes, calc.TotalExpectedValue, inputPrice, f);
                        if(anal.Profit > 0)
                        {
                            yield return anal;
                        }
                    }
                }

            }
        }

    }

    public class AnalysisData
    {

        public List<Skin> InputSkins { get; set; } = new List<Skin>();
        private List<Skin> OutputSkins { get; set; }  = new List<Skin>();
        public List<ResultSkin> output { get; set; } = new List<ResultSkin>();
        public AnalysisData(List<Skin> inputSkins, List<Skin> outputSkins, double expectedValue, double inputPrice, float inputAvgFloat)
        {
            InputSkins = inputSkins;
            OutputSkins = outputSkins;
            ExpectedValue = expectedValue;
            Profit = expectedValue - inputPrice;
            InputPrice = inputPrice;
            inputFloat = inputAvgFloat;
            Profitability = expectedValue - inputPrice;
            ProfitWithFree = (expectedValue * 0.87) - inputPrice; // 13%

            foreach (var item in outputSkins)
            {
                var inputCount = outputSkins.Where(x => x.collectionName == item.collectionName).Count();
                var groupFactor = inputCount * 0.1;
                var outPutCollectionSkinCount = outputSkins.Select(x => x.collectionName).Distinct().Count();

                var luck = groupFactor / outPutCollectionSkinCount;
                var a  =new ResultSkin(item.name, item.SkinFloat, item.Price, luck,item.ExteriorStr);
                output.Add(a);
            }

        }

        private string getCols()
        {
            return string.Join(", ", InputSkins.Select(x => x.collectionName).Distinct());
        }


        public string usedCollections => String.Join(", ", OutputSkins.Select(x => x.collectionName).Distinct());
        public int NumOutputs => OutputSkins.Count();

        public bool statTrak => InputSkins.First().statTrak;
        public string rarity => InputSkins.First().rarity;
        public float inputFloat { get; set; }
        public double ExpectedValue { get; set; }
        public double Profit { get; set; }
        public double ProfitWithFree { get; set; }
        public double InputPrice { get; set; }
        public double Profitability { get; set; }

        public class ResultSkin
        {
            public ResultSkin(string name, float skinFloat, double price, double luck,string ext)
            {
                Extrerior = ext;
                Name = name;
                SkinFloat = skinFloat;
                Price = price;
                this.luck = luck;
            }

            public string Name { get; set; }
            public string Extrerior { get; set; }
            public float SkinFloat{get;set;}
            public double Price { get; set; }
            public double luck { get; set; }
        }
    }
}
