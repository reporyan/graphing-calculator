using System;

namespace GraphingCalculator
{
    public class AbsOperation : IOperation
    {
        private string operationName = "abs";
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
                return "abs";
            }
        }

        public bool FitsOperation(List<Token> _tokens, int _index)
        {
            if (_tokens[_index].Word == "abs")
                return true;

            return false;
        }

        public void Operate(List<Token> _tokens, int _index)
        {
            //Console.WriteLine("Adding " + _tokens[indexA].Word + " and " + _tokens[indexC].Word);
            _tokens[_index].Num = (float)Math.Abs(_tokens[_index + 1].Num);
            _tokens.RemoveAt(_index + 1);
        }
    }
}