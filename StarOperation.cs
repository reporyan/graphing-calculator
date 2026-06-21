using System;

namespace GraphingCalculator
{
    public class StarOperation : IOperation
    {
        private string operationName = "star";
        private int priority = 2;
        
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
                return "*";
            }
        }

        public bool FitsOperation(List<Token> _tokens, int _index)
        {
            if (_tokens[_index].Word == "*")
                return true;

            return false;
        }

        public void Operate(List<Token> _tokens, int _index)
        {
            _tokens.RemoveAt(_index);//literally just remove it
        }
    }
}