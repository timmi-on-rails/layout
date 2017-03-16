using LayoutLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Layout
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			StackPanelLayout stackPanelLayout = new StackPanelLayout
			{
				Orientation = Orientation.Vertical,
				Children =
				{
					new StackPanelLayout
					{
						Orientation = Orientation.Horizontal,
						Children = { new BoxElement(100, 200), new BoxElement(200,300) }
					},
					new StackPanelLayout
					{
						Orientation = Orientation.Horizontal,
						Children = { new BoxElement(100, 200), new BoxElement(200,300) }
					}
				}
			};

			stackPanelLayout.CalculateLayout(new Size(2000, 2000));

			foreach (LayoutElement layoutElement in stackPanelLayout.GetLeafs())
			{
				Rect bounds = stackPanelLayout.GetDescendantBounds(layoutElement);
			}
		}
	}


}
