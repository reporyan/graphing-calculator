using System;

namespace GraphingCalculator
{
    public class DivideOperation : IOperation
    {
        private string operationName = "divide";
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
                return "/";
            }
        }

        public bool FitsOperation(List<Token> _tokens, int _index)
        {
            if (_tokens[_index].Word == "/")
                return true;

            return false;
        }

        public void Operate(List<Token> _tokens, int _index)
        {
            _tokens[_index - 1].Num = _tokens[_index - 1].Num / _tokens[_index + 1].Num;
            _tokens.RemoveAt(_index);
            _tokens.RemoveAt(_index);
        }
    }
}