using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); 
            dataGridView2.Columns.Add("Name", "Name");
            dataGridView2.Columns.Add("Post", "Post");
            dataGridView2.Columns.Add("Status", "Status");
            dataGridView2.Columns.Add("Department", "Department");
            dataGridView2.Columns.Add("Date_employed", "Date_employed");
            dataGridView2.Columns.Add("Date_Fired", "Date_Fired");
        }

        private void persons_list_button_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            using (employees db = new employees())
            {
                var list = from person in db.persons
                           join dep in db.deps on person.id_dep equals dep.id
                           join stat in db.status on person.status equals stat.id
                           join post in db.posts on person.id_post equals post.id
                           select new { f_name = person.first_name, s_name = person.second_name , l_name= person.last_name, _Post = post.name, _Status = stat.name, _Department = dep.name, _Date_employed = person.date_employ, _Date_Fired = person.date_uneploy };
                
                // как я читал отложенный запрос выполняется по факту востребования, поэтому фильтры пойдут в запрос, а не будут применены к выполненному запросу
                if (textBox1.Text != "") list = list.Where(s => s._Status == textBox1.Text);
                if (textBox2.Text != "") list = list.Where(d => d._Department == textBox2.Text);
                if (textBox3.Text != "") list = list.Where(p => p._Post == textBox3.Text);
                if (textBox4.Text != "") list = list.Where(f => f.s_name.Contains(textBox4.Text));

                if (radioButton1.Checked) list = list.OrderBy(s => s._Status);
                if (radioButton2.Checked) list = list.OrderBy(s => s._Department);
                if (radioButton3.Checked) list = list.OrderBy(s => s._Post);
                if (radioButton4.Checked) list = list.OrderBy(s => s.s_name);

                var list1 = from p in list
                       select new { Name = p.f_name + " " + p.s_name + " " + p.l_name, Post = p._Post, Status = p._Status, Department = p._Department, Date_employed = p._Date_employed, Date_Fired = p._Date_Fired };

                if (list1.FirstOrDefault() == null)
                {
                    dataGridView2.Rows.Clear();
                    return;
                }

                foreach (var p in list1)
                {
                    DataGridViewRow r = new DataGridViewRow();
                    foreach (var c in p.GetType().GetProperties())
                    {
                        r.Cells.Add(new DataGridViewTextBoxCell { Value = c.GetValue(p,null) });
                    }
                    dataGridView2.Rows.Add(r);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (employees db = new employees())
            {
                persons p1 = new persons() { first_name = "Vasya", second_name = "Pupkin", id = 0, date_employ = new DateTime(2008, 5, 1, 8, 30, 52) ,id_dep =1, id_post=0,last_name="Viktorovich", status = 0};
                db.persons.Add(p1);

                deps dep = new deps() { id=1,name="HR"};
                db.deps.Add(dep);

                status status = new status() {id=0, name="Working" };
                db.status.Add(status);

                posts post = new posts() {id=0,name = "Department head" };
                db.posts.Add(post);

                db.SaveChanges();
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var x = new Form2();
            x.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (employees db = new employees())
            {

                List<persons> pers1 = new List<persons>();
                pers1.Add(new persons() { first_name = "Vasya", second_name = "Pupkin", id = 1, date_employ = new DateTime(2008, 5, 1, 8, 30, 52), id_dep = 1, id_post = 1, last_name = "Viktorovich", status = 1 });
                pers1.Add(new persons() { first_name = "Sergey", second_name = "Lebedev", id = 2, date_employ = new DateTime(2008, 6, 2, 9, 40, 52), id_dep = 2, id_post = 2, last_name = "Petrovich", status = 1 });
                pers1.Add(new persons() { first_name = "Artem", second_name = "Vasilyev", id = 3, date_employ = new DateTime(2008, 7, 3, 10, 40, 52), id_dep = 3, id_post = 3, last_name = "Igorevich", status = 2 });
                pers1.Add(new persons() { first_name = "Dmitriy", second_name = "Pletnev", id = 4, date_employ = new DateTime(2008, 7, 3, 10, 40, 52), id_dep = 2, id_post = 2, last_name = "Michailovich", status = 2 });
                db.persons.AddRange(pers1);

                List<deps> deps1 = new List<deps>();
                deps1.Add(new deps() { id = 1, name = "HR" });
                deps1.Add(new deps() { id = 2, name = "Project_Department" });
                deps1.Add(new deps() { id = 3, name = "Research_Department" });
                db.deps.AddRange(deps1);

                List<status> status1 = new List<status>();
                status1.Add(new status() { id = 1, name = "Working" });
                status1.Add(new status() { id = 2, name = "Not_Working" });
                db.status.AddRange(status1);

                List<posts> post = new List<posts>();
                post.Add(new posts() { id = 1, name = "Department_head" });
                post.Add(new posts() { id = 2, name = "Senior" });
                post.Add(new posts() { id = 3, name = "Middle" });
                db.posts.AddRange(post);

                db.SaveChanges();

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (employees db = new employees())
            {
                foreach (var x in db.persons)
                {
                    db.persons.Remove(x);
                }
                foreach (var x in db.deps)
                {
                    db.deps.Remove(x);
                }
                foreach (var x in db.posts)
                {
                    db.posts.Remove(x);
                }
                foreach (var x in db.status)
                {
                    db.status.Remove(x);
                }
                
                db.SaveChanges();

            }


        }
    }
}
