/*******************************************************************************
  * Copyright (c) 2018 Premier Crop Systems, LLC
  * All rights reserved. This program and the accompanying materials
  * are made available under the terms of the Eclipse Public License v1.0
  * which accompanies this distribution, and is available at
  * http://www.eclipse.org/legal/epl-v20.html
  *
  * Contributors:
  *    Keith Reimer - Initial version.
  *******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PremierCrop.ADAPT.Rest
{
    /// <summary>
    /// A client to wrap <see cref="HttpClient"/> class to use <see cref="ReferenceLink"/>s to generate URLs and deserialize JSON objects.
    /// </summary>
    public class ReferenceLinkClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseAddress;

        public ReferenceLinkClient(HttpClient httpClient, string apiBaseAddress)
        {
            _httpClient = httpClient;
            _apiBaseAddress = apiBaseAddress;
        }

        /// <summary>
        /// Gets a single object by looking in the 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="links"></param>
        /// <param name="rel"></param>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<ModelEnvelope<T>> GetObjectByRel<T>(IEnumerable<ReferenceLink> links, string rel = null, params object[] queryParams) where T : class
        {
            rel = rel ?? typeof(T).ObjectRel();
            var link = links.Single(l => l.Rel == rel);
            return await Get<ModelEnvelope<T>>(link.Link, queryParams);
        }

        public async Task<IReadOnlyCollection<ModelEnvelope<T>>> GetObjectsByMultipleRels<T>(IEnumerable<ReferenceLink> links, params object[] queryParams) where T : class
        {
            var rel = typeof(T).ObjectRel();
            var relLinks = links.Where(l => l.Rel == rel);
            var list = new List<ModelEnvelope<T>>();
            foreach (var link in relLinks)
            {
                var x = await Get<ModelEnvelope<T>>(link.Link, queryParams);
                list.Add(x);
            }

            return list.AsReadOnly();
        }

        public async Task<IReadOnlyCollection<ModelEnvelope<T>>> GetListByRel<T>(IEnumerable<ReferenceLink> links, params object[] queryParams) where T : class
        {
            var link = links.Single(l => l.Rel == typeof(T).ListRel());

            return await Get<IReadOnlyCollection<ModelEnvelope<T>>>(link.Link, queryParams);
        }

        public async Task<T> Get<T>(string url, params object[] queryParams) where T : class
        {
            foreach (var p in queryParams)
            {
                url += $"/{p}";
            }
            var response = await _httpClient.GetStringAsync($"{_apiBaseAddress}{url}");
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}
