using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualBasic;


namespace kursach
{
    public partial class Form1 : Form
    {
        private string nameToFind = "";

        //Создаём вторую форму
        private Form2 form2 = new Form2();
        

        private XmlSerializer xmls = new XmlSerializer(typeof(List<alco>));
        public Form1()
        {
            InitializeComponent();
            bindingSource1.DataSource = new List<alco>();
            name2.Tag = 0;
            sort2.Tag = 1;
            prise2.Tag = 2;
            AddOwnedForm(form2);
            

        }
        private int CompareByName(alco a, alco b)
        {

            return a.Name.CompareTo(b.Name);
        }

        private int CompareBySort(alco a, alco b)
        {
            return a.Sort.CompareTo(b.Sort);
        }

        private int CompareByPrice(alco a, alco b)
        {
            return a.Price - b.Price;
        }


        private void SaveData(string name)
        {
            if (name == "" || dataGridView1.RowCount == 1)
                return;
            if (dataGridView1.CurrentRow.IsNewRow)
                dataGridView1.CurrentCell =
             dataGridView1[0, dataGridView1.RowCount - 2];

            StreamWriter sw = new StreamWriter(name, false, Encoding.Default);
            xmls.Serialize(sw, bindingSource1.DataSource);
            sw.Close();

        }



        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].IsNewRow)
                return;

            string err = "", s = e.FormattedValue.ToString();
            switch (e.ColumnIndex)
            {

                case 0:// Название
                    if (s == "")
                        err = "Поле \"Название\" должно быть непустым";
                    break;
                case 1:   // Крпеость
                    int i1;
                    if (!int.TryParse(s, out i1))
                        err = "Строку нельзя преобразовать в число";
                    else
                        if ((i1 < 0) | (i1 > 100))
                            err = "Отрицательные числа и числа больше 100 не допускаются";
                    break;
                case 2:   // Наименование
                    if (s == "")
                        err = "Поле \"Наименование\" должно быть непустым";
                    break;
                case 3:    // Цена
                    int i2;
                    if (!int.TryParse(s, out i2))
                        err = "Строку нельзя преобразовать в число";
                    else
                        if (i2 < 0)
                            err = "Отрицательные числа не допускаются";
                    break;
                case 4:   // Количество
                    int i;
                    if (!int.TryParse(s, out i))
                        err = "Строку нельзя преобразовать в число";
                    else
                        if (i < 0)
                            err = "Отрицательные числа не допускаются";
                    break;
                case 5:   // Галка
                    break;

            }
            e.Cancel = err != "";
            dataGridView1.Rows[e.RowIndex].ErrorText = err;


        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            saveAs1.Enabled = dataGridView1.RowCount > 1;
        }

        private void new1_Click(object sender, EventArgs e)
        {
            SaveData(saveFileDialog1.FileName);
            bindingSource1.DataSource = new List<alco>();
            dataGridView1.CurrentCell = dataGridView1[0, 0];
            saveFileDialog1.FileName = "";
            Text = "Alco";

        }

        private void open1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SaveData(saveFileDialog1.FileName);
                string s = openFileDialog1.FileName;
                StreamReader sr = new StreamReader(s, Encoding.Default);
                bindingSource1.SuspendBinding();
                bindingSource1.DataSource = xmls.Deserialize(sr);
                bindingSource1.ResumeBinding();
                sr.Close();
                saveFileDialog1.FileName = s;
                Text = "Alco - " + Path.GetFileNameWithoutExtension(s);
            }

        }

        private void save1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string s = saveFileDialog1.FileName;
                SaveData(s);
                Text = "Alco - " + Path.GetFileNameWithoutExtension(s);
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData(saveFileDialog1.FileName);
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            find1.Enabled = 
                menuStrip1.Enabled = !dataGridView1.IsCurrentCellDirty;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
           find1.Enabled = 
            bindingNavigatorDeleteItem.Enabled =
                !dataGridView1.Rows[e.RowIndex].IsNewRow;

        }

        private void name2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 1)
                return;
            dataGridView1.CurrentCell = dataGridView1[0, 0];
            Comparison<alco> comp = CompareByName;
            switch ((int)(sender as ToolStripMenuItem).Tag)
            {
                case 1:
                    comp = CompareBySort;
                    break;
                case 2:
                    comp = CompareByPrice;
                    break;

            }
            (bindingSource1.DataSource as List<alco>).Sort(comp);
            bindingSource1.ResetBindings(false);


        }
        //Поиск
        private void find1_Click(object sender, EventArgs e)
        {
            nameToFind =
            Interaction.InputBox("Введите начальную часть названия для поиска:",
            "Поиск по названию", nameToFind, -1, -1).Trim();
            if (nameToFind == "")
                return;
            int ind = (bindingSource1.DataSource as
            List<alco>).FindIndex(dataGridView1.CurrentRow.Index,
            delegate(alco a)
            {
                return a.Name.StartsWith(nameToFind,
                StringComparison.OrdinalIgnoreCase);
            });
            if (ind != -1)
                dataGridView1.CurrentCell = dataGridView1[1, ind];
            else
                MessageBox.Show("Название не найдено", "Поиск по названию");

        }
        //Подсчет стомости
              private void button1_Click(object sender, EventArgs e)
               {
                   
                   int i,s;
                   s=0;
                   for (i = 0; i <dataGridView1.RowCount; i++)
                   {
                       if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[5].Value) == true)
                           s += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value) * 
                           Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                   }

          
            
                   label1.Text = "Стоимость заказа равна: "+s;
               }
              //Отчистка столбцов выбора
              private void button2_Click(object sender, EventArgs e)
              {
                  int i, s;
                  s = 0;
                  for (i = 0; i < dataGridView1.RowCount; i++)
                  {
                      dataGridView1.Rows[i].Cells[5].Value = 0;
                      dataGridView1.Rows[i].Cells[4].Value = 0;
                        
                  }



                  label1.Text = "Стоимость заказа равна: " + s;
              }
              //Открытие подчиненной формы
              private void button3_Click(object sender, EventArgs e)
              {
                  form2.Owner = this;
                  form2.ShowDialog();
              }
              //Передача данных в bindingSource1
              public void vvod(string name3, int spirt3, string sort3, int price3)
              {
                  
                 
                 alco nn = new alco();
                  nn.Name=name3;
                  nn.Spirt=spirt3;
                  nn.Sort=sort3;
                  nn.Price=price3;
                  nn.Kol=0;
                  nn.Scholarship=false;
                  bindingSource1.Add(nn);
                  
              }

              
         


    }
}

