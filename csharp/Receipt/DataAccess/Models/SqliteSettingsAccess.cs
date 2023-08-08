using DataAccess.Models;
using DataModel.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Storage;

namespace DbAccess.Models
{
    public class SqliteSettingsAccess
    {
        public static void Init()
        {
            DatabaseFacade facade = new DatabaseFacade(new SettingsContext());
            facade.EnsureCreated();
        }

        public static bool create(Settings settings)
        {
            using (SettingsContext context = new SettingsContext())
            {
                context.Settings.Add(settings);
                context.SaveChanges();
                return true;
            }
        }

        public static List<Settings>? read()
        {
            using (SettingsContext context = new SettingsContext())
            {
                var settings = context.Settings;
                if (settings == null)
                {
                    return null;
                }
                return settings.ToList();
            }
        }

        public static Settings? readOne(int Id)
        {
            var settings = read();
            Settings _settings = settings.Find(x => x.Id == Id);
            return _settings;
        }

        public static Settings? update(Settings settings)
        {
            var _settings = readOne(settings.Id);
            using (SettingsContext context = new SettingsContext())
            {
               
                if (_settings != null)
                {
                    _settings = settings;
                    context.Update(settings);
                    context.SaveChanges();
                    return _settings;
                }
                return null;
            }
        }
    }
}
