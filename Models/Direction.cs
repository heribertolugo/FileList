using System;

namespace FileList.Models
{
    public struct Direction : IEquatable<int>, IEquatable<Direction>
    {
        public static Direction None = new Direction(-1);
        public static Direction Up = new Direction(0);
        public static Direction Down = new Direction(1);

        private int _value;
        private Direction(int value)
        {
            this._value = value;
        }

        public Direction(int newValue, int oldValue)
        {
            int result = newValue - oldValue;

            if (result == 0)
                this._value = Direction.None._value;
            else if (result < 0)
                this._value = Direction.Up._value;
            else
                this._value = Direction.Down._value;
        }

        public bool Equals(int other)
        {
            return this._value == other;
        }

        public bool Equals(Direction other)
        {
            return this._value == other._value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is Direction))
                return false;
            return this.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this._value;
        }

        public override string ToString()
        {
            switch (this._value)
            {
                case 0:
                    return "Up";
                case 1:
                    return "Down";
                default:
                    return "None";
            }
        }

        public static explicit operator Direction(int i)
        {
            if (i == 0)
                return Direction.Up;
            if (i == 1)
                return Direction.Down;
            return Direction.None;
        }
        public static bool operator ==(Direction d1, Direction d2)
        {
            return d1.Equals(d2);
        }

        public static bool operator !=(Direction d1, Direction d2)
        {
            return !d1.Equals(d2);
        }
    }
}
