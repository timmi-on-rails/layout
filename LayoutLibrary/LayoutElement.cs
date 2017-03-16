using System.Windows;

namespace LayoutLibrary
{
	public abstract class LayoutElement
	{
		public Thickness Margin
		{
			get
			{
				return FrameworkElement.Margin;
			}
			set
			{
				FrameworkElement.Margin = value;
			}
		}

		abstract internal FrameworkElement FrameworkElement { get; }
	}

	public class BoxElement : LayoutElement
	{
		public Size Size
		{
			get
			{
				return new Size(FrameworkElement.Width, FrameworkElement.Height);
			}
			set
			{
				FrameworkElement.Width = value.Width;
				FrameworkElement.Height = value.Height;
			}
		}

		public BoxElement(double width = 0, double height = 0)
		{
			Size = new Size(width, height);
		}

		internal override FrameworkElement FrameworkElement { get; } = new FrameworkElement();
	}
}
