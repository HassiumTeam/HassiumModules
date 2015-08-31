using System;
using System.Windows.Forms;
using Hassium;
using Hassium.HassiumObjects;

namespace Hassium.Functions
{
    public class FormsLib : ILibrary
    {
        [IntFunc("Form", true)]
        public static HassiumObject Form(HassiumObject[] args)
        {
            return new HassiumForm(args[0].ToString());
        }

        [IntFunc("Label", true)]
        public static HassiumObject Label(HassiumObject[] args)
        {
            return new HassiumLabel(args[0].ToString());
        }
    }
}

