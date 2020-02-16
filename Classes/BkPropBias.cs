using System;
using System.IO;

namespace Bkprop
{
	// Backpropagation with momentum and bias
	class BkPropBias
	{
		const double momentum = 0.1;        // momentum - to discourage oscillation.
		const double learnRate = 0.75;      // learning rate - controls the maginitude of the increase in the change in weights.
		const double errorThresh = 0.025;   // error threshold
		const double biasValue = 1.0;       // bias value

		public int NumInput { get; private set; }
		public int NumHidden { get; private set; }
		public int NumOutput { get; private set; }

		public event ProgressEventHandler OnProgress;

		public double[] inputs;             // Input
		public double[] hOutputs;           // Hidden
		public double[] outputs;            // Output

		private double[,] ihWeights;        // input-to-hidden
		private double[,] hoWeights;        // hidden-to-output

		// for momentum with back-propagation
		private double[,] ihPrevWeightsDelta;
		private double[,] hoPrevWeightsDelta;

		private double[] errhid;            // hidden layer error
		private double[] errout;            // out error

		private readonly double[] errpat;   // pattern error
		private readonly bool[] patr;       // pattern is trained

		private readonly DataPattern[] _patterns;

		private Func<double, double> activation1;
		private Func<double, double> dxactivation1;

		private Func<double, double> activation2;
		private Func<double, double> dxactivation2;

		public BkPropBias(DataPattern[] patterns, int inputNumber, int hiddenNumber, int outputNumber)
		{
			_patterns = patterns;
			NumInput = inputNumber;
			NumHidden = hiddenNumber;
			NumOutput = outputNumber;

			inputs = new double[NumInput + 1];
			ihWeights = new double[NumInput + 1, NumHidden];

			hOutputs = new double[NumHidden + 1];
			hoWeights = new double[NumHidden + 1, NumOutput];

			outputs = new double[NumOutput];

			ihPrevWeightsDelta = new double[NumInput + 1, NumHidden];
			hoPrevWeightsDelta = new double[NumHidden + 1, NumOutput];

			errhid = new double[NumHidden + 1];
			errout = new double[NumOutput];

			errpat = new double[_patterns.Length];
			patr = new bool[_patterns.Length];

			activation1 = Utils.Sigmoid;
			dxactivation1 = Utils.dxSigmoid;

			activation2 = Utils.HyperTan;
			dxactivation2 = Utils.dxHyperTan;

			Init();
		}

		void Init()
		{
			inputs[NumInput] = biasValue;
			hOutputs[NumHidden] = biasValue;

			for (int i = 0; i < NumInput + 1; i++)
				for (int h = 0; h < NumHidden; h++)
				{
					ihWeights[i, h] = Utils.randomWeight();
					ihPrevWeightsDelta[i, h] = 0;
				}

			for (int h = 0; h < NumHidden + 1; h++)
				for (int o = 0; o < NumOutput; o++)
				{
					hoWeights[h, o] = Utils.randomWeight();
					hoPrevWeightsDelta[h, o] = 0;
				}

			for (int p = 0; p < _patterns.Length; p++)
				patr[p] = false;
		}

		// back-propagation
		void UpdateWeights(int p)
		{
			// error out
			for (int o = 0; o < NumOutput; o++)
				errout[o] = dxactivation1(outputs[o]) * (_patterns[p].DesiredOut[o] - outputs[o]);

			// error hidden
			for (int h = 0; h < NumHidden; h++)
			{
				double sum = 0.0;
				for (int o = 0; o < NumOutput; o++)
					sum += hoWeights[h, o] * errout[o];

				errhid[h] = sum * dxactivation2(hOutputs[h]);
			}

			// update input to hidden weights 
			for (int i = 0; i < NumInput + 1; i++)
				for (int h = 0; h < NumHidden; h++)
				{
					double delta = learnRate * inputs[i] * errhid[h];
					ihWeights[i, h] += delta + momentum * ihPrevWeightsDelta[i, h];
					ihPrevWeightsDelta[i, h] = delta;
				}

			// update hidden to output weights
			for (int h = 0; h < NumHidden + 1; h++)
				for (int o = 0; o < NumOutput; o++)
				{
					double delta = learnRate * hOutputs[h] * errout[o];
					hoWeights[h, o] += delta + momentum * hoPrevWeightsDelta[h, o];
					hoPrevWeightsDelta[h, o] = delta;
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
				double tothid = 0.0;

				for (int i = 0; i < NumInput + 1; i++)
					tothid += inputs[i] * ihWeights[i, h];

				hOutputs[h] = activation2(tothid);
			}

			// compute output layer weighted sums
			for (int o = 0; o < NumOutput; o++)
			{
				double totout = 0.0;

				for (int h = 0; h < NumHidden + 1; h++)
					totout += hOutputs[h] * hoWeights[h, o];

				outputs[o] = activation1(totout);
			}
		}

		//
		public double training(int patt, int maxEpochs = 1)
		{
			int pattLength = _patterns.Length;

			if (patt < 0)
				for (int p = 0; p < pattLength; p++)
					errpat[p] = 10.0 * errorThresh;

			int currPatt = patt;
			int currEpoch = 0;

			double maxerror;
			bool done;

			do
			{
				if (patt < 0)        //entire set?
				{
					bool v = false;

					for (int p = 0; p < pattLength; p++)
						if (!patr[p])
						{
							v = true;
							break;
						}

					// select a random training pattern
					do
						currPatt = Utils.RandInt(pattLength);
					while (patr[currPatt] && v);
				}

				for (int rep = 0; rep < 3; rep++)
				{
					currEpoch++;
					netanswer(_patterns[currPatt].Pattern);
					UpdateWeights(currPatt);
				}

				//===============================================

				maxerror = Utils.Error(ref _patterns[currPatt].DesiredOut, ref outputs);
				errpat[currPatt] = maxerror;

				done = maxerror < errorThresh;

				if (patt < 0)
					for (int p = 0; p < pattLength; p++)
					{
						if (errpat[p] > maxerror)
							maxerror = errpat[p];

						patr[p] = errpat[p] < errorThresh;
						done &= patr[p];
					}

				if (OnProgress != null && (currEpoch & 31) == 31)
					OnProgress(this, currEpoch, maxerror);

			} while (currEpoch < maxEpochs && !done);

			if (OnProgress != null && (currEpoch & 31) != 31)
				OnProgress(this, currEpoch, maxerror);

			if (patt >= 0)
				return errpat[patt];

			return maxerror;
		}

		void FilErrors()
		{
			for (int p = 0; p < _patterns.Length; p++)
			{
				netanswer(_patterns[p].Pattern);
				errpat[p] = Utils.Error(ref _patterns[p].DesiredOut, ref outputs);
			}
		}

		public void LoadFromFile(string FileName)
		{
			TextReader tr = new StreamReader(FileName);
			string inputLine = tr.ReadLine().Trim();

			if (!inputLine.Contains("neural network file. v2.0."))
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

			inputs = new double[NumInput + 1];
			inputs[NumInput] = biasValue;

			hOutputs = new double[NumHidden + 1];
			hOutputs[NumHidden] = biasValue;

			outputs = new double[NumOutput];

			ihWeights = new double[NumInput + 1, NumHidden];
			hoWeights = new double[NumHidden + 1, NumOutput];

			ihPrevWeightsDelta = new double[NumInput + 1, NumHidden];
			hoPrevWeightsDelta = new double[NumHidden + 1, NumOutput];

			errhid = new double[NumHidden];
			errout = new double[NumOutput];

			i = 0;
			while (tr.Peek() >= 0 && i < NumInput + 1)
			{
				inputLine = tr.ReadLine();
				strings = inputLine.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
				if (strings.Length == 0)
					continue;

				for (h = 0; h < NumHidden; h++)
					ihWeights[i, h] = double.Parse(strings[h]);

				i++;
			}

			if (i != NumInput + 1)
				throw new Exception("Invalid file format.");

			h = 0;
			while (tr.Peek() >= 0 && h < NumHidden + 1)
			{
				inputLine = tr.ReadLine();
				strings = inputLine.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
				if (strings.Length == 0)
					continue;

				for (int o = 0; o < NumOutput; o++)
					hoWeights[h, o] = double.Parse(strings[o]);

				h++;
			}
			if (h != NumHidden + 1)
				throw new Exception("Invalid file format.");

			tr.Close();
		}

		public void SaveToFile(string FileName)
		{
			TextWriter tw = new StreamWriter(FileName);
			tw.WriteLine("BkProp demo neural network file. v2.0.");         //Backpropagation
			tw.WriteLine("#Do not modify the contents of this file manually!");
			tw.WriteLine();

			tw.WriteLine(String.Format("InputCount = {0}", NumInput));
			tw.WriteLine(String.Format("HiddenCount = {0}", NumHidden));
			tw.WriteLine(String.Format("OutputCount = {0}", NumOutput));

			for (int i = 0; i < NumInput + 1; i++)
			{
				for (int h = 0; h < NumHidden; h++)
					tw.Write(ihWeights[i, h] + " ");

				tw.WriteLine();
			}
			tw.WriteLine();

			for (int h = 0; h < NumHidden + 1; h++)
			{
				for (int o = 0; o < NumOutput; o++)
					tw.Write(hoWeights[h, o] + " ");

				tw.WriteLine();
			}

			tw.Close();
		}

	}
}
