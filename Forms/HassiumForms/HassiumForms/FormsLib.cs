using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hassium.Functions;
using Hassium.HassiumObjects;

namespace HassiumForms
{
    public class FormsLib : ILibrary
    {
        [IntFunc("Form", true, 1)]
        public static HassiumObject Form(HassiumObject[] args)
        {
            return new HassiumForm(args[0].ToString());
        }

        [IntFunc("Label", true, 1)]
        public static HassiumObject Label(HassiumObject[] args)
        {
            return new HassiumLabel(args[0].ToString());
        }
    }
}
