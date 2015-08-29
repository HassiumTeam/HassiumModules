using System;
using System.Windows.Forms;
using Hassium;

namespace FormsModule
{
	public class Program : ILibrary
	{
		[IntFunc("newform")]
		public static object NewForm(object[] args)
		{
			Form myForm = new Form();
			myForm.Text = args[0].ToString();
			myForm.Show();

			return myForm;
		}

		[IntFunc("formclose")]
		public static object FormClose(object[] args)
		{
			((Form)args[0]).Close();
		
			return null;
		}

		[IntFunc("messagebox")]
		public static object MessageBox(object[] args)
		{
			System.Windows.Forms.MessageBox.Show(args[0].ToString());

			return null;
		}

		[IntFunc("formadd")]
		public static object FormAdd(object[] args)
		{
			Form myForm = ((Form)args[0]);
			myForm.Controls.Add(((Control)args[1]));

			return null;
		}

		[IntFunc("label")]
		public static object Label(object[] args)
		{
			return new Label() {Text = args[0].ToString()};
		}

		[IntFunc("settext")]
		public static object SetText(object[] args)
		{
			Control myControl = ((Control)args[0]);
			myControl.Text = args[1].ToString();

			return null;
		}
	}
}
