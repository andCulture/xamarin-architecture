using Core.Domain.ViewModels.Abstracts;

namespace Core.Domain.ViewModels {
    public class UserListItem : UserItem {
        #region Additional Properties
        #endregion

        public UserListItem(Entities.User user) : base(user) {
            // Any additional properties get set here
        }
    }
}
