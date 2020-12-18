using System.Collections.Generic;
using System.Linq;

namespace Runner.Problems.Day16
{
    public class Node
    {
        List<Validator> validatorsSoFar = new List<Validator>();

        public Node(Validator start)
        {
            this.validatorsSoFar.Add(start);
        }


        public Node(List<Validator> validators, Validator next)
        {
            this.validatorsSoFar.AddRange(validators);
            this.validatorsSoFar.Add(next);
        }
        public bool HasValidator(Validator validator)
        {
            return this.validatorsSoFar.Contains(validator);
        }

        public ulong CalculateTicket(int[] ticket)
        {
            ulong multi = 1;
            for (int i = 0; i < ticket.Length; i++)
            {
                if (this.validatorsSoFar[i].GetName().Contains("departure"))
                {
                    multi *= (ulong) ticket[i];
                }
            }

            return multi;
        }

        public Node WithNextValidator(Validator validator)
        {
            return new Node(this.validatorsSoFar, validator);
        }

        public Validator GetNextValidator()
        {
            return this.validatorsSoFar.Last();
        }

        public int GetIndex()
        {
            return this.validatorsSoFar.Count - 1;
        }
    }
}
