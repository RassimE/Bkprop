//#define BkProp
#define BkPropBias

using System;
using System.Windows.Forms;

namespace Bkprop
{
	public partial class MainForm : Form
	{
#if BkProp
		private BkProp _bkProp;
#else
		private BkPropBias _bkProp;
#endif

		private TrainingSet1 _traningSet1;
		private TrainingSet2 _traningSet2;
		private TrainingSet3 _traningSet3;
		private TrainingSet4 _traningSet4;
		private TrainingSet5 _traningSet5;

		private ITrainingSet _traningSet;
		private ITrainingSet[] _traningSets;

		public MainForm()
		{
			InitializeComponent();

			_traningSet1 = new TrainingSet1();
			_traningSet2 = new TrainingSet2();
			_traningSet3 = new TrainingSet3();
			_traningSet4 = new TrainingSet4();
			_traningSet5 = new TrainingSet5();

			_traningSets = new ITrainingSet[] { _traningSet1, _traningSet2, _traningSet3, _traningSet4, _traningSet5 };

			updnSet.Maximum = _traningSets.Length;
			setTraningSet((int)updnSet.Value);
		}

		void setTraningSet(int n)
		{
			// display state =========================================
			_traningSet = _traningSets[n - 1];

			patternEdit1.PatternWidth = _traningSet.PatternWidth;
			patternEdit1.PatternHeight = _traningSet.PatternHeight;

			int maxPat = _traningSet.Patterns.Length - 1;

			if (updnPattern.Value > maxPat)
				updnPattern.Value = maxPat;
			else
				updnPattern_ValueChanged(updnPattern, null);

			updnPattern.Maximum = maxPat;

			// PatternEditor ========================================
			double kw = (double)patternEdit1.Width / _traningSet.PatternWidth;
			//double kh = (double)inOutGrid.Height / _traningSet.PatternHeight;
			//double k = Math.Min(kw, kh);
			double k = kw;

			//int blockWidth = (int)Math.Round(k);       //kw
			//patternEdit1.Width = _traningSet.PatternWidth * blockWidth;

			int blockHeight = (int)Math.Round(k);      //kh
			patternEdit1.Height = _traningSet.PatternHeight * blockHeight;

			// bkProp ==========================================
			setHiddenLayers();
		}

		void setHiddenLayers()
		{
			int n = (int)updnHiddenLayers.Value;

			// bkProp ==========================================
			int inputNumber = _traningSet.PatternWidth * _traningSet.PatternHeight;

#if BkProp
			_bkProp = new BkProp(_traningSet.Patterns, inputNumber, n, _traningSet.OutputNumber);
#else
			_bkProp = new BkPropBias(_traningSet.Patterns, inputNumber, n, _traningSet.OutputNumber);
#endif

			_bkProp.OnProgress += OnProgress;

			outputLayer.Bars = _bkProp.NumOutput;
			hiddenLayer.Bars = _bkProp.NumHidden;

			FillBars();
		}

		void FillBars()
		{
			outputLayer.BeginUpdate();
			hiddenLayer.BeginUpdate();

			for (int i = 0; i < _bkProp.NumOutput; i++)
				outputLayer[i] = _bkProp.outputs[i];

			for (int i = 0; i < _bkProp.NumHidden; i++)
				hiddenLayer[i] = _bkProp.hOutputs[i];

			outputLayer.EndUpdate();
			hiddenLayer.EndUpdate();
		}

		void OnProgress(object sender, long iteration, double error)
		{
			lblProgress.Text = "Iterations: " + iteration;

			errorGauge1.Value = 100.0 * error;
			FillBars();
			Application.DoEvents();
		}

		private void updnSet_ValueChanged(object sender, EventArgs e)
		{
			setTraningSet((int)updnSet.Value);
		}

		private void updnHiddenLayers_ValueChanged(object sender, EventArgs e)
		{
			setHiddenLayers();
		}

		private void chkMode_CheckedChanged(object sender, System.EventArgs e)
		{
			//errorGauge1.Enabled = !chkMode.Checked;
			//hiddenLayer.Enabled = chkMode.Checked;

			//errorGauge1.Visible = !chkMode.Checked;
			//hiddenLayer.Visible = chkMode.Checked;

			lblInput.Visible = chkMode.Checked;

			btnTest.Enabled = chkMode.Checked;
			lblGuest.Enabled = chkMode.Checked;

			btnStep.Enabled = !chkMode.Checked;
			btn100Step.Enabled = !chkMode.Checked;
			btnTrain.Enabled = !chkMode.Checked;

			updnSet.Enabled = !chkMode.Checked;
			updnPattern.Enabled = !chkMode.Checked;
			updnHiddenLayers.Enabled = !chkMode.Checked;

			lblProgress.Enabled = !chkMode.Checked;
			lblPattern.Enabled = !chkMode.Checked;
			lblDesiredOutput.Enabled = !chkMode.Checked;

			patternEdit1.InEditMode = chkMode.Checked;
		}

		private void btnQuit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void btnStep_Click(object sender, System.EventArgs e)
		{
			chkMode.Enabled = false;

			int i = (int)updnPattern.Value;
			double error = _bkProp.training(i, 1);
			errorGauge1.Value = 100.0 * error;

			chkMode.Enabled = true;
		}

		private void btn100Step_Click(object sender, System.EventArgs e)
		{
			chkMode.Enabled = false;

			int i = (int)updnPattern.Value;
			double error = _bkProp.training(i, 100);
			errorGauge1.Value = 100.0 * error;

			chkMode.Enabled = true;
		}

		private void btnTrain_Click(object sender, System.EventArgs e)
		{
			lblProgress.Text = "Progress: 0";

			chkMode.Enabled = false;
			double error = _bkProp.training(-1, 100000);
			chkMode.Enabled = true;

			errorGauge1.Value = 100.0 * error;
		}

		private void updnPattern_ValueChanged(object sender, System.EventArgs e)
		{
			int p = (int)updnPattern.Value;
			patternEdit1.Pattern = _traningSet.Patterns[p].Pattern;

			lblDesiredOutput.Text = "Desired Output:";

			if (p >= 0 && !chkMode.Checked)
				for (int i = 0; i < _traningSet.Patterns[p].DesiredOut.Length; i++)
					if (_traningSet.Patterns[p].DesiredOut[i] != 0)
						lblDesiredOutput.Text += " " + _traningSet.OutputNames[i];
		}

		private void btnTest_Click(object sender, System.EventArgs e)
		{
			_bkProp.netanswer(patternEdit1.Pattern);

			int i, maxi = -1;
			double max = 0.0;

			for (i = 0; i < _bkProp.NumOutput; i++)
			{
				outputLayer[i] = _bkProp.outputs[i];
				if (_bkProp.outputs[i] > max)
				{
					max = _bkProp.outputs[i];
					maxi = i;
				}
			}

			if (maxi >= 0)
				lblGuest.Text = "Guest: " + _traningSet.OutputNames[maxi];

			for (i = 0; i < _bkProp.NumHidden; i++)
				hiddenLayer[i] = _bkProp.hOutputs[i];
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				_bkProp.SaveToFile(saveFileDialog1.FileName);
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
				_bkProp.LoadFromFile(openFileDialog1.FileName);
		}
	}
}
