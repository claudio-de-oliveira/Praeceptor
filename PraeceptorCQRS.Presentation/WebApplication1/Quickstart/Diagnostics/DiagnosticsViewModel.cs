// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Ardalis.GuardClauses;

using IdentityModel;

using Microsoft.AspNetCore.Authentication;

using Newtonsoft.Json;

using System.Text;

namespace IdentityServer.Api.Quickstart.UI
{
    public class DiagnosticsViewModel
    {
        public DiagnosticsViewModel(AuthenticateResult result)
        {
            AuthenticateResult = result;

            Guard.Against.Null(result.Properties);
            if (result.Properties.Items.ContainsKey("client_list"))
            {
                var encoded = result.Properties.Items["client_list"];
                var bytes = Base64Url.Decode(encoded);
                var value = Encoding.UTF8.GetString(bytes);

                var o = JsonConvert.DeserializeObject<string[]>(value);
                Guard.Against.Null(o);
                Clients = o;
            }
        }

        public AuthenticateResult AuthenticateResult { get; }
        public IEnumerable<string> Clients { get; } = new List<string>();
    }
}