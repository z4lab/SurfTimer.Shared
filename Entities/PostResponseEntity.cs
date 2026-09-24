namespace SurfTimer.Shared.Entities
{
	public class PostResponseEntity
	{
		public int Id { get; set; }
		public int Inserted { get; set; }
		public int Affected { get; set; } = 0;
		public bool Trx { get; set; }
	}
}
