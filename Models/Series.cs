using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFigures.Models
{
    public class Series
    {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }
            public string Name { get; set; }
            public string Number { get; set; }
            public int NumberOfFigures { get; set; }
            public string SeriesColor { get; set; }
            public string SeriePicture { get; set; }
    }
}
