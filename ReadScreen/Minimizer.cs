using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadScreen
{

    public class Calculator
    {
        public Calculator(Dictionary<String, double> values)
        {
            fValues = values == null ? new Dictionary<String, double>() : values;
        }

        public void SetRootExpression(String str)
        {
            fRoot = ParseExpression(str);
        }

        public void SetRootExpression(Expression exp)
        {
            fRoot = exp;
        }


        public Expression fRoot;
        public Dictionary<string, double> fValues;
        public ISet<Variable> fVars = new HashSet<Variable>();
        public Dictionary<String, Variable> fVarsDir = new Dictionary<String, Variable>();

        private bool IsAlpha(char ch)
        {
            return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z');
        }
        private bool IsNum(char ch)
        {
            return ch <= '9' && ch >= '0';
        }

        private String ReadString(CharIterator it)
        {
            var sb = new StringBuilder();
            for (; it.Valid();)
            {
                char ch = it.Value();
                if (IsAlpha(ch) || ch == '@' || IsNum(ch))
                {
                    sb.Append(ch);
                    it.MoveNext();
                }
                else
                {
                    break;
                }
            }
            return sb.ToString();
        }
        private double ReadNum(CharIterator it)
        {
            var sb = new StringBuilder();
            // read - +
            if (it.Value() == '-' || it.Value() == '+')
            {
                sb.Append(it.Value());
                it.MoveNext();
            }

            // read integer
            for (; it.Valid() && IsNum(it.Value());)
            {
                sb.Append(it.Value());
                it.MoveNext();
            }

            // read faction
            if (it.Valid() && it.Value() == '.')
            {
                sb.Append(it.Value());
                it.MoveNext();

                // read faction
                for (; it.Valid() && IsNum(it.Value());)
                {
                    sb.Append(it.Value());
                    it.MoveNext();
                }

            }

            // read exponent
            if (it.Valid() && (it.Value() == 'E' || it.Value() == 'e'))
            {
                sb.Append(it.Value());
                it.MoveNext();

                // read - +
                if (it.Valid() && (it.Value() == '-' || it.Value() == '+'))
                {
                    sb.Append(it.Value());
                    it.MoveNext();
                }

                // read integer
                for (; it.Valid() && IsNum(it.Value());)
                {
                    sb.Append(it.Value());
                    it.MoveNext();
                }
            }

            double ret = System.Convert.ToDouble(sb.ToString());
            return ret;
        }

        static Random gRnd = new Random();
        private Expression ParseID(CharIterator it)
        {
            char ch = it.Value();
            if (IsAlpha(ch) || ch == '@')
            {
                String varname = ReadString(it);
                Variable variable;
                if (!fVarsDir.TryGetValue(varname, out variable))
                {
                    variable = new Variable(varname);
                    variable.fIndex = fVars.Count;
                    variable.fName = varname;
                    if(fValues.ContainsKey(varname))
                    {
                        variable.fVal = fValues[varname];
                    }
                    else
                    {
                        variable.fVal = gRnd.Next() % 1000 / 1000.0;
                    }
                    fVars.Add(variable);
                    fVarsDir.Add(varname, variable);
                }
                var id = new IDExpression(variable);
                return id;
            }
            else if (ch == '-' || ch == '+' || IsNum(ch))
            {
                double v = ReadNum(it);
                var num = new NumberExpression(v);
                return num;
            }
            else if (ch == '(')
            {
                it.MoveNext();
                var expr = ParseAdd(it);
                if (it.Valid() && it.Value() == ')')
                {
                    it.MoveNext();
                    return expr;
                }
                else
                {
                    throw new Exception("Expect ')'");
                }
            }
            throw new Exception("Need a id or number!");
        }

        private Expression ParseUnitary(CharIterator it)
        {
            if (it.Valid() && (it.Value() == '-' || it.Value() == '+'))
            {
                var op = it.Value() == '-' ? Op.Negative : Op.Positive;
                it.MoveNext();
                var exp = ParseUnitary(it);
                if (exp is NumberExpression)
                {
                    var ne = (NumberExpression)exp;
                    if (op == Op.Negative)
                    {
                        ne.fValue = -ne.fValue;
                    }
                    return ne;
                }
                else
                {
                    return new UnitaryExpression(op, exp);
                }
            }
            return ParseID(it);
        }

        private Expression ParsePoW(CharIterator it)
        {
            var exp1 = ParseUnitary(it);
            if (it.Valid() && it.Value() == '^')
            {
                it.MoveNext();
                if (it.Valid() && IsNum(it.Value()))
                {
                    char ch = it.Value();
                    it.MoveNext();
                    if (it.Valid() && IsNum(it.Value()))
                    {
                        throw new Exception("Need a number in range (0-9)");
                    }
                    int n = ch - '0';
                    exp1 = new PowExpression(Op.Pow, exp1, n);
                }
                else {
                    throw new Exception("Need a number after ^!");
                }
            }
            return exp1;
        }

        private Expression ParseMul(CharIterator it)
        {
            var exp1 = ParsePoW(it);
            for (; it.Valid() && (it.Value() == '*' || it.Value() == '/');)
            {
                char ch = it.Value();
                Op op = ch == '*' ? Op.Mul : Op.Div;
                it.MoveNext();
                var exp2 = ParsePoW(it);
                exp1 = new BinaryExpression(op, exp1, exp2);
            }
            return exp1;
        }

        private Expression ParseAdd(CharIterator it)
        {
            var exp1 = ParseMul(it);
            for (; it.Valid() && (it.Value() == '+' || it.Value() == '-');)
            {
                char ch = it.Value();
                Op op = ch == '+' ? Op.Add : Op.Sub;
                it.MoveNext();
                var exp2 = ParseMul(it);
                exp1 = new BinaryExpression(op, exp1, exp2);
            }
            return exp1;
        }

        public Expression ParseExpression(String str)
        {
            var it = new CharIterator(str);
            var exp1 = ParseAdd(it);
            if (it.Valid())
            {
                throw new Exception("should reach end of string");
            }
            return exp1;
        }

    }


    public class Expression
    {

        public static Expression operator*(Expression a, Expression b)
        {
            return new BinaryExpression(Op.Mul, a, b);
        }
        public static Expression operator /(Expression a, Expression b)
        {
            return new BinaryExpression(Op.Div, a, b);
        }
        public static Expression operator -(Expression a, Expression b)
        {
            return new BinaryExpression(Op.Sub, a, b);
        }
        public static Expression operator +(Expression a, Expression b)
        {
            return new BinaryExpression(Op.Add, a, b);
        }
        public static Expression operator ^(Expression a, int b)
        {
            return new PowExpression(Op.Pow, a, b);
        }

        virtual public double OnEval() { throw new Exception(); }
        virtual public double OnDerivative(Variable x) { throw new Exception(); }
        virtual public double OnDerivative(Variable x, Variable y) { throw new Exception(); }


        virtual public double Eval() {
            if (!fEvalValid) {
                fEval = OnEval();
                fEvalValid = true;
            }
            return fEval;
        }
        virtual public double Derivative(Variable x) {
            if (!fDerivativeValid[x.fIndex])
            {
                fDerivativeValid[x.fIndex] = true;
                fDerivative[x.fIndex] = OnDerivative(x);
            }
            return fDerivative[x.fIndex];
        }

        virtual public double Derivative(Variable x, Variable y)
        {
            if (x.fIndex > y.fIndex) {
                return Derivative(y, x);
            }

            if (!fDerivative2Valid[x.fIndex, y.fIndex])
            {
                fDerivative2Valid[x.fIndex, y.fIndex] = true;
                fDerivative2[x.fIndex, y.fIndex] = OnDerivative(x, y);
            }
            return fDerivative2[x.fIndex, y.fIndex];
        }

        double fEval = 0;
        double[] fDerivative;
        double[,] fDerivative2;
        bool fEvalValid;
        bool[] fDerivativeValid;
        bool[,] fDerivative2Valid;
        

        virtual public void OnVariablesChanged(Calculator cal)
        {
            fEval = 0;
            fEvalValid = false;
            fDerivativeValid = new bool[cal.fVars.Count];
            fDerivative = new double[cal.fVars.Count];
            fDerivative2Valid = new bool[cal.fVars.Count, cal.fVars.Count];
            fDerivative2 = new double[cal.fVars.Count, cal.fVars.Count];
        }
    }

    public enum Op
    {
        Add,
        Sub,
        Mul,
        Div,
        Pow,
        Negative,
        Positive,
    }

    public class BinaryExpression : Expression
    {

        override public void OnVariablesChanged(Calculator cal) {
            base.OnVariablesChanged(cal);
            fExpr1.OnVariablesChanged(cal);
            fExpr2.OnVariablesChanged(cal);
        }

        public BinaryExpression(Op op,Expression exp1, Expression exp2)
        {
            fOp = op;
            fExpr1 = exp1;
            fExpr2 = exp2;
        }
        override public double OnEval()
        {
            if (fOp == Op.Add)
            {
                return fExpr1.Eval()+ fExpr2.Eval();
            }
            else if (fOp == Op.Sub)
            {
                return fExpr1.Eval() - fExpr2.Eval();
            }
            else if (fOp == Op.Mul)
            {
                return fExpr1.Eval() * fExpr2.Eval();
            }
            else if (fOp == Op.Div)
            {
                return fExpr1.Eval() / fExpr2.Eval();
            }
            else
            {
                throw new Exception();
            }
        }

        override public double OnDerivative(Variable x)
        {
            if (fOp == Op.Add)
            {
                return fExpr1.Derivative(x) + fExpr2.Derivative(x);
            }
            else if (fOp == Op.Sub)
            {
                return fExpr1.Derivative(x) - fExpr2.Derivative(x);
            }
            else if (fOp == Op.Mul)
            {
                return fExpr1.Derivative(x) * fExpr2.Eval() + fExpr1.Eval() * fExpr2.Derivative(x);
            }
            else if (fOp == Op.Div)
            {
                return fExpr1.Derivative(x) / fExpr2.Eval() - fExpr1.Eval() * fExpr2.Derivative(x) / fExpr2.Eval() / fExpr2.Eval();
            }
            else
            {
                throw new Exception();
            }
        }
        override public double OnDerivative(Variable x, Variable y)
        {
            if (x.fIndex > y.fIndex)
            {
                return Derivative(y, x);
            }

            if (fOp == Op.Add)
            {
                return fExpr1.Derivative(x, y) + fExpr2.Derivative(x, y);
            }
            else if (fOp == Op.Sub)
            {
                return fExpr1.Derivative(x, y) - fExpr2.Derivative(x, y);
            }
            else if (fOp == Op.Mul)
            {
                return fExpr1.Derivative(x, y) * fExpr2.Eval()
                    + fExpr1.Derivative(x) * fExpr2.Derivative(y)
                    + fExpr1.Derivative(y) * fExpr2.Derivative(x)
                    + fExpr1.Eval() * fExpr2.Derivative(x, y);
            }
            else if (fOp == Op.Div)
            {
                return fExpr1.Derivative(x, y) / fExpr2.Eval()
                    + fExpr1.Derivative(x) * (-fExpr2.Derivative(y) / fExpr2.Eval() / fExpr2.Eval())
                    + fExpr1.Derivative(y) * (-fExpr2.Derivative(x) / fExpr2.Eval() / fExpr2.Eval())
                    + fExpr1.Eval() * (
                    -fExpr2.Derivative(x, y) / fExpr2.Eval() / fExpr2.Eval()
                    + 2 * fExpr2.Derivative(x) * fExpr2.Derivative(y) / fExpr2.Eval() / fExpr2.Eval() / fExpr2.Eval()
                    );
            }
            else
            {
                throw new Exception();
            }
        }

        Expression fExpr1;
        Expression fExpr2;
        Op fOp;
    }


    class PowExpression : Expression
    {
        public PowExpression(Op op,Expression exp1, int n) { fE = n; fExpr = exp1; fOp = op; }
        override public void OnVariablesChanged(Calculator cal) {
            fExpr.OnVariablesChanged(cal);
        }

        override public double Eval()
        {
            if (fOp == Op.Pow)
            {
                return Math.Pow(fExpr.Eval(), fE);
            }
            else
            {
                throw new Exception();
            }
        }

        override public double Derivative(Variable x)
        {
            if (fOp == Op.Pow)
            {
                return fE * fExpr.Derivative(x) * Math.Pow(fExpr.Eval(), fE - 1);
            }
            else
            {
                throw new Exception();
            }
        }
        override public double Derivative(Variable x, Variable y)
        {
            if (x.fIndex > y.fIndex)
            {
                return Derivative(y, x);
            }

            if (fOp == Op.Pow)
            {
                return fE * fExpr.Derivative(x, y) * Math.Pow(fExpr.Eval(), fE - 1)
                    + fE * (fE - 1) * fExpr.Derivative(x)* fExpr.Derivative(y) * Math.Pow(fExpr.Eval(), fE - 2);
            }
            else
            {
                throw new Exception();
            }
        }
        Op fOp;
        Expression fExpr;
        int fE;
    }
    class UnitaryExpression : Expression
    {
       public  UnitaryExpression(Op op, Expression exp) { fOp = op; fExpr = exp; }
        override public void OnVariablesChanged(Calculator cal) {
            fExpr.OnVariablesChanged(cal);
        }

        override public double Eval()
        {
            if (fOp == Op.Negative)
            {
                return -fExpr.Eval();
            }
            else if (fOp == Op.Positive)
            {
                return +fExpr.Eval();
            }
            else
            {
                throw new Exception();
            }
        }

        override public double Derivative(Variable x)
        {
            if (fOp == Op.Negative)
            {
                return -fExpr.Derivative(x);
            }
            else if (fOp == Op.Positive)
            {
                return +fExpr.Derivative(x);
            }
            else
            {
                throw new Exception();
            }
        }
        override public double Derivative(Variable x, Variable y)
        {
            if (fOp == Op.Negative)
            {
                return -fExpr.Derivative(x, y);
            }
            else if (fOp == Op.Positive)
            {
                return +fExpr.Derivative(x, y);
            }
            else
            {
                throw new Exception();
            }
        }

        Expression fExpr;
        Op fOp;
    }

    class IDExpression : Expression
    {

        public IDExpression(Variable var) { fVariable = var; }
        Variable fVariable;

        override public void OnVariablesChanged(Calculator cal) {
        }

        override public double Eval()
        {
            return fVariable.fVal;
        }

        override public double Derivative(Variable x)
        {
            if (x == fVariable)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        override public double Derivative(Variable x, Variable y)
        {
            return 0;
        }
    }

    class NumberExpression : Expression
    {
        public NumberExpression(double v) { fValue = v; }
        public double fValue;

        override public void OnVariablesChanged(Calculator cal) { }

        override public double Eval()
        {
            return fValue;
        }

        override public double Derivative(Variable x)
        {
            return 0;
        }

        override public double Derivative(Variable x, Variable y)
        {
            return 0;
        }
    }

    public class Variable
    {
        public Variable(String name)
        {
            fName = name;
        }
        public double fVal;
        public int fIndex;
        public String fName;    
    }

    class CharIterator
    {
        CharEnumerator fEn;
        bool fValid;
        char fVal;
        public CharIterator(String str)
        {
            fEn = str.GetEnumerator();
            fValid = true;
            MoveNext();
        }
        public char Value() { return fVal; }
        public void MoveNext() {
            if (!fValid) throw new Exception();
            fValid = fEn.MoveNext();
            if (fValid)
                fVal = fEn.Current;
        }
        public bool Valid() { return fValid; }
    }


    public class Minimizer
    {
        public Minimizer(Calculator cal, int maxInter = 20) {
            fCal = cal;
            fMaxInter = maxInter;
        }

        public Dictionary<String,double> Minimize(out double min)
        {
            min = 0;
            var fRoot = fCal.fRoot;
            var allVars = fCal.fVars;

            for (int n = 0; n < fMaxInter; ++n)
            {
                var vars = allVars;
                if (n == 0) {
                    vars = new HashSet<Variable>();
                    int i = 0;
                    foreach(var v in allVars)
                    {
                        if(v.fName.Contains("@"))
                            vars.Add(v);
                    }
                }

                int nVar = vars.Count;

                Matrix qm = new Matrix(nVar, nVar);
                Matrix d = new Matrix(nVar, 1);
                Matrix deltaV = new Matrix(nVar, 1);

                fRoot.OnVariablesChanged(fCal);

                foreach (var v in vars)
                {
                    d[v.fIndex, 0] = fRoot.Derivative(v);
                }
                foreach (var v1 in vars)
                {
                    foreach (var v2 in vars)
                    {
                        if (v1.fIndex <= v2.fIndex)
                        {
                            qm[v1.fIndex, v2.fIndex] = fRoot.Derivative(v1, v2);
                            qm[v2.fIndex, v1.fIndex] = qm[v1.fIndex, v2.fIndex];
                        }
                    }
                }
                // q(delta v) + d = 0;
                // deltaV = q^-1 * (-d)
                deltaV =  qm.Invert()*(-d);
                foreach (var v in vars)
                {
                    v.fVal += deltaV[v.fIndex, 0];
                }
                min = fRoot.Eval();
            }

            fResults.Clear();
            foreach (var v in allVars)
            {
                fResults.Add(v.fName, v.fVal);
            }
            return fResults;
        }

        private Calculator fCal;
        private int fMaxInter;
        private Dictionary<string, double> fResults = new Dictionary<string, double>();
    }
}
