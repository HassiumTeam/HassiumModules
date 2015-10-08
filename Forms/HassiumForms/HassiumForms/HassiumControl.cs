using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Drawing;
using Hassium.HassiumObjects.Types;

namespace HassiumForms
{
    public class HassiumControl : HassiumObject
    {
        public Control Value { get; protected set; }

        protected HassiumControl(Control c) : this()
        {
            Value = c;
            Value.Invalidated += (sender, args) => Value.Parent.Invalidate();
        }

        protected HassiumControl()
        {
            Attributes.Add("backColor", new HassiumProperty("backColor", x => new HassiumColor(Value.BackColor),
                x => { Value.BackColor = ((HassiumColor) x[0]).Value;
                         return null;
                }));

            Attributes.Add("backImage", new HassiumProperty("backImage", x => new HassiumImage(Value.BackgroundImage), 
                x => {
                    Value.BackgroundImage = ((HassiumImage)x[0]).Value;
                    return null;
                }));

            Attributes.Add("controls", new HassiumProperty("controls", x =>
            {
                var ret = new HassiumArray(Value.Controls.Cast<Control>().Select(y => new HassiumControl(y)));
                ret.Attributes["add"] = new InternalFunction(y =>
                {
                    Value.Controls.Add(((HassiumControl) y[0]).Value);
                    return null;
                }, 1);

                ret.Attributes["remove"] = new InternalFunction(y =>
                {
                    Value.Controls.Remove(((HassiumControl)y[0]).Value);
                    return null;
                }, 1);
                return ret;
            }, null, true));

            Attributes.Add("enabled", new HassiumProperty("enabled", x => Value.Enabled, x => Value.Enabled = x[0].HBool().Value));

            Attributes.Add("foreColor", new HassiumProperty("foreColor", x => new HassiumColor(Value.ForeColor),
                x => {
                    Value.ForeColor = ((HassiumColor)x[0]).Value;
                    return null;
                }));

            Attributes.Add("location", new HassiumProperty("location", x => new HassiumPoint(Value.Location.X, Value.Location.Y),
                x =>
                {
                    Value.Location = ((HassiumPoint) x[0]).Value;
                    return null;
                }));

            Attributes.Add("x", new HassiumProperty("x", x => Value.Location.X, x =>
            {
                Value.Location = new Point(x[0].HInt().Value, Value.Location.Y);
                return null;
            }));

            Attributes.Add("y", new HassiumProperty("y", x => Value.Location.Y, x =>
            {
                Value.Location = new Point(Value.Location.X, x[0].HInt().Value);
                return null;
            }));

            Attributes.Add("name", new HassiumProperty("name", x => Value.Name, x => Value.Name = x[0].ToString()));

            Attributes.Add("parent", new HassiumProperty("parent", x => new HassiumControl(Value.Parent), x =>
            {
                Value.Parent = ((HassiumControl) x[0]).Value;
                return null;
            }));

            Attributes.Add("size", new HassiumProperty("size", x => new HassiumSize(Value.Width, Value.Height),
                x =>
                {
                    Value.Size = ((HassiumSize)x[0]).Value;
                    return null;
                }));

            Attributes.Add("width", new HassiumProperty("width", x => Value.Width, x => Value.Width = x[0].HInt().Value));

            Attributes.Add("height", new HassiumProperty("height", x => Value.Height, x => Value.Height = x[0].HInt().Value));

            Attributes.Add("visible", new HassiumProperty("visible", x => Value.Visible, x => Value.Visible = x[0].HBool().Value));
        }
    }
}
