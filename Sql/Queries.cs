namespace SurfTimer.Shared.Sql
{
	/// <summary>
	/// Contains all the queries used by MySQL for the SurfTimer plugin.
	/// </summary>
	public static class Queries
	{
		public const string DB_QUERY_PING = "SELECT 1;";

		// Map.cs related queries
		public const string DB_QUERY_MAP_GET_INFO = "SELECT * FROM Maps WHERE name=@MapName;";
		public const string DB_QUERY_MAP_INSERT_INFO =
			"INSERT INTO Maps (name, author, tier, stages, bonuses, ranked, date_added, last_played) VALUES (@Name, @Author, @Tier, @Stages, @Bonuses, @Ranked, @DateAdded, @LastPlayed)"; // "INSERT INTO Maps (name, author, tier, stages, ranked, date_added, last_played) VALUES ('{MySqlHelper.EscapeString(Name)}', 'Unknown', {this.Stages}, {this.Bonuses}, 0, {(int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()}, {(int)DateTimeOffset.UtcNow.ToUnixTimeSeconds()})"
		public const string DB_QUERY_MAP_UPDATE_INFO_FULL =
			"UPDATE Maps SET last_played=@LastPlayed, stages=@Stages, bonuses=@Bonuses, author=@Author, tier=@Tier, ranked=@Ranked  WHERE id=@Id;";
		public const string DB_QUERY_MAP_GET_RECORD_RUNS_AND_COUNT =
			@"
            SELECT 
                ranked_times.*
            FROM (
                SELECT 
                    MapTimes.*,
                    Player.name AS Name,
                    ROW_NUMBER() OVER (
                        PARTITION BY MapTimes.type, MapTimes.stage 
                        ORDER BY MapTimes.run_time ASC
                    ) AS row_num,
                    COUNT(*) OVER (PARTITION BY MapTimes.type, MapTimes.stage) AS TotalCount
                FROM MapTimes
                JOIN Player ON MapTimes.player_id = Player.id
                WHERE MapTimes.map_id = @Id
            ) AS ranked_times
            WHERE ranked_times.row_num = 1;";

		// PlayerStats.cs related queries
		public const string DB_QUERY_PS_GET_ALL_RUNTIMES =
			@"
                SELECT mainquery.*, (SELECT COUNT(*) FROM `MapTimes` AS subquery 
                WHERE subquery.`map_id` = mainquery.`map_id` AND subquery.`style` = mainquery.`style` 
                AND subquery.`run_time` <= mainquery.`run_time` AND subquery.`type` = mainquery.`type` AND subquery.`stage` = mainquery.`stage`) AS `Rank` FROM `MapTimes` AS mainquery 
                WHERE mainquery.`player_id` = @PlayerId AND mainquery.`map_id` = @MapId; 
            ";

		// PersonalBest.cs related queries
		public const string DB_QUERY_PB_GET_TYPE_RUNTIME =
			@"
                SELECT mainquery.*, (SELECT COUNT(*) FROM `MapTimes` AS subquery 
                WHERE subquery.`map_id` = mainquery.`map_id` AND subquery.`style` = mainquery.`style` 
                AND subquery.`run_time` <= mainquery.`run_time` AND subquery.`type` = mainquery.`type` AND subquery.`stage` = mainquery.`stage`) AS `Rank` FROM `MapTimes` AS mainquery 
                WHERE mainquery.`player_id` = @PlayerId AND mainquery.`map_id` = @MapId AND mainquery.`type` = @Type AND mainquery.`style` = @Style";
		public const string DB_QUERY_PB_GET_SPECIFIC_MAPTIME_DATA =
			@"
                SELECT mainquery.*, (SELECT COUNT(*) FROM `MapTimes` AS subquery 
                WHERE subquery.`map_id` = mainquery.`map_id` AND subquery.`style` = mainquery.`style` 
                AND subquery.`run_time` <= mainquery.`run_time` AND subquery.`type` = mainquery.`type` AND subquery.`stage` = mainquery.`stage`) AS `Rank` FROM `MapTimes` AS mainquery 
                WHERE mainquery.`id` = @MapTimeId; 
            ";
		public const string DB_QUERY_PB_GET_CPS =
			"SELECT * FROM `Checkpoints` WHERE `maptime_id` = @MapTimeID;";

		// CurrentRun.cs related queries
		public const string DB_QUERY_CR_INSERT_TIME =
			@"
            INSERT INTO `MapTimes` 
                (`player_id`, `map_id`, `style`, `type`, `stage`, 
                 `run_time`, `start_vel_x`, `start_vel_y`, `start_vel_z`, 
                 `end_vel_x`, `end_vel_y`, `end_vel_z`, `run_date`, `replay_frames`) 
            VALUES 
                (@PlayerId, @MapId, @Style, @Type, @Stage, 
                 @RunTime, @StartVelX, @StartVelY, @StartVelZ, 
                 @EndVelX, @EndVelY, @EndVelZ, @RunDate, @ReplayFrames);
        ";
		public const string DB_QUERY_CR_UPDATE_TIME =
			@"
            UPDATE `MapTimes` SET 
                `run_time` = @RunTime,
                `start_vel_x` = @StartVelX,
                `start_vel_y` = @StartVelY,
                `start_vel_z` = @StartVelZ,
                `end_vel_x` = @EndVelX,
                `end_vel_y` = @EndVelY,
                `end_vel_z` = @EndVelZ,
                `run_date` = @RunDate,
                `replay_frames` = @ReplayFrames
            WHERE 
                `id` = @MapTimeId;
        ";
		public const string DB_QUERY_CR_INSERT_CP =
			@"
                INSERT INTO `Checkpoints` 
                (`maptime_id`, `cp`, `run_time`, `start_vel_x`, `start_vel_y`, `start_vel_z`, 
                `end_vel_x`, `end_vel_y`, `end_vel_z`, `attempts`, `end_touch`) 
                VALUES (@MapTimeId, @CP, @RunTime, @StartVelX, @StartVelY, @StartVelZ, @EndVelX, @EndVelY, @EndVelZ, @Attempts, @EndTouch) 
                ON DUPLICATE KEY UPDATE 
                run_time=VALUES(run_time), start_vel_x=VALUES(start_vel_x), start_vel_y=VALUES(start_vel_y), start_vel_z=VALUES(start_vel_z), 
                end_vel_x=VALUES(end_vel_x), end_vel_y=VALUES(end_vel_y), end_vel_z=VALUES(end_vel_z), attempts=VALUES(attempts), end_touch=VALUES(end_touch);
            ";

		// ReplayPlayer.cs related queries
		public const string DB_QUERY_RP_LOAD_REPLAY =
			@"
                SELECT MapTimes.replay_frames, MapTimes.run_time, Player.name
                FROM MapTimes
                JOIN Player ON MapTimes.player_id = Player.id
                WHERE MapTimes.id={0};
            ";

		// Players.cs related queries
		public const string DB_QUERY_PP_GET_PROFILE =
			"SELECT * FROM `Player` WHERE `steam_id` = @SteamID LIMIT 1;";
		public const string DB_QUERY_PP_DELETE_PROFILE = "DELETE FROM `Player` WHERE `id` = @Id;";
		public const string DB_QUERY_PP_INSERT_PROFILE =
			@"
                INSERT INTO `Player` (`name`, `steam_id`, `country`, `join_date`, `last_seen`, `connections`) 
                VALUES (@Name, @SteamID, @Country, @JoinDate, @LastSeen, @Connections);
            ";
		public const string DB_QUERY_PP_UPDATE_PROFILE =
			@"
                UPDATE `Player` SET country = @Country, 
                `last_seen` = @LastSeen, `connections` = `connections` + 1, `name` = @Name
                WHERE `id` = @Id LIMIT 1;
            ";
	}
}
