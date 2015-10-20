using Core.Domain.Attributes;
using Core.Domain.Interfaces.Application;

namespace Core.Domain.Entities.Base {
    public abstract class BaseEntity<T> : IEntity<T> where T : IEntity<T> {
        [PrimaryKey, AutoIncrement]
        public virtual int Id { get; set; }

        public override bool Equals(object obj) {
            var other = obj as BaseEntity<T>;
            if (other == null) {
                return false;
            }
            if (IsNew() && other.IsNew()) {
                return ReferenceEquals(this, other);
            }
            return Id.Equals(other.Id);
        }

        private int? oldHashCode;
        public override int GetHashCode() {
            if (oldHashCode.HasValue) {
                return oldHashCode.Value;
            }
            if (IsNew()) {
                oldHashCode = base.GetHashCode();
                return oldHashCode.Value;
            }
            return Id.GetHashCode();
        }

        public static bool operator ==(BaseEntity<T> lEntity, BaseEntity<T> rEntity) {
            return Equals(lEntity, rEntity);
        }

        public static bool operator !=(BaseEntity<T> lEntity, BaseEntity<T> rEntity) {
            return !Equals(lEntity, rEntity);
        }

        public virtual bool IsNew() {
            return Id.Equals(default(int));
        }
    }
}
