﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UICanvas
{
	/// <summary>
	/// Interaktionslogik für CanvasControl.xaml
	/// </summary>
	public partial class CanvasControl : UserControl
	{
		public CanvasControl()
		{
			InitializeComponent();
		}

		public ObservableCollection<Screen> Screens
		{
			get { return (ObservableCollection<Screen>)GetValue(ScreensProperty); }
			set { SetValue(ScreensProperty, value); }
		}
		public static readonly DependencyProperty ScreensProperty =
			DependencyProperty.Register("Screens", typeof(ObservableCollection<Screen>), typeof(CanvasControl), new PropertyMetadata());
	}
}
