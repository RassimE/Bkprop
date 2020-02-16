using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Bkprop.UserControls
{
	public partial class PatternEdit : UserControl
	{
		private bool _inEditMode;
		private int _blockWidth, _blockHeight;

		private int[] _matrizin;
		private int[] _pattern;

		private int _patternWidth;
		private int _patternHeight;
		private int _inputNumber;

		public PatternEdit()
		{
			InitializeComponent();

			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer |
					ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);

			_patternWidth = 1;
			_patternHeight = 1;
			_inputNumber = 1;
			_matrizin = new int[_inputNumber];
			_pattern = new int[_inputNumber];

			_inEditMode = false;
		}

		[Browsable(false)]
		public int[] Pattern
		{
			get { return _matrizin; }
			set
			{
				if (value.Length != _inputNumber)
				{

				}

				Array.Copy(value, _pattern, _inputNumber);
				Invalidate();
			}
		}

		public int this[int i]
		{
			get { return _matrizin[i]; }
			set { _pattern[i] = value; }
		}

		///
		/// InEditMode property
		/// 
		[DefaultValue(false)]
		public bool InEditMode
		{
			get { return _inEditMode; }
			set
			{
				_inEditMode = value;
				Invalidate();
			}
		}

		///
		/// InputNumber property
		/// 
		public int InputNumber
		{
			get { return _inputNumber; }
		}

		///
		/// PatternWidth property
		/// 
		[DefaultValue(1)]
		public int PatternWidth
		{
			get { return _patternWidth; }
			set
			{
				if (_patternWidth == value)
					return;

				_patternWidth = value;

				double kw = (double)Width / _patternWidth;
				//double kh = (double)inOutGrid.Height / _traningSet.PatternHeight;
				//double k = Math.Min(kw, kh);
				double k = kw;

				_blockWidth = (int)Math.Round(k);
				_blockHeight = (int)Math.Round(k);

				setInputNumber(_patternWidth * _patternHeight);
			}
		}

		///
		/// PatternHeight property
		/// 
		[DefaultValue(1)]
		public int PatternHeight
		{
			get { return _patternHeight; }
			set
			{
				if (_patternHeight == value)
					return;
				_patternHeight = value;

				setInputNumber(_patternWidth * _patternHeight);
			}
		}

		void setInputNumber(int newNum)
		{
			_inputNumber = newNum;
			_matrizin = new int[_inputNumber];
			_pattern = new int[_inputNumber];

			Invalidate();
		}

		// Paint the control
		private void DrawBlock(Graphics g, int i, int j, Color color)
		{
			int x0 = i * _blockWidth;
			int y0 = j * _blockHeight;

			using (Brush fillBrush = new SolidBrush(color))
			{
				// fill Block
				g.FillRectangle(fillBrush, x0 + 1, y0 + 1, _blockWidth - 2, _blockHeight - 2);
				// draw border
				g.DrawRectangle(Pens.Black, x0, y0, _blockWidth - 1, _blockHeight - 1);
			}
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			Graphics g = pe.Graphics;

			if (_inEditMode)
			{
				Color color1 = Color.White;
				Color color2 = Color.FromArgb(0x50, 0x50, 0x50);

				for (int j = 0; j < _patternHeight; j++)
				{
					for (int i = 0; i < _patternWidth; i++)
					{
						Color color = _matrizin[j * _patternWidth + i] == 0 ? color1 : color2;
						DrawBlock(g, i, j, color);
					}
				}
			}
			else
			{
				Color color1 = Color.FromArgb(0xef, 0xef, 0xef);
				Color color2 = Color.FromArgb(0x7f, 0x7f, 0x7f);

				for (int j = 0; j < _patternHeight; j++)
				{
					for (int i = 0; i < _patternWidth; i++)
					{
						Color color = _pattern[j * _patternWidth + i] == 0 ? color1 : color2;
						DrawBlock(g, i, j, color);
					}
				}
			}

			base.OnPaint(pe);
		}

		private void PatternEdit_Resize(object sender, EventArgs e)
		{
			if (_patternWidth <= 0)
				return;

			double kw = (double)Width / _patternWidth;
			//double kh = (double)inOutGrid.Height / _traningSet.PatternHeight;
			//double k = Math.Min(kw, kh);
			double k = kw;

			_blockWidth = (int)Math.Round(k);
			_blockHeight = (int)Math.Round(k);
			Invalidate();
		}

		private void PatternEdit_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && _inEditMode)
			{
				int i = e.X / _blockWidth;
				int j = e.Y / _blockHeight;

				if (i >= _patternWidth)
					i = _patternWidth - 1;
				if (j >= _patternHeight)
					j = _patternHeight - 1;

				_matrizin[j * _patternWidth + i] = 1 - _matrizin[j * _patternWidth + i];

				Invalidate();
			}
		}

	}
}
