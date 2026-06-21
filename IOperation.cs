using System;

namespace GraphingCalculator
{
    public interface IOperation
    {
        public string OperationName { get; }//for storing action?
        public string OperationCode { get; }//for finding multi char strings in equation
        public int Priority { get; }//smaller priority means done first in BODMAS
        public bool FitsOperation(List<Token> _tokens, int _index);//true if it is suitable for this operation to occur
        public void Operate(List<Token> _tokens, int _index);//executes operation
    }
}