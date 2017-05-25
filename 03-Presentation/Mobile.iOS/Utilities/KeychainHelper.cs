using Foundation;
using Newtonsoft.Json;
using Security;
using SyncedCare.Mobile.Core.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncedCare.Mobile.Presentation.iOS.Helpers
{
    public static class KeychainHelper
    {
		private static string KEY = "synced_care";
        /// <summary>
        /// Adds an entry into key chain. If a duplicate is found, it is removed & re-added.
        /// </summary>
        /// <param name="secureValues">Object containing secure values</param>
        /// <returns>Whether the key chain entry was added successfully.</returns>
        public static bool StoreSecureDataInKeychain(SecureValuesViewModel secureValues)
        {
			var success = false;
            var s = new SecRecord(SecKind.GenericPassword)
            {
                ValueData = NSData.FromString(JsonConvert.SerializeObject(secureValues)),
                Generic = NSData.FromString(KEY),
				AccessGroup = "H29ZBDYNSU.com.syncedcare.patientapp"
            };
            var err = SecKeyChain.Add(s);
            if(err == SecStatusCode.DuplicateItem)
            {
                err = SecKeyChain.Remove(s);
				if (err == SecStatusCode.Success)
				{
					return StoreSecureDataInKeychain(secureValues);
				}
            }
            else if(err == SecStatusCode.Success)
            {
                success = true;
            }
            return success;
        }

        /// <summary>
        /// Removes an entry from Keychain
        /// </summary>
        /// <returns>Whether or not the key was removed successfully.</returns>
        public static bool RemoveSecureDataFromKeychain()
        {
            var success = false;
            var s = new SecRecord(SecKind.GenericPassword)
            {
                ValueData = NSData.FromString(string.Empty),
                Generic = NSData.FromString(KEY)
            };
            var err = SecKeyChain.Remove(s);

            if (err == SecStatusCode.Success)
            {
                success = true;
            }
            return success;
        }

        /// <summary>
        /// Gets a string value from keychain for the supplied key.
        /// </summary>
        /// <returns>The value of the entry as a string.</returns>
        public static SecureValuesViewModel GetSecureDataFromKeychain()
        {
            SecureValuesViewModel result = null;
            SecStatusCode res;
            var rec = new SecRecord(SecKind.GenericPassword)
            {
                Generic = NSData.FromString(KEY)
            };
            var match = SecKeyChain.QueryAsRecord(rec, out res);
            if (match != null && match.ValueData != null)
            {
                result = JsonConvert.DeserializeObject<SecureValuesViewModel>(match.ValueData.ToString());
            }

            return result;
        }
    }
}
