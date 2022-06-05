import { group, sleep } from 'k6';
import http from 'k6/http';

export const options = {
    vus: 10,
    duration: '30s',
};

export default function() {

	group("API tests", function() {
		let req, res;
		req = [
		// {
		// 	"method": "post",
		// 	"url": "http://127.0.0.1:5002/SignIn",
		// 	"body": "{\"username\":\"massaynus\",\"password\":\"jhon4freedom\"}",
		// 	"params": {
		// 		"headers": {
		// 			"Host": "127.0.0.1:5002",
		// 			"User-Agent": "Mozilla/5.0 (X11; Linux x86_64; rv:101.0) Gecko/20100101 Firefox/101.0",
		// 			"Accept": "*/*",
		// 			"Accept-Language": "en-US,en;q=0.5",
		// 			"Accept-Encoding": "gzip, deflate, br",
		// 			"Content-Type": "application/json",
		// 			"Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9wcml2YXRlcGVyc29uYWxpZGVudGlmaWVyIjoiZTAyMDkyMjctNTNhMi00MzRmLTViNDEtMDhkYTQ1NjIwNTE1IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJ1MSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE2NTQzODUyMDksImlzcyI6IkNoYXRBUEkuQXV0aG9yaXR5IiwiYXVkIjoiQ2hhdEFQSS5BdWRpZW5jZSJ9.ixjhz1hOxB4PaZzwwqCKyBK3FwmYLg5aBSVvUaero6w",
		// 			"Origin": "http://127.0.0.1:3000",
		// 			"Connection": "keep-alive",
		// 			"Referer": "http://127.0.0.1:3000/",
		// 			"Sec-Fetch-Dest": "empty",
		// 			"Sec-Fetch-Mode": "cors",
		// 			"Sec-Fetch-Site": "same-site"
		// 		}
		// 	}
		// },
		{
			"method": "post",
			"url": "http://127.0.0.1:5002/SignIn",
			"body": "{\"username\":\"u1\",\"password\":\"P1\"}",
			"params": {
				"headers": {
					"Host": "127.0.0.1:5002",
					"User-Agent": "Mozilla/5.0 (X11; Linux x86_64; rv:101.0) Gecko/20100101 Firefox/101.0",
					"Accept": "*/*",
					"Accept-Language": "en-US,en;q=0.5",
					"Accept-Encoding": "gzip, deflate, br",
					"Content-Type": "application/json",
					"Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9wcml2YXRlcGVyc29uYWxpZGVudGlmaWVyIjoiZTAyMDkyMjctNTNhMi00MzRmLTViNDEtMDhkYTQ1NjIwNTE1IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJ1MSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE2NTQzODUyMDksImlzcyI6IkNoYXRBUEkuQXV0aG9yaXR5IiwiYXVkIjoiQ2hhdEFQSS5BdWRpZW5jZSJ9.ixjhz1hOxB4PaZzwwqCKyBK3FwmYLg5aBSVvUaero6w",
					"Origin": "http://127.0.0.1:3000",
					"Connection": "keep-alive",
					"Referer": "http://127.0.0.1:3000/",
					"Sec-Fetch-Dest": "empty",
					"Sec-Fetch-Mode": "cors",
					"Sec-Fetch-Site": "same-site"
				}
			}
		},
		// {
		// 	"method": "get",
		// 	"url": "http://127.0.0.1:5002/api/Users/GetUsers/0",
		// 	"params": {
		// 		"headers": {
		// 			"Host": "127.0.0.1:5002",
		// 			"User-Agent": "Mozilla/5.0 (X11; Linux x86_64; rv:101.0) Gecko/20100101 Firefox/101.0",
		// 			"Accept": "*/*",
		// 			"Accept-Language": "en-US,en;q=0.5",
		// 			"Accept-Encoding": "gzip, deflate, br",
		// 			"Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9wcml2YXRlcGVyc29uYWxpZGVudGlmaWVyIjoiZTAyMDkyMjctNTNhMi00MzRmLTViNDEtMDhkYTQ1NjIwNTE1IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJ1MSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE2NTQzODUyMDksImlzcyI6IkNoYXRBUEkuQXV0aG9yaXR5IiwiYXVkIjoiQ2hhdEFQSS5BdWRpZW5jZSJ9.ixjhz1hOxB4PaZzwwqCKyBK3FwmYLg5aBSVvUaero6w",
		// 			"Origin": "http://127.0.0.1:3000",
		// 			"Connection": "keep-alive",
		// 			"Referer": "http://127.0.0.1:3000/",
		// 			"Sec-Fetch-Dest": "empty",
		// 			"Sec-Fetch-Mode": "cors",
		// 			"Sec-Fetch-Site": "same-site"
		// 		}
		// 	}
		// },
		// {
		// 	"method": "get",
		// 	"url": "http://127.0.0.1:5002/api/Users/Statuses",
		// 	"params": {
		// 		"headers": {
		// 			"Host": "127.0.0.1:5002",
		// 			"User-Agent": "Mozilla/5.0 (X11; Linux x86_64; rv:101.0) Gecko/20100101 Firefox/101.0",
		// 			"Accept": "*/*",
		// 			"Accept-Language": "en-US,en;q=0.5",
		// 			"Accept-Encoding": "gzip, deflate, br",
		// 			"Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9wcml2YXRlcGVyc29uYWxpZGVudGlmaWVyIjoiZTAyMDkyMjctNTNhMi00MzRmLTViNDEtMDhkYTQ1NjIwNTE1IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZpZXIiOiJ1MSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IlVzZXIiLCJleHAiOjE2NTQzODUyMDksImlzcyI6IkNoYXRBUEkuQXV0aG9yaXR5IiwiYXVkIjoiQ2hhdEFQSS5BdWRpZW5jZSJ9.ixjhz1hOxB4PaZzwwqCKyBK3FwmYLg5aBSVvUaero6w",
		// 			"Origin": "http://127.0.0.1:3000",
		// 			"Connection": "keep-alive",
		// 			"Referer": "http://127.0.0.1:3000/",
		// 			"Sec-Fetch-Dest": "empty",
		// 			"Sec-Fetch-Mode": "cors",
		// 			"Sec-Fetch-Site": "same-site"
		// 		}
		// 	}
		// }
	];

		res = http.batch(req);
	});

}
