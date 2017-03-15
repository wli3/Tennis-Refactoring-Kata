using System;

namespace Tennis
{
	internal interface IOldEither<TL, out TR>
	{
		bool IsLeft();
		bool IsRight();
		TL Left();
		TR Right();
		IOldEither<TL, TO> Map<TO>(Func<TR, TO> mapFunc);
		IOldEither<TL, TO> Chain<TO>(Func<TR, IOldEither<TL, TO>> chainFunc);
	}

	class OldEither<TL, TR> : IOldEither<TL, TR>
	{
		static public OldEither<TL, TR> Left(TL left)
		{
			return new OldEither<TL, TR>(left);
		}

		static public OldEither<TL, TR> Right(TR right)
		{
			return new OldEither<TL, TR>(right);
		}

		private OldEither(TL left)
		{
			if (left == null)
				throw new ArgumentNullException(nameof(left));

			_left = left;
			_right = default(TR);
			_isLeft = true;
		}

		private OldEither(TR right)
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

		public IOldEither<TL, TO> Map<TO>(Func<TR, TO> mapFunc)
		{
			return IsLeft()
				? OldEither<TL, TO>.Left(Left())
				: OldEither<TL, TO>.Right(mapFunc(Right()));
		}

		public IOldEither<TL, TO> Chain<TO>(Func<TR, IOldEither<TL, TO>> chainFunc)
		{
			return IsLeft()
				? OldEither<TL, TO>.Left(Left())
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
