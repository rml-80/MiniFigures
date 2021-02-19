using MiniFigures.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFigures.Data
{
    public class MiniFigureService : IMiniFigureService
    {
        private IMongoCollection<MiniFigure> _miniFigure;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        public MiniFigureService(IMongoDBSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
        }

        public List<MiniFigure> GetMiniFigures(string collectionName)
        {
            try
            {
                _miniFigure = _database.GetCollection<MiniFigure>(collectionName);
                return _miniFigure.Find(minifigure => true).SortBy(n => n.Number).ToList();
            }
            catch
            {
                return null;
            }
        }
        public async Task<MiniFigure> GetOneMiniFigure(string collectionName, int number)
        {
            try
            {
                _miniFigure = _database.GetCollection<MiniFigure>(collectionName);

                FilterDefinition<MiniFigure> filterMiniFigure = Builders<MiniFigure>.Filter.Eq("Number", number);
                return await _miniFigure.Find(filterMiniFigure).FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> EditFigure(string Id, MiniFigure miniFigure, string collectionName)
        {
            try
            {
                _miniFigure = _database.GetCollection<MiniFigure>(collectionName);
                await _miniFigure.ReplaceOneAsync(n => n.ID == Id, miniFigure) ;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteFigure(string id)
        {
            try
            {
                await _miniFigure.DeleteOneAsync(i => i.ID == id);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> AddFiguresToDb(int numberOfFigures, string collectionName, string collectionNumber)
        {
            var mini = _database.GetCollection<BsonDocument>(collectionName);
            var Image = string.Empty;
            var bson = new List<BsonDocument>();
            for (int i = 1; i <= numberOfFigures; i++)
            {
                if (i.ToString().Length > 1)
                {
                    Image = $"{collectionNumber}-{i}.jpg";
                }
                else
                {
                    Image = $"{collectionNumber}-0{i}.jpg";
                }
                var figure = new BsonDocument
                {
                    { "Name",i.ToString()},
                    { "CountSealed",0},
                    { "CountOpened",0},
                    { "CountTotal",0},
                    { "Number",i},
                    { "Displayed",false},
                    { "Image",Image},
                };
                bson.Add(figure);
            }
            await mini.InsertManyAsync(bson);
            return true;
        }
    }
}

