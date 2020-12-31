using MiniFigures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFigures.Data
{
    public interface ISeriesService
    {
        Task<bool> CreateNewCollection(string name);
        Task<bool> RenameCollection(string oldName, string newName);
        Task<bool> DeleteCollection(string name);
        Task<List<Series>> GetSeries();
        Task<Series> GetOneSerie(string SerieName);
        Task<bool> AddSerie(Series series);
        Task<bool> EditSerie(string ID, Series series);
        Task<bool> DeleteSerie(string ID);
    }
}
