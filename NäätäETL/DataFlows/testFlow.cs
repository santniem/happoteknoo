using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using Gridsum.DataflowEx;

namespace NäätäETL.DataFlows
{
    public class AggregatorFlow : Dataflow<string>
    {
        private readonly ActionBlock<KeyValuePair<string, int>> _aggregater;

        //Data
        private readonly Dictionary<string, int> _dict;

        //Blocks
        private readonly TransformBlock<string, KeyValuePair<string, int>> _splitter;

        public AggregatorFlow() : base(DataflowOptions.Default)
        {
            _splitter = new TransformBlock<string, KeyValuePair<string, int>>(s => Split(s));
            _dict = new Dictionary<string, int>();
            _aggregater = new ActionBlock<KeyValuePair<string, int>>(p => Aggregate(p));

            //Block linking
            _splitter.LinkTo(_aggregater, new DataflowLinkOptions {PropagateCompletion = true});

            /* IMPORTANT */
            RegisterChild(_splitter);
            RegisterChild(_aggregater);
        }

        public override ITargetBlock<string> InputBlock => _splitter;

        public IDictionary<string, int> Result => _dict;

        protected virtual void Aggregate(KeyValuePair<string, int> pair)
        {
            int oldValue;
            _dict[pair.Key] = _dict.TryGetValue(pair.Key, out oldValue) ? oldValue + pair.Value : pair.Value;
        }

        protected virtual KeyValuePair<string, int> Split(string input)
        {
            var splitted = input.Split('=');
            return new KeyValuePair<string, int>(splitted[0], int.Parse(splitted[1]));
        }
    }
}