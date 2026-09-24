using MySqlConnector;

namespace SurfTimer.Shared.Entities
{
	public class MapEntity
	{
		public int ID { get; set; }
		public string? Name { get; set; }
		public string? Author { get; set; }
		public short Tier { get; set; }
		public short Stages { get; set; }
		public short Bonuses { get; set; }
		public bool Ranked { get; set; }
		public int DateAdded { get; set; }
		public int LastPlayed { get; set; }

		public MapEntity() { }

		/// <summary>
		/// Assigns data from MySqlDataReader (MySQL query) to the needed data model
		/// </summary>
		public MapEntity(MySqlDataReader data)
		{
			ID = data.GetInt16("id");
			Name = data.GetString("name");
			Author = data.GetString("author") ?? "Unknown";
			Tier = data.GetInt16("tier");
			Ranked = data.GetBoolean("ranked");
			DateAdded = data.GetInt32("date_added");
			LastPlayed = data.GetInt32("last_played");
		}
	}
}
