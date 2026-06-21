using System;

namespace GraphingCalculator
{
    public class Token
    {
        private string word;

        private Graph graph;

        public Token(string _word, Graph _graph)
        {
            word = _word;
            graph = _graph;
        }

        public Token(char _char, Graph _graph)
        {
            word = _char.ToString();
            graph = _graph;
        }

        public Token(float _num, Graph _graph)
        {
            word = _num.ToString();
            graph = _graph;
        }

        public string Word
        {
            get
            {
                return word;
            }
            set
            {
                word = value;
            }
        }

        public float Num
        {
            get
            {
                return float.Parse(word);
            }
            set
            {
                word = value.ToString();
            }
        }

        public Graph Graph
        {
            set
            {
                graph = value;
            }
        }

        public bool IsNum()
        {
            if(float.TryParse(Word, out float result))//had problem with infinity
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Log()
        {
            Console.WriteLine("Token Value: " + Word);
        }
    }
}