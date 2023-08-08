using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public string? Key { get; set; }
        public string? Value { get; set; }

        public Settings update(Settings settings)
        {
            Key = settings.Key;
            Value = settings.Value;
            return this;
        }
    }
}
