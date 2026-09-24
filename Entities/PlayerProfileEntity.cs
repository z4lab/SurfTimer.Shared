using MySqlConnector;

namespace SurfTimer.Shared.Entities
{
	public class PlayerProfileEntity
	{
		public int ID { get; set; }
		public string? Name { get; set; }
		public ulong SteamID { get; set; }
		public string? Country { get; set; }
		public int JoinDate { get; set; }
		public int LastSeen { get; set; }
		public int Connections { get; set; }

		public PlayerProfileEntity() { }

		/// <summary>
		/// Assigns data from MySqlDataReader (MySQL query) to the needed data model
		/// </summary>
		public PlayerProfileEntity(MySqlDataReader data)
		{
			ID = data.GetInt32("id");
			Name = data.GetString("name");
			Country = data.GetString("country");
			JoinDate = data.GetInt32("join_date");
			LastSeen = data.GetInt32("last_seen");
			Connections = data.GetInt32("connections");
		}
	}
}
