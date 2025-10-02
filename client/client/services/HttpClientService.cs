using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace client.services;

public class HttpClientService
{
    public HttpClient _HttpClient { get; set; }

    public HttpClientService()
    {
        _HttpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:7084/api/")
        };
    }
}
