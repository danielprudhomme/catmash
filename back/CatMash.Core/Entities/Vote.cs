using System;
using System.Collections.Generic;
using System.Text;

namespace CatMash.Core.Entities
{
    public class Vote
    {
        public Guid Id { get; set; }
        public int Occurence { get; set; }
        public ICollection<VoteCat> VoteCats { get; set; }
    }
}
