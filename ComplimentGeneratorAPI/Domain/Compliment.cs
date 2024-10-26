using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ComplimentGeneratorAPI.Domain
{
    public class Compliment
    {
        public ObjectId Id { get; set; }
        public string? Content { get; set; }
    }
}