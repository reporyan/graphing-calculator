using System;

namespace GraphingCalculator
{
    public class MultiplyOperation : IOperation
    {
        private string operationName = "multiply";
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
                return "";
            }
        }
        
        public bool FitsOperation(List<Token> _tokens, int _index)
        {
            if (_tokens[_index].IsNum() && _tokens.Count > _index + 1 && _tokens[_index + 1].IsNum())
                return true;

            return false;
        }

        public void Operate(List<Token> _tokens, int _index)
        {
            _tokens[_index].Num = _tokens[_index].Num * _tokens[_index + 1].Num;
            _tokens.RemoveAt(_index + 1);
        }
    }
}