using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TimeSeries.Core.Model;

namespace TimeSeries.Core.Xml
{

    public interface IDataAssetParser
    {
        DataAsset Parse(string xml);
    }

    public class DataAssetParser : IDataAssetParser
    {
        bool _ignoreError;
        string _importedByUser;
        public DataAssetParser(string user,bool keepGoOnDepisteError = true )
        {
            _ignoreError = keepGoOnDepisteError;
            _importedByUser = user;
        }


        public DataAsset Parse(string xml)
        {
            var ret = new DataAsset();
            ret.ImportedBy = _importedByUser;
            XDocument doc = XDocument.Parse(xml);
            ret.Asset = doc.Root.Attribute("Asset").Value.ToString();
            foreach (var priceElement in doc.Descendants("Price"))
            {
                try
                {
                    var price = BuildPriceItem(priceElement);
                    price.Asset = ret;
                    ret.Price.Add(price);
                }
                catch (Exception ex)
                {
                    if (!_ignoreError)
                    {
                        string errorMsg = String.Format("Issue during th parse data from xml.Details: {0}", ex.Message);
                        throw;
                    }
                }
            }

            return ret;
        }

        private Price BuildPriceItem(XElement element)
        {
            var price = new Price();
            string dateText = element.Attribute("Date").Value.ToString();
            string valueText = element.Attribute("Value").Value.ToString();
            price.Date = DateTime.ParseExact(dateText, "yyyyMMdd", new CultureInfo("en-US"));
            price.Value = Convert.ToDouble(valueText.Replace('.', ','));
            return price;
        }
    }
}
