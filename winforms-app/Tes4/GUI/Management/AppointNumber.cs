using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace Tes4.GUI.Management
{
    public partial class AppointNumber : Form
    {
        public AppointNumber()
        {
            InitializeComponent();
        }


        private async void SimpleButton1_ClickAsync(object sender, EventArgs e)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), "https://api.heroku.com/apps/priclinic/config-vars"))
                {
                    request.Headers.TryAddWithoutValidation("Accept", "application/vnd.heroku+json; version=3");
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer 5e131778-96f4-41a7-bcf8-712298bae689");
                    string str = "{\"maxSingleSlotCount\": " + numericUpDown1.Value + "}";
                    request.Content = new StringContent(str, Encoding.UTF8, "application/json");

                    var response = await httpClient.SendAsync(request);
                    this.Close();
                }
            }
        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AppointNumber_Load(object sender, EventArgs e)
        {

        }
    }
}
