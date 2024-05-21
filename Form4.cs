using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midtermProgLab
{
    public partial class Form4 : Form
    {
        private Timer timer;
        private int elapsedTime = 0;

        public Form4()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 500; // Set the timer interval to 500 milliseconds (0.5 seconds)
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTime += timer.Interval;
            if (elapsedTime == 500)
            {
                progressBar1.Value = 20;
            }
            else if (elapsedTime == 1500)
            {
                progressBar1.Value = 50;
            }
            else if (elapsedTime == 3500)
            {
                progressBar1.Value = 80;
            }
            else if (elapsedTime >= 5000)
            {
                timer.Stop();
                ShowForm2();
            }
        }

        private void ShowForm2()
        {
            Form1 form2 = new Form1();
            form2.FormClosed += (s, e) => Application.Exit();
            form2.Show();
            this.Hide();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
        }

        private void Form4_Load(object sender, EventArgs e)
        {
        }
    }
}
