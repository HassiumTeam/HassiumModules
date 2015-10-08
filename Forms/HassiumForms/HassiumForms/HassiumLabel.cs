using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hassium.Functions;
using Hassium.HassiumObjects;

namespace HassiumForms
{
    public class HassiumLabel : HassiumControl
    {
        //private Label vLabel { get { return (Label)Value; } set { Value = value; } }
        public HassiumLabel(string text)
        {
            Value = new Label() { Text = text, AutoSize = true, Location = new Point(13, 13), Name = string.Concat(text.Where(char.IsLetter)) };
            Attributes.Add("text", new HassiumProperty("text", x => Value.Text, x => Value.Text = x[1].ToString()));

        }
    }
}
