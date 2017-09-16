using System;
using Gtk;

class MainClass {
	public static void Main (string[] args)
	{
		Application.Init();
		SetUpGui ();
		Application.Run ();
	}

	static void SetUpGui ()
	{
		Window w = new Window ("Gtk# Contacts");

		VBox v = new VBox();
		v.BorderWidth = 6;
		v.Spacing = 6;
		w.Add(v);

		HBox h = new HBox ();
		h.BorderWidth = 6;
		h.Spacing = 6;
		v.PackStart(h, false, false, 0);

		Button b = new Button ("Save");
		b.Clicked += new EventHandler (Button_Clicked);
		v.PackStart(b, false, false, 0);

		v = new VBox ();
		v.Spacing = 6;
		h.PackStart (v, false, false, 0);

		Label l = new Label ("Full name: ");
		l.Xalign = 0;
		v.PackStart (l, true, false, 0);

		l = new Label ("Email address: ");
		l.Xalign = 0;
		v.PackStart (l, true, false, 0);

		v = new VBox ();
		v.Spacing = 6;
		h.PackStart (v, true, true, 0);

		v.PackStart (new Entry(), true, true, 0);
		v.PackStart (new Entry(), true, true, 0);

		w.DeleteEvent += new DeleteEventHandler (Window_Delete);
		w.ShowAll ();
	}

	static void Window_Delete (object o, DeleteEventArgs args)
	{
		Application.Quit ();
		args.RetVal = true;
	}

	static void Button_Clicked (object o, EventArgs args)
	{
		System.Console.WriteLine ("Hello, World!");
	}
}

