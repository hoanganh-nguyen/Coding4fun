using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSeries.Core.Model
{
    [Serializable]
    public class DataAsset
    {
        [Key]
        public Guid Id { get; set; }
        public string Asset { get; set; }
        public string ImportedBy { get; set; }
        public virtual ICollection<Price> Price { get; set; }
        public DataAsset()
        {
            Price = new List<Price>();
            Id = Guid.NewGuid();

        }
    }
}
