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

            var dataflow = new AnalFlow(stage, false);
            var ConnectionString = "Data Source=Localhost;Initial Catalog=DW;Trusted_Connection=True";

            //SETUP TRADEUP
            var tradeUpInputLevel = Rarity.classified;
           // var Collections = new[] { "Spectrum Case"};

            var Collections = DataUpGetter.getcollections(ConnectionString).Where(x => x != "Dust Collection");

            var results = new List<AnalysisData>();


            Console.WriteLine("GENERATING COMBINATIONS");
            var Combinations = InputGenerator.GenerateCollectionCombinations(2, Collections);
            Console.WriteLine("asd COMBINATIONS");

            var inputData = DataUpGetter.GetInputData(ConnectionString, tradeUpInputLevel, Collections).ToList();
            var outputData = DataUpGetter.GetOutPutData(ConnectionString, tradeUpInputLevel + 1, Collections).ToList();

            var i = 1;
            var a = Combinations.Count();
            Stopwatch sw = Stopwatch.StartNew();
            foreach (var Collection in Combinations)
            {
                if(i % 10 == 0)
                {
                    var msPerStep = sw.ElapsedMilliseconds / i;
                    var remainingSteps = a - i;
                    var remainingEstimateSeconds = (msPerStep * remainingSteps) / 1000;
                    Console.WriteLine($"{msPerStep}ms {sw.ElapsedMilliseconds / 1000}s   {remainingEstimateSeconds}s {i}/{a}");
                }
                i++;


                var inputFiltered = inputData.Where(x => Collection.Contains(x.collectionName)).ToList();
                var outputFiltered = outputData.Where(x => Collection.Contains(x.collectionName)).ToList();
                var data = BruteForceCombination(inputFiltered, outputFiltered);
                dataflow.postData(data);
                results.AddRange(data);
            }
            await dataflow.clean();

            var sorted = results.Where(x => x.Profit > 0).OrderByDescending(x => x.ProfitWithFree);
            StringBuilder sb  = new StringBuilder();
            foreach (var item in sorted)
            {
                sb.AppendLine(item.PrintResult());
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
        }

        private string getCols()
        {
            return string.Join(", ", InputSkins.Select(x => x.collectionName).Distinct());
        }

        public string PrintResult()
        {
            var dist = InputSkins.GroupBy(x => x.collectionName);
            var a = dist.First().Count();
            var b = dist.Last().Count();

            return $"[{getCols(),50}] " +
                            $"| {a,2} / {b,2}" +
                            $"| {InputSkins.First().statTrak}" +
                            $"| {InputSkins.First().rarity}" +
                            $"| {Math.Round(inputFloat, 4),5} " +
                            $"| {Math.Round(InputPrice, 2),5} " +
                            $"| {Math.Round(Profitability, 2),12}% " +
                            $"| {Math.Round(ExpectedValue, 2),7}$ " +
                            $"| {Math.Round(Profit, 2),7}$" +
                            $"| {Math.Round(ProfitWithFree, 2),7}$";
        }
        public int NumOutputs => OutputSkins.Count();

        public bool statTrak => InputSkins.First().statTrak;
        public string rarity => InputSkins.First().rarity;
        public float inputFloat { get; set; }
        public double ExpectedValue { get; set; }
        public double Profit { get; set; }
        public double ProfitWithFree { get; set; }
        public double InputPrice { get; set; }
        public double Profitability { get; set; }
    }
}
