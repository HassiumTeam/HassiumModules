using System;
using System.Windows.Forms;
using Hassium;

namespace Hassium.HassiumObjects
{
	public class HassiumForm: HassiumObject
	{
		public Form Value { get; private set; }

		public HassiumForm(string title)
		{
			this.Value = new Form();
			this.Value.Text = title;
			this.Attributes.Add("add", new InternalFunction(add));
			this.Attributes.Add("show", new InternalFunction(show));
		}

		private HassiumObject add(HassiumObject[] args)
		{
            this.Value.Controls.Add(((HassiumLabel)args[0]).Value);
			return null;
		}

		private HassiumObject show(HassiumObject[] args)
		{
			this.Value.ShowDialog();
			return null;
		}
	}
}
