using Core.Application.Injection;
using Core.Application.Interfaces.Services;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Core.Application.Services {

    #region Endpoints
    public class WebRequestEndpoints {
        public const string USER_GET = "{locale}/api_endpoint_to_get_users";
    }
    #endregion

    public class WebRequestService : ServiceBase, IWebRequestService {
        #region Properties
        private IConfigService _configService = Injector.Resolve<IConfigService>();
        private IUserService _userService = Injector.Resolve<IUserService>();
        #endregion

        #region Public Methods
        public void LoadAndCacheData() {
            var client = new RestClient(_configService.APIEndpoint());
            LoadAndCacheUsers(client);
        }
        #endregion

        #region Private Methods

        private void LoadAndCacheUsers(RestClient client) {
            var response = client.Execute<Domain.DTO.UserResponse>(BuildRequest(
                WebRequestEndpoints.USER_GET,
                Method.GET
            ));

            if (response.ResponseStatus == ResponseStatus.Completed && response.StatusCode == System.Net.HttpStatusCode.OK) {
                if (response.Data != null && response.Data.TotalCount > 0) {
                    var users = new List<Domain.Entities.User>();
                    foreach (var item in response.Data.Rows) {
                        var typedUser = item.ConvertToEntity();
                        var savedUser = _userService.Get(typedUser.UserId);
                        if (savedUser != null) {
                            typedUser.Id = savedUser.Id;
                        }

                        if (!item.DeletedAt.Equals(DateTime.MinValue)) {
                            // If file upload has been deleted, destroy it from our database
                            if (savedUser != null) {
                                _userService.Delete(savedUser.Id);
                            }
                            continue;
                        }

                        if (savedUser == null || (savedUser.UpdatedAt < savedUser.UpdatedAt)) {
                            users.Add(typedUser);
                        }
                    }
                    if (users.Count > 0) {
                        _userService.SaveAll(users);
                    }
                }
            } else {
                //throw new System.Exception(response.StatusCode + response.StatusDescription);
            }
        }

        private RestRequest BuildRequest(string endpoint, Method method,
                Dictionary<string, string> urlParams = null,
                Dictionary<string, string> inputParams = null,
                Dictionary<string, string> headerParams = null) {
            var request = new RestRequest(endpoint, method);

            if (urlParams == null) {
                urlParams = new Dictionary<string, string>();
            }
            urlParams.Add("locale", _configService.Locale());

            foreach (var entry in urlParams) {
                request.AddUrlSegment(entry.Key, entry.Value);
            }
            if (inputParams != null) {
                foreach (var entry in inputParams) {
                    request.AddParameter(entry.Key, entry.Value);
                }
            }
            if (headerParams != null) {
                foreach (var entry in headerParams) {
                    request.AddHeader(entry.Key, entry.Value);
                }
            }

            return request;
        }
        #endregion
    }
}
