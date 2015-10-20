namespace Core.Domain.ViewModels.Abstracts {
    public abstract class UserItem {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName {
            get {
                return FirstName + " " + LastName;
            }
        }
        public bool IsFavorite { get; set; }

        protected UserItem(Entities.User user) {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            IsFavorite = user.IsFavorite;
        }
    }
}
