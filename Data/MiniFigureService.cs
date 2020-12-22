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
        public List<string> series = new List<string>();
        public MiniFigureService(IMongoDBSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
            //_miniFigure = database.GetCollection<MiniFigure>(settings.CollectionName);
        }
        public async Task<List<string>> GetSeries()
        {
            try
            {
                List<string> series = await (new MongoDB.Driver.MongoClient()).GetDatabase("MiniFigures").ListCollectionNames().ToListAsync();
                series.Reverse();
                return series;
                //series = await _database.ListCollectionNames().ToListAsync();
                //return series.Reverse();
            }
            catch
            {
                return null;
            }
        }
        public List<MiniFigure> GetMiniFigures(string collectionName)
        {
            try
            {
                _miniFigure = _database.GetCollection<MiniFigure>(collectionName);
                return _miniFigure.Find(minifigure => true).ToList();
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
                //    return await _miniFigure.Find(miniFigure => miniFigure.Number == number).FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<bool> AddFigure(MiniFigure miniFigure, string collectionName)
        {
            try
            {
                _miniFigure = _database.GetCollection<MiniFigure>(collectionName);
                await _miniFigure.InsertOneAsync(miniFigure);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditFigure(int number, MiniFigure miniFigure, string collectionName)
        {
            try
            {
                _miniFigure = _database.GetCollection<MiniFigure>(collectionName);
                await _miniFigure.ReplaceOneAsync(n => n.Number == number, miniFigure) ;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

