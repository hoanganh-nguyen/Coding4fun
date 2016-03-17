using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TimeSeries.Core.Model
{
    public class Price
    {
        [Key]
        public DateTime Date { get; set; }
        public double Value { get; set; }
        [Key]
        public virtual DataAsset Asset { get; set; }
    }
}
