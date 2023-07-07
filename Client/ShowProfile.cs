﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Client
{
    public partial class ShowProfile : Form
    {
        private string userId;
        private string Introduction;
        private string textboxUsername;
        public ShowProfile(string userid, string username)
        {
            InitializeComponent();
            this.userId = userid;
            this.textboxUsername = username;
            richTextBox1.Enabled = false;
            txtUsername.Enabled = false;
            bool getResult = GetProfile(userid);
            if (getResult)
            {

            }
            else
            {
                MessageBox.Show("error in get profile");
                richTextBox1.Text = "ERROR";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool GetProfile(string userId)
        {
            using (HttpClient client = new HttpClient())
            {
                // Set the base URL of your API server
                client.BaseAddress = new Uri($"https://localhost:7276/api/Profile/get/{userId}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = client.GetAsync("").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        JObject json = JObject.Parse(responseBody);
                        string getIntroduction = (string)json["introduction"];
                        string getProfileId = (string)json["id"];
                        richTextBox1.Text = getIntroduction.Trim();
                        txtUsername.Text = textboxUsername;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    // Handle any exceptions that occurred during the API request
                    return false;
                }
            }
        }



        private async void ShowProfile_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
