using System;

namespace Bkprop
{
	delegate void ProgressEventHandler(object sender, long iteration, double error);

	static class Utils
	{
		static Random _randObj = new Random(DateTime.Now.Millisecond);

		internal static double randomWeight()
		{
			return _randObj.NextDouble() - 0.5;
		}

		public static int RandInt(int x)
		{
			return _randObj.Next(x);
		}

		internal static double Sigmoid(double x)
		{
			return 1.0 / (1.0 + Math.Exp(-x));
		}

		internal static double dxSigmoid(double y)
		{
			return y * (1.0 - y);
		}

		internal static double HyperTan(double x)
		{
			return Math.Tanh(x);
		}

		internal static double dxHyperTan(double y)
		{
			return 1.0 - y * y;
		}

		internal static double Linear(double x)
		{
			if (x > 0.5) return 1;
			if (x < -0.5) return 0;
			return x + 0.5f;
		}

		internal static double dxLinear(double y)
		{
			if (y > 0.5) return 0;
			if (y < -0.5) return 0;
			return 1;
		}

		internal static double Heaviside(double x)
		{
			if (x > 0) return 1;
			return 0;
		}

		internal static double dxHeaviside(double y)
		{
			if (Math.Abs(y) < 0.0001) return double.MaxValue;
			return 0;
		}

		// Error measure
		internal static double Error(ref int[] tValues, ref double[] outputs)
		{
			double e = 0.0;

			for (int i = 0; i < outputs.Length; i++)
				e += (tValues[i] - outputs[i]) * (tValues[i] - outputs[i]);

			return Math.Sqrt(e);
		}

	}
}
