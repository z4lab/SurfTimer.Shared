using SurfTimer.Shared.Types;

namespace SurfTimer.Shared.Entities
{
	public abstract class RunStatsEntity
	{
		public int ID { get; set; } = -1;
		public short Type { get; set; } // 0 = Map, 1 = Bonus, 2 = Stage, 3 = Checkpoint segment
		public int RunTime { get; set; }
		public short Stage { get; set; } // Signifies Bonus number when Type == 1, or Checkpoint number when Type == 3
		public short Style { get; set; }
		public string? Name { get; set; }
		public float StartVelX { get; set; }
		public float StartVelY { get; set; }
		public float StartVelZ { get; set; }
		public float EndVelX { get; set; }
		public float EndVelY { get; set; }
		public float EndVelZ { get; set; }
		public int RunDate { get; set; }
		public ReplayFramesString? ReplayFrames { get; set; }
	}
}
