using System;
using Eto.Forms;

namespace Eto.GtkSharp
{
	public class SeparatorMenuItemHandler : MenuHandler<Gtk.SeparatorMenuItem, SeparatorMenuItem, SeparatorMenuItem.ICallback>, SeparatorMenuItem.IHandler
	{
		public SeparatorMenuItemHandler()
		{
			Control = new Gtk.SeparatorMenuItem();
		}

		public void CreateFromCommand(Command command)
		{
		}

		protected override Keys GetShortcut()
		{
			return Keys.None;
		}

		public string Text
		{
			get { return null; }
			set { throw new NotSupportedException(); }
		}

		public string ToolTip
		{
			get { return null; }
			set { throw new NotSupportedException(); }
		}

		public Keys Shortcut
		{
			get { return Keys.None; }
			set { throw new NotSupportedException(); }
		}

		public bool Enabled
		{
			get { return false; }
			set { }
		}
	}
}
