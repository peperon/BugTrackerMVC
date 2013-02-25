using System;
using System.Collections.Specialized;
using System.Web;

namespace BugTracker.Tests.WebUI.Fakes
{

    public class FakeHttpRequest : HttpRequestBase
    {
        private readonly NameValueCollection _formParams;
        private readonly NameValueCollection _queryStringParams;
        private readonly HttpCookieCollection _cookies;

        private bool? _isAuthenticated;

        public FakeHttpRequest(NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection cookies)
        {
            _formParams = formParams;
            _queryStringParams = queryStringParams;
            _cookies = cookies;
        }

        public override NameValueCollection Form
        {
            get
            {
                return _formParams;
            }
        }

        public override NameValueCollection QueryString
        {
            get
            {
                return _queryStringParams;
            }
        }

        public override HttpCookieCollection Cookies
        {
            get
            {
                return _cookies;
            }
        }

        public override bool IsAuthenticated
        {
            get
            {
                if (_isAuthenticated.HasValue)
                    return _isAuthenticated.Value;
                return base.IsAuthenticated;
            }
        }

        public void SetIsAuthenticated(bool value)
        {
            _isAuthenticated = value;
        }

    }



}
