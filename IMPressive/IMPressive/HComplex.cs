using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hassium.Functions;
using Hassium.HassiumObjects;
using Hassium.HassiumObjects.Types;
using Hassium.Interpreter;
using IMPression;

namespace IMPressive
{
    public class HComplex : HassiumObject
    {
        public double Real
        {
            get { return Value.Real; }
            set { Value = new Complex(value, Imaginary); }
        }

        public double Imaginary
        {
            get { return Value.Imaginary; }
            set { Value = new Complex(Real, value); }
        }

        public HComplex(Complex c) : this(c.Real, c.Imaginary)
        {

        }

        public HComplex(double r, double i) : this()
        {
            Value = new Complex(r, i);
        }

        public HComplex()
        {
            Value = new Complex(0, 0);

            Attributes.Add("real", new HassiumProperty("real", x => Real, (self, x) => Real = x[0].HDouble().Value));
            Attributes.Add("imaginary", new HassiumProperty("imaginary", x => Imaginary, (self, x) => Imaginary = x[0].HDouble().Value));
            Attributes.Add("imag", new HassiumProperty("imag", x => Imaginary, (self, x) => Imaginary = x[0].HDouble().Value));

            Attributes.Add("module", new HassiumProperty("module", x => (double)Value.Module, null, true));
            Attributes.Add("argument", new HassiumProperty("argument", x => (double)Value.Argument, null, true));
            Attributes.Add("conjugate", new HassiumProperty("conjugate", x => (double)Value.Conjugate, null, true));

            Attributes.Add("__add", new InternalFunction(Add, -1));
            Attributes.Add("__substract", new InternalFunction(Substract, -1));
            Attributes.Add("__multiply", new InternalFunction(Multiply, -1));
            Attributes.Add("__divide", new InternalFunction(Divide, -1));

            Attributes.Add("__compare", new InternalFunction(x =>
            {
                var v = x[0].HComplex().Value;
                if (v > Value) return 1;
                if (v < Value) return -1;
                return 0;
            }, 1));

            Attributes.Add("__pow", new InternalFunction(x => new HComplex(Functions.Pow(Value, x[0].HComplex().Value)), 1));
            Attributes.Add("__root", new InternalFunction(x => new HComplex(Functions.NthRoot(Value, x[0].HComplex().Value)), 1));

            Attributes.Add("negate", new InternalFunction(Negate, 0));

            Attributes.Add("square", new InternalFunction(x => new HComplex(Value.Square()), 0));
        }

        public HassiumObject Negate(HassiumObject[] args)
        {
            return new HComplex(-Value);
        }

        public HassiumObject Add(HassiumObject[] args)
        {
            return new HComplex(Value + args[0].HComplex().Value);
        }

        public HassiumObject Substract(HassiumObject[] args)
        {
            return new HComplex(Value - args[0].HComplex().Value);
        }

        public HassiumObject Multiply(HassiumObject[] args)
        {
            return new HComplex(Value * args[0].HComplex().Value);
        }

        public HassiumObject Divide(HassiumObject[] args)
        {
            return new HComplex(Value / args[0].HComplex().Value);
        }

        public Complex Value { get; private set; }

        public static implicit operator HComplex(HassiumDouble d)
        {
            return new HComplex(d.Value, 0);
        }

        public static implicit operator HComplex(Complex c)
        {
            return new HComplex(c.Real, c.Imaginary);
        }

        public static explicit operator Complex(HComplex c)
        {
            return new Complex(c.Real, c.Imaginary);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public static class Extensions
    {
        public static HComplex HComplex(this HassiumObject obj)
        {
            if (obj is HassiumDouble || obj is HassiumInt) return obj.HDouble();
            if (obj is HComplex) return (HComplex) obj;
            throw new ParseException("Only double and int can be cast to Complex", -1);
        }
    }
}
