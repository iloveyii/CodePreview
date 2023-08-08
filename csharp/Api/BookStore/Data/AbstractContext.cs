using System;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
	public abstract class AbstractContext : DbContext
	{
        public AbstractContext(DbContextOptions options) : base(options)
        {
        }

    }
}

