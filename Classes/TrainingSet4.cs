namespace Bkprop
{
	class TrainingSet4 : ITrainingSet
	{
		public int PatternWidth { get { return 3; } }
		public int PatternHeight { get { return 3; } }
		public int OutputNumber { get { return 2; } }

		public string[] OutputNames { get; } = { "Horisontal", "Vertical" };

		public DataPattern[] Patterns { get; } = {
				new DataPattern(new int[]
						{   1,1,1,
							0,0,0,
							0,0,0 },
				new int[] { 1, 0 }),

				new DataPattern(new int[]
						{   0,0,0,
							1,1,1,
							0,0,0 },
				new int[] { 1, 0 }),

				new DataPattern(new int[]
						{   0,0,0,
							0,0,0,
							1,1,1},
				new int[] { 1, 0 }),

				new DataPattern(new int[]
						{   1,0,0,
							1,0,0,
							1,0,0 },
				new int[] { 0, 1 }),

				new DataPattern(new int[]
						{   0,1,0,
							0,1,0,
							0,1,0},
				new int[] { 0, 1 }),

				new DataPattern(new int[]
						{   0,0,1,
							0,0,1,
							0,0,1},
				new int[] { 0, 1 }),

				new DataPattern(new int[]
						{   1,1,1,
							1,0,0,
							1,0,0},
				new int[] { 1, 1 }),

				new DataPattern(new int[]
						{   1,1,1,
							0,1,0,
							0,1,0},
				new int[] { 1, 1 }),

				new DataPattern(new int[]
						{   1,1,1,
							0,0,1,
							0,0,1},
				new int[] { 1, 1 }),

				new DataPattern(new int[]
						{   1,0,0,
							1,1,1,
							1,0,0},
				new int[] { 1, 1 }),

				new DataPattern(new int[]
						{   0,1,0,
							1,1,1,
							0,1,0},
				new int[] { 1, 1 }),

				new DataPattern(new int[]
						{   0,0,1,
							1,1,1,
							0,0,1},
				new int[] { 1, 1 }),

				new DataPattern(new int[]
						{   1,0,0,
							1,0,0,
							1,1,1},
				new int[] { 1, 1 }),

				new DataPattern(new int[]
						{   0,1,0,
							0,1,0,
							1,1,1},
				new int[] { 1, 1 }),

				new DataPattern(new int[]
						{   0,0,1,
							0,0,1,
							1,1,1},
				new int[] { 1, 1 }),

				new DataPattern(new int[]
						{   0,0,0,
							0,0,0,
							0,0,0},
				new int[] { 0, 0 })
		};
	}
}
