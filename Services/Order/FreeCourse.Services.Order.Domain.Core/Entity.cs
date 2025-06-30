using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.Core
{
    public abstract class Entity
    {
        public int Id { get; protected set; }

        // Equality sadece Id üzerinden değerlendirilir.
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;

            Entity entity = (Entity)obj;
            return entity.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !Equals(left, right);
        }
    }
}
