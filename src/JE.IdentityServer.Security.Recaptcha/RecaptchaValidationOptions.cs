﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using JE.IdentityServer.Security.OpenIdConnect;
using JE.IdentityServer.Security.Resources;

namespace JE.IdentityServer.Security.Recaptcha
{
    public class RecaptchaValidationOptions : IOpenIdConnectRequestOptions, IIdentityServerRecaptchaOptions
    {
        public RecaptchaValidationOptions()
        {
            ExcludedSubnets = Enumerable.Empty<IPNetwork>();
            VerifyUri = new Uri("https://www.google.com/recaptcha/api/siteverify");
            ExcludedSubnets = Enumerable.Empty<IPNetwork>();
            WebClients = Enumerable.Empty<IOpenIdConnectClient>();
            LinkToChallenge = "/recaptcha.aspx";
        }

        public IEnumerable<IPNetwork> ExcludedSubnets { get; set; }

        public string ProtectedPath { get; set; }

        public IEnumerable<string> ProtectedGrantTypes { get; set; }

        public int NumberOfAllowedLoginFailuresPerIpAddress { get; set; }

        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }

        public Uri VerifyUri { get; set; }

        public string LinkToChallenge { get; set; }

        public string ContentServerName { get; set; }

        public bool SupportBrowsersWithoutJavaScript { get; set; }

        public IEnumerable<IOpenIdConnectClient> WebClients { get; set; }

        public HttpStatusCode HttpChallengeStatusCode { get; set; }

        public Regex ExcludedUsernameExpression { get; set; }

        public Regex ExcludedTenantExpression { get; set; }

        public Regex ExcludedOsVersionExpression { get; set; }

        public Regex ExcludedDeviceExpression { get; set; }
    }
}