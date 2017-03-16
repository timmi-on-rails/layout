using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LayoutLibrary
{
	public class LayoutElementCollection : IEnumerable<LayoutElement>
	{
		private readonly Action<LayoutElement> addLayoutElement;
		private List<LayoutElement> collection = new List<LayoutElement>();

		public LayoutElementCollection(Action<LayoutElement> addLayoutElement)
		{
			this.addLayoutElement = addLayoutElement;
		}

		public void Add(LayoutElement child)
		{
			addLayoutElement(child);
			collection.Add(child);
		}

		public IEnumerator<LayoutElement> GetEnumerator()
		{
			return collection.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return collection.GetEnumerator();
		}
	}

	public abstract class LayoutContainer : LayoutElement
	{
		public LayoutContainer()
		{
			Children = new LayoutElementCollection((child) => AddChildInternal(child.FrameworkElement));
		}

		public LayoutElementCollection Children { get; }

		abstract internal protected void AddChildInternal(FrameworkElement child);

		public Rect GetDescendantBounds(LayoutElement child)
		{
			return child.FrameworkElement.TransformToVisual(FrameworkElement)
						.TransformBounds(new Rect(child.FrameworkElement.RenderSize));
		}

		public IEnumerable<LayoutElement> GetLeafs()
		{
			foreach (LayoutElement layoutElement in Children)
			{
				if (layoutElement is LayoutContainer)
				{
					foreach (LayoutElement child in (layoutElement as LayoutContainer).GetLeafs())
					{
						yield return child;
					}
				}
				else
				{
					yield return layoutElement;
				}
			}
		}

		public void CalculateLayout(Size availableSize)
		{
			FrameworkElement.Measure(availableSize);
			FrameworkElement.Arrange(new Rect(availableSize));
		}
	}

	public class StackPanelLayout : LayoutContainer
	{
		private StackPanel stackPanel = new StackPanel();

		public Orientation Orientation
		{
			get
			{
				return stackPanel.Orientation;
			}
			set
			{
				stackPanel.Orientation = value;
			}
		}

		internal override FrameworkElement FrameworkElement { get { return stackPanel; } }

		internal protected override void AddChildInternal(FrameworkElement child)
		{
			stackPanel.Children.Add(child);
		}
	}
}
