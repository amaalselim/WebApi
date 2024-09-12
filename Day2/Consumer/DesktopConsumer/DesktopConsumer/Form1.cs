using Microsoft.VisualBasic;
using System.Net.Http.Json;

namespace DesktopConsumer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        HttpClient client = new HttpClient();

        private async void button1_Click(object sender, EventArgs e)
        {

            client.BaseAddress = new Uri("http://localhost:35769/");
            Department dept1 = await client.GetFromJsonAsync<Department>("api/department/1");
            label1.Text = dept1.Name;
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }


        private async void button2_Click(object sender, EventArgs e)
        {
            Department d = new Department()
            {
                Id=0,
                Name = "From Wind",
                Manager = "Amaal"
            };
            HttpResponseMessage result = await client.PostAsJsonAsync<Department>
                ("http://localhost:35769/api/department", d);
            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                MessageBox.Show("Created Success");
            }
            else
            {
                MessageBox.Show(result.StatusCode.ToString());
            }
        }

    }
}
