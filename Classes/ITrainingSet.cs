namespace Bkprop
{
	interface ITrainingSet
	{
		int PatternWidth{get;}
		int PatternHeight { get; }
		int OutputNumber { get; }

		string[] OutputNames { get; }
		DataPattern[] Patterns { get; }
	}
}
