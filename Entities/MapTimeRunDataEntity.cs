using MySqlConnector;

namespace SurfTimer.Shared.Entities
{
	public class MapTimeRunDataEntity : RunStatsEntity
	{
		public int PlayerID { get; set; }
		public int MapID { get; set; }
		public int Rank { get; set; }
		public int TotalCount { get; set; }

		public MapTimeRunDataEntity() { }

		/// <summary>
		/// Assigns data from MySqlDataReader (MySQL query) to the needed data model
		/// </summary>
		public MapTimeRunDataEntity(MySqlDataReader data)
		{
			ID = data.GetInt32("id");
			RunTime = data.GetInt32("run_time");
			Rank = data.GetInt32("rank");
			StartVelX = (float)data.GetDouble("start_vel_x");
			StartVelY = (float)data.GetDouble("start_vel_y");
			StartVelZ = (float)data.GetDouble("start_vel_z");
			EndVelX = (float)data.GetDouble("end_vel_x");
			EndVelY = (float)data.GetDouble("end_vel_y");
			EndVelZ = (float)data.GetDouble("end_vel_z");
			RunDate = data.GetInt32("run_date");
		}
	}
}
