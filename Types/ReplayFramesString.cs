namespace SurfTimer.Shared.Types
{
	public class ReplayFramesString
	{
		public string Value { get; }

		public ReplayFramesString(string value)
		{
			Value = value;
		}

		public override string ToString() => Value;
	}
}
