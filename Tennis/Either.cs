using System;

namespace Tennis
{
	internal interface IEither<TL, out TR>
	{
		bool IsLeft();
		bool IsRight();
		TL Left();
		TR Right();
		IEither<TL, TO> Map<TO>(Func<TR, TO> mapFunc);
		IEither<TL, TO> Chain<TO>(Func<TR, IEither<TL, TO>> chainFunc);
	}

	class Either<TL, TR> : IEither<TL, TR>
	{
		static public Either<TL, TR> Left(TL left)
		{
			return new Either<TL, TR>(left);
		}

		static public Either<TL, TR> Right(TR right)
		{
			return new Either<TL, TR>(right);
		}

		private Either(TL left)
		{
			if (left == null)
				throw new ArgumentNullException(nameof(left));

			_left = left;
			_right = default(TR);
			_isLeft = true;
		}

		private Either(TR right)
		{
			if (right == null)
				throw new ArgumentNullException(nameof(right));

			_left = default(TL);
			_right = right;
			_isLeft = false;
		}

		private readonly TL _left;
		private readonly TR _right;
		private readonly bool _isLeft;

		public bool IsLeft()
		{
			return _isLeft;
		}

		public bool IsRight()
		{
			return !_isLeft;
		}

		public TL Left()
		{
			if (IsRight())
				throw new InvalidOperationException();

			return _left;
		}

		public IEither<TL, TO> Map<TO>(Func<TR, TO> mapFunc)
		{
			return IsLeft()
				? Either<TL, TO>.Left(Left())
				: Either<TL, TO>.Right(mapFunc(Right()));
		}

		public IEither<TL, TO> Chain<TO>(Func<TR, IEither<TL, TO>> chainFunc)
		{
			return IsLeft()
				? Either<TL, TO>.Left(Left())
				: chainFunc(Right());
		}

		public TR Right()
		{
			if (IsLeft())
				throw new InvalidOperationException();

			return _right;
		}
	}
}
