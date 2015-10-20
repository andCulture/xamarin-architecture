using Core.Domain.ViewModels.Abstracts;

namespace Core.Domain.ViewModels {
    public class UserDetailItem : UserItem {
        #region Additional Properties
        #endregion

        public UserDetailItem(Entities.User user) : base(user) {
            // Any additional properties get set here
        }
    }
}
