using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using OEC.Model;

namespace OEC
{
    public class SpreadsheetManager
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "osu! Tournament Elo Calculator";
        const string TABLE_NAME = "test";

        public static void UpdateSheet(List<Player> players)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            

            String spreadsheetId = "1fSBElkpi6uQjHgbcMU7wP8-tS52Udmjof5zTq0naWE4";
            String range = TABLE_NAME + "!" + "A1";
            ValueRange vRange = new ValueRange();
            vRange.MajorDimension = "COLUMNS";
            var nameList = new List<object>();
            var eloList = new List<object>();
            foreach (Player player in players)
            {
                nameList.Add(player.PlayerName);
                eloList.Add(player.Elo);
            }
            vRange.Values = new List<IList<object>> { nameList, eloList };
            SpreadsheetsResource.ValuesResource.UpdateRequest request =
                service.Spreadsheets.Values.Update(vRange, spreadsheetId, range);

            request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse result2 = request.Execute();


            /* // Define request parameters.
             String spreadsheetId = "1fSBElkpi6uQjHgbcMU7wP8-tS52Udmjof5zTq0naWE4";
             String range = "test!A1:B2";
             SpreadsheetsResource.ValuesResource.GetRequest request =
                     service.Spreadsheets.Values.Get(spreadsheetId, range);

             // Prints the names and majors of students in a sample spreadsheet:
             // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
             ValueRange response = request.Execute();
             IList<IList<Object>> values = response.Values;
             if (values != null && values.Count > 0)
             {
                 Console.WriteLine("First cell, second cell:");
                 foreach (var row in values)
                 {
                     // Print columns A and E, which correspond to indices 0 and 4.
                     Console.WriteLine("{0}, {1}", row[0], row[4]);
                 }
             }
             else
             {
                 Console.WriteLine("No data found.");
             }
             Console.Read();*/
        }
    }
}