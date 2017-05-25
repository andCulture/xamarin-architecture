using CoreGraphics;
using Foundation;
using SyncedCare.Mobile.Presentation.iOS.Extensions;
using System;
using System.IO;
using UIKit;
using SyncedCare.Mobile.Core.ViewLogic;
using SyncedCare.Mobile.Core.DataAccess;
using LocalAuthentication;
using SyncedCare.Mobile.Core.ViewModels.Users;
using System.Linq;

namespace SyncedCare.Mobile.Presentation.iOS.Helpers
{
    public static class UIUtilities
    {
        #region Properties

        public static UIImage DefaultAvatar
        {
            get
            {
                return GetDefaultAvatarImage();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the avatar image for a care team member.
        /// </summary>
        /// <param name="fileName">The avatar image file name.</param>
        /// <param name="userId">The user id (for decrypting)</param>
        /// <returns>An avatar image for a care team member.</returns>
        public static UIImage GetAvatarImage(string fileName, string userId)
        {
			if (!string.IsNullOrEmpty(fileName))
			{
				var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				var imagePath = Path.Combine(documentsDirectory, fileName);
				if (!string.IsNullOrEmpty(fileName) && File.Exists(imagePath))
				{
					var cyptoAlg = SharedViewLogic.GetCyptoAlgorithm(userId);
					var decryptedBytes = SharedViewLogic.DecryptBytes(cyptoAlg, File.ReadAllBytes(imagePath));
					return decryptedBytes.ToImage();
				}
			}
            return DefaultAvatar;
        }

        /// <summary>
        /// Pushes the user to the supplied view controller.
        /// </summary>
        /// <typeparam name="T">The type of the view controller to take the user to.</typeparam>
        /// <param name="storyboard">The storyboard that contains the view controller</param>
        /// <param name="navigationController">An instance of the navigation controller.</param>
        public static void PushToScreen<T>(UIStoryboard storyboard, UINavigationController navigationController)
		{
			var typeName = typeof(T).ToString();
			var controllerName = typeName.Contains(".") ? typeName.Substring(typeName.LastIndexOf(".") + 1) : typeName;
			var viewController = storyboard.InstantiateViewController(controllerName);
			Convert.ChangeType(viewController, typeof(T));
			if (viewController != null)
			{
				navigationController.PushViewController(viewController, true);
			}
		}

        /// <summary>
        /// Generates an image of a solid color with the supplied dimenations.
        /// </summary>
        /// <param name="color">The color of the image.</param>
        /// <param name="size">The dimensions of the image.</param>
        /// <returns>A rectangular image of a solid color.</returns>
        public static UIImage CreateImageFromColor(UIColor color, CGSize size)
        {
            var rect = new CGRect(new CGPoint(0,0), size);
            UIGraphics.BeginImageContextWithOptions(size, false, 0);
            color.SetFill();
            UIGraphics.RectFill(rect);
            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return image;
        }

        public static UIButton GetPrimaryButton(string text)
        {
            var button = UIButton.FromType(UIButtonType.RoundedRect);
            button.TranslatesAutoresizingMaskIntoConstraints = false;
            button.SetTitle(text, UIControlState.Normal);
            button.SetTitleColor(AppColors.WHITE.ToUIColor(), UIControlState.Normal);
            button.TitleLabel.Font = UIFont.SystemFontOfSize(13.5f, UIFontWeight.Medium);
            button.BackgroundColor = AppColors.PRIMARY_BLUE.ToUIColor();
            button.Layer.CornerRadius = 6.0f;
            // Add the shadow
            button.Layer.ShadowColor = AppColors.DARKEST_GRAY.ToUIColor().CGColor;
            button.Layer.ShadowOpacity = 0.25f;
            button.Layer.ShadowRadius = 2;
            button.Layer.ShadowOffset = new CGSize(0f, 2f);

            return button;
        }

		/// <summary>
		/// Saves an image to disk. The image is encrypted prior to being saved.
		/// </summary>
		/// <param name="image">The image to save</param>
		/// <param name="fileName">File name.</param>
		/// <param name="userId">User id.</param>
		/// <param name="onSaveComplete">On save complete action</param>
		/// <param name="scaleTo">Size to scale to (optional)</param>
        public static void SaveImage(UIImage image, string fileName, string userId, Action<UIImage, byte[], string> onSaveComplete, CGSize scaleTo)
        {
			if (scaleTo != image.Size)
			{
				image = image.Scale(scaleTo);
			}
            var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var imagePath = Path.Combine(documentsDirectory, fileName);
            NSError err = null;
			using (NSData imgData = image.AsJPEG())
			{
				var imgBytes = new byte[imgData.Length];
				System.Runtime.InteropServices.Marshal.Copy(imgData.Bytes, imgBytes, 0, Convert.ToInt32(imgData.Length));
				var encryptionAlg = SharedViewLogic.GetCyptoAlgorithm(userId);
				var encryptedBytes = SharedViewLogic.EncryptBytes(encryptionAlg, imgBytes);
				// Save the encrypted image.
				File.WriteAllBytes(imagePath, encryptedBytes);
				if (onSaveComplete != null && err == null)
                {
                    onSaveComplete(image, imgBytes, imagePath);
                }
			}
        }

	     /// <summary>
	     /// Calls a phone number with the app that is configured to handle telephone requests.
	     /// </summary>
	     /// <param name="phoneNumber"></param>
	     public static void CallPhoneNumber(string phoneNumber)
	     {
			// Bail if empty
			if (string.IsNullOrEmpty(phoneNumber))
			{
				return;
			}
			// Extract digits only
			var trimmed = new string(phoneNumber.Where(c => char.IsDigit(c)).ToArray());
			// Bail if not vaild phone
			if (string.IsNullOrEmpty(trimmed) || trimmed.Length < 7)
			{
				return;
			}
			try
			{
				var url = new NSUrl("tel://" + trimmed);
				if (UIApplication.SharedApplication.CanOpenUrl(url))
				{
					UIApplication.SharedApplication.OpenUrl(url);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Unable to call phone number: {trimmed}");
				Console.Write(ex);
			}
	     }

        /// <summary>
        /// Will try to open the url in the devices default browser
        /// </summary>
        /// <param name="url">The url to open</param>
        public static void OpenUrl(string url)
        {
            var urlToOpen = new NSUrl(url);
            if (UIApplication.SharedApplication.CanOpenUrl(urlToOpen))
            {
                UIApplication.SharedApplication.OpenUrl(urlToOpen);
            }
        }

        /// <summary>
        /// Will try to open the map in Google Maps if installed. If not it falls back to apple maps.
        /// </summary>
        /// <param name="address"></param>
        public static void OpenMap(string address)
        {
            var encoded = System.Net.WebUtility.UrlEncode(address);
            var google = new NSUrl("comgooglemaps://?q=" + encoded);
            var apple = new NSUrl("http://maps.apple.com?q=" + encoded);
            if (UIApplication.SharedApplication.CanOpenUrl(new NSUrl("comgooglemaps://")))
            {
                UIApplication.SharedApplication.OpenUrl(google);
            }
            else if(UIApplication.SharedApplication.CanOpenUrl(apple))
            {
                UIApplication.SharedApplication.OpenUrl(apple);
            }
        }

        /// <summary>
        /// Gets the default avatar image from file.
        /// </summary>
        /// <returns>A UI Image of the avatar.</returns>
        public static UIImage GetDefaultAvatarImage()
        {
            return UIImage.FromFile("Images/Avatar.jpg");
        }

		/// <summary>
		/// Adds a gesture recognizer to the view to dismiss the keyboard when editing ends.
		/// </summary>
		/// <param name="view">The view to apply the gesture recognizer to.</param>
		public static void ApplyKeyboardDismissToView(this UIView view, UIView viewToCheck)
		{
			var g = new UITapGestureRecognizer(() =>
			{
				var firstResponder = GetFirstResponder(viewToCheck);
				if (firstResponder != null)
				{
					firstResponder.ResignFirstResponder();
				}
			});
			g.CancelsTouchesInView = false; //for iOS5
			view.AddGestureRecognizer(g);
		}

		/// <summary>
		/// Gets the first responder for a view (recursive).
		/// </summary>
		/// <returns>The first responder.</returns>
		/// <param name="view">The view to search.</param>
		public static UIView GetFirstResponder(UIView view)
		{
			if (view.IsFirstResponder)
			{
				return view;
			}
			if (view.Subviews != null)
			{
				foreach (var v in view.Subviews)
				{
					var firstResponder = GetFirstResponder(v);
					if (firstResponder != null)
					{
						return firstResponder;
					}
				}
			}
			return null;
		}

        public static UIBarButtonItem CreateNavigationBarButton(string buttonText, CGSize buttonSize, EventHandler onTap)
        {
            var btn = UIButton.FromType(UIButtonType.Custom);
            btn.TouchUpInside += onTap;
            btn.SetTitle(buttonText, UIControlState.Normal);
            btn.SetTitleColor(AppColors.PRIMARY_BLUE.ToUIColor(), UIControlState.Normal);
            btn.Frame = new CGRect(0, 0, buttonSize.Width, buttonSize.Height);
            var tbButton = new UIBarButtonItem(btn);
            return tbButton;
        }

		/// <summary>
		/// Gets the next and previous subviews based on the current index.
		/// </summary>
		/// <returns>The next and previous subviews.</returns>
		/// <param name="container">The UI view containing the child subviews.</param>
		/// <param name="index">The current subview index to base next and previous off of.</param>
		public static SurroundingSubviews GetNextAndPreviousSubviews(UIView container, int index)
		{
			if (container?.Subviews?.Length > 0)
			{
				var previous = index == 0 || container.Subviews.Length == 1 ? null : container.Subviews[index - 1];
				var next = index == container.Subviews.Length - 1 ? null : container.Subviews[index + 1];
				return new SurroundingSubviews(previous, next);
			}
			return null;
		}

		/// <summary>
		/// Creates a local notification for daily to-do reminder.
		/// </summary>
		/// <param name="date">The date of the reminder.</param>
		/// <param name="todoCount">The number of to-dos (including missed) for the supplied date.</param>
		public static void ScheduleTodoNotification(DateTime date, int todoCount)
		{
			// Made the scheduled datetime 8:00 AM on the date that was supplied.
			var scheduleDate = new DateTime(date.Year, date.Month, date.Day, 8, 0, 0);
			// If today then fire the alert immediately.
			if (date.Date == SharedViewLogic.Today.ToUniversalTime().Date)
			{
				scheduleDate = DateTime.Now;
			}
			if (scheduleDate.Date >= SharedViewLogic.Today.ToUniversalTime().Date && todoCount > 0)
			{
				var notification = new UILocalNotification() 
				{ 
					FireDate = scheduleDate.ToNSDate(),
					AlertTitle = "To-do Reminder", // required for Apple Watch notifications
					AlertAction = "To-do Reminder",
					AlertBody = $"You have {todoCount} to-dos today.",
					ApplicationIconBadgeNumber = todoCount,
					SoundName = UILocalNotification.DefaultSoundName
				};

				UIApplication.SharedApplication.ScheduleLocalNotification(notification);				
			}
		}

		/// <summary>
		/// Cancels all future local notifications
		/// </summary>
		public static void CancelFutureNotifications()
		{
			if (UIApplication.SharedApplication.ScheduledLocalNotifications != null)
			{
				foreach (var n in UIApplication.SharedApplication.ScheduledLocalNotifications)
				{
					if (n.FireDate.ToDateTime().Date > SharedViewLogic.Today.Date)
					{
						UIApplication.SharedApplication.CancelLocalNotification(n);
					}
				}
			}
		}

		/// <summary>
		/// Updates the secure data in keychain and sets the user's touch id
		/// preferences in the database.
		/// </summary>
		/// <param name="userId">The user's Id</param>
		/// <param name="usernameValue">The value to set the username as (blank to remove).</param>
		/// <param name="passwordValue">The value to set the username as (blank to remove).</param>
		/// <param name="isEnabled">Whether touch id is enabled for the app.</param>
		/// <returns>If the update was successful.</returns>
		public static bool UpdateTouchIdSettings(string userId, string usernameValue, string passwordValue, bool isEnabled)
		{
			if (UpdateKeyChainData(usernameValue, passwordValue))
			{
				UserDataAccess.UpdateTouchIdSetting(userId, isEnabled);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Updates the secure data in keychain.
		/// </summary>
		/// <param name="username">The value to set the username as (blank to remove).</param>
		/// <param name="password">The value to set the username as (blank to remove).</param>
		/// <returns>If the update was successful.</returns>
		public static bool UpdateKeyChainData(string username, string password)
		{
			var secureData = KeychainHelper.GetSecureDataFromKeychain();
			if (secureData != null)
			{
				secureData.Username = username;
				secureData.Password = password;
				return KeychainHelper.StoreSecureDataInKeychain(secureData);
			}
			return false;
		}

		/// <summary>
		/// Prompts for touch id authenticaion.
		/// </summary>
		/// <param name="responseAction">Code to execute after a response is received from Touch ID.</param>
		public static LAContext PromptForTouchID(LAContextReplyHandler responseAction)
		{
			
			var context = new LAContext();
			context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, "Logging in with Touch ID", responseAction);
			return context;
		}

		/// <summary>
		/// Prompts user for notifications.
		/// </summary>
		public static void RequestPushNotificationSetting()
		{
			// Ask user for local notifications
			var settings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Sound | UIUserNotificationType.Alert | UIUserNotificationType.Badge, null);
			UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
		}

		/// <summary>
		/// Unsubscribes from remote notifications.
		/// </summary>
		public static void UnsubscribeFromRemoteNotification(UserViewModel user, Action unregisterForRemoteNotifications, Action<string, string> onError)
		{
			// Unregister from remote notifications
			if (unregisterForRemoteNotifications != null)
			{
				unregisterForRemoteNotifications();
			}
			SharedViewLogic.UpdatePushNotifications(user, false, onError);
		}

		/// <summary>
		/// Subscribes to remote notifications.
		/// </summary>
		public static void SubscribeForRemoteNotification(UserViewModel user, Action registerForRemoteNotifications, Action<string, string> onError)
		{
			if (registerForRemoteNotifications != null)
			{
				registerForRemoteNotifications();
			}
			SharedViewLogic.UpdatePushNotifications(user, true, onError);
		}

		/// <summary>
		/// Gets a text field.
		/// </summary>
		/// <returns>The text field.</returns>
		/// <param name="placeholder">Placeholder text.</param>
		/// <param name="fontSize">The font size</param>
		/// <param name="isSecure">If set to <c>true</c> the field is treated as a password entry.</param>
		public static UITextField GetTranslucentTextField(string placeholder, float fontSize, bool isSecure = false)
		{
			var placeholderAttr = new NSAttributedString(placeholder, new UIStringAttributes { Font = UIFont.SystemFontOfSize(fontSize, UIFontWeight.Regular), ForegroundColor = AppColors.WHITE.ToUIColor(.5f) });
			var leftViewForPadding = new UIView(new CGRect(0, 0, 21, 45));
			var textField = new UITextField()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				AttributedPlaceholder = placeholderAttr,
				Font = UIFont.SystemFontOfSize(fontSize, UIFontWeight.Regular),
				TextColor = AppColors.WHITE.ToUIColor(),
				BorderStyle = UITextBorderStyle.None,
				ClipsToBounds = true,
				SecureTextEntry = isSecure,
				LeftView = leftViewForPadding,
				LeftViewMode = UITextFieldViewMode.Always
			};
			textField.Layer.BorderWidth = 1f;
			textField.Layer.CornerRadius = 6f;
			textField.Layer.MasksToBounds = true;
			StyleTranslucentFieldAsUnfocused(textField);
			return textField;
		}

		/// <summary>
		/// Styles the field as unfocused.
		/// </summary>
		/// <param name="field">Field.</param>
		public static void StyleTranslucentFieldAsUnfocused(UITextField field)
		{
			UIView.Animate(.3f, () =>
			{
				field.BackgroundColor = AppColors.WHITE.ToUIColor(.06f);
				field.Layer.BorderColor = AppColors.WHITE.ToUIColor(.3f).CGColor;
			});
		}

		/// <summary>
		/// Styles the field as focused.
		/// </summary>
		/// <param name="field">Field.</param>
		public static void StyleTranslucentFieldAsFocused(UITextField field)
		{
			UIView.Animate(.3f, () =>
			{
				field.BackgroundColor = AppColors.WHITE.ToUIColor(.2f);
				field.Layer.BorderColor = AppColors.WHITE.ToUIColor().CGColor;
			});
		}

		#endregion Methods
	}
}