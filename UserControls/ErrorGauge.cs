using System.ComponentModel;
using System.Drawing;

namespace Bkprop
{
	/// <summary>
	/// Summary description for ErrorGauge.
	/// </summary>
	public class ErrorGauge : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// ErrorGauge
			// 
			this.Name = "ErrorGauge";
			this.Size = new System.Drawing.Size(88, 168);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ErrorGauge_Paint);
			this.Text = "Max. Error";
		}

		#endregion

		public ErrorGauge()
		{
			InitializeComponent();
			//this.Text = "Global Error";
		}

		[Browsable(true)			/*Category("Appearance")*/	]
		public override string Text { get; set; }

		private double _val = 100.0;
		/*
		[
			Browsable(true),
			Category("Appearance"),
			Description("Specifies the Value in %.")
		]
		*/
		public double Value
		{
			get { return _val; }

			set
			{
				_val = value;
				Invalidate();
			}
		}

		private void ErrorGauge_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			float h, v = (float)_val;
			if (v < 0)
				v = 0;
			if (v > 100)
				v = 100;

			Graphics g = e.Graphics;
			SizeF stringSize = g.MeasureString(Text, Font);

			using (Brush backBrush = new SolidBrush(BackColor))
				g.FillRectangle(backBrush, 0, 0, Width - 1, Height - 1);

			h = 0.01f * (100 - v) * (Height - 2 - stringSize.Height);

			g.FillRectangle(Brushes.Red,
				Width / 2 + 1, h,
				Width - Width / 2 - 2, Height - stringSize.Height - h);

			g.DrawRectangle(Pens.Black,
				Width / 2, 0,
				Width - Width / 2 - 1, Height - stringSize.Height - 1);

			using (Brush foreBrush = new SolidBrush(ForeColor))
			{
				g.DrawString("100%", Font, foreBrush, 0, 0);
				g.DrawString(_val.ToString("F") + "%", Font, foreBrush, 0, Height / 2 - 2 * stringSize.Height);
				g.DrawString("0%", Font, foreBrush, 0, Height - 2 * stringSize.Height);

				g.DrawString(Text, Font, foreBrush, 0.5f * (Width - stringSize.Width), Height - stringSize.Height);
			}
		}
	}
}
