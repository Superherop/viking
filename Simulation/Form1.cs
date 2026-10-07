using Kozos;
using System.Drawing.Text;

namespace Simulation
{
    public partial class Form1 : Form
    {
        private List<int> tippek = new List<int>();
        private VikingContext context = new VikingContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBezar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnSorsol.Enabled = false;
            this.Size = new Size(900, 600);
            panel.Left = 30;
            panel.Top = 30;
            panel.Size = new Size(400, 300);
            panel.BackColor = Color.White;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    CheckBox box = new CheckBox();
                    box.Left = j * 50 + 10;
                    box.Top = i * 50 + 10;
                    box.AutoSize = true;
                    box.Text = (i * 8 + j + 1).ToString();

                    this.panel.Controls.Add(box);
                    box.CheckedChanged += checkBox1_CheckedChanged;
                }
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;

            if (box.Checked)
            {
                tippek.Add(int.Parse(box.Text));
                label1.Text = string.Join(",", tippek);

                if (tippek.Count == 6)
                {
                    btnSorsol.Enabled = true;
                    foreach (Control control in panel.Controls)
                    {
                        if (control is CheckBox)
                        {
                            control.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                tippek.Remove(int.Parse(box.Text));
                btnSorsol.Enabled = false;
            }
            
        }

        private void btnSorsol_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            HashSet<int> halmaz = new HashSet<int>();
            do
            {
                halmaz.Add(rnd.Next(1,49));
            }while (halmaz.Count < 6);

            
            label1.Text ="Kisorsolt számok:\n"+ 
                string.Join(",", halmaz.OrderBy(x => x))+
                "\nTippeid:\n"+string.Join(",", tippek.OrderBy(x => x))+
                "\n"+halmaz.Intersect(tippek).Count()+" találatod volt";

            context.LottoSzamok.Add(new Szamok(string.Join(";",halmaz.OrderBy(x => x))));
            context.SaveChanges();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            context.Dispose();
            base.OnFormClosing(e);
        }
    }
}
