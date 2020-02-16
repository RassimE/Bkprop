
namespace Bkprop
{
	struct DataPattern
	{
		public int[] Pattern;
		public int[] DesiredOut;    //double[] DesiredOut;

		public DataPattern(int[] Patt, int[] Out)
		{
			Pattern = Patt;
			DesiredOut = Out;
		}
	}
}
