using System;
using System.Runtime.InteropServices.Marshalling;
using SplashKitSDK;

namespace GraphingCalculator
{
    public class Graph : GraphObject
    {
        private int NODES_PER_GRAPH = 256;

        private List<Node> nodes = new List<Node>();

        //used for determining number
        private Char[] posNumbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.'};

        //multichar added at runtime
        private List<string> multiCharComponents = new List<string>();//function names, put longer ones first

        private Dictionary<string, IOperation> operations = new Dictionary<string, IOperation>();

        private Dictionary<string, float> constants = new Dictionary<string, float>();

        public Graph(string _equation)
        {
            equation = _equation;

            operations["sin"] = new SinOperation();
            operations["cos"] = new CosOperation();
            operations["tan"] = new TanOperation();
            operations["log"] = new LogOperation();
            operations["sqrt"] = new SqrtOperation();
            operations["abs"] = new AbsOperation();
            operations["power"] = new PowerOperation();
            operations["multiply"] = new MultiplyOperation();
            operations["star"] = new StarOperation();
            operations["divide"] = new DivideOperation();
            operations["add"] = new AddOperation();

            constants["pi"] = MathF.PI;

            foreach (IOperation operation in operations.Values)
            {
                if (operation.OperationCode.Length > 1)
                {
                    multiCharComponents.Add(operation.OperationCode);
                    Console.WriteLine("Adding MCC: " + operation.OperationCode);
                }
            }
            foreach(string constant in constants.Keys)
            {
                if(constant.Length > 1)
                {
                    multiCharComponents.Add(constant);
                    Console.WriteLine("Adding MCC: " + constant);
                }
            }

            Reload();
        }

        public Graph(string _equation, Color _colour) : this(_equation)
        {
            Colour = _colour;
        }

        public override void DrawObject()
        {
            for(int i = 0; i < nodes.Count() - 1; i++)
            {
                if (nodes[i].NoValue || nodes[i + 1].NoValue)
                    continue;

                SplashKit.DrawLine(Colour,
                WorldToScreenX(nodes[i].Position.X), WorldToScreenY(nodes[i].Position.Y),
                WorldToScreenX(nodes[i + 1].Position.X), WorldToScreenY(nodes[i + 1].Position.Y));
            }
        }

        public void CalculateNodes(string _equation, int _nodeCount, float _from, float _to)
        {
            nodes = new List<Node>();

            for (float x = _from; x < _to; x += (_to - _from) / (float)_nodeCount)
            {
                AddNode(new Vector2(x, EvaluateFunction(_equation, x)));
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Nodes Calculated");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void AddNode(Vector2 _nodePos)
        {
            Node node = new Node(_nodePos.X, _nodePos.Y);
            nodes.Add(node);
        }

        public float EvaluateFunction(string _equation, float _x)
        {
            List<Token> tokens = GenerateTokens(_equation, _x);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("---");
            Console.WriteLine(DebugTokens(tokens));
            Console.ForegroundColor = ConsoleColor.White;

            return EvaluateTokens(tokens);
        }

        public override void Reload()
        {
            CalculateNodes(equation, NODES_PER_GRAPH, cam.CamBoundNegX(true), cam.CamBoundPosX(true));
        }
        
        public List<Token> GenerateTokens(string _equation, float _x)//this needs to take care of formatting it in right way
        {
            List<Token> tokens = new List<Token>();

            //replace with numbers and combine numbers
            string build = "";

            //loop over and add to token list
            for (int i = 0; i < _equation.Length; i++)
            {
                if (posNumbers.Contains(_equation[i]))//number
                {
                    build += _equation[i].ToString();//build number
                }
                else//this character is not a number / continuation of a number
                {
                    if (build != "")//but there was a number being built
                    {
                        if (build == "-")//if the build was just a minus, replace with -1 to stop -x error
                        {
                            build = "-1";
                        }

                        tokens.Add(new Token(build, this));//add the pending number and reset build
                        build = "";
                    }

                    //for each component

                    bool componentFound = false;
                    foreach (string component in multiCharComponents)
                    {
                        //if that function cannot remain in the equation
                        if (i + component.Length > _equation.Length)//see if it goes out of range. i takes up a character of component
                        {
                            continue;
                        }

                        //see if it's a match
                        bool match = true;
                        for (int j = 0; j < component.Length; j++)
                        {
                            if (component[j] != _equation[i + j])
                            {
                                match = false;
                                break;
                            }
                        }
                        if (match)//this is a function, add it and add i
                        {
                            componentFound = true;

                            if (constants.ContainsKey(component))
                            {
                                tokens.Add(new Token(constants[component], this));
                            }
                            else
                            {
                                tokens.Add(new Token(component, this));
                            }

                            i += component.Length - 1;
                        }
                    }

                    //do not keep going if we have found a multi char component
                    if (componentFound)
                        continue;

                    //change this to pass reference to object?
                    if (_equation[i] == 'x')//add number if it's x
                    {
                        tokens.Add(new Token(_x, this));
                        continue;
                    }
                    if (_equation[i] == 'e')//euler
                    {
                        tokens.Add(new Token(MathF.E, this));
                        continue;
                    }
                    if (_equation[i] == '-')//- means new number
                    {
                        //don't add a token coz it does that at end
                        //build a new number starting with minus
                        if (tokens.Count > 0 && tokens[tokens.Count - 1].IsNum())//the previous token was a number and this is a minus
                        {
                            tokens.Add(new Token("+", this));
                        }
                        build = "-";
                        continue;
                    }

                    //add whatever it is otherwise
                    tokens.Add(new Token(_equation[i], this));
                }
            }

            //need to check after because there could be a number!
            if (build != "")//there is a number pending
            {
                tokens.Add(new Token(build, this));//add the pending number and reset
            }

            return tokens;
        }

        public float EvaluateTokens(List<Token> _tokens)
        {
            //now calcuate!
            while (_tokens.Count() > 1)//while we don't have a result
            {
                int lowestPriority = 999;//used for BODMAS, which type of action will be performed
                string lowestPriorityActionKey = "n/a";
                int indexToModify = -1;

                bool foundBracket = false;//determines if operation should be ran;

                for (int i = 0; i < _tokens.Count(); i++)//for every token
                {
                    //we are condensing token list

                    if (_tokens[i].Word == ")")//brackets are special
                    {
                        List<Token> insideBracketTokens = new List<Token>();

                        _tokens.RemoveAt(i);//delete end bracket
                        i--;
                        while (_tokens[i].Word != "(")
                        {
                            insideBracketTokens.Insert(0, _tokens[i]);
                            _tokens.RemoveAt(i);
                            i--;
                        }
                        _tokens.RemoveAt(i);//delete start bracket

                        _tokens.Insert(i, new Token(EvaluateTokens(insideBracketTokens), this));//recursive function!!! this is so cool!

                        foundBracket = true;
                        break;
                    }
                    else
                    {
                        //find operation and simplify
                        foreach (KeyValuePair<string, IOperation> operation in operations)//for each operation type
                        {
                            string operationName = operation.Key;//store name
                            if (operations[operationName].FitsOperation(_tokens, i) && operations[operationName].Priority < lowestPriority)//if lower and is part of string
                            {
                                lowestPriority = operations[operationName].Priority;//reset lowest
                                lowestPriorityActionKey = operationName;//new action
                                indexToModify = i;//new index to modify
                            }
                        }
                    }
                }

                //now we execute the appropriate action
                if (!foundBracket)
                    operations[lowestPriorityActionKey].Operate(_tokens, indexToModify);
                    
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(DebugTokens(_tokens));
                Console.ForegroundColor = ConsoleColor.White;
            }

            return _tokens[0].Num;
        }

        public string DebugTokens(List<Token> _comps)
        {
            string output = "Current Tokens: ";

            foreach (Token tkn in _comps)
            {
                output += tkn.Word + ", ";
            }

            return output;
        }

        //this function isn't used
        public bool TokensContains(List<Token> _tokens, string _contains)//may be unused
        {
            bool contains = false;

            foreach (Token token in _tokens)
            {
                if (token.Word == _contains)
                {
                    contains = true;
                }
            }

            return contains;
        }
    }
}
