using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using JE.IdentityServer.Security.Resolver;
using JE.IdentityServer.Security.Services;
using JE.IdentityServer.Security.Throttling;
using Microsoft.Owin.Testing;

namespace JE.IdentityServer.Security.Tests.Infrastructure
{
    public class IdentityServerWithThrottledLoginRequests : IDisposable
    {
        private string _protectedPath = "/identity/connect/token";
        private readonly LoginStatisticsStub _loginStatistics = new LoginStatisticsStub();
        private int _numberOfAllowedLoginFailures;
        private readonly IList<Regex> _excludedUsernameExpressions = new List<Regex>();
        private readonly IList<string> _protectedGrantTypes = new List<string>();
        private TestServer _testServer;

        public LoginStatisticsStub LoginStatistics => _loginStatistics;

        public IdentityServerWithThrottledLoginRequests WithProtectedPath(string protectedPath)
        {
            _protectedPath = protectedPath;
            return this;
        }

        public IdentityServerWithThrottledLoginRequests WithNumberOfAllowedLoginFailures(int numberOfAllowedLoginFailures)
        {
            _numberOfAllowedLoginFailures = numberOfAllowedLoginFailures;
            return this;
        }

        public IdentityServerWithThrottledLoginRequests WithProtectedGrantType(string protectedGrantType)
        {
            _protectedGrantTypes.Add(protectedGrantType);
            return this;
        }

        public IdentityServerWithThrottledLoginRequests WithExcludedUsernameExpression(string excludedUsernameExpression)
        {
            _excludedUsernameExpressions.Add(new Regex(excludedUsernameExpression));
            return this;
        }

        public TestServer Build()
        {
            _testServer = TestServer.Create(app =>
            {
                app.UsePerOwinContext<ILoginStatistics>(() => _loginStatistics);
                app.UseThrottlingForAuthenticationRequests(new IdentityServerThrottlingOptions
                {
                    ProtectedPath = _protectedPath,
                    NumberOfAllowedLoginFailures = _numberOfAllowedLoginFailures,
                    ExcludedUsernameExpressions = _excludedUsernameExpressions,
                    ProtectedGrantTypes = _protectedGrantTypes,
                });
                app.UseInMemoryIdentityServer();
            });

            return _testServer;
        }

        public void Dispose()
        {
            _testServer?.Dispose();
        }
    }
}