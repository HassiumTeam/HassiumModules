using System;
using System.Windows.Forms;

namespace Hassium.HassiumObjects
{
	public class HassiumLabel: HassiumObject
	{
        public Control Value { get; private set; }

		public HassiumLabel(string value)
		{
            this.Attributes.Add("text", new InternalFunction(text));
            this.Value = new Label();
			this.Value.Text = value;
		}

        private HassiumObject text(HassiumObject[] args)
        {
            HassiumString ret = new HassiumString(Value.Text);
            Value.Text = ((HassiumString)args[0]).Value;
            return ret;
        }
	}
}
	
