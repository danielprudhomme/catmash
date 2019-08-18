using System;
using System.Collections.Generic;
using System.Text;

namespace CatMash.Core.Dto
{
    public class WeightedVote
    {
        public Guid Id { get; set; }
        public int Weight { get; set; }
        public int WeightedSum { get; set; }
    }
}
