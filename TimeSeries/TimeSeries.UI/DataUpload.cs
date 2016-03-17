using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeSeries.Core.DataAccess;
using TimeSeries.Core.Helper;
using TimeSeries.Core.Model;
using TimeSeries.Core.Xml;

namespace TimeSeries.UI
{
    public partial class DataUpload : Form
    {
        string _user;
        public DataUpload(string user)
        {
            InitializeComponent();
            _user = user;
            btSave.Enabled = false;
        }

        DataAsset _dataAsset;
     
        private void btLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML Files (.xml)|*.xml";
            if(openFile.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = openFile.FileName;
                LoadFromFile();
                btSave.Enabled = true;
            }
        }

        private void LoadFromFile()
        {
            try
            {
                DataAssetParser parser = new DataAssetParser(_user);
                 _dataAsset = parser.Parse(File.ReadAllText(txtFileName.Text));
                if(_dataAsset != null)
                {
                    lbAsset.Text = _dataAsset.Asset;
                    
                    IBindingList list = new SortableBindingList<Price>(_dataAsset.Price.ToList());
                    dataGridView1.DataSource = list;
                    dataGridView1.Sort(dataGridView1.Columns["Date"], ListSortDirection.Descending);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error",ex.Message);
            }
               
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dataAsset != null)
                {
                    using (var db = new TimeSeriesContext())
                    {
                        db.DataAssets.Add(_dataAsset);
                        db.Prices.AddRange(_dataAsset.Price);
                        db.SaveChanges();
                        MessageBox.Show("Save into database succesful");
                    }
                }
                else
                    MessageBox.Show("Nothing hanppens");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
