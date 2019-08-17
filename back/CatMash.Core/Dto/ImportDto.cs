using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatMash.Core.Dto
{
    public class ImportDto
    {
        [JsonProperty("images")]
        public IEnumerable<ImportCatDto> Cats { get; set; }
    }
}
