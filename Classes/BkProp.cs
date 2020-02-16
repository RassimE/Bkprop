using System;
using System.IO;

namespace Bkprop
{
	// Backpropagation with momentum
	class BkProp
	{
		const double alfa = 0.1;            // momentum - to discourage oscillation.
		const double delta = 0.75;          // learning rate - controls the maginitude of the increase in the change in weights.
		const double Epsilon = 0.025;       // error threshold

		public int NumInput { get; private set; }
		public int NumHidden { get; private set; }
		public int NumOutput { get; private set; }

		public event ProgressEventHandler OnProgress;

		public double[] inputs;		// Input
		public double[] hOutputs;	// Hidden
		public double[] outputs;	// Output

		private double[,] inhiddw;
		private double[,] hidoutw;

		private double[,] deltaihw;
		private double[,] deltahow;

		private double[] ehid;
		private double[] eout;

		private readonly double[] ecm;	// pattern error
		private readonly bool[] patr;	// pattern is trained

		private readonly DataPattern[] _patterns;
		private readonly Random _randObj;

		private Func<double, double> activation1;
		private Func<double, double> dxactivation1;

		private Func<double, double> activation2;
		private Func<double, double> dxactivation2;

		public BkProp(DataPattern[] patterns, int inputNumber, int hiddenNumber, int outputNumber)
		{
			_randObj = new Random((int)DateTime.Now.Ticks);

			_patterns = patterns;
			NumInput = inputNumber;
			NumHidden = hiddenNumber;
			NumOutput = outputNumber;

			inputs = new double[NumInput];
			hOutputs = new double[NumHidden];
			outputs = new double[NumOutput];

			inhiddw = new double[NumInput, NumHidden];
			hidoutw = new double[NumHidden, NumOutput];

			deltaihw = new double[NumInput, NumHidden];
			deltahow = new double[NumHidden, NumOutput];

			ehid = new double[NumHidden];
			eout = new double[NumOutput];

			ecm = new double[_patterns.Length];
			patr = new bool[_patterns.Length];

			activation1 = Utils.Sigmoid;
			dxactivation1 = Utils.dxSigmoid;

			activation2 = Utils.HyperTan;
			dxactivation2 = Utils.dxHyperTan;

			init();
		}

		void init()
		{
			for (int i = 0; i < NumInput; i++)
				for (int h = 0; h < NumHidden; h++)
				{
					inhiddw[i, h] = _randObj.NextDouble() - 0.5;
					deltaihw[i, h] = 0;
				}

			for (int h = 0; h < NumHidden; h++)
				for (int o = 0; o < NumOutput; o++)
				{
					hidoutw[h, o] = _randObj.NextDouble() - 0.5;
					deltahow[h, o] = 0;
				}

			for (int p = 0; p < _patterns.Length; p++)
				patr[p] = false;
		}

		void backprop(int p)
		{
			// error out
			for (int o = 0; o < NumOutput; o++)
				eout[o] = dxactivation1(outputs[o]) * (outputs[o] - _patterns[p].DesiredOut[o]);

			// error hidden
			for (int h = 0; h < NumHidden; h++)
			{
				double sum = 0.0;

				for (int o = 0; o < NumOutput; o++)
					sum += hidoutw[h, o] * eout[o];
				ehid[h] = sum * dxactivation2(hOutputs[h]);
			}

			// update input to hidden weights 
			for (int i = 0; i < NumInput; i++)
				for (int h = 0; h < NumHidden; h++)
				{
					double temp = -delta * inputs[i] * ehid[h];
					inhiddw[i, h] += temp + alfa * deltaihw[i, h];
					deltaihw[i, h] = temp;
				}

			// update hidden to output weights
			for (int h = 0; h < NumHidden; h++)
				for (int o = 0; o < NumOutput; o++)
				{
					double temp = -delta * hOutputs[h] * eout[o];
					hidoutw[h, o] += temp + alfa * deltahow[h, o];
					deltahow[h, o] = temp;
				}
		}

		// recalling
		public void netanswer(int[] afer)
		{
			// copy inputs data to input neurons
			for (int i = 0; i < NumInput; i++)
				inputs[i] = afer[i];

			// compute hidden layer weighted sums
			for (int h = 0; h < NumHidden; h++)
			{
				double totin = 0.0;
				for (int i = 0; i < NumInput; i++)
					totin += inputs[i] * inhiddw[i, h];

				hOutputs[h] = activation2(totin);
			}

			// compute output layer weighted sums
			for (int o = 0; o < NumOutput; o++)
			{
				double totin = 0.0;
				for (int h = 0; h < NumHidden; h++)
					totin += hOutputs[h] * hidoutw[h, o];

				outputs[o] = activation1(totin);
			}
		}

		public double training(int pat, int num = 1)
		{
			int p = pat;
			int j = 0;

			double maxerror;
			bool l;
			if (pat < 0)
				for (int i = 0; i < _patterns.Length; i++)
					ecm[i] = 10.0 * Epsilon;

			do
			{
				if (pat < 0)
				{
					bool v = false;

					for (p = 0; p < _patterns.Length; p++)
						if (!patr[p])
						{
							v = true;
							break;
						}

					// select a random training pattern:  0 <= p < _patterns.Length
					do
						p = _randObj.Next(_patterns.Length);
					while (patr[p] && v);
				}

				for (int rep = 0; rep < 3; rep++)
				{
					j++;
					netanswer(_patterns[p].Pattern);
					backprop(p);
				}

				maxerror = Utils.Error(ref _patterns[p].DesiredOut, ref outputs);
				ecm[p] = maxerror;

				l = maxerror < Epsilon;

				if (pat < 0)
					for (int t = 0; t < _patterns.Length; t++)
					{
						if (ecm[t] > maxerror)
							maxerror = ecm[t];

						patr[t] = ecm[t] < Epsilon;
						l &= (patr[t]);
					}

				if (OnProgress != null && (j & 15) == 15)
					OnProgress(this, j, maxerror);
			} while (j < num && !l);

			if (OnProgress != null && (j & 15) != 15)
				OnProgress(this, j, maxerror);

			if (pat >= 0)
				return ecm[pat];

			return maxerror;
		}

		void FilErrors()
		{
			for (int p = 0; p < _patterns.Length; p++)
			{
				netanswer(_patterns[p].Pattern);
				ecm[p] = Utils.Error(ref _patterns[p].DesiredOut, ref outputs);
			}
		}

		public void LoadFromFile(string FileName)
		{
			TextReader tr = new StreamReader(FileName);

			string inputLine = tr.ReadLine().Trim();
			if (!inputLine.Contains("neural network file. v1.0."))
				throw new Exception("Invalid file format.");

			string[] strings;
			int i = 0, h;

			while (tr.Peek() >= 0 && (i & 7) != 7)
			{
				do
				{
					inputLine = tr.ReadLine().Trim();
					strings = inputLine.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
				}
				while (tr.Peek() >= 0 && strings.Length < 2);

				if (strings.Length < 2)
					break;

				string name = strings[0].Trim();
				string value = strings[1];

				if (name.ToLower() == "inputcount")
				{
					NumInput = int.Parse(value);
					i |= 1;
				}
				else if (name.ToLower() == "hiddencount")
				{
					NumHidden = int.Parse(value);
					i |= 2;
				}
				else if (name.ToLower() == "outputcount")
				{
					NumOutput = int.Parse(value);
					i |= 4;
				}
			}

			if (i != 7)
				throw new Exception("Invalid file format.");

			inputs = new double[NumInput];
			hOutputs = new double[NumHidden];
			outputs = new double[NumOutput];

			inhiddw = new double[NumInput, NumHidden];
			hidoutw = new double[NumHidden, NumOutput];

			deltaihw = new double[NumInput, NumHidden];
			deltahow = new double[NumHidden, NumOutput];

			ehid = new double[NumHidden];
			eout = new double[NumOutput];

			i = 0;
			while (tr.Peek() >= 0 && i < NumInput)
			{
				inputLine = tr.ReadLine();
				strings = inputLine.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
				if (strings.Length == 0)
					continue;

				for (h = 0; h < NumHidden; h++)
					inhiddw[i, h] = double.Parse(strings[h]);

				i++;
			}
			if (i != NumInput)
				throw new Exception("Invalid file format.");

			h = 0;
			while (tr.Peek() >= 0 && h < NumHidden)
			{
				inputLine = tr.ReadLine();
				strings = inputLine.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
				if (strings.Length == 0)
					continue;

				for (int o = 0; o < NumOutput; o++)
					hidoutw[h, o] = double.Parse(strings[o]);

				h++;
			}
			if (h != NumHidden)
				throw new Exception("Invalid file format.");

			tr.Close();
		}

		public void SaveToFile(string FileName)
		{
			TextWriter tw = new StreamWriter(FileName);
			tw.WriteLine("BkProp demo neural network file. v1.0.");         //Backpropagation
			tw.WriteLine("#Do not modify the contents of this file manually!");
			tw.WriteLine();

			tw.WriteLine(String.Format("InputCount = {0}", NumInput));
			tw.WriteLine(String.Format("HiddenCount = {0}", NumHidden));
			tw.WriteLine(String.Format("OutputCount = {0}", NumOutput));

			for (int i = 0; i < NumInput; i++)
			{
				for (int h = 0; h < NumHidden; h++)
					tw.Write(inhiddw[i, h] + " ");

				tw.WriteLine();
			}
			tw.WriteLine();

			for (int h = 0; h < NumHidden; h++)
			{
				for (int o = 0; o < NumOutput; o++)
					tw.Write(hidoutw[h, o] + " ");

				tw.WriteLine();
			}

			tw.Close();
		}
	}
}
