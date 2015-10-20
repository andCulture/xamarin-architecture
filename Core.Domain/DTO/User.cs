using System;
using System.Collections.Generic;

namespace Core.Domain.DTO {
    public class User {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AvatarImageUrl { get; set; }
        public string Bio { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Entities.User ConvertToEntity() {
            return new Entities.User() {
                UserId = Id,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                AvatarImageUrl = AvatarImageUrl,
                Bio = Bio,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt
            };
        }
    }

    public class UserResponse {
        public List<User> Rows { get; set; }
        public int TotalCount { get; set; }
    }
}