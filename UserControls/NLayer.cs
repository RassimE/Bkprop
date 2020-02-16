using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bkprop
{
	/// <summary>
	/// Summary description for NLayer.
	/// </summary>
	public class NLayer : UserControl
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
			// NLayer
			// 
			this.Name = "NLayer";
			this.Size = new System.Drawing.Size(192, 104);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.NLayer_Paint);
		}
		#endregion

		public NLayer()
		{
			InitializeComponent();

			_startNum = 0;
			_bars = 0;
			Bars = 4;
			_updFlg = 0;
		}

		#region properties

		double _minValue = 0;
		[DefaultValue(0.0)]
		public double MinValue
		{
			get { return _minValue; }
			set
			{
				if (value == _minValue)
					return;
				_minValue = value;
				Invalidate();
			}
		}

		double _maxValue = 1;
		[DefaultValue(1.0)]
		public double MaxValue
		{
			get { return _maxValue; }
			set
			{
				if (value == _maxValue)
					return;
				_maxValue = value;
				Invalidate();
			}
		}

		Color _barBackColor = Color.Cyan;

		[DefaultValue(typeof(Color), "0xFF00FFFF")]
		public Color BarBackColor
		{
			get { return _barBackColor; }
			set
			{
				_barBackColor = value;
				Invalidate();
			}
		}

		Color _barForeColor = Color.DimGray;

		[DefaultValue(typeof(Color), "0xFF696969")]
		public Color BarForeColor
		{
			get { return _barForeColor; }
			set
			{
				_barForeColor = value;
				Invalidate();
			}
		}

		int _bars = 0;
		public int Bars
		{
			get { return _bars; }

			set
			{
				if (value > 0 && _bars != value)
				{
					int i;
					_bars = value;
					_barVals = new double[_bars];
					for (i = 0; i < _bars; i++)
						_barVals[i] = 0;	//_minValue + (_maxValue - _minValue) * i / (_bars - 1);//1;

					Invalidate();
				}
			}
		}

		double[] _barVals;

		public double this[int i]
		{
			get
			{
				if (i >= 0 && i < _bars)
					return _barVals[i];

				return 0;
			}

			set
			{
				if (i >= 0 && i < _bars)
				{
					if (_barVals[i] == value)
						return;

					_barVals[i] = value;
					if (_updFlg <= 0)
						Invalidate();
				}
			}
		}

		int _startNum = 0;
		[DefaultValue(0)]
		public int StartNum
		{
			get { return _startNum; }

			set
			{
				if (_startNum == value)
					return;

				_startNum = value;
				Invalidate();
			}
		}

		#endregion  properties

		#region Painting menegment

		int _updFlg;

		public void BeginUpdate()
		{
			_updFlg++;
		}

		public void EndUpdate()
		{
			_updFlg--;
			if (_updFlg <= 0)
			{
				_updFlg = 0;
				Invalidate();
			}
		}

		private void NLayer_Paint(object sender, PaintEventArgs e)
		{
			int i;
			Graphics g = e.Graphics;
			SizeF stringSize = g.MeasureString("9", Font);

			float w = 0.5f * (Width - stringSize.Width) / _bars;
			float h = Height - stringSize.Height - 2;
			float y = -1;

			using (Brush backBrush = new SolidBrush(BackColor))
				g.FillRectangle(backBrush, 0, 0, Width - 1, Height - 1);

			PointF[] points =
			{
				new PointF( 0.0F,  0.0F),
				new PointF( 0.0F, h),
				new PointF(Width,  h),
			};

			g.DrawLines(Pens.Black, points);

			Brush foreBrush = new SolidBrush(ForeColor);
			Brush barBackBrush = new SolidBrush(_barBackColor);
			Brush barForeBrush = new SolidBrush(_barForeColor);
			double inv = 1.0 / (_maxValue - _minValue);
			for (i = 0; i < _bars; i++)
			{
				float x = 2 * i * w + 0.5f * w;

				g.DrawString((_startNum + i).ToString(), Font, foreBrush,
					x + 0.5f * (w - stringSize.Width), h + 2);

				double v = (float)_barVals[i];
				if (v < _minValue) v = _minValue;
				if (v > _maxValue) v = _maxValue;
				float fv = (float)((v - _minValue) * inv);

				g.FillRectangle(barBackBrush, x, y, w, h * (1 - fv));
				g.FillRectangle(barForeBrush, x, y + h * (1 - fv), w, h * fv);
			}

			foreBrush.Dispose();
			barBackBrush.Dispose();
			barForeBrush.Dispose();
		}

		#endregion
	}
}
