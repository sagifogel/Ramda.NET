namespace Ramda.NET
{
	public static partial class R
	{
        /// <summary>
		/// Maps an [Applicative](https://github.com/fantasyland/fantasy-land#applicative)-returningfunction over a [Traversable](https://github.com/fantasyland/fantasy-land#traversable),then uses [`sequence`](#sequence) to transform the resulting Traversable of Applicativeinto an Applicative of Traversable.Dispatches to the `sequence` method of the third argument, if present.
		/// <para />
		/// sig: (Applicative f, Traversable t) => (a -> f a) -> (a -> f b) -> t a -> f (t b)
		/// </summary>
		/// <param name="of">first</param>
		/// <param name="f">second</param>
		/// <param name="traversable">third</param>
		/// <returns>*</returns>
		/// <see cref="R.Sequence"/>
        public static dynamic Traverse<TTraversable>(DynamicDelegate of, DynamicDelegate f, TTraversable traversable) {
			return Currying.Traverse(of, f, traversable);
		}

        /// <summary>
		/// Maps an [Applicative](https://github.com/fantasyland/fantasy-land#applicative)-returningfunction over a [Traversable](https://github.com/fantasyland/fantasy-land#traversable),then uses [`sequence`](#sequence) to transform the resulting Traversable of Applicativeinto an Applicative of Traversable.Dispatches to the `sequence` method of the third argument, if present.
		/// <para />
		/// sig: (Applicative f, Traversable t) => (a -> f a) -> (a -> f b) -> t a -> f (t b)
		/// </summary>
		/// <param name="of">first</param>
		/// <param name="f">second</param>
		/// <param name="traversable">third</param>
		/// <returns>*</returns>
		/// <see cref="R.Sequence"/>
		public static dynamic Traverse<TTraversable>(RamdaPlaceholder of, DynamicDelegate f, TTraversable traversable) {
			return Currying.Traverse(of, f, traversable);
		}

        /// <summary>
		/// Maps an [Applicative](https://github.com/fantasyland/fantasy-land#applicative)-returningfunction over a [Traversable](https://github.com/fantasyland/fantasy-land#traversable),then uses [`sequence`](#sequence) to transform the resulting Traversable of Applicativeinto an Applicative of Traversable.Dispatches to the `sequence` method of the third argument, if present.
		/// <para />
		/// sig: (Applicative f, Traversable t) => (a -> f a) -> (a -> f b) -> t a -> f (t b)
		/// </summary>
		/// <param name="of">first</param>
		/// <param name="f">second</param>
		/// <param name="traversable">third</param>
		/// <returns>*</returns>
		/// <see cref="R.Sequence"/>
		public static dynamic Traverse<TTraversable>(DynamicDelegate of, RamdaPlaceholder f, TTraversable traversable) {
			return Currying.Traverse(of, f, traversable);
		}

        /// <summary>
		/// Maps an [Applicative](https://github.com/fantasyland/fantasy-land#applicative)-returningfunction over a [Traversable](https://github.com/fantasyland/fantasy-land#traversable),then uses [`sequence`](#sequence) to transform the resulting Traversable of Applicativeinto an Applicative of Traversable.Dispatches to the `sequence` method of the third argument, if present.
		/// <para />
		/// sig: (Applicative f, Traversable t) => (a -> f a) -> (a -> f b) -> t a -> f (t b)
		/// </summary>
		/// <param name="of">first</param>
		/// <param name="f">second</param>
		/// <param name="traversable">third</param>
		/// <returns>*</returns>
		/// <see cref="R.Sequence"/>
		public static dynamic Traverse(DynamicDelegate of, DynamicDelegate f, RamdaPlaceholder traversable = null) {
			return Currying.Traverse(of, f, traversable);
		}

        /// <summary>
		/// Maps an [Applicative](https://github.com/fantasyland/fantasy-land#applicative)-returningfunction over a [Traversable](https://github.com/fantasyland/fantasy-land#traversable),then uses [`sequence`](#sequence) to transform the resulting Traversable of Applicativeinto an Applicative of Traversable.Dispatches to the `sequence` method of the third argument, if present.
		/// <para />
		/// sig: (Applicative f, Traversable t) => (a -> f a) -> (a -> f b) -> t a -> f (t b)
		/// </summary>
		/// <param name="of">first</param>
		/// <param name="f">second</param>
		/// <param name="traversable">third</param>
		/// <returns>*</returns>
		/// <see cref="R.Sequence"/>
		public static dynamic Traverse(RamdaPlaceholder of = null, RamdaPlaceholder f = null, RamdaPlaceholder traversable = null) {
			return Currying.Traverse(of, f, traversable);
		}
	}
}