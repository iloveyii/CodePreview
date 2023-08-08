using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.SettingsService
{
    public interface ISettingsService
    {
        List<Settings> GetAll();
        Dictionary<string, string> GetAllDictionary();
        Settings? GetOne(int id);
        Settings Create(Settings settings);
        Settings? UpSert(Settings settings);
        List<Settings> Delete(int id);
        Settings? SearchByKey(string Key);
    }
}
