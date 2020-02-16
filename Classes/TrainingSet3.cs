namespace Bkprop
{
	class TrainingSet3 : ITrainingSet
	{
		public int PatternWidth { get { return 4; } }
		public int PatternHeight { get { return 5; } }
		public int OutputNumber { get { return 6; } }

		public string[] OutputNames { get; } = { "A", "B", "C", "D", "E", "F" };

		public DataPattern[] Patterns { get; } = {
				new DataPattern(new int[]			// 'A'
						{
					0, 1, 1, 0,
					1, 0, 0, 1,
					1, 1, 1, 1,
					1, 0, 0, 1,
					1, 0, 0, 1
						},
				new int[] { 1, 0, 0, 0, 0, 0 }),

				new DataPattern(new int[]			// 'B'
						{
					1, 1, 1, 0,
					1, 0, 0, 1,
					1, 1, 1, 0,
					1, 0, 0, 1,
					1, 1, 1, 0
						},
				new int[] { 0, 1, 0, 0, 0, 0 }),

				new DataPattern(new int[]			// 'C'
						{
					0, 1, 1, 1,
					1, 0, 0, 0,
					1, 0, 0, 0,
					1, 0, 0, 0,
					0, 1, 1, 1
						},
				new int[] { 0, 0, 1, 0, 0, 0 }),

				new DataPattern(new int[]			// 'D'
						{
					1, 1, 1, 0,
					1, 0, 0, 1,
					1, 0, 0, 1,
					1, 0, 0, 1,
					1, 1, 1, 0
						},
				new int[] { 0, 0, 0, 1, 0, 0 }),

				new DataPattern(new int[]			// 'E'
						{
					1, 1, 1, 1,
					1, 0, 0, 0,
					1, 1, 1, 0,
					1, 0, 0, 0,
					1, 1, 1, 1
						},
				new int[] { 0, 0, 0, 0, 1, 0 }),

				new DataPattern(new int[]			// 'F'
						{
					1, 1, 1, 1,
					1, 0, 0, 0,
					1, 1, 1, 0,
					1, 0, 0, 0,
					1, 0, 0, 0
						},
				new int[] { 0, 0, 0, 0, 0, 1 })
		};
	}
}
