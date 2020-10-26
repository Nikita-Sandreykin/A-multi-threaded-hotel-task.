using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace Lab3_v22
{
    public partial class Form1 : Form
    {
        List<Посетитель> room_200;
        List<Посетитель> room_400;
        List<Посетитель> room_600;
        List<Посетитель> incoming;
        BackgroundWorker _200;
        BackgroundWorker _400;
        BackgroundWorker _600;
        BackgroundWorker waiters;
        BackgroundWorker main;
        public static bool one = false;
        public static bool two = false;
        public static bool three = false;
        public static int i = 0;
        public static int count_200 = 0;
        public static int count_400 = 0;
        public static int count_600 = 0;
        public Form1()
        {
            InitializeComponent();
        }
        async void main_f()
        {
            while (true)
            {
                Action action = () =>
                {
                    label11.Text = count_200.ToString();
                    label12.Text = count_400.ToString();
                    label13.Text = count_600.ToString();
                };
                Invoke(action);
                Thread.Sleep(50);
            }
        }
        async void incoming_f()
        {
            while (true)
            {
                for (int j = 0; j < incoming.Count; j++)
                {
                    Action action = () =>
                    {
                        if (incoming[j].n >= 600)
                        {
                            if (room_600.Count < 3)
                            {
                                room_600.Add(incoming[j]);
                                count_600++;
                                one = true;
                                incoming.RemoveAt(j);
                            }
                            else if (room_400.Count < 5)
                            {
                                room_400.Add(incoming[j]);
                                count_400++;
                                two = true;
                                incoming.RemoveAt(j);
                            }
                            else if (room_200.Count < 3)
                            {
                                room_200.Add(incoming[j]);
                                count_200++;
                                three = true;
                                incoming.RemoveAt(j);
                            }
                        }
                        else if (incoming[j].n >= 400)
                        {
                            if (room_400.Count < 5)
                            {
                                room_400.Add(incoming[j]);
                                count_400++;
                                two = true;
                                incoming.RemoveAt(j);
                            }
                            else if (room_200.Count < 3)
                            {
                                room_200.Add(incoming[j]);
                                count_200++;
                                three = true;
                                incoming.RemoveAt(j);
                            }
                        }
                        else
                        {
                            if (room_200.Count < 3)
                            {
                                room_200.Add(incoming[j]);
                                count_200++;
                                three = true;
                                incoming.RemoveAt(j);
                            }
                        }
                    };
                    Invoke(action);
                    Thread.Sleep(50);
                }
            }
        }
        async void using_200(object a)
        {
            Посетитель temp = (Посетитель)a;
            Action action = () =>
            {
                temp.label6.Text = "В номере за 200 рублей";
            };
            Invoke(action);
            Thread.Sleep(10000);
            Action action2 = () =>
            {
                temp.Close();
                count_200--;
                room_200.Remove(temp);
            };
            Invoke(action2);
        }
        async void using_400(object a)
        {
            Посетитель temp = (Посетитель)a;
            Action action = () =>
            {
                temp.label6.Text = "В номере за 400 рублей";
            };
            Invoke(action);
            Thread.Sleep(10000);
            Action action2 = () =>
            {
                temp.Close();
                count_400--;
                room_400.Remove(temp);
            };
            Invoke(action2);
        }
        async void using_600(object a)
        {
            Посетитель temp = (Посетитель)a;
            Action action = () =>
            {
                temp.label6.Text = "В номере за 600 рублей";
            };
            Invoke(action);
            Thread.Sleep(10000);
            Action action2 = () =>
            {
                temp.Close();
                count_600--;
                room_600.Remove(temp);
            };
            Invoke(action2);
        }
        async void _200_f()
        {
            while (true)
            {
                if (three)
                {
                    for (int j = 0; j < room_200.Count(); j++)
                    {
                        if (room_200[j].check)
                        {
                            Thread temp = new Thread(new ParameterizedThreadStart(using_200));
                            temp.Start(room_200[j]);
                            room_200[j].check = false;
                            Thread.Sleep(50);
                        }
                    }
                    three = false;
                }
            }
        }
        async void _400_f()
        {
            while (true)
            {
                if (two)
                {
                    for (int j = 0; j < room_400.Count(); j++)
                    {
                        if (room_400[j].check)
                        {
                            Thread temp = new Thread(new ParameterizedThreadStart(using_400));
                            temp.Start(room_400[j]);
                            room_400[j].check = false;
                            Thread.Sleep(50);
                        }
                    }
                    two = false;
                }
            }
        }
        async void _600_f()
        {
            while (true)
            {
                if (one)
                {
                    for (int j = 0; j < room_600.Count(); j++)
                    {
                        if (room_600[j].check)
                        {
                            Thread temp = new Thread(new ParameterizedThreadStart(using_600));
                            temp.Start(room_600[j]);
                            room_600[j].check = false;
                            Thread.Sleep(50);
                        }
                    }
                    one = false;
                }
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            label1.Visible = true; label2.Visible = true; label3.Visible = true; label4.Visible = true;
            label5.Visible = true; label6.Visible = true; label7.Visible = true; label8.Visible = true;
            label9.Visible = true; label10.Visible = true; label11.Visible = true; label12.Visible = true;
            label13.Visible = true; label14.Visible = true; label15.Visible = true; label16.Visible = true;
            textBox1.Visible = true; button2.Visible = true; button1.Visible = false;
            room_600 = new List<Посетитель>();
            room_400 = new List<Посетитель>();
            room_200 = new List<Посетитель>();
            incoming = new List<Посетитель>();
            main = new BackgroundWorker();
            main.DoWork += (obj, ea) => main_f();
            main.RunWorkerAsync();
            _200 = new BackgroundWorker();
            _200.DoWork += (obj, ea) => _200_f();
            _200.RunWorkerAsync();
            _400 = new BackgroundWorker();
            _400.DoWork += (obj, ea) => _400_f();
            _400.RunWorkerAsync();
            _600 = new BackgroundWorker();
            _600.DoWork += (obj, ea) => _600_f();
            _600.RunWorkerAsync();
            waiters = new BackgroundWorker();
            waiters.DoWork += (obj, ea) => incoming_f();
            waiters.RunWorkerAsync();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Label16_Click(object sender, EventArgs e)
        {

        }

        private void Label15_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            i++;
            int n = Convert.ToInt32(textBox1.Text);
            if (n < 200) { MessageBox.Show("У посетителя слишком мало денег", "Ошибка", MessageBoxButtons.OK); }
            else
            {
                Посетитель temp = new Посетитель();
                temp.check = true;
                temp.n = n;
                incoming.Add(temp);
                temp.label4.Text = i.ToString();
                temp.label5.Text = n.ToString();
                temp.Show();
            }
        }
    }
}
