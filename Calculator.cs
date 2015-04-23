namespace Kontur.Courses.Git
{
	public class Calculator
	{
		private Maybe<double> lastResult = 0;

		public Maybe<double> Calculate(string[] args)
		{
			if (args.Length == 0)
				return lastResult;
			if (args.Length == 1)
				return lastResult = double.Parse(args[0]);
			if (args.Length == 2)
			{
                var op = args[0];
                var parseResult = double.TryParse(args[1], out var v2);
                if (parseResult)
                    return lastResult = Execute(op, lastResult.Value, v2);

				return lastResult = Maybe<double>.FromError("Error input");
			}
			if (args.Length == 3)
			{
				var v1 = TryParseDouble(args[0]);
				var v2 = TryParseDouble(args[2]);
				if (!v1.HasValue) return v1;
				if (!v2.HasValue) return v2;
				return lastResult = Execute(args[1], v1.Value, v2.Value);
			}
			return Maybe<double>.FromError("Error input");
		}

		private Maybe<double> TryParseDouble(string s)
		{
			double v;
			if (double.TryParse(s, out v))
				return v;
			return Maybe<double>.FromError("Not a number '{0}'", s);
		}

		private Maybe<double> Execute(string op, double v1, double v2)
		{
			if (op == "+")
				return v1 + v2;
			if (op == "-")
				return v1 - v2;
			if (op == "*")
				return v1 - v2;
			if (op == "/")
				return v1 / v2;
			return Maybe<double>.FromError("Unknown operation '{0}'", op);
		}
	}
}