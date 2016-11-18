namespace Ramda.NET
{
	public static partial class R
	{	
		public static dynamic Traverse<TTraversable>(DynamicDelegate of, DynamicDelegate f, TTraversable traversable) {
			return Currying.Traverse(of, f, traversable);
		}

		public static dynamic Traverse<TTraversable>(RamdaPlaceholder of, DynamicDelegate f, TTraversable traversable) {
			return Currying.Traverse(of, f, traversable);
		}

		public static dynamic Traverse<TTraversable>(DynamicDelegate of, RamdaPlaceholder f, TTraversable traversable) {
			return Currying.Traverse(of, f, traversable);
		}

		public static dynamic Traverse(DynamicDelegate of, DynamicDelegate f, RamdaPlaceholder traversable = null) {
			return Currying.Traverse(of, f, traversable);
		}

		public static dynamic Traverse(RamdaPlaceholder of = null, RamdaPlaceholder f = null, RamdaPlaceholder traversable = null) {
			return Currying.Traverse(of, f, traversable);
		}
	}
}