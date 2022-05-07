using System;

namespace TradeUpper.Modes
{
    public class Skin
    {
        public Skin(object collectionName, object rarityCode, object exterior, object price, object name, object floatTarget, float? skinFloat, object statTrack)
        {
            this.collectionName = collectionName.ToString();
            RarityCode = (Rarity)Enum.Parse(typeof(Rarity), rarityCode.ToString());
            Exterior = ext(exterior.ToString());
            Price = double.Parse(price.ToString());
            this.name = name.ToString();
            FloatTarget = float.Parse(floatTarget.ToString());
            SkinFloat = skinFloat ?? defaultFloat();
            statTrak = (int)statTrack == 0 ? false : true;
        }
        public Skin(object collectionName, object rarityCode, object exterior, object price, object name, object floatTarget, object minFloat, object maxFloat, object statTrack)
        {
            this.collectionName = collectionName.ToString();
            RarityCode = (Rarity)Enum.Parse(typeof(Rarity), rarityCode.ToString());
            Exterior = ext(exterior.ToString());
            Price = double.Parse(price.ToString());
            this.name = name.ToString();
            FloatTarget = float.Parse(floatTarget.ToString());
            MinFloat = float.Parse(minFloat.ToString());
            MaxFloat = float.Parse(maxFloat.ToString());
            SkinFloat = defaultFloat();
            statTrak = (int)statTrack == 0 ? false : true;

        }
        private float[][] FloatRanges = new[]
{
            new float[] {0.00f,0.07f },
            new float[] {0.07f,0.15f },
            new float[] { 0.15f, 0.38f },
            new float[] {0.38f,0.45f },
            new float[] { 0.45f, 1.00f },

        };
        private Exterior ext(string exterior)
        {
            switch (exterior)
            {
                case "Factory-New":
                    return Exterior.FN;
                case "Minimal-Wear":
                    return Exterior.MW;
                case "Field-Tested":
                    return Exterior.FT;
                case "Well-Worn":
                    return Exterior.WW;
                case "Battle-Scarred":
                    return Exterior.BS;
                default:
                    return Exterior.BS;
            }
        }


        private float defaultFloat()
        {
            switch (Exterior)
            {
                case Exterior.FN:
                    return (MinFloat + FloatRanges[0][1]) / 2;
                case Exterior.MW:
                    return FloatRanges[1][1] > MaxFloat ? (MaxFloat + FloatRanges[1][0]) / 2 : (FloatRanges[1][0] + FloatRanges[1][1]) / 2;
                case Exterior.FT:
                    return FloatRanges[2][1] > MaxFloat ? (MaxFloat + FloatRanges[2][0]) / 2 : (FloatRanges[2][0] + FloatRanges[2][1]) / 2;
                case Exterior.WW:
                    return FloatRanges[3][1] > MaxFloat ? (MaxFloat + FloatRanges[3][0]) / 2 : (FloatRanges[3][0] + FloatRanges[3][1]) / 2;
                case Exterior.BS:
                    return (MaxFloat + FloatRanges[4][0]) / 2;
                default:
                    return 1f;
            }
        }

        public double CostPerFloat => Price / SkinFloat;
        public double CostPerFloatRevers => Price / (1 - SkinFloat);

        public bool statTrak { get; set; }
        public string collectionName { get; set; }
        private Rarity RarityCode { get; set; }
        public string rarity => RarityCode.ToString();
        public Exterior Exterior { get; set; }
        public string ExteriorStr => Exterior.ToString();
        public double Price { get; set; }
        public string name { get; set; }
        public float FloatTarget { get; set; }
        public float SkinFloat { get; set; }
        public float MinFloat { get; set; }
        public float MaxFloat { get; set; }
        public float TradeupAvgFloat { get; set; }
    }
}
