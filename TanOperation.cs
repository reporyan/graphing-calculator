using System;

namespace GraphingCalculator
{
    public class TanOperation : IOperation
    {
        private string operationName = "tan";
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
                return "tan";
            }
        }
        
        public bool FitsOperation(List<Token> _tokens, int _index)
        {
            if (_tokens[_index].Word == "tan")
                return true;

            return false;
        }

        public void Operate(List<Token> _tokens, int _index)
        {
            _tokens[_index].Num = (float)Math.Tan(_tokens[_index + 1].Num);
            _tokens.RemoveAt(_index + 1);
        }
    }
}