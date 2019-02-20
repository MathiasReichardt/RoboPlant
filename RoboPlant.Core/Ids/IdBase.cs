﻿using System;

namespace RoboPlant.Domain.Ids
{
    public abstract class IdBase
    {
        public Guid Value { get; }

        protected IdBase(Guid value)
        {
            Value = value;
        }

        protected IdBase()
        {
            Value = Guid.NewGuid();
        }

        public static bool operator ==(IdBase l, IdBase r)
        {
            return l.Value == r.Value;
        }

        public static bool operator !=(IdBase l, IdBase r)
        {
            return !(l == r);
        }

        protected bool Equals(IdBase other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IdBase) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}