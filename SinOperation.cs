using System;

namespace GraphingCalculator
{
    public class SinOperation : IOperation
    {
        private string operationName = "sin";
        private int priority = 0;

        public string OperationName
        {
            get
            {
                return operationName;
            }
        }
        
        public int Priority
        {
            get
            {
                return priority;
            }
        }

        public string OperationCode
        {
            get
            {
                return "sin";
            }
        }

        public bool FitsOperation(List<Token> _tokens, int _index)
        {
            if (_tokens[_index].Word == "sin")
                return true;

            return false;
        }

        public void Operate(List<Token> _tokens, int _index)
        {
            _tokens[_index].Num = (float)Math.Sin(_tokens[_index + 1].Num);
            _tokens.RemoveAt(_index + 1);
        }
    }
}