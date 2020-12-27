﻿using MiniFigures.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFigures.Data
{
    public class SeriesService : ISeriesService
    {
        private IMongoCollection<Series> _series;
        public SeriesService(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _series = database.GetCollection<Series>(settings.CollectionName);
        }

        public Task<bool> AddSerie(Series series)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditSerie(string ID, Series series)
        {
            throw new NotImplementedException();
        }

        public async Task<Series> GetOneSerie(string SerieName)
        {
            try
            {
                FilterDefinition<Series> filterSeries = Builders<Series>.Filter.Eq("Name", SerieName);
                return await _series.Find(filterSeries).FirstOrDefaultAsync();
                //return await _series.Find(s => s.Name == SerieName).FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }

        public Task<List<string>> GetSeries()
        {
            throw new NotImplementedException();
        }
    }
}