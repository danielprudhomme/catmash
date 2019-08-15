using System;
using System.Collections.Generic;
using System.Text;

namespace CatMash.Core.Entities
{
    public class VoteCat
    {
        public string CatId { get; set; }
        public Cat Cat { get; set; }
        public Guid VoteId { get; set; }
        public Vote Vote { get; set; }
        public int Order { get; set; }
    }
}
