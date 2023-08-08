using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.SettingsService
{
    public class SettingsService : ISettingsService
    {
        private readonly SqliteContext _context;
        public SettingsService(SqliteContext context)
        {
            _context = context;
        }

        public Settings Create(Settings settings)
        {
            _context.Settings.Add(settings);
            _context.SaveChanges();
            return settings;
        }

        public List<Settings> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Settings> GetAll()
        {
            return _context.Settings.ToList();
        }
        public Dictionary<string, string> GetAllDictionary()
        {
            var list = _context.Settings.ToList();
            var dic = list.ToDictionary(s => s.Key, s => s.Value);
            return dic;
        }

        public Settings? GetOne(int id)
        {
            return _context.Settings.Find(id);
        }

        public Settings? SearchByKey(string Key)
        {
            var setting = _context.Settings.Where(s => s.Key == Key).FirstOrDefault();
            return setting;
        }

        public Settings? Update(int id, Settings request)
        {
            var setting = GetOne(id);
            if (setting is not null)
            {
                setting.update(request);
                _context.SaveChanges();
            }
            return setting;
        }

        public Settings? UpSert(Settings settings)
        {
            var _setting = SearchByKey(settings.Key);
            if(_setting != null)
            {
                Update(_setting.Id, settings);
            } else
            {
                _context.Settings.Add(settings);
            }
            _context.SaveChanges();

            return _setting;
        }
    }
}
