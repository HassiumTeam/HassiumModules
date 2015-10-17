using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.Interpreter;
using IMPression;

namespace IMPressive
{
    public class Constructors : ILibrary
    {
        public static Dictionary<string, HassiumObject> GetConstants()
        {
            var res = new Dictionary<string, HassiumObject>();

            res.Add("I", new HComplex(0, 1));

            #region Constants
            var cts = new HassiumObject();
            cts.Attributes.Add("Pi", new InternalFunction(x => (HComplex)Constants.Pi, 0, true));
            cts.Attributes.Add("E", new InternalFunction(x => (HComplex)Constants.E, 0, true));
            cts.Attributes.Add("Catalan", new InternalFunction(x => (HComplex)Constants.Catalan, 0, true));
            cts.Attributes.Add("Degree", new InternalFunction(x => (HComplex)Constants.Degree, 0, true));
            cts.Attributes.Add("Epsilon", new InternalFunction(x => (HComplex)Constants.Epsilon, 0, true));
            cts.Attributes.Add("EulerGamma", new InternalFunction(x => (HComplex)Constants.EulerGamma, 0, true));
            cts.Attributes.Add("Glaisher", new InternalFunction(x => (HComplex)Constants.Glaisher, 0, true));
            cts.Attributes.Add("Khinchin", new InternalFunction(x => (HComplex)Constants.Khinchin, 0, true));
            cts.Attributes.Add("Phi", new InternalFunction(x => (HComplex)Constants.Phi, 0, true));
            cts.Attributes.Add("SpeedOfLight", new InternalFunction(x => (HComplex)Constants.SpeedOfLight, 0, true));
            cts.Attributes.Add("Tau", new InternalFunction(x => (HComplex)Constants.Tau, 0, true));

            res.Add("Constants", cts);
            #endregion

            #region Functions
            var imp = new HassiumObject();

            var funcs =
                typeof(Functions).GetMethods(BindingFlags.Public |
                                                  BindingFlags.Static);
            var ln = "";
            try
            {
                foreach (var x in funcs)
                {
                    if (Attribute.IsDefined(x, typeof(MathFunc)))
                        if ((Attribute.GetCustomAttribute(x, typeof(MathFunc)) as MathFunc).Hide) continue;
                    var n = x.Name;
                    ln = n;
                    if (Attribute.IsDefined(x, typeof(MathFunc)))
                        if ((Attribute.GetCustomAttribute(x, typeof(MathFunc)) as MathFunc).Name != "")
                            n = (Attribute.GetCustomAttribute(x, typeof(MathFunc)) as MathFunc).Name;

                    if (imp.Attributes.Any(f => f.Key == n)) continue;

                    if(funcs.Any(f => f.Name == n && f.GetParameters().Length != x.GetParameters().Length))
                    {
                        var overloads =
                            funcs.Where(f => f.Name == n && f.GetParameters().Length != x.GetParameters().Length);
                        var parameters = overloads.Select(f => f.GetParameters().Length).ToList();
                        parameters.Insert(0, x.GetParameters().Length);


                        imp.Attributes.Add(n, new InternalFunction(a =>
                        {
                            return new HComplex((Complex)overloads.First(f => f.GetParameters().Length == a.Length)
                                .Invoke(null, a.Select(y => (object) y.HComplex().Value).ToArray()));
                        }, parameters.ToArray()));
                    }
                    else imp.Attributes.Add(n, new InternalFunction(a => new HComplex((Complex)x.Invoke(null, a.Select(y => (object)y.HComplex().Value).ToArray())), x.GetParameters().Length));
                }
            }
            catch
            {
                throw new ParseException("Error while loading function: " + ln, -1);
            }


            imp.Attributes.Add("integrate",
                new InternalFunction(
                    x =>
                        new HComplex(Functions.Integrate(
                            y => HassiumMethod.GetFunc1(x[0])(new HComplex(y)).HComplex().Value, x[1].HComplex().Value,
                            x[2].HComplex().Value)), 3));


            res.Add("IMP", imp);
            #endregion

            return res;
        }

        [IntFunc("Complex", true, new []{0, 1, 2})]
        public static HassiumObject Complex(HassiumObject[] args)
        {
            if(args.Length == 0) return new HComplex(0, 0);
            if(args.Length == 1) return new HComplex(args[0].HDouble().Value, 0);
            else return new HComplex(args[0].HDouble().Value, args[1].HDouble().Value);
        }
    }
}
