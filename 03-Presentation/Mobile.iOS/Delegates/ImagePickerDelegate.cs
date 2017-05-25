using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace SyncedCare.Mobile.Presentation.iOS.Delegates
{
    public class ImagePickerDelegate : UIImagePickerControllerDelegate
    {
        #region Variables

        private Action<NSDictionary> _callback;
        private Action _onCancel;

        #endregion Variables

        #region Properties

        public Action<NSDictionary> Callback
        {
            get
            {
                return _callback;
            }

            set
            {
                _callback = value;
            }
        }

        public Action OnCancel
        {
            get
            {
                return _onCancel;
            }

            set
            {
                _onCancel = value;
            }
        }

        #endregion Properties

        public ImagePickerDelegate(Action<NSDictionary> callback, Action onCancel)
        {
            _callback = callback;
            _onCancel = onCancel;
        }

        public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
        {
            picker.DismissModalViewController(true);
            if (_callback != null)
            {
                _callback(info);
            }
        }

        public override void Canceled(UIImagePickerController picker)
        {
            if (_onCancel != null)
            {
                _onCancel();
            }
        }
    }
}
