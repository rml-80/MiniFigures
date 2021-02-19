using MiniFigures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFigures.Data
{
    public interface IMiniFigureService
    {
        List<MiniFigure> GetMiniFigures(string collectionName);
        Task<MiniFigure> GetOneMiniFigure(string collectionName, int number);
        Task<bool> EditFigure(string ID, MiniFigure miniFigure, string collectionName);
        Task<bool> DeleteFigure(string id);
        Task<bool> AddFiguresToDb(int numberOfFigures,string collectionName, string collectionNumber);
    }
}
