using System;

namespace Runner.Problems.Day16
{
    public class Validator
    {
        private string name;
        private Predicate<int> function;

        public Validator(string name, Predicate<int> function)
        {
            this.name = name;
            this.function = function;
        }

        public Predicate<int> GetFunction()
        {
            return this.function;
        }

        public string GetName()
        {
            return this.name;
        }
    }
}
