using MiniFigures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFigures.Data
{
    public interface IMiniFigureService
    {
        Task<List<string>> GetSeries();
        List<MiniFigure> GetMiniFigures(string collectionName);
        Task<MiniFigure> GetOneMiniFigure(string collectionName, int number);
        Task<bool> AddFigure(MiniFigure miniFigure, string collectionName);
    }
}
