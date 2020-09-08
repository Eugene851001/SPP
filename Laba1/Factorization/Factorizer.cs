using Tracert;

namespace Factorization
{
    class Factorizer
	{ 
		public ITracer tracer;

		public Factorizer(ITracer tracer)
		{
			this.tracer = tracer;
		}

		bool isSimple(long n)
		{
			tracer.StartTrace();
			long divider = 2;
			while (divider <= System.Math.Sqrt(n))
			{
				if (n % divider == 0)
				{
					tracer.StopTrace();
					return false;
				}
				divider++;
			}
			tracer.StopTrace();
			return true;
		}
		long fastPow(long a, long z, long n)
		{
			long x = 1;
			while (z != 0)
			{
				while (z % 2 == 0)
				{
					z = z / 2;
					a = (a * a) % n;
				}
				z--;
				x = (x * a) % n;
			}
			return x;
		}

		bool isSimpleRabin(long n)
		{
			System.Random random = new System.Random();
			long a = random.Next() % (n - 3) + 2;
			return fastPow(a, n, n) == a % n;
		}

		public int factorize(long n, Divider[] dividers)
		{
			tracer.StartTrace();
			long simpleDivider = 2;
			int divCounter = 0;
			while (n != 1)
			{
				if (n % simpleDivider == 0)
				{
					dividers[divCounter].Base = simpleDivider;
					dividers[divCounter].Degree = 0;
					while (n % simpleDivider == 0)
					{
						n = n / simpleDivider;
						dividers[divCounter].Degree++;
					}
					divCounter++;
				}
				do
				{
					simpleDivider++;
				} while (!isSimple(simpleDivider));
			}
			tracer.StopTrace();
			return divCounter;
		}
	}
}
