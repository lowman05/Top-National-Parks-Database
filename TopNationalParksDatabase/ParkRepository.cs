﻿using Dapper;
using System.Data;
using System.Data.Common;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using TopNationalParksDatabase.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TopNationalParksDatabase
{
    public class ParkRepository : IParkRepository
    {
        private readonly IDbConnection _conn;
        public ParkRepository(IDbConnection conn)
        {
            _conn = conn;


        }

        public IEnumerable<Park> GetAllParks()
        {
            return _conn.Query<Park>("SELECT * FROM PARKS;");
        }

        public Park GetPark(int id)
        {
            return _conn.QuerySingle<Park>("SELECT * FROM PARKS WHERE ParkID = @id", new { id = id });            
        }

        public void UpdatePark(Park park)
        {
            _conn.Execute("UPDATE parks SET FullName = @fullname, ParkCode = @parkcode, PhoneNumber = @phonenumber, EmailAddress = @emailaddress, Website = @website WHERE ParkID = @id",
                new { fullname = park.FullName, parkcode = park.ParkCode, phonenumber = park.PhoneNumber, emailaddress = park.EmailAddress, website = park.Website, id = park.ParkID });
        }

        public void InsertPark(Park parkToInsert)
        {
            _conn.Execute("INSERT INTO parks (FULLNAME, PARKCODE, PHONENUMBER, EMAILADDRESS, WEBSITE) VALUES(@fullname, @parkcode, @phonenumber, @emailaddress, @website);",
                new { fullname = parkToInsert.FullName, parkcode = parkToInsert.ParkCode, phonenumber = parkToInsert.PhoneNumber, emailaddress = parkToInsert.EmailAddress, website = parkToInsert.Website, });
        }

        public void DeletePark(Park park)
        {
            _conn.Execute("DELETE FROM parks WHERE ParkID = @id;", new { id = park.ParkID });
            _conn.Execute("DELETE FROM reviews WHERE ParkId=@id;", new { id = park.ParkID });
        }

        public IEnumerable<Park> Gallery()
        {
            return _conn.Query<Park>("SELECT PhotoURL FROM PARKS");
        }

        public Park GetPreviousPark(int id)
        {
            return _conn.QueryFirstOrDefault<Park>("SELECT * FROM Parks WHERE ParkID < @id ORDER BY ParkID DESC LIMIT 1", new {id =id});
        }
        public Park GetNextPark(int id)
        {
            return _conn.QueryFirstOrDefault<Park>("SELECT * FROM Parks WHERE ParkID > @id ORDER BY ParkID ASC LIMIT 1", new { id = id});
        }

        public IEnumerable<Park> GetAllParkCodes()
        {
            return _conn.Query<Park>("SELECT ParkCode FROM PARKS");
        }

        public IEnumerable<Park> GetAlertsByParkCode()
        {
            var parkCodes = GetAllParkCodes();
            var appSettingsJson = File.ReadAllText("appsettings.json");
            var appSettings = JObject.Parse(appSettingsJson);
            var apiKey = appSettings["apiKeyObj"]["apiKey"].ToString();

           
            List<Park> alerts = new List<Park>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://developer.nps.gov/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                foreach (var parkCode in parkCodes)
                {
                    string alertURL = $"api/v1/alerts?parkCode={parkCode.ParkCode}&api_key={apiKey}";

                    HttpResponseMessage response = client.GetAsync(alertURL).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = response.Content.ReadAsStringAsync().Result;
                        var alertResponse = JObject.Parse(jsonString);
                        var data = alertResponse.GetValue("data").ToString();

                        // Deserialize the alerts for the current park code
                        var parkAlerts = JsonConvert.DeserializeObject<IEnumerable<Park>>(data);

                        // Assign the park code to each alert
                        foreach (var alert in parkAlerts)
                        {
                            alert.ParkCode = parkCode.ParkCode;
                        }

                        // Add the alerts to the list
                        alerts.AddRange(parkAlerts);
                    }
                }
            }

            return alerts;
        }



    }  
}       
