using OEC.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
namespace OEC
{
    public static class DatabaseConnector
    {
        public static void InsertNewPlayer(MatchPlayer player)
        {
            string connectionString;
            SqlConnection cnn;
            string sql;
            SqlDataAdapter adapter = new SqlDataAdapter();
            connectionString = @"Data Source=localhost;Initial Catalog=EloData;User ID=lwl;Password=bancho";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            sql = "INSERT INTO players (uid, rating, number_of_maps) VALUES('" + player.PlayerID + "', " + player.NewElo + ", " + player.MapsPlayed +")"; 
            adapter.InsertCommand = new SqlCommand(sql, cnn); 
            adapter.InsertCommand.ExecuteNonQuery();
            cnn.Close();
        }

        public static void UpdatePlayer(MatchPlayer player, int previousPlayed)
        {
            string connectionString;
            SqlConnection cnn;
            string sql;
            SqlDataAdapter adapter = new SqlDataAdapter();
            connectionString = @"Data Source=localhost;Initial Catalog=EloData;User ID=lwl;Password=bancho";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            sql = "UPDATE players SET rating=" + player.NewElo + " WHERE uid=" + player.PlayerID;
            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();
            sql = "UPDATE players SET number_of_maps=" + previousPlayed + player.MapsPlayed + " WHERE uid=" + player.PlayerID;
            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();
            cnn.Close();
        }

        public static void GetPlayerData(string playerID)
        {
            string connectionString;
            SqlConnection cnn;
            string sql;
            SqlDataAdapter adapter = new SqlDataAdapter();
            connectionString = @"Data Source=localhost;Initial Catalog=EloData;User ID=lwl;Password=bancho";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            sql = "SELECT";
            adapter.SelectCommand = new SqlCommand(sql, cnn);
            adapter.SelectCommand.ExecuteQuery();
            cnn.Close();
        }
    }
}
*/