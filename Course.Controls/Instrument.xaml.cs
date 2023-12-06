using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Course.Controls
{
	/// <summary>
	/// Instrument.xaml 的交互逻辑
	/// </summary>
	public partial class Instrument : UserControl
	{

		public Brush PlateBackground
		{
			get { return (Brush)GetValue(PlateBackgroundProperty); }
			set { SetValue(PlateBackgroundProperty, value); }
		}

		// Using a DependencyProperty as the backing store for PlateBackground.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty PlateBackgroundProperty =
			DependencyProperty.Register("PlateBackground", typeof(Brush), typeof(Instrument), new PropertyMetadata(default(Brush)));


		public int Value
		{
			get { return (int)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(int), typeof(Instrument),
			new PropertyMetadata(default(int), new PropertyChangedCallback(OnPropertyChanged)));

		public int Minimum
		{
			get { return (int)GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Minimum.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty MinimumProperty =
			DependencyProperty.Register("Minimum", typeof(int), typeof(Instrument),
			new PropertyMetadata(default(int), new PropertyChangedCallback(OnPropertyChanged)));

		public int Maximum
		{
			get { return (int)GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Maximum.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty MaximumProperty =
			DependencyProperty.Register("Maximum", typeof(int), typeof(Instrument),
			new PropertyMetadata(default(int), new PropertyChangedCallback(OnPropertyChanged)));

		public int Interval
		{
			get { return (int)GetValue(IntervalProperty); }
			set { SetValue(IntervalProperty, value); }
		}

		// Using a DependencyProperty as the backing store for Interval.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty IntervalProperty =
			DependencyProperty.Register("Interval", typeof(int), typeof(Instrument),
			new PropertyMetadata(default(int), new PropertyChangedCallback(OnPropertyChanged)));


		public int ScaleTextSize
		{
			get { return (int)GetValue(ScaleTextSizeProperty); }
			set { SetValue(ScaleTextSizeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ScaleTextSize.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ScaleTextSizeProperty =
			DependencyProperty.Register("ScaleTextSize", typeof(int), typeof(Instrument),
			new PropertyMetadata(default(int), new PropertyChangedCallback(OnPropertyChanged)));



		public Brush ScaleBrush
		{
			get { return (Brush)GetValue(ScaleBrushProperty); }
			set { SetValue(ScaleBrushProperty, value); }
		}

		// Using a DependencyProperty as the backing store for ScaleBrush.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ScaleBrushProperty =
			DependencyProperty.Register("ScaleBrush", typeof(Brush), typeof(Instrument),
			new PropertyMetadata(default(Brush), new PropertyChangedCallback(OnPropertyChanged)));




		public static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			(d as Instrument).Refresh();
		}

		public Instrument()
		{
			InitializeComponent();
			this.SizeChanged += Instrument_SizeChanged;
		}

		private void Instrument_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			double minSize = Math.Min(this.RenderSize.Width, this.RenderSize.Height);
			this.backEllipse.Width = minSize;
			this.backEllipse.Height = minSize;
		}

		private void Refresh()
		{
			double radius = this.backEllipse.Width / 2;
			if (double.IsNaN(radius)) { return; }
			this.mainCanvas.Children.Clear();

			//double min = 0, max = 100;
			//double scaleAreaCount = 10;
			//double step = 270.0 / (max - min);
			double step = 270.0 / (this.Maximum - this.Minimum);

			//for (int i = 0; i < max - min; i++)
			for (int i = 0; i < this.Maximum - this.Minimum; i++)
			{
				Line lineScale = new Line();
				lineScale.X1 = radius - (radius - 13) * Math.Cos((i * step - 45) * Math.PI / 180);
				lineScale.Y1 = radius - (radius - 13) * Math.Sin((i * step - 45) * Math.PI / 180);
				lineScale.X2 = radius - (radius - 8) * Math.Cos((i * step - 45) * Math.PI / 180);
				lineScale.Y2 = radius - (radius - 8) * Math.Sin((i * step - 45) * Math.PI / 180);

				lineScale.Stroke = this.ScaleBrush;
				lineScale.StrokeThickness = 1;

				this.mainCanvas.Children.Add(lineScale);
			}

			//step = 270.0 / scaleAreaCount;
			step = 270.0 / Interval;
			//int scaleText = (int)min;
			int scaleText = (int)this.Minimum;
			//for (int i = 0; i <= scaleAreaCount; i++)
			for (int i = 0; i <= Interval; i++)
			{
				Line lineScale = new Line();
				lineScale.X1 = radius - (radius - 20) * Math.Cos((i * step - 45) * Math.PI / 180);
				lineScale.Y1 = radius - (radius - 20) * Math.Sin((i * step - 45) * Math.PI / 180);
				lineScale.X2 = radius - (radius - 8) * Math.Cos((i * step - 45) * Math.PI / 180);
				lineScale.Y2 = radius - (radius - 8) * Math.Sin((i * step - 45) * Math.PI / 180);

				lineScale.Stroke = this.ScaleBrush;
				lineScale.StrokeThickness = 1;
				this.mainCanvas.Children.Add(lineScale);

				TextBlock textScale = new TextBlock();
				textScale.Width = 34;
				textScale.TextAlignment = TextAlignment.Center;
				textScale.FontSize = this.ScaleTextSize;
				//textScale.Text = (scaleText + (max - min) / scaleAreaCount * i).ToString();
				textScale.Text = (scaleText + (this.Maximum - this.Minimum) / Interval * i).ToString();
				textScale.Foreground = this.ScaleBrush;
				Canvas.SetLeft(textScale, radius - (radius - 36) * Math.Cos((i * step - 45) * Math.PI / 180) - 17);
				Canvas.SetTop(textScale, radius - (radius - 36) * Math.Sin((i * step - 45) * Math.PI / 180) - 10);
				this.mainCanvas.Children.Add(textScale);
			}

			string sData = "M{0} {1} A{0} {0} 0 1 1 {1} {2}";
			sData = string.Format(sData, radius / 2, radius, radius * 1.5);
			var converter = TypeDescriptor.GetConverter(typeof(Geometry));
			this.circle.Data = (Geometry)converter.ConvertFrom(sData);

			//step = 270.0 / (max - min);
			step = 270.0 / (this.Maximum - this.Minimum);
			DoubleAnimation da = new DoubleAnimation((int)((this.Value - this.Minimum) * step) - 45, new Duration(TimeSpan.FromMilliseconds(200)));
			this.rtPointer.BeginAnimation(RotateTransform.AngleProperty, da);
			sData = "M{0} {1},{1} {2},{1} {3}";
			sData = string.Format(sData, radius * 0.3, radius, radius - 5, radius + 5);
			this.pointer.Data = (Geometry)converter.ConvertFrom(sData);
		}
	}
}
