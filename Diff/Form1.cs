using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diff
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public object JsonSerializer { get; private set; }

        private async Task button1_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync("https://localhost:44394/api/Stations/" + textBox1.Text);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = response.Content;
                    var data = await content.ReadAsStringAsync();

                    Station des = JsonSerializer.Deserialize<Station>(data);
                    new Form2(des).Show();
                }
                else
                {
                    Station st = new Station()
                    {
                        Id = Convert.ToInt32(textBox1.Text),
                        address = "",
                        fuel = "[\r\n         {\r\n            \"Name\": \"92\",\r\n            \"Price\": 0.0,\r\n            \"AmountOfFuel\": 0\r\n         },\r\n         {\r\n            \"Name\": \"95\",\r\n            \"Price\": 0.0,\r\n            \"AmountOfFuel\": 0\r\n         },\r\n         {\r\n            \"Name\": \"98\",\r\n            \"Price\": 0.0,\r\n            \"AmountOfFuel\": 0\r\n         },\r\n         {\r\n            \"Name\": \"Disel Fuel\",\r\n            \"Price\": 0.0,\r\n            \"AmountOfFuel\": 0\r\n         }\r\n      ]"
                    };
                    var input = new StringContent(JsonSerializer.Serialize(st), Encoding.UTF8, "application/json");
                    await client.PostAsync("https://localhost:44394/api/Stations/", input);
                    MessageBox.Show("Объект создан!");
                    textBox1.Clear();
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

        }
    }
    }
}
