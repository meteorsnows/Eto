using System;
using Eto.Forms;
using MonoTouch.UIKit;
using System.Linq;
using MonoTouch.Foundation;
using System.Diagnostics;
using Eto.Platform.Mac.Forms;

namespace Eto.Platform.iOS.Forms.Controls
{
	public class SplitterHandler : MacContainer<UIView, Splitter>, ISplitter
	{
		protected override UIViewController CreateController()
		{
			return (UIViewController)MGSplitController ?? SplitController;
		}

		public override UIView ContainerControl { get { return Control; } }
		
		public UISplitViewController SplitController { get; set; }

		public MG.MGSplitViewController MGSplitController { get; set; }

		UIViewController[] children;
		Control panel1;
		Control panel2;

		public bool UseMGSplitViewController { get; set; }

		public override UIView CreateControl ()
		{
			if (UseMGSplitViewController) {
				MGSplitController = new MG.MGSplitViewController ();
				return MGSplitController.View;
			} else {
				SplitController = new UISplitViewController ();
				return SplitController.View;
			}
		}
		
		public SplitterHandler ()
		{
			UseMGSplitViewController = false;
		}

		protected override void Initialize ()
		{
			base.Initialize ();
			//SplitController.Delegate = new MG.MGSplitViewControllerDelegate();
			Control.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			
			children = new UIViewController[2];
			children [0] = new UIViewController ();
			children [1] = new UIViewController ();
			SetViewControllers ();

			if (UseMGSplitViewController) {
				MGSplitController.AllowsDraggingDivider = true;
				MGSplitController.DividerStyle = MG.MGSplitViewDividerStyle.PaneSplitter;
				MGSplitController.SplitWidth = 10;
			}
		}

		/**/
		public int Position {
			get { 
				if (UseMGSplitViewController)
					return (int)MGSplitController.SplitPosition;
				else
					return 0;
			}
			set { 
				//SplitController.SplitPosition = value;
			}
		}
		
		public SplitterOrientation Orientation {
			get {
				if (UseMGSplitViewController)
					return MGSplitController.Vertical ? SplitterOrientation.Vertical : SplitterOrientation.Horizontal;
				else
					return SplitterOrientation.Horizontal;
			}
			set { 
				if (UseMGSplitViewController)
					MGSplitController.Vertical = value == SplitterOrientation.Vertical;
				else if (value == SplitterOrientation.Vertical)
					Debug.WriteLine ("UISplitViewController cannot set orientation to vertical");
			}
		}

		public SplitterFixedPanel FixedPanel {
			get {
				return SplitterFixedPanel.None;
			}
			set {
				
			}
		}

		void SetViewControllers ()
		{
			if (UseMGSplitViewController)
				MGSplitController.ViewControllers = children;
			else
				SplitController.ViewControllers = children;
		}

		public Control Panel1 {
			get { return panel1; }
			set {
				if (panel1 != value) {
					children [0] = value.GetViewController () ?? new RotatableViewController ();
					SetViewControllers ();
					panel1 = value;
				}
			}
		}

		public Control Panel2 {
			get { return panel2; }
			set {
				if (panel2 != value) {
					children [1] = value.GetViewController () ?? new RotatableViewController ();
					SetViewControllers ();
					panel2 = value;
				}
			}
		}
	}
}
