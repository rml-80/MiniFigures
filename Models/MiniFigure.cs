using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFigures.Models
{
    public class MiniFigure
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string Name { get; set; }
        public int CountSealed { get; set; }
        public int CountOpened { get; set; }
        public int CountTotal { get; set; }
        public int Number { get; set; }
        public bool Displayed { get; set; }
        public string Image { get; set; }

    }
}
