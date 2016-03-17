using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeSeries.Core.Model;

namespace TimeSeries.Core.DataAccess
{
    public class TimeSeriesContext:DbContext
    {
        public DbSet<DataAsset> DataAssets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Price> Prices { get; set; }

    }
}
