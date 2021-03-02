using MiniFigures.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace MiniFigures.Data
{
    public class SeriesService : ISeriesService
    {
        private readonly IWebHostEnvironment env;

        private IMongoCollection<Series> _series;
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        public SeriesService(IMongoDBSettings settings, IWebHostEnvironment _env)
        {
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
            _series = _database.GetCollection<Series>(settings.CollectionName);
            env = _env;
        }
        //Creating New Collection in MongoDB
        public async Task<bool> CreateNewCollection(string name)
        {
            try
            {
                await _database.CreateCollectionAsync(name);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> RenameCollection(string oldName, string newName)
        {
            try
            {
                await _database.RenameCollectionAsync(oldName, newName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteCollection(string name)   //using name for deleting both collection in db and info in SeriesData
        {
            try
            {
                await _database.DropCollectionAsync(name);
                await _series.DeleteOneAsync(i => i.Name == name);
                if (Directory.Exists($"{env.WebRootPath}\\images\\Series\\{name}"))
                {
                    Directory.Delete($"{env.WebRootPath}\\images\\Series\\{name}", true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Adding "Serie " to DisplayName
        //Generating Name by replacing " "(space) with "_"
        //inserting it to MongoDB
        //Checking if series folder exists, if not creating it
        public async Task<bool> AddSerie(Series serie)
        {
            try
            {
                serie.BagImage = string.Empty;
                serie.SeriePicture = string.Empty;
                serie.DisplayName = "Serie " + serie.DisplayName;
                serie.Name = serie.DisplayName.Replace(" ", "_");
                await _series.InsertOneAsync(serie);
                if (!Directory.Exists($"{env.WebRootPath}\\images\\Series\\{serie.Name}"))
                {
                    Directory.CreateDirectory($"{env.WebRootPath}\\images\\Series\\{serie.Name}");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> EditSerie(string ID, Series series)
        {
            try
            {
                series.Name = series.DisplayName.Replace(" ", "_");
                await _series.ReplaceOneAsync(s => s.Id == ID, series);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Series> GetOneSerie(string SerieName)
        {
            try
            {
                return await _series.Find(s => s.Name == SerieName).SortBy(n => n.Number).FirstOrDefaultAsync();
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Series>> GetSeries(string i)
        {
            List<Series> list = new List<Series>();
            switch (i)
            {
                case "A-Z":
                    list = await _series.Find(series => true).SortBy(n => n.Name).ToListAsync();

                    break;
                case "Z-A":
                    list = await _series.Find(series => true).SortByDescending(n => n.Name).ToListAsync();
                    break;
                case "Release date":
                    list = await _series.Find(series => true).SortBy(n => n.ReleaseDate).ToListAsync();
                    break;

                default:
                    break;
            }
            return list;
        }
    }
}
