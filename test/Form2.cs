using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            using (employees db = new employees())
            {
                var list = from s in db.status
                           select s.name;

                if (list.FirstOrDefault() == null) this.Close();
                foreach (var x in list)
                {
                    comboBox1.Items.Add(x);
                }
                comboBox1.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> dates = new Dictionary<string, int>();

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            using (employees db = new employees())
            {
                if (db.persons.FirstOrDefault() == null) return;
                if (radioButton1.Checked)
                {
                    var list1 = from d in db.persons
                                where d.status == comboBox1.SelectedIndex + 1
                                group d by EntityFunctions.TruncateTime(d.date_employ) into _date
                                select new { date= _date , amount = _date.Count()};
                    dates = list1.ToDictionary(mc => mc.date.Key.ToString(),
                                 mc => mc.amount);
                }
                else
                {
                    var list1 = from d in db.persons
                                where d.status == comboBox1.SelectedIndex + 1
                                group d by EntityFunctions.TruncateTime(d.date_uneploy) into _date
                                select new { date = _date, amount = _date.Count() };
                    dates = list1.ToDictionary(mc => mc.date.Key.ToString(),
                                 mc => mc.amount);
                }

                DataGridViewRow row = new DataGridViewRow();
                foreach (KeyValuePair<string,int> pair in dates)
                {
                    dataGridView1.Columns.Add("", pair.Key);
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = pair.Value });
                }
                dataGridView1.Rows.Add(row);


            }
        }
    }
}
