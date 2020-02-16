namespace Bkprop
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}


		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.btnQuit = new System.Windows.Forms.Button();
			this.btnStep = new System.Windows.Forms.Button();
			this.btn100Step = new System.Windows.Forms.Button();
			this.btnTest = new System.Windows.Forms.Button();
			this.lblInput = new System.Windows.Forms.Label();
			this.lblOutput = new System.Windows.Forms.Label();
			this.lblHidden = new System.Windows.Forms.Label();
			this.updnPattern = new System.Windows.Forms.NumericUpDown();
			this.btnTrain = new System.Windows.Forms.Button();
			this.chkMode = new System.Windows.Forms.CheckBox();
			this.lblPattern = new System.Windows.Forms.Label();
			this.lblProgress = new System.Windows.Forms.Label();
			this.lblGuest = new System.Windows.Forms.Label();
			this.lblDesiredOutput = new System.Windows.Forms.Label();
			this.lblPatternSet = new System.Windows.Forms.Label();
			this.updnHiddenLayers = new System.Windows.Forms.NumericUpDown();
			this.updnSet = new System.Windows.Forms.NumericUpDown();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnLoad = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.patternEdit1 = new Bkprop.UserControls.PatternEdit();
			this.hiddenLayer = new Bkprop.NLayer();
			this.outputLayer = new Bkprop.NLayer();
			this.errorGauge1 = new Bkprop.ErrorGauge();
			((System.ComponentModel.ISupportInitialize)(this.updnPattern)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnHiddenLayers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updnSet)).BeginInit();
			this.SuspendLayout();
			// 
			// btnQuit
			// 
			this.btnQuit.Location = new System.Drawing.Point(0, 1);
			this.btnQuit.Name = "btnQuit";
			this.btnQuit.Size = new System.Drawing.Size(75, 23);
			this.btnQuit.TabIndex = 0;
			this.btnQuit.Text = "&Quit";
			this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
			// 
			// btnStep
			// 
			this.btnStep.Location = new System.Drawing.Point(80, 1);
			this.btnStep.Name = "btnStep";
			this.btnStep.Size = new System.Drawing.Size(75, 23);
			this.btnStep.TabIndex = 1;
			this.btnStep.Text = "&Step";
			this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
			// 
			// btn100Step
			// 
			this.btn100Step.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn100Step.Location = new System.Drawing.Point(160, 1);
			this.btn100Step.Name = "btn100Step";
			this.btn100Step.Size = new System.Drawing.Size(75, 23);
			this.btn100Step.TabIndex = 2;
			this.btn100Step.Text = "100 step";
			this.btn100Step.Click += new System.EventHandler(this.btn100Step_Click);
			// 
			// btnTest
			// 
			this.btnTest.Enabled = false;
			this.btnTest.Location = new System.Drawing.Point(92, 496);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(75, 23);
			this.btnTest.TabIndex = 4;
			this.btnTest.Text = "&Test";
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// lblInput
			// 
			this.lblInput.AutoSize = true;
			this.lblInput.Location = new System.Drawing.Point(237, 299);
			this.lblInput.Name = "lblInput";
			this.lblInput.Size = new System.Drawing.Size(31, 13);
			this.lblInput.TabIndex = 7;
			this.lblInput.Text = "Input";
			this.lblInput.Visible = false;
			// 
			// lblOutput
			// 
			this.lblOutput.AutoSize = true;
			this.lblOutput.Location = new System.Drawing.Point(301, 501);
			this.lblOutput.Name = "lblOutput";
			this.lblOutput.Size = new System.Drawing.Size(52, 13);
			this.lblOutput.TabIndex = 9;
			this.lblOutput.Text = "OUTPUT";
			// 
			// lblHidden
			// 
			this.lblHidden.AutoSize = true;
			this.lblHidden.Location = new System.Drawing.Point(440, 279);
			this.lblHidden.Name = "lblHidden";
			this.lblHidden.Size = new System.Drawing.Size(87, 13);
			this.lblHidden.TabIndex = 10;
			this.lblHidden.Text = "HIDDEN LAYER";
			// 
			// updnPattern
			// 
			this.updnPattern.Location = new System.Drawing.Point(304, 38);
			this.updnPattern.Maximum = new decimal(new int[] {
            17,
            0,
            0,
            0});
			this.updnPattern.Name = "updnPattern";
			this.updnPattern.Size = new System.Drawing.Size(56, 20);
			this.updnPattern.TabIndex = 13;
			this.updnPattern.ValueChanged += new System.EventHandler(this.updnPattern_ValueChanged);
			// 
			// btnTrain
			// 
			this.btnTrain.Location = new System.Drawing.Point(240, 1);
			this.btnTrain.Name = "btnTrain";
			this.btnTrain.Size = new System.Drawing.Size(75, 23);
			this.btnTrain.TabIndex = 14;
			this.btnTrain.Text = "Train";
			this.btnTrain.Click += new System.EventHandler(this.btnTrain_Click);
			// 
			// chkMode
			// 
			this.chkMode.Location = new System.Drawing.Point(3, 499);
			this.chkMode.Name = "chkMode";
			this.chkMode.Size = new System.Drawing.Size(83, 16);
			this.chkMode.TabIndex = 15;
			this.chkMode.Text = "Test Mode";
			this.chkMode.CheckedChanged += new System.EventHandler(this.chkMode_CheckedChanged);
			// 
			// lblPattern
			// 
			this.lblPattern.AutoSize = true;
			this.lblPattern.Location = new System.Drawing.Point(237, 42);
			this.lblPattern.Name = "lblPattern";
			this.lblPattern.Size = new System.Drawing.Size(44, 13);
			this.lblPattern.TabIndex = 16;
			this.lblPattern.Text = "Pattern:";
			// 
			// lblProgress
			// 
			this.lblProgress.AutoSize = true;
			this.lblProgress.Location = new System.Drawing.Point(373, 6);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Size = new System.Drawing.Size(62, 13);
			this.lblProgress.TabIndex = 17;
			this.lblProgress.Text = "Iterations: 0";
			// 
			// lblGuest
			// 
			this.lblGuest.AutoSize = true;
			this.lblGuest.Enabled = false;
			this.lblGuest.Location = new System.Drawing.Point(361, 501);
			this.lblGuest.Name = "lblGuest";
			this.lblGuest.Size = new System.Drawing.Size(38, 13);
			this.lblGuest.TabIndex = 18;
			this.lblGuest.Text = "Guest:";
			// 
			// lblDesiredOutput
			// 
			this.lblDesiredOutput.AutoSize = true;
			this.lblDesiredOutput.Location = new System.Drawing.Point(373, 42);
			this.lblDesiredOutput.Name = "lblDesiredOutput";
			this.lblDesiredOutput.Size = new System.Drawing.Size(81, 13);
			this.lblDesiredOutput.TabIndex = 19;
			this.lblDesiredOutput.Text = "Desired Output:";
			// 
			// lblPatternSet
			// 
			this.lblPatternSet.AutoSize = true;
			this.lblPatternSet.Location = new System.Drawing.Point(585, 42);
			this.lblPatternSet.Name = "lblPatternSet";
			this.lblPatternSet.Size = new System.Drawing.Size(61, 13);
			this.lblPatternSet.TabIndex = 20;
			this.lblPatternSet.Text = "Pattern set:";
			// 
			// updnHiddenLayers
			// 
			this.updnHiddenLayers.Location = new System.Drawing.Point(553, 277);
			this.updnHiddenLayers.Maximum = new decimal(new int[] {
            21,
            0,
            0,
            0});
			this.updnHiddenLayers.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.updnHiddenLayers.Name = "updnHiddenLayers";
			this.updnHiddenLayers.Size = new System.Drawing.Size(56, 20);
			this.updnHiddenLayers.TabIndex = 21;
			this.updnHiddenLayers.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
			this.updnHiddenLayers.ValueChanged += new System.EventHandler(this.updnHiddenLayers_ValueChanged);
			// 
			// updnSet
			// 
			this.updnSet.Location = new System.Drawing.Point(652, 38);
			this.updnSet.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.updnSet.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.updnSet.Name = "updnSet";
			this.updnSet.Size = new System.Drawing.Size(56, 20);
			this.updnSet.TabIndex = 22;
			this.updnSet.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.updnSet.ValueChanged += new System.EventHandler(this.updnSet_ValueChanged);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(670, 309);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(15, 20);
			this.btnSave.TabIndex = 23;
			this.btnSave.Text = "S";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(693, 309);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(15, 20);
			this.btnLoad.TabIndex = 24;
			this.btnLoad.TabStop = false;
			this.btnLoad.Text = "L";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
			// 
			// patternEdit1
			// 
			this.patternEdit1.Location = new System.Drawing.Point(12, 39);
			this.patternEdit1.Name = "patternEdit1";
			this.patternEdit1.Pattern = new int[] {
        0};
			this.patternEdit1.Size = new System.Drawing.Size(210, 280);
			this.patternEdit1.TabIndex = 25;
			this.patternEdit1.TabStop = false;
			// 
			// hiddenLayer
			// 
			this.hiddenLayer.Bars = 7;
			this.hiddenLayer.Location = new System.Drawing.Point(376, 101);
			this.hiddenLayer.Name = "hiddenLayer";
			this.hiddenLayer.Size = new System.Drawing.Size(348, 160);
			this.hiddenLayer.StartNum = 1;
			this.hiddenLayer.TabIndex = 11;
			// 
			// outputLayer
			// 
			this.outputLayer.Bars = 10;
			this.outputLayer.Location = new System.Drawing.Point(8, 343);
			this.outputLayer.Name = "outputLayer";
			this.outputLayer.Size = new System.Drawing.Size(716, 145);
			this.outputLayer.TabIndex = 12;
			// 
			// errorGauge1
			// 
			this.errorGauge1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.errorGauge1.Location = new System.Drawing.Point(240, 101);
			this.errorGauge1.Name = "errorGauge1";
			this.errorGauge1.Size = new System.Drawing.Size(120, 160);
			this.errorGauge1.TabIndex = 8;
			this.errorGauge1.Value = 100D;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(725, 525);
			this.Controls.Add(this.patternEdit1);
			this.Controls.Add(this.btnLoad);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.updnSet);
			this.Controls.Add(this.updnHiddenLayers);
			this.Controls.Add(this.lblPatternSet);
			this.Controls.Add(this.lblDesiredOutput);
			this.Controls.Add(this.lblGuest);
			this.Controls.Add(this.lblProgress);
			this.Controls.Add(this.lblPattern);
			this.Controls.Add(this.lblHidden);
			this.Controls.Add(this.lblOutput);
			this.Controls.Add(this.lblInput);
			this.Controls.Add(this.chkMode);
			this.Controls.Add(this.btnTrain);
			this.Controls.Add(this.updnPattern);
			this.Controls.Add(this.hiddenLayer);
			this.Controls.Add(this.outputLayer);
			this.Controls.Add(this.errorGauge1);
			this.Controls.Add(this.btnTest);
			this.Controls.Add(this.btn100Step);
			this.Controls.Add(this.btnStep);
			this.Controls.Add(this.btnQuit);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Backpropagation with momentum";
			((System.ComponentModel.ISupportInitialize)(this.updnPattern)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnHiddenLayers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updnSet)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		#endregion

		private System.Windows.Forms.Label lblInput;
		private System.Windows.Forms.Label lblOutput;
		private System.Windows.Forms.Label lblHidden;
		private System.Windows.Forms.Label lblPattern;
		private Bkprop.ErrorGauge errorGauge1;
		private Bkprop.NLayer outputLayer;
		private Bkprop.NLayer hiddenLayer;
		private System.Windows.Forms.NumericUpDown updnPattern;
		private System.Windows.Forms.CheckBox chkMode;
		private System.Windows.Forms.Button btnStep;
		private System.Windows.Forms.Button btn100Step;
		private System.Windows.Forms.Button btnQuit;
		private System.Windows.Forms.Button btnTrain;
		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.Label lblProgress;
		private System.Windows.Forms.Label lblGuest;
		private System.Windows.Forms.Label lblDesiredOutput;
		private System.Windows.Forms.Label lblPatternSet;
		private System.Windows.Forms.NumericUpDown updnHiddenLayers;
		private System.Windows.Forms.NumericUpDown updnSet;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnLoad;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private UserControls.PatternEdit patternEdit1;
	}
}

