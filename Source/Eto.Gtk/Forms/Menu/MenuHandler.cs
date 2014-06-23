using System;
using Eto.Forms;
using System.Linq;

namespace Eto.GtkSharp
{
	public interface IMenuHandler
	{
		void SetAccelGroup(Gtk.AccelGroup accel);
	}

	public abstract class MenuHandler<TControl, TWidget, TCallback> : WidgetHandler<TControl, TWidget, TCallback>, Menu.IHandler, IMenuHandler
		where TWidget: Menu
		where TCallback : Menu.ICallback
	{
		protected void ValidateItems()
		{
			var subMenu = Widget as ISubmenu;
			if (subMenu != null)
			{
				foreach (var item in subMenu.Items)
				{
					var handler = item.Handler as IMenuActionItemHandler;
					if (handler != null)
						handler.TriggerValidate();
				}
			}
		}

		public virtual void SetAccelGroup(Gtk.AccelGroup accel)
		{
			AccelGroup = accel;
			SetAccelerator();
			var subMenu = Widget as ISubmenu;
			if (subMenu != null)
			{
				foreach (var item in subMenu.Items.Select(r => r.Handler).OfType<IMenuHandler>())
				{
					item.SetAccelGroup(accel);
				}
			}
		}

		protected virtual void SetChildAccelGroup(MenuItem item)
		{
			if (item == null)
				return;
			var handler = item.Handler as IMenuHandler;
			if (handler != null)
				handler.SetAccelGroup(AccelGroup);
		}

		protected abstract Keys GetShortcut();

		protected void SetAccelerator()
		{
			var shortcut = GetShortcut();
			if (AccelGroup != null && shortcut != Keys.None)
			{
				var widget = (Gtk.Widget)Widget.ControlObject;
				var key = new Gtk.AccelKey(shortcut.ToGdkKey(), shortcut.ToGdkModifier(), Gtk.AccelFlags.Visible | Gtk.AccelFlags.Locked);
				widget.AddAccelerator("activate", AccelGroup, key);
			}
		}

		protected Gtk.AccelGroup AccelGroup { get; private set; }
	}
}
