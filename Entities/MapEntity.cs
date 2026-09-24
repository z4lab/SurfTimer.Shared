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
		/// <summary>
		/// Staged map where stage starts (except stage 1 / map start) allow bhopping without the start-zone speed cap.
		/// Only set in the DB - the plugin reads it but never writes it.
		/// </summary>
		public bool StagedLinear { get; set; }
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
			StagedLinear = data.GetBoolean("staged_linear");
			DateAdded = data.GetInt32("date_added");
			LastPlayed = data.GetInt32("last_played");
		}
	}
}
