using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExpressionProblemSolution
{
    public class ExpressionAssignmentCalculator
    {
        /// <summary>
        /// _solvedVariables dictionary will contain all solved variables with their value
        /// </summary>
        private readonly Dictionary<string, long> _solvedVariables = new();
        /// <summary>
        /// _unsolvedVariables dictionary will contain all unsolved variables with their simplified expressions so that we can solve later
        /// </summary>
        private readonly Dictionary<string, string> _unsolvedVariables = new();

        public void Calculate(string input)
        {
            if (!IsInputValid(input))
            {
                Console.WriteLine("Invalid input");
                return;
            }

            var expParts = input.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
            if (expParts.Count != 2)
            {
                Console.WriteLine("Invalid input");
                return;
            }
            var name = expParts[0];
            var exp = expParts[1];
            SolveExpression(name, exp);
        }
        /// <summary>
        /// this method try to solve / simplify current exp and pending unsolved exp with recursion
        /// </summary>
        /// <param name="name"></param>
        /// <param name="exp"></param>
        private void SolveExpression(string name, string exp)
        {
            if (exp == null)
            {
                // skipping unsolved variable
                return;
            }
            // splitting expression 
            var expParts = exp.Split(new[] { '+' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
            long sum = 0;
            var pendingVar = new List<string>();
            foreach (var val in expParts)
            {
                if (long.TryParse(val, out var number))
                {
                    // add if exp part is number
                    sum += number;
                }
                else
                {
                    if (_solvedVariables.TryGetValue(val, out var num))
                    {
                        // if variable is already solved get value
                        sum += num;
                    }
                    else if (_unsolvedVariables.TryGetValue(val, out var unsolvedExp))
                    {
                        // if variable is not solved try to solve and add to pendingVar dictionary
                        SolveExpression(val, unsolvedExp);
                        pendingVar.Add(val);
                    }
                    else
                    {
                        // new unsolved variable found and added to unsolved dictionary
                        pendingVar.Add(val);
                        _unsolvedVariables[val] = null;
                    }
                }
            }

            if (pendingVar.Count > 0)
            {
                // add/update unsolved dictionary ( simplify solvable part and append unsolvable part)
                _unsolvedVariables[name] = sum > 0 ? $"{string.Join('+', pendingVar)}+{sum}" : $"{string.Join('+', pendingVar)}";
            }
            else if (sum != 0)
            {
                // variable is completely solved adding it to _solved variables dictionary so that we can use it for future calculations
                _solvedVariables[name] = sum;
                if (_unsolvedVariables.ContainsKey(name))
                {
                    // removing from unsolved dictionary as it is solved
                    _unsolvedVariables.Remove(name);
                }
                Console.WriteLine($"===> {name} = {sum}");

                // trying to solve / simplify unsolved variables
                foreach (var (key, value) in _unsolvedVariables)
                {
                    SolveExpression(key, value);
                }
            }
        }

        /// <summary>
        /// Validate user input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool IsInputValid(string input)
        {
            // checking if = present in string and only once
            var first = input.IndexOf('=');
            //if (!input.Contains("="))
            if (first == -1)
                return false;
            var last = input.LastIndexOf('=');
            // checking if = exists multiple time
            if (first != last)
            {
                return false;
            }
            // check if string doesn't contains chars other than alpha numeric and = + 
            var rgx = new Regex(@"^([A-Za-z0-9\=\+\s]*)$");
            if (!rgx.IsMatch(input))
            {
                return false;
            }
            return true;
        }
    }
}
