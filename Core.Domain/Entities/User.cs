using Core.Domain.Attributes;
using Core.Domain.Entities.Base;
using System;

namespace Core.Domain.Entities {
    public class User : BaseEntity<User> {
        #region Properties
        [Unique]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AvatarImageUrl { get; set; }
        public string Bio { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsFavorite { get; set; }
        #endregion
        public User() { }
    }
}